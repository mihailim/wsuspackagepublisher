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
        private List<WsusServer> _serverlist = new List<WsusServer>();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmSettings).Assembly);

        internal FrmSettings()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            switch (Properties.Settings.Default.Credential)
            {
                case "SameAsApplication":
                    rdBtnSameAsApplication.Checked = true;
                    break;
                case "Specified":
                    rdBtnSpecified.Checked = true;
                    txtBxLogin.Text = Properties.Settings.Default.Login;
                    txtBxPassword.Text = Properties.Settings.Default.Password;
                    break;
                case " Ask":
                    rdBtnAsk.Checked = true;
                    break;
                default:
                    rdBtnSameAsApplication.Checked = true;
                    break;
            }
        }

        internal List<WsusServer> ServerList
        {
            get { return _serverlist; }
            private set { _serverlist = value; }
        }

        internal List<WsusServer> LoadServerSettings()
        {
            ServerList.Clear();
            cmbBxServerList.Items.Clear();

            if (System.IO.File.Exists("Options.xml"))
            {
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.Load("Options.xml");
                System.Xml.XmlNodeList nodeList = document.GetElementsByTagName("Server");
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    if (node.HasChildNodes)
                    {
                        WsusServer serverWsus = new WsusServer();
                        foreach (System.Xml.XmlNode childNode in node.ChildNodes)
                        {
                            switch (childNode.Name)
                            {
                                case "Name":
                                    serverWsus.Name = childNode.InnerText;
                                    break;
                                case "IsLocal":
                                    bool isLocal;
                                    if (bool.TryParse(childNode.InnerText, out isLocal))
                                        serverWsus.IsLocal = isLocal;
                                    break;
                                case "Port":
                                    int port;
                                    if (int.TryParse(childNode.InnerText, out port))
                                        serverWsus.Port = port;
                                    break;
                                case "UseSSL":
                                    bool useSSL;
                                    if (bool.TryParse(childNode.InnerText, out useSSL))
                                        serverWsus.UseSSL = useSSL;
                                    break;
                                case "DeadLineDaysSpan":
                                    int day;
                                    if (int.TryParse(childNode.InnerText, out day))
                                        serverWsus.DeadLineDaysSpan = day;
                                    break;
                                case "DeadLineHour":
                                    int hour;
                                    if (int.TryParse(childNode.InnerText, out hour))
                                        serverWsus.DeadLineHour = hour;
                                    break;
                                case "DeadLineMinute":
                                    int minute;
                                    if (int.TryParse(childNode.InnerText, out minute))
                                        serverWsus.DeadLineMinute = minute;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (serverWsus.IsValid())
                        {
                            ServerList.Add(serverWsus);
                            cmbBxServerList.Items.Add(serverWsus);
                        }
                    }
                }
            }
            if (ServerList.Count == 0)
                this.ShowDialog();

            return ServerList;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _serverlist.Clear();
            foreach (object obj in cmbBxServerList.Items)
            {
                WsusServer server = (WsusServer)obj;
                _serverlist.Add(server);
            }
            SaveSettings();
            if (rdBtnSameAsApplication.Checked)
                Properties.Settings.Default.Credential = "SameAsApplication";
            if (rdBtnSpecified.Checked)
            {
                Properties.Settings.Default.Credential = "Specified";
                Properties.Settings.Default.Login = txtBxLogin.Text;
                Properties.Settings.Default.Password = txtBxPassword.Text;
            }
            if (rdBtnAsk.Checked)
                Properties.Settings.Default.Credential = "Ask";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void SaveSettings()
        {
            if (System.IO.File.Exists("Options.xml"))
                System.IO.File.Move("Options.xml", "Options.xml.bak");

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlElement rootElement = (System.Xml.XmlElement)xmlDoc.AppendChild(xmlDoc.CreateElement("WsusPackagePublisher"));

            foreach (WsusServer server in ServerList)
            {
                System.Xml.XmlElement serverElement = (System.Xml.XmlElement)rootElement.AppendChild(xmlDoc.CreateElement("Server"));
                serverElement.AppendChild(xmlDoc.CreateElement("Name")).InnerText = server.Name;
                serverElement.AppendChild(xmlDoc.CreateElement("IsLocal")).InnerText = server.IsLocal.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("Port")).InnerText = server.Port.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("UseSSL")).InnerText = server.UseSSL.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("DeadLineDaysSpan")).InnerText = server.DeadLineDaysSpan.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("DeadLineHour")).InnerText = server.DeadLineHour.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("DeadLineMinute")).InnerText = server.DeadLineMinute.ToString();
            }

            xmlDoc.Save("Options.xml");

            if (System.IO.File.Exists("Options.xml.bak"))
                System.IO.File.Delete("Options.xml.bak");
        }

        private void ValidateData()
        {
            btnAddServer.Enabled = (!string.IsNullOrEmpty(txtBxServerName.Text) && cmbBxConnectionPort.SelectedItem != null);
            btnEditServer.Enabled = btnAddServer.Enabled;
            btnRemoveServer.Enabled = (cmbBxServerList.Items.Count != 0 && cmbBxServerList.SelectedItem != null && !string.IsNullOrEmpty(cmbBxServerList.SelectedItem.ToString()));
            btnOk.Enabled = (cmbBxServerList.Items.Count != 0);
        }

        private void btnRemoveServer_Click(object sender, EventArgs e)
        {
            if (cmbBxServerList.SelectedItem != null)
            {
                WsusServer serverToRemove = (WsusServer)cmbBxServerList.SelectedItem;
                if (ServerList.Contains(serverToRemove))
                {
                    ServerList.Remove(serverToRemove);
                    cmbBxServerList.Items.Remove(serverToRemove);
                    if (cmbBxServerList.Items.Count != 0)
                        cmbBxServerList.SelectedIndex = 0;
                    ValidateData();
                }
            }
        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            int port = 443;
            int.TryParse(cmbBxConnectionPort.SelectedItem.ToString(), out port);
            WsusServer serverToAdd = new WsusServer(txtBxServerName.Text, chkBxConnectToLocalServer.Checked, port, chkBxUseSSL.Checked, (int)nupDeadLineDaysSpan.Value, (int)nupDeadLineHour.Value, (int)nupDeadLineMinute.Value);
            cmbBxServerList.Items.Add(serverToAdd);
            cmbBxServerList.SelectedIndex = cmbBxServerList.Items.Count - 1;
            ServerList.Add(serverToAdd);
            ValidateData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cmbBxConnectionPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxConnectionPort.SelectedItem != null)
            {
                switch (cmbBxConnectionPort.SelectedItem.ToString())
                {
                    case "80":
                    case "8530":
                        chkBxUseSSL.Checked = false;
                        break;
                    case "443":
                    case "8531":
                        chkBxUseSSL.Checked = true;
                        break;
                    default:
                        chkBxUseSSL.Checked = false;
                        break;
                }
            }
            ValidateData();
        }

        private void cmbBxServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveServer.Enabled = !string.IsNullOrEmpty(cmbBxServerList.SelectedItem.ToString());

            if (cmbBxServerList.SelectedItem != null)
            {
                WsusServer serverWsus = (WsusServer)cmbBxServerList.SelectedItem;
                txtBxServerName.Text = serverWsus.Name;
                chkBxConnectToLocalServer.Checked = serverWsus.IsLocal;
                if (cmbBxConnectionPort.Items.Contains(serverWsus.Port.ToString()))
                    cmbBxConnectionPort.SelectedItem = serverWsus.Port.ToString();
                chkBxUseSSL.Checked = serverWsus.UseSSL;
                nupDeadLineDaysSpan.Value = serverWsus.DeadLineDaysSpan;
                nupDeadLineHour.Value = serverWsus.DeadLineHour;
                nupDeadLineMinute.Value = serverWsus.DeadLineMinute;
            }
        }

        private void txtBxServerName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void FrmSettings_Shown(object sender, EventArgs e)
        {
            ClearServerSettings();
            if (ServerList.Count == 0)
                MessageBox.Show(resMan.GetString("NeedServerSettings"));
            if (cmbBxServerList.Items.Count != 0)
                cmbBxServerList.SelectedIndex = 0;
        }

        private void ClearServerSettings()
        {
            txtBxServerName.Text = "";
            chkBxConnectToLocalServer.Checked = false;
            cmbBxConnectionPort.SelectedItem = null;
            chkBxUseSSL.Checked = false;
            nupDeadLineDaysSpan.Value = 0;
            nupDeadLineHour.Value = 0;
            nupDeadLineMinute.Value = 0;
        }

        private void btnEditServer_Click(object sender, EventArgs e)
        {
            if (cmbBxServerList.SelectedItem != null)
            {
                WsusServer serverToEdit = (WsusServer)cmbBxServerList.SelectedItem;
                int port = 443;
                if (int.TryParse(cmbBxConnectionPort.SelectedItem.ToString(), out port))
                    serverToEdit.Port = port;
                serverToEdit.IsLocal = chkBxConnectToLocalServer.Checked;
                serverToEdit.UseSSL = chkBxUseSSL.Checked;
                serverToEdit.DeadLineDaysSpan = (int)nupDeadLineDaysSpan.Value;
                serverToEdit.DeadLineHour = (int)nupDeadLineHour.Value;
                serverToEdit.DeadLineMinute = (int)nupDeadLineMinute.Value;

                ServerList.Clear();
                foreach (object obj in cmbBxServerList.Items)
                {
                    WsusServer server = (WsusServer)obj;
                    ServerList.Add(server);
                }
                cmbBxServerList.Items.Clear();
                cmbBxServerList.Items.AddRange(ServerList.ToArray());
                cmbBxServerList.SelectedItem = serverToEdit;
            }
        }

        private void rdBtnSameThanApplication_CheckedChanged(object sender, EventArgs e)
        {
            txtBxLogin.Enabled = rdBtnSpecified.Checked;
            txtBxPassword.Enabled = rdBtnSpecified.Checked;
        }
    }
}
