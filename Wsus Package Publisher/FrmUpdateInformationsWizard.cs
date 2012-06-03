using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateInformationsWizard : Form
    {
        private Dictionary<string, Company> _companies;
        private string _vendorName;
        private string _productName;
        private string _title;
        private string _description = "Description par défaut";


        internal FrmUpdateInformationsWizard(Dictionary<string, Company> Companies)
        {
            InitializeComponent();

            _companies = Companies;
            cmbBxUpdateClassification.Items.AddRange( Enum.GetNames(typeof(PackageUpdateClassification)));
            cmbBxUpdateClassification.SelectedItem = PackageUpdateClassification.Updates.ToString();
            cmbBxImpact.Items.AddRange(Enum.GetNames(typeof(InstallationImpact)));
            cmbBxImpact.SelectedItem = InstallationImpact.Normal.ToString();
            cmbBxRebootBehavior.Items.AddRange(Enum.GetNames(typeof(RebootBehavior)));
            cmbBxRebootBehavior.SelectedItem = RebootBehavior.CanRequestReboot.ToString();
            cmbBxMsrcSeverity.Items.AddRange(Enum.GetNames(typeof(MsrcSeverity)));
            cmbBxMsrcSeverity.SelectedItem = MsrcSeverity.Unspecified.ToString();
            cmbBxVendorName.Select();

            foreach (string company in Companies.Keys)
            {
                cmbBxVendorName.Items.Add(company);
            }
        }

        private void cmbBxVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vendorName = cmbBxVendorName.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(vendorName))
            {
                Company selectedCompany = _companies[vendorName];

                VendorName = vendorName;
                cmbBxProductName.Items.Clear();
                foreach (Product product in selectedCompany.Products.Values)
                {
                    cmbBxProductName.Items.Add(product.ProductName);
                }
                cmbBxProductName.Select();
            }
        }

        private void cmbBxVendorName_TextChanged(object sender, EventArgs e)
        {
            cmbBxProductName.Text = "";
            cmbBxProductName.Items.Clear();
            VendorName = cmbBxVendorName.Text;
        }

        private void cmbBxProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBxTitle.Select();
            ProductName = cmbBxProductName.Text;
        }

        private void cmbBxVendorName_Leave(object sender, EventArgs e)
        {
            btnNext.Enabled = false;

            if ((cmbBxVendorName.SelectedIndex != -1 || !string.IsNullOrEmpty(cmbBxVendorName.Text)) && (cmbBxProductName.SelectedIndex != -1 || !string.IsNullOrEmpty(cmbBxProductName.Text))
                && !string.IsNullOrEmpty(txtBxTitle.Text))
                btnNext.Enabled = true;
        }

        private void txtBxTitle_TextChanged(object sender, EventArgs e)
        {
            Title = txtBxTitle.Text;

            if (!string.IsNullOrEmpty(txtBxTitle.Text))
                btnNext.Enabled = true;
            else
                btnNext.Enabled = false;
        }

        internal string VendorName
        {
            get { return _vendorName; }
            private set { _vendorName = value; }
        }

        internal string ProductName
        {
            get { return _productName; }
            private set { _productName = value; }
        }

        internal string Title
        {
            get { return _title; }
            private set { _title = value; }
        }

        internal string Description
        {
            get { return _description; }
            private set { _description = value; }
        }

        private void cmbBxProductName_TextChanged(object sender, EventArgs e)
        {
            ProductName = cmbBxProductName.Text;
        }

        private void txtBxDescription_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxDescription.Text))
            Description = txtBxDescription.Text;
            else
                Description = "Description par défaut";
        }

    }
}
