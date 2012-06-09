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
        UpdateDetailViewer _detailViewer;
        Company _company;
        Product _product;
       
        internal UpdateListViewer(FrmWsusPackagePublisher wsusPackagePublisher, Company company, Product product)
        {
            InitializeComponent();

            _company = company;
            _product = product;
            _detailViewer = new UpdateDetailViewer(wsusPackagePublisher, this);
            wsusPackagePublisher.splitContainer2.Panel2.Controls.Clear();
            wsusPackagePublisher.splitContainer2.Panel2.Controls.Add(_detailViewer);
            _detailViewer.Dock = DockStyle.Fill;
            Display();
        }

        internal void Display()
        {
            dgvUpdateList.SuspendLayout();

            dgvUpdateList.Rows.Clear();

            foreach (IUpdate update in ViewedProduct.Updates)
            {
                dgvUpdateList.Rows.Add(update.Title, update.ArrivalDate, update.CreationDate, update);
            }

            dgvUpdateList.ResumeLayout();
        }

        private void dgvUpdateList_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgvUpdateList.SelectedRows;

            if (rows.Count == 1)
            {
                _detailViewer.ViewedUpdate = (IUpdate)rows[0].Cells["UpdateId"].Value;
            }
        }

        internal Company ViewedCompany
        {
            get { return _company; }
        }

        internal Product ViewedProduct
        {
            get { return _product; }
        }
    }
}
