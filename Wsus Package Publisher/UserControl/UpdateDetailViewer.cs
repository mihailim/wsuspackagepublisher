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
    public partial class UpdateDetailViewer : UserControl
    {
        private struct ReportResult
        {
            internal DataGridViewRow Row { get; set; }
            internal int InstalledCount { get; set; }
            internal int InstalledPendingRebootCount { get; set; }
            internal int NotInstalledCount { get; set; }
            internal int DownloadedCount { get; set; }
            internal int NotApplicableCount { get; set; }
            internal int FailedCount { get; set; }
            internal int UnknownCount { get; set; }
            internal int GetTotal()
            {
                return InstalledCount + InstalledPendingRebootCount + NotInstalledCount + DownloadedCount + NotApplicableCount + FailedCount + UnknownCount;

            }
        }

        private UpdateCollection _update;
        private WsusWrapper _wsus;
        private ComputerGroup _computerGroups;
        private List<UpdateInstallationState> _filter = new List<UpdateInstallationState>();
        private System.Threading.Thread displayReportThread;
        private bool cancelDisplayReport = false;
        private bool updateReportDisplayed = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(UpdateDetailViewer).Assembly);

        public UpdateDetailViewer()
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();

            chkCmbBxFilter.AddItem(resMan.GetString("Downloaded"));
            chkCmbBxFilter.AddItem(resMan.GetString("Failed"));
            chkCmbBxFilter.AddItem(resMan.GetString("Installed"));
            chkCmbBxFilter.AddItem(resMan.GetString("InstalledPendingReboot"));
            chkCmbBxFilter.AddItem(resMan.GetString("NotApplicable"));
            chkCmbBxFilter.AddItem(resMan.GetString("NotInstalled"));
            chkCmbBxFilter.AddItem(resMan.GetString("Unknown"));

            List<object> allItem = new List<object>();
            allItem.Add(resMan.GetString("Downloaded"));
            allItem.Add(resMan.GetString("Failed"));
            allItem.Add(resMan.GetString("Installed"));
            allItem.Add(resMan.GetString("InstalledPendingReboot"));
            allItem.Add(resMan.GetString("NotApplicable"));
            allItem.Add(resMan.GetString("NotInstalled"));
            allItem.Add(resMan.GetString("Unknown"));

            chkCmbBxFilter.AddShortcut(resMan.GetString("All"), allItem);

            List<object> neededButNotInstalled = new List<object>();
            neededButNotInstalled.Add(resMan.GetString("Downloaded"));
            neededButNotInstalled.Add(resMan.GetString("Failed"));
            neededButNotInstalled.Add(resMan.GetString("NotInstalled"));

            chkCmbBxFilter.AddShortcut(resMan.GetString("NeededButNotInstalled"), neededButNotInstalled);

            List<object> installedOrNotApplicable = new List<object>();
            installedOrNotApplicable.Add(resMan.GetString("Installed"));
            installedOrNotApplicable.Add(resMan.GetString("InstalledPendingReboot"));
            installedOrNotApplicable.Add(resMan.GetString("NotApplicable"));

            chkCmbBxFilter.AddShortcut(resMan.GetString("InstalledOrNotApplicable"), installedOrNotApplicable);

            chkCmbBxFilter.SelectShortcut(resMan.GetString("All"), true);

            dgvReport.Columns["Groups"].HeaderText = resMan.GetString("Groups");
            dgvReport.Columns["Installed"].HeaderText = resMan.GetString("Installed");
            dgvReport.Columns["InstalledPendingReboot"].HeaderText = resMan.GetString("InstalledPendingReboot");
            dgvReport.Columns["NotInstalled"].HeaderText = resMan.GetString("NotInstalled");
            dgvReport.Columns["Downloaded"].HeaderText = resMan.GetString("Downloaded");
            dgvReport.Columns["NotApplicable"].HeaderText = resMan.GetString("NotApplicable");
            dgvReport.Columns["Failed"].HeaderText = resMan.GetString("Failed");
            dgvReport.Columns["Unknown"].HeaderText = resMan.GetString("Unknown");

            ctxMnuStripCommand.Items.Add(GetItem(resMan.GetString("SendDetectNow"), "DetectNow"));
            ctxMnuStripCommand.Items.Add(GetItem(resMan.GetString("SendReportNow"), "ReportNow"));
            ctxMnuStripCommand.Items.Add(GetItem(resMan.GetString("SendRebootNow"), "RebootNow"));

            LockFunctionnalities(_wsus.IsReplica);

            displayReportThread = new System.Threading.Thread(new System.Threading.ThreadStart(DisplayReport));
            displayReportThread.IsBackground = false;
        }

        public new void Dispose()
        {
            cancelDisplayReport = true;
            if (displayReportThread.ThreadState != System.Threading.ThreadState.Unstarted)
            {
                displayReportThread.Abort();
                displayReportThread.Join(500);
            }
            displayReportThread = null;
            base.Dispose(true);
        }

        #region (Methods - Méthodes)

        private ToolStripMenuItem GetItem(string text, string itemName)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Name = itemName;
            return item;
        }

        internal void SetComputerGroups(ComputerGroup computerGroups, TreeNode allComputersNode)
        {
            _computerGroups = computerGroups;
            cmbBxComputerGroup.Items.Clear();
            FillComputerGroup(computerGroups);
        }

        private void FillComputerGroup(ComputerGroup computerGrouptoAdd)
        {
            cmbBxComputerGroup.Items.Add(computerGrouptoAdd);
            int index = dgvReport.Rows.Add();
            dgvReport.Rows[index].Cells["Groups"].Value = computerGrouptoAdd;
            foreach (ComputerGroup group in computerGrouptoAdd.InnerComputerGroup)
            {
                FillComputerGroup(group);
            }
        }

        internal void DisplayUpdates(UpdateCollection updates)
        {
            ClearDisplay();

            // Informations Tab

            if (updates != null && updates.Count != 0)
            {
                IUpdate update = updates[0];

                if ((update.CompanyTitles.Count != 0) && (!string.IsNullOrEmpty(update.CompanyTitles[0])))
                    txtBxCompany.Text = update.CompanyTitles[0].ToString();
                if ((update.ProductTitles.Count != 0) && (!string.IsNullOrEmpty(update.ProductTitles[0])))
                    txtBxProductTitle.Text = update.ProductTitles[0].ToString();

                chkBxIsApproved.CheckState = GetApproveState(updates);
                chkBxIsDeclined.CheckState = GetDeclineState(updates);
                chkBxIsExpired.CheckState = GetExpireState(updates);
                chkBxIsSupersedes.CheckState = GetSupersededState(updates);

                if (updates.Count > 1)
                {
                    btnRevise.Enabled = false;
                    btnDelete.Enabled = !HasSomeApprove(updates);
                    txtBxTitle.Text = "*";
                    txtBxAdditionnalInformationURL.Text = "*";
                    txtBxDescription.Text = "*";
                }
                else
                {
                    if (!string.IsNullOrEmpty(update.Title))
                        txtBxTitle.Text = update.Title;
                    if (update.CreationDate != null)
                        txtBxCreationDate.Text = update.CreationDate.ToString();
                    if (update.ArrivalDate != null)
                        txtBxArrivalDate.Text = update.ArrivalDate.ToString();
                    btnDecline.Enabled = !update.IsDeclined && !_wsus.IsReplica;
                    btnExpire.Enabled = (update.PublicationState != PublicationState.Expired && !_wsus.IsReplica);
                    btnRevise.Enabled = !_wsus.IsReplica;
                    btnDelete.Enabled = !update.IsApproved;

                    if ((update.AdditionalInformationUrls.Count != 0) && (!string.IsNullOrEmpty(update.AdditionalInformationUrls[0].ToString())))
                        txtBxAdditionnalInformationURL.Text = update.AdditionalInformationUrls[0].ToString();
                    if (!string.IsNullOrEmpty(update.Description))
                        txtBxDescription.Text = update.Description;
                    System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.UpdateServices.Administration.IInstallableItem> items = update.GetInstallableItems();
                    if (items.Count != 0 && items[0].Files.Count != 0 && items[0].Files[0].FileUri != null)
                        txtBxFolder.Text = items[0].Files[0].FileUri.ToString();
                    if (!System.IO.File.Exists(GetPathFromUrl(txtBxFolder.Text)))
                        txtBxFolder.BackColor = Color.Red;
                    else
                        txtBxFolder.BackColor = SystemColors.Control;
                    txtBxId.Text = update.Id.UpdateId.ToString();
                    if (!IsUpdateServicesPackagesFolderExists(txtBxId.Text))
                        txtBxId.BackColor = Color.Orange;
                    else
                        txtBxId.BackColor = SystemColors.Control;
                }
                // Status Tab

                if (ViewedUpdates.Count == 1)
                {
                    if (cmbBxComputerGroup.SelectedItem != null)
                    {
                        Guid targetGroupId = (cmbBxComputerGroup.SelectedItem as ComputerGroup).ComputerGroupId;
                        UpdateInstallationInfoCollection updateInfo = _wsus.GetUpdateInstallationInfoPerComputerTarget(targetGroupId, ViewedUpdates[0]);

                        foreach (IUpdateInstallationInfo info in updateInfo)
                        {
                            if (_filter.Contains(info.UpdateInstallationState))
                                dgvComputerStatus.Rows.Add(_wsus.GetComputerName(info.ComputerTargetId), resMan.GetString(info.UpdateInstallationState.ToString()), resMan.GetString(info.UpdateApprovalAction.ToString()));
                        }
                    }
                }
                // Report Tab

                ComputeReport();
            }
        }

        private bool HasSomeApprove(UpdateCollection updates)
        {
            foreach (IUpdate update in updates)
                if (update.IsApproved)
                    return true;
            return false;
        }

        internal void ResetControl()
        {
            _update = null;
            updateReportDisplayed = false;
            ClearDisplay();
            dgvReport.Rows.Clear();
        }

        internal void ClearDisplay()
        {
            txtBxCompany.Text = "";
            txtBxProductTitle.Text = "";
            txtBxTitle.Text = "";
            txtBxCreationDate.Text = "";
            txtBxArrivalDate.Text = "";
            txtBxAdditionnalInformationURL.Text = "";
            txtBxDescription.Text = "";
            txtBxFolder.Text = "";
            txtBxFolder.BackColor = SystemColors.Control;
            txtBxId.Text = "";
            txtBxId.BackColor = SystemColors.Control;

            dgvComputerStatus.Rows.Clear();
        }

        internal void UpdateSelectionChanged(DataGridViewSelectedRowCollection rows)
        {
            UpdateCollection updates = new UpdateCollection();

            foreach (DataGridViewRow row in rows)
                updates.Add((IUpdate)row.Cells["UpdateId"].Value);

            ViewedUpdates = updates;
        }

        internal void LockFunctionnalities(bool isLock)
        {
            btnApprove.Enabled = !isLock;
            btnDecline.Enabled = !isLock;
            btnExpire.Enabled = !isLock;
            btnRevise.Enabled = !isLock;
        }

        internal void RunningLongOperation(bool running)
        {
            if (running)
                this.Cursor = Cursors.WaitCursor;
            else
                this.Cursor = Cursors.Default;
        }

        private void DisplayReport()
        {
            char[] oneSpace = new char[] { ' ' };
            ReportResult resultToDisplay = new ReportResult();
            UpdateInstallationInfoCollection updateInfo;

            if (ViewedUpdates != null && ViewedUpdates.Count == 1)
            {
                IUpdate update = ViewedUpdates[0];
                int installedCount;
                int installedPendingRebootCount;
                int notInstalledCount;
                int downloadedCount;
                int notApplicableCount;
                int failedCount;
                int unknownCount;
                UpdateInstallationState state;

                foreach (DataGridViewRow row in dgvReport.Rows)
                {
                    installedCount = 0;
                    installedPendingRebootCount = 0;
                    notInstalledCount = 0;
                    downloadedCount = 0;
                    notApplicableCount = 0;
                    failedCount = 0;
                    unknownCount = 0;

                    Guid computerGroupID = (row.Cells["Groups"].Value as ComputerGroup).ComputerGroupId;
                    if (!cancelDisplayReport)
                        updateInfo = _wsus.GetUpdateInstallationInfoPerComputerTarget(computerGroupID, update);
                    else
                        break;

                    foreach (IUpdateInstallationInfo info in updateInfo)
                    {
                        state = info.UpdateInstallationState;
                        switch (state)
                        {
                            case UpdateInstallationState.Downloaded:
                                downloadedCount++;
                                break;
                            case UpdateInstallationState.Failed:
                                failedCount++;
                                break;
                            case UpdateInstallationState.Installed:
                                installedCount++;
                                break;
                            case UpdateInstallationState.InstalledPendingReboot:
                                installedPendingRebootCount++;
                                break;
                            case UpdateInstallationState.NotApplicable:
                                notApplicableCount++;
                                break;
                            case UpdateInstallationState.NotInstalled:
                                notInstalledCount++;
                                break;
                            case UpdateInstallationState.Unknown:
                                unknownCount++;
                                break;
                            default:
                                break;
                        }
                    }
                    resultToDisplay.Row = row;
                    resultToDisplay.InstalledCount = installedCount;
                    resultToDisplay.InstalledPendingRebootCount = installedPendingRebootCount;
                    resultToDisplay.NotInstalledCount = notInstalledCount;
                    resultToDisplay.DownloadedCount = downloadedCount;
                    resultToDisplay.NotApplicableCount = notApplicableCount;
                    resultToDisplay.FailedCount = failedCount;
                    resultToDisplay.UnknownCount = unknownCount;
                    if (!cancelDisplayReport)
                        FillRow(resultToDisplay);
                }
            }
            else
                ClearReport();
        }

        private void ClearReport()
        {
            ReportResult resultToDisplay = new ReportResult();

            dgvReport.SuspendLayout();
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                resultToDisplay.Row = row;
                resultToDisplay.InstalledCount = 0;
                resultToDisplay.InstalledPendingRebootCount = 0;
                resultToDisplay.NotInstalledCount = 0;
                resultToDisplay.DownloadedCount = 0;
                resultToDisplay.NotApplicableCount = 0;
                resultToDisplay.FailedCount = 0;
                resultToDisplay.UnknownCount = 0;
                FillRow(resultToDisplay);
            }
            dgvReport.ResumeLayout();
        }

        private void FillRow(ReportResult resultToDisplay)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<ReportResult>(FillRow), new object[] { resultToDisplay });
                return;
            }

            if (resultToDisplay.InstalledCount != 0)
                resultToDisplay.Row.Cells["Installed"].Style.BackColor = Properties.Settings.Default.InstalledColor;
            else
                resultToDisplay.Row.Cells["Installed"].Style.BackColor = Color.White;
            resultToDisplay.Row.Cells["Installed"].Value = resultToDisplay.InstalledCount;

            if (resultToDisplay.InstalledPendingRebootCount != 0)
                resultToDisplay.Row.Cells["InstalledPendingReboot"].Style.BackColor = Properties.Settings.Default.InstalledPendingRebootColor;
            else
                resultToDisplay.Row.Cells["InstalledPendingReboot"].Style.BackColor = Color.White;
            resultToDisplay.Row.Cells["InstalledPendingReboot"].Value = resultToDisplay.InstalledPendingRebootCount;

            if (resultToDisplay.DownloadedCount != 0)
                resultToDisplay.Row.Cells["Downloaded"].Style.BackColor = Properties.Settings.Default.DownloadedColor;
            else
                resultToDisplay.Row.Cells["Downloaded"].Style.BackColor = Color.White;
            resultToDisplay.Row.Cells["Downloaded"].Value = resultToDisplay.DownloadedCount;

            if (resultToDisplay.NotApplicableCount != 0)
                resultToDisplay.Row.Cells["NotApplicable"].Style.BackColor = Properties.Settings.Default.NotApplicableColor;
            else
                resultToDisplay.Row.Cells["NotApplicable"].Style.BackColor = Color.White;
            resultToDisplay.Row.Cells["NotApplicable"].Value = resultToDisplay.NotApplicableCount;

            if (resultToDisplay.NotInstalledCount != 0)
                resultToDisplay.Row.Cells["NotInstalled"].Style.BackColor = Properties.Settings.Default.NotInstalledColor;
            else
                resultToDisplay.Row.Cells["NotInstalled"].Style.BackColor = Color.White;
            resultToDisplay.Row.Cells["NotInstalled"].Value = resultToDisplay.NotInstalledCount;

            if (resultToDisplay.UnknownCount != 0)
                resultToDisplay.Row.Cells["Unknown"].Style.BackColor = Properties.Settings.Default.UnknownColor;
            else
                resultToDisplay.Row.Cells["Unknown"].Style.BackColor = Color.White;
            resultToDisplay.Row.Cells["Unknown"].Value = resultToDisplay.UnknownCount;

            if (resultToDisplay.FailedCount != 0)
                resultToDisplay.Row.Cells["Failed"].Style.BackColor = Properties.Settings.Default.FailedColor;
            else
                resultToDisplay.Row.Cells["Failed"].Style.BackColor = Color.White;
            resultToDisplay.Row.Cells["Failed"].Value = resultToDisplay.FailedCount;

            if (chkBxShowRowsWithValues.Checked)
                resultToDisplay.Row.Visible = (resultToDisplay.GetTotal() != 0);
        }

        private System.Windows.Forms.CheckState GetApproveState(UpdateCollection updates)
        {
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.IsApproved)
                    nbrTrue++;
                else
                    nbrFalse++;
            }
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private System.Windows.Forms.CheckState GetDeclineState(UpdateCollection updates)
        {
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.IsDeclined)
                    nbrTrue++;
                else
                    nbrFalse++;
            }
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private System.Windows.Forms.CheckState GetExpireState(UpdateCollection updates)
        {
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.PublicationState == PublicationState.Expired)
                    nbrTrue++;
                else
                    nbrFalse++;
            }
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private System.Windows.Forms.CheckState GetSupersededState(UpdateCollection updates)
        {
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.IsSuperseded)
                    nbrTrue++;
                else
                    nbrFalse++;
            }
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private string GetPathFromUrl(string url)
        {
            if (url.ToLower().StartsWith("http://"))
            {
                url = url.Substring(7);
                url = url.Replace('/', '\\');
                url = url.Replace("Content", "WsusContent");
                int index = url.IndexOf(':');
                if (index != -1)
                    url = url.Substring(0, index) + url.Substring(url.IndexOf('\\'));
                return @"\\" + url;
            }
            return string.Empty;
        }

        private bool IsUpdateServicesPackagesFolderExists(string subfolder)
        {
            string updateFolder = @"\\" + _wsus.Server.Name + @"\UpdateServicesPackages\" + subfolder;
            return System.IO.Directory.Exists(updateFolder);
        }
        
        #endregion

        #region (Properties - Propriétés)

        internal UpdateCollection ViewedUpdates
        {
            get { return _update; }
            set
            {
                _update = value;
                updateReportDisplayed = false;
                DisplayUpdates(ViewedUpdates);
            }
        }

        #endregion

        #region (response to events - Réponses aux événements)

        private void cmbBxComputerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUpdates(ViewedUpdates);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            btnApprove.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (ApproveUpdate != null)
                ApproveUpdate(ViewedUpdates);
            btnApprove.Enabled = !_wsus.IsReplica;
            this.Cursor = Cursors.Default;
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            btnDecline.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (DeclineUpdate != null)
                DeclineUpdate(ViewedUpdates);
            btnDecline.Enabled = !_wsus.IsReplica;
            this.Cursor = Cursors.Default;
        }

        private void btnExpire_Click(object sender, EventArgs e)
        {
            btnExpire.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (ExpireUpdate != null)
                ExpireUpdate(ViewedUpdates);
            btnExpire.Enabled = !_wsus.IsReplica;
            this.Cursor = Cursors.Default;
        }

        private void btnRevise_Click(object sender, EventArgs e)
        {
            btnRevise.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (ReviseUpdate != null)
                ReviseUpdate(ViewedUpdates[0]);
            btnRevise.Enabled = !_wsus.IsReplica;
            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (DeleteUpdate != null)
                DeleteUpdate(ViewedUpdates);
            btnDelete.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void checkComboBox1_SelectionChanged()
        {
            _filter.Clear();
            List<object> selectedObj = chkCmbBxFilter.SelectedItems;

            foreach (object obj in selectedObj)
            {
                string item = obj.ToString();
                if (item == resMan.GetString("Downloaded"))
                    _filter.Add(UpdateInstallationState.Downloaded);

                if (item == resMan.GetString("Failed"))
                    _filter.Add(UpdateInstallationState.Failed);

                if (item == resMan.GetString("Installed"))
                    _filter.Add(UpdateInstallationState.Installed);

                if (item == resMan.GetString("InstalledPendingReboot"))
                    _filter.Add(UpdateInstallationState.InstalledPendingReboot);

                if (item == resMan.GetString("NotApplicable"))
                    _filter.Add(UpdateInstallationState.NotApplicable);

                if (item == resMan.GetString("NotInstalled"))
                    _filter.Add(UpdateInstallationState.NotInstalled);

                if (item == resMan.GetString("Unknown"))
                    _filter.Add(UpdateInstallationState.Unknown);
            }
            DisplayUpdates(ViewedUpdates);
        }

        private void ctxMnuStripCommand_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            List<string> targetComputers = new List<string>();
            string login = null;
            string password = null;
            FrmRemoteExecution remoteExecution = new FrmRemoteExecution();

            foreach (DataGridViewRow row in dgvComputerStatus.SelectedRows)
            {
                targetComputers.Add(row.Cells[0].Value.ToString());
            }
            ctxMnuStripCommand.Hide();
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
            remoteExecution.Show(this);
            switch (e.ClickedItem.Name)
            {
                case "DetectNow":
                    remoteExecution.SendDetectNow(targetComputers, login, password);
                    break;
                case "ReportNow":
                    remoteExecution.SendReportNow(targetComputers, login, password);
                    break;
                case "RebootNow":
                    remoteExecution.SendRebootNow(targetComputers, login, password);
                    break;
                default:
                    break;
            }
        }

        private void ComputeReport()
        {
            if (!updateReportDisplayed && tabUpdateDetailViewer.SelectedTab == tabUpdateDetailViewer.TabPages["TabReport"])
            {
                ClearReport();

                if (displayReportThread.IsAlive)
                {
                    cancelDisplayReport = true;
                    displayReportThread.Join(2000);
                }
                cancelDisplayReport = false;
                displayReportThread = null;
                displayReportThread = new System.Threading.Thread(new System.Threading.ThreadStart(DisplayReport));
                displayReportThread.Priority = System.Threading.ThreadPriority.Lowest;
                displayReportThread.Start();
                updateReportDisplayed = true;
            }
        }

        private void tabUpdateDetailViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabUpdateDetailViewer.SelectedTab == tabUpdateDetailViewer.TabPages["TabReport"])
                ComputeReport();
        }

        private void chkBxShowRowsWithValues_CheckedChanged(object sender, EventArgs e)
        {
            int total = 0;
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (chkBxShowRowsWithValues.Checked)
                {
                    total = (int)row.Cells["installed"].Value +
                        (int)row.Cells["installedPendingReboot"].Value +
                        (int)row.Cells["notInstalled"].Value +
                        (int)row.Cells["downloaded"].Value +
                        (int)row.Cells["notApplicable"].Value +
                        (int)row.Cells["failed"].Value +
                        (int)row.Cells["unknown"].Value;
                    row.Visible = (total != 0);
                }
                else
                    row.Visible = true;
            }
        }

        private void dtGrdVReport_DoubleClick(object sender, EventArgs e)
        {
            cmbBxComputerGroup.SelectedItem = (ComputerGroup)dgvReport.SelectedRows[0].Cells["Groups"].Value;
            tabUpdateDetailViewer.SelectedTab = tabUpdateDetailViewer.TabPages["tabStatus"];
        }

        private void txtBxFolder_DoubleClick(object sender, EventArgs e)
        {
            //   http:// wsus01/Content/4D/421BBF7E360DACA8B302BCFF84EC6486A2206C4D.cab
            if (!string.IsNullOrEmpty(txtBxFolder.Text))
            {
                string udpateFile = GetPathFromUrl(txtBxFolder.Text);
                if(System.IO.File.Exists(udpateFile))
                    System.Diagnostics.Process.Start("explorer.exe", @"/Select, " + udpateFile);
            }
        }

        private void txtBxId_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxId.Text) && IsUpdateServicesPackagesFolderExists(txtBxId.Text))
                System.Diagnostics.Process.Start("explorer.exe", @"\\" + _wsus.Server.Name + @"\UpdateServicesPackages\" + txtBxId.Text);
        }

        #endregion

        #region (Event Delegates - événements)

        public delegate void ApproveUpdateEventHandler(UpdateCollection udpatesToApprove);
        public event ApproveUpdateEventHandler ApproveUpdate;

        public delegate void DeclineUpdateEventHandler(UpdateCollection udpatesToDecline);
        public event DeclineUpdateEventHandler DeclineUpdate;

        public delegate void ExpireUpdateEventHandler(UpdateCollection updatesToExpire);
        public event ExpireUpdateEventHandler ExpireUpdate;

        public delegate void DeleteUpdateEventHandler(UpdateCollection udpatesToDelete);
        public event DeleteUpdateEventHandler DeleteUpdate;

        public delegate void ReviseUpdateEventHandler(IUpdate updateToRevise);
        public event ReviseUpdateEventHandler ReviseUpdate;

        #endregion

    }
}
