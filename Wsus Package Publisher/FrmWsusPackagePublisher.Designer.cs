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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.FichierStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certificatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langagueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.françaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.misesÀJourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.créerUneMiseÀJourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
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
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            // 
            // splitContainer2.Panel2
            // 
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
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
            this.toolStripComboBox1});
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
            // 
            // outilsToolStripMenuItem
            // 
            resources.ApplyResources(this.outilsToolStripMenuItem, "outilsToolStripMenuItem");
            this.outilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.certificatToolStripMenuItem,
            this.paramètresToolStripMenuItem,
            this.langagueToolStripMenuItem});
            this.outilsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.outilsToolStripMenuItem.Name = "outilsToolStripMenuItem";
            // 
            // certificatToolStripMenuItem
            // 
            resources.ApplyResources(this.certificatToolStripMenuItem, "certificatToolStripMenuItem");
            this.certificatToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Certificate_64;
            this.certificatToolStripMenuItem.Name = "certificatToolStripMenuItem";
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
            this.créerUneMiseÀJourToolStripMenuItem});
            this.misesÀJourToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.misesÀJourToolStripMenuItem.Name = "misesÀJourToolStripMenuItem";
            // 
            // créerUneMiseÀJourToolStripMenuItem
            // 
            resources.ApplyResources(this.créerUneMiseÀJourToolStripMenuItem, "créerUneMiseÀJourToolStripMenuItem");
            this.créerUneMiseÀJourToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Add_Files_To_Archive_Blue_64;
            this.créerUneMiseÀJourToolStripMenuItem.Name = "créerUneMiseÀJourToolStripMenuItem";
            this.créerUneMiseÀJourToolStripMenuItem.Click += new System.EventHandler(this.createUpdateToolStripMenuItem_Click);
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
            // toolStripComboBox1
            // 
            resources.ApplyResources(this.toolStripComboBox1, "toolStripComboBox1");
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            // 
            // FrmWsusPackagePublisher
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmWsusPackagePublisher";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem certificatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem créerUneMiseÀJourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem langagueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem françaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        internal System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}