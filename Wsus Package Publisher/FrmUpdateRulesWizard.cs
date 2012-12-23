using System;
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
            _allSupportedRules.Add(new RuleFileCreatedPrependRegSz());
            _allSupportedRules.Add(new RuleFileModified());
            _allSupportedRules.Add(new RuleFileSize());
            _allSupportedRules.Add(new RuleRegKeyExists());
            _allSupportedRules.Add(new RuleRegValueExists());
            _allSupportedRules.Add(new RuleRegDword());
            _allSupportedRules.Add(new RuleRegExpandSz());
            _allSupportedRules.Add(new RuleRegSz());
            _allSupportedRules.Add(new RuleRegSzToVersion());
            _allSupportedRules.Add(new RuleWmiQuery());
            _allSupportedRules.Add(new RuleMsiPatchInstalledForProduct());


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
            _masterGroup.Reset();
            _masterGroup = ParseXml(_masterGroup, xml);

            grpDsp1.Initialize(_masterGroup);
        }

        internal bool EmptyRuleAtPackageLevel
        {
            get { return chkBxEmptyInstallableItemRule.Checked; }
        }

        internal bool IsAllReadyInitialized { get; set; }

        private RulesGroup ParseXml(RulesGroup group, string xml)
        {
            bool reverseRule = false;
            bool thisGroup = true;
            XmlNamespaceManager namespaceMng = new XmlNamespaceManager(new System.Xml.NameTable());
            namespaceMng.AddNamespace("lar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/LogicalApplicabilityRules.xsd");
            namespaceMng.AddNamespace("bar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/BaseApplicabilityRules.xsd");
            namespaceMng.AddNamespace("msiar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/MsiApplicabilityRules.xsd");

            XmlParserContext context = new XmlParserContext(null, namespaceMng, null, XmlSpace.Default);
            XmlTextReader xmlReader = new XmlTextReader(xml, XmlNodeType.Element, context);

            xmlReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlReader.Read();
            while (!xmlReader.EOF)
            {
                switch (xmlReader.Prefix)
                {
                    case "lar":
                        switch (xmlReader.LocalName)
                        {
                            case "Not":
                                reverseRule = (xmlReader.NodeType != XmlNodeType.EndElement);
                                xmlReader.Read();
                                break;
                            case "And":
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    if (thisGroup)
                                    {
                                        group.GroupType = RulesGroup.GroupLogicalOperator.And;
                                        thisGroup = false;
                                        xmlReader.Read();
                                    }
                                    else
                                    {
                                        RulesGroup tempGroup = new RulesGroup();
                                        group.AddGroup(ParseXml(tempGroup, xmlReader.ReadOuterXml()));
                                    }
                                }
                                else
                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                        return group;
                                break;
                            case "Or":
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    if (thisGroup)
                                    {
                                        group.GroupType = RulesGroup.GroupLogicalOperator.Or;
                                        thisGroup = false;
                                        xmlReader.Read();
                                    }
                                    else
                                    {
                                        RulesGroup tempGroup = new RulesGroup();
                                        group.AddGroup(ParseXml(tempGroup, xmlReader.ReadOuterXml()));
                                    }
                                }
                                else
                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                        return group;
                                break;
                            default:
                                xmlReader.Read();
                                break;
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
                                group.AddRule(tempRule);
                                break;
                            }
                        }
                        xmlReader.Read();
                        break;
                    default:
                        xmlReader.Read();
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

        private void grpDsp1_RuleEditionRequest(GenericRule ResquestingRule)
        {
            EditRule(ResquestingRule);
        }

        private void grpDsp1_EditionRequest(GroupDisplayer sender)
        {
            sender.InnerGroup.Edit();
            grpDsp1.Initialize(_masterGroup);
        }

        private void grpDsp1_SelectionChange(GroupDisplayer sender)
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
            frm.KeyPreview = true;
            frm.KeyDown += new KeyEventHandler(frm_KeyDown);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Controls.Add(frmRule);
            frm.Size = new Size(frmRule.Width + 20, frmRule.Height + 2 * SystemInformation.CaptionHeight);
            frmRule.Dock = DockStyle.Fill;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _currentGroup.AddRule(frmRule);
            }

            frm.Hide();
            frm.KeyDown -= new KeyEventHandler(frm_KeyDown);
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
            frm.KeyPreview = true;
            frm.KeyDown += new KeyEventHandler(frm_KeyDown);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Size = new Size(editedRule.Width + 20, editedRule.Height + 2 * SystemInformation.CaptionHeight);
            editedRule.Dock = DockStyle.Fill;
            frm.Controls.Add(editedRule);
            if (frm.ShowDialog() == DialogResult.Cancel)
                ReplaceRule(editedRule, backup, _masterGroup);
            frm.Hide();
            frm.KeyDown -= new KeyEventHandler(frm_KeyDown);
            frm = null;
        }

        void frm_KeyDown(object sender, KeyEventArgs e)
        {
            Form ruleForm = (Form)sender;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ruleForm.Controls[0].Controls["btnOk"].Enabled)
                        (ruleForm.Controls[0].Controls["btnOk"] as Button).PerformClick();
                    break;
                case Keys.Escape:
                    (ruleForm.Controls[0].Controls["btnCancel"] as Button).PerformClick();
                    break;
                default:
                    break;
            }
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
