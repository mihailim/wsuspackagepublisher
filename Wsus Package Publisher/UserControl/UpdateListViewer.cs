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
    public partial class UpdateListViewer : UserControl
    {
        private WsusWrapper _wsus;
        private List<MetaGroup> _metaGroups;
        private Product _product;
        private bool _populateDGV = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(UpdateListViewer).Assembly);

        public UpdateListViewer()
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();
            _wsus.UpdateExpired += new WsusWrapper.UpdateExpiredEventHandler(_wsus_UpdateExpired);
            _wsus.UpdateDeleted += new WsusWrapper.UpdateDeletedEventHandler(_wsus_UpdateDeleted);
            _wsus.UpdateDeclined += new WsusWrapper.UpdateDeclinedEventHandler(_wsus_UpdateDeclined);
            _wsus.UpdateApprovalChange += new WsusWrapper.UpdateApprovalChangeEventHandler(_wsus_UpdateApprovalChange);
        }

        private ToolStripMenuItem GetItem(string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Name = text;
            return item;
        }

        private bool HasSomeApprove(UpdateCollection updates)
        {
            bool result = false;
            foreach (IUpdate update in updates)
            {
                if (update.IsApproved)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void _wsus_UpdateApprovalChange(IUpdate ApprovedUpdate)
        {
            ViewedProduct.RefreshUpdate(ApprovedUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value).Id.UpdateId == ApprovedUpdate.Id.UpdateId)
                {
                    row.Cells["UpdateId"].Value = ApprovedUpdate;
                    dgvUpdateList_SelectionChanged(null, null);
                    break;
                }
            }
        }

        internal void UpdateDisplay()
        {
            if (ViewedProduct != null)
            {
                _populateDGV = true;
                ClearDisplay();
                dgvUpdateList.SuspendLayout();

                foreach (IUpdate update in ViewedProduct.Updates)
                {
                    dgvUpdateList.Rows.Add(update.Title, GetUpdateStatus(update), update.ArrivalDate, update.CreationDate, update);
                }
                if (ContentChanged != null)
                    ContentChanged();
                dgvUpdateList.ResumeLayout();
                _populateDGV = false;
                dgvUpdateList_SelectionChanged(null, null);
            }
        }

        private string GetUpdateStatus(IUpdate update)
        {
            if (update.IsApproved)
                if (update.IsSuperseded)
                    return resMan.GetString("Approved") + " (" + resMan.GetString("Superseded") + ")";
                else
                    return resMan.GetString("Approved");
            if (update.IsDeclined && update.PublicationState == PublicationState.Expired)
                return resMan.GetString("Declined") + " (" + resMan.GetString("Expired") + ")";
            if (update.IsDeclined)
                return resMan.GetString("Declined");
            if (update.IsSuperseded)
                return resMan.GetString("Superseded");
            if (update.PublicationState == PublicationState.Expired)
                return resMan.GetString("Expired");

            return resMan.GetString("NotApproved");
        }

        internal void LockFunctionnalities(bool isLock)
        {
            foreach (ToolStripItem item in mnuStripUpdateListViewer.Items)
            {
                if (!isLock && item.Name == resMan.GetString("QuickApproval"))
                    item.Enabled = (_metaGroups.Count != 0);
                else
                    if (item.Name == resMan.GetString("Delete"))
                        item.Enabled = true;
                    else
                        item.Enabled = !isLock;
            }
        }

        internal void ClearDisplay()
        {
            dgvUpdateList.Rows.Clear();
            if (ContentChanged != null)
                ContentChanged();
        }

        internal void RunningLongOperation(bool running)
        {
            if (running)
                this.Cursor = Cursors.WaitCursor;
            else
                this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Get a collection of updates selected in the control.
        /// </summary>
        /// <returns>Collection of IUpdate. May be empty if no update are selected.</returns>
        internal UpdateCollection GetSelectedUpdates()
        {
            UpdateCollection updates = new UpdateCollection();

            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                updates.Add((IUpdate)row.Cells["UpdateId"].Value);
            }

            return updates;
        }

        /// <summary>
        /// Get a collection of updates displayed by the control.
        /// </summary>
        /// <returns>Collection of IUpdate. May be empty if no update are displayed.</returns>
        internal UpdateCollection GetDisplayedUpdates()
        {
            UpdateCollection updates = new UpdateCollection();

            foreach (DataGridViewRow row in dgvUpdateList.Rows)
            {
                updates.Add((IUpdate)row.Cells["UpdateId"].Value);
            }

            return updates;
        }

        internal Product ViewedProduct
        {
            get { return _product; }
            set
            {
                _product = value;
                ClearDisplay();
                UpdateDisplay();
            }
        }

        internal int DataGridViewHeight
        {
            get
            {
                int height = 0;

                height += dgvUpdateList.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dgvUpdateList.Rows)
                {
                    height += row.Height;
                }

                return height;
            }
        }

        internal void SetMetaGroups(List<MetaGroup> metaGroups)
        {
            _metaGroups = metaGroups;

            mnuStripUpdateListViewer.Items.Clear();
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Approve")));
            ToolStripMenuItem quickApprovalItem = GetItem(resMan.GetString("QuickApproval"));
            foreach (MetaGroup metaGroup in metaGroups)
            {
                ToolStripMenuItem metaGroupItem = GetItem(metaGroup.Name);
                metaGroupItem.Click += new EventHandler(metaGroupItem_Click);
                quickApprovalItem.DropDownItems.Add(metaGroupItem);
            }
            quickApprovalItem.Enabled = (metaGroups.Count != 0);
            mnuStripUpdateListViewer.Items.Add(quickApprovalItem);
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Revise")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Decline")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Expire")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Delete")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Resign")));

            LockFunctionnalities(_wsus.IsReplica);
        }

        #region (Responses to Events - Réponses aux événements)

        private void _wsus_UpdateExpired(IUpdate expiredUpdate)
        {
            ViewedProduct.RefreshUpdate(expiredUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value).Id.UpdateId == expiredUpdate.Id.UpdateId)
                {
                    row.Cells["UpdateId"].Value = expiredUpdate;
                    dgvUpdateList_SelectionChanged(null, null);
                    break;
                }
            }
        }

        private void _wsus_UpdateDeleted(IUpdate deletedUpdate)
        {
            ViewedProduct.RemoveUpdate(deletedUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value) == deletedUpdate)
                {
                    dgvUpdateList.Rows.Remove(row);
                    if (ContentChanged != null)
                        ContentChanged();
                    break;
                }
            }
        }

        private void _wsus_UpdateDeclined(IUpdate declinedUpdate)
        {
            ViewedProduct.RefreshUpdate(declinedUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value).Id.UpdateId == declinedUpdate.Id.UpdateId)
                {
                    row.Cells["UpdateId"].Value = declinedUpdate;
                    dgvUpdateList_SelectionChanged(null, null);
                    break;
                }
            }
        }

        private void dgvUpdateList_Click(object sender, EventArgs e)
        {
            MouseEventArgs clickEvent = (MouseEventArgs)e;

            if (clickEvent.Button == System.Windows.Forms.MouseButtons.Right)
            {
                mnuStripUpdateListViewer.Show((Control)sender, clickEvent.X, clickEvent.Y);
            }
        }

        private void dgvUpdateList_SelectionChanged(object sender, EventArgs e)
        {
            UpdateCollection selectedUpdates = new UpdateCollection();

            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                selectedUpdates.Add((IUpdate)row.Cells["UpdateId"].Value);
            }

            mnuStripUpdateListViewer.Items[resMan.GetString("Revise")].Enabled = ((dgvUpdateList.SelectedRows.Count < 2) && (!_wsus.IsReplica));
            mnuStripUpdateListViewer.Items[resMan.GetString("Delete")].Enabled = !HasSomeApprove(selectedUpdates);
            if (UpdateSelectionChanged != null && !_populateDGV)
                UpdateSelectionChanged(dgvUpdateList.SelectedRows);
        }

        private void mnuStripUpdateListViewer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string command = e.ClickedItem.Text;
            mnuStripUpdateListViewer.Hide();
            this.Cursor = Cursors.WaitCursor;

            if (dgvUpdateList.SelectedRows.Count != 0)
            {
                UpdateCollection updates = new UpdateCollection();

                foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
                {
                    updates.Add((IUpdate)row.Cells["UpdateId"].Value);
                }

                if (command == resMan.GetString("Approve"))
                {
                    if (ApproveUpdate != null)
                        ApproveUpdate(updates);
                }

                if (command == resMan.GetString("Revise"))
                {
                    if (ReviseUpdate != null)
                        ReviseUpdate(updates[0]);
                }

                if (command == resMan.GetString("Decline"))
                {
                    if (DeclineUpdate != null)
                        DeclineUpdate(updates);
                }

                if (command == resMan.GetString("Expire"))
                {
                    if (ExpireUpdate != null)
                        ExpireUpdate(updates);
                }

                if (command == resMan.GetString("Delete"))
                {
                    if (DeleteUpdate != null)
                        DeleteUpdate(updates);
                }

                if (command == resMan.GetString("Resign"))
                {
                    if (ResignUpdate != null)
                        ResignUpdate(updates);
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void metaGroupItem_Click(object sender, EventArgs e)
        {
            string metaGroupName = sender.ToString();
            MetaGroup selectedMetaGroup = null;
            mnuStripUpdateListViewer.Hide();
            this.Cursor = Cursors.WaitCursor;

            foreach (MetaGroup metaGroup in _metaGroups)
            {
                if (metaGroup.Name == metaGroupName)
                {
                    selectedMetaGroup = metaGroup;
                    break;
                }
            }
            if (dgvUpdateList.SelectedRows.Count != 0)
            {
                UpdateCollection updates = new UpdateCollection();

                foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
                {
                    updates.Add((IUpdate)row.Cells["UpdateId"].Value);
                }
                if (QuicklyApproveUpdate != null)
                    QuicklyApproveUpdate(updates, selectedMetaGroup);
            }
        }


        #endregion

        public delegate void UpdateSelectionChangedEventHandler(DataGridViewSelectedRowCollection rowCollection);
        public event UpdateSelectionChangedEventHandler UpdateSelectionChanged;

        public delegate void ContentChangedEventHandler();
        public event ContentChangedEventHandler ContentChanged;

        public delegate void ApproveUpdateEventHandler(UpdateCollection udpatesToApprove);
        public event ApproveUpdateEventHandler ApproveUpdate;

        internal delegate void QuicklyApproveUpdateEventHandler(UpdateCollection udpatesToApprove, MetaGroup metaGroup);
        internal event QuicklyApproveUpdateEventHandler QuicklyApproveUpdate;

        public delegate void DeclineUpdateEventHandler(UpdateCollection udpatesToDecline);
        public event DeclineUpdateEventHandler DeclineUpdate;

        public delegate void ExpireUpdateEventHandler(UpdateCollection updatesToExpire);
        public event ExpireUpdateEventHandler ExpireUpdate;

        public delegate void DeleteUpdateEventHandler(UpdateCollection udpatesToDelete);
        public event DeleteUpdateEventHandler DeleteUpdate;

        public delegate void ReviseUpdateEventHandler(IUpdate updateToRevise);
        public event ReviseUpdateEventHandler ReviseUpdate;

        public delegate void ResignUpdateEventHandler(UpdateCollection updateToResign);
        public event ResignUpdateEventHandler ResignUpdate;

    }
}
