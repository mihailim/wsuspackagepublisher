namespace Wsus_Package_Publisher
{
    partial class FrmUpdateRulesWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateRulesWizard));
            this.cmbBxRules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnAddAndGroup = new System.Windows.Forms.Button();
            this.btnAddOrGroup = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpDsp1 = new Wsus_Package_Publisher.GroupDisplayer();
            this.chkBxEmptyInstallableItemRule = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbBxRules
            // 
            resources.ApplyResources(this.cmbBxRules, "cmbBxRules");
            this.tableLayoutPanel1.SetColumnSpan(this.cmbBxRules, 2);
            this.cmbBxRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxRules.Name = "cmbBxRules";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnAddRule
            // 
            resources.ApplyResources(this.btnAddRule, "btnAddRule");
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnAddAndGroup
            // 
            resources.ApplyResources(this.btnAddAndGroup, "btnAddAndGroup");
            this.btnAddAndGroup.Name = "btnAddAndGroup";
            this.btnAddAndGroup.UseVisualStyleBackColor = true;
            this.btnAddAndGroup.Click += new System.EventHandler(this.btnAddAndGroup_Click);
            // 
            // btnAddOrGroup
            // 
            resources.ApplyResources(this.btnAddOrGroup, "btnAddOrGroup");
            this.btnAddOrGroup.Name = "btnAddOrGroup";
            this.btnAddOrGroup.UseVisualStyleBackColor = true;
            this.btnAddOrGroup.Click += new System.EventHandler(this.btnAddOrGroup_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnAddRule, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpDsp1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxRules, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddAndGroup, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddOrGroup, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkBxEmptyInstallableItemRule, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnPrevious, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnNext, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // grpDsp1
            // 
            resources.ApplyResources(this.grpDsp1, "grpDsp1");
            this.tableLayoutPanel1.SetColumnSpan(this.grpDsp1, 4);
            this.grpDsp1.Name = "grpDsp1";
            // 
            // chkBxEmptyInstallableItemRule
            // 
            resources.ApplyResources(this.chkBxEmptyInstallableItemRule, "chkBxEmptyInstallableItemRule");
            this.tableLayoutPanel1.SetColumnSpan(this.chkBxEmptyInstallableItemRule, 2);
            this.chkBxEmptyInstallableItemRule.Name = "chkBxEmptyInstallableItemRule";
            this.chkBxEmptyInstallableItemRule.UseVisualStyleBackColor = true;
            // 
            // FrmUpdateRulesWizard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdateRulesWizard";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBxRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnAddAndGroup;
        private System.Windows.Forms.Button btnAddOrGroup;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrevious;
        private GroupDisplayer grpDsp1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkBxEmptyInstallableItemRule;
    }
}