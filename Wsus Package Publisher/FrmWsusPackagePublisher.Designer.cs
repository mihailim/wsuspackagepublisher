namespace Wsus_Package_Publisher
{
    partial class FrmWsusPackagePublisher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWsusPackagePublisher));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnConnectToServer = new System.Windows.Forms.Button();
            this.trvWsus = new System.Windows.Forms.TreeView();
            this.imgLstServer = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.filetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quittoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certificatetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languagetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiReadertoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatestoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createUpdatetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helptoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abouttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbBxServerList = new System.Windows.Forms.ToolStripComboBox();
            this.ctxMnuTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createMetaGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.editMetaGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMetaGroup = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.ctxMnuTreeview.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnConnectToServer);
            this.splitContainer1.Panel1.Controls.Add(this.trvWsus);
            // 
            // btnConnectToServer
            // 
            resources.ApplyResources(this.btnConnectToServer, "btnConnectToServer");
            this.btnConnectToServer.Name = "btnConnectToServer";
            this.btnConnectToServer.UseVisualStyleBackColor = true;
            this.btnConnectToServer.Click += new System.EventHandler(this.btnConnectToServer_Click);
            // 
            // trvWsus
            // 
            this.trvWsus.AllowDrop = true;
            resources.ApplyResources(this.trvWsus, "trvWsus");
            this.trvWsus.ItemHeight = 16;
            this.trvWsus.Name = "trvWsus";
            this.trvWsus.StateImageList = this.imgLstServer;
            this.trvWsus.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvWsus_AfterSelect);
            this.trvWsus.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvWsus_DragDrop);
            this.trvWsus.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvWsus_DragEnter);
            // 
            // imgLstServer
            // 
            this.imgLstServer.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imgLstServer, "imgLstServer");
            this.imgLstServer.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filetoolStripMenuItem,
            this.toolstoolStripMenuItem,
            this.updatestoolStripMenuItem,
            this.helptoolStripMenuItem,
            this.cmbBxServerList});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // filetoolStripMenuItem
            // 
            this.filetoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quittoolStripMenuItem});
            this.filetoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.filetoolStripMenuItem.Name = "filetoolStripMenuItem";
            resources.ApplyResources(this.filetoolStripMenuItem, "filetoolStripMenuItem");
            // 
            // quittoolStripMenuItem
            // 
            this.quittoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Log_Out_48;
            this.quittoolStripMenuItem.Name = "quittoolStripMenuItem";
            resources.ApplyResources(this.quittoolStripMenuItem, "quittoolStripMenuItem");
            this.quittoolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // toolstoolStripMenuItem
            // 
            this.toolstoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.certificatetoolStripMenuItem,
            this.languagetoolStripMenuItem,
            this.settingstoolStripMenuItem,
            this.msiReadertoolStripMenuItem});
            this.toolstoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolstoolStripMenuItem.Name = "toolstoolStripMenuItem";
            resources.ApplyResources(this.toolstoolStripMenuItem, "toolstoolStripMenuItem");
            // 
            // certificatetoolStripMenuItem
            // 
            resources.ApplyResources(this.certificatetoolStripMenuItem, "certificatetoolStripMenuItem");
            this.certificatetoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Certificate_64;
            this.certificatetoolStripMenuItem.Name = "certificatetoolStripMenuItem";
            this.certificatetoolStripMenuItem.Click += new System.EventHandler(this.certificatToolStripMenuItem_Click);
            // 
            // languagetoolStripMenuItem
            // 
            this.languagetoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frenchtoolStripMenuItem,
            this.englishtoolStripMenuItem});
            this.languagetoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Drapeau_Bleu;
            this.languagetoolStripMenuItem.Name = "languagetoolStripMenuItem";
            resources.ApplyResources(this.languagetoolStripMenuItem, "languagetoolStripMenuItem");
            // 
            // frenchtoolStripMenuItem
            // 
            this.frenchtoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.drapeau_france;
            this.frenchtoolStripMenuItem.Name = "frenchtoolStripMenuItem";
            resources.ApplyResources(this.frenchtoolStripMenuItem, "frenchtoolStripMenuItem");
            this.frenchtoolStripMenuItem.Click += new System.EventHandler(this.françaisToolStripMenuItem_Click);
            // 
            // englishtoolStripMenuItem
            // 
            this.englishtoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.drapeau_grande_bretagne;
            this.englishtoolStripMenuItem.Name = "englishtoolStripMenuItem";
            resources.ApplyResources(this.englishtoolStripMenuItem, "englishtoolStripMenuItem");
            this.englishtoolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // settingstoolStripMenuItem
            // 
            this.settingstoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Settings_64;
            this.settingstoolStripMenuItem.Name = "settingstoolStripMenuItem";
            resources.ApplyResources(this.settingstoolStripMenuItem, "settingstoolStripMenuItem");
            this.settingstoolStripMenuItem.Click += new System.EventHandler(this.paramètresToolStripMenuItem_Click);
            // 
            // msiReadertoolStripMenuItem
            // 
            this.msiReadertoolStripMenuItem.Name = "msiReadertoolStripMenuItem";
            resources.ApplyResources(this.msiReadertoolStripMenuItem, "msiReadertoolStripMenuItem");
            this.msiReadertoolStripMenuItem.Click += new System.EventHandler(this.mSIPropertyReaderToolStripMenuItem_Click);
            // 
            // updatestoolStripMenuItem
            // 
            this.updatestoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createUpdatetoolStripMenuItem});
            this.updatestoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.updatestoolStripMenuItem.Name = "updatestoolStripMenuItem";
            resources.ApplyResources(this.updatestoolStripMenuItem, "updatestoolStripMenuItem");
            // 
            // createUpdatetoolStripMenuItem
            // 
            resources.ApplyResources(this.createUpdatetoolStripMenuItem, "createUpdatetoolStripMenuItem");
            this.createUpdatetoolStripMenuItem.Name = "createUpdatetoolStripMenuItem";
            this.createUpdatetoolStripMenuItem.Click += new System.EventHandler(this.createUpdatetoolStripMenuItem_Click);
            // 
            // helptoolStripMenuItem
            // 
            this.helptoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abouttoolStripMenuItem});
            this.helptoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helptoolStripMenuItem.Name = "helptoolStripMenuItem";
            resources.ApplyResources(this.helptoolStripMenuItem, "helptoolStripMenuItem");
            // 
            // abouttoolStripMenuItem
            // 
            this.abouttoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Help_48;
            this.abouttoolStripMenuItem.Name = "abouttoolStripMenuItem";
            resources.ApplyResources(this.abouttoolStripMenuItem, "abouttoolStripMenuItem");
            this.abouttoolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // cmbBxServerList
            // 
            this.cmbBxServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxServerList.Name = "cmbBxServerList";
            resources.ApplyResources(this.cmbBxServerList, "cmbBxServerList");
            this.cmbBxServerList.SelectedIndexChanged += new System.EventHandler(this.cmbBxServerList_SelectedIndexChanged);
            // 
            // ctxMnuTreeview
            // 
            this.ctxMnuTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createMetaGroup,
            this.editMetaGroup,
            this.deleteMetaGroup});
            this.ctxMnuTreeview.Name = "ctxMnuTreeview";
            this.ctxMnuTreeview.ShowImageMargin = false;
            resources.ApplyResources(this.ctxMnuTreeview, "ctxMnuTreeview");
            this.ctxMnuTreeview.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuTreeview_ItemClicked);
            // 
            // createMetaGroup
            // 
            this.createMetaGroup.Name = "createMetaGroup";
            resources.ApplyResources(this.createMetaGroup, "createMetaGroup");
            // 
            // editMetaGroup
            // 
            this.editMetaGroup.Name = "editMetaGroup";
            resources.ApplyResources(this.editMetaGroup, "editMetaGroup");
            // 
            // deleteMetaGroup
            // 
            this.deleteMetaGroup.Name = "deleteMetaGroup";
            resources.ApplyResources(this.deleteMetaGroup, "deleteMetaGroup");
            // 
            // FrmWsusPackagePublisher
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmWsusPackagePublisher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWsusPackagePublisher_FormClosing);
            this.Shown += new System.EventHandler(this.FrmWsusPackagePublisher_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ctxMnuTreeview.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvWsus;
        private System.Windows.Forms.Button btnConnectToServer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem filetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quittoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolstoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem certificatetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languagetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingstoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiReadertoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatestoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createUpdatetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helptoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abouttoolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmbBxServerList;
        private System.Windows.Forms.ImageList imgLstServer;
        private System.Windows.Forms.ContextMenuStrip ctxMnuTreeview;
        private System.Windows.Forms.ToolStripMenuItem createMetaGroup;
        private System.Windows.Forms.ToolStripMenuItem editMetaGroup;
        private System.Windows.Forms.ToolStripMenuItem deleteMetaGroup;
    }
}