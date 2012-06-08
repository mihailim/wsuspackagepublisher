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
    internal partial class FrmSettings : Form
    {
        internal FrmSettings()
        {
            InitializeComponent();

            txtBxServerName.Text = Properties.Settings.Default.ServerName;
            chkBxUseSSL.Checked = Properties.Settings.Default.UseSSL;
            cmbBxConnectionPort.Text = Properties.Settings.Default.ServerPort.ToString();
        }

        internal string ServerName { get; set; }

        internal int ServerPort { get; set; }

        internal bool UseSSL { get; set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int port = 80;

            if ((!string.IsNullOrEmpty(txtBxServerName.Text)) && (cmbBxConnectionPort.SelectedIndex != -1))
            {
                this.DialogResult = DialogResult.OK;
                ServerName = txtBxServerName.Text;
                int.TryParse(cmbBxConnectionPort.SelectedItem.ToString(), out port);
                ServerPort = port;
                UseSSL = chkBxUseSSL.Checked;

                Properties.Settings.Default.ServerName = ServerName;
                Properties.Settings.Default.ServerPort = ServerPort;
                Properties.Settings.Default.UseSSL = UseSSL;
                Properties.Settings.Default.Save();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cmbBxConnectionPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxConnectionPort.SelectedIndex == 0)
                chkBxUseSSL.Checked = false;
            else
                chkBxUseSSL.Checked = true;
        }

    }
}
