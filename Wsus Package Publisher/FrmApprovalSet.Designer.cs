namespace Wsus_Package_Publisher
{
    partial class FrmApprovalSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmApprovalSet));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTargetGroup = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtDeadLine = new System.Windows.Forms.DateTimePicker();
            this.btnSetDeadLine = new System.Windows.Forms.Button();
            this.cmbBxApproval = new System.Windows.Forms.ComboBox();
            this.btnSetApproval = new System.Windows.Forms.Button();
            this.nupHour = new System.Windows.Forms.NumericUpDown();
            this.nupMinute = new System.Windows.Forms.NumericUpDown();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Approvval = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DeadLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargetGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinute)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTargetGroup
            // 
            this.dgvTargetGroup.AllowUserToAddRows = false;
            this.dgvTargetGroup.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvTargetGroup, "dgvTargetGroup");
            this.dgvTargetGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTargetGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Group,
            this.Approvval,
            this.DeadLine});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTargetGroup.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTargetGroup.Name = "dgvTargetGroup";
            this.dgvTargetGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTargetGroup.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTargetGroup_CellMouseDoubleClick);
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dtDeadLine
            // 
            resources.ApplyResources(this.dtDeadLine, "dtDeadLine");
            this.dtDeadLine.Name = "dtDeadLine";
            // 
            // btnSetDeadLine
            // 
            resources.ApplyResources(this.btnSetDeadLine, "btnSetDeadLine");
            this.btnSetDeadLine.Name = "btnSetDeadLine";
            this.btnSetDeadLine.UseVisualStyleBackColor = true;
            this.btnSetDeadLine.Click += new System.EventHandler(this.btnSetDeadLine_Click);
            // 
            // cmbBxApproval
            // 
            resources.ApplyResources(this.cmbBxApproval, "cmbBxApproval");
            this.cmbBxApproval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxApproval.FormattingEnabled = true;
            this.cmbBxApproval.Name = "cmbBxApproval";
            // 
            // btnSetApproval
            // 
            resources.ApplyResources(this.btnSetApproval, "btnSetApproval");
            this.btnSetApproval.Name = "btnSetApproval";
            this.btnSetApproval.UseVisualStyleBackColor = true;
            this.btnSetApproval.Click += new System.EventHandler(this.btnSetApproval_Click);
            // 
            // nupHour
            // 
            resources.ApplyResources(this.nupHour, "nupHour");
            this.nupHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nupHour.Name = "nupHour";
            this.nupHour.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // nupMinute
            // 
            resources.ApplyResources(this.nupMinute, "nupMinute");
            this.nupMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nupMinute.Name = "nupMinute";
            this.nupMinute.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // Group
            // 
            this.Group.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Group.DefaultCellStyle = dataGridViewCellStyle1;
            this.Group.FillWeight = 30F;
            resources.ApplyResources(this.Group, "Group");
            this.Group.Name = "Group";
            this.Group.ReadOnly = true;
            // 
            // Approvval
            // 
            this.Approvval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Approvval.DefaultCellStyle = dataGridViewCellStyle2;
            this.Approvval.FillWeight = 40F;
            resources.ApplyResources(this.Approvval, "Approvval");
            this.Approvval.Name = "Approvval";
            this.Approvval.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Approvval.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DeadLine
            // 
            this.DeadLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeadLine.FillWeight = 20F;
            resources.ApplyResources(this.DeadLine, "DeadLine");
            this.DeadLine.Name = "DeadLine";
            this.DeadLine.ReadOnly = true;
            // 
            // FrmApprovalSet
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.nupMinute);
            this.Controls.Add(this.nupHour);
            this.Controls.Add(this.btnSetApproval);
            this.Controls.Add(this.cmbBxApproval);
            this.Controls.Add(this.btnSetDeadLine);
            this.Controls.Add(this.dtDeadLine);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dgvTargetGroup);
            this.Name = "FrmApprovalSet";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargetGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTargetGroup;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dtDeadLine;
        private System.Windows.Forms.Button btnSetDeadLine;
        private System.Windows.Forms.ComboBox cmbBxApproval;
        private System.Windows.Forms.Button btnSetApproval;
        private System.Windows.Forms.NumericUpDown nupHour;
        private System.Windows.Forms.NumericUpDown nupMinute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewComboBoxColumn Approvval;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeadLine;
    }
}