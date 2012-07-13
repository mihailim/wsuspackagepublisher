﻿using System;
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

        internal UpdateControl()
        {
            InitializeComponent();
            updateListViewer1.ContentChanged += new UpdateListViewer.ContentChangedEventHandler(AdjustSplitterDistance);
            updateListViewer1.UpdateSelectionChanged += new UpdateListViewer.UpdateSelectionChangedEventHandler(updateListViewer1_UpdateSelectionChanged);
            updateDetailViewer1.ApproveUpdate += new UpdateDetailViewer.ApproveUpdateEventHandler(updateDetailViewer1_ApproveUpdate);
            updateDetailViewer1.DeclineUpdate += new UpdateDetailViewer.DeclineUpdateEventHandler(updateDetailViewer1_DeclineUpdate);
            updateDetailViewer1.DeleteUpdate += new UpdateDetailViewer.DeleteUpdateEventHandler(updateDetailViewer1_DeleteUpdate);
            updateDetailViewer1.ExpireUpdate += new UpdateDetailViewer.ExpireUpdateEventHandler(updateDetailViewer1_ExpireUpdate);
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

        #endregion

        #region (Methods - Méthodes)

        internal void RefreshDisplay()
        {
            updateListViewer1.UpdateDisplay();
        }

        internal void SetComputerGroups(Dictionary<string, Guid> computerGroups)
        {
            updateDetailViewer1.SetComputerGroups(computerGroups);
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

        #region (response to events - réponses aux événements)

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

        void updateDetailViewer1_DeleteUpdate(UpdateCollection updatesToDelete)
        {
            foreach (IUpdate update in updatesToDelete)
            {
                _wsus.DeleteUpdate(update);
            }
        }

        void updateDetailViewer1_DeclineUpdate(UpdateCollection updatesToDecline)
        {
            foreach (IUpdate update in updatesToDecline)
            {
                _wsus.DeclineUpdate(update);
            }
        }

        void updateDetailViewer1_ApproveUpdate(UpdateCollection updatesToApprove)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}