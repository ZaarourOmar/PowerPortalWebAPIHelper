﻿using Extensions;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using PowerPortalWebAPIHelper.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        private EntityItemModel SelectedEntityInfo { get; set; } = new EntityItemModel();
        private List<WebsiteModel> Websites = new List<WebsiteModel>();
        private bool ForceAllAttributeCheck = true;
        public List<EntityItemModel> AllEntitiesList { get; set; } = new List<EntityItemModel>();
        private WebsiteModel SelectedWebsiteInfo { get; set; } = new WebsiteModel();
        public string RepositoryName => "PowerPortalWebAPIHelper";
        public string UserName => "ZaarourOmar";

        const string ENTITY_FILTER_HINT = "Search for the entity name here..";
        const string ATTRIBUTE_FILTER_HINT = "Search for the attribute name here..";
        const string ENABLE_INNER_ERROR_TEXT = "Enable Inner Error Tracking for this website";
        const string DISABLE_INNER_ERROR_TEXT = "Disable Inner Error Tracking for this website";
        const string INNER_ERROR_CONFIRMATION_MESSAGE = "This command will either create or update the  sites etting(webapi/innererror/enabled) for the selected website.Do you want to continue?";
        const string SAVE_CHANGES_CONFIRMATION_MESSAGE = "This command will either create or update the needed site settings for the selected entity and under the selected website, do you want to continue? ";

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
            howToHelpString = "This tool quickly enables/disables Portal WebAPI capability for entities and their attributes. Simply ,select the entity you want to enable/disable and select all the attributes you wish to expose through the web api and hit save! In addition, simple Create/Update/Delete Javascript snippets can be generated using this tool. For more detailed setup and usage info of the web api in the portals, please visit https://docs.microsoft.com/en-us/powerapps/maker/portals/web-api-overview ";
            MessageBox.Show(howToHelpString, "How to", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Entity Management
        private void LoadAllEntities()
        {
            //clear previous lists
            AllEntitiesList.Clear();
            lstBxAllEntities.Items.Clear();
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
                        foreach (EntityMetadata entityMetadata in entityMetaDataArray)
                        {
                            if (!MetadataValidator.IsValidEntity(entityMetadata)) continue;

                            EntityItemModel entityItemModel = new EntityItemModel(entityMetadata);
                            AllEntitiesList.Add(entityItemModel);
                            lstBxAllEntities.Items.Add(entityItemModel);
                        }

                        ToggleWebsiteToolbarComponents(true);

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
            chkdLstBxAllAttibutes.Items.Clear();
            EntityInformationContainer.Visible = false;
            SelectedEntityInfo = new EntityItemModel();
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
                tsbSwitchInnerError.Text = ENABLE_INNER_ERROR_TEXT;
                SelectedWebsiteInfo.InnerErrorEnabled = false;
                SelectedWebsiteInfo.InnerErrorSiteSettingsId = Guid.Empty;
                return;
            }
            bool isInternalErrorEnabled = innerErrorSiteSetting.GetAttributeValue<string>("adx_value").ToLower() == "true";
            if (isInternalErrorEnabled)
            {
                tsbSwitchInnerError.Text = DISABLE_INNER_ERROR_TEXT;
                SelectedWebsiteInfo.InnerErrorEnabled = true;
            }
            else
            {
                tsbSwitchInnerError.Text = ENABLE_INNER_ERROR_TEXT;
                SelectedWebsiteInfo.InnerErrorEnabled = false;
            }
            SelectedWebsiteInfo.InnerErrorSiteSettingsId = innerErrorSiteSetting.Id;
        }
        private void tsbSaveChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(SAVE_CHANGES_CONFIRMATION_MESSAGE, "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bool isEnabled = chkBxIsWebAPIEnabled.Checked;


                if (SelectedEntityInfo.WebAPIEnabledSiteSettingId == Guid.Empty)
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, SelectedEntityInfo.LogicalName, isEnabled.ToString().ToLower(), SelectedWebsiteInfo.Id);
                }
                else
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.EnabledSetting, SelectedEntityInfo.WebAPIEnabledSiteSettingId, SelectedEntityInfo.LogicalName, isEnabled.ToString().ToLower(), SelectedWebsiteInfo.Id);
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
                if (SelectedEntityInfo.WebAPIFieldsSiteSettingId == Guid.Empty)
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, SelectedEntityInfo.LogicalName, fieldsSettingValue.ToString(), SelectedWebsiteInfo.Id);
                }
                else
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.FieldsSetting, SelectedEntityInfo.WebAPIFieldsSiteSettingId, SelectedEntityInfo.LogicalName, fieldsSettingValue.ToString(), SelectedWebsiteInfo.Id);
                }

            }
        }
        private void txtAllEntitiesFilter_TextChanged(object sender, EventArgs e)
        {
            string txt = txtAllEntitiesFilter.Text;
            if (txt == ENTITY_FILTER_HINT) return;


            var itemList = AllEntitiesList.Cast<EntityItemModel>().ToList();
            if (itemList.Count > 0)
            {
                //clear the items from the list
                lstBxAllEntities.Items.Clear();

                //filter the items and add them to the list
                lstBxAllEntities.Items.AddRange(
                    itemList.Where(i => i.DisplayName.ToLower().Contains(txt.ToLower())).ToArray());
            }
            else
            {
                lstBxAllEntities.Items.AddRange(itemList.ToArray());
            }
        }
        private void LoadAllEntities_Click(object sender, EventArgs e)
        {
            if (Websites == null || Websites.Count == 0)
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
            EntityItemModel selectedEntity = lstBxAllEntities.SelectedItem as EntityItemModel;
            if (selectedEntity != null)
            {
                SelectedEntityInfo = selectedEntity;
                ExecuteMethod(LoadSelectedEntityAttributes, SelectedEntityInfo.LogicalName);
                ShowEntityInformationPanel();
                txtAttributeFilter.Init(ATTRIBUTE_FILTER_HINT);
                txtAllEntitiesFilter.Init(ENTITY_FILTER_HINT);

                // find possible associations for snippet generation
                foreach (var mToMRelationsip in SelectedEntityInfo.MToMRelationships)
                {
                    var item = mToMRelationsip.Entity1NavigationPropertyName;
                    cbBxAssociateWith.Items.Add(new AssociationInfo(mToMRelationsip));
                }

                // populate the operation type combobox
                InitializeOperationTypes();


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
            txtAttributeFilter.Text = "";
            SelectedEntityInfo.AllAttributesList.Clear();
            chkdLstBxAllAttibutes.Items.Clear();
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
                            WebAPIAttributeItemModel contactAttribute = new WebAPIAttributeItemModel(attribute, SelectedEntityInfo, true, CustomerType.Contact);
                            WebAPIAttributeItemModel accountAttribute = new WebAPIAttributeItemModel(attribute, SelectedEntityInfo, true, CustomerType.Account);
                            SelectedEntityInfo.AllAttributesList.Add(contactAttribute);
                            chkdLstBxAllAttibutes.Items.Add(contactAttribute);
                            SelectedEntityInfo.AllAttributesList.Add(accountAttribute);
                            chkdLstBxAllAttibutes.Items.Add(accountAttribute);
                        }
                        else
                        {
                            WebAPIAttributeItemModel newAttribute = new WebAPIAttributeItemModel(attribute, SelectedEntityInfo);
                            SelectedEntityInfo.AllAttributesList.Add(newAttribute);
                            chkdLstBxAllAttibutes.Items.Add(newAttribute);
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
                SelectedEntityInfo.WebAPIFieldsSiteSettingId = attributeSiteSettingEntity.Id;
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
                    //chkdLstBxAllAttibutes.Items.Clear();
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
                SelectedEntityInfo.WebAPIFieldsSiteSettingId = Guid.Empty;
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
                ForceAllAttributeCheck = false;
                chkBxSelectAllAttributes.Checked = false;
            }
        }
        private void txtAttributeFilter_TextChanged(object sender, EventArgs e)
        {

            //string txt = txtAttributeFilter.Text;
            //if (txt == ATTRIBUTE_FILTER_HINT) return;

            //var itemList = SelectedEntityInfo.AllAttributesList.Cast<WebAPIAttributeItemModel>().ToList();
            //if (itemList.Count > 0)
            //{
            //    //clear the items from the list
            //    chkdLstBxAllAttibutes.Items.Clear();

            //    //filter the items and add them to the list
            //    chkdLstBxAllAttibutes.Items.AddRange(
            //        itemList.Where(i => i.DisplayName.ToLower().Contains(txt.ToLower())).ToArray());

            //    foreach (WebAPIAttributeItemModel item in chkdLstBxAllAttibutes.Items)
            //    {
            //        if (chkdLstBxAllAttibutes.Items.IndexOf(item) >= 0)
            //            chkdLstBxAllAttibutes.SetItemChecked(chkdLstBxAllAttibutes.Items.IndexOf(item), true);

            //    }

            //}
            //else
            //{
            //    chkdLstBxAllAttibutes.Items.AddRange(itemList.ToArray());
            //}

        }
        private void chkBxSelectAllAttributes_CheckedChanged(object sender, EventArgs e)
        {
            if (ForceAllAttributeCheck)
            {
                for (int i = 0; i < chkdLstBxAllAttibutes.Items.Count; i++)
                {
                    chkdLstBxAllAttibutes.SetItemChecked(i, chkBxSelectAllAttributes.Checked);
                }
            }
            else
            {
                ForceAllAttributeCheck = true;
            }

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
        }
        private void btnGenerateSnippet_Click(object sender, EventArgs e)
        {
            var selectedOperation = cbBxOperationType.SelectedItem as OperationTypeInfo;
            var selectedAssociation = cbBxAssociateWith.SelectedItem as AssociationInfo;
            if (selectedOperation != null)
            {
                APIOperationTypes operationType = selectedOperation.Type;
                bool addFields = chBxUseSelectedFields.Checked;
                List<WebAPIAttributeItemModel> selectedAttributes = chkdLstBxAllAttibutes.CheckedItems.Cast<WebAPIAttributeItemModel>().ToList();
                string snippet = SnippetsGenerator.GenerateSnippet(SelectedEntityInfo, selectedAttributes,selectedOperation.Type, selectedAssociation, addFields);
                rchTxtBoxOperation.Text = snippet;
            }
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
                    websiteQuery.ColumnSet = new ColumnSet(new string[] { "adx_name", "adx_websiteid" });
                    args.Result = Service.RetrieveMultiple(websiteQuery);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Result != null)
                    {
                        var entities = (args.Result as EntityCollection).Entities;
                        if (entities.Count == 0)
                        {
                            MessageBox.Show("The target organization has no portal setup. Please setup a portal first then restart this tool.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            CloseTool();
                        }
                        foreach (Entity webSite in entities)
                        {
                            Guid websiteId = webSite.Id;
                            string websiteName = webSite.GetAttributeValue<string>("adx_name");
                            tsbWebsiteList.ComboBox.DisplayMember = "Name";
                            tsbWebsiteList.ComboBox.ValueMember = "Id";
                            tsbWebsiteList.Items.Add(new WebsiteModel(websiteName, websiteId));
                        }
                        tsbWebsiteList.SelectedIndex = 0;

                    // LoadInnerErrorTrackingSettings();

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
                SelectedWebsiteInfo = selectedWebsite;
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
                    innerErrorFilter.AddCondition("adx_websiteid", ConditionOperator.Equal, SelectedWebsiteInfo.Id);
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
            if (MessageBox.Show(INNER_ERROR_CONFIRMATION_MESSAGE, "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (SelectedWebsiteInfo.InnerErrorSiteSettingsId != Guid.Empty)
                {
                    UpdateSiteSetting(WebAPISiteSettingTypes.InnerError, SelectedWebsiteInfo.InnerErrorSiteSettingsId, null, (SelectedWebsiteInfo.InnerErrorEnabled ? "false" : "true"), SelectedWebsiteInfo.Id);
                    SelectedWebsiteInfo.InnerErrorEnabled = !SelectedWebsiteInfo.InnerErrorEnabled;
                    tsbSwitchInnerError.Text = SelectedWebsiteInfo.InnerErrorEnabled ? DISABLE_INNER_ERROR_TEXT : ENABLE_INNER_ERROR_TEXT;

                }
                else
                {
                    CreateSiteSetting(WebAPISiteSettingTypes.InnerError, null, "true", SelectedWebsiteInfo.Id);
                    SelectedWebsiteInfo.InnerErrorEnabled = true;
                    tsbSwitchInnerError.Text = DISABLE_INNER_ERROR_TEXT;
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
                    webApiWebsiteFilter.AddCondition("adx_websiteid", ConditionOperator.Equal, SelectedWebsiteInfo.Id);
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
                            settingEntity.Attributes["adx_value"] = value;
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            SelectedEntityInfo.WebAPIFieldsSiteSettingId = Service.Create(settingEntity);
                            break;


                        case WebAPISiteSettingTypes.InnerError:
                            settingEntity.Attributes["adx_name"] = $"Webapi/error/innererror";
                            settingEntity.Attributes["adx_value"] = value.ToLower();
                            settingEntity.Attributes["adx_websiteid"] = new EntityReference("adx_website", websiteId);
                            SelectedWebsiteInfo.InnerErrorSiteSettingsId = Service.Create(settingEntity);
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


    }

}