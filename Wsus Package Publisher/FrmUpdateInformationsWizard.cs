using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateInformationsWizard : Form
    {
        private struct SupersedesUpdate
        {
            private IUpdate _update;

            internal SupersedesUpdate(IUpdate update)
            {
                this._update = update;
            }

            internal IUpdate Update
            {
                get { return _update; }
                set { _update = value; }
            }
            public override string ToString()
            {
                if (Update != null)
                    return Update.Title;
                else
                    return string.Empty;
            }
        }

        private struct Prerequisite
        {
            private IUpdate _update;

            internal Prerequisite(IUpdate update)
            {
                this._update = update;
            }

            internal IUpdate Update
            {
                get { return _update; }
                set { _update = value; }
            }
            public override string ToString()
            {
                if (Update != null)
                    return Update.Title;
                else
                    return string.Empty;
            }
        }

        private Dictionary<string, Company> _companies;
        private string _vendorName;
        private string _productName;
        private string _title;
        private string _description;
        private IList<ReturnCode> _returnCodes = new List<ReturnCode>();
        private string _commandLine = "";
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateInformationsWizard).Assembly);

        internal FrmUpdateInformationsWizard(Dictionary<string, Company> Companies, Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {
            InitializeComponent();

            DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)dtgrvReturnCodes.Columns["Result"];
            column.Items.Add(resMan.GetString("Failed"));
            column.Items.Add(resMan.GetString("Succeeded"));
            column.Items.Add(resMan.GetString("Cancelled"));

            _companies = Companies;
            FillPrerequisites();
            InitializeComponent(SelectedCompany, SelectedProduct, sdp);
        }

        private void InitializeComponent(Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {
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
            foreach (string company in _companies.Keys)
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
                if (sdp.InstallableItems != null && sdp.InstallableItems.Count != 0)
                {
                    Type updateType = sdp.InstallableItems[0].GetType();
                    if (updateType == typeof(CommandLineItem))
                    {
                        CommandLine = (sdp.InstallableItems[0] as CommandLineItem).Arguments;
                        ReturnCodes = (sdp.InstallableItems[0] as CommandLineItem).ReturnCodes;
                    }
                    else
                        if (updateType == typeof(WindowsInstallerItem))
                            CommandLine = (sdp.InstallableItems[0] as WindowsInstallerItem).InstallCommandLine;
                        else
                            if (updateType == typeof(WindowsInstallerPatchItem))
                                CommandLine = (sdp.InstallableItems[0] as WindowsInstallerPatchItem).InstallCommandLine;
                }
                foreach (Guid id in sdp.SupersededPackages)
                {
                    for (int i = 0; i < chkCmbBxSupersedes.Items.Count; i++)
                    {
                        SupersedesUpdate supersededUpdate = (SupersedesUpdate)chkCmbBxSupersedes.Items[i];
                        if (supersededUpdate.Update.Id.UpdateId == id)
                        {
                            chkCmbBxSupersedes.SelectItem(i, true);
                            break;
                        }
                    }
                }
                foreach (PrerequisiteGroup prerequisiteGrp in sdp.Prerequisites)
                    foreach (Guid id in prerequisiteGrp.Ids)
                        for (int i = 0; i < chkCmbBxSupersedes.Items.Count; i++)
                        {
                            Prerequisite prerequisiteUpdate = (Prerequisite)chkCmbBxPrerequisites.Items[i];
                            if (prerequisiteUpdate.Update.Id.UpdateId == id)
                            {
                                chkCmbBxPrerequisites.SelectItem(i, true);
                                break;
                            }
                        }
            }
        }

        internal void InitializeVendorName(string vendorName)
        {
            bool found = false;
            foreach (object obj in cmbBxVendorName.Items)
            {
                if (obj.ToString().ToLower() == vendorName.ToLower())
                {
                    cmbBxVendorName.SelectedItem = obj;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                cmbBxVendorName.SelectedIndex = -1;
                cmbBxVendorName.Text = vendorName;
            }
        }

        internal void InitializeProductName(string productName)
        {
            cmbBxProductName.SelectedIndex = -1;
            cmbBxProductName.Text = productName;
        }

        private void ValidateData(bool vendorOrProductChange)
        {
            btnNext.Enabled = false;

            if ((cmbBxVendorName.SelectedIndex != -1 || !string.IsNullOrEmpty(cmbBxVendorName.Text)) && (cmbBxProductName.SelectedIndex != -1 || !string.IsNullOrEmpty(cmbBxProductName.Text)))
            {
                if (vendorOrProductChange)
                    EnableSupersedesControl();
                if (!string.IsNullOrEmpty(txtBxTitle.Text))
                    btnNext.Enabled = true;
            }
        }

        private void EnableSupersedesControl()
        {
            chkCmbBxSupersedes.Enabled = false;
            chkCmbBxSupersedes.ClearItems();
            Product selectedProduct;
            Company selectedCompany;
            if (cmbBxVendorName.SelectedIndex != -1 && _companies.ContainsKey(cmbBxVendorName.SelectedItem.ToString()) && cmbBxProductName.SelectedIndex != -1)
            {
                chkCmbBxSupersedes.Enabled = true;
                selectedCompany = _companies[cmbBxVendorName.SelectedItem.ToString()];
                foreach (KeyValuePair<string, Product> pair in selectedCompany.Products)
                {
                    if (pair.Value.ProductName == cmbBxProductName.SelectedItem.ToString())
                    {
                        selectedProduct = pair.Value;
                        foreach (IUpdate update in selectedProduct.Updates)
                            chkCmbBxSupersedes.AddItem(new SupersedesUpdate(update));
                        break;
                    }
                }
            }
        }

        internal List<Guid> GetSupersedes()
        {
            List<Guid> result = new List<Guid>();

            foreach (SupersedesUpdate supersededUpdate in chkCmbBxSupersedes.SelectedItems)
                result.Add(supersededUpdate.Update.Id.UpdateId);

            return result;
        }

        private void FillPrerequisites()
        {
            chkCmbBxPrerequisites.Enabled = false;
            chkCmbBxPrerequisites.Items.Clear();

            foreach (KeyValuePair<string, Company> companyPair in _companies)
            {
                foreach (KeyValuePair<string, Product> productPair in companyPair.Value.Products)
                {
                    foreach (IUpdate update in productPair.Value.Updates)
                    {
                        chkCmbBxPrerequisites.AddItem(new Prerequisite(update));
                    }
                }
            }
            chkCmbBxPrerequisites.Enabled = true;
        }

        internal List<Guid> GetPrerequisites()
        {
            List<Guid> result = new List<Guid>();

            foreach (Prerequisite prerequisite in chkCmbBxPrerequisites.SelectedItems)
                result.Add(prerequisite.Update.Id.UpdateId);

            return result;
        }

        private InstallationResult GetInstallationResult(string p)
        {
            if (p == resMan.GetString("Failed"))
                return InstallationResult.Failed;
            if (p == resMan.GetString("Succeeded"))
                return InstallationResult.Succeeded;
            if (p == resMan.GetString("Cancelled"))
                return InstallationResult.Cancelled;

            return InstallationResult.Failed;
        }

        private int GetReturnCodeValue(string p)
        {
            int result;
            int.TryParse(p, out result);
            return result;
        }

        internal string CommandLine
        {
            get { return _commandLine; }
            private set
            {
                _commandLine = value;
                txtBxCommandLine.Text = value;
            }
        }

        internal IList<ReturnCode> ReturnCodes
        {
            get
            {
                _returnCodes.Clear();

                foreach (DataGridViewRow row in dtgrvReturnCodes.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        ReturnCode currentReturnCode = new ReturnCode();

                        currentReturnCode.ReturnCodeValue = GetReturnCodeValue(row.Cells["Value"].Value.ToString());
                        currentReturnCode.InstallationResult = GetInstallationResult(row.Cells["Result"].Value.ToString());
                        if (row.Cells["NeedReboot"].Value != null)
                            currentReturnCode.IsRebootRequired = (bool)row.Cells["NeedReboot"].Value;

                        _returnCodes.Add(currentReturnCode);
                    }
                }
                return _returnCodes;
            }
            private set
            {
                foreach (ReturnCode code in value)
                {
                    int rowIndex = dtgrvReturnCodes.Rows.Add();
                    DataGridViewRow row = dtgrvReturnCodes.Rows[rowIndex];
                    row.Cells["Value"].Value = code.ReturnCodeValue.ToString();
                    switch (code.InstallationResult)
                    {
                        case InstallationResult.Cancelled:
                            row.Cells["Result"].Value = resMan.GetString("Cancelled");
                            break;
                        case InstallationResult.Failed:
                            row.Cells["Result"].Value = resMan.GetString("Failed");
                            break;
                        case InstallationResult.Succeeded:
                            row.Cells["Result"].Value = resMan.GetString("Succeeded");
                            break;
                        default:
                            break;
                    }
                    row.Cells["NeedReboot"].Value = code.IsRebootRequired;
                }
            }
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


        private void cmbBxVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxVendorName.SelectedIndex != -1)
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
            ValidateData(true);
        }

        private void cmbBxVendorName_TextChanged(object sender, EventArgs e)
        {
            cmbBxProductName.Text = "";
            cmbBxProductName.Items.Clear();
            VendorName = cmbBxVendorName.Text;
            ValidateData(true);
        }

        private void cmbBxProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBxTitle.Select();
            ProductName = cmbBxProductName.Text;
            ValidateData(true);
        }

        private void cmbBxProductName_TextChanged(object sender, EventArgs e)
        {
            ProductName = cmbBxProductName.Text;
            ValidateData(true);
        }

        private void txtBxDescription_TextChanged(object sender, EventArgs e)
        {
            if (txtBxDescription.Text.Trim().Length != 0)
                Description = txtBxDescription.Text;
            else
                Description = "Default description";
        }

        private void txtBxTitle_TextChanged(object sender, EventArgs e)
        {
            Title = txtBxTitle.Text;
            ValidateData(false);
        }

        private bool ValidateUrl(string URLtoValidate)
        {
            Uri uriResult;
            return System.Uri.TryCreate(URLtoValidate, UriKind.RelativeOrAbsolute, out uriResult);
        }

        private void txtBxCommandLine_TextChanged(object sender, EventArgs e)
        {
            CommandLine = txtBxCommandLine.Text;
        }
    }
}
