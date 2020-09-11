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
            ExecuteMethod(GetEntities);
        }

        public void GetEntities()
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
                        foreach(EntityMetadata emd in result)
                        {
                            EntityListItem item = new EntityListItem(emd);

                            if (IsConfigurationEntity(item.LogicalName)) continue;

                            AllEntitiesList.Items.Add(item);
                        }

                        //sort the list, will find a better way later
                        var list = AllEntitiesList.Items.Cast<EntityListItem>().OrderBy(item => item.DisplayName).ToList();
                        AllEntitiesList.Items.Clear();
                        foreach (EntityListItem listItem in list)
                        {
                            AllEntitiesList.Items.Add(listItem);
                        }
                    }
                }
            });
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

        private void AllEntitiesList_Click(object sender, EventArgs e)
        {
            EntityListItem clickedEntity = (sender as ListBox).SelectedItem as EntityListItem;
            if (clickedEntity!=null && clickedEntity.IsValid())
            {
                ExecuteMethod(GetSelectedEntityFields, clickedEntity.LogicalName);
                lblEntityLogicalName.Text = $"Entity Logical Name: {clickedEntity.LogicalName}";
                lblEntityDisplayName.Text = $"Entity Dsplay Name: {clickedEntity.DisplayName}";
                EntityInformationContainer.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid Entity");
            }
        }



        private void GetSelectedEntityFields(string logicalName)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting entity fields",
                Work = (worker, args) =>
                {

                    RetrieveEntityRequest entityRequest = new RetrieveEntityRequest();
                    entityRequest.LogicalName = logicalName;
                    entityRequest.RetrieveAsIfPublished = true;
                    entityRequest.EntityFilters = EntityFilters.Attributes;
                    var response = (RetrieveEntityResponse)Service.Execute(entityRequest);

                    foreach (var attribute in response.EntityMetadata.Attributes)
                    {
                        EntityFieldsList.Items.Add(new FieldListItem(attribute));
                    }



                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ExecuteMethod(ShowEntityWebAPIInformation, logicalName);


                }
            });
        }


        private void ShowEntityWebAPIInformation(string logicalName)
        {
            try
            {


                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Getting site settings",
                    Work = (worker, args) =>
                    {

                        // Get sitesettings for this entity
                        QueryExpression siteSettingsQuery = new QueryExpression("adx_sitesetting");
                        siteSettingsQuery.ColumnSet = new ColumnSet("adx_name", "adx_value");

                        FilterExpression webApiFilter = new FilterExpression(LogicalOperator.Or);
                        webApiFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/{logicalName}/");
                        webApiFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/error/innererror");

                        siteSettingsQuery.Criteria.AddFilter(webApiFilter);                      
                        
                        var siteSettingsRecords = Service.RetrieveMultiple(siteSettingsQuery).Entities;

                        // set the settings to the checkboxes
                        var innerErrorSiteSetting = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/error/innererror");
                        var enabledSiteSetting = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/enabled");
                        var fieldsSiteSettings = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/fields");

                        InitializeInnerErrorSwitch(innerErrorSiteSetting);
                        if (enabledSiteSetting != null)
                        {
                            if (enabledSiteSetting.GetAttributeValue<string>("adx_value").ToLower() == "true")
                            {
                                ChkBxIsWebAPIEnabled.Checked = true;
                            }
                            else
                            {
                                ChkBxIsWebAPIEnabled.Checked = false;
                            }
                        }

                        if (fieldsSiteSettings != null)
                        {
                            var fieldsString = fieldsSiteSettings.GetAttributeValue<string>("adx_value");
                            if (string.IsNullOrEmpty(fieldsString)) return;


                            // if the value is star, all fields are selecteed
                            if (fieldsString == "*")
                            {
                                for (int i = 0; i < EntityFieldsList.Items.Count; i++)
                                {
                                    EntityFieldsList.SetItemChecked(i, true);
                                }
                            }

                            var fieldsArray = fieldsString.Split(',');
                            for (int i = 0; i < EntityFieldsList.Items.Count; i++) {
                                var found = fieldsArray.FirstOrDefault(x => x == ((FieldListItem)EntityFieldsList.Items[i]).LogicalName);
                                if (found != null)
                                {
                                    EntityFieldsList.SetItemChecked(i, true);
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

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public bool InternalErrorEnabled { get; set; }
        public Guid InternalErrorSiteSettingsId { get; set; }
        private void InitializeInnerErrorSwitch(Entity innerErrorSiteSetting)
        {
            tsbSwitchInnerError.Visible = true;

            if (innerErrorSiteSetting == null)
            {
                tsbSwitchInnerError.Text = "Enable Inner Error Tracking";
                InternalErrorEnabled = false;
                InternalErrorSiteSettingsId = Guid.Empty;
                return;
            }
            bool isInternalErrorEnabled= innerErrorSiteSetting.GetAttributeValue<string>("adx_value").ToLower()=="true";
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
            InternalErrorSiteSettingsId = innerErrorSiteSetting.Id ;
        }

        private void tsbSwitchInnerError_Click(object sender, EventArgs e)
        {
            if (InternalErrorSiteSettingsId != Guid.Empty)
            {

                WorkAsync(new WorkAsyncInfo
                {
                    Message = $"Switching Inner Error Tracking to {!InternalErrorEnabled}",
                    Work = (worker, args) =>
                    {
                        Entity internalErrorSitesettingsEntity = new Entity("adx_sitesetting", InternalErrorSiteSettingsId);
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
                        InternalErrorSiteSettingsId= Service.Create(internalErrorSitesettingsEntity);


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
    }
}