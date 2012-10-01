using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Wsus_Package_Publisher
{
    public partial class FrmRemoteExecution : Form
    {
        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

        public FrmRemoteExecution()
        {
            InitializeComponent();
        }

        internal void SendDetectNow(List<string> targetComputers, string login, string password)
        {
            this.Refresh();
            foreach (string computer in targetComputers)
            {
                SendCommand(computer, @"WuAuClt /DetectNow", login, password);
            }
            btnClose.Enabled = true;
        }

        internal void SendReportNow(List<string> targetComputers, string login, string password)
        {
            this.Refresh();
            foreach (string computer in targetComputers)
            {
                SendCommand(computer, @"WuAuClt /ReportNow", login, password);
            }
            btnClose.Enabled = true;
        }

        private bool SendCommand(string targetComputer, string commandLine, string login, string password)
        {
#if DEBUG
            System.Windows.Forms.MessageBox.Show("Sending Command : " + commandLine + " With Credential : " + login + " / " + password);
#endif
            dtgvRemoteExecution.Rows.Add(targetComputer);
            int index = dtgvRemoteExecution.Rows.GetLastRow(DataGridViewElementStates.None);
            DataGridViewRow row = dtgvRemoteExecution.Rows[index];
            try
            {
                row.Cells["Result"].Value = "Pinging";
                dtgvRemoteExecution.Refresh();
                System.Net.NetworkInformation.PingReply reply = ping.Send(targetComputer, 200);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    row.Cells["Result"].Value = "Sending Command";
                    dtgvRemoteExecution.Refresh();
                    ConnectionOptions connOptions = new ConnectionOptions();
                    connOptions.Impersonation = ImpersonationLevel.Impersonate;
                    connOptions.EnablePrivileges = true;
                    if (login != null && password != null)
                    {
                        connOptions.Username = login;
                        connOptions.Password = password;
                    }
                    ManagementScope mgmtScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", targetComputer), connOptions);
                    mgmtScope.Connect();
                    ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                    ManagementPath mgmtPath = new ManagementPath("Win32_Process");
                    ManagementClass processClass = new ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                    ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                    inParams["CommandLine"] = commandLine;
                    processClass.InvokeMethod("Create", inParams, null);
                    row.Cells["Result"].Value = "Command Sended";
                    row.Cells["Result"].Style.BackColor = Color.LightGreen;
                    dtgvRemoteExecution.Refresh();
                    return true;
                }
                else
                {
                    row.Cells["Result"].Value = "No ping";
                    row.Cells["Result"].Style.BackColor = Color.Silver;
                    dtgvRemoteExecution.Refresh();
                    return false;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show("Fail to send command : " + ex.Message);
#endif
                row.Cells["Result"].Value = "Exception";
                row.Cells["Result"].Style.BackColor = Color.Silver;
                dtgvRemoteExecution.Refresh();
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
