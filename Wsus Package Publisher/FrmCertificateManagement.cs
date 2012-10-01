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
    public partial class FrmCertificateManagement : Form
    {
        WsusWrapper _wsus;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmCertificateManagement).Assembly);

        public FrmCertificateManagement()
        {
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();
        }

        #region (response to events - réponse aux événemments)

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            _wsus.GenerateSelfSignedCertificate();
            MessageBox.Show(resMan.GetString("CertificateSuccessfullyGenerate"));
        }
        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _wsus.UseExistingCertificate(openFileDialog1.FileName, txtBxPassword.Text);
                MessageBox.Show(resMan.GetString("CertificateSuccessfullyLoaded"));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _wsus.SaveCertificate(saveFileDialog1.FileName);
                MessageBox.Show(resMan.GetString("CertificateSuccessfullySaved"));
            }
        }
        
        private void txtBxPassword_TextChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = (txtBxPassword.Text.Length != 0);
        }


        #endregion

    }
}
