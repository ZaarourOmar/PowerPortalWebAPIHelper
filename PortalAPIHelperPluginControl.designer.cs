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
            this.tsbGenerateSnippets = new System.Windows.Forms.ToolStripButton();
            this.tsbWebsiteList = new System.Windows.Forms.ToolStripComboBox();
            this.tsbWebsiteLabel = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtAllEntitiesFilter = new System.Windows.Forms.TextBox();
            this.lstBxAllEntities = new System.Windows.Forms.ListBox();
            this.EntityInformationContainer = new System.Windows.Forms.SplitContainer();
            this.grpBxEntityAttributes = new System.Windows.Forms.GroupBox();
            this.chkdLstBxAllAttibutes = new System.Windows.Forms.CheckedListBox();
            this.txtAttributeFilter = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEntityDisplayName = new System.Windows.Forms.Label();
            this.chkBxIsWebAPIEnabled = new System.Windows.Forms.CheckBox();
            this.lblEntityLogicalName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControlSnippets = new System.Windows.Forms.TabControl();
            this.tabWrapperFunction = new System.Windows.Forms.TabPage();
            this.rchTxtBxWrapperFunction = new PowerPortalWebAPIHelper.Extensions.SimpleSyntaxHighlightingRTB();
            this.snippetsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tabOperation = new System.Windows.Forms.TabPage();
            this.splitContainerOperation = new System.Windows.Forms.SplitContainer();
            this.groupBoxSnippetSettings = new System.Windows.Forms.GroupBox();
            this.lblOperationMessage = new System.Windows.Forms.Label();
            this.grpBxAssociationEntity = new System.Windows.Forms.GroupBox();
            this.lblAssociateWith = new System.Windows.Forms.Label();
            this.cbBxAssociateWith = new System.Windows.Forms.ComboBox();
            this.chBxUseSelectedFields = new System.Windows.Forms.CheckBox();
            this.btnGenerateSnippet = new System.Windows.Forms.Button();
            this.lblOperationType = new System.Windows.Forms.Label();
            this.cbBxOperationType = new System.Windows.Forms.ComboBox();
            this.rchTxtBoxOperation = new PowerPortalWebAPIHelper.Extensions.SimpleSyntaxHighlightingRTB();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.rchTxtBxCreate = new PowerPortalWebAPIHelper.Extensions.SimpleSyntaxHighlightingRTB();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.rchTxtBxUpdate = new PowerPortalWebAPIHelper.Extensions.SimpleSyntaxHighlightingRTB();
            this.tabDelete = new System.Windows.Forms.TabPage();
            this.rchTxtBxDelete = new PowerPortalWebAPIHelper.Extensions.SimpleSyntaxHighlightingRTB();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EntityInformationContainer)).BeginInit();
            this.EntityInformationContainer.Panel1.SuspendLayout();
            this.EntityInformationContainer.Panel2.SuspendLayout();
            this.EntityInformationContainer.SuspendLayout();
            this.grpBxEntityAttributes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControlSnippets.SuspendLayout();
            this.tabWrapperFunction.SuspendLayout();
            this.snippetsContextMenu.SuspendLayout();
            this.tabOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOperation)).BeginInit();
            this.splitContainerOperation.Panel1.SuspendLayout();
            this.splitContainerOperation.Panel2.SuspendLayout();
            this.splitContainerOperation.SuspendLayout();
            this.groupBoxSnippetSettings.SuspendLayout();
            this.grpBxAssociationEntity.SuspendLayout();
            this.tabCreate.SuspendLayout();
            this.tabUpdate.SuspendLayout();
            this.tabDelete.SuspendLayout();
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
            this.tsbGenerateSnippets,
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
            this.tsbSwitchInnerError.Size = new System.Drawing.Size(235, 28);
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
            // tsbGenerateSnippets
            // 
            this.tsbGenerateSnippets.Enabled = false;
            this.tsbGenerateSnippets.Image = global::PowerPortalWebAPIHelper.Properties.Resources.snippetsicon;
            this.tsbGenerateSnippets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGenerateSnippets.Name = "tsbGenerateSnippets";
            this.tsbGenerateSnippets.Size = new System.Drawing.Size(130, 28);
            this.tsbGenerateSnippets.Text = "Generate Snippets";
            this.tsbGenerateSnippets.Click += new System.EventHandler(this.tsbGenerateSnippets_Click);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.EntityInformationContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1240, 534);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtAllEntitiesFilter);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstBxAllEntities);
            this.splitContainer2.Size = new System.Drawing.Size(207, 534);
            this.splitContainer2.SplitterDistance = 27;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtAllEntitiesFilter
            // 
            this.txtAllEntitiesFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtAllEntitiesFilter.Location = new System.Drawing.Point(0, 7);
            this.txtAllEntitiesFilter.Name = "txtAllEntitiesFilter";
            this.txtAllEntitiesFilter.Size = new System.Drawing.Size(207, 20);
            this.txtAllEntitiesFilter.TabIndex = 0;
            this.txtAllEntitiesFilter.TextChanged += new System.EventHandler(this.txtAllEntitiesFilter_TextChanged);
            // 
            // lstBxAllEntities
            // 
            this.lstBxAllEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxAllEntities.FormattingEnabled = true;
            this.lstBxAllEntities.Location = new System.Drawing.Point(0, 0);
            this.lstBxAllEntities.Name = "lstBxAllEntities";
            this.lstBxAllEntities.Size = new System.Drawing.Size(207, 503);
            this.lstBxAllEntities.Sorted = true;
            this.lstBxAllEntities.TabIndex = 1;
            this.lstBxAllEntities.SelectedIndexChanged += new System.EventHandler(this.AllEntitiesListBox_SelectedIndexChanged);
            // 
            // EntityInformationContainer
            // 
            this.EntityInformationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntityInformationContainer.Location = new System.Drawing.Point(0, 0);
            this.EntityInformationContainer.Name = "EntityInformationContainer";
            // 
            // EntityInformationContainer.Panel1
            // 
            this.EntityInformationContainer.Panel1.Controls.Add(this.grpBxEntityAttributes);
            this.EntityInformationContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // EntityInformationContainer.Panel2
            // 
            this.EntityInformationContainer.Panel2.Controls.Add(this.groupBox3);
            this.EntityInformationContainer.Size = new System.Drawing.Size(1029, 534);
            this.EntityInformationContainer.SplitterDistance = 472;
            this.EntityInformationContainer.TabIndex = 0;
            this.EntityInformationContainer.Visible = false;
            // 
            // grpBxEntityAttributes
            // 
            this.grpBxEntityAttributes.Controls.Add(this.chkdLstBxAllAttibutes);
            this.grpBxEntityAttributes.Controls.Add(this.txtAttributeFilter);
            this.grpBxEntityAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxEntityAttributes.Location = new System.Drawing.Point(0, 127);
            this.grpBxEntityAttributes.Name = "grpBxEntityAttributes";
            this.grpBxEntityAttributes.Size = new System.Drawing.Size(472, 407);
            this.grpBxEntityAttributes.TabIndex = 6;
            this.grpBxEntityAttributes.TabStop = false;
            this.grpBxEntityAttributes.Text = "Entity Attributes";
            // 
            // chkdLstBxAllAttibutes
            // 
            this.chkdLstBxAllAttibutes.CheckOnClick = true;
            this.chkdLstBxAllAttibutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdLstBxAllAttibutes.FormattingEnabled = true;
            this.chkdLstBxAllAttibutes.Location = new System.Drawing.Point(3, 36);
            this.chkdLstBxAllAttibutes.Name = "chkdLstBxAllAttibutes";
            this.chkdLstBxAllAttibutes.Size = new System.Drawing.Size(466, 368);
            this.chkdLstBxAllAttibutes.Sorted = true;
            this.chkdLstBxAllAttibutes.TabIndex = 5;
            this.chkdLstBxAllAttibutes.SelectedIndexChanged += new System.EventHandler(this.EntityAttributesListBox_SelectedIndexChanged);
            // 
            // txtAttributeFilter
            // 
            this.txtAttributeFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtAttributeFilter.Location = new System.Drawing.Point(3, 16);
            this.txtAttributeFilter.Name = "txtAttributeFilter";
            this.txtAttributeFilter.Size = new System.Drawing.Size(466, 20);
            this.txtAttributeFilter.TabIndex = 4;
            this.txtAttributeFilter.TextChanged += new System.EventHandler(this.txtAttributeFilter_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEntityDisplayName);
            this.groupBox1.Controls.Add(this.chkBxIsWebAPIEnabled);
            this.groupBox1.Controls.Add(this.lblEntityLogicalName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(300, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 127);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity Information";
            // 
            // lblEntityDisplayName
            // 
            this.lblEntityDisplayName.AutoSize = true;
            this.lblEntityDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityDisplayName.Location = new System.Drawing.Point(6, 24);
            this.lblEntityDisplayName.Name = "lblEntityDisplayName";
            this.lblEntityDisplayName.Size = new System.Drawing.Size(194, 18);
            this.lblEntityDisplayName.TabIndex = 1;
            this.lblEntityDisplayName.Text = "Sample Entity Display Name";
            // 
            // chkBxIsWebAPIEnabled
            // 
            this.chkBxIsWebAPIEnabled.AutoSize = true;
            this.chkBxIsWebAPIEnabled.Location = new System.Drawing.Point(10, 101);
            this.chkBxIsWebAPIEnabled.Name = "chkBxIsWebAPIEnabled";
            this.chkBxIsWebAPIEnabled.Size = new System.Drawing.Size(135, 17);
            this.chkBxIsWebAPIEnabled.TabIndex = 3;
            this.chkBxIsWebAPIEnabled.Text = "Enabled For Web API?";
            this.chkBxIsWebAPIEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEntityLogicalName
            // 
            this.lblEntityLogicalName.AutoSize = true;
            this.lblEntityLogicalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityLogicalName.Location = new System.Drawing.Point(6, 56);
            this.lblEntityLogicalName.Name = "lblEntityLogicalName";
            this.lblEntityLogicalName.Size = new System.Drawing.Size(193, 18);
            this.lblEntityLogicalName.TabIndex = 2;
            this.lblEntityLogicalName.Text = "Sample Entity Logical Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControlSnippets);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(553, 534);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code Snippets";
            // 
            // tabControlSnippets
            // 
            this.tabControlSnippets.Controls.Add(this.tabWrapperFunction);
            this.tabControlSnippets.Controls.Add(this.tabOperation);
            this.tabControlSnippets.Controls.Add(this.tabCreate);
            this.tabControlSnippets.Controls.Add(this.tabUpdate);
            this.tabControlSnippets.Controls.Add(this.tabDelete);
            this.tabControlSnippets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSnippets.Location = new System.Drawing.Point(3, 16);
            this.tabControlSnippets.Name = "tabControlSnippets";
            this.tabControlSnippets.SelectedIndex = 0;
            this.tabControlSnippets.Size = new System.Drawing.Size(547, 515);
            this.tabControlSnippets.TabIndex = 7;
            // 
            // tabWrapperFunction
            // 
            this.tabWrapperFunction.Controls.Add(this.rchTxtBxWrapperFunction);
            this.tabWrapperFunction.Location = new System.Drawing.Point(4, 22);
            this.tabWrapperFunction.Name = "tabWrapperFunction";
            this.tabWrapperFunction.Padding = new System.Windows.Forms.Padding(3);
            this.tabWrapperFunction.Size = new System.Drawing.Size(539, 489);
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
            this.rchTxtBxWrapperFunction.Size = new System.Drawing.Size(533, 483);
            this.rchTxtBxWrapperFunction.TabIndex = 0;
            this.rchTxtBxWrapperFunction.Text = "";
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
            // tabOperation
            // 
            this.tabOperation.Controls.Add(this.splitContainerOperation);
            this.tabOperation.Location = new System.Drawing.Point(4, 22);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperation.Size = new System.Drawing.Size(539, 489);
            this.tabOperation.TabIndex = 4;
            this.tabOperation.Text = "Operation";
            this.tabOperation.UseVisualStyleBackColor = true;
            // 
            // splitContainerOperation
            // 
            this.splitContainerOperation.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainerOperation.Size = new System.Drawing.Size(533, 483);
            this.splitContainerOperation.SplitterDistance = 159;
            this.splitContainerOperation.TabIndex = 2;
            // 
            // groupBoxSnippetSettings
            // 
            this.groupBoxSnippetSettings.Controls.Add(this.lblOperationMessage);
            this.groupBoxSnippetSettings.Controls.Add(this.grpBxAssociationEntity);
            this.groupBoxSnippetSettings.Controls.Add(this.chBxUseSelectedFields);
            this.groupBoxSnippetSettings.Controls.Add(this.btnGenerateSnippet);
            this.groupBoxSnippetSettings.Controls.Add(this.lblOperationType);
            this.groupBoxSnippetSettings.Controls.Add(this.cbBxOperationType);
            this.groupBoxSnippetSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSnippetSettings.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSnippetSettings.Name = "groupBoxSnippetSettings";
            this.groupBoxSnippetSettings.Size = new System.Drawing.Size(533, 159);
            this.groupBoxSnippetSettings.TabIndex = 3;
            this.groupBoxSnippetSettings.TabStop = false;
            this.groupBoxSnippetSettings.Text = "Snippet Settings";
            // 
            // lblOperationMessage
            // 
            this.lblOperationMessage.AutoSize = true;
            this.lblOperationMessage.Location = new System.Drawing.Point(7, 49);
            this.lblOperationMessage.Name = "lblOperationMessage";
            this.lblOperationMessage.Size = new System.Drawing.Size(24, 13);
            this.lblOperationMessage.TabIndex = 8;
            this.lblOperationMessage.Text = "test";
            // 
            // grpBxAssociationEntity
            // 
            this.grpBxAssociationEntity.Controls.Add(this.lblAssociateWith);
            this.grpBxAssociationEntity.Controls.Add(this.cbBxAssociateWith);
            this.grpBxAssociationEntity.Location = new System.Drawing.Point(9, 80);
            this.grpBxAssociationEntity.Name = "grpBxAssociationEntity";
            this.grpBxAssociationEntity.Size = new System.Drawing.Size(391, 73);
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
            this.cbBxAssociateWith.Size = new System.Drawing.Size(302, 21);
            this.cbBxAssociateWith.TabIndex = 5;
            // 
            // chBxUseSelectedFields
            // 
            this.chBxUseSelectedFields.Location = new System.Drawing.Point(232, 10);
            this.chBxUseSelectedFields.Name = "chBxUseSelectedFields";
            this.chBxUseSelectedFields.Size = new System.Drawing.Size(283, 52);
            this.chBxUseSelectedFields.TabIndex = 4;
            this.chBxUseSelectedFields.Text = "Use Selected Fields for Create/Update record operation in the Snippet";
            this.chBxUseSelectedFields.UseVisualStyleBackColor = true;
            // 
            // btnGenerateSnippet
            // 
            this.btnGenerateSnippet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateSnippet.Location = new System.Drawing.Point(418, 130);
            this.btnGenerateSnippet.Name = "btnGenerateSnippet";
            this.btnGenerateSnippet.Size = new System.Drawing.Size(109, 23);
            this.btnGenerateSnippet.TabIndex = 3;
            this.btnGenerateSnippet.Text = "Generate Snippet";
            this.btnGenerateSnippet.UseVisualStyleBackColor = true;
            this.btnGenerateSnippet.Click += new System.EventHandler(this.btnGenerateSnippet_Click);
            // 
            // lblOperationType
            // 
            this.lblOperationType.AutoSize = true;
            this.lblOperationType.Location = new System.Drawing.Point(6, 29);
            this.lblOperationType.Name = "lblOperationType";
            this.lblOperationType.Size = new System.Drawing.Size(80, 13);
            this.lblOperationType.TabIndex = 1;
            this.lblOperationType.Text = "Operation Type";
            // 
            // cbBxOperationType
            // 
            this.cbBxOperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBxOperationType.FormattingEnabled = true;
            this.cbBxOperationType.Location = new System.Drawing.Point(92, 26);
            this.cbBxOperationType.Name = "cbBxOperationType";
            this.cbBxOperationType.Size = new System.Drawing.Size(121, 21);
            this.cbBxOperationType.TabIndex = 0;
            // 
            // rchTxtBoxOperation
            // 
            this.rchTxtBoxOperation.ContextMenuStrip = this.snippetsContextMenu;
            this.rchTxtBoxOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBoxOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBoxOperation.Location = new System.Drawing.Point(0, 0);
            this.rchTxtBoxOperation.Name = "rchTxtBoxOperation";
            this.rchTxtBoxOperation.Size = new System.Drawing.Size(533, 320);
            this.rchTxtBoxOperation.TabIndex = 1;
            this.rchTxtBoxOperation.Text = "";
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.rchTxtBxCreate);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreate.Size = new System.Drawing.Size(539, 489);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxCreate
            // 
            this.rchTxtBxCreate.ContextMenuStrip = this.snippetsContextMenu;
            this.rchTxtBxCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxCreate.Location = new System.Drawing.Point(3, 3);
            this.rchTxtBxCreate.Name = "rchTxtBxCreate";
            this.rchTxtBxCreate.Size = new System.Drawing.Size(533, 483);
            this.rchTxtBxCreate.TabIndex = 1;
            this.rchTxtBxCreate.Text = "SomeText";
            // 
            // tabUpdate
            // 
            this.tabUpdate.Controls.Add(this.rchTxtBxUpdate);
            this.tabUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdate.Size = new System.Drawing.Size(539, 489);
            this.tabUpdate.TabIndex = 3;
            this.tabUpdate.Text = "Update";
            this.tabUpdate.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxUpdate
            // 
            this.rchTxtBxUpdate.ContextMenuStrip = this.snippetsContextMenu;
            this.rchTxtBxUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxUpdate.Location = new System.Drawing.Point(3, 3);
            this.rchTxtBxUpdate.Name = "rchTxtBxUpdate";
            this.rchTxtBxUpdate.Size = new System.Drawing.Size(533, 483);
            this.rchTxtBxUpdate.TabIndex = 1;
            this.rchTxtBxUpdate.Text = "";
            // 
            // tabDelete
            // 
            this.tabDelete.Controls.Add(this.rchTxtBxDelete);
            this.tabDelete.Location = new System.Drawing.Point(4, 22);
            this.tabDelete.Name = "tabDelete";
            this.tabDelete.Size = new System.Drawing.Size(539, 489);
            this.tabDelete.TabIndex = 2;
            this.tabDelete.Text = "Delete";
            this.tabDelete.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxDelete
            // 
            this.rchTxtBxDelete.ContextMenuStrip = this.snippetsContextMenu;
            this.rchTxtBxDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxDelete.Location = new System.Drawing.Point(0, 0);
            this.rchTxtBxDelete.Name = "rchTxtBxDelete";
            this.rchTxtBxDelete.Size = new System.Drawing.Size(539, 489);
            this.rchTxtBxDelete.TabIndex = 2;
            this.rchTxtBxDelete.Text = "";
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
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.EntityInformationContainer.Panel1.ResumeLayout(false);
            this.EntityInformationContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EntityInformationContainer)).EndInit();
            this.EntityInformationContainer.ResumeLayout(false);
            this.grpBxEntityAttributes.ResumeLayout(false);
            this.grpBxEntityAttributes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControlSnippets.ResumeLayout(false);
            this.tabWrapperFunction.ResumeLayout(false);
            this.snippetsContextMenu.ResumeLayout(false);
            this.tabOperation.ResumeLayout(false);
            this.splitContainerOperation.Panel1.ResumeLayout(false);
            this.splitContainerOperation.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOperation)).EndInit();
            this.splitContainerOperation.ResumeLayout(false);
            this.groupBoxSnippetSettings.ResumeLayout(false);
            this.groupBoxSnippetSettings.PerformLayout();
            this.grpBxAssociationEntity.ResumeLayout(false);
            this.grpBxAssociationEntity.PerformLayout();
            this.tabCreate.ResumeLayout(false);
            this.tabUpdate.ResumeLayout(false);
            this.tabDelete.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lstBxAllEntities;
        private System.Windows.Forms.SplitContainer EntityInformationContainer;
        private System.Windows.Forms.CheckBox chkBxIsWebAPIEnabled;
        private SimpleSyntaxHighlightingRTB rchTxtBxWrapperFunction;
        private System.Windows.Forms.CheckedListBox chkdLstBxAllAttibutes;
        private System.Windows.Forms.Label lblEntityDisplayName;
        private System.Windows.Forms.Label lblEntityLogicalName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripButton tsbSwitchInnerError;
        private System.Windows.Forms.TextBox txtAttributeFilter;
        private System.Windows.Forms.ToolStripComboBox tsbWebsiteList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControlSnippets;
        private System.Windows.Forms.TabPage tabWrapperFunction;
        private System.Windows.Forms.TabPage tabCreate;
        private System.Windows.Forms.TabPage tabDelete;
        private SimpleSyntaxHighlightingRTB rchTxtBxCreate;
        private SimpleSyntaxHighlightingRTB rchTxtBxDelete;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.GroupBox grpBxEntityAttributes;
        private System.Windows.Forms.ToolStripLabel tsbWebsiteLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tsbHowTo;
        private SimpleSyntaxHighlightingRTB rchTxtBxUpdate;
        private System.Windows.Forms.ToolStripButton tsbSaveChanges;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbGenerateSnippets;
        private System.Windows.Forms.ContextMenuStrip snippetsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.TabPage tabOperation;
        private SimpleSyntaxHighlightingRTB rchTxtBoxOperation;
        private System.Windows.Forms.SplitContainer splitContainerOperation;
        private System.Windows.Forms.Label lblOperationType;
        private System.Windows.Forms.ComboBox cbBxOperationType;
        private System.Windows.Forms.GroupBox groupBoxSnippetSettings;
        private System.Windows.Forms.Button btnGenerateSnippet;
        private System.Windows.Forms.CheckBox chBxUseSelectedFields;
        private System.Windows.Forms.Label lblAssociateWith;
        private System.Windows.Forms.ComboBox cbBxAssociateWith;
        private System.Windows.Forms.GroupBox grpBxAssociationEntity;
        private System.Windows.Forms.Label lblOperationMessage;
    }
}
