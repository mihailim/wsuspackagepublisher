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
    internal partial class UpdateDetailViewer : UserControl
    {
        private IUpdate _update;
        private FrmWsusPackagePublisher _wsusPackagePublisher;
        private UpdateListViewer _updateListViewer;

        internal UpdateDetailViewer(FrmWsusPackagePublisher wsusPackagePublisher, UpdateListViewer updateListViewer)
        {
            InitializeComponent();
            _wsusPackagePublisher = wsusPackagePublisher;
            _updateListViewer = updateListViewer;

            foreach (string group in _wsusPackagePublisher.ComputerGroups.Keys)
            {
                cmbBxComputerGroup.Items.Add(group);
            }
        }

        internal void DisplayUpdate(IUpdate update)
        {
            // Informations Tab

            if ((update.CompanyTitles.Count != 0) && (!string.IsNullOrEmpty(update.CompanyTitles[0])))
                txtBxCompany.Text = update.CompanyTitles[0].ToString();
            if ((update.ProductTitles.Count != 0) && (!string.IsNullOrEmpty(update.ProductTitles[0])))
                txtBxProductTitle.Text = update.ProductTitles[0].ToString();
            if (!string.IsNullOrEmpty(update.Title))
                txtBxTitle.Text = update.Title;
            if (update.CreationDate != null)
                txtBxCreationDate.Text = update.CreationDate.ToString();
            if (update.ArrivalDate != null)
                txtBxArrivalDate.Text = update.ArrivalDate.ToString();
            chkBxIsApproved.Checked = update.IsApproved;
            chkBxIsExpired.Checked = update.IsDeclined;
            btnDecline.Enabled = !update.IsDeclined;
            if ((update.AdditionalInformationUrls.Count != 0) && (!string.IsNullOrEmpty(update.AdditionalInformationUrls[0].ToString())))
                txtBxAdditionnalInformationURL.Text = update.AdditionalInformationUrls[0].ToString();
            if (!string.IsNullOrEmpty(update.Description))
                txtBxDescription.Text = update.Description;

            // Status Tab

            dgvComputerStatus.Rows.Clear();
            if (cmbBxComputerGroup.SelectedItem != null)
            {
                IComputerTargetGroup targetGroup = _wsusPackagePublisher.GetTargetGroup(cmbBxComputerGroup.SelectedItem.ToString());
                UpdateInstallationInfoCollection updateInfo = targetGroup.GetUpdateInstallationInfoPerComputerTarget(ViewedUpdate);
                
                foreach (IUpdateInstallationInfo info in updateInfo)
                {
                    dgvComputerStatus.Rows.Add(_wsusPackagePublisher.GetComputerName(info.ComputerTargetId), info.UpdateInstallationState.ToString(), info.UpdateApprovalAction.ToString());
                }
            }
        }

        internal void ClearDisplay()
        {
            _update = null;

            txtBxCompany.Text = "";
            txtBxProductTitle.Text = "";
            txtBxTitle.Text = "";
            txtBxCreationDate.Text = "";
            txtBxArrivalDate.Text = "";
            txtBxAdditionnalInformationURL.Text = "";
            txtBxDescription.Text = "";

            dgvComputerStatus.Rows.Clear();
        }

        internal IUpdate ViewedUpdate
        {
            get { return _update; }
            set
            {
                _update = value;
                DisplayUpdate(ViewedUpdate);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _wsusPackagePublisher.DeleteUpdate(ViewedUpdate.Id.UpdateId, _updateListViewer.ViewedCompany, _updateListViewer.ViewedProduct);
            ClearDisplay();
            _updateListViewer.Display();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            _wsusPackagePublisher.Approve(ViewedUpdate);
        }

        private void cmbBxComputerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUpdate(ViewedUpdate);
        }

    }
}
