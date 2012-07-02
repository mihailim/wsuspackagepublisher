namespace Wsus_Package_Publisher
{
    partial class RulesViewer
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
            this.rtxtBxRulesViewer = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtBxRulesViewer
            // 
            this.rtxtBxRulesViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtBxRulesViewer.HideSelection = false;
            this.rtxtBxRulesViewer.Location = new System.Drawing.Point(0, 0);
            this.rtxtBxRulesViewer.Name = "rtxtBxRulesViewer";
            this.rtxtBxRulesViewer.ReadOnly = true;
            this.rtxtBxRulesViewer.Size = new System.Drawing.Size(510, 319);
            this.rtxtBxRulesViewer.TabIndex = 0;
            this.rtxtBxRulesViewer.Text = "";
            this.rtxtBxRulesViewer.WordWrap = false;
            // 
            // RulesViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtxtBxRulesViewer);
            this.Name = "RulesViewer";
            this.Size = new System.Drawing.Size(510, 319);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtBxRulesViewer;
    }
}
