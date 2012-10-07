using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    public partial class FrmUpdateFilesWizard : Form
    {
        public enum UpdateType
        {
            WindowsInstaller,
            WindowsInstallerPatch,
            Executable
        }
        private List<ReturnCode> _returnCodes = new List<ReturnCode>();

        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateFilesWizard).Assembly);

        private string _updateFileName = "";
        private List<string> _additionnalFileName = new List<string>();
        private string _commandLine = "";
        private UpdateType _fileType;

        public FrmUpdateFilesWizard()
        {
            InitializeComponent();
            DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)dtgrvReturnCodes.Columns["Result"];
            column.Items.Add(resManager.GetString("Failed"));
            column.Items.Add(resManager.GetString("Succeeded"));
            column.Items.Add(resManager.GetString("Cancelled"));

            txtBxUpdateFile.Select();
        }

        internal string updateFileName
        {
            get { return _updateFileName; }
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _updateFileName = value;
            }
        }

        internal List<string> AdditionnalFileName
        {
            get { return _additionnalFileName; }

            private set
            {
                if (value != null)
                    _additionnalFileName = value;
            }
        }

        internal string CommandLine
        {
            get { return _commandLine; }
            private set
            {
                _commandLine = value;
            }
        }

        internal List<ReturnCode> ReturnCodes
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
        }

        private InstallationResult GetInstallationResult(string p)
        {
            if (p == resManager.GetString("Failed"))
                return InstallationResult.Failed;
            if (p == resManager.GetString("Succeeded"))
                return InstallationResult.Succeeded;
            if (p == resManager.GetString("Cancelled"))
                return InstallationResult.Cancelled;

            return InstallationResult.Failed;
        }

        private int GetReturnCodeValue(string p)
        {
            int result;
            int.TryParse(p, out result);
            return result;
        }

        internal UpdateType FileType
        {
            get { return _fileType; }
            private set { _fileType = value; }
        }

        private void btnBrowseUpdateFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogUpdateFile = new OpenFileDialog();

            if (!string.IsNullOrEmpty(txtBxUpdateFile.Text))
                openFileDialogUpdateFile.InitialDirectory = txtBxUpdateFile.Text;
            else
                openFileDialogUpdateFile.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            openFileDialogUpdateFile.Filter = resManager.GetString("openFileDialogueUpdateFile");
            openFileDialogUpdateFile.Multiselect = false;

            if (openFileDialogUpdateFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtBxUpdateFile.Text = openFileDialogUpdateFile.FileName;
        }

        private void txtBxUpdateFile_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;

            if (!string.IsNullOrEmpty(txtBxUpdateFile.Text) && System.IO.File.Exists(txtBxUpdateFile.Text))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(txtBxUpdateFile.Text);
                string extension = fileInfo.Extension.ToLower();
                if (extension == ".msi" || extension == ".msp" || extension == ".exe")
                {
                    btnNext.Enabled = true;
                    updateFileName = txtBxUpdateFile.Text;
                    switch (extension)
                    {
                        case ".msi":
                            FileType = FrmUpdateFilesWizard.UpdateType.WindowsInstaller;
                            break;
                        case ".msp":
                            FileType = FrmUpdateFilesWizard.UpdateType.WindowsInstallerPatch;
                            break;
                        case ".exe":
                            FileType = FrmUpdateFilesWizard.UpdateType.Executable;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void btnAddAdditonnalFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogAdditionnalFile = new OpenFileDialog();

            openFileDialogAdditionnalFile.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            openFileDialogAdditionnalFile.Multiselect = true;

            if (openFileDialogAdditionnalFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && openFileDialogAdditionnalFile.FileNames.Length != 0)
            {
                lstBxAdditionnalFiles.Items.AddRange(openFileDialogAdditionnalFile.FileNames);
                _additionnalFileName.AddRange(openFileDialogAdditionnalFile.FileNames);
            }
        }

        private void btnRemoveAdditionnalFile_Click(object sender, EventArgs e)
        {
            int index = lstBxAdditionnalFiles.SelectedIndex;

            if (index != -1)
                lstBxAdditionnalFiles.Items.RemoveAt(lstBxAdditionnalFiles.SelectedIndex);
            if (lstBxAdditionnalFiles.Items.Count != 0)
                if (index == 0)
                    lstBxAdditionnalFiles.SelectedIndex = 0;
                else
                    if (index == lstBxAdditionnalFiles.Items.Count)
                        lstBxAdditionnalFiles.SelectedIndex = index - 1;
                    else
                        lstBxAdditionnalFiles.SelectedIndex = index;
            _additionnalFileName.Clear();
            foreach (object file in lstBxAdditionnalFiles.Items)
            {
                _additionnalFileName.Add((string)file);
            }
        }

        private void lstBxAdditionnalFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBxAdditionnalFiles.SelectedIndex == -1)
                btnRemoveAdditionnalFile.Enabled = false;
            else
                btnRemoveAdditionnalFile.Enabled = true;
        }

        private void txtBxCommandLine_TextChanged(object sender, EventArgs e)
        {
            CommandLine = txtBxCommandLine.Text;
        }

    }
}
