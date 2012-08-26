﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateRulesWizard : Form
    {
        RulesGroup _masterGroup = new RulesGroup();
        RulesGroup _currentGroup;
        List<GenericRule> _allSupportedRules = new List<GenericRule>();
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateRulesWizard).Assembly);

        internal FrmUpdateRulesWizard()
        {
            InitializeComponent();
            grpDsp1.SelectionChange += new GroupDisplayer.SelectionChangeEventHandler(grpDsp1_SelectionChange);
            grpDsp1.EditionRequest += new GroupDisplayer.EditionRequestEventHandler(grpDsp1_EditionRequest);
            grpDsp1.RuleEditionRequest += new GroupDisplayer.RuleEditionRequestEventHandler(grpDsp1_RuleEditionRequest);
            _currentGroup = _masterGroup;
            _allSupportedRules.Add(new RuleMsiProductInstalled());
            _allSupportedRules.Add(new RuleProcessorArchitecture());
            _allSupportedRules.Add(new RuleWindowsVersion());
            _allSupportedRules.Add(new RuleWindowsLanguage());
            _allSupportedRules.Add(new RuleFileExists());
            _allSupportedRules.Add(new RuleFileExistsPrependRegSz());
            _allSupportedRules.Add(new RuleFileVersion());
            _allSupportedRules.Add(new RuleFileVersionPrependRegSZ());
            _allSupportedRules.Add(new RuleFileCreated());

            foreach (GenericRule rule in _allSupportedRules)
            {
                cmbBxRules.Items.Add(rule);
            }

            cmbBxRules.SelectedIndex = 0;
            cmbBxRules.Focus();

            _masterGroup.IsSelected = true;
            grpDsp1.Initialize(_masterGroup);
        }

        internal void InitializeFromXml(string xml)
        {
            _masterGroup = new RulesGroup();
            _currentGroup = _masterGroup;

            _masterGroup = ParseXml(_masterGroup, xml);

            grpDsp1.Initialize(_masterGroup);
        }

        private RulesGroup ParseXml(RulesGroup group, string xml)
        {
            bool reverseRule = false;
            XmlNamespaceManager namespaceMng = new XmlNamespaceManager(new System.Xml.NameTable());
            namespaceMng.AddNamespace("lar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/LogicalApplicabilityRules.xsd");
            namespaceMng.AddNamespace("bar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/BaseApplicabilityRules.xsd");
            namespaceMng.AddNamespace("msiar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/MsiApplicabilityRules.xsd");

            XmlParserContext context = new XmlParserContext(null, namespaceMng, null, XmlSpace.Default);
            XmlTextReader xmlReader = new XmlTextReader(xml, XmlNodeType.Element, context);

            xmlReader.WhitespaceHandling = WhitespaceHandling.None;
            while (!xmlReader.EOF)
            {
                xmlReader.Read();
                switch (xmlReader.Prefix)
                {
                    case "lar":
                        if (xmlReader.LocalName == "Not")
                            reverseRule = (xmlReader.NodeType != XmlNodeType.EndElement);
                        else
                            if (xmlReader.LocalName == "And")
                            {
                                RulesGroup tempGroup = new RulesGroup();
                                group.InnerGroups.Add(Guid.NewGuid(), ParseXml(tempGroup, xmlReader.ReadInnerXml()));
                            }
                            else
                                if (xmlReader.LocalName == "Or")
                                {
                                    RulesGroup tempGroup = new RulesGroup();
                                    tempGroup.GroupType = RulesGroup.GroupLogicalOperator.Or;
                                    group.InnerGroups.Add(Guid.NewGuid(), ParseXml(tempGroup, xmlReader.ReadInnerXml()));
                                }
                        break;
                    case "bar":
                    case "msiar":
                        foreach (GenericRule rule in _allSupportedRules)
                        {
                            if (xmlReader.LocalName == rule.XmlElementName)
                            {
                                GenericRule tempRule = GetSelectedForm(rule);
                                tempRule.ReverseRule = reverseRule;
                                tempRule.InitializeWithAttributes(GetAttributes(xmlReader));
                                group.InnerRules.Add(Guid.NewGuid(), tempRule);
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return group;
        }

        private Dictionary<string, string> GetAttributes(XmlTextReader reader)
        {
            Dictionary<string, string> attributes = new Dictionary<string, string>();

            while (reader.MoveToNextAttribute())
            {
                if (!reader.Name.StartsWith("xmlns:"))
                    attributes.Add(reader.Name, reader.Value);
            }

            return attributes;
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
