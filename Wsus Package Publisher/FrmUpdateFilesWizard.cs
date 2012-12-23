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
        private UpdateType _fileType;

        public FrmUpdateFilesWizard()
        {
            InitializeComponent();
            txtBxUpdateFile.Select();
        }

        public FrmUpdateFilesWizard(string fileName)
        {
            InitializeComponent();
            if (System.IO.File.Exists(fileName))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                string ext = fileInfo.Extension.ToLower();
                if (ext == ".msi" || ext == ".msp" || ext == ".exe")
                    txtBxUpdateFile.Text = fileName;
            }
        }

        internal string UpdateFileName
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
                    UpdateFileName = txtBxUpdateFile.Text;
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
        
        private void lstBxAdditionnalFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lstBxAdditionnalFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (fileNames != null)
                    foreach (string file in fileNames)
                    {
                        if (System.IO.File.Exists(file))
                        {
                            lstBxAdditionnalFiles.Items.Add(file);
                            _additionnalFileName.Add(file);
                        }
                    }
            }
        }

        private void txtBxUpdateFile_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects effect = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Array tab = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (tab != null && tab.GetValue(0) != null && tab.Length == 1)
                {
                    string fileName = tab.GetValue(0).ToString();
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                    string ext = fileInfo.Extension.ToLower();
                    if (ext == ".msi" || ext == ".msp" || ext == ".exe")
                        effect = DragDropEffects.Copy;
                }
            }
            e.Effect = effect;
        }

        private void txtBxUpdateFile_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (fileNames != null && fileNames.GetValue(0) != null)
                {
                    string fileName = fileNames[0];
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                    string ext = fileInfo.Extension.ToLower();
                    if (ext == ".msi" || ext == ".msp" || ext == ".exe")
                        txtBxUpdateFile.Text = fileName;
                }
            }
        }

    }
}
