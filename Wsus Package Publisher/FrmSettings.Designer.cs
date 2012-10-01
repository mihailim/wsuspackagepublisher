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
            this.chkBxConnectToLocalServer = new System.Windows.Forms.CheckBox();
            this.btnEditServer = new System.Windows.Forms.Button();
            this.nupDeadLineMinute = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nupDeadLineHour = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nupDeadLineDaysSpan = new System.Windows.Forms.NumericUpDown();
            this.btnRemoveServer = new System.Windows.Forms.Button();
            this.cmbBxServerList = new System.Windows.Forms.ComboBox();
            this.btnAddServer = new System.Windows.Forms.Button();
            this.chkBxUseSSL = new System.Windows.Forms.CheckBox();
            this.cmbBxConnectionPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabCommonSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBxPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBxLogin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rdBtnAsk = new System.Windows.Forms.RadioButton();
            this.rdBtnSpecified = new System.Windows.Forms.RadioButton();
            this.rdBtnSameAsApplication = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabSettings.SuspendLayout();
            this.tabServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineDaysSpan)).BeginInit();
            this.tabCommonSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabServer);
            this.tabSettings.Controls.Add(this.tabCommonSettings);
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.chkBxConnectToLocalServer);
            this.tabServer.Controls.Add(this.btnEditServer);
            this.tabServer.Controls.Add(this.nupDeadLineMinute);
            this.tabServer.Controls.Add(this.label5);
            this.tabServer.Controls.Add(this.nupDeadLineHour);
            this.tabServer.Controls.Add(this.label4);
            this.tabServer.Controls.Add(this.label3);
            this.tabServer.Controls.Add(this.nupDeadLineDaysSpan);
            this.tabServer.Controls.Add(this.btnRemoveServer);
            this.tabServer.Controls.Add(this.cmbBxServerList);
            this.tabServer.Controls.Add(this.btnAddServer);
            this.tabServer.Controls.Add(this.chkBxUseSSL);
            this.tabServer.Controls.Add(this.cmbBxConnectionPort);
            this.tabServer.Controls.Add(this.label2);
            this.tabServer.Controls.Add(this.txtBxServerName);
            this.tabServer.Controls.Add(this.label1);
            resources.ApplyResources(this.tabServer, "tabServer");
            this.tabServer.Name = "tabServer";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // chkBxConnectToLocalServer
            // 
            resources.ApplyResources(this.chkBxConnectToLocalServer, "chkBxConnectToLocalServer");
            this.chkBxConnectToLocalServer.Name = "chkBxConnectToLocalServer";
            this.chkBxConnectToLocalServer.UseVisualStyleBackColor = true;
            // 
            // btnEditServer
            // 
            resources.ApplyResources(this.btnEditServer, "btnEditServer");
            this.btnEditServer.Name = "btnEditServer";
            this.btnEditServer.UseVisualStyleBackColor = true;
            this.btnEditServer.Click += new System.EventHandler(this.btnEditServer_Click);
            // 
            // nupDeadLineMinute
            // 
            resources.ApplyResources(this.nupDeadLineMinute, "nupDeadLineMinute");
            this.nupDeadLineMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nupDeadLineMinute.Name = "nupDeadLineMinute";
            this.nupDeadLineMinute.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // nupDeadLineHour
            // 
            resources.ApplyResources(this.nupDeadLineHour, "nupDeadLineHour");
            this.nupDeadLineHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nupDeadLineHour.Name = "nupDeadLineHour";
            this.nupDeadLineHour.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // nupDeadLineDaysSpan
            // 
            resources.ApplyResources(this.nupDeadLineDaysSpan, "nupDeadLineDaysSpan");
            this.nupDeadLineDaysSpan.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nupDeadLineDaysSpan.Name = "nupDeadLineDaysSpan";
            this.nupDeadLineDaysSpan.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // btnRemoveServer
            // 
            resources.ApplyResources(this.btnRemoveServer, "btnRemoveServer");
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.UseVisualStyleBackColor = true;
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // cmbBxServerList
            // 
            resources.ApplyResources(this.cmbBxServerList, "cmbBxServerList");
            this.cmbBxServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxServerList.FormattingEnabled = true;
            this.cmbBxServerList.Name = "cmbBxServerList";
            this.cmbBxServerList.SelectedIndexChanged += new System.EventHandler(this.cmbBxServerList_SelectedIndexChanged);
            // 
            // btnAddServer
            // 
            resources.ApplyResources(this.btnAddServer, "btnAddServer");
            this.btnAddServer.Name = "btnAddServer";
            this.btnAddServer.UseVisualStyleBackColor = true;
            this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
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
            this.cmbBxConnectionPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxConnectionPort.FormattingEnabled = true;
            this.cmbBxConnectionPort.Items.AddRange(new object[] {
            resources.GetString("cmbBxConnectionPort.Items"),
            resources.GetString("cmbBxConnectionPort.Items1"),
            resources.GetString("cmbBxConnectionPort.Items2"),
            resources.GetString("cmbBxConnectionPort.Items3")});
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
            this.txtBxServerName.TextChanged += new System.EventHandler(this.txtBxServerName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabCommonSettings
            // 
            this.tabCommonSettings.Controls.Add(this.groupBox1);
            this.tabCommonSettings.Controls.Add(this.label6);
            resources.ApplyResources(this.tabCommonSettings, "tabCommonSettings");
            this.tabCommonSettings.Name = "tabCommonSettings";
            this.tabCommonSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtBxPassword);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBxLogin);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.rdBtnAsk);
            this.groupBox1.Controls.Add(this.rdBtnSpecified);
            this.groupBox1.Controls.Add(this.rdBtnSameAsApplication);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtBxPassword
            // 
            resources.ApplyResources(this.txtBxPassword, "txtBxPassword");
            this.txtBxPassword.Name = "txtBxPassword";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtBxLogin
            // 
            resources.ApplyResources(this.txtBxLogin, "txtBxLogin");
            this.txtBxLogin.Name = "txtBxLogin";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // rdBtnAsk
            // 
            resources.ApplyResources(this.rdBtnAsk, "rdBtnAsk");
            this.rdBtnAsk.Name = "rdBtnAsk";
            this.rdBtnAsk.UseVisualStyleBackColor = true;
            this.rdBtnAsk.CheckedChanged += new System.EventHandler(this.rdBtnSameThanApplication_CheckedChanged);
            // 
            // rdBtnSpecified
            // 
            resources.ApplyResources(this.rdBtnSpecified, "rdBtnSpecified");
            this.rdBtnSpecified.Name = "rdBtnSpecified";
            this.rdBtnSpecified.UseVisualStyleBackColor = true;
            this.rdBtnSpecified.CheckedChanged += new System.EventHandler(this.rdBtnSameThanApplication_CheckedChanged);
            // 
            // rdBtnSameAsApplication
            // 
            resources.ApplyResources(this.rdBtnSameAsApplication, "rdBtnSameAsApplication");
            this.rdBtnSameAsApplication.Checked = true;
            this.rdBtnSameAsApplication.Name = "rdBtnSameAsApplication";
            this.rdBtnSameAsApplication.TabStop = true;
            this.rdBtnSameAsApplication.UseVisualStyleBackColor = true;
            this.rdBtnSameAsApplication.CheckedChanged += new System.EventHandler(this.rdBtnSameThanApplication_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            // FrmSettings
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.Shown += new System.EventHandler(this.FrmSettings_Shown);
            this.tabSettings.ResumeLayout(false);
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineDaysSpan)).EndInit();
            this.tabCommonSettings.ResumeLayout(false);
            this.tabCommonSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TabPage tabCommonSettings;
        private System.Windows.Forms.Button btnRemoveServer;
        private System.Windows.Forms.ComboBox cmbBxServerList;
        private System.Windows.Forms.Button btnAddServer;
        private System.Windows.Forms.NumericUpDown nupDeadLineMinute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nupDeadLineHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nupDeadLineDaysSpan;
        private System.Windows.Forms.Button btnEditServer;
        private System.Windows.Forms.CheckBox chkBxConnectToLocalServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBxPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBxLogin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rdBtnAsk;
        private System.Windows.Forms.RadioButton rdBtnSpecified;
        private System.Windows.Forms.RadioButton rdBtnSameAsApplication;
        private System.Windows.Forms.Label label6;
    }
}