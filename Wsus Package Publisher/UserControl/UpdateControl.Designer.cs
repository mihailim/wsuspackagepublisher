﻿namespace Wsus_Package_Publisher
{
    partial class UpdateControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.updateListViewer1 = new Wsus_Package_Publisher.UpdateListViewer();
            this.updateDetailViewer1 = new Wsus_Package_Publisher.UpdateDetailViewer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.updateListViewer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.updateDetailViewer1);
            // 
            // updateListViewer1
            // 
            resources.ApplyResources(this.updateListViewer1, "updateListViewer1");
            this.updateListViewer1.Name = "updateListViewer1";
            // 
            // updateDetailViewer1
            // 
            resources.ApplyResources(this.updateDetailViewer1, "updateDetailViewer1");
            this.updateDetailViewer1.Name = "updateDetailViewer1";
            // 
            // UpdateControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UpdateControl";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private UpdateListViewer updateListViewer1;
        private UpdateDetailViewer updateDetailViewer1;
    }
}
