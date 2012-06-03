namespace UpdateViewer
{
    partial class FrmUpdateViewer
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.trvWsus = new System.Windows.Forms.TreeView();
            this.txtBxRule = new System.Windows.Forms.TextBox();
            this.lstBxUpdates = new System.Windows.Forms.ListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvWsus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(759, 488);
            this.splitContainer1.SplitterDistance = 253;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lstBxUpdates);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtBxRule);
            this.splitContainer2.Size = new System.Drawing.Size(502, 488);
            this.splitContainer2.SplitterDistance = 280;
            this.splitContainer2.TabIndex = 0;
            // 
            // trvWsus
            // 
            this.trvWsus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvWsus.Location = new System.Drawing.Point(0, 0);
            this.trvWsus.Name = "trvWsus";
            this.trvWsus.Size = new System.Drawing.Size(253, 488);
            this.trvWsus.TabIndex = 0;
            this.trvWsus.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvWsus_AfterSelect);
            // 
            // txtBxRule
            // 
            this.txtBxRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxRule.Location = new System.Drawing.Point(0, 0);
            this.txtBxRule.Multiline = true;
            this.txtBxRule.Name = "txtBxRule";
            this.txtBxRule.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBxRule.Size = new System.Drawing.Size(502, 204);
            this.txtBxRule.TabIndex = 0;
            // 
            // lstBxUpdates
            // 
            this.lstBxUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxUpdates.FormattingEnabled = true;
            this.lstBxUpdates.Location = new System.Drawing.Point(0, 0);
            this.lstBxUpdates.Name = "lstBxUpdates";
            this.lstBxUpdates.Size = new System.Drawing.Size(502, 280);
            this.lstBxUpdates.TabIndex = 0;
            this.lstBxUpdates.SelectedIndexChanged += new System.EventHandler(this.lstBxUpdates_SelectedIndexChanged);
            // 
            // FrmUpdateViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 488);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmUpdateViewer";
            this.Text = "Update Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView trvWsus;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtBxRule;
        private System.Windows.Forms.ListBox lstBxUpdates;
    }
}

