namespace Wsus_Package_Publisher
{
    partial class FrmUpdateFilesWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateFilesWizard));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxUpdateFile = new System.Windows.Forms.TextBox();
            this.btnBrowseUpdateFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddAdditonnalFiles = new System.Windows.Forms.Button();
            this.btnRemoveAdditionnalFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxCommandLine = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstBxAdditionnalFiles = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtgrvReturnCodes = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.NeedReboot = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvReturnCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxUpdateFile
            // 
            resources.ApplyResources(this.txtBxUpdateFile, "txtBxUpdateFile");
            this.txtBxUpdateFile.Name = "txtBxUpdateFile";
            this.txtBxUpdateFile.TextChanged += new System.EventHandler(this.txtBxUpdateFile_TextChanged);
            // 
            // btnBrowseUpdateFile
            // 
            resources.ApplyResources(this.btnBrowseUpdateFile, "btnBrowseUpdateFile");
            this.btnBrowseUpdateFile.Name = "btnBrowseUpdateFile";
            this.btnBrowseUpdateFile.UseVisualStyleBackColor = true;
            this.btnBrowseUpdateFile.Click += new System.EventHandler(this.btnBrowseUpdateFile_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnAddAdditonnalFiles
            // 
            resources.ApplyResources(this.btnAddAdditonnalFiles, "btnAddAdditonnalFiles");
            this.btnAddAdditonnalFiles.Name = "btnAddAdditonnalFiles";
            this.btnAddAdditonnalFiles.UseVisualStyleBackColor = true;
            this.btnAddAdditonnalFiles.Click += new System.EventHandler(this.btnAddAdditonnalFiles_Click);
            // 
            // btnRemoveAdditionnalFile
            // 
            resources.ApplyResources(this.btnRemoveAdditionnalFile, "btnRemoveAdditionnalFile");
            this.btnRemoveAdditionnalFile.Name = "btnRemoveAdditionnalFile";
            this.btnRemoveAdditionnalFile.UseVisualStyleBackColor = true;
            this.btnRemoveAdditionnalFile.Click += new System.EventHandler(this.btnRemoveAdditionnalFile_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxCommandLine
            // 
            resources.ApplyResources(this.txtBxCommandLine, "txtBxCommandLine");
            this.txtBxCommandLine.Name = "txtBxCommandLine";
            this.txtBxCommandLine.TextChanged += new System.EventHandler(this.txtBxCommandLine_TextChanged);
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lstBxAdditionnalFiles
            // 
            resources.ApplyResources(this.lstBxAdditionnalFiles, "lstBxAdditionnalFiles");
            this.lstBxAdditionnalFiles.FormattingEnabled = true;
            this.lstBxAdditionnalFiles.Name = "lstBxAdditionnalFiles";
            this.lstBxAdditionnalFiles.SelectedIndexChanged += new System.EventHandler(this.lstBxAdditionnalFiles_SelectedIndexChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // dtgrvReturnCodes
            // 
            resources.ApplyResources(this.dtgrvReturnCodes, "dtgrvReturnCodes");
            this.dtgrvReturnCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrvReturnCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value,
            this.Result,
            this.NeedReboot});
            this.dtgrvReturnCodes.Name = "dtgrvReturnCodes";
            // 
            // Value
            // 
            resources.ApplyResources(this.Value, "Value");
            this.Value.Name = "Value";
            // 
            // Result
            // 
            resources.ApplyResources(this.Result, "Result");
            this.Result.Name = "Result";
            this.Result.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // NeedReboot
            // 
            this.NeedReboot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.NeedReboot, "NeedReboot");
            this.NeedReboot.Name = "NeedReboot";
            // 
            // FrmUpdateFilesWizard
            // 
            this.AcceptButton = this.btnNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.dtgrvReturnCodes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lstBxAdditionnalFiles);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBxCommandLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemoveAdditionnalFile);
            this.Controls.Add(this.btnAddAdditonnalFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseUpdateFile);
            this.Controls.Add(this.txtBxUpdateFile);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdateFilesWizard";
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvReturnCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxUpdateFile;
        private System.Windows.Forms.Button btnBrowseUpdateFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddAdditonnalFiles;
        private System.Windows.Forms.Button btnRemoveAdditionnalFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxCommandLine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lstBxAdditionnalFiles;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dtgrvReturnCodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewComboBoxColumn Result;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NeedReboot;
    }
}