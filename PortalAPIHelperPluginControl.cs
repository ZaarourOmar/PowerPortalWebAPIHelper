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
        private Settings mySettings;
        private EntityInfo selectedEntityInfo;

        private List<WebsiteModel> Websites = new List<WebsiteModel>();
        public List<EntityItemModel> AllEntitiesList { get; set; } = new List<EntityItemModel>();
        public bool InternalErrorEnabled { get; set; }
        public Guid InnerErrorSiteSettingsId { get; set; }

        private Guid TargetWebsiteId { get; set; } = Guid.Empty;
        public PortalAPIHelperPluginControl()
        {
            InitializeComponent();
            selectedEntityInfo = new EntityInfo();

        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

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
                    }
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error while retrieving the websites from the environment.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });


        }
        private void LoadAllEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadAllEntities);

        }

        private void LoadAllEntities()
        {
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
                            EntityItemModel entityItemModel = new EntityItemModel(entityMetadata);

                            if (IsConfigurationEntity(entityItemModel.LogicalName)) continue;

                            AllEntitiesList.Add(entityItemModel);
                        }
                        lstBxAllEntities.DataSource = AllEntitiesList;
                    }
                }
            });
        }


        private void AllEntitiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleanupPreviousEntityInformationPanel();
            EntityItemModel selectedEntity = lstBxAllEntities.SelectedItem as EntityItemModel;
            if (selectedEntity != null)
            {
                selectedEntityInfo.LogicalName = selectedEntity.LogicalName;
                selectedEntityInfo.DisplayName = selectedEntity.DisplayName;
                lblEntityLogicalName.Text = $"Entity Logical Name: {selectedEntityInfo.LogicalName}";
                lblEntityDisplayName.Text = $"Entity Dsplay Name: {selectedEntityInfo.DisplayName}";
                ExecuteMethod(LoadSelectedEntityAttributes, selectedEntity.LogicalName);
                ShowEntityInformationPanel();
            }
            else
            {
                MessageBox.Show("Invalid Entity");
            }
        }

        private void CleanupPreviousEntityInformationPanel()
        {
            // Clear previous attribute lists
            chkdLstBxAllAttibutes.Items.Clear();
            EntityInformationContainer.Visible = false;
            selectedEntityInfo = new EntityInfo();
        }

        private void ShowEntityInformationPanel()
        {
            EntityInformationContainer.Visible = true;
            tsbSaveChanges.Visible = true;
            tsbSwitchInnerError.Visible = true;
        }

        private void LoadSelectedEntityAttributes(string logicalName)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting entity attributes",
                Work = (worker, args) =>
                {

                    RetrieveEntityRequest entityRequest = new RetrieveEntityRequest();
                    entityRequest.LogicalName = logicalName;
                    entityRequest.RetrieveAsIfPublished = true;
                    entityRequest.EntityFilters = EntityFilters.Attributes;
                    var response = (RetrieveEntityResponse)Service.Execute(entityRequest);

                    foreach (var attribute in response.EntityMetadata.Attributes)
                    {
                        if (!AttributeValidator.IsValidAttribute(attribute)) continue; // we don't want calculated fields.

                        AttributeItemModel newAttribute = new AttributeItemModel(attribute);
                        selectedEntityInfo.AllAttributesList.Add(newAttribute);
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

        private void InitializeAttributesBasedOnSiteSetting(Entity attributeSiteSettingEntity)
        {
            if (attributeSiteSettingEntity != null)
            {
                selectedEntityInfo.WebAPIFieldsSiteSettingId = attributeSiteSettingEntity.Id;
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
                selectedEntityInfo.SelectedAttributesList.Clear();
                for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                {
                    var found = fieldsArray.FirstOrDefault(x => x == ((AttributeItemModel)chkdLstBxAllAttibutes.Items[i]).LogicalName);
                    if (found != null)
                    {
                        chkdLstBxAllAttibutes.SetItemChecked(i, true);
                        selectedEntityInfo.SelectedAttributesList.Add((AttributeItemModel)chkdLstBxAllAttibutes.Items[i]);
                    }
                }

            }
            else
            {
                selectedEntityInfo.WebAPIFieldsSiteSettingId = Guid.Empty;
            }
        }

        private void InitializeEnabledCheckBoxBasedOnSiteSetting(Entity enabledSiteSettingEntity)
        {
            chkBxIsWebAPIEnabled.Checked = false;
            if (enabledSiteSettingEntity != null)
            {
                selectedEntityInfo.WebAPIEnabledSiteSettingId = enabledSiteSettingEntity.Id;
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
                selectedEntityInfo.WebAPIEnabledSiteSettingId = Guid.Empty;
            }
        }

        private void InitializeInnerErrorSwitchBasedOnSiteSetting(Entity innerErrorSiteSetting)
        {
            tsbSwitchInnerError.Visible = true;

            if (innerErrorSiteSetting == null)
            {
                tsbSwitchInnerError.Text = "Enable Inner Error Tracking";
                InternalErrorEnabled = false;
                InnerErrorSiteSettingsId = Guid.Empty;
                return;
            }
            bool isInternalErrorEnabled = innerErrorSiteSetting.GetAttributeValue<string>("adx_value").ToLower() == "true";
            if (isInternalErrorEnabled)
            {
                tsbSwitchInnerError.Text = "Disable Inner Error Tracking";
                InternalErrorEnabled = true;
            }
            else
            {
                tsbSwitchInnerError.Text = "Enable Inner Error Tracking";
                InternalErrorEnabled = false;
            }
            InnerErrorSiteSettingsId = innerErrorSiteSetting.Id;
        }
        private void tsbSwitchInnerError_Click(object sender, EventArgs e)
        {
            if (InnerErrorSiteSettingsId != Guid.Empty)
            {

                WorkAsync(new WorkAsyncInfo
                {
                    Message = $"Switching Inner Error Tracking to {!InternalErrorEnabled}",
                    Work = (worker, args) =>
                    {
                        Entity internalErrorSitesettingsEntity = new Entity("adx_sitesetting", InnerErrorSiteSettingsId);
                        internalErrorSitesettingsEntity.Attributes["adx_name"] = "Webapi/error/innererror";
                        internalErrorSitesettingsEntity.Attributes["adx_value"] = InternalErrorEnabled ? "false" : "true";
                        Service.Update(internalErrorSitesettingsEntity);


                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        InternalErrorEnabled = !InternalErrorEnabled;
                        tsbSwitchInnerError.Text = InternalErrorEnabled ? "Disable Inner Error Tracking" : "Enable Inner Error Tracking";

                    }
                });

            }
            else
            {
                WorkAsync(new WorkAsyncInfo
                {
                    Message = $"Creating Inner Error Tracking site setting",
                    Work = (worker, args) =>
                    {
                        Entity internalErrorSitesettingsEntity = new Entity("adx_sitesetting");
                        internalErrorSitesettingsEntity.Attributes["adx_name"] = "Webapi/error/innererror";
                        internalErrorSitesettingsEntity.Attributes["adx_value"] = "true";
                        InnerErrorSiteSettingsId = Service.Create(internalErrorSitesettingsEntity);


                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        InternalErrorEnabled = true;
                        tsbSwitchInnerError.Text = InternalErrorEnabled ? "Disable Inner Error Tracking" : "Enable Inner Error Tracking";


                    }
                });
            }
        }

        private void tsbSaveChanges_Click(object sender, EventArgs e)
        {
            bool isEnabled = chkBxIsWebAPIEnabled.Checked;

          
            if (selectedEntityInfo.WebAPIEnabledSiteSettingId == Guid.Empty)
            {
                CreateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, selectedEntityInfo.LogicalName, isEnabled.ToString().ToLower(), TargetWebsiteId);
            }
            else
            {
                UpdateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, selectedEntityInfo.WebAPIEnabledSiteSettingId, selectedEntityInfo.LogicalName, isEnabled.ToString().ToLower(), TargetWebsiteId);
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
                foreach (AttributeItemModel checkedItemModel in selectedEntityInfo.SelectedAttributesList)
                {
                    fieldsSettingValue.Append(checkedItemModel.LogicalName);
                    fieldsSettingValue.Append(",");
                }
                if (selectedEntityInfo.WebAPIFieldsSiteSettingId == Guid.Empty)
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, selectedEntityInfo.LogicalName, fieldsSettingValue.ToString().ToLower(), TargetWebsiteId);
                }
                else
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, selectedEntityInfo.WebAPIFieldsSiteSettingId, selectedEntityInfo.LogicalName, fieldsSettingValue.ToString().ToLower(), TargetWebsiteId);
                }
            }
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
                            selectedEntityInfo.WebAPIEnabledSiteSettingId = Service.Create(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.FieldsSetting:
                            settingEntity.Attributes["adx_name"] = $"Webapi/{entityLogicalName}/fields";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            selectedEntityInfo.WebAPIFieldsSiteSettingId = Service.Create(settingEntity);
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
        private void txtAttributeFilter_TextChanged(object sender, EventArgs e)
        {
            var itemList = selectedEntityInfo.AllAttributesList.Cast<AttributeItemModel>().ToList();
            if (itemList.Count > 0)
            {
                //clear the items from the list
                chkdLstBxAllAttibutes.Items.Clear();

                //filter the items and add them to the list
                chkdLstBxAllAttibutes.Items.AddRange(
                    itemList.Where(i => i.DisplayName.ToLower().Contains(txtAttributeFilter.Text.ToLower())).ToArray());

                foreach (AttributeItemModel item in selectedEntityInfo.SelectedAttributesList)
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

        private void EntityAttributesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var changedAttribute = (sender as CheckedListBox).SelectedItem as AttributeItemModel;
            var selectedIndex = (sender as CheckedListBox).SelectedIndex;
            if (chkdLstBxAllAttibutes.GetItemChecked(selectedIndex) == true)
            {
                selectedEntityInfo.SelectedAttributesList.Add(changedAttribute);
            }
            else
            {
                if (selectedEntityInfo.SelectedAttributesList.Find(x => x == changedAttribute) != null)
                {
                    selectedEntityInfo.SelectedAttributesList.Remove(changedAttribute);
                }
            }

        }
        #region Later

        /// <summary>
        /// Some entities can't be enabled for the WebAPI on the portal and for now , those are the configuration entities. 
        /// </summary>
        /// <param name="logicalName"></param>
        /// <returns></returns>
        private bool IsConfigurationEntity(string logicalName)
        {
            return logicalName.StartsWith("adx_");
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

        private void tsbWebsiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebsiteModel selectedWebsite = tsbWebsiteList.SelectedItem as WebsiteModel;
            if (selectedWebsite != null)
            {
                TargetWebsiteId = selectedWebsite.Id;
                CleanupPreviousEntityInformationPanel();
                LoadAllEntities();
                AllEntitiesListBox_SelectedIndexChanged(this, null);
            }
        }
    }

}