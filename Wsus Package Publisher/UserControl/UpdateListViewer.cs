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
        WsusWrapper _wsus;
        Product _product;
        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(UpdateListViewer).Assembly);

        public UpdateListViewer()
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();
            _wsus.UpdateExpired += new WsusWrapper.UpdateExpiredEventHandler(_wsus_UpdateExpired);
            _wsus.UpdateDeleted += new WsusWrapper.UpdateDeletedEventHandler(_wsus_UpdateDeleted);
            _wsus.UpdateDeclined += new WsusWrapper.UpdateDeclinedEventHandler(_wsus_UpdateDeclined);

            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Approve"));
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Revise"));
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Decline"));
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Expire"));
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Delete"));
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Resign"));

            if (_wsus.IsReplica)
                mnuStripUpdateListViewer.Enabled = false;
            else
                mnuStripUpdateListViewer.Enabled = true;
        }

        internal void UpdateDisplay()
        {
            if (ViewedProduct != null)
            {
                ClearDisplay();
                dgvUpdateList.SuspendLayout();

                foreach (IUpdate update in ViewedProduct.Updates)
                {
                    dgvUpdateList.Rows.Add(update.Title, GetUpdateStatus(update), update.ArrivalDate, update.CreationDate, update);
                }
                if (ContentChanged != null)
                    ContentChanged();
                dgvUpdateList.ResumeLayout();
            }
        }

        private string GetUpdateStatus(IUpdate update)
        {
            if (update.IsApproved)
                if (update.IsSuperseded)
                    return resMan.GetString("Approve") + " (" + resMan.GetString("Superseded") + ")";
                else
                    return resMan.GetString("Approve");
            if (update.IsDeclined)
                return resMan.GetString("Declined");
            if (update.IsSuperseded)
                return resMan.GetString("Superseded");
            if (update.PublicationState == PublicationState.Expired)
                return resMan.GetString("Expired");

            return resMan.GetString("NotApproved");
        }

        internal void ClearDisplay()
        {
            dgvUpdateList.Rows.Clear();
            if (ContentChanged != null)
                ContentChanged();
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

        private void dgvUpdateList_SelectionChanged(object sender, EventArgs e)
        {
            if (UpdateSelectionChanged != null)
                UpdateSelectionChanged(dgvUpdateList.SelectedRows);
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

        private void mnuStripUpdateListViewer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Text;

            if (dgvUpdateList.SelectedRows.Count != 0)
            {
                UpdateCollection updates = new UpdateCollection();

                foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
                {
                    updates.Add((IUpdate)row.Cells["UpdateId"].Value);
                }

                if (name == resMan.GetString("Approve"))
                {
                    if (ApproveUpdate != null)
                        ApproveUpdate(updates);
                }

                if (name == resMan.GetString("Revise"))
                {
                    if (ReviseUpdate != null)
                        ReviseUpdate(updates[0]);
                }

                if (name == resMan.GetString("Decline"))
                {
                    if (DeclineUpdate != null)
                        DeclineUpdate(updates);
                }

                if (name == resMan.GetString("Expire"))
                {
                    if (ExpireUpdate != null)
                        ExpireUpdate(updates);
                }

                if (name == resMan.GetString("Delete"))
                {
                    if (DeleteUpdate != null)
                        DeleteUpdate(updates);
                }

                if (name == resMan.GetString("Resign"))
                {
                    if (ResignUpdate != null)
                        ResignUpdate(updates);
                }
            }
        }

        #endregion

        public delegate void UpdateSelectionChangedEventHandler(DataGridViewSelectedRowCollection rowCollection);
        public event UpdateSelectionChangedEventHandler UpdateSelectionChanged;

        public delegate void ContentChangedEventHandler();
        public event ContentChangedEventHandler ContentChanged;

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

        public delegate void ResignUpdateEventHandler(UpdateCollection updateToResign);
        public event ResignUpdateEventHandler ResignUpdate;

    }
}
