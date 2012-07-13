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
    internal partial class FrmUpdateRulesWizard : Form
    {
        RulesGroup _masterGroup = new RulesGroup();
        RulesGroup _currentGroup;
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateRulesWizard).Assembly);

        internal FrmUpdateRulesWizard()
        {
            InitializeComponent();
            grpDsp1.SelectionChange += new GroupDisplayer.SelectionChangeEventHandler(grpDsp1_SelectionChange);
            grpDsp1.EditionRequest += new GroupDisplayer.EditionRequestEventHandler(grpDsp1_EditionRequest);
            grpDsp1.RuleEditionRequest += new GroupDisplayer.RuleEditionRequestEventHandler(grpDsp1_RuleEditionRequest);
            _currentGroup = _masterGroup;
            cmbBxRules.Items.Add(new RuleMsiProductInstalled());
            cmbBxRules.Items.Add(new RuleProcessorArchitecture());
            cmbBxRules.Items.Add(new RuleWindowsVersion());
            cmbBxRules.Items.Add(new RuleWindowsLanguage());
            cmbBxRules.Items.Add(new RuleFileExists());
            cmbBxRules.Items.Add(new RuleFileExistsPrependRegSz());
            cmbBxRules.Items.Add(new RuleFileVersion());
            cmbBxRules.Items.Add(new RuleFileVersionPrependRegSZ());
            cmbBxRules.Items.Add(new RuleFileCreated());
            cmbBxRules.Items.Add(new RuleFileCreated());
            cmbBxRules.SelectedIndex = 0;
            cmbBxRules.Focus();

            _masterGroup.IsSelected = true;
            grpDsp1.Initialize(_masterGroup);
        }

        void grpDsp1_RuleEditionRequest(GenericRule ResquestingRule)
        {
            EditRule(ResquestingRule);
        }

        void grpDsp1_EditionRequest(GroupDisplayer sender)
        {
            sender.InnerGroup.Edit();
            grpDsp1.Initialize(_masterGroup);
        }

        void grpDsp1_SelectionChange(GroupDisplayer sender)
        {
            int total = sender.SelectedRules.Count + sender.SelectedGroups.Count;

            btnEdit.Enabled = (total == 1);
            btnDelete.Enabled = (total > 0);

            if (sender.SelectedGroups.Count == 1)
            {
                btnAddAndGroup.Enabled = true;
                btnAddOrGroup.Enabled = true;
                btnAddRule.Enabled = true;
                _currentGroup = sender.SelectedGroups[0];
            }
            else
            {
                btnAddAndGroup.Enabled = false;
                btnAddOrGroup.Enabled = false;
                btnAddRule.Enabled = false;
            }
        }
        
        private void btnAddRule_Click(object sender, EventArgs e)
        {
            GenericRule frmRule;

            frmRule = GetSelectedForm(cmbBxRules.SelectedItem);
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Controls.Add(frmRule);
            frm.Size = new Size(frmRule.Width + 20, frmRule.Height + 2 * SystemInformation.CaptionHeight);
            frmRule.Dock = DockStyle.Fill;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _currentGroup.AddRule(frmRule);
            }

            frm.Hide();
            frm = null;
            grpDsp1.Initialize(_masterGroup);
        }

        private GenericRule GetSelectedForm(Object obj)
        {
            Type ruleType = obj.GetType();
            System.Reflection.Assembly assembly = ruleType.Assembly;
            return (GenericRule)assembly.CreateInstance(ruleType.FullName);
        }

        internal string GetXmlFormattedRule()
        {
            return _masterGroup.GetXmlFormattedRule();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grpDsp1.SelectedRules.Count == 1)
                EditRule(grpDsp1.SelectedRules[0]);
            if (grpDsp1.SelectedGroups.Count == 1)
                grpDsp1.SelectedGroups[0].Edit();
            grpDsp1.Initialize(_masterGroup);
        }

        private void EditRule(GenericRule editedRule)
        {
            GenericRule backup = editedRule.Clone();
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Size = new Size(editedRule.Width + 20, editedRule.Height + 2 * SystemInformation.CaptionHeight);
            editedRule.Dock = DockStyle.Fill;
            frm.Controls.Add(editedRule);
            if (frm.ShowDialog() == DialogResult.Cancel)
                ReplaceRule(editedRule, backup, _masterGroup);
            frm.Hide();
            frm = null;
        }

        private bool ReplaceRule(GenericRule editedRule, GenericRule backupRule, RulesGroup groupToSearchInto)
        {
            if (groupToSearchInto.InnerRules.ContainsKey(editedRule.Id))
            {
                groupToSearchInto.InnerRules.Remove(editedRule.Id);                
                groupToSearchInto.AddRule(backupRule);
                grpDsp1.Initialize(_masterGroup);
                return true;
            }
            else
                foreach (RulesGroup group in groupToSearchInto.InnerGroups.Values)
                {
                    if (ReplaceRule(editedRule, backupRule, group))
                        break;
                }
            return true;
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<GenericRule> selectedRules = grpDsp1.SelectedRules;
            List<RulesGroup> selectedGroup = grpDsp1.SelectedGroups;

            foreach (GenericRule rule in selectedRules)
                DeleteRule(rule, _masterGroup);
            foreach (RulesGroup group in selectedGroup)
                DeleteGroup(group, _masterGroup);
            grpDsp1.Initialize(_masterGroup);
        }

        private bool DeleteRule(GenericRule ruleToDelete, RulesGroup groupToSearchInto)
        {
            if (groupToSearchInto.InnerRules.ContainsKey(ruleToDelete.Id))
            {
                groupToSearchInto.InnerRules.Remove(ruleToDelete.Id);
                return true;
            }
            else
                foreach (RulesGroup group in groupToSearchInto.InnerGroups.Values)
                {
                    if (DeleteRule(ruleToDelete, group))
                        break;
                }
            return true;
        }

        private bool DeleteGroup(RulesGroup groupToDelete, RulesGroup groupToSearchInto)
        {
            if (groupToDelete.Id == _masterGroup.Id)
                return false;

            if (groupToSearchInto.InnerGroups.ContainsKey(groupToDelete.Id))
            {
                groupToSearchInto.InnerGroups.Remove(groupToDelete.Id);
                return true;
            }
            else
                foreach (RulesGroup group in groupToSearchInto.InnerGroups.Values)
                {
                    if (DeleteGroup(groupToDelete, group))
                        break;
                }
            return true;
        }

        private void btnAddAndGroup_Click(object sender, EventArgs e)
        {
            RulesGroup addedGroup = new RulesGroup();

            _currentGroup.IsSelected = false;            
            addedGroup.GroupType = RulesGroup.GroupLogicalOperator.And;
            addedGroup.IsSelected = true;
            _currentGroup.AddGroup(addedGroup);
            grpDsp1.Initialize(_masterGroup);
        }

        private void btnAddOrGroup_Click(object sender, EventArgs e)
        {
            RulesGroup addedGroup = new RulesGroup();

            _currentGroup.IsSelected = false;
            addedGroup.GroupType = RulesGroup.GroupLogicalOperator.Or;
            addedGroup.IsSelected = true;
            _currentGroup.AddGroup(addedGroup);
            grpDsp1.Initialize(_masterGroup);
        }

    }
}
