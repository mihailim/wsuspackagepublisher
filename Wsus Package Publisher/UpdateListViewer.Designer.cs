namespace Wsus_Package_Publisher
{
    partial class UpdateListViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateListViewer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvUpdateList = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArrivalDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUpdateList
            // 
            resources.ApplyResources(this.dgvUpdateList, "dgvUpdateList");
            this.dgvUpdateList.AllowUserToAddRows = false;
            this.dgvUpdateList.AllowUserToDeleteRows = false;
            this.dgvUpdateList.AllowUserToOrderColumns = true;
            this.dgvUpdateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdateList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.ArrivalDate,
            this.CreationDate,
            this.UpdateId});
            this.dgvUpdateList.Name = "dgvUpdateList";
            this.dgvUpdateList.ReadOnly = true;
            this.dgvUpdateList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUpdateList.SelectionChanged += new System.EventHandler(this.dgvUpdateList_SelectionChanged);
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Title.DefaultCellStyle = dataGridViewCellStyle1;
            this.Title.FillWeight = 60F;
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // ArrivalDate
            // 
            this.ArrivalDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.ArrivalDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.ArrivalDate.FillWeight = 20F;
            resources.ApplyResources(this.ArrivalDate, "ArrivalDate");
            this.ArrivalDate.Name = "ArrivalDate";
            this.ArrivalDate.ReadOnly = true;
            // 
            // CreationDate
            // 
            this.CreationDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.CreationDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.CreationDate.FillWeight = 20F;
            resources.ApplyResources(this.CreationDate, "CreationDate");
            this.CreationDate.Name = "CreationDate";
            this.CreationDate.ReadOnly = true;
            // 
            // UpdateId
            // 
            resources.ApplyResources(this.UpdateId, "UpdateId");
            this.UpdateId.Name = "UpdateId";
            this.UpdateId.ReadOnly = true;
            // 
            // UpdateListViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvUpdateList);
            this.Name = "UpdateListViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUpdateList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArrivalDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateId;
    }
}
