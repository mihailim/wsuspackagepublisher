﻿namespace Wsus_Package_Publisher
{
    partial class RuleRegSzToVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleRegSzToVersion));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxSubKey = new System.Windows.Forms.TextBox();
            this.chkBxReverseRule = new System.Windows.Forms.CheckBox();
            this.chkBxRegType32 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBxValue = new System.Windows.Forms.TextBox();
            this.cmbBxComparison = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nupVersion1 = new System.Windows.Forms.NumericUpDown();
            this.nupVersion2 = new System.Windows.Forms.NumericUpDown();
            this.nupVersion3 = new System.Windows.Forms.NumericUpDown();
            this.nupVersion4 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion4)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxSubKey
            // 
            resources.ApplyResources(this.txtBxSubKey, "txtBxSubKey");
            this.txtBxSubKey.Name = "txtBxSubKey";
            this.txtBxSubKey.TextChanged += new System.EventHandler(this.txtBxSubKey_TextChanged);
            // 
            // chkBxReverseRule
            // 
            resources.ApplyResources(this.chkBxReverseRule, "chkBxReverseRule");
            this.chkBxReverseRule.Name = "chkBxReverseRule";
            this.chkBxReverseRule.UseVisualStyleBackColor = true;
            // 
            // chkBxRegType32
            // 
            resources.ApplyResources(this.chkBxRegType32, "chkBxRegType32");
            this.chkBxRegType32.Name = "chkBxRegType32";
            this.chkBxRegType32.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtBxValue
            // 
            resources.ApplyResources(this.txtBxValue, "txtBxValue");
            this.txtBxValue.Name = "txtBxValue";
            this.txtBxValue.TextChanged += new System.EventHandler(this.txtBxValue_TextChanged);
            // 
            // cmbBxComparison
            // 
            resources.ApplyResources(this.cmbBxComparison, "cmbBxComparison");
            this.cmbBxComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxComparison.FormattingEnabled = true;
            this.cmbBxComparison.Name = "cmbBxComparison";
            this.cmbBxComparison.SelectedIndexChanged += new System.EventHandler(this.cmbBxComparison_SelectedIndexChanged);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // nupVersion1
            // 
            resources.ApplyResources(this.nupVersion1, "nupVersion1");
            this.nupVersion1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion1.Name = "nupVersion1";
            this.nupVersion1.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // nupVersion2
            // 
            resources.ApplyResources(this.nupVersion2, "nupVersion2");
            this.nupVersion2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion2.Name = "nupVersion2";
            this.nupVersion2.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // nupVersion3
            // 
            resources.ApplyResources(this.nupVersion3, "nupVersion3");
            this.nupVersion3.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion3.Name = "nupVersion3";
            this.nupVersion3.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // nupVersion4
            // 
            resources.ApplyResources(this.nupVersion4, "nupVersion4");
            this.nupVersion4.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion4.Name = "nupVersion4";
            this.nupVersion4.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // RuleRegSzToVersion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nupVersion4);
            this.Controls.Add(this.nupVersion3);
            this.Controls.Add(this.nupVersion2);
            this.Controls.Add(this.nupVersion1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbBxComparison);
            this.Controls.Add(this.txtBxValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBxRegType32);
            this.Controls.Add(this.chkBxReverseRule);
            this.Controls.Add(this.txtBxSubKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxDescription);
            this.Name = "RuleRegSzToVersion";
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxSubKey;
        private System.Windows.Forms.CheckBox chkBxReverseRule;
        private System.Windows.Forms.CheckBox chkBxRegType32;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBxValue;
        private System.Windows.Forms.ComboBox cmbBxComparison;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nupVersion1;
        private System.Windows.Forms.NumericUpDown nupVersion2;
        private System.Windows.Forms.NumericUpDown nupVersion3;
        private System.Windows.Forms.NumericUpDown nupVersion4;
    }
}
