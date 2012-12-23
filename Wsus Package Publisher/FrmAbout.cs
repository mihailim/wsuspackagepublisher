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
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblRelease.Text = "Release : " + version.ToString();
            WsusWrapper _wsus = WsusWrapper.GetInstance();
            if (_wsus.IsConnected)
            {
                lblServerVersion.Text = "Server Version : " + _wsus.GetServerVersion().ToString();
                txtBxServers.Text = "";
                foreach (Microsoft.UpdateServices.Administration.IDownstreamServer server in _wsus.DownStreamServers)
                    txtBxServers.Text += server.FullDomainName + "\r\n";
                txtBxUserRole.Text = _wsus.UserRole.ToString();
            }
            else
                lblServerVersion.Text = "Server Version : Please, connect first to the server.";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo((sender as LinkLabel).Text);
            System.Diagnostics.Process.Start(sInfo);
        }

    }
}
