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
        ComputerTargetCollection _computerTargetCollection;
        WsusWrapper _wsus;

        public ComputerListViewer()
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();
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
            dGVComputer.Rows.Clear();
            foreach (IComputerTarget computer in ComputerCollection)
            {
                dGVComputer.Rows.Add(computer.FullDomainName, computer.IPAddress);
            }
            dGVComputer.ResumeLayout();
        }

        internal void Display(Guid computerGroupId)
        {
            ComputerCollection = _wsus.GetComputerTargets(computerGroupId);
        }

        private void dGVComputer_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(dGVComputer.SelectedRows);
        }

        private void dGVComputer_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (mouse.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (dGVComputer.SelectedRows.Count != 0)
                    ctxMnuComputer.Show(this, new Point(mouse.X, mouse.Y));
            }
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
            remoteExecution.Show();
            switch (e.ClickedItem.Name)
            {
                case "DetectNow":
                    remoteExecution.SendDetectNow(targetComputers, login, password);
                    break;
                case "ReportNow":
                    remoteExecution.SendReportNow(targetComputers, login, password);
                    break;
                default:
                    break;
            }

        }



        #region (Event Delegates - événements)

        public delegate void SelectionChangedEventHandler(DataGridViewSelectedRowCollection rows);
        public event SelectionChangedEventHandler SelectionChanged;

        #endregion





    }
}
