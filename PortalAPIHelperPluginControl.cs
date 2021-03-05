using Extensions;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using PowerPortalWebAPIHelper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace PowerPortalWebAPIHelper
{
    public partial class PortalAPIHelperPluginControl : PluginControlBase, IGitHubPlugin, INoConnectionRequired
    {
        #region Props and Variables
        private Settings mySettings;
        private WebAPIHelperModel Model = new WebAPIHelperModel();
        public string RepositoryName => "PowerPortalWebAPIHelper";
        public string UserName => "ZaarourOmar";
        #endregion

        #region Plugin Control
        public PortalAPIHelperPluginControl()
        {
            InitializeComponent();
        }
        private void PortalAPIHelperPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("Please report bugs or enhancments by clicking on Learn More.", new Uri("https://github.com/ZaarourOmar/PowerPortalWebAPIHelper"));

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

            txtAllEntitiesFilter.Init(Constants.ENTITY_FILTER_HINT);


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
        private void tsbHowTo_Click(object sender, EventArgs e)
        {
            string howToHelpString = "";
            howToHelpString = "This tool quickly enables/disables Portal WebAPI capability for entities and their attributes. Simpley ,select the entity you want to enable/disable and select all the attributes you wish to expose through the web api and hit save! In addition, simple Create/Update/Delete Javascript snippets can be generated using this tool along with the Ajax Wrapper that you need to execute those calls. For more detailed setup and usage info of the portal Web Api, please visit https://docs.microsoft.com/en-us/powerapps/maker/portals/web-api-overview.";
            MessageBox.Show(howToHelpString, "How to", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Entity Management
        private void LoadAllEntities()
        {
            //clear previous lists
            Model?.ClearEntities();
            lstBxAllEntities.DataSource = Model.AllEntitiesList;
            txtAllEntitiesFilter.Text = "";
            // load entity list
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Entities",
                Work = (worker, args) =>
                {
                    RetrieveAllEntitiesRequest retrieveAllEntityRequest = new RetrieveAllEntitiesRequest
                    {
                        RetrieveAsIfPublished = true,
                        EntityFilters = EntityFilters.Entity | EntityFilters.Relationships
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
                        lstBxAllEntities.SelectedIndexChanged -= AllEntitiesListBox_SelectedIndexChanged;
                        foreach (EntityMetadata entityMetadata in entityMetaDataArray)
                        {
                            if (!MetadataValidator.IsValidEntity(entityMetadata)) continue;

                            EntityItemModel entityItemModel = new EntityItemModel(entityMetadata);
                            Model.AddEntity(entityItemModel);
                            //lstBxAllEntities.Items.Add(entityItemModel);
                        }
                        lstBxAllEntities.SelectedIndexChanged += AllEntitiesListBox_SelectedIndexChanged;
                        lstBxAllEntities.SelectedItem = null;
                        ToggleWebsiteToolbarComponents(true);
                        entitiesSplitContainer.Visible = true;
                    }
                    else
                    {
                        ToggleWebsiteToolbarComponents(false);
                        MessageBox.Show("Error while connecting to the environment.");
                    }
                    LoadInnerErrorTrackingSettings();
                }
            });
        }
        private void ResetEntityInformationPanel()
        {
            EntityInformationSplitContainer.Visible = false;
        }
        private void ShowEntityInformationPanel()
        {
            EntityInformationSplitContainer.Visible = true;
            tsbSwitchInnerError.Visible = true;
            grpBoxEntityInformation.Text = Model?.SelectedEntityInfo?.DisplayName + " Entity Information";
        }
        private void InitializeEnabledCheckBoxBasedOnSiteSetting(Entity enabledSiteSettingEntity)
        {
            chkBxIsWebAPIEnabled.Checked = false;
            if (enabledSiteSettingEntity != null)
            {
                Model.WebAPIEnabledSiteSettingId = enabledSiteSettingEntity.Id;
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
                Model.WebAPIEnabledSiteSettingId = Guid.Empty;
            }
        }
        private void InitializeInnerErrorSwitchBasedOnSiteSetting(Entity innerErrorSiteSetting)
        {
            tsbSwitchInnerError.Visible = true;

            if (innerErrorSiteSetting == null)
            {
                tsbSwitchInnerError.Text = Constants.ENABLE_INNER_ERROR_TEXT;
                Model.InnerErrorEnabled = false;
                Model.InnerErrorSiteSettingsId = Guid.Empty;
                return;
            }
            bool isInternalErrorEnabled = innerErrorSiteSetting.GetAttributeValue<string>("adx_value").ToLower() == "true";
            if (isInternalErrorEnabled)
            {
                tsbSwitchInnerError.Text = Constants.DISABLE_INNER_ERROR_TEXT;
                Model.InnerErrorEnabled = true;
            }
            else
            {
                tsbSwitchInnerError.Text = Constants.ENABLE_INNER_ERROR_TEXT;
                Model.InnerErrorEnabled = false;
            }
            Model.InnerErrorSiteSettingsId = innerErrorSiteSetting.Id;
        }
        private void tsbSaveChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constants.SAVE_CHANGES_CONFIRMATION_MESSAGE, "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bool isEnabled = chkBxIsWebAPIEnabled.Checked;

                if (Model.WebAPIEnabledSiteSettingId == Guid.Empty)
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, Model.SelectedEntityLogicalName, isEnabled.ToString().ToLower(), Model.SelectedWebSiteId);
                }
                else
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, Model.WebAPIEnabledSiteSettingId, Model.SelectedEntityLogicalName, isEnabled.ToString().ToLower(), Model.SelectedWebSiteId);
                }


                // get the fields setting value;
                StringBuilder fieldsSettingValue = new StringBuilder();
                // if all are selected, set the setting value to "*"
                if (chkdLstBxAllAttibutes.CheckedItems.Count == 0)
                {
                    fieldsSettingValue.Append("");
                }
                else if (chkdLstBxAllAttibutes.CheckedItems.Count == chkdLstBxAllAttibutes.Items.Count)
                {
                    fieldsSettingValue.Append("*");
                }
                else
                {
                    foreach (WebAPIAttributeItemModel checkedItemModel in chkdLstBxAllAttibutes.CheckedItems)
                    {
                        fieldsSettingValue.Append(checkedItemModel.WebAPIName);
                        if (chkdLstBxAllAttibutes.CheckedItems.IndexOf(checkedItemModel) == chkdLstBxAllAttibutes.CheckedItems.Count - 1) break;
                        fieldsSettingValue.Append(",");
                    }

                }
                if (Model.WebAPIFieldsSiteSettingId == Guid.Empty)
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, Model.SelectedEntityLogicalName, fieldsSettingValue.ToString(), Model.SelectedWebSiteId);
                }
                else
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, Model.WebAPIFieldsSiteSettingId, Model.SelectedEntityLogicalName, fieldsSettingValue.ToString(), Model.SelectedWebSiteId);
                }

            }
        }
        private void txtAllEntitiesFilter_TextChanged(object sender, EventArgs e)
        {
            string txt = txtAllEntitiesFilter.Text;
            if (txt == Constants.ENTITY_FILTER_HINT) return;

            lstBxAllEntities.SelectedIndexChanged -= AllEntitiesListBox_SelectedIndexChanged;

            var allEntities = Model.AllEntitiesList;
            var filteredEntities = Model.AllEntitiesList.Where(i => i.DisplayName.ToLower().Contains(txt.ToLower())).ToList();
            if (!string.IsNullOrEmpty(txt))
            {
                lstBxAllEntities.DataSource = filteredEntities;
            }
            else
            {
                lstBxAllEntities.DataSource = allEntities;
            }

            lstBxAllEntities.SelectedIndexChanged += AllEntitiesListBox_SelectedIndexChanged;

        }
        private void LoadAllEntities_Click(object sender, EventArgs e)
        {
            if (Model.Websites == null || Model.Websites.Count == 0)
            {
                ExecuteMethod(LoadWebsites);
            }
            else
            {
                ExecuteMethod(LoadAllEntities);
            }
        }
        private void AllEntitiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetEntityInformationPanel();
            Model.UpdateSelectedEntity(lstBxAllEntities.SelectedItem as EntityItemModel);
            if (Model.SelectedEntityInfo != null)
            {
                ExecuteMethod(LoadSelectedEntityAttributes, Model.SelectedEntityLogicalName);
                // find possible associations for snippet generation
                foreach (var mToMRelationsip in Model.SelectedEntityMtoMRelationships)
                {
                    var item = mToMRelationsip.Entity1NavigationPropertyName;
                    cbBxAssociateWith.Items.Add(new AssociationInfo(mToMRelationsip));
                }
            }

        }
        private void ToggleSelectedEntityToolbarComponents(bool enabled)
        {
            if (enabled)
            {
                tsbSaveChanges.Enabled = true;
            }
            else
            {
                tsbSaveChanges.Enabled = false;
            }

        }

        #endregion

        #region Attribute Management
        private void LoadSelectedEntityAttributes(string logicalName)
        {
            ResetEntityInformationPanel();
            ((ListBox)chkdLstBxAllAttibutes).DataSource = Model.SelectedEntityAttributes;
            Dictionary<string, string> relatedEntitiesPluralNames = new Dictionary<string, string>();
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

                    foreach (var mto1Relationship in response.EntityMetadata.ManyToOneRelationships)
                    {
                        RetrieveEntityRequest relatedEntityRequest = new RetrieveEntityRequest();
                        relatedEntityRequest.LogicalName = mto1Relationship.ReferencedEntity;
                        relatedEntityRequest.EntityFilters = EntityFilters.Entity;

                        RetrieveEntityResponse relatedEntityResponse = (RetrieveEntityResponse)Service.Execute(relatedEntityRequest);
                        if (!relatedEntitiesPluralNames.ContainsKey(mto1Relationship.ReferencedEntity))
                        {
                            relatedEntitiesPluralNames.Add(mto1Relationship.ReferencedEntity, relatedEntityResponse.EntityMetadata.LogicalCollectionName);
                        }
                    }

                    // an attribute needs to be aware of all the collection names that it is related to.
                    WebAPIAttributeItemModel.RelatedEntitiesPluralNames = relatedEntitiesPluralNames;
                    args.Result = response.EntityMetadata.Attributes;

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    var allAttributeMetaData = args.Result as AttributeMetadata[];
                    foreach (var attribute in allAttributeMetaData)
                    {
                        if (!MetadataValidator.IsValidAttribute(attribute))
                            continue;

                        if (attribute.AttributeType == AttributeTypeCode.Customer)
                        {
                            WebAPIAttributeItemModel contactAttribute = new WebAPIAttributeItemModel(attribute, Model.SelectedEntityInfo, true, CustomerType.Contact);
                            WebAPIAttributeItemModel accountAttribute = new WebAPIAttributeItemModel(attribute, Model.SelectedEntityInfo, true, CustomerType.Account);
                            Model.AddAttribute(contactAttribute);
                            Model.AddAttribute(accountAttribute);
                        }
                        else
                        {
                            WebAPIAttributeItemModel newAttribute = new WebAPIAttributeItemModel(attribute, Model.SelectedEntityInfo);
                            Model.AddAttribute(newAttribute);
                        }

                    }


                    ExecuteMethod(LoadEntityWebAPISettings, logicalName);
                }
            });
        }
        private void InitializeAttributesBasedOnSiteSetting(Entity attributeSiteSettingEntity)
        {
            chkBxSelectAllAttributes.Checked = false;
            if (attributeSiteSettingEntity != null)
            {
                Model.WebAPIFieldsSiteSettingId = attributeSiteSettingEntity.Id;
                var fieldsString = attributeSiteSettingEntity.GetAttributeValue<string>("adx_value");
                if (string.IsNullOrEmpty(fieldsString))
                {
                    for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                    {
                        chkdLstBxAllAttibutes.SetItemChecked(i, false);
                    }
                    chkBxSelectAllAttributes.Checked = false;
                    return;
                }
                // if the value is star, all fields are selecteed
                else if (fieldsString == "*")
                {
                    chkBxSelectAllAttributes.Checked = true;
                    for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                    {
                        chkdLstBxAllAttibutes.SetItemChecked(i, true);
                    }

                }
                else
                {
                    var fieldsArray = fieldsString.Split(',');
                    for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                    {
                        var found = fieldsArray.FirstOrDefault(x => x == ((WebAPIAttributeItemModel)(chkdLstBxAllAttibutes.Items[i])).WebAPIName);
                        if (!string.IsNullOrEmpty(found))
                        {
                            chkdLstBxAllAttibutes.SetItemChecked(i, true);
                        }
                        else
                        {
                            chkdLstBxAllAttibutes.SetItemChecked(i, false);
                        }
                    }
                }

            }
            else
            {
                Model.WebAPIFieldsSiteSettingId = Guid.Empty;
            }
        }
        private void EntityAttributesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkdLstBxAllAttibutes.CheckedItems.Count == chkdLstBxAllAttibutes.Items.Count)
            {
                chkBxSelectAllAttributes.Checked = true;
            }
            else
            {
                Model.ForceAllAttributeCheck = false;
                chkBxSelectAllAttributes.Checked = false;
            }
            UpdateOrGenerateSnippets();
        }
        private void chkBxSelectAllAttributes_CheckedChanged(object sender, EventArgs e)
        {

            if (Model.ForceAllAttributeCheck)
            {
                for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                {
                    chkdLstBxAllAttibutes.SetItemChecked(i, chkBxSelectAllAttributes.Checked);
                }
            }
            else
            {
                Model.ForceAllAttributeCheck = true;
            }
            UpdateOrGenerateSnippets();
        }

        #endregion

        #region Snippets Management
        private void snippetsContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                var richTxtBox = snippetsContextMenu.SourceControl as System.Windows.Forms.RichTextBox;
                var itemClicked = e.ClickedItem;
                if (richTxtBox != null && itemClicked.Name == "tsmCopy")
                {
                    Clipboard.SetText(richTxtBox.Text);
                    MessageBox.Show("The snippet has been copied to the clipboard.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitializeOperationTypes()
        {
            cbBxOperationType.DataSource = OperationTypeInfo.LoadAvailableTypes();
            cbBxOperationType.SelectedIndexChanged += CbBxOperationType_SelectedIndexChanged;
            cbBxOperationType.SelectedIndex = 0;
            CbBxOperationType_SelectedIndexChanged(this, null);

        }
        private void CbBxOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = cbBxOperationType.SelectedItem as OperationTypeInfo;

            if (selectedItem.Type == APIOperationTypes.AssociateDisassociate)
            {
                grpBxAssociationEntity.Visible = true;
            }
            else
            {
                grpBxAssociationEntity.Visible = false;
            }

            lblOperationMessage.Text = selectedItem.Message;

            UpdateOrGenerateSnippets();
        }
        private void UpdateOrGenerateSnippets()
        {
            rchTxtBxWrapperFunction.Text = SnippetsGenerator.GenerateWrapperFunction();
            var selectedOperation = cbBxOperationType.SelectedItem as OperationTypeInfo;
            var selectedAssociation = cbBxAssociateWith.SelectedItem as AssociationInfo;
            if (selectedOperation != null)
            {
                APIOperationTypes operationType = selectedOperation.Type;
                bool addFields = chBxUseSelectedFields.Checked;
                List<WebAPIAttributeItemModel> selectedAttributes = chkdLstBxAllAttibutes.CheckedItems.Cast<WebAPIAttributeItemModel>().ToList();
                string snippet = SnippetsGenerator.GenerateSnippet(Model.SelectedEntityInfo, selectedAttributes, selectedOperation.Type, selectedAssociation, addFields);
                rchTxtBoxOperation.Text = snippet;
            }
        }
        private void chBxUseSelectedFields_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOrGenerateSnippets();
        }
        #endregion

        #region Website Management


        private void ToggleWebsiteToolbarComponents(bool enabled)
        {
            if (enabled)
            {
                tsbWebsiteList.Enabled = true;
                tsbWebsiteLabel.Enabled = true;
                tsbSwitchInnerError.Enabled = true;
            }
            else
            {
                tsbWebsiteList.Enabled = false;
                tsbWebsiteLabel.Enabled = false;
                tsbSwitchInnerError.Enabled = false;

            }
        }
        private void LoadWebsites()
        {

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Websites",
                Work = (worker, args) =>
                {
                    QueryExpression websiteQuery = new QueryExpression("adx_website");
                    websiteQuery.ColumnSet = new ColumnSet(new string[] { "adx_name", "adx_websiteid","statecode" });
                    websiteQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);// only active websites
                    args.Result = Service.RetrieveMultiple(websiteQuery);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Result != null)
                    {
                        var entities = (args.Result as EntityCollection).Entities;
                        if (entities.Count == 0)
                        {
                            MessageBox.Show(Constants.NO_PORTAL_WEBSITE_FOUND, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            CloseTool();
                            return;
                        }
                        tsbWebsiteList.Items.Clear();
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
        private void tsbWebsiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebsiteModel selectedWebsite = tsbWebsiteList.SelectedItem as WebsiteModel;
            if (selectedWebsite != null)
            {
                Model.UpdateWebsite(selectedWebsite);
                ResetEntityInformationPanel();
                LoadAllEntities();
            }
        }
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
                    innerErrorFilter.AddCondition("adx_websiteid", ConditionOperator.Equal, Model.SelectedWebSiteId);
                    innerErrorFilter.AddCondition("adx_name", ConditionOperator.BeginsWith, $"Webapi/error/innererror");
                    siteSettingsQuery.Criteria.AddFilter(innerErrorFilter);

                    var siteSettingsRecords = Service.RetrieveMultiple(siteSettingsQuery).Entities;
                    var innerErrorSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/error/innererror");

                    args.Result = innerErrorSiteSettingEntity;

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Entity innerErrorSiteSettingEntity = args.Result as Entity;
                    InitializeInnerErrorSwitchBasedOnSiteSetting(innerErrorSiteSettingEntity);

                }
            });
        }
        private void tsbSwitchInnerError_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constants.INNER_ERROR_CONFIRMATION_MESSAGE, "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Model.InnerErrorSiteSettingsId != Guid.Empty)
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.InnerError, Model.InnerErrorSiteSettingsId, null, (Model.InnerErrorEnabled ? "false" : "true"), Model.SelectedWebSiteId);
                    Model.InnerErrorEnabled = !Model.InnerErrorEnabled;
                    tsbSwitchInnerError.Text = Model.InnerErrorEnabled ? Constants.DISABLE_INNER_ERROR_TEXT : Constants.ENABLE_INNER_ERROR_TEXT;

                }
                else
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.InnerError, null, "true", Model.SelectedWebSiteId);
                    Model.InnerErrorEnabled = true;
                    tsbSwitchInnerError.Text = Constants.DISABLE_INNER_ERROR_TEXT;
                }
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
                    webApiWebsiteFilter.AddCondition("adx_websiteid", ConditionOperator.Equal, Model.SelectedWebSiteId);
                    siteSettingsQuery.Criteria.AddFilter(webApiWebsiteFilter);

                    //retreive the settings based on the above query
                    var siteSettingsRecords = Service.RetrieveMultiple(siteSettingsQuery).Entities;
                    var innerErrorSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/error/innererror");
                    var enabledSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/enabled");
                    var attributeSiteSettingEntity = siteSettingsRecords.FirstOrDefault(x => x.GetAttributeValue<string>("adx_name") == $"Webapi/{logicalName}/fields");


                    args.Result = new Tuple<Entity, Entity, Entity>(innerErrorSiteSettingEntity, enabledSiteSettingEntity, attributeSiteSettingEntity);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        ToggleSelectedEntityToolbarComponents(false);
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Tuple<Entity, Entity, Entity> results = args.Result as Tuple<Entity, Entity, Entity>;
                    InitializeInnerErrorSwitchBasedOnSiteSetting(results.Item1);
                    InitializeEnabledCheckBoxBasedOnSiteSetting(results.Item2);
                    InitializeAttributesBasedOnSiteSetting(results.Item3);
                    ToggleSelectedEntityToolbarComponents(true);
                    CheckEntityPermissions(logicalName,Model.SelectedWebSiteId);
                    ShowEntityInformationPanel();
                    // populate the operation type combobox
                    InitializeOperationTypes();

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
                            Model.WebAPIEnabledSiteSettingId = Service.Create(settingEntity);
                            break;

                        case WebAPISiteSettingTypes.FieldsSetting:
                            settingEntity.Attributes["adx_name"] = $"Webapi/{entityLogicalName}/fields";
                            settingEntity.Attributes["adx_value"] = value;
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            Model.WebAPIFieldsSiteSettingId = Service.Create(settingEntity);
                            break;


                        case WebAPISiteSettingTypes.InnerError:
                            settingEntity.Attributes["adx_name"] = $"Webapi/error/innererror";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            Model.InnerErrorSiteSettingsId = Service.Create(settingEntity);
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
                            settingEntity.Attributes["adx_value"] = value;
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

        #region Entity Permissions
        private void CheckEntityPermissions(string entityLogicalName,Guid websiteId)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Checking Entity Permissions",
                Work = (worker, args) =>
                {

                    // Get sitesettings for this entity that are related to the web api setup
                    QueryExpression entityPermissionsQuery = new QueryExpression("adx_entitypermission");
                    entityPermissionsQuery.ColumnSet = new ColumnSet(new string[] { "adx_entitypermissionid", "adx_entitylogicalname","adx_websiteid" });
                    entityPermissionsQuery.Criteria.AddCondition("adx_entitylogicalname", ConditionOperator.Equal, entityLogicalName);
                    entityPermissionsQuery.Criteria.AddCondition("adx_websiteid", ConditionOperator.Equal, websiteId);
                    var entityPermissionsResult = Service.RetrieveMultiple(entityPermissionsQuery);
                    args.Result = entityPermissionsResult.Entities.Count;

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int entitPermissionCount = (int)args.Result;
                    if (entitPermissionCount == 0)
                    {
                        lblEntityPermissionsNotification.ForeColor = Color.Red;
                        lblEntityPermissionsNotification.Text = Constants.ENTITY_HAS_NO_ENTITYPERMISSION; ;
                        btnCreateEntityPermission.Visible = true;
                    }
                    else
                    {
                        lblEntityPermissionsNotification.ForeColor = Color.Green;
                        lblEntityPermissionsNotification.Text = Constants.ENTITY_HAS_ENTITYPERMISSION; ;
                        btnCreateEntityPermission.Visible = false;
                    }

                }
            });
        }


        private void btnCreateEntityPermission_Click(object sender, EventArgs e)
        {

            if (Model == null || Model.SelectedEntityInfo == null || Model.SelectedWebsiteInfo == null)
            {
                MessageBox.Show(Constants.UNABLE_TO_CREATE_ENTITY_PERMISSION, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(Constants.CREATE_ENTITY_PERMISSION_DIALOG, "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Creating a global entity permission for the administrator webrole",
                    Work = (worker, args) =>
                    {

                        QueryExpression webRolesQuery = new QueryExpression("adx_webrole");
                        FilterExpression authenticatedUserWebRoleFilter = webRolesQuery.Criteria.AddFilter(LogicalOperator.Or);
                        authenticatedUserWebRoleFilter.AddCondition("adx_name", ConditionOperator.Equal, Constants.WEBROLE_ADMIN_NAME);
                        authenticatedUserWebRoleFilter.AddCondition("adx_webroleid", ConditionOperator.Equal, Constants.WEBROLE_ADMIN_ID);
                        webRolesQuery.Criteria.AddFilter(authenticatedUserWebRoleFilter);
                        webRolesQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0); // only active webroles

                        var webRolesResult = Service.RetrieveMultiple(webRolesQuery);
                        Guid webRoleId = Guid.Empty;
                        if (webRolesResult.Entities != null && webRolesResult.Entities.Count > 0)
                        {
                            // normally, the web role will have the same id in all microsoft portal, but this is just to confirm that no one created a custom role with the same name but different id
                            webRoleId = webRolesResult.Entities[0].Id;
                        }
                        else
                        {
                            MessageBox.Show(Constants.WEBROLE_ADMIN_NOTFOUND, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        string entityPermissionEntityName = "adx_entitypermission";
                        string entityName = Model.SelectedEntityLogicalName;
                        string entityDisplayName = Model.SelectedEntityDisplayName;
                        Guid websiteId = Model.SelectedWebSiteId;
                        int scope = 756150000; // this is the global scope

                        Entity newEntityPermission = new Entity(entityPermissionEntityName);
                        newEntityPermission.Attributes.Add("adx_entityname", "Auto Generated Global Entity Permission for " + entityDisplayName);
                        newEntityPermission.Attributes.Add("adx_entitylogicalname", entityName);
                        newEntityPermission.Attributes.Add("adx_websiteid", new EntityReference("adx_website", websiteId));
                        newEntityPermission.Attributes.Add("adx_read", true);
                        newEntityPermission.Attributes.Add("adx_write", true);
                        newEntityPermission.Attributes.Add("adx_create", true);
                        newEntityPermission.Attributes.Add("adx_delete", true);
                        newEntityPermission.Attributes.Add("adx_append", true);
                        newEntityPermission.Attributes.Add("adx_appendto", true);
                        newEntityPermission.Attributes.Add("adx_scope", new OptionSetValue(scope));
                        newEntityPermission.Attributes.Add("statecode", 0);// only active permisions

                        Guid recordId = Service.Create(newEntityPermission);
                        if (recordId != Guid.Empty && webRoleId != Guid.Empty)
                        {
                            AssociateRequest associateRequest = new AssociateRequest();
                            associateRequest.Relationship = new Relationship("adx_entitypermission_webrole");
                            associateRequest.RelatedEntities = new EntityReferenceCollection();
                            associateRequest.RelatedEntities.Add(new EntityReference("adx_webrole", webRoleId));
                            associateRequest.Target = new EntityReference("adx_entitypermission", recordId);
                            Service.Execute(associateRequest);
                        }

                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        lblEntityPermissionsNotification.ForeColor = Color.Green;
                        lblEntityPermissionsNotification.Text = Constants.ENTITY_PERMISSION_CREATED; ;
                        btnCreateEntityPermission.Visible = false;

                    }
                });
            }
        }
        #endregion

       
    }

}