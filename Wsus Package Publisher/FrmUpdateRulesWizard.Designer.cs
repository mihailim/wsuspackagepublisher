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
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnAndGrouping = new System.Windows.Forms.Button();
            this.btnORGrouping = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.rulesViewer1 = new Wsus_Package_Publisher.RulesViewer();
            this.SuspendLayout();
            // 
            // cmbBxRules
            // 
            resources.ApplyResources(this.cmbBxRules, "cmbBxRules");
            this.cmbBxRules.FormattingEnabled = true;
            this.cmbBxRules.Items.AddRange(new object[] {
            resources.GetString("cmbBxRules.Items"),
            resources.GetString("cmbBxRules.Items1"),
            resources.GetString("cmbBxRules.Items2"),
            resources.GetString("cmbBxRules.Items3"),
            resources.GetString("cmbBxRules.Items4")});
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
            // btnAndGrouping
            // 
            resources.ApplyResources(this.btnAndGrouping, "btnAndGrouping");
            this.btnAndGrouping.Name = "btnAndGrouping";
            this.btnAndGrouping.UseVisualStyleBackColor = true;
            // 
            // btnORGrouping
            // 
            resources.ApplyResources(this.btnORGrouping, "btnORGrouping");
            this.btnORGrouping.Name = "btnORGrouping";
            this.btnORGrouping.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // rulesViewer1
            // 
            resources.ApplyResources(this.rulesViewer1, "rulesViewer1");
            this.rulesViewer1.Name = "rulesViewer1";
            // 
            // FrmUpdateRulesWizard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rulesViewer1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.btnORGrouping);
            this.Controls.Add(this.btnAndGrouping);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnAddRule);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxRules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdateRulesWizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBxRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnAndGrouping;
        private System.Windows.Forms.Button btnORGrouping;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private RulesViewer rulesViewer1;
    }
}