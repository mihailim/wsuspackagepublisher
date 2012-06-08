namespace Wsus_Package_Publisher
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkBxUseSSL = new System.Windows.Forms.CheckBox();
            this.cmbBxConnectionPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabSettings.SuspendLayout();
            this.tabServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Controls.Add(this.tabServer);
            this.tabSettings.Controls.Add(this.tabPage2);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            // 
            // tabServer
            // 
            resources.ApplyResources(this.tabServer, "tabServer");
            this.tabServer.Controls.Add(this.btnCancel);
            this.tabServer.Controls.Add(this.btnOk);
            this.tabServer.Controls.Add(this.chkBxUseSSL);
            this.tabServer.Controls.Add(this.cmbBxConnectionPort);
            this.tabServer.Controls.Add(this.label2);
            this.tabServer.Controls.Add(this.txtBxServerName);
            this.tabServer.Controls.Add(this.label1);
            this.tabServer.Name = "tabServer";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkBxUseSSL
            // 
            resources.ApplyResources(this.chkBxUseSSL, "chkBxUseSSL");
            this.chkBxUseSSL.Name = "chkBxUseSSL";
            this.chkBxUseSSL.UseVisualStyleBackColor = true;
            // 
            // cmbBxConnectionPort
            // 
            resources.ApplyResources(this.cmbBxConnectionPort, "cmbBxConnectionPort");
            this.cmbBxConnectionPort.FormattingEnabled = true;
            this.cmbBxConnectionPort.Items.AddRange(new object[] {
            resources.GetString("cmbBxConnectionPort.Items"),
            resources.GetString("cmbBxConnectionPort.Items1")});
            this.cmbBxConnectionPort.Name = "cmbBxConnectionPort";
            this.cmbBxConnectionPort.SelectedIndexChanged += new System.EventHandler(this.cmbBxConnectionPort_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxServerName
            // 
            resources.ApplyResources(this.txtBxServerName, "txtBxServerName");
            this.txtBxServerName.Name = "txtBxServerName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmSettings";
            this.tabSettings.ResumeLayout(false);
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabServer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkBxUseSSL;
        private System.Windows.Forms.ComboBox cmbBxConnectionPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}