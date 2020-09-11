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

namespace PowerPortalWebAPIHelper
{
    public partial class PortalAPIHelperPluginControl : PluginControlBase
    {
        private Settings mySettings;

        public PortalAPIHelperPluginControl()
        {
            InitializeComponent();
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

        private void LoadAllEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadAllEntities);
           
        }

        private void LoadAllEntities()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting entities",
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
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityMetadata[];
                    if (result != null)
                    {
                        foreach (EntityMetadata emd in result)
                        {
                            EntityItemModel item = new EntityItemModel(emd);

                            if (IsConfigurationEntity(item.LogicalName)) continue;

                            AllEntitiesList.Add(item);
                            AllEntitiesListBox.Items.Add(item);
                        }

                        SortListBox<EntityItemModel>(AllEntitiesListBox);

                    }
                }
            });
        }

        public List<EntityItemModel> AllEntitiesList { get; set; } = new List<EntityItemModel>();

        private void SortListBox<T>(ListBox listBox)
        {
            //sort the list, will find a better way later
            var list = listBox.Items.Cast<T>().OrderBy(item => item.ToString()).ToList();
            listBox.Items.Clear();
            foreach (T listItem in list)
            {
                listBox.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Some entities can't be enabled for the WebAPI on the portal and for now , those are the configuration entities. 
        /// </summary>
        /// <param name="logicalName"></param>
        /// <returns></returns>
        private bool IsConfigurationEntity(string logicalName)
        {
            return logicalName.StartsWith("adx_");
        }


        private void AllEntitiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Clear previous lists
            EntityAttributesList.Items.Clear();

            EntityItemModel clickedEntity = (sender as ListBox).SelectedItem as EntityItemModel;
            if (clickedEntity != null && clickedEntity.IsValid())
            {
                ExecuteMethod(LoadSelectedEntityAttributes, clickedEntity.LogicalName);
                SelectedEntityLogicalName = clickedEntity.LogicalName;
                lblEntityLogicalName.Text = $"Entity Logical Name: {clickedEntity.LogicalName}";
                lblEntityDisplayName.Text = $"Entity Dsplay Name: {clickedEntity.DisplayName}";
                EntityInformationContainer.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid Entity");
            }
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
                        if (!string.IsNullOrEmpty(attribute.AttributeOf)) continue; // we don't want calculated fields.
                        EntityAttributesList.Items.Add(new AttributeItemModel(attribute));
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
                    FilterExpression webApiFilter = new FilterExpression(LogicalOperator.Or);
                    webApiFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/{logicalName}/");
                    webApiFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/error/innererror");
                    siteSettingsQuery.Criteria.AddFilter(webApiFilter);

                    //retreive the settings based on the above query
                    var siteSettingsRecords = Service.RetrieveMultiple(siteSettingsQuery).Entities;
                    var innerErrorSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/error/innererror");
                    var enabledSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/enabled");
                    var fieldsSiteSettingsEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/fields");

                    // the inner error setting is general setting and not related to specific entity
                    InitializeInnerErrorSwitch(innerErrorSiteSettingEntity);

                    ChkBxIsWebAPIEnabled.Checked = false;
                    if (enabledSiteSettingEntity != null)
                    {
                        SelectedEntityEnabledSiteSettingsId = enabledSiteSettingEntity.Id;
                        if (enabledSiteSettingEntity.GetAttributeValue<string>("adx_value").ToLower() == "true")
                        {
                            ChkBxIsWebAPIEnabled.Checked = true;
                        }
                        else
                        {
                            ChkBxIsWebAPIEnabled.Checked = false;
                        }
                    }

                    if (fieldsSiteSettingsEntity != null)
                    {
                        SelectedEntityFieldsSiteSettingsId = fieldsSiteSettingsEntity.Id;
                        var fieldsString = fieldsSiteSettingsEntity.GetAttributeValue<string>("adx_value");
                        if (string.IsNullOrEmpty(fieldsString)) return;


                        // if the value is star, all fields are selecteed
                        if (fieldsString == "*")
                        {
                            AllAttributesSelected = true;
                            for (int i = 0; i < EntityAttributesList.Items.Count; i++)
                            {
                                EntityAttributesList.SetItemChecked(i, true);
                            }
                        }

                        var fieldsArray = fieldsString.Split(',');
                        for (int i = 0; i < EntityAttributesList.Items.Count; i++)
                        {
                            var found = fieldsArray.FirstOrDefault(x => x == ((AttributeItemModel)EntityAttributesList.Items[i]).LogicalName);
                            if (found != null)
                            {
                                EntityAttributesList.SetItemChecked(i, true);
                            }
                        }

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


        public bool InternalErrorEnabled { get; set; }
        public string SelectedEntityLogicalName { get; set; }
        public Guid InnerErrorSiteSettingsId { get; set; }
        public Guid SelectedEntityEnabledSiteSettingsId { get; set; }
        public Guid SelectedEntityFieldsSiteSettingsId { get; set; }
        public bool AllAttributesSelected { get; set; } = false;
        private void InitializeInnerErrorSwitch(Entity innerErrorSiteSetting)
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
            bool isEnabled = ChkBxIsWebAPIEnabled.Checked;
            if (SelectedEntityEnabledSiteSettingsId == Guid.Empty)
            {
                CreateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, SelectedEntityLogicalName, isEnabled.ToString().ToLower());
            }
            else
            {
                UpdateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, SelectedEntityEnabledSiteSettingsId, SelectedEntityLogicalName, isEnabled.ToString().ToLower());
            }


            // get the fields setting value;
            StringBuilder fieldsSettingValue =new StringBuilder();
            // if all are selected, set the setting value to "*"
            if (EntityAttributesList.CheckedItems.Count == EntityAttributesList.Items.Count)
            {
                fieldsSettingValue.Append("*");
            }
            else
            {
                foreach(AttributeItemModel checkedItemModel in EntityAttributesList.CheckedItems)
                {
                    fieldsSettingValue.Append(checkedItemModel.LogicalName);
                    fieldsSettingValue.Append(",");
                }
                if (SelectedEntityFieldsSiteSettingsId == Guid.Empty)
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, SelectedEntityLogicalName, fieldsSettingValue.ToString().ToLower());
                }
                else
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, SelectedEntityFieldsSiteSettingsId, SelectedEntityLogicalName, fieldsSettingValue.ToString().ToLower());
                }
            }
        }


        private void CreateSiteSetting(WebAPISiteSettingTypes settingType, string entityLogicalName, string value)
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
                            SelectedEntityEnabledSiteSettingsId = Service.Create(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.FieldsSetting:
                            settingEntity.Attributes["adx_name"] = $"Webapi/{entityLogicalName}/fields";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            SelectedEntityEnabledSiteSettingsId = Service.Create(settingEntity);
                            break;


                        case WebAPISiteSettingTypes.InnerError:
                            settingEntity.Attributes["adx_name"] = $"Webapi/error/innererror";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
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



        private void UpdateSiteSetting(WebAPISiteSettingTypes settingType, Guid settingId, string entityLogicalName, string value)
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
                            Service.Update(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.FieldsSetting:
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            Service.Update(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.InnerError:
                            settingEntity.Attributes["adx_name"] = $"Webapi/error/innererror";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
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
                AllEntitiesListBox.Items.Clear();

                //filter the items and add them to the list
                AllEntitiesListBox.Items.AddRange(
                    itemList.Where(i => i.DisplayName.ToLower().Contains(txtAllEntitiesFilter.Text.ToLower())).ToArray());
            }
            else
            {
                AllEntitiesListBox.Items.AddRange(itemList.ToArray());
            }
        }

        
    }

    public enum WebAPISiteSettingTypes { EnabledSetting, FieldsSetting, InnerError }
}