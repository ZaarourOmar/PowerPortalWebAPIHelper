using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using PowerPortalWebAPIHelper.Models;
using static System.Windows.Forms.CheckedListBox;
using System.Workflow.ComponentModel.Compiler;
using System.Windows.Controls;
using System.Activities.Expressions;

namespace PowerPortalWebAPIHelper
{
    public partial class PortalAPIHelperPluginControl : PluginControlBase
    {

        #region Variables
        private Settings mySettings;
        private EntityItemModel SelectedEntityInfo { get; set; } = new EntityItemModel();
        private List<WebsiteModel> Websites = new List<WebsiteModel>();
        public List<EntityItemModel> AllEntitiesList { get; set; } = new List<EntityItemModel>();
        public bool InnerErrorEnabled { get; set; } = false;
        public Guid InnerErrorSiteSettingsId { get; set; }
        private Guid TargetWebsiteId { get; set; } = Guid.Empty;
        #endregion
        #region Plugin Control
        private void PortalAPIHelperPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/ZaarourOmar/PowerPortalWebAPIHelper"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }

            ExecuteMethod(LoadWebsites);
        }
        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }
        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }
        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        #endregion

        #region Constructor
        public PortalAPIHelperPluginControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Entity Management
        private void LoadAllEntities()
        {
            AllEntitiesList.Clear();
            lstBxAllEntities.Items.Clear();
            txtAllEntitiesFilter.Text = "";

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Entities",
                Work = (worker, args) =>
                {

                    RetrieveAllEntitiesRequest retrieveAllEntityRequest = new RetrieveAllEntitiesRequest
                    {
                        RetrieveAsIfPublished = true,
                        EntityFilters = EntityFilters.Entity
                    };
                    RetrieveAllEntitiesResponse retrieveAllEntityResponse = (RetrieveAllEntitiesResponse)Service.Execute(retrieveAllEntityRequest);

                    args.Result = retrieveAllEntityResponse.EntityMetadata;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error while retrieving the entities from the environment.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var entityMetaDataArray = args.Result as EntityMetadata[];
                    if (entityMetaDataArray != null)
                    {
                        foreach (EntityMetadata entityMetadata in entityMetaDataArray)
                        {
                            if (!MetadataValidator.IsValidEntity(entityMetadata)) continue;

                            EntityItemModel entityItemModel = new EntityItemModel(entityMetadata);
                            AllEntitiesList.Add(entityItemModel);
                            lstBxAllEntities.Items.Add(entityItemModel);
                        }
                    }
                }
            });
        }
        private void CleanupPreviousEntityInformationPanel()
        {
            // Clear previous attribute lists
            chkdLstBxAllAttibutes.Items.Clear();
            EntityInformationContainer.Visible = false;
            SelectedEntityInfo = new EntityItemModel();
            //txtAllEntitiesFilter.Text = "";
            //txtAttributeFilter.Text = "";
        }
        private void ShowEntityInformationPanel()
        {
            EntityInformationContainer.Visible = true;
            tsbSwitchInnerError.Visible = true;
        }
        private void InitializeEnabledCheckBoxBasedOnSiteSetting(Entity enabledSiteSettingEntity)
        {
            chkBxIsWebAPIEnabled.Checked = false;
            if (enabledSiteSettingEntity != null)
            {
                SelectedEntityInfo.WebAPIEnabledSiteSettingId = enabledSiteSettingEntity.Id;
                if (enabledSiteSettingEntity.GetAttributeValue<string>("adx_value").ToLower() == "true")
                {
                    chkBxIsWebAPIEnabled.Checked = true;
                }
                else
                {
                    chkBxIsWebAPIEnabled.Checked = false;
                }
            }
            else
            {
                SelectedEntityInfo.WebAPIEnabledSiteSettingId = Guid.Empty;
            }
        }
        private void InitializeInnerErrorSwitchBasedOnSiteSetting(Entity innerErrorSiteSetting)
        {
            tsbSwitchInnerError.Visible = true;

            if (innerErrorSiteSetting == null)
            {
                tsbSwitchInnerError.Text = "Enable Inner Error Tracking";
                InnerErrorEnabled = false;
                InnerErrorSiteSettingsId = Guid.Empty;
                return;
            }
            bool isInternalErrorEnabled = innerErrorSiteSetting.GetAttributeValue<string>("adx_value").ToLower() == "true";
            if (isInternalErrorEnabled)
            {
                tsbSwitchInnerError.Text = "Disable Inner Error Tracking";
                InnerErrorEnabled = true;
            }
            else
            {
                tsbSwitchInnerError.Text = "Enable Inner Error Tracking";
                InnerErrorEnabled = false;
            }
            InnerErrorSiteSettingsId = innerErrorSiteSetting.Id;
        }
        private void btnSaveEntityChanges_Click(object sender, EventArgs e)
        {
            bool isEnabled = chkBxIsWebAPIEnabled.Checked;


            if (SelectedEntityInfo.WebAPIEnabledSiteSettingId == Guid.Empty)
            {
                CreateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, SelectedEntityInfo.LogicalName, isEnabled.ToString().ToLower(), TargetWebsiteId);
            }
            else
            {
                UpdateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, SelectedEntityInfo.WebAPIEnabledSiteSettingId, SelectedEntityInfo.LogicalName, isEnabled.ToString().ToLower(), TargetWebsiteId);
            }


            // get the fields setting value;
            StringBuilder fieldsSettingValue = new StringBuilder();
            // if all are selected, set the setting value to "*"
            if (chkdLstBxAllAttibutes.CheckedItems.Count == chkdLstBxAllAttibutes.Items.Count)
            {
                fieldsSettingValue.Append("*");
            }
            else
            {
                foreach (AttributeItemModel checkedItemModel in SelectedEntityInfo.SelectedAttributesList)
                {
                    fieldsSettingValue.Append(checkedItemModel.LogicalName);
                    if (SelectedEntityInfo.SelectedAttributesList.IndexOf(checkedItemModel) == SelectedEntityInfo.SelectedAttributesList.Count - 1) break;
                    fieldsSettingValue.Append(",");
                }
                if (SelectedEntityInfo.WebAPIFieldsSiteSettingId == Guid.Empty)
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, SelectedEntityInfo.LogicalName, fieldsSettingValue.ToString().ToLower(), TargetWebsiteId);
                }
                else
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, SelectedEntityInfo.WebAPIFieldsSiteSettingId, SelectedEntityInfo.LogicalName, fieldsSettingValue.ToString().ToLower(), TargetWebsiteId);
                }
            }

        }
        private void txtAllEntitiesFilter_TextChanged(object sender, EventArgs e)
        {
            var itemList = AllEntitiesList.Cast<EntityItemModel>().ToList();
            if (itemList.Count > 0)
            {
                //clear the items from the list
                lstBxAllEntities.Items.Clear();

                //filter the items and add them to the list
                lstBxAllEntities.Items.AddRange(
                    itemList.Where(i => i.DisplayName.ToLower().Contains(txtAllEntitiesFilter.Text.ToLower())).ToArray());
            }
            else
            {
                lstBxAllEntities.Items.AddRange(itemList.ToArray());
            }
        }

        private void LoadAllEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadAllEntities);

        }
        private void AllEntitiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleanupPreviousEntityInformationPanel();
            EntityItemModel selectedEntity = lstBxAllEntities.SelectedItem as EntityItemModel;
            if (selectedEntity != null)
            {
                SelectedEntityInfo = selectedEntity;
                lblEntityLogicalName.Text = $"Entity Logical Name: {SelectedEntityInfo.LogicalName}";
                lblEntityDisplayName.Text = $"Entity Dsplay Name: {SelectedEntityInfo.DisplayName}";
                ExecuteMethod(LoadSelectedEntityAttributes, SelectedEntityInfo.LogicalName);
                ShowEntityInformationPanel();
            }

        }
        #endregion

        #region Attribute Management
        private void LoadSelectedEntityAttributes(string logicalName)
        {
            txtAttributeFilter.Text = "";

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting entity attributes",
                Work = (worker, args) =>
                {

                    RetrieveEntityRequest entityRequest = new RetrieveEntityRequest();
                    entityRequest.LogicalName = logicalName;
                    entityRequest.RetrieveAsIfPublished = true;
                    entityRequest.EntityFilters = EntityFilters.All;
                    var response = (RetrieveEntityResponse)Service.Execute(entityRequest);

                    foreach (var attribute in response.EntityMetadata.Attributes)
                    {
                        if (!MetadataValidator.IsValidAttribute(attribute)) 
                            continue; // we don't want calculated fields.

                        AttributeItemModel newAttribute = new AttributeItemModel(attribute);
                        SelectedEntityInfo.AllAttributesList.Add(newAttribute);
                        chkdLstBxAllAttibutes.Items.Add(newAttribute);
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ExecuteMethod(LoadEntityWebAPISettings, logicalName);
                }
            });
        }
        private void InitializeAttributesBasedOnSiteSetting(Entity attributeSiteSettingEntity)
        {
            if (attributeSiteSettingEntity != null)
            {
                SelectedEntityInfo.WebAPIFieldsSiteSettingId = attributeSiteSettingEntity.Id;
                var fieldsString = attributeSiteSettingEntity.GetAttributeValue<string>("adx_value");
                if (string.IsNullOrEmpty(fieldsString)) return;


                // if the value is star, all fields are selecteed
                if (fieldsString == "*")
                {
                    for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                    {
                        chkdLstBxAllAttibutes.SetItemChecked(i, true);
                    }
                }

                var fieldsArray = fieldsString.Split(',');
                SelectedEntityInfo.SelectedAttributesList.Clear();
                for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                {
                    var found = fieldsArray.FirstOrDefault(x => x == ((AttributeItemModel)chkdLstBxAllAttibutes.Items[i]).LogicalName);
                    if (found != null)
                    {
                        chkdLstBxAllAttibutes.SetItemChecked(i, true);
                        SelectedEntityInfo.SelectedAttributesList.Add((AttributeItemModel)chkdLstBxAllAttibutes.Items[i]);
                    }
                }

            }
            else
            {
                SelectedEntityInfo.WebAPIFieldsSiteSettingId = Guid.Empty;
            }
        }
        private void EntityAttributesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var changedAttribute = (sender as CheckedListBox).SelectedItem as AttributeItemModel;
            var selectedIndex = (sender as CheckedListBox).SelectedIndex;
            if (chkdLstBxAllAttibutes.GetItemChecked(selectedIndex) == true)
            {
                SelectedEntityInfo.SelectedAttributesList.Add(changedAttribute);
            }
            else
            {
                if (SelectedEntityInfo.SelectedAttributesList.Find(x => x == changedAttribute) != null)
                {
                    SelectedEntityInfo.SelectedAttributesList.Remove(changedAttribute);
                }
            }

        }
        private void txtAttributeFilter_TextChanged(object sender, EventArgs e)
        {
            var itemList = SelectedEntityInfo.AllAttributesList.Cast<AttributeItemModel>().ToList();
            if (itemList.Count > 0)
            {
                //clear the items from the list
                chkdLstBxAllAttibutes.Items.Clear();

                //filter the items and add them to the list
                chkdLstBxAllAttibutes.Items.AddRange(
                    itemList.Where(i => i.DisplayName.ToLower().Contains(txtAttributeFilter.Text.ToLower())).ToArray());

                foreach (AttributeItemModel item in SelectedEntityInfo.SelectedAttributesList)
                {
                    if (chkdLstBxAllAttibutes.Items.IndexOf(item) >= 0)
                        chkdLstBxAllAttibutes.SetItemChecked(chkdLstBxAllAttibutes.Items.IndexOf(item), true);

                }

            }
            else
            {
                chkdLstBxAllAttibutes.Items.AddRange(itemList.ToArray());
            }

        }


        #endregion

        #region Inner Error Management

        private void LoadInnerErrorTrackingSettings()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting site setting for the inner error tracking",
                Work = (worker, args) =>
                {

                    // Get sitesettings for this entity that are related to the web api setup
                    QueryExpression siteSettingsQuery = new QueryExpression("adx_sitesetting");
                    siteSettingsQuery.ColumnSet = new ColumnSet("adx_name", "adx_value");
                    FilterExpression innerErrorFilter = new FilterExpression(LogicalOperator.And);
                    innerErrorFilter.AddCondition("adx_websiteid", ConditionOperator.Equal, TargetWebsiteId);
                    innerErrorFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/error/innererror");
                    siteSettingsQuery.Criteria.AddFilter(innerErrorFilter);

                    var siteSettingsRecords = Service.RetrieveMultiple(siteSettingsQuery).Entities;
                    var innerErrorSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/error/innererror");

                    InitializeInnerErrorSwitchBasedOnSiteSetting(innerErrorSiteSettingEntity);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });
        }
        private void tsbSwitchInnerError_Click(object sender, EventArgs e)
        {
            if (InnerErrorSiteSettingsId != Guid.Empty)
            {
                UpdateSiteSetting(WebAPISiteSettingTypes.InnerError, InnerErrorSiteSettingsId, null, (InnerErrorEnabled ? "false" : "true"), TargetWebsiteId);
                InnerErrorEnabled = !InnerErrorEnabled;
                tsbSwitchInnerError.Text = InnerErrorEnabled ? "Disable Inner Error Tracking" : "Enable Inner Error Tracking";

            }
            else
            {
                CreateSiteSetting(WebAPISiteSettingTypes.InnerError, null, "true", TargetWebsiteId);
                InnerErrorEnabled = true;
                tsbSwitchInnerError.Text = "Disable Inner Error Tracking";
            }
        }
        #endregion

        #region Snippets Management
        private void btnGenerateSnippets_Click(object sender, EventArgs e)
        {
            PopulateSnippets();
        }
        private void PopulateSnippets()
        {
            rchTxtBxWrapperFunction.Text = SnippetsGenerator.GenerateWrapperFunction();
            rchTxtBxCreate.Text = SnippetsGenerator.GenerateCreateSnippet(SelectedEntityInfo.CollectionName, SelectedEntityInfo.SelectedAttributesList);
            rchTxtBxUpdate.Text = SnippetsGenerator.GenerateUpdateSnippet(SelectedEntityInfo.CollectionName, SelectedEntityInfo.SelectedAttributesList);
            rchTxtBxDelete.Text = SnippetsGenerator.GenerateDeleteSnippet(SelectedEntityInfo.CollectionName);
        }

        #endregion
        #region Website Management
        private void LoadWebsites()
        {

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Websites",
                Work = (worker, args) =>
                {
                    QueryExpression websiteQuery = new QueryExpression("adx_website");
                    websiteQuery.ColumnSet = new ColumnSet(new string[] { "adx_name", "adx_websiteid" });
                    args.Result = Service.RetrieveMultiple(websiteQuery);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Result != null)
                    {
                        var entities = (args.Result as EntityCollection).Entities;
                        foreach (Entity webSite in entities)
                        {
                            Guid websiteId = webSite.Id;
                            string websiteName = webSite.GetAttributeValue<string>("adx_name");
                            tsbWebsiteList.ComboBox.DisplayMember = "Name";
                            tsbWebsiteList.ComboBox.ValueMember = "Id";
                            tsbWebsiteList.Items.Add(new WebsiteModel(websiteName, websiteId));
                        }

                        tsbWebsiteList.SelectedIndex = 0;

                        LoadInnerErrorTrackingSettings();

                    }
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error while retrieving the websites from the environment.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });


        }
        private void tsbWebsiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebsiteModel selectedWebsite = tsbWebsiteList.SelectedItem as WebsiteModel;
            if (selectedWebsite != null)
            {
                TargetWebsiteId = selectedWebsite.Id;

                CleanupPreviousEntityInformationPanel();
                LoadAllEntities();
                LoadInnerErrorTrackingSettings();

                //AllEntitiesListBox_SelectedIndexChanged(this, null);
            }
        }


        #endregion

        #region Sitesettings Management

        private void LoadEntityWebAPISettings(string logicalName)
        {

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting site settings for the selected entity",
                Work = (worker, args) =>
                {

                    // Get sitesettings for this entity that are related to the web api setup
                    QueryExpression siteSettingsQuery = new QueryExpression("adx_sitesetting");
                    siteSettingsQuery.ColumnSet = new ColumnSet("adx_name", "adx_value");
                    FilterExpression webApiNameFilter = new FilterExpression(LogicalOperator.Or);
                    webApiNameFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/{logicalName}/");
                    webApiNameFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/error/innererror");
                    siteSettingsQuery.Criteria.AddFilter(webApiNameFilter);
                    FilterExpression webApiWebsiteFilter = new FilterExpression(LogicalOperator.And);
                    webApiWebsiteFilter.AddCondition("adx_websiteid", ConditionOperator.Equal, TargetWebsiteId);
                    siteSettingsQuery.Criteria.AddFilter(webApiWebsiteFilter);

                    //retreive the settings based on the above query
                    var siteSettingsRecords = Service.RetrieveMultiple(siteSettingsQuery).Entities;
                    var innerErrorSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/error/innererror");
                    var enabledSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/enabled");
                    var attributeSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/fields");

                    InitializeInnerErrorSwitchBasedOnSiteSetting(innerErrorSiteSettingEntity);
                    InitializeEnabledCheckBoxBasedOnSiteSetting(enabledSiteSettingEntity);
                    InitializeAttributesBasedOnSiteSetting(attributeSiteSettingEntity);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });

        }
        private void CreateSiteSetting(WebAPISiteSettingTypes settingType, string entityLogicalName, string value, Guid websiteId)
        {

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Creating site setting",
                Work = (worker, args) =>
                {
                    Entity settingEntity = new Entity("adx_sitesetting");
                    switch (settingType)
                    {
                        case WebAPISiteSettingTypes.EnabledSetting:
                            settingEntity.Attributes["adx_name"] = $"Webapi/{entityLogicalName}/enabled";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            SelectedEntityInfo.WebAPIEnabledSiteSettingId = Service.Create(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.FieldsSetting:
                            settingEntity.Attributes["adx_name"] = $"Webapi/{entityLogicalName}/fields";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            SelectedEntityInfo.WebAPIFieldsSiteSettingId = Service.Create(settingEntity);
                            break;


                        case WebAPISiteSettingTypes.InnerError:
                            settingEntity.Attributes["adx_name"] = $"Webapi/error/innererror";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            InnerErrorSiteSettingsId = Service.Create(settingEntity);
                            break;
                    }

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            });

        }
        private void UpdateSiteSetting(WebAPISiteSettingTypes settingType, Guid settingId, string entityLogicalName, string value, Guid websiteId)
        {

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Updating site setting",
                Work = (worker, args) =>
                {
                    Entity settingEntity = new Entity("adx_sitesetting", settingId);
                    switch (settingType)
                    {
                        case WebAPISiteSettingTypes.EnabledSetting:
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            Service.Update(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.FieldsSetting:
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            Service.Update(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.InnerError:
                            settingEntity.Attributes["adx_name"] = $"Webapi/error/innererror";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            Service.Update(settingEntity);
                            break;
                    }

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            });

        }


        #endregion

       
    }

}