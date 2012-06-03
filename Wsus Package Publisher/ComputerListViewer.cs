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
    public partial class ComputerListViewer : UserControl
    {
        ComputerTargetCollection _computerTargetCollection;
        ComputerDetailViewer _detailViewer;
        IUpdateServer _wsus;

        internal ComputerListViewer(ComputerTargetCollection computerCollection, ComputerDetailViewer detailViewer, IUpdateServer wsus)
        {
            InitializeComponent();
            _detailViewer = detailViewer;
            _wsus = wsus;
            ComputerCollection = computerCollection;
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

        private void dGVComputer_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewSelectedRowCollection selectedRows = dGVComputer.SelectedRows;
            StringBuilder builder = new StringBuilder();

            _detailViewer.ClearText();

            foreach (DataGridViewRow row in selectedRows)
            {
                IComputerTarget computer = _wsus.GetComputerTargetByName(row.Cells[0].Value.ToString());
                IUpdateSummary updateSummary = computer.GetUpdateInstallationSummary();
                int installed = updateSummary.InstalledCount;
                UpdateScope scope = new UpdateScope();
                scope.FromArrivalDate = System.DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0));
                scope.ToArrivalDate = System.DateTime.Now;
                scope.IncludedInstallationStates = UpdateInstallationStates.Installed;
                UpdateInstallationInfoCollection updateInfo = computer.GetUpdateInstallationInfoPerUpdate(scope);
                IUpdate installedUpdate;
                foreach (IUpdateInstallationInfo update in updateInfo)
                {
                    installedUpdate = update.GetUpdate();
                    builder.AppendLine(installedUpdate.Title);
                }                
            }
            _detailViewer.AddText(builder.ToString());
            this.Cursor = Cursors.Default;
        }

    }
}
