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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.AllEntitiesFilter = new System.Windows.Forms.TextBox();
            this.AllEntitiesList = new System.Windows.Forms.ListBox();
            this.EntityInformationContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.ChkBxIsWebAPIEnabled = new System.Windows.Forms.CheckBox();
            this.sampleCodeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.EntityFieldsList = new System.Windows.Forms.CheckedListBox();
            this.tsbSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.lblEntityDisplayName = new System.Windows.Forms.Label();
            this.lblEntityLogicalName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tsbSwitchInnerError = new System.Windows.Forms.ToolStripButton();
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
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbLoadEntities,
            this.tsbSaveChanges,
            this.tsbSwitchInnerError});
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
            this.splitContainer2.Panel1.Controls.Add(this.AllEntitiesFilter);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.AllEntitiesList);
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
            // AllEntitiesFilter
            // 
            this.AllEntitiesFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AllEntitiesFilter.Location = new System.Drawing.Point(0, 23);
            this.AllEntitiesFilter.Name = "AllEntitiesFilter";
            this.AllEntitiesFilter.Size = new System.Drawing.Size(209, 20);
            this.AllEntitiesFilter.TabIndex = 1;
            // 
            // AllEntitiesList
            // 
            this.AllEntitiesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllEntitiesList.FormattingEnabled = true;
            this.AllEntitiesList.Location = new System.Drawing.Point(0, 0);
            this.AllEntitiesList.Name = "AllEntitiesList";
            this.AllEntitiesList.Size = new System.Drawing.Size(209, 451);
            this.AllEntitiesList.TabIndex = 0;
            this.AllEntitiesList.Click += new System.EventHandler(this.AllEntitiesList_Click);
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
            this.splitContainer4.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer4.Size = new System.Drawing.Size(514, 498);
            this.splitContainer4.SplitterDistance = 144;
            this.splitContainer4.TabIndex = 0;
            // 
            // ChkBxIsWebAPIEnabled
            // 
            this.ChkBxIsWebAPIEnabled.AutoSize = true;
            this.ChkBxIsWebAPIEnabled.Location = new System.Drawing.Point(10, 101);
            this.ChkBxIsWebAPIEnabled.Name = "ChkBxIsWebAPIEnabled";
            this.ChkBxIsWebAPIEnabled.Size = new System.Drawing.Size(135, 17);
            this.ChkBxIsWebAPIEnabled.TabIndex = 0;
            this.ChkBxIsWebAPIEnabled.Text = "Enabled For Web API?";
            this.ChkBxIsWebAPIEnabled.UseVisualStyleBackColor = true;
            // 
            // sampleCodeRichTextBox
            // 
            this.sampleCodeRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleCodeRichTextBox.Location = new System.Drawing.Point(3, 16);
            this.sampleCodeRichTextBox.Name = "sampleCodeRichTextBox";
            this.sampleCodeRichTextBox.Size = new System.Drawing.Size(489, 479);
            this.sampleCodeRichTextBox.TabIndex = 0;
            this.sampleCodeRichTextBox.Text = "";
            // 
            // EntityFieldsList
            // 
            this.EntityFieldsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntityFieldsList.FormattingEnabled = true;
            this.EntityFieldsList.Location = new System.Drawing.Point(3, 16);
            this.EntityFieldsList.Name = "EntityFieldsList";
            this.EntityFieldsList.Size = new System.Drawing.Size(508, 331);
            this.EntityFieldsList.TabIndex = 0;
            // 
            // tsbSaveChanges
            // 
            this.tsbSaveChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSaveChanges.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveChanges.Image")));
            this.tsbSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveChanges.Name = "tsbSaveChanges";
            this.tsbSaveChanges.Size = new System.Drawing.Size(84, 22);
            this.tsbSaveChanges.Text = "Save Changes";
            this.tsbSaveChanges.Visible = false;
            // 
            // lblEntityDisplayName
            // 
            this.lblEntityDisplayName.AutoSize = true;
            this.lblEntityDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityDisplayName.Location = new System.Drawing.Point(6, 24);
            this.lblEntityDisplayName.Name = "lblEntityDisplayName";
            this.lblEntityDisplayName.Size = new System.Drawing.Size(60, 24);
            this.lblEntityDisplayName.TabIndex = 1;
            this.lblEntityDisplayName.Text = "label2";
            // 
            // lblEntityLogicalName
            // 
            this.lblEntityLogicalName.AutoSize = true;
            this.lblEntityLogicalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityLogicalName.Location = new System.Drawing.Point(6, 52);
            this.lblEntityLogicalName.Name = "lblEntityLogicalName";
            this.lblEntityLogicalName.Size = new System.Drawing.Size(60, 24);
            this.lblEntityLogicalName.TabIndex = 2;
            this.lblEntityLogicalName.Text = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEntityDisplayName);
            this.groupBox1.Controls.Add(this.ChkBxIsWebAPIEnabled);
            this.groupBox1.Controls.Add(this.lblEntityLogicalName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 144);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EntityFieldsList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 350);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entity Attributes (Only Checked ones are enabled for portal)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sampleCodeRichTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(495, 498);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code Snippets";
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
            // PortalAPIHelperPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "PortalAPIHelperPluginControl";
            this.Size = new System.Drawing.Size(1226, 523);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox AllEntitiesFilter;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox AllEntitiesList;
        private System.Windows.Forms.SplitContainer EntityInformationContainer;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.CheckBox ChkBxIsWebAPIEnabled;
        private System.Windows.Forms.RichTextBox sampleCodeRichTextBox;
        private System.Windows.Forms.CheckedListBox EntityFieldsList;
        private System.Windows.Forms.ToolStripButton tsbSaveChanges;
        private System.Windows.Forms.Label lblEntityDisplayName;
        private System.Windows.Forms.Label lblEntityLogicalName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripButton tsbSwitchInnerError;
    }
}
