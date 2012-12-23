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
    internal partial class frmMetaGroupCreation : Form
    {
        private List<MetaGroup> _metaGroups;
        private ComputerGroup _computerGroups;
        private MetaGroup newMetaGroup;

        internal frmMetaGroupCreation(List<MetaGroup> metaGroups, ComputerGroup computerGroups)
        {
            CommonInitialization(metaGroups, computerGroups);
            newMetaGroup = new MetaGroup();
            Editing = false;
        }

        internal frmMetaGroupCreation(List<MetaGroup> metaGroups, ComputerGroup computerGroups, MetaGroup metaGroupToEdit)
        {
            metaGroups.Remove(metaGroupToEdit);
            CommonInitialization(metaGroups, computerGroups);
            txtBxMetaGroupName.Text = metaGroupToEdit.Name;
            SelectMetaGroup(metaGroupToEdit);
            SelectComputersGroups(metaGroupToEdit);
            newMetaGroup = metaGroupToEdit;
            Editing = true;
        }

        #region {Methods - Méthodes}

        private void CommonInitialization(List<MetaGroup> metaGroups, ComputerGroup computerGroups)
        {
            InitializeComponent();
            _metaGroups = metaGroups;
            _computerGroups = computerGroups;

            foreach (MetaGroup metaGroup in metaGroups)
                chkCmbBxMetaGroups.AddItem(metaGroup);

            FillComputerGroups(computerGroups);
        }

        private void ValidateData()
        {
            if (!string.IsNullOrEmpty(txtBxMetaGroupName.Text))
            {
                bool found = false;
                foreach (MetaGroup metaGroup in _metaGroups)
                {
                    if (metaGroup.Name == txtBxMetaGroupName.Text)
                    {
                        found = true;
                        break;
                    }
                }
                btnOk.Enabled = (!found && chkCmbBxMetaGroups.SelectedItems.Count != 0 || chkCmbBxComputerGroups.SelectedItems.Count != 0);
            }
        }

        private void FillComputerGroups(ComputerGroup computerGroups)
        {
            chkCmbBxComputerGroups.AddItem(computerGroups);
            foreach (ComputerGroup group in computerGroups.InnerComputerGroup)
            {
                FillComputerGroups(group);
            }
        }

        private void SelectComputersGroups(MetaGroup metaGroupToEdit)
        {
            foreach (ComputerGroup group in metaGroupToEdit.InnerComputerGroups)
            {
                foreach (object obj in chkCmbBxComputerGroups.Items)
                {
                    if (group.Name == (obj as ComputerGroup).Name)
                    {
                        chkCmbBxComputerGroups.SelectItem(obj, true);
                        break;
                    }
                }
            }
        }

        private void SelectMetaGroup(MetaGroup metaGroupToEdit)
        {
            foreach (MetaGroup group in metaGroupToEdit.InnerMetaGroups)
            {
                foreach (object obj in chkCmbBxMetaGroups.Items)
                {
                    if (group.Name == (obj as MetaGroup).Name)
                    {
                        chkCmbBxMetaGroups.SelectItem(obj, true);
                        break;
                    }
                }
            }
        }

        #endregion #region {Methods - Méthodes}

        #region {Properties - Propriétés}

        /// <summary>
        /// Get the MetaGroup List after it was Modified.
        /// </summary>
        internal List<MetaGroup> MetaGroupList
        {
            get { return _metaGroups; }
        }

        private bool Editing { get; set; }

        #endregion {Properties - Propriétés}

        #region {Responses to Events - Réponses aux Evenements}

        private void txtBxMetaGroupName_TextChanged(object sender, EventArgs e)
        {
            bool found = false;

            if (!string.IsNullOrEmpty(txtBxMetaGroupName.Text))
                foreach (MetaGroup metagroup in _metaGroups)
                {
                    if (metagroup.Name.ToLower() == txtBxMetaGroupName.Text.ToLower())
                    {
                        found = true;
                        break;
                    }
                }
            if (!found)
                ValidateData();
            else
                btnOk.Enabled = false;
        }

        private void chkCmbBxMetaGroup_SelectionChanged()
        {
            ValidateData();
        }

        private void chkCmbBxComputerGroups_SelectionChanged()
        {
            ValidateData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Editing)
                _metaGroups.Add(newMetaGroup);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            newMetaGroup.InnerComputerGroups.Clear();
            newMetaGroup.InnerMetaGroups.Clear();
            newMetaGroup.Name = txtBxMetaGroupName.Text;

            foreach (object obj in chkCmbBxMetaGroups.SelectedItems)
                newMetaGroup.InnerMetaGroups.Add((MetaGroup)obj);

            foreach (ComputerGroup selectedComputerGroup in chkCmbBxComputerGroups.SelectedItems)
            {
                newMetaGroup.InnerComputerGroups.Add(selectedComputerGroup);
            }

            _metaGroups.Add(newMetaGroup);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion {Responses to Events - Réponses aux Evenements}

    }
}
