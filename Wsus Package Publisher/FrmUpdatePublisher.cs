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
        private WsusWrapper _wsus;
        private FrmUpdateFilesWizard _filesWizard;
        private FrmUpdateInformationsWizard _informationsWizard;
        private FrmUpdateRulesWizard _isInstalledRulesWizard;
        private FrmUpdateRulesWizard _isInstallableRulesWizard;

        internal FrmUpdatePublisher(FrmUpdateFilesWizard FilesWizard, FrmUpdateInformationsWizard InformationsWizard, FrmUpdateRulesWizard IsInstalledRulesWizard, FrmUpdateRulesWizard IsInstallableRulesWizard)
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();
            _filesWizard = FilesWizard;
            _informationsWizard = InformationsWizard;
            _isInstalledRulesWizard = IsInstalledRulesWizard;
            _isInstallableRulesWizard = IsInstallableRulesWizard;
        }

        internal void Publish()
        {
            System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdatePublisher).Assembly);

            prgBrPublishing.Value = 0;

            _wsus.UpdatePublishingProgress += new WsusWrapper.UpdatePublishingProgressEventHandler(publisher_Progress);
            _wsus.PublishUpdate(_filesWizard, _informationsWizard, _isInstalledRulesWizard, _isInstallableRulesWizard);
            
            MessageBox.Show(resManager.GetString("UpdatePublished"));
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
