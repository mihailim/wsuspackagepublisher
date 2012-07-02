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
    internal partial class UpdateListViewer : UserControl
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
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Decline"));
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Expire"));
            mnuStripUpdateListViewer.Items.Add(resMan.GetString("Delete"));
        }

        internal void UpdateDisplay()
        {
            if (ViewedProduct != null)
            {
                UpdateCollection updateCollection = GetDisplayedUpdates();
                bool changed = false;
                dgvUpdateList.SuspendLayout();

                foreach (IUpdate update in ViewedProduct.Updates)
                {
                    if (!updateCollection.Contains(update))
                    {
                        dgvUpdateList.Rows.Add(update.Title, update.ArrivalDate, update.CreationDate, update);
                        changed = true;
                    }
                }
                if (changed && ContentChanged != null)
                    ContentChanged();
                dgvUpdateList.ResumeLayout();
            }
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
            //string name = e.ClickedItem.Text;

            //if (name == resMan.GetString("Decline"))
            //{
            //    DeclineSelectedUpdate();
            //}
            //else
            //    if (name == resMan.GetString("Expire"))
            //    {
            //        ExpireSelectedUpdate();
            //    }
            //    else
            //        if (name == resMan.GetString("Delete"))
            //        {
            //            DeleteSelectedUpdate();
            //        }
        }

        #endregion

        public delegate void UpdateSelectionChangedEventHandler(DataGridViewSelectedRowCollection rowCollection);
        public event UpdateSelectionChangedEventHandler UpdateSelectionChanged;

        public delegate void ContentChangedEventHandler();
        public event ContentChangedEventHandler ContentChanged;

    }
}
