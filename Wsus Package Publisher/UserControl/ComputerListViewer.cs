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

#region (Event Delegates - événements)

        public delegate void SelectionChangedEventHandler(DataGridViewSelectedRowCollection rows);
        public event SelectionChangedEventHandler SelectionChanged;

#endregion
    }
}
