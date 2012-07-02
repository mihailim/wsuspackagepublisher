namespace Wsus_Package_Publisher
{
    partial class ComputerDetailViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerDetailViewer));
            this.txtBxDetail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpInstalledAfter = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpInstalledBefore = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // txtBxDetail
            // 
            resources.ApplyResources(this.txtBxDetail, "txtBxDetail");
            this.txtBxDetail.Name = "txtBxDetail";
            this.txtBxDetail.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dtpInstalledAfter
            // 
            resources.ApplyResources(this.dtpInstalledAfter, "dtpInstalledAfter");
            this.dtpInstalledAfter.Name = "dtpInstalledAfter";
            this.dtpInstalledAfter.ValueChanged += new System.EventHandler(this.dtpInstalledAfter_ValueChanged);
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dtpInstalledBefore
            // 
            resources.ApplyResources(this.dtpInstalledBefore, "dtpInstalledBefore");
            this.dtpInstalledBefore.Name = "dtpInstalledBefore";
            this.dtpInstalledBefore.ValueChanged += new System.EventHandler(this.dtpInstalledAfter_ValueChanged);
            // 
            // ComputerDetailViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpInstalledBefore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dtpInstalledAfter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxDetail);
            this.Name = "ComputerDetailViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInstalledAfter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpInstalledBefore;
    }
}
