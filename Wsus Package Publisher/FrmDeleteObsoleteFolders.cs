using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class FrmDeleteObsoleteFolders : Form
    {
        List<System.IO.DirectoryInfo> _obsoleteFolders;

        public FrmDeleteObsoleteFolders(List<System.IO.DirectoryInfo> obsoleteFolders)
        {
            InitializeComponent();
            _obsoleteFolders = obsoleteFolders;
            this.label1.Text += " (" + obsoleteFolders.Count + ")";
            foreach (System.IO.DirectoryInfo directory in _obsoleteFolders)
                chkLstBxFoldersToDelete.Items.Add(directory, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            prgBarDelete.Minimum = 0;
            prgBarDelete.Maximum = chkLstBxFoldersToDelete.CheckedItems.Count;
            prgBarDelete.Value = 0;

            foreach (Object directory in chkLstBxFoldersToDelete.CheckedItems)
            {
                (directory as System.IO.DirectoryInfo).Delete(true);
                prgBarDelete.Value++;
                prgBarDelete.Refresh();
            }
                
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
