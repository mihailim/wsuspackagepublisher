using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class ComputerDetailViewer : UserControl
    {
        DataGridViewSelectedRowCollection _selectedRows;
        WsusWrapper _wsus;

        public ComputerDetailViewer()
        {
            InitializeComponent();

            InstalledAfter = System.DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0));
            InstalledBefore = System.DateTime.Now;
            _wsus = WsusWrapper.GetInstance();
        }
        
        private void CheckDate()
        {
            if (dtpInstalledAfter.Value > dtpInstalledBefore.Value)
                dtpInstalledAfter.Value = dtpInstalledBefore.Value;
            if (dtpInstalledBefore.Value < dtpInstalledAfter.Value)
                dtpInstalledBefore.Value = dtpInstalledAfter.Value;
        }
        
        internal DateTime InstalledAfter
        {
            get { return dtpInstalledAfter.Value; }
            set { dtpInstalledAfter.Value = value; }
        }

        internal DateTime InstalledBefore
        {
            get { return dtpInstalledBefore.Value; }
            set { dtpInstalledBefore.Value = value; }
        }

        private DataGridViewSelectedRowCollection SelectedRows
        {
            get { return _selectedRows; }
            set { _selectedRows = value; }
        }

        private void dtpInstalledAfter_ValueChanged(object sender, EventArgs e)
        {
            CheckDate();
            Display(SelectedRows);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CheckDate();
            Display(SelectedRows);
        }

        internal void Display(DataGridViewSelectedRowCollection rows)
        {
            if (rows != null)
            {
                List<Guid> installedUpdate = new List<Guid>();
                this.Cursor = Cursors.WaitCursor;
                StringBuilder builder = new StringBuilder();
                txtBxDetail.Text = "";
                SelectedRows = rows;

                foreach (DataGridViewRow row in rows)
                {
                    UpdateInstallationInfoCollection updateInfo = _wsus.GetUpdateInstallationInfo(row.Cells[0].Value.ToString(), InstalledAfter, InstalledBefore);

                    foreach (IUpdateInstallationInfo update in updateInfo)
                    {
                        if (!installedUpdate.Contains(update.UpdateId))
                        {
                            installedUpdate.Add(update.UpdateId);
                            builder.AppendLine(update.GetUpdate().Title);
                        }
                    }
                }
                txtBxDetail.Text = builder.ToString();
                this.Cursor = Cursors.Default;
            }
        }
    }
}
