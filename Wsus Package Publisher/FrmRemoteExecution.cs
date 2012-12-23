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
        private struct CommandParameters
        {
            internal List<string> TargetComputers { get; set; }
            internal string CommandLine { get; set; }
            internal string Login { get; set; }
            internal string Password { get; set; }
        }

        private string localComputerName = string.Empty;
        private System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        private int success = 0;
        private int failed = 0;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmRemoteExecution).Assembly);

        public FrmRemoteExecution()
        {
            InitializeComponent();
            localComputerName = Environment.MachineName;
        }

        internal void SendDetectNow(List<string> targetComputers, string login, string password)
        {
            CommandParameters parameters = new CommandParameters();

            parameters.TargetComputers = targetComputers;
            parameters.CommandLine = @"WuAuClt /DetectNow";
            parameters.Login = login;
            parameters.Password = password;
            success = 0;
            failed = 0;

            prgBarSendCommand.Value = 1;
            prgBarSendCommand.Maximum = targetComputers.Count + 1;
            prgBarSendCommand.Refresh();

            System.Threading.Thread commandThread = new System.Threading.Thread(SendCommand);
            commandThread.Start(parameters);
        }

        internal void SendReportNow(List<string> targetComputers, string login, string password)
        {
            CommandParameters parameters = new CommandParameters();

            parameters.TargetComputers = targetComputers;
            parameters.CommandLine = @"WuAuClt /ReportNow";
            parameters.Login = login;
            parameters.Password = password;
            success = 0;
            failed = 0;

            prgBarSendCommand.Value = 1;
            prgBarSendCommand.Maximum = targetComputers.Count + 1;
            prgBarSendCommand.Refresh();

            System.Threading.Thread commandThread = new System.Threading.Thread(SendCommand);
            commandThread.Start(parameters);
        }

        internal void SendRebootNow(List<string> targetComputers, string login, string password)
        {
            CommandParameters parameters = new CommandParameters();

            parameters.TargetComputers = targetComputers;
            parameters.CommandLine = @"Shutdown /r /t 0 /d p:2:17";
            parameters.Login = login;
            parameters.Password = password;
            success = 0;
            failed = 0;

            prgBarSendCommand.Value = 1;
            prgBarSendCommand.Maximum = targetComputers.Count + 1;
            prgBarSendCommand.Refresh();

            System.Threading.Thread commandThread = new System.Threading.Thread(SendCommand);
            commandThread.Start(parameters);
        }

        private void SendCommand(Object sendParameters)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<Object>(SendCommand), new object[] { sendParameters });
                return;
            }

            CommandParameters parameters = (CommandParameters)sendParameters;
            bool credentialFailed = false;
            
#if DEBUG
            System.Windows.Forms.MessageBox.Show("Sending Command : " + parameters.CommandLine + " With Credential : " + parameters.Login + " / " + parameters.Password);
#endif

            foreach (string computer in parameters.TargetComputers)
            {
                dtgvRemoteExecution.Rows.Add(computer);
                int index = dtgvRemoteExecution.Rows.GetLastRow(DataGridViewElementStates.None);
                DataGridViewRow row = dtgvRemoteExecution.Rows[index];
                dtgvRemoteExecution.CurrentCell = dtgvRemoteExecution[0, index];
                try
                {
                    row.Cells["Result"].Value = resMan.GetString("Pinging");
                    dtgvRemoteExecution.Refresh();
                    System.Net.NetworkInformation.PingReply reply = ping.Send(computer, 200);
                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        row.Cells["Result"].Value = resMan.GetString("SendingCommand");
                        dtgvRemoteExecution.Refresh();
                        ConnectionOptions connOptions = new ConnectionOptions();
                        connOptions.Impersonation = ImpersonationLevel.Impersonate;
                        connOptions.EnablePrivileges = true;
                        if (parameters.Login != null && parameters.Password != null && computer.ToLower() != localComputerName.ToLower())
                        {
                            connOptions.Username = parameters.Login;
                            connOptions.Password = parameters.Password;
                        }
                        ManagementScope mgmtScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", computer), connOptions);
                        mgmtScope.Connect();
                        ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                        ManagementPath mgmtPath = new ManagementPath("Win32_Process");
                        ManagementClass processClass = new ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                        ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                        inParams["CommandLine"] = parameters.CommandLine;
                        processClass.InvokeMethod("Create", inParams, null);
                        row.Cells["Result"].Value = resMan.GetString("CommandSended");
                        row.Cells["Result"].Style.BackColor = Color.LightGreen;
                        dtgvRemoteExecution.Refresh();
                        prgBarSendCommand.PerformStep();
                        success++;
                    }
                    else
                    {
                        row.Cells["Result"].Value = resMan.GetString("Noping");
                        row.Cells["Result"].Style.BackColor = Color.Silver;
                        dtgvRemoteExecution.Refresh();
                        prgBarSendCommand.PerformStep();
                        failed++;
                    }
                    prgBarSendCommand.Refresh();
                }
                catch (UnauthorizedAccessException accessDenied)
                {
                    row.Cells["Result"].Value = resMan.GetString("Exception");
                    row.Cells["Result"].Style.BackColor = Color.Silver;
                    dtgvRemoteExecution.Refresh();
                    prgBarSendCommand.PerformStep();
                    failed++;

                    if (!credentialFailed)
                    {
                        credentialFailed = true;
                        if (MessageBox.Show(resMan.GetString("CredentialFailed"), resMan.GetString("FailToConnect"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            break;
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                System.Windows.Forms.MessageBox.Show("Fail to send command : " + ex.Message);
#endif
                    row.Cells["Result"].Value = resMan.GetString("Exception");
                    row.Cells["Result"].Style.BackColor = Color.Silver;
                    dtgvRemoteExecution.Refresh();
                    prgBarSendCommand.PerformStep();
                    failed++;
                }
            }

            btnClose.Enabled = true;
            lblSummary.Text = resMan.GetString("Summary") + " : " + resMan.GetString("Succeeded") + " = " + success + "   " + resMan.GetString("Failed") + " = " + failed + " " + resMan.GetString("On") + " " + (success + failed);
            dtgvRemoteExecution.Sort(dtgvRemoteExecution.Columns["Result"], ListSortDirection.Descending);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
