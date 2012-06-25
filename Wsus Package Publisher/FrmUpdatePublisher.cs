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
    internal partial class FrmUpdatePublisher : Form
    {
        private IUpdateServer _wsus;
        private FrmUpdateFilesWizard _filesWizard;
        private FrmUpdateInformationsWizard _informationsWizard;
        private FrmUpdateRulesWizard _isInstalledRulesWizard;
        private FrmUpdateRulesWizard _isInstallableRulesWizard;

        internal FrmUpdatePublisher(IUpdateServer Wsus, FrmUpdateFilesWizard FilesWizard, FrmUpdateInformationsWizard InformationsWizard, FrmUpdateRulesWizard IsInstalledRulesWizard, FrmUpdateRulesWizard IsInstallableRulesWizard)
        {
            InitializeComponent();
            _wsus = Wsus;
            _filesWizard = FilesWizard;
            _informationsWizard = InformationsWizard;
            _isInstalledRulesWizard = IsInstalledRulesWizard;
            _isInstallableRulesWizard = IsInstallableRulesWizard;
        }

        internal void Publish()
        {
            System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdatePublisher).Assembly);
            SoftwareDistributionPackage sdp = new SoftwareDistributionPackage();
            string tmpFolderPath;            

            prgBrPublishing.Value = 0;

            switch (_filesWizard.FileType)
            {
                case FrmUpdateFilesWizard.UpdateType.WindowsInstaller:
                    sdp.PopulatePackageFromWindowsInstaller(_filesWizard.updateFileName);
                    break;
                case FrmUpdateFilesWizard.UpdateType.WindowsInstallerPatch:
                    sdp.PopulatePackageFromWindowsInstallerPatch(_filesWizard.updateFileName);
                    break;
                case FrmUpdateFilesWizard.UpdateType.Executable:
                    sdp.PopulatePackageFromExe(_filesWizard.updateFileName);
                    break;
                default:
                    break;
            }

            sdp.Title = _informationsWizard.Title;
            sdp.Description = _informationsWizard.Description;
            sdp.VendorName = _informationsWizard.VendorName;
            sdp.ProductNames.Add(_informationsWizard.ProductName);
            sdp.IsInstalled = _isInstalledRulesWizard.GetXmlFormattedRule();
            sdp.IsInstallable = _isInstallableRulesWizard.GetXmlFormattedRule();
            tmpFolderPath = Environment.GetEnvironmentVariable("Temp") + "\\Wsus Package Publisher\\";
            if (!System.IO.Directory.Exists(tmpFolderPath))
                System.IO.Directory.CreateDirectory(tmpFolderPath);
            if (System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.Delete(tmpFolderPath + sdp.PackageId);
            System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
            System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");
            System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Bin\\");
            
            System.IO.FileInfo updateFile = new System.IO.FileInfo(_filesWizard.updateFileName);
            updateFile.CopyTo(tmpFolderPath + sdp.PackageId + "\\Bin\\" + updateFile.Name);
            
            sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            IPublisher publisher = _wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            publisher.ProgressHandler += new EventHandler<PublishingEventArgs>(publisher_Progress);
            publisher.PublishPackage(tmpFolderPath + sdp.PackageId + "\\Bin\\", null);
            MessageBox.Show(resManager.GetString("UpdatePublished"));
            System.IO.Directory.Delete(tmpFolderPath + sdp.PackageId, true);
            btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void publisher_Progress(object sender, EventArgs e)
        {
            PublishingEventArgs eventArgs = (PublishingEventArgs)e;

            if (eventArgs.UpperProgressBound > int.MaxValue)
            {
                prgBrPublishing.Maximum = int.MaxValue;
                prgBrPublishing.Value = (int)(eventArgs.CurrentProgress * (int.MaxValue / eventArgs.CurrentProgress));
            }
            else
            {
                prgBrPublishing.Maximum = (int)eventArgs.UpperProgressBound;
                prgBrPublishing.Value = (int)eventArgs.CurrentProgress;
            }
            lblProgress.Text = eventArgs.ProgressStep.ToString() + " : " + eventArgs.ProgressInfo;
            prgBrPublishing.Refresh();
            lblProgress.Refresh();
        }
    }
}
