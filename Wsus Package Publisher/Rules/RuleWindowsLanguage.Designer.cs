namespace Wsus_Package_Publisher
{
    partial class RuleWindowsLanguage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleWindowsLanguage));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBxLanguage = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            this.txtBxDescription.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbBxLanguage
            // 
            resources.ApplyResources(this.cmbBxLanguage, "cmbBxLanguage");
            this.cmbBxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxLanguage.FormattingEnabled = true;
            this.cmbBxLanguage.Items.AddRange(new object[] {
            resources.GetString("cmbBxLanguage.Items"),
            resources.GetString("cmbBxLanguage.Items1"),
            resources.GetString("cmbBxLanguage.Items2"),
            resources.GetString("cmbBxLanguage.Items3"),
            resources.GetString("cmbBxLanguage.Items4"),
            resources.GetString("cmbBxLanguage.Items5"),
            resources.GetString("cmbBxLanguage.Items6"),
            resources.GetString("cmbBxLanguage.Items7"),
            resources.GetString("cmbBxLanguage.Items8"),
            resources.GetString("cmbBxLanguage.Items9"),
            resources.GetString("cmbBxLanguage.Items10"),
            resources.GetString("cmbBxLanguage.Items11"),
            resources.GetString("cmbBxLanguage.Items12"),
            resources.GetString("cmbBxLanguage.Items13"),
            resources.GetString("cmbBxLanguage.Items14"),
            resources.GetString("cmbBxLanguage.Items15"),
            resources.GetString("cmbBxLanguage.Items16"),
            resources.GetString("cmbBxLanguage.Items17"),
            resources.GetString("cmbBxLanguage.Items18"),
            resources.GetString("cmbBxLanguage.Items19"),
            resources.GetString("cmbBxLanguage.Items20"),
            resources.GetString("cmbBxLanguage.Items21"),
            resources.GetString("cmbBxLanguage.Items22"),
            resources.GetString("cmbBxLanguage.Items23"),
            resources.GetString("cmbBxLanguage.Items24")});
            this.cmbBxLanguage.Name = "cmbBxLanguage";
            this.cmbBxLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbBxLanguage_SelectedIndexChanged);
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
            // RuleWindowsLanguage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbBxLanguage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxDescription);
            this.Name = "RuleWindowsLanguage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBxLanguage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}
