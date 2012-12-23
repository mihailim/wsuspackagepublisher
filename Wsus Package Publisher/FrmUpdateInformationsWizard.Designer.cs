namespace Wsus_Package_Publisher
{
    partial class FrmUpdateInformationsWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateInformationsWizard));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBxVendorName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBxProductName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxTitle = new System.Windows.Forms.TextBox();
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBxMoreInfoURL = new System.Windows.Forms.TextBox();
            this.txtBxSupportURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBxSecurityBulletinId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbBxMsrcSeverity = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBxCVEId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBxKBArticleId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbBxUpdateClassification = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkBxCanREquestUserInput = new System.Windows.Forms.CheckBox();
            this.chkBxRequiresNetworkConnectivity = new System.Windows.Forms.CheckBox();
            this.cmbBxImpact = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbBxRebootBehavior = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBxCommandLine = new System.Windows.Forms.TextBox();
            this.dtgrvReturnCodes = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.NeedReboot = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.chkCmbBxSupersedes = new EasyCompany.Controls.CheckComboBox();
            this.chkCmbBxPrerequisites = new EasyCompany.Controls.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvReturnCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbBxVendorName
            // 
            this.cmbBxVendorName.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxVendorName, "cmbBxVendorName");
            this.cmbBxVendorName.Name = "cmbBxVendorName";
            this.cmbBxVendorName.Sorted = true;
            this.cmbBxVendorName.SelectedIndexChanged += new System.EventHandler(this.cmbBxVendorName_SelectedIndexChanged);
            this.cmbBxVendorName.TextChanged += new System.EventHandler(this.cmbBxVendorName_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmbBxProductName
            // 
            this.cmbBxProductName.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxProductName, "cmbBxProductName");
            this.cmbBxProductName.Name = "cmbBxProductName";
            this.cmbBxProductName.SelectedIndexChanged += new System.EventHandler(this.cmbBxProductName_SelectedIndexChanged);
            this.cmbBxProductName.TextChanged += new System.EventHandler(this.cmbBxProductName_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxTitle
            // 
            resources.ApplyResources(this.txtBxTitle, "txtBxTitle");
            this.txtBxTitle.Name = "txtBxTitle";
            this.txtBxTitle.TextChanged += new System.EventHandler(this.txtBxTitle_TextChanged);
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.TextChanged += new System.EventHandler(this.txtBxDescription_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtBxMoreInfoURL
            // 
            resources.ApplyResources(this.txtBxMoreInfoURL, "txtBxMoreInfoURL");
            this.txtBxMoreInfoURL.Name = "txtBxMoreInfoURL";
            // 
            // txtBxSupportURL
            // 
            resources.ApplyResources(this.txtBxSupportURL, "txtBxSupportURL");
            this.txtBxSupportURL.Name = "txtBxSupportURL";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtBxSecurityBulletinId
            // 
            resources.ApplyResources(this.txtBxSecurityBulletinId, "txtBxSecurityBulletinId");
            this.txtBxSecurityBulletinId.Name = "txtBxSecurityBulletinId";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbBxMsrcSeverity
            // 
            this.cmbBxMsrcSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxMsrcSeverity.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxMsrcSeverity, "cmbBxMsrcSeverity");
            this.cmbBxMsrcSeverity.Name = "cmbBxMsrcSeverity";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtBxCVEId
            // 
            resources.ApplyResources(this.txtBxCVEId, "txtBxCVEId");
            this.txtBxCVEId.Name = "txtBxCVEId";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtBxKBArticleId
            // 
            resources.ApplyResources(this.txtBxKBArticleId, "txtBxKBArticleId");
            this.txtBxKBArticleId.Name = "txtBxKBArticleId";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // cmbBxUpdateClassification
            // 
            this.cmbBxUpdateClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxUpdateClassification.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxUpdateClassification, "cmbBxUpdateClassification");
            this.cmbBxUpdateClassification.Name = "cmbBxUpdateClassification";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // chkBxCanREquestUserInput
            // 
            resources.ApplyResources(this.chkBxCanREquestUserInput, "chkBxCanREquestUserInput");
            this.chkBxCanREquestUserInput.Name = "chkBxCanREquestUserInput";
            this.chkBxCanREquestUserInput.UseVisualStyleBackColor = true;
            // 
            // chkBxRequiresNetworkConnectivity
            // 
            resources.ApplyResources(this.chkBxRequiresNetworkConnectivity, "chkBxRequiresNetworkConnectivity");
            this.chkBxRequiresNetworkConnectivity.Name = "chkBxRequiresNetworkConnectivity";
            this.chkBxRequiresNetworkConnectivity.UseVisualStyleBackColor = true;
            // 
            // cmbBxImpact
            // 
            this.cmbBxImpact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxImpact.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxImpact, "cmbBxImpact");
            this.cmbBxImpact.Name = "cmbBxImpact";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // cmbBxRebootBehavior
            // 
            this.cmbBxRebootBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxRebootBehavior.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxRebootBehavior, "cmbBxRebootBehavior");
            this.cmbBxRebootBehavior.Name = "cmbBxRebootBehavior";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // txtBxCommandLine
            // 
            resources.ApplyResources(this.txtBxCommandLine, "txtBxCommandLine");
            this.txtBxCommandLine.Name = "txtBxCommandLine";
            this.txtBxCommandLine.TextChanged += new System.EventHandler(this.txtBxCommandLine_TextChanged);
            // 
            // dtgrvReturnCodes
            // 
            this.dtgrvReturnCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrvReturnCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value,
            this.Result,
            this.NeedReboot});
            resources.ApplyResources(this.dtgrvReturnCodes, "dtgrvReturnCodes");
            this.dtgrvReturnCodes.Name = "dtgrvReturnCodes";
            // 
            // Value
            // 
            this.Value.FillWeight = 20F;
            resources.ApplyResources(this.Value, "Value");
            this.Value.Name = "Value";
            // 
            // Result
            // 
            this.Result.FillWeight = 40F;
            resources.ApplyResources(this.Result, "Result");
            this.Result.Name = "Result";
            // 
            // NeedReboot
            // 
            this.NeedReboot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.NeedReboot, "NeedReboot");
            this.NeedReboot.Name = "NeedReboot";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // chkCmbBxSupersedes
            // 
            this.chkCmbBxSupersedes.BackColor = System.Drawing.Color.AliceBlue;
            this.chkCmbBxSupersedes.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.chkCmbBxSupersedes, "chkCmbBxSupersedes");
            this.chkCmbBxSupersedes.MinimumSize = new System.Drawing.Size(85, 21);
            this.chkCmbBxSupersedes.Name = "chkCmbBxSupersedes";
            // 
            // chkCmbBxPrerequisites
            // 
            this.chkCmbBxPrerequisites.BackColor = System.Drawing.Color.AliceBlue;
            this.chkCmbBxPrerequisites.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.chkCmbBxPrerequisites, "chkCmbBxPrerequisites");
            this.chkCmbBxPrerequisites.MinimumSize = new System.Drawing.Size(85, 21);
            this.chkCmbBxPrerequisites.Name = "chkCmbBxPrerequisites";
            // 
            // FrmUpdateInformationsWizard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkCmbBxPrerequisites);
            this.Controls.Add(this.chkCmbBxSupersedes);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dtgrvReturnCodes);
            this.Controls.Add(this.txtBxCommandLine);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.chkBxRequiresNetworkConnectivity);
            this.Controls.Add(this.chkBxCanREquestUserInput);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtBxKBArticleId);
            this.Controls.Add(this.txtBxCVEId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbBxUpdateClassification);
            this.Controls.Add(this.cmbBxRebootBehavior);
            this.Controls.Add(this.cmbBxImpact);
            this.Controls.Add(this.cmbBxMsrcSeverity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBxSecurityBulletinId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBxSupportURL);
            this.Controls.Add(this.txtBxMoreInfoURL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBxDescription);
            this.Controls.Add(this.txtBxTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbBxProductName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBxVendorName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdateInformationsWizard";
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvReturnCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBxVendorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBxProductName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxTitle;
        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBxMoreInfoURL;
        private System.Windows.Forms.TextBox txtBxSupportURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBxSecurityBulletinId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBxMsrcSeverity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBxCVEId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBxKBArticleId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbBxUpdateClassification;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkBxCanREquestUserInput;
        private System.Windows.Forms.CheckBox chkBxRequiresNetworkConnectivity;
        private System.Windows.Forms.ComboBox cmbBxImpact;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbBxRebootBehavior;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtBxCommandLine;
        private System.Windows.Forms.DataGridView dtgrvReturnCodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewComboBoxColumn Result;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NeedReboot;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private EasyCompany.Controls.CheckComboBox chkCmbBxSupersedes;
        private EasyCompany.Controls.CheckComboBox chkCmbBxPrerequisites;
    }
}