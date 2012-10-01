namespace Wsus_Package_Publisher
{
    partial class ComputerListViewer
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerListViewer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dGVComputer = new System.Windows.Forms.DataGridView();
            this.ComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMnuComputer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DetectNow = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportNow = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dGVComputer)).BeginInit();
            this.ctxMnuComputer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dGVComputer
            // 
            this.dGVComputer.AllowUserToAddRows = false;
            this.dGVComputer.AllowUserToDeleteRows = false;
            this.dGVComputer.AllowUserToOrderColumns = true;
            this.dGVComputer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dGVComputer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVComputer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ComputerName,
            this.IPAdress});
            resources.ApplyResources(this.dGVComputer, "dGVComputer");
            this.dGVComputer.Name = "dGVComputer";
            this.dGVComputer.ReadOnly = true;
            this.dGVComputer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVComputer.SelectionChanged += new System.EventHandler(this.dGVComputer_SelectionChanged);
            this.dGVComputer.Click += new System.EventHandler(this.dGVComputer_Click);
            // 
            // ComputerName
            // 
            this.ComputerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.ComputerName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ComputerName.FillWeight = 50F;
            resources.ApplyResources(this.ComputerName, "ComputerName");
            this.ComputerName.Name = "ComputerName";
            this.ComputerName.ReadOnly = true;
            this.ComputerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // IPAdress
            // 
            this.IPAdress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.IPAdress.DefaultCellStyle = dataGridViewCellStyle4;
            this.IPAdress.FillWeight = 50F;
            resources.ApplyResources(this.IPAdress, "IPAdress");
            this.IPAdress.Name = "IPAdress";
            this.IPAdress.ReadOnly = true;
            // 
            // ctxMnuComputer
            // 
            this.ctxMnuComputer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DetectNow,
            this.ReportNow});
            this.ctxMnuComputer.Name = "ctxMnuComputer";
            resources.ApplyResources(this.ctxMnuComputer, "ctxMnuComputer");
            this.ctxMnuComputer.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuComputer_ItemClicked);
            // 
            // DetectNow
            // 
            this.DetectNow.Name = "DetectNow";
            resources.ApplyResources(this.DetectNow, "DetectNow");
            // 
            // ReportNow
            // 
            this.ReportNow.Name = "ReportNow";
            resources.ApplyResources(this.ReportNow, "ReportNow");
            // 
            // ComputerListViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dGVComputer);
            this.Name = "ComputerListViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dGVComputer)).EndInit();
            this.ctxMnuComputer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVComputer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPAdress;
        private System.Windows.Forms.ContextMenuStrip ctxMnuComputer;
        private System.Windows.Forms.ToolStripMenuItem DetectNow;
        private System.Windows.Forms.ToolStripMenuItem ReportNow;
    }
}
