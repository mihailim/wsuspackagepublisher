using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateWizard : Form
    {
        private Dictionary<string, Company> _companies;
        private Microsoft.UpdateServices.Administration.IUpdateServer _wsus;
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateWizard).Assembly);

        FrmUpdateFilesWizard updateFilesWizard = new FrmUpdateFilesWizard();
        FrmUpdateInformationsWizard updateInformationsWizard;
        FrmUpdateRulesWizard updateIsInstalledRulesWizard = new FrmUpdateRulesWizard();
        FrmUpdateRulesWizard updateIsInstallableRulesWizard = new FrmUpdateRulesWizard();

        internal FrmUpdateWizard(Microsoft.UpdateServices.Administration.IUpdateServer Wsus, Dictionary<string, Company> Companies)
        {
            InitializeComponent();

            this.Companies = Companies;
            _wsus = Wsus;
            updateInformationsWizard = new FrmUpdateInformationsWizard(this.Companies);

            // UpdateFilesWizard :
            updateFilesWizard.TopLevel = false;
            InitializeUpdateFilesWizard();
            updateFilesWizard.Controls["btnNext"].Click += new EventHandler(updateFilesWizard_btnNext_Click);
            updateFilesWizard.Controls["btnCancel"].Click += new EventHandler(updateFilesWizard_btnCancel_Click);

            //UpdateInformationsWizard :
            updateInformationsWizard.TopLevel = false;
            updateInformationsWizard.Controls["btnNext"].Click += new EventHandler(updateInformationsWizard_btnNext_Click);
            updateInformationsWizard.Controls["btnCancel"].Click += new EventHandler(updateInformationsWizard_btnCancel_Click);
            updateInformationsWizard.Controls["btnPrevious"].Click += new EventHandler(updateInformationsWizard_btnPrevious_Click);

            //updateIsInstalledRulesWizard :
            updateIsInstalledRulesWizard.TopLevel = false;
            updateIsInstalledRulesWizard.Controls["btnNext"].Click += new EventHandler(updateIsInstalledRulesWizard_btnNext_Click);
            updateIsInstalledRulesWizard.Controls["btnCancel"].Click += new EventHandler(updateIsInstalledRulesWizard_btnCancel_Click);
            updateIsInstalledRulesWizard.Controls["btnPrevious"].Click += new EventHandler(updateIsInstalledRulesWizard_btnPrevious_Click);

            //updateIsInstallableRulesWizard :
            updateIsInstallableRulesWizard.TopLevel = false;
            updateIsInstallableRulesWizard.Controls["btnNext"].Click += new EventHandler(updateIsInstallableRulesWizard_btnNext_Click);
            updateIsInstallableRulesWizard.Controls["btnCancel"].Click += new EventHandler(updateIsInstallableRulesWizard_btnCancel_Click);
            updateIsInstallableRulesWizard.Controls["btnPrevious"].Click += new EventHandler(updateIsInstallableRulesWizard_btnPrevious_Click);

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
            InitializeInformationsWizard();
        }

        private void updateFilesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            updateFilesWizard.Dispose();
            updateFilesWizard = null;
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void InitializeInformationsWizard()
        {
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
            updateFilesWizard.Dispose();
            updateFilesWizard = null;
            updateInformationsWizard.Dispose();
            updateInformationsWizard = null;
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void InitializeUpdateIsInstalledRulesWizard()
        {
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionInformationsWizard");

            updateIsInstalledRulesWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateIsInstalledRulesWizard);
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
            updateIsInstalledRulesWizard.Dispose();
            updateIsInstalledRulesWizard = null;
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void updateIsInstalledRulesWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            updateIsInstalledRulesWizard.Hide();
            InitializeInformationsWizard();
        }

        private void InitializeUpdateIsInstallableRulesWizard()
        {
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionInformationsWizard");

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
            FrmUpdatePublisher updatePublisher = new FrmUpdatePublisher(_wsus, updateFilesWizard, updateInformationsWizard, updateIsInstalledRulesWizard, updateIsInstallableRulesWizard);
            updatePublisher.TopLevel = false;
            splitContainer1.Panel2.Controls.Add(updatePublisher);
            updatePublisher.Show();
            this.Size = new System.Drawing.Size(updateIsInstalledRulesWizard.Width + 20, txtBxDescription.Height + updateIsInstalledRulesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updatePublisher.Dock = DockStyle.Fill;
            updatePublisher.Select();
            updatePublisher.Publish();

        }

        private void updateIsInstallableRulesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            updateIsInstallableRulesWizard.Dispose();
            updateIsInstallableRulesWizard = null;
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void updateIsInstallableRulesWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            updateIsInstallableRulesWizard.Hide();
            InitializeUpdateIsInstalledRulesWizard();
        }
        
        internal Dictionary<string, Company> Companies
        {
            get { return _companies; }
            set { _companies = value; }
        }

    }
}
