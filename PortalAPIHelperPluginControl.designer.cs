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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortalAPIHelperPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.tsbSwitchInnerError = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbWebsiteList = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAllEntitiesFilter = new System.Windows.Forms.TextBox();
            this.lstBxAllEntities = new System.Windows.Forms.ListBox();
            this.EntityInformationContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGenerateSnippets = new System.Windows.Forms.Button();
            this.btnSaveEntityChanges = new System.Windows.Forms.Button();
            this.lblEntityDisplayName = new System.Windows.Forms.Label();
            this.chkBxIsWebAPIEnabled = new System.Windows.Forms.CheckBox();
            this.lblEntityLogicalName = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAttributeFilter = new System.Windows.Forms.TextBox();
            this.chkdLstBxAllAttibutes = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControlSnippets = new System.Windows.Forms.TabControl();
            this.tabWrapperFunction = new System.Windows.Forms.TabPage();
            this.rchTxtBxWrapperFunction = new System.Windows.Forms.RichTextBox();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.rchTxtBxCreate = new System.Windows.Forms.RichTextBox();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.rchTxtBxUpdate = new System.Windows.Forms.RichTextBox();
            this.tabDelete = new System.Windows.Forms.TabPage();
            this.rchTxtBxDelete = new System.Windows.Forms.RichTextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControlSnippets.SuspendLayout();
            this.tabWrapperFunction.SuspendLayout();
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
            this.tsbSwitchInnerError,
            this.toolStripSeparator1,
            this.tsbWebsiteList});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1226, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(86, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(78, 22);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.LoadAllEntities_Click);
            // 
            // tsbSwitchInnerError
            // 
            this.tsbSwitchInnerError.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSwitchInnerError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSwitchInnerError.Image = ((System.Drawing.Image)(resources.GetObject("tsbSwitchInnerError.Image")));
            this.tsbSwitchInnerError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwitchInnerError.Name = "tsbSwitchInnerError";
            this.tsbSwitchInnerError.Size = new System.Drawing.Size(152, 22);
            this.tsbSwitchInnerError.Text = "Enable Inner Error Tracking";
            this.tsbSwitchInnerError.Visible = false;
            this.tsbSwitchInnerError.Click += new System.EventHandler(this.tsbSwitchInnerError_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbWebsiteList
            // 
            this.tsbWebsiteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbWebsiteList.Name = "tsbWebsiteList";
            this.tsbWebsiteList.Size = new System.Drawing.Size(200, 25);
            this.tsbWebsiteList.SelectedIndexChanged += new System.EventHandler(this.tsbWebsiteList_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.EntityInformationContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1226, 498);
            this.splitContainer1.SplitterDistance = 209;
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
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.txtAllEntitiesFilter);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstBxAllEntities);
            this.splitContainer2.Size = new System.Drawing.Size(209, 498);
            this.splitContainer2.SplitterDistance = 43;
            this.splitContainer2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search for an Entity";
            // 
            // txtAllEntitiesFilter
            // 
            this.txtAllEntitiesFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtAllEntitiesFilter.Location = new System.Drawing.Point(0, 23);
            this.txtAllEntitiesFilter.Name = "txtAllEntitiesFilter";
            this.txtAllEntitiesFilter.Size = new System.Drawing.Size(209, 20);
            this.txtAllEntitiesFilter.TabIndex = 1;
            this.txtAllEntitiesFilter.TextChanged += new System.EventHandler(this.txtAllEntitiesFilter_TextChanged);
            // 
            // lstBxAllEntities
            // 
            this.lstBxAllEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxAllEntities.FormattingEnabled = true;
            this.lstBxAllEntities.Location = new System.Drawing.Point(0, 0);
            this.lstBxAllEntities.Name = "lstBxAllEntities";
            this.lstBxAllEntities.Size = new System.Drawing.Size(209, 451);
            this.lstBxAllEntities.Sorted = true;
            this.lstBxAllEntities.TabIndex = 0;
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
            this.EntityInformationContainer.Panel1.Controls.Add(this.splitContainer4);
            // 
            // EntityInformationContainer.Panel2
            // 
            this.EntityInformationContainer.Panel2.Controls.Add(this.groupBox3);
            this.EntityInformationContainer.Size = new System.Drawing.Size(1013, 498);
            this.EntityInformationContainer.SplitterDistance = 514;
            this.EntityInformationContainer.TabIndex = 0;
            this.EntityInformationContainer.Visible = false;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer4.Size = new System.Drawing.Size(514, 498);
            this.splitContainer4.SplitterDistance = 144;
            this.splitContainer4.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGenerateSnippets);
            this.groupBox1.Controls.Add(this.btnSaveEntityChanges);
            this.groupBox1.Controls.Add(this.lblEntityDisplayName);
            this.groupBox1.Controls.Add(this.chkBxIsWebAPIEnabled);
            this.groupBox1.Controls.Add(this.lblEntityLogicalName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 144);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity Information";
            // 
            // btnGenerateSnippets
            // 
            this.btnGenerateSnippets.Location = new System.Drawing.Point(367, 56);
            this.btnGenerateSnippets.Name = "btnGenerateSnippets";
            this.btnGenerateSnippets.Size = new System.Drawing.Size(131, 23);
            this.btnGenerateSnippets.TabIndex = 4;
            this.btnGenerateSnippets.Text = "Generate Snippets";
            this.btnGenerateSnippets.UseVisualStyleBackColor = true;
            this.btnGenerateSnippets.Click += new System.EventHandler(this.btnGenerateSnippets_Click);
            // 
            // btnSaveEntityChanges
            // 
            this.btnSaveEntityChanges.Location = new System.Drawing.Point(367, 23);
            this.btnSaveEntityChanges.Name = "btnSaveEntityChanges";
            this.btnSaveEntityChanges.Size = new System.Drawing.Size(131, 23);
            this.btnSaveEntityChanges.TabIndex = 3;
            this.btnSaveEntityChanges.Text = "Save Entity Changes";
            this.btnSaveEntityChanges.UseVisualStyleBackColor = true;
            this.btnSaveEntityChanges.Click += new System.EventHandler(this.btnSaveEntityChanges_Click);
            // 
            // lblEntityDisplayName
            // 
            this.lblEntityDisplayName.AutoSize = true;
            this.lblEntityDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityDisplayName.Location = new System.Drawing.Point(6, 24);
            this.lblEntityDisplayName.Name = "lblEntityDisplayName";
            this.lblEntityDisplayName.Size = new System.Drawing.Size(245, 24);
            this.lblEntityDisplayName.TabIndex = 1;
            this.lblEntityDisplayName.Text = "Sample Entity Display Name";
            // 
            // chkBxIsWebAPIEnabled
            // 
            this.chkBxIsWebAPIEnabled.AutoSize = true;
            this.chkBxIsWebAPIEnabled.Location = new System.Drawing.Point(10, 101);
            this.chkBxIsWebAPIEnabled.Name = "chkBxIsWebAPIEnabled";
            this.chkBxIsWebAPIEnabled.Size = new System.Drawing.Size(135, 17);
            this.chkBxIsWebAPIEnabled.TabIndex = 0;
            this.chkBxIsWebAPIEnabled.Text = "Enabled For Web API?";
            this.chkBxIsWebAPIEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEntityLogicalName
            // 
            this.lblEntityLogicalName.AutoSize = true;
            this.lblEntityLogicalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityLogicalName.Location = new System.Drawing.Point(6, 56);
            this.lblEntityLogicalName.Name = "lblEntityLogicalName";
            this.lblEntityLogicalName.Size = new System.Drawing.Size(245, 24);
            this.lblEntityLogicalName.TabIndex = 2;
            this.lblEntityLogicalName.Text = "Sample Entity Logical Name";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.txtAttributeFilter);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.chkdLstBxAllAttibutes);
            this.splitContainer3.Size = new System.Drawing.Size(514, 350);
            this.splitContainer3.SplitterDistance = 41;
            this.splitContainer3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search for Attributes";
            // 
            // txtAttributeFilter
            // 
            this.txtAttributeFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtAttributeFilter.Location = new System.Drawing.Point(0, 21);
            this.txtAttributeFilter.Name = "txtAttributeFilter";
            this.txtAttributeFilter.Size = new System.Drawing.Size(514, 20);
            this.txtAttributeFilter.TabIndex = 1;
            this.txtAttributeFilter.TextChanged += new System.EventHandler(this.txtAttributeFilter_TextChanged);
            // 
            // chkdLstBxAllAttibutes
            // 
            this.chkdLstBxAllAttibutes.CheckOnClick = true;
            this.chkdLstBxAllAttibutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdLstBxAllAttibutes.FormattingEnabled = true;
            this.chkdLstBxAllAttibutes.Location = new System.Drawing.Point(0, 0);
            this.chkdLstBxAllAttibutes.Name = "chkdLstBxAllAttibutes";
            this.chkdLstBxAllAttibutes.Size = new System.Drawing.Size(514, 305);
            this.chkdLstBxAllAttibutes.Sorted = true;
            this.chkdLstBxAllAttibutes.TabIndex = 0;
            this.chkdLstBxAllAttibutes.SelectedIndexChanged += new System.EventHandler(this.EntityAttributesListBox_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControlSnippets);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(495, 498);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code Snippets";
            // 
            // tabControlSnippets
            // 
            this.tabControlSnippets.Controls.Add(this.tabWrapperFunction);
            this.tabControlSnippets.Controls.Add(this.tabCreate);
            this.tabControlSnippets.Controls.Add(this.tabUpdate);
            this.tabControlSnippets.Controls.Add(this.tabDelete);
            this.tabControlSnippets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSnippets.Location = new System.Drawing.Point(3, 16);
            this.tabControlSnippets.Name = "tabControlSnippets";
            this.tabControlSnippets.SelectedIndex = 0;
            this.tabControlSnippets.Size = new System.Drawing.Size(489, 479);
            this.tabControlSnippets.TabIndex = 1;
            // 
            // tabWrapperFunction
            // 
            this.tabWrapperFunction.Controls.Add(this.rchTxtBxWrapperFunction);
            this.tabWrapperFunction.Location = new System.Drawing.Point(4, 22);
            this.tabWrapperFunction.Name = "tabWrapperFunction";
            this.tabWrapperFunction.Padding = new System.Windows.Forms.Padding(3);
            this.tabWrapperFunction.Size = new System.Drawing.Size(481, 453);
            this.tabWrapperFunction.TabIndex = 0;
            this.tabWrapperFunction.Text = "Wrapper Function";
            this.tabWrapperFunction.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxWrapperFunction
            // 
            this.rchTxtBxWrapperFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxWrapperFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxWrapperFunction.Location = new System.Drawing.Point(3, 3);
            this.rchTxtBxWrapperFunction.Name = "rchTxtBxWrapperFunction";
            this.rchTxtBxWrapperFunction.Size = new System.Drawing.Size(475, 447);
            this.rchTxtBxWrapperFunction.TabIndex = 0;
            this.rchTxtBxWrapperFunction.Text = "";
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.rchTxtBxCreate);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreate.Size = new System.Drawing.Size(481, 453);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxCreate
            // 
            this.rchTxtBxCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxCreate.Location = new System.Drawing.Point(3, 3);
            this.rchTxtBxCreate.Name = "rchTxtBxCreate";
            this.rchTxtBxCreate.Size = new System.Drawing.Size(475, 447);
            this.rchTxtBxCreate.TabIndex = 1;
            this.rchTxtBxCreate.Text = "SomeText";
            // 
            // tabUpdate
            // 
            this.tabUpdate.Controls.Add(this.rchTxtBxUpdate);
            this.tabUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdate.Size = new System.Drawing.Size(481, 453);
            this.tabUpdate.TabIndex = 3;
            this.tabUpdate.Text = "Update";
            this.tabUpdate.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxUpdate
            // 
            this.rchTxtBxUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxUpdate.Location = new System.Drawing.Point(3, 3);
            this.rchTxtBxUpdate.Name = "rchTxtBxUpdate";
            this.rchTxtBxUpdate.Size = new System.Drawing.Size(475, 447);
            this.rchTxtBxUpdate.TabIndex = 1;
            this.rchTxtBxUpdate.Text = "";
            // 
            // tabDelete
            // 
            this.tabDelete.Controls.Add(this.rchTxtBxDelete);
            this.tabDelete.Location = new System.Drawing.Point(4, 22);
            this.tabDelete.Name = "tabDelete";
            this.tabDelete.Size = new System.Drawing.Size(481, 453);
            this.tabDelete.TabIndex = 2;
            this.tabDelete.Text = "Delete";
            this.tabDelete.UseVisualStyleBackColor = true;
            // 
            // rchTxtBxDelete
            // 
            this.rchTxtBxDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchTxtBxDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtBxDelete.Location = new System.Drawing.Point(0, 0);
            this.rchTxtBxDelete.Name = "rchTxtBxDelete";
            this.rchTxtBxDelete.Size = new System.Drawing.Size(481, 453);
            this.rchTxtBxDelete.TabIndex = 2;
            this.rchTxtBxDelete.Text = "";
            // 
            // PortalAPIHelperPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "PortalAPIHelperPluginControl";
            this.Size = new System.Drawing.Size(1226, 523);
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
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControlSnippets.ResumeLayout(false);
            this.tabWrapperFunction.ResumeLayout(false);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstBxAllEntities;
        private System.Windows.Forms.SplitContainer EntityInformationContainer;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.CheckBox chkBxIsWebAPIEnabled;
        private System.Windows.Forms.RichTextBox rchTxtBxWrapperFunction;
        private System.Windows.Forms.CheckedListBox chkdLstBxAllAttibutes;
        private System.Windows.Forms.Label lblEntityDisplayName;
        private System.Windows.Forms.Label lblEntityLogicalName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripButton tsbSwitchInnerError;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAttributeFilter;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStripComboBox tsbWebsiteList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnSaveEntityChanges;
        private System.Windows.Forms.Button btnGenerateSnippets;
        private System.Windows.Forms.TabControl tabControlSnippets;
        private System.Windows.Forms.TabPage tabWrapperFunction;
        private System.Windows.Forms.TabPage tabCreate;
        private System.Windows.Forms.TabPage tabDelete;
        private System.Windows.Forms.RichTextBox rchTxtBxCreate;
        private System.Windows.Forms.RichTextBox rchTxtBxDelete;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.RichTextBox rchTxtBxUpdate;
    }
}
