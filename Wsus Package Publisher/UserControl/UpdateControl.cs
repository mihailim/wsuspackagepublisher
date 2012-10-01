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
    internal partial class UpdateControl : UserControl
    {
        WsusWrapper _wsus = WsusWrapper.GetInstance();
        Product _product;
        Dictionary<string, Guid> _computerGroups;

        internal UpdateControl()
        {
            InitializeComponent();
            updateListViewer1.ContentChanged += new UpdateListViewer.ContentChangedEventHandler(AdjustSplitterDistance);
            updateListViewer1.UpdateSelectionChanged += new UpdateListViewer.UpdateSelectionChangedEventHandler(updateListViewer1_UpdateSelectionChanged);
            updateListViewer1.ApproveUpdate += new UpdateListViewer.ApproveUpdateEventHandler(updateDetailViewer1_ApproveUpdate);
            updateListViewer1.DeclineUpdate += new UpdateListViewer.DeclineUpdateEventHandler(updateDetailViewer1_DeclineUpdate);
            updateListViewer1.DeleteUpdate += new UpdateListViewer.DeleteUpdateEventHandler(updateDetailViewer1_DeleteUpdate);
            updateListViewer1.ExpireUpdate += new UpdateListViewer.ExpireUpdateEventHandler(updateDetailViewer1_ExpireUpdate);
            updateListViewer1.ReviseUpdate += new UpdateListViewer.ReviseUpdateEventHandler(updateDetailViewer1_ReviseUpdate);
            updateListViewer1.ResignUpdate += new UpdateListViewer.ResignUpdateEventHandler(updateListViewer1_ResignUpdate);

            updateDetailViewer1.ApproveUpdate += new UpdateDetailViewer.ApproveUpdateEventHandler(updateDetailViewer1_ApproveUpdate);
            updateDetailViewer1.DeclineUpdate += new UpdateDetailViewer.DeclineUpdateEventHandler(updateDetailViewer1_DeclineUpdate);
            updateDetailViewer1.DeleteUpdate += new UpdateDetailViewer.DeleteUpdateEventHandler(updateDetailViewer1_DeleteUpdate);
            updateDetailViewer1.ExpireUpdate += new UpdateDetailViewer.ExpireUpdateEventHandler(updateDetailViewer1_ExpireUpdate);
            updateDetailViewer1.ReviseUpdate += new UpdateDetailViewer.ReviseUpdateEventHandler(updateDetailViewer1_ReviseUpdate);
        }

        void updateListViewer1_DeclineUpdate(UpdateCollection udpatesToDecline)
        {
            throw new NotImplementedException();
        }

        #region (Properties - Propriétés)

        /// <summary>
        /// Get or Set the product for which updates are display.
        /// </summary>
        internal Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                updateListViewer1.ViewedProduct = value;
            }
        }

        public Dictionary<string, Company> Companies { get; set; }

        #endregion

        #region (Methods - Méthodes)

        internal void RefreshDisplay()
        {
            updateListViewer1.UpdateDisplay();
        }

        internal void SetComputerGroups(Dictionary<string, Guid> computerGroups)
        {
            _computerGroups = computerGroups;
            updateDetailViewer1.SetComputerGroups(computerGroups);
        }

        internal void LockFunctionnalities(bool isLock)
        {
            updateDetailViewer1.LockFunctionnalities(isLock);
        }

        private void AdjustSplitterDistance()
        {
            int height = updateListViewer1.DataGridViewHeight;

            if (height < (splitContainer1.Height / 2))
                splitContainer1.SplitterDistance = (int)(height + 10);
            else
                splitContainer1.SplitterDistance = (int)(splitContainer1.Height / 2);
        }

        #endregion

        #region (responses to events - réponses aux événements)

        private void updateListViewer1_UpdateSelectionChanged(DataGridViewSelectedRowCollection selectedRows)
        {
            updateDetailViewer1.UpdateSelectionChanged(selectedRows);
        }

        private void updateDetailViewer1_ExpireUpdate(UpdateCollection updatesToExpire)
        {
            foreach (IUpdate update in updatesToExpire)
            {
                _wsus.ExpireUpdate(update);
            }
        }

        private void updateDetailViewer1_DeleteUpdate(UpdateCollection updatesToDelete)
        {
            foreach (IUpdate update in updatesToDelete)
            {
                _wsus.DeleteUpdate(update);
            }
        }

        private void updateDetailViewer1_DeclineUpdate(UpdateCollection updatesToDecline)
        {
            foreach (IUpdate update in updatesToDecline)
            {
                _wsus.DeclineUpdate(update);
            }
        }

        private void updateDetailViewer1_ApproveUpdate(UpdateCollection updatesToApprove)
        {
            FrmApprovalSet approvalForm = new FrmApprovalSet(_computerGroups, updatesToApprove);

            approvalForm.ShowDialog(this);
            if (approvalForm.DialogResult == DialogResult.OK)
            {
                foreach (IUpdate updateToApprove in updateDetailViewer1.ViewedUpdates)
                {
                    foreach (ApprovalObject approval in approvalForm.Approvals)
                    {
                        switch (approval.Approval)
                        {
                            case ApprovalObject.Approvals.ApproveForInstallation:
                                if (approval.HasDeadLine && !updateToApprove.InstallationBehavior.CanRequestUserInput)
                                    _wsus.ApproveUpdateForInstallation(approval.GroupId, updateToApprove, approval.DeadLine);
                                else
                                    _wsus.ApproveUpdateForInstallation(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.ApproveForOptionalInstallation:
                                _wsus.ApproveUpdateForOptionalInstallation(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.ApproveForUninstallation:
                                if (approval.HasDeadLine && !updateToApprove.InstallationBehavior.CanRequestUserInput)
                                    _wsus.ApproveUpdateForUninstallation(approval.GroupId, updateToApprove, approval.DeadLine);
                                else
                                    _wsus.ApproveUpdateForUninstallation(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.NotApproved:
                                _wsus.DisapproveUpdate(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.Unchanged:
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
        }

        private void updateDetailViewer1_ReviseUpdate(IUpdate updateToRevise)
        {
            FrmUpdateWizard updateWizard = new FrmUpdateWizard(Companies, _wsus.GetMetaData(updateToRevise));
            updateWizard.ShowDialog();
        }

        private void updateListViewer1_ResignUpdate(UpdateCollection updates)
        {
            foreach (IUpdate updateToResign in updates)
            {
                MessageBox.Show(_wsus.ResignPackage(updateToResign));
            }
        }

        #endregion

    }
}
