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
        private UpdateCollection _update;
        private WsusWrapper _wsus;
        private Dictionary<string, Guid> _computerGroups;
        private List<UpdateInstallationState> _filter = new List<UpdateInstallationState>();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(UpdateDetailViewer).Assembly);

        public UpdateDetailViewer()
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();

            cmbBxFilter.Items.Add(resMan.GetString("All"));
            cmbBxFilter.Items.Add(resMan.GetString("AllButInstalled"));
            cmbBxFilter.Items.Add(resMan.GetString("Downloaded"));
            cmbBxFilter.Items.Add(resMan.GetString("Failed"));
            cmbBxFilter.Items.Add(resMan.GetString("Installed"));
            cmbBxFilter.Items.Add(resMan.GetString("InstalledPendingReboot"));
            cmbBxFilter.Items.Add(resMan.GetString("NotApplicable"));
            cmbBxFilter.Items.Add(resMan.GetString("NotInstalled"));
            cmbBxFilter.Items.Add(resMan.GetString("Unknown"));
            cmbBxFilter.SelectedIndex = 0;

            LockFunctionnalities(_wsus.IsReplica);
        }

        #region (Methods - Méthodes)

        internal void SetComputerGroups(Dictionary<string, Guid> computerGroups)
        {
            _computerGroups = computerGroups;

            foreach (KeyValuePair<string, Guid> pair in computerGroups)
            {
                cmbBxComputerGroup.Items.Add(pair.Key);
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

                if (updates.Count > 1)
                {
                    btnRevise.Enabled = false;
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

                    if ((update.AdditionalInformationUrls.Count != 0) && (!string.IsNullOrEmpty(update.AdditionalInformationUrls[0].ToString())))
                        txtBxAdditionnalInformationURL.Text = update.AdditionalInformationUrls[0].ToString();
                    if (!string.IsNullOrEmpty(update.Description))
                        txtBxDescription.Text = update.Description;
                }
                // Status Tab

                if (ViewedUpdates.Count == 1)
                {
                    if (cmbBxComputerGroup.SelectedItem != null)
                    {
                        Guid targetGroupId = _computerGroups[cmbBxComputerGroup.SelectedItem.ToString()];
                        UpdateInstallationInfoCollection updateInfo = _wsus.GetUpdateInstallationInfoPerComputerTarget(targetGroupId, ViewedUpdates[0]);

                        foreach (IUpdateInstallationInfo info in updateInfo)
                        {
                            if (_filter.Contains(info.UpdateInstallationState))
                                dgvComputerStatus.Rows.Add(_wsus.GetComputerName(info.ComputerTargetId), resMan.GetString(info.UpdateInstallationState.ToString()), resMan.GetString(info.UpdateApprovalAction.ToString()));
                        }
                    }
                }
            }
        }

        internal void ResetControl()
        {
            _update = null;
            ClearDisplay();
            dgvComputerStatus.Rows.Clear();
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

            dgvComputerStatus.Rows.Clear();
        }

        internal void UpdateSelectionChanged(DataGridViewSelectedRowCollection rows)
        {
            UpdateCollection updates = new UpdateCollection();

            foreach (DataGridViewRow row in rows)
            {
                updates.Add((IUpdate)row.Cells["UpdateId"].Value);
            }

            ViewedUpdates = updates;
        }

        internal void LockFunctionnalities(bool isLock)
        {
            btnApprove.Enabled = !isLock;
            btnDecline.Enabled = !isLock;
            btnDelete.Enabled = !isLock;
            btnExpire.Enabled = !isLock;
            btnRevise.Enabled = !isLock;
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


        #endregion

        #region (Properties - Propriétés)


        internal UpdateCollection ViewedUpdates
        {
            get { return _update; }
            set
            {
                _update = value;
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
            if (ApproveUpdate != null)
                ApproveUpdate(ViewedUpdates);
            btnApprove.Enabled = !_wsus.IsReplica;
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            btnDecline.Enabled = false;
            if (DeclineUpdate != null)
                DeclineUpdate(ViewedUpdates);
            btnDecline.Enabled = !_wsus.IsReplica;
        }

        private void btnExpire_Click(object sender, EventArgs e)
        {
            btnExpire.Enabled = false;
            if (ExpireUpdate != null)
                ExpireUpdate(ViewedUpdates);
            btnExpire.Enabled = !_wsus.IsReplica;
        }

        private void btnRevise_Click(object sender, EventArgs e)
        {
            btnRevise.Enabled = false;
            if (ReviseUpdate != null)
                ReviseUpdate(ViewedUpdates[0]);
            btnRevise.Enabled = !_wsus.IsReplica;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            if (DeleteUpdate != null)
                DeleteUpdate(ViewedUpdates);
            btnDelete.Enabled = !_wsus.IsReplica;
        }

        private void cmbBxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _filter.Clear();
            switch (cmbBxFilter.SelectedIndex)
            {
                case 0:
                    _filter.Add(UpdateInstallationState.Downloaded);
                    _filter.Add(UpdateInstallationState.Failed);
                    _filter.Add(UpdateInstallationState.Installed);
                    _filter.Add(UpdateInstallationState.InstalledPendingReboot);
                    _filter.Add(UpdateInstallationState.NotApplicable);
                    _filter.Add(UpdateInstallationState.NotInstalled);
                    _filter.Add(UpdateInstallationState.Unknown);
                    break;
                case 1:
                    _filter.Add(UpdateInstallationState.Downloaded);
                    _filter.Add(UpdateInstallationState.Failed);
                    _filter.Add(UpdateInstallationState.NotInstalled);
                    break;
                case 2:
                    _filter.Add(UpdateInstallationState.Downloaded);
                    break;
                case 3:
                    _filter.Add(UpdateInstallationState.Failed);
                    break;
                case 4:
                    _filter.Add(UpdateInstallationState.Installed);
                    break;
                case 5:
                    _filter.Add(UpdateInstallationState.InstalledPendingReboot);
                    break;
                case 6:
                    _filter.Add(UpdateInstallationState.NotApplicable);
                    break;
                case 7:
                    _filter.Add(UpdateInstallationState.NotInstalled);
                    break;
                case 8:
                    _filter.Add(UpdateInstallationState.Unknown);
                    break;
                default:
                    _filter.Add(UpdateInstallationState.Unknown);
                    break;
            }
            DisplayUpdates(ViewedUpdates);
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
