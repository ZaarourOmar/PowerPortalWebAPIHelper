using PowerPortalWebAPIHelper.Extensions;

namespace PowerPortalWebAPIHelper
{
    partial class PortalAPIHelperPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortalAPIHelperPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.tsbHowTo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSwitchInnerError = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbWebsiteList = new System.Windows.Forms.ToolStripComboBox();
            this.tsbWebsiteLabel = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.entitiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.txtAllEntitiesFilter = new System.Windows.Forms.TextBox();
            this.lstBxAllEntities = new System.Windows.Forms.ListBox();
            this.EntityInformationSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grpBxEntityAttributes = new System.Windows.Forms.GroupBox();
            this.chkdLstBxAllAttibutes = new System.Windows.Forms.CheckedListBox();
            this.grpBoxEntityInformation = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCreateEntityPermission = new System.Windows.Forms.Button();
            this.lblEntityPermissionsNotification = new System.Windows.Forms.Label();
            this.chkBxSelectAllAttributes = new System.Windows.Forms.CheckBox();
            this.chkBxIsWebAPIEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControlSnippets = new System.Windows.Forms.TabControl();
            this.tabOperation = new System.Windows.Forms.TabPage();
            this.splitContainerOperation = new System.Windows.Forms.SplitContainer();
            this.groupBoxSnippetSettings = new System.Windows.Forms.GroupBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lblOperationType = new System.Windows.Forms.Label();
            this.chBxUseSelectedFields = new System.Windows.Forms.CheckBox();
            this.cbBxOperationType = new System.Windows.Forms.ComboBox();
            this.grpBxAssociationEntity = new System.Windows.Forms.GroupBox();
            this.lblAssociateWith = new System.Windows.Forms.Label();
            this.cbBxAssociateWith = new System.Windows.Forms.ComboBox();
            this.lblOperationMessage = new System.Windows.Forms.Label();
            this.rchTxtBoxOperation = new PowerPortalWebAPIHelper.Extensions.SimpleSyntaxHighlightingRTB();
            this.snippetsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tabWrapperFunction = new System.Windows.Forms.TabPage();
            this.rchTxtBxWrapperFunction = new PowerPortalWebAPIHelper.Extensions.SimpleSyntaxHighlightingRTB();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entitiesSplitContainer)).BeginInit();
            this.entitiesSplitContainer.Panel1.SuspendLayout();
            this.entitiesSplitContainer.Panel2.SuspendLayout();
            this.entitiesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EntityInformationSplitContainer)).BeginInit();
            this.EntityInformationSplitContainer.Panel1.SuspendLayout();
            this.EntityInformationSplitContainer.Panel2.SuspendLayout();
            this.EntityInformationSplitContainer.SuspendLayout();
            this.grpBxEntityAttributes.SuspendLayout();
            this.grpBoxEntityInformation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControlSnippets.SuspendLayout();
            this.tabOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOperation)).BeginInit();
            this.splitContainerOperation.Panel1.SuspendLayout();
            this.splitContainerOperation.Panel2.SuspendLayout();
            this.splitContainerOperation.SuspendLayout();
            this.groupBoxSnippetSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grpBxAssociationEntity.SuspendLayout();
            this.snippetsContextMenu.SuspendLayout();
            this.tabWrapperFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbLoadEntities,
            this.tsbHowTo,
            this.toolStripSeparator2,
            this.tsbSwitchInnerError,
            this.toolStripSeparator3,
            this.tsbSaveChanges,
            this.toolStripSeparator1,
            this.tsbWebsiteList,
            this.tsbWebsiteLabel});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1240, 31);
            this.toolStripMenu.TabIndex = 10;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = global::PowerPortalWebAPIHelper.Properties.Resources.closeicon;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(110, 28);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = global::PowerPortalWebAPIHelper.Properties.Resources.loadicon;
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(102, 28);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.LoadAllEntities_Click);
            // 
            // tsbHowTo
            // 
            this.tsbHowTo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbHowTo.Image = global::PowerPortalWebAPIHelper.Properties.Resources.helpicon;
            this.tsbHowTo.Name = "tsbHowTo";
            this.tsbHowTo.Size = new System.Drawing.Size(106, 28);
            this.tsbHowTo.Text = "Why this tool?";
            this.tsbHowTo.Click += new System.EventHandler(this.tsbHowTo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbSwitchInnerError
            // 
            this.tsbSwitchInnerError.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSwitchInnerError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSwitchInnerError.Enabled = false;
            this.tsbSwitchInnerError.Image = ((System.Drawing.Image)(resources.GetObject("tsbSwitchInnerError.Image")));
            this.tsbSwitchInnerError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwitchInnerError.Name = "tsbSwitchInnerError";
            this.tsbSwitchInnerError.Size = new System.Drawing.Size(234, 28);
            this.tsbSwitchInnerError.Text = "Enable Inner Error Tracking for this website";
            this.tsbSwitchInnerError.Click += new System.EventHandler(this.tsbSwitchInnerError_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbSaveChanges
            // 
            this.tsbSaveChanges.Enabled = false;
            this.tsbSaveChanges.Image = global::PowerPortalWebAPIHelper.Properties.Resources.saveicon;
            this.tsbSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveChanges.Name = "tsbSaveChanges";
            this.tsbSaveChanges.Size = new System.Drawing.Size(108, 28);
            this.tsbSaveChanges.Text = "Save Changes";
            this.tsbSaveChanges.Click += new System.EventHandler(this.tsbSaveChanges_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbWebsiteList
            // 
            this.tsbWebsiteList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbWebsiteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbWebsiteList.Enabled = false;
            this.tsbWebsiteList.Name = "tsbWebsiteList";
            this.tsbWebsiteList.Size = new System.Drawing.Size(200, 31);
            this.tsbWebsiteList.Sorted = true;
            this.tsbWebsiteList.SelectedIndexChanged += new System.EventHandler(this.tsbWebsiteList_SelectedIndexChanged);
            // 
            // tsbWebsiteLabel
            // 
            this.tsbWebsiteLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbWebsiteLabel.Enabled = false;
            this.tsbWebsiteLabel.Name = "tsbWebsiteLabel";
            this.tsbWebsiteLabel.Size = new System.Drawing.Size(52, 28);
            this.tsbWebsiteLabel.Text = "Website:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.entitiesSplitContainer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.EntityInformationSplitContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1240, 534);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 5;
            // 
            // entitiesSplitContainer
            // 
            this.entitiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entitiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.entitiesSplitContainer.Name = "entitiesSplitContainer";
            this.entitiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // entitiesSplitContainer.Panel1
            // 
            this.entitiesSplitContainer.Panel1.Controls.Add(this.txtAllEntitiesFilter);
            // 
            // entitiesSplitContainer.Panel2
            // 
            this.entitiesSplitContainer.Panel2.Controls.Add(this.lstBxAllEntities);
            this.entitiesSplitContainer.Size = new System.Drawing.Size(320, 534);
            this.entitiesSplitContainer.SplitterDistance = 27;
            this.entitiesSplitContainer.TabIndex = 0;
            this.entitiesSplitContainer.Visible = false;
            // 
            // txtAllEntitiesFilter
            // 
            this.txtAllEntitiesFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtAllEntitiesFilter.Location = new System.Drawing.Point(0, 7);
            this.txtAllEntitiesFilter.Name = "txtAllEntitiesFilter";
            this.txtAllEntitiesFilter.Size = new System.Drawing.Size(320, 20);
            this.txtAllEntitiesFilter.TabIndex = 0;
            this.txtAllEntitiesFilter.TextChanged += new System.EventHandler(this.txtAllEntitiesFilter_TextChanged);
            // 
            // lstBxAllEntities
            // 
            this.lstBxAllEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxAllEntities.FormattingEnabled = true;
            this.lstBxAllEntities.HorizontalScrollbar = true;
            this.lstBxAllEntities.Location = new System.Drawing.Point(0, 0);
            this.lstBxAllEntities.Name = "lstBxAllEntities";
            this.lstBxAllEntities.Size = new System.Drawing.Size(320, 503);
            this.lstBxAllEntities.Sorted = true;
            this.lstBxAllEntities.TabIndex = 1;
            this.lstBxAllEntities.SelectedIndexChanged += new System.EventHandler(this.AllEntitiesListBox_SelectedIndexChanged);
            // 
            // EntityInformationSplitContainer
            // 
            this.EntityInformationSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntityInformationSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.EntityInformationSplitContainer.Name = "EntityInformationSplitContainer";
            // 
            // EntityInformationSplitContainer.Panel1
            // 
            this.EntityInformationSplitContainer.Panel1.Controls.Add(this.grpBxEntityAttributes);
            this.EntityInformationSplitContainer.Panel1.Controls.Add(this.grpBoxEntityInformation);
            // 
            // EntityInformationSplitContainer.Panel2
            // 
            this.EntityInformationSplitContainer.Panel2.Controls.Add(this.groupBox3);
            this.EntityInformationSplitContainer.Size = new System.Drawing.Size(916, 534);
            this.EntityInformationSplitContainer.SplitterDistance = 420;
            this.EntityInformationSplitContainer.TabIndex = 0;
            this.EntityInformationSplitContainer.Visible = false;
            // 
            // grpBxEntityAttributes
            // 
            this.grpBxEntityAttributes.Controls.Add(this.chkdLstBxAllAttibutes);
            this.grpBxEntityAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxEntityAttributes.Location = new System.Drawing.Point(0, 127);
            this.grpBxEntityAttributes.Name = "grpBxEntityAttributes";
            this.grpBxEntityAttributes.Size = new System.Drawing.Size(420, 407);
            this.grpBxEntityAttributes.TabIndex = 6;
            this.grpBxEntityAttributes.TabStop = false;
            this.grpBxEntityAttributes.Text = "Entity Attributes";
            // 
            // chkdLstBxAllAttibutes
            // 
            this.chkdLstBxAllAttibutes.CheckOnClick = true;
            this.chkdLstBxAllAttibutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdLstBxAllAttibutes.FormattingEnabled = true;
            this.chkdLstBxAllAttibutes.Location = new System.Drawing.Point(3, 16);
            this.chkdLstBxAllAttibutes.Name = "chkdLstBxAllAttibutes";
            this.chkdLstBxAllAttibutes.Size = new System.Drawing.Size(414, 388);
            this.chkdLstBxAllAttibutes.Sorted = true;
            this.chkdLstBxAllAttibutes.TabIndex = 5;
            this.chkdLstBxAllAttibutes.SelectedIndexChanged += new System.EventHandler(this.EntityAttributesListBox_SelectedIndexChanged);
            // 
            // grpBoxEntityInformation
            // 
            this.grpBoxEntityInformation.Controls.Add(this.panel1);
            this.grpBoxEntityInformation.Controls.Add(this.chkBxSelectAllAttributes);
            this.grpBoxEntityInformation.Controls.Add(this.chkBxIsWebAPIEnabled);
            this.grpBoxEntityInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBoxEntityInformation.Location = new System.Drawing.Point(0, 0);
            this.grpBoxEntityInformation.MinimumSize = new System.Drawing.Size(300, 100);
            this.grpBoxEntityInformation.Name = "grpBoxEntityInformation";
            this.grpBoxEntityInformation.Size = new System.Drawing.Size(420, 127);
            this.grpBoxEntityInformation.TabIndex = 3;
            this.grpBoxEntityInformation.TabStop = false;
            this.grpBoxEntityInformation.Text = "Entity Information";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCreateEntityPermission);
            this.panel1.Controls.Add(this.lblEntityPermissionsNotification);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(168, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 108);
            this.panel1.TabIndex = 7;
            // 
            // btnCreateEntityPermission
            // 
            this.btnCreateEntityPermission.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCreateEntityPermission.Location = new System.Drawing.Point(0, 85);
            this.btnCreateEntityPermission.Name = "btnCreateEntityPermission";
            this.btnCreateEntityPermission.Size = new System.Drawing.Size(249, 23);
            this.btnCreateEntityPermission.TabIndex = 8;
            this.btnCreateEntityPermission.Text = "Create one now?";
            this.btnCreateEntityPermission.UseVisualStyleBackColor = true;
            this.btnCreateEntityPermission.Visible = false;
            this.btnCreateEntityPermission.Click += new System.EventHandler(this.btnCreateEntityPermission_Click);
            // 
            // lblEntityPermissionsNotification
            // 
            this.lblEntityPermissionsNotification.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEntityPermissionsNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityPermissionsNotification.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblEntityPermissionsNotification.Location = new System.Drawing.Point(0, 0);
            this.lblEntityPermissionsNotification.Name = "lblEntityPermissionsNotification";
            this.lblEntityPermissionsNotification.Size = new System.Drawing.Size(249, 84);
            this.lblEntityPermissionsNotification.TabIndex = 7;
            // 
            // chkBxSelectAllAttributes
            // 
            this.chkBxSelectAllAttributes.AutoSize = true;
            this.chkBxSelectAllAttributes.Location = new System.Drawing.Point(6, 62);
            this.chkBxSelectAllAttributes.Margin = new System.Windows.Forms.Padding(10);
            this.chkBxSelectAllAttributes.Name = "chkBxSelectAllAttributes";
            this.chkBxSelectAllAttributes.Size = new System.Drawing.Size(117, 17);
            this.chkBxSelectAllAttributes.TabIndex = 6;
            this.chkBxSelectAllAttributes.Text = "Select All Attributes";
            this.chkBxSelectAllAttributes.UseVisualStyleBackColor = true;
            this.chkBxSelectAllAttributes.CheckedChanged += new System.EventHandler(this.chkBxSelectAllAttributes_CheckedChanged);
            // 
            // chkBxIsWebAPIEnabled
            // 
            this.chkBxIsWebAPIEnabled.AutoSize = true;
            this.chkBxIsWebAPIEnabled.Location = new System.Drawing.Point(6, 31);
            this.chkBxIsWebAPIEnabled.Margin = new System.Windows.Forms.Padding(10);
            this.chkBxIsWebAPIEnabled.Name = "chkBxIsWebAPIEnabled";
            this.chkBxIsWebAPIEnabled.Size = new System.Drawing.Size(135, 17);
            this.chkBxIsWebAPIEnabled.TabIndex = 3;
            this.chkBxIsWebAPIEnabled.Text = "Enabled For Web API?";
            this.chkBxIsWebAPIEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControlSnippets);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(492, 534);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code Snippets";
            // 
            // tabControlSnippets
            // 
            this.tabControlSnippets.Controls.Add(this.tabOperation);
            this.tabControlSnippets.Controls.Add(this.tabWrapperFunction);
            this.tabControlSnippets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSnippets.Location = new System.Drawing.Point(3, 16);
            this.tabControlSnippets.Name = "tabControlSnippets";
            this.tabControlSnippets.SelectedIndex = 0;
            this.tabControlSnippets.Size = new System.Drawing.Size(486, 515);
            this.tabControlSnippets.TabIndex = 7;
            // 
            // tabOperation
            // 
            this.tabOperation.Controls.Add(this.splitContainerOperation);
            this.tabOperation.Location = new System.Drawing.Point(4, 22);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperation.Size = new System.Drawing.Size(478, 489);
            this.tabOperation.TabIndex = 4;
            this.tabOperation.Text = "Operation";
            this.tabOperation.UseVisualStyleBackColor = true;
            // 
            // splitContainerOperation
            // 
            this.splitContainerOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOperation.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerOperation.Location = new System.Drawing.Point(3, 3);
            this.splitContainerOperation.Name = "splitContainerOperation";
            this.splitContainerOperation.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerOperation.Panel1
            // 
            this.splitContainerOperation.Panel1.Controls.Add(this.groupBoxSnippetSettings);
            // 
            // splitContainerOperation.Panel2
            // 
            this.splitContainerOperation.Panel2.Controls.Add(this.rchTxtBoxOperation);
            this.splitContainerOperation.Size = new System.Drawing.Size(472, 483);
            this.splitContainerOperation.SplitterDistance = 173;
            this.splitContainerOperation.SplitterWidth = 8;
            this.splitContainerOperation.TabIndex = 2;
            // 
            // groupBoxSnippetSettings
            // 
            this.groupBoxSnippetSettings.Controls.Add(this.splitContainer3);
            this.groupBoxSnippetSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSnippetSettings.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSnippetSettings.Name = "groupBoxSnippetSettings";
            this.groupBoxSnippetSettings.Size = new System.Drawing.Size(472, 173);
            this.groupBoxSnippetSettings.TabIndex = 3;
            this.groupBoxSnippetSettings.TabStop = false;
            this.groupBoxSnippetSettings.Text = "Snippet Settings";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(3, 16);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lblOperationType);
            this.splitContainer3.Panel1.Controls.Add(this.chBxUseSelectedFields);
            this.splitContainer3.Panel1.Controls.Add(this.cbBxOperationType);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.grpBxAssociationEntity);
            this.splitContainer3.Panel2.Controls.Add(this.lblOperationMessage);
            this.splitContainer3.Size = new System.Drawing.Size(466, 154);
            this.splitContainer3.SplitterDistance = 176;
            this.splitContainer3.SplitterWidth = 8;
            this.splitContainer3.TabIndex = 9;
            // 
            // lblOperationType
            // 
            this.lblOperationType.AutoSize = true;
            this.lblOperationType.Location = new System.Drawing.Point(6, 6);
            this.lblOperationType.Name = "lblOperationType";
            this.lblOperationType.Size = new System.Drawing.Size(80, 13);
            this.lblOperationType.TabIndex = 1;
            this.lblOperationType.Text = "Operation Type";
            // 
            // chBxUseSelectedFields
            // 
            this.chBxUseSelectedFields.Checked = true;
            this.chBxUseSelectedFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBxUseSelectedFields.Location = new System.Drawing.Point(6, 43);
            this.chBxUseSelectedFields.Name = "chBxUseSelectedFields";
            this.chBxUseSelectedFields.Size = new System.Drawing.Size(167, 40);
            this.chBxUseSelectedFields.TabIndex = 4;
            this.chBxUseSelectedFields.Text = "Include Selected Fields";
            this.chBxUseSelectedFields.UseVisualStyleBackColor = true;
            this.chBxUseSelectedFields.CheckedChanged += new System.EventHandler(this.chBxUseSelectedFields_CheckedChanged);
            // 
            // cbBxOperationType
            // 
            this.cbBxOperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBxOperationType.FormattingEnabled = true;
            this.cbBxOperationType.Location = new System.Drawing.Point(6, 22);
            this.cbBxOperationType.Name = "cbBxOperationType";
            this.cbBxOperationType.Size = new System.Drawing.Size(167, 21);
            this.cbBxOperationType.TabIndex = 0;
            // 
            // grpBxAssociationEntity
            // 
            this.grpBxAssociationEntity.Controls.Add(this.lblAssociateWith);
            this.grpBxAssociationEntity.Controls.Add(this.cbBxAssociateWith);
            this.grpBxAssociationEntity.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpBxAssociationEntity.Location = new System.Drawing.Point(0, 111);
            this.grpBxAssociationEntity.Name = "grpBxAssociationEntity";
            this.grpBxAssociationEntity.Size = new System.Drawing.Size(282, 43);
            this.grpBxAssociationEntity.TabIndex = 7;
            this.grpBxAssociationEntity.TabStop = false;
            this.grpBxAssociationEntity.Text = "Association Entity Settings";
            this.grpBxAssociationEntity.Visible = false;
            // 
            // lblAssociateWith
            // 
            this.lblAssociateWith.AutoSize = true;
            this.lblAssociateWith.Location = new System.Drawing.Point(0, 21);
            this.lblAssociateWith.Name = "lblAssociateWith";
            this.lblAssociateWith.Size = new System.Drawing.Size(81, 13);
            this.lblAssociateWith.TabIndex = 6;
            this.lblAssociateWith.Text = "Associate With:";
            // 
            // cbBxAssociateWith
            // 
            this.cbBxAssociateWith.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBxAssociateWith.FormattingEnabled = true;
            this.cbBxAssociateWith.Location = new System.Drawing.Point(83, 16);
            this.cbBxAssociateWith.Name = "cbBxAssociateWith";
            this.cbBxAssociateWith.Size = new System.Drawing.Size(253, 21);
            this.cbBxAssociateWith.TabIndex = 5;
            // 
            // lblOperationMessage
            // 
            this.lblOperationMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOperationMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperationMessage.Location = new System.Drawing.Point(0, 0);
            this.lblOperationMessage.Name = "lblOperationMessage";
            this.lblOperationMessage.Size = new System.Drawing.Size(282, 113);
            this.lblOperationMessage.TabIndex = 8;
            this.lblOperationMessage.Text = resources.GetString("lblOperationMessage.Text");
            // 
            // rchTxtBoxOperation
            // 
            this.rchTxtBoxOperation.ContextMenuStrip = this.snippetsContextMenu;
            this.rchTxtBoxOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBoxOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBoxOperation.Location = new System.Drawing.Point(0, 0);
            this.rchTxtBoxOperation.Name = "rchTxtBoxOperation";
            this.rchTxtBoxOperation.Size = new System.Drawing.Size(472, 302);
            this.rchTxtBoxOperation.TabIndex = 1;
            this.rchTxtBoxOperation.Text = "";
            // 
            // snippetsContextMenu
            // 
            this.snippetsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopy});
            this.snippetsContextMenu.Name = "snippetsContextMenu";
            this.snippetsContextMenu.Size = new System.Drawing.Size(146, 26);
            this.snippetsContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.snippetsContextMenu_ItemClicked);
            // 
            // tsmCopy
            // 
            this.tsmCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.Size = new System.Drawing.Size(145, 22);
            this.tsmCopy.Text = "Copy Snippet";
            // 
            // tabWrapperFunction
            // 
            this.tabWrapperFunction.Controls.Add(this.rchTxtBxWrapperFunction);
            this.tabWrapperFunction.Location = new System.Drawing.Point(4, 22);
            this.tabWrapperFunction.Name = "tabWrapperFunction";
            this.tabWrapperFunction.Padding = new System.Windows.Forms.Padding(3);
            this.tabWrapperFunction.Size = new System.Drawing.Size(478, 489);
            this.tabWrapperFunction.TabIndex = 0;
            this.tabWrapperFunction.Text = "Wrapper Function";
            this.tabWrapperFunction.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxWrapperFunction
            // 
            this.rchTxtBxWrapperFunction.ContextMenuStrip = this.snippetsContextMenu;
            this.rchTxtBxWrapperFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxWrapperFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxWrapperFunction.Location = new System.Drawing.Point(3, 3);
            this.rchTxtBxWrapperFunction.Name = "rchTxtBxWrapperFunction";
            this.rchTxtBxWrapperFunction.Size = new System.Drawing.Size(472, 483);
            this.rchTxtBxWrapperFunction.TabIndex = 0;
            this.rchTxtBxWrapperFunction.Text = "";
            // 
            // PortalAPIHelperPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "PortalAPIHelperPluginControl";
            this.Size = new System.Drawing.Size(1240, 565);
            this.Load += new System.EventHandler(this.PortalAPIHelperPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.entitiesSplitContainer.Panel1.ResumeLayout(false);
            this.entitiesSplitContainer.Panel1.PerformLayout();
            this.entitiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entitiesSplitContainer)).EndInit();
            this.entitiesSplitContainer.ResumeLayout(false);
            this.EntityInformationSplitContainer.Panel1.ResumeLayout(false);
            this.EntityInformationSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EntityInformationSplitContainer)).EndInit();
            this.EntityInformationSplitContainer.ResumeLayout(false);
            this.grpBxEntityAttributes.ResumeLayout(false);
            this.grpBoxEntityInformation.ResumeLayout(false);
            this.grpBoxEntityInformation.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControlSnippets.ResumeLayout(false);
            this.tabOperation.ResumeLayout(false);
            this.splitContainerOperation.Panel1.ResumeLayout(false);
            this.splitContainerOperation.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOperation)).EndInit();
            this.splitContainerOperation.ResumeLayout(false);
            this.groupBoxSnippetSettings.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.grpBxAssociationEntity.ResumeLayout(false);
            this.grpBxAssociationEntity.PerformLayout();
            this.snippetsContextMenu.ResumeLayout(false);
            this.tabWrapperFunction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtAllEntitiesFilter;
        private System.Windows.Forms.SplitContainer entitiesSplitContainer;
        private System.Windows.Forms.ListBox lstBxAllEntities;
        private System.Windows.Forms.SplitContainer EntityInformationSplitContainer;
        private System.Windows.Forms.CheckBox chkBxIsWebAPIEnabled;
        private SimpleSyntaxHighlightingRTB rchTxtBxWrapperFunction;
        private System.Windows.Forms.CheckedListBox chkdLstBxAllAttibutes;
        private System.Windows.Forms.GroupBox grpBoxEntityInformation;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripButton tsbSwitchInnerError;
        private System.Windows.Forms.ToolStripComboBox tsbWebsiteList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControlSnippets;
        private System.Windows.Forms.TabPage tabWrapperFunction;
        private System.Windows.Forms.GroupBox grpBxEntityAttributes;
        private System.Windows.Forms.ToolStripLabel tsbWebsiteLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tsbHowTo;
        private System.Windows.Forms.ToolStripButton tsbSaveChanges;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip snippetsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.TabPage tabOperation;
        private SimpleSyntaxHighlightingRTB rchTxtBoxOperation;
        private System.Windows.Forms.SplitContainer splitContainerOperation;
        private System.Windows.Forms.Label lblOperationType;
        private System.Windows.Forms.ComboBox cbBxOperationType;
        private System.Windows.Forms.GroupBox groupBoxSnippetSettings;
        private System.Windows.Forms.CheckBox chBxUseSelectedFields;
        private System.Windows.Forms.Label lblAssociateWith;
        private System.Windows.Forms.ComboBox cbBxAssociateWith;
        private System.Windows.Forms.GroupBox grpBxAssociationEntity;
        private System.Windows.Forms.Label lblOperationMessage;
        private System.Windows.Forms.CheckBox chkBxSelectAllAttributes;
        private System.Windows.Forms.Label lblEntityPermissionsNotification;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCreateEntityPermission;
    }
}
