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
    internal partial class FrmUpdateWizard : Form
    {
        private Dictionary<string, Company> _companies;
        private FrmUpdateFilesWizard updateFilesWizard = new FrmUpdateFilesWizard();
        private FrmUpdateInformationsWizard updateInformationsWizard;
        private FrmUpdateRulesWizard updateIsInstalledRulesWizard = new FrmUpdateRulesWizard();
        private FrmUpdateRulesWizard updateIsInstallableRulesWizard = new FrmUpdateRulesWizard();
        private SoftwareDistributionPackage _sdp;
        private bool _revising = false;
        private bool _inferFromMsiProperties = true;

        private System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateWizard).Assembly);

        internal FrmUpdateWizard(Dictionary<string, Company> Companies)
        {
            InitializeComponent();
            InitializeComponent(Companies, null, null, null);
        }

        internal FrmUpdateWizard(Dictionary<string, Company> Companies, string updateFile)
        {
            InitializeComponent();
            updateFilesWizard = new FrmUpdateFilesWizard(updateFile);
            InitializeComponent(Companies, null, null, null);
        }

        internal FrmUpdateWizard(Dictionary<string, Company> Companies, Company SelectedCompany, Product SelectedProduct, string updateFile)
        {
            InitializeComponent();
            InferFromMsiProperties = false;
            updateFilesWizard = new FrmUpdateFilesWizard(updateFile);
            InitializeComponent(Companies, SelectedCompany, SelectedProduct, null);
        }

        internal FrmUpdateWizard(Dictionary<string, Company> Companies, Company SelectedCompany, string updateFile)
        {
            InitializeComponent();
            InferFromMsiProperties = false;
            updateFilesWizard = new FrmUpdateFilesWizard(updateFile);
            InitializeComponent(Companies, SelectedCompany, null, null);
        }

        internal FrmUpdateWizard(Dictionary<string, Company> Companies, Company SelectedCompany, Product SelectedProduct)
        {
            InitializeComponent();

            InitializeComponent(Companies, SelectedCompany, SelectedProduct, null);
        }

        internal FrmUpdateWizard(Dictionary<string, Company> Companies, Company SelectedCompany)
        {
            InitializeComponent();

            InitializeComponent(Companies, SelectedCompany, null, null);
        }

        internal FrmUpdateWizard(Dictionary<string, Company> Companies, SoftwareDistributionPackage sdp)
        {
            InitializeComponent();
            Revising = true;
            this.Sdp = sdp;
            this.Companies = Companies;
            InitializeComponent(this.Companies, Companies[sdp.VendorName], Companies[sdp.VendorName].Products[sdp.ProductNames[0]], sdp);

        }

        private void InitializeComponent(Dictionary<string, Company> Companies, Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {

            this.Companies = Companies;
            updateInformationsWizard = new FrmUpdateInformationsWizard(this.Companies, SelectedCompany, SelectedProduct, sdp);

            // UpdateFilesWizard :
            updateFilesWizard.TopLevel = false;
            updateFilesWizard.Controls["btnNext"].Click += new EventHandler(updateFilesWizard_btnNext_Click);
            updateFilesWizard.Controls["btnCancel"].Click += new EventHandler(updateFilesWizard_btnCancel_Click);

            //UpdateInformationsWizard :
            updateInformationsWizard.TopLevel = false;
            updateInformationsWizard.Controls["btnNext"].Click += new EventHandler(updateInformationsWizard_btnNext_Click);
            updateInformationsWizard.Controls["btnCancel"].Click += new EventHandler(updateInformationsWizard_btnCancel_Click);
            updateInformationsWizard.Controls["btnPrevious"].Click += new EventHandler(updateInformationsWizard_btnPrevious_Click);


            //updateIsInstalledRulesWizard :
            updateIsInstalledRulesWizard.TopLevel = false;
            updateIsInstalledRulesWizard.Controls["tableLayoutPanel1"].Controls["btnNext"].Click += new EventHandler(updateIsInstalledRulesWizard_btnNext_Click);
            updateIsInstalledRulesWizard.Controls["tableLayoutPanel1"].Controls["btnCancel"].Click += new EventHandler(updateIsInstalledRulesWizard_btnCancel_Click);
            updateIsInstalledRulesWizard.Controls["tableLayoutPanel1"].Controls["btnPrevious"].Click += new EventHandler(updateIsInstalledRulesWizard_btnPrevious_Click);

            //updateIsInstallableRulesWizard :
            updateIsInstallableRulesWizard.TopLevel = false;
            updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnNext"].Click += new EventHandler(updateIsInstallableRulesWizard_btnNext_Click);
            updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnCancel"].Click += new EventHandler(updateIsInstallableRulesWizard_btnCancel_Click);
            updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnPrevious"].Click += new EventHandler(updateIsInstallableRulesWizard_btnPrevious_Click);

            if (Revising)
                InitializeInformationsWizard();
            else
                InitializeUpdateFilesWizard();
        }

        private void InitializeUpdateFilesWizard()
        {
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionUpdateFileWizard");

            updateFilesWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateFilesWizard);
            updateFilesWizard.Show();
            this.Size = new System.Drawing.Size(updateFilesWizard.Width + 20, txtBxDescription.Height + updateFilesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateFilesWizard.Dock = DockStyle.Fill;
            updateFilesWizard.Select();
        }

        private void updateFilesWizard_btnNext_Click(object sender, EventArgs e)
        {
            updateFilesWizard.Hide();
            if (updateFilesWizard.FileType == FrmUpdateFilesWizard.UpdateType.WindowsInstaller)
            {
                MsiReader.MsiReader msiReader = new MsiReader.MsiReader();
                msiReader.MsiFilePath = updateFilesWizard.UpdateFileName;
                string msiCode = msiReader.GetProductCode();
                string MsiVendorName = msiReader.GetManufacturer();
                string MsiProductName = msiReader.GetProductName();
                if (!string.IsNullOrEmpty(msiCode))
                {
                    updateIsInstalledRulesWizard.InitializeFromXml("<msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/>");
                    updateIsInstallableRulesWizard.InitializeFromXml("<lar:Not><msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/></lar:Not>");
                }
                bool foundVendor = false;
                bool foundProduct = false;

                if (InferFromMsiProperties)
                {
                    foreach (KeyValuePair<string, Company> pair in _companies)
                    {
                        if (pair.Key.ToLower() == MsiVendorName.ToLower())
                        {
                            updateInformationsWizard.InitializeVendorName(pair.Value.CompanyName);
                            foundVendor = true;
                            foreach (KeyValuePair<string, Product> prod in pair.Value.Products)
                            {
                                if (prod.Key.ToLower() == MsiProductName.ToLower())
                                {
                                    updateInformationsWizard.InitializeProductName(prod.Value.ProductName);
                                    foundProduct = true;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (!foundVendor)
                        updateInformationsWizard.InitializeVendorName(MsiVendorName);
                    if (!foundProduct)
                        updateInformationsWizard.InitializeProductName(MsiProductName);
                }

            }
            else
            {
                updateIsInstalledRulesWizard.InitializeFromXml("");
                updateIsInstallableRulesWizard.InitializeFromXml("");
            }
            InitializeInformationsWizard();
        }

        private void updateFilesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            CleanAndClose();
        }

        private void InitializeInformationsWizard()
        {
            if (Revising)
                updateInformationsWizard.Controls["btnPrevious"].Enabled = false;
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionInformationsWizard");

            updateInformationsWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateInformationsWizard);
            updateInformationsWizard.Show();
            this.Size = new System.Drawing.Size(updateInformationsWizard.Width + 20, txtBxDescription.Height + updateInformationsWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateInformationsWizard.Dock = DockStyle.Fill;
            updateInformationsWizard.Select();
        }

        private void updateInformationsWizard_btnNext_Click(object sender, EventArgs e)
        {
            updateInformationsWizard.Hide();
            InitializeUpdateIsInstalledRulesWizard();
        }

        private void updateInformationsWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            updateInformationsWizard.Hide();
            InitializeUpdateFilesWizard();
        }

        private void updateInformationsWizard_btnCancel_Click(object sender, EventArgs e)
        {
            CleanAndClose();
        }

        private void InitializeUpdateIsInstalledRulesWizard()
        {
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionIsInstalledWizard");

            updateIsInstalledRulesWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateIsInstalledRulesWizard);
            if (Revising && !updateIsInstalledRulesWizard.IsAllReadyInitialized)
                updateIsInstalledRulesWizard.InitializeFromXml(Sdp.IsInstalled);
            updateIsInstalledRulesWizard.IsAllReadyInitialized = true;
            updateIsInstalledRulesWizard.Show();
            this.Size = new System.Drawing.Size(updateIsInstalledRulesWizard.Width + 20, txtBxDescription.Height + updateIsInstalledRulesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateIsInstalledRulesWizard.Dock = DockStyle.Fill;
            updateIsInstalledRulesWizard.Select();
        }

        private void updateIsInstalledRulesWizard_btnNext_Click(object sender, EventArgs e)
        {
            updateIsInstalledRulesWizard.Hide();
            InitializeUpdateIsInstallableRulesWizard();
        }

        private void updateIsInstalledRulesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            CleanAndClose();
        }

        private void updateIsInstalledRulesWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            updateIsInstalledRulesWizard.Hide();
            InitializeInformationsWizard();
        }

        private void InitializeUpdateIsInstallableRulesWizard()
        {
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionIsInstallableWizard");

            if (!Revising)
                updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnNext"].Text = resManager.GetString("Publish");
            else
            {
                updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnNext"].Text = resManager.GetString("Revise");
                if (!updateIsInstallableRulesWizard.IsAllReadyInitialized)
                    updateIsInstallableRulesWizard.InitializeFromXml(Sdp.IsInstallable);
            }
            updateIsInstallableRulesWizard.IsAllReadyInitialized = true;
            updateIsInstallableRulesWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateIsInstallableRulesWizard);
            updateIsInstallableRulesWizard.Show();
            this.Size = new System.Drawing.Size(updateIsInstallableRulesWizard.Width + 20, txtBxDescription.Height + updateIsInstallableRulesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateIsInstallableRulesWizard.Dock = DockStyle.Fill;
            updateIsInstallableRulesWizard.Select();
        }

        private void updateIsInstallableRulesWizard_btnNext_Click(object sender, EventArgs e)
        {
            updateIsInstallableRulesWizard.Hide();
            splitContainer1.Panel2.Controls.Clear();
            FrmUpdatePublisher updatePublisher = new FrmUpdatePublisher(updateFilesWizard, updateInformationsWizard, updateIsInstalledRulesWizard, updateIsInstallableRulesWizard);
            updatePublisher.Controls["btnOk"].Click += new EventHandler(this.Close);
            updatePublisher.TopLevel = false;
            splitContainer1.Panel2.Controls.Add(updatePublisher);
            updatePublisher.Show();
            this.Size = new System.Drawing.Size(updateIsInstalledRulesWizard.Width + 20, txtBxDescription.Height + updateIsInstalledRulesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updatePublisher.Dock = DockStyle.Fill;
            updatePublisher.Select();
            if (Revising)
                updatePublisher.Revise(this.Sdp);
            else
                updatePublisher.Publish();
        }

        private void updateIsInstallableRulesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            CleanAndClose();
        }

        private void updateIsInstallableRulesWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            updateIsInstallableRulesWizard.Hide();
            InitializeUpdateIsInstalledRulesWizard();
        }

        private void Close(object sender, EventArgs e)
        {
            base.Close();
        }

        internal Dictionary<string, Company> Companies
        {
            get { return _companies; }
            set { _companies = value; }
        }

        private bool Revising
        {
            get { return _revising; }
            set { _revising = value; }
        }

        private bool InferFromMsiProperties
        {
            get { return _inferFromMsiProperties; }
            set { _inferFromMsiProperties = value; }
        }

        private SoftwareDistributionPackage Sdp
        {
            get { return _sdp; }
            set { _sdp = value; }
        }

        private void FrmUpdateWizard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            CleanAndClose();
        }

        private void CleanAndClose()
        {
            updateFilesWizard.Dispose();
            updateFilesWizard = null;
            if (updateInformationsWizard != null)
                updateInformationsWizard.Dispose();
            updateInformationsWizard = null;
            updateIsInstalledRulesWizard.Dispose();
            updateIsInstalledRulesWizard = null;
            updateIsInstallableRulesWizard.Dispose();
            updateIsInstallableRulesWizard = null;
            this.Close();
        }

    }
}
