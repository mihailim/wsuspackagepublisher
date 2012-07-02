using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class ComputerControl : UserControl
    {
        public ComputerControl()
        {
            InitializeComponent();
            computerListViewer1.SelectionChanged += new ComputerListViewer.SelectionChangedEventHandler(computerListViewer1_SelectionChanged);
        }

#region (Properties - Propriétés)
                

#endregion

        internal void Display(Guid computerGroupId)
        {
            computerListViewer1.Display(computerGroupId);
        }

        private void computerListViewer1_SelectionChanged(DataGridViewSelectedRowCollection rows)
        {
            computerDetailViewer1.Display(rows);
        }
    }
}
