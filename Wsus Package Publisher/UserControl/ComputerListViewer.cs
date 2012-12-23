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
    internal partial class ComputerListViewer : UserControl
    {
        private ComputerTargetCollection _computerTargetCollection;
        private WsusWrapper _wsus;
        private bool populatingDataGridView = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(ComputerListViewer).Assembly);

        public ComputerListViewer()
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();

            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendDetectNow"), "DetectNow"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendReportNow"), "ReportNow"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendRebootNow"), "RebootNow"));

            foreach (DataGridViewColumn column in dGVComputer.Columns)
                ctxMnuHeader.Items.Add(GetItem(column.HeaderText, column.Name, column.Visible));
        }

        private ToolStripMenuItem GetItem(string text, string itemName)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Name = itemName;
            return item;
        }

        private ToolStripMenuItem GetItem(string text, string itemName, bool isChecked)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Name = itemName;
            item.Checked = isChecked;
            return item;
        }

        internal ComputerTargetCollection ComputerCollection
        {
            get { return _computerTargetCollection; }
            set
            {
                _computerTargetCollection = value;
                RefreshDisplay();
            }
        }

        internal int DataGridViewHeight
        {
            get
            {
                int height = 0;

                height += dGVComputer.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dGVComputer.Rows)
                {
                    height += row.Height;
                }

                return height;
            }
        }

        private void RefreshDisplay()
        {
            dGVComputer.SuspendLayout();
            populatingDataGridView = true;
            dGVComputer.Rows.Clear();
            foreach (IComputerTarget computer in ComputerCollection)
            {
                int index = dGVComputer.Rows.Add();
                DataGridViewRow addedRow = dGVComputer.Rows[index];
                addedRow.Cells["ComputerName"].Value = computer.FullDomainName;
                addedRow.Cells["IPAdress"].Value = computer.IPAddress;
                addedRow.Cells["BiosName"].Value = computer.BiosInfo.Name;
                addedRow.Cells["BiosVersion"].Value = computer.BiosInfo.Version;
                addedRow.Cells["LastReportedStatusTime"].Value = computer.LastReportedStatusTime.ToLocalTime().ToString();
                addedRow.Cells["LastSyncTime"].Value = computer.LastSyncTime.ToLocalTime().ToString();
                addedRow.Cells["LastSyncResult"].Value = computer.LastSyncResult.ToString();
                addedRow.Cells["Make"].Value = computer.Make;
                addedRow.Cells["Model"].Value = computer.Model;
                addedRow.Cells["OSArchitecture"].Value = computer.OSArchitecture;
                addedRow.Cells["OSDescription"].Value = computer.OSDescription;
            }
            populatingDataGridView = false;
            dGVComputer.ResumeLayout();
        }

        internal void Display(Guid computerGroupId)
        {
            ComputerCollection = _wsus.GetComputerTargets(computerGroupId);
        }

        private void dGVComputer_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null && !populatingDataGridView)
                SelectionChanged(dGVComputer.SelectedRows);
        }

        private void ctxMnuComputer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            List<string> targetComputers = new List<string>();
            string login = null;
            string password = null;
            FrmRemoteExecution remoteExecution = new FrmRemoteExecution();

            foreach (DataGridViewRow row in dGVComputer.SelectedRows)
            {
                targetComputers.Add(row.Cells[0].Value.ToString());
            }
            ctxMnuComputer.Hide();
            switch (Properties.Settings.Default.Credential)
            {
                case "Ask":
                    Credentials cred = new Credentials();
                    if (cred.ShowDialog() == DialogResult.OK)
                    {
                        login = cred.Login;
                        password = cred.Password;
                    }
                    break;
                case "Specified":
                    login = Properties.Settings.Default.Login;
                    password = Properties.Settings.Default.Password;
                    break;
                default:
                    break;
            }
            remoteExecution.Show(this);
            switch (e.ClickedItem.Name)
            {
                case "DetectNow":
                    remoteExecution.SendDetectNow(targetComputers, login, password);
                    break;
                case "ReportNow":
                    remoteExecution.SendReportNow(targetComputers, login, password);
                    break;
                case "RebootNow":
                    remoteExecution.SendRebootNow(targetComputers, login, password);
                    break;
                default:
                    break;
            }

        }

        private void ctxMnuHeader_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            (e.ClickedItem as ToolStripMenuItem).Checked = !(e.ClickedItem as ToolStripMenuItem).Checked;

            foreach (ToolStripMenuItem menuItem in ctxMnuHeader.Items)
                dGVComputer.Columns[menuItem.Name].Visible = menuItem.Checked;
        }

        private void dGVComputer_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex != -1)
                ctxMnuComputer.Show(dGVComputer, dGVComputer.PointToClient(Cursor.Position));
        }

        private void dGVComputer_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex == -1)
                ctxMnuHeader.Show(dGVComputer, dGVComputer.PointToClient(Cursor.Position));
        }

        #region (Event Delegates - événements)

        public delegate void SelectionChangedEventHandler(DataGridViewSelectedRowCollection rows);
        public event SelectionChangedEventHandler SelectionChanged;

        #endregion

    }
}
