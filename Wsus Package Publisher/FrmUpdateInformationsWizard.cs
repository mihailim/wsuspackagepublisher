using System;
using System.Collections.Generic;
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
        private string _description;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateInformationsWizard).Assembly);


        internal FrmUpdateInformationsWizard(Dictionary<string, Company> Companies, Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {
            InitializeComponent();

            InitializeComponent(Companies, SelectedCompany, SelectedProduct, sdp);
        }

        private void InitializeComponent(Dictionary<string, Company> Companies, Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {
            _companies = Companies;
            cmbBxUpdateClassification.Items.AddRange(Enum.GetNames(typeof(PackageUpdateClassification)));
            cmbBxUpdateClassification.SelectedItem = PackageUpdateClassification.Updates.ToString();
            cmbBxImpact.Items.AddRange(Enum.GetNames(typeof(InstallationImpact)));
            cmbBxImpact.SelectedItem = InstallationImpact.Normal.ToString();
            cmbBxRebootBehavior.Items.AddRange(Enum.GetNames(typeof(RebootBehavior)));
            cmbBxRebootBehavior.SelectedItem = RebootBehavior.CanRequestReboot.ToString();
            cmbBxMsrcSeverity.Items.AddRange(Enum.GetNames(typeof(SecurityRating)));
            cmbBxMsrcSeverity.SelectedItem = SecurityRating.None.ToString();
            cmbBxVendorName.Select();
            _description = resMan.GetString("NoDescription");
            foreach (string company in Companies.Keys)
            {
                cmbBxVendorName.Items.Add(company);
            }

            if (SelectedCompany != null)
            {
                cmbBxVendorName.SelectedItem = SelectedCompany.CompanyName;
            }

            if (SelectedProduct != null)
            {
                cmbBxVendorName.SelectedItem = SelectedProduct.Vendor.CompanyName;
                cmbBxProductName.SelectedItem = SelectedProduct.ProductName;
            }

            if (sdp != null)
            {
                txtBxTitle.Text = sdp.Title;
                txtBxDescription.Text = sdp.Description;
                if (sdp.AdditionalInformationUrls != null && sdp.AdditionalInformationUrls.Count != 0)
                    txtBxMoreInfoURL.Text = sdp.AdditionalInformationUrls[0].ToString();
                if (sdp.SupportUrl != null)
                    txtBxSupportURL.Text = sdp.SupportUrl.ToString();
                if (sdp.InstallableItems != null && sdp.InstallableItems.Count != 0)
                {
                    chkBxCanREquestUserInput.Checked = sdp.InstallableItems[0].InstallBehavior.CanRequestUserInput;
                    chkBxRequiresNetworkConnectivity.Checked = sdp.InstallableItems[0].InstallBehavior.RequiresNetworkConnectivity;
                    cmbBxImpact.SelectedItem = sdp.InstallableItems[0].InstallBehavior.Impact.ToString();
                    cmbBxRebootBehavior.SelectedItem = sdp.InstallableItems[0].InstallBehavior.RebootBehavior.ToString();
                }
                cmbBxUpdateClassification.SelectedItem = sdp.PackageUpdateClassification.ToString();
                if (!string.IsNullOrEmpty(sdp.SecurityBulletinId))
                    txtBxSecurityBulletinId.Text = sdp.SecurityBulletinId;
                cmbBxMsrcSeverity.SelectedItem = sdp.SecurityRating;
                if (sdp.CommonVulnerabilitiesIds != null && sdp.CommonVulnerabilitiesIds.Count != 0 && !string.IsNullOrEmpty(sdp.CommonVulnerabilitiesIds[0]))
                    txtBxCVEId.Text = sdp.CommonVulnerabilitiesIds[0];
                if (!string.IsNullOrEmpty(sdp.KnowledgebaseArticleId))
                    txtBxKBArticleId.Text = sdp.KnowledgebaseArticleId;
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

        internal string UrlMoreInfo
        {
            get
            {
                if (ValidateUrl(txtBxMoreInfoURL.Text))
                    return txtBxMoreInfoURL.Text;
                return string.Empty;
            }
            set
            {
                if (ValidateUrl(value))
                    txtBxMoreInfoURL.Text = value;
            }
        }

        internal string UrlSupport
        {
            get
            {
                if (ValidateUrl(txtBxSupportURL.Text))
                    return txtBxSupportURL.Text;
                return string.Empty;
            }
            set
            {
                if (ValidateUrl(value))
                    txtBxSupportURL.Text = value;
            }
        }

        internal bool CanRequestUserInput
        {
            get { return chkBxCanREquestUserInput.Checked; }
            set { chkBxCanREquestUserInput.Checked = value; }
        }

        internal bool CanRequestNetworkConnectivity
        {
            get { return chkBxRequiresNetworkConnectivity.Checked; }
            set { chkBxRequiresNetworkConnectivity.Checked = value; }
        }

        internal PackageUpdateClassification UpdateClassification
        {
            get { return (PackageUpdateClassification)Enum.Parse(typeof(PackageUpdateClassification), cmbBxUpdateClassification.SelectedItem.ToString()); }
            set { cmbBxUpdateClassification.SelectedItem = value; }
        }

        internal InstallationImpact Impact
        {
            get { return (InstallationImpact)Enum.Parse(typeof(InstallationImpact), cmbBxImpact.SelectedItem.ToString()); }
            set { cmbBxImpact.SelectedItem = value; }
        }

        internal RebootBehavior Behavior
        {
            get { return (RebootBehavior)Enum.Parse(typeof(RebootBehavior), cmbBxRebootBehavior.SelectedItem.ToString()); }
            set { cmbBxRebootBehavior.SelectedItem = value; }
        }

        internal string SecurityBulletinId
        {
            get { return txtBxSecurityBulletinId.Text; }
            set { txtBxSecurityBulletinId.Text = value; }
        }

        internal SecurityRating Severity
        {
            get { return (SecurityRating)Enum.Parse(typeof(SecurityRating), cmbBxMsrcSeverity.SelectedItem.ToString()); }
            set { cmbBxMsrcSeverity.SelectedItem = value; }
        }

        internal string Cve
        {
            get { return txtBxCVEId.Text; }
            set { txtBxCVEId.Text = value; }
        }

        internal string KbArticle
        {
            get { return txtBxKBArticleId.Text; }
            set { txtBxKBArticleId.Text = value; }
        }


        private void cmbBxProductName_TextChanged(object sender, EventArgs e)
        {
            ProductName = cmbBxProductName.Text;
        }

        private void txtBxDescription_TextChanged(object sender, EventArgs e)
        {
            if (txtBxDescription.Text.Trim().Length != 0)
                Description = txtBxDescription.Text;
            else
                Description = "Default description";
        }

        private bool ValidateUrl(string URLtoValidate)
        {
            Uri uriResult;
            return System.Uri.TryCreate(URLtoValidate, UriKind.RelativeOrAbsolute, out uriResult);
        }
    }
}
