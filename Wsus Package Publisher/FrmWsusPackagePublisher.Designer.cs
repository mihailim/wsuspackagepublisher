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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWsusPackagePublisher));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnConnectToServer = new System.Windows.Forms.Button();
            this.trvWsus = new System.Windows.Forms.TreeView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.FichierStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langagueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.françaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.misesÀJourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbBxServerList = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip.SuspendLayout();
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
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.btnConnectToServer);
            this.splitContainer1.Panel1.Controls.Add(this.trvWsus);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
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
            resources.ApplyResources(this.trvWsus, "trvWsus");
            this.trvWsus.Name = "trvWsus";
            this.trvWsus.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvWsus_AfterSelect);
            // 
            // menuStrip
            // 
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FichierStripMenuItem,
            this.outilsToolStripMenuItem,
            this.misesÀJourToolStripMenuItem,
            this.aideToolStripMenuItem,
            this.cmbBxServerList});
            this.menuStrip.Name = "menuStrip";
            // 
            // FichierStripMenuItem
            // 
            resources.ApplyResources(this.FichierStripMenuItem, "FichierStripMenuItem");
            this.FichierStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitterToolStripMenuItem});
            this.FichierStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.FichierStripMenuItem.Name = "FichierStripMenuItem";
            // 
            // quitterToolStripMenuItem
            // 
            resources.ApplyResources(this.quitterToolStripMenuItem, "quitterToolStripMenuItem");
            this.quitterToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Log_Out_48;
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // outilsToolStripMenuItem
            // 
            resources.ApplyResources(this.outilsToolStripMenuItem, "outilsToolStripMenuItem");
            this.outilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.certificateToolStripMenuItem,
            this.paramètresToolStripMenuItem,
            this.langagueToolStripMenuItem});
            this.outilsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.outilsToolStripMenuItem.Name = "outilsToolStripMenuItem";
            // 
            // certificateToolStripMenuItem
            // 
            resources.ApplyResources(this.certificateToolStripMenuItem, "certificateToolStripMenuItem");
            this.certificateToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Certificate_64;
            this.certificateToolStripMenuItem.Name = "certificateToolStripMenuItem";
            this.certificateToolStripMenuItem.Click += new System.EventHandler(this.certificatToolStripMenuItem_Click);
            // 
            // paramètresToolStripMenuItem
            // 
            resources.ApplyResources(this.paramètresToolStripMenuItem, "paramètresToolStripMenuItem");
            this.paramètresToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Settings_64;
            this.paramètresToolStripMenuItem.Name = "paramètresToolStripMenuItem";
            this.paramètresToolStripMenuItem.Click += new System.EventHandler(this.paramètresToolStripMenuItem_Click);
            // 
            // langagueToolStripMenuItem
            // 
            resources.ApplyResources(this.langagueToolStripMenuItem, "langagueToolStripMenuItem");
            this.langagueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.françaisToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.langagueToolStripMenuItem.Name = "langagueToolStripMenuItem";
            // 
            // françaisToolStripMenuItem
            // 
            resources.ApplyResources(this.françaisToolStripMenuItem, "françaisToolStripMenuItem");
            this.françaisToolStripMenuItem.Name = "françaisToolStripMenuItem";
            this.françaisToolStripMenuItem.Click += new System.EventHandler(this.françaisToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Checked = true;
            this.englishToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // misesÀJourToolStripMenuItem
            // 
            resources.ApplyResources(this.misesÀJourToolStripMenuItem, "misesÀJourToolStripMenuItem");
            this.misesÀJourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createUpdateToolStripMenuItem});
            this.misesÀJourToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.misesÀJourToolStripMenuItem.Name = "misesÀJourToolStripMenuItem";
            // 
            // createUpdateToolStripMenuItem
            // 
            resources.ApplyResources(this.createUpdateToolStripMenuItem, "createUpdateToolStripMenuItem");
            this.createUpdateToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Add_Files_To_Archive_Blue_64;
            this.createUpdateToolStripMenuItem.Name = "createUpdateToolStripMenuItem";
            this.createUpdateToolStripMenuItem.Click += new System.EventHandler(this.createUpdateToolStripMenuItem_Click);
            // 
            // aideToolStripMenuItem
            // 
            resources.ApplyResources(this.aideToolStripMenuItem, "aideToolStripMenuItem");
            this.aideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
            this.aideToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aideToolStripMenuItem.Name = "aideToolStripMenuItem";
            // 
            // aProposToolStripMenuItem
            // 
            resources.ApplyResources(this.aProposToolStripMenuItem, "aProposToolStripMenuItem");
            this.aProposToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Help_48;
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            this.aProposToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // cmbBxServerList
            // 
            resources.ApplyResources(this.cmbBxServerList, "cmbBxServerList");
            this.cmbBxServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxServerList.Name = "cmbBxServerList";
            this.cmbBxServerList.SelectedIndexChanged += new System.EventHandler(this.cmbBxServerList_SelectedIndexChanged);
            // 
            // FrmWsusPackagePublisher
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmWsusPackagePublisher";
            this.Shown += new System.EventHandler(this.FrmWsusPackagePublisher_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem FichierStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem misesÀJourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.TreeView trvWsus;
        private System.Windows.Forms.Button btnConnectToServer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem certificateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem langagueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem françaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmbBxServerList;
    }
}