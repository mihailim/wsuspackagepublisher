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
            _currentGroup = _masterGroup;
            cmbBxRules.Items.Add(new RuleMsiProductInstalled());
            cmbBxRules.Items.Add(new RuleProcessorArchitecture());
            cmbBxRules.Items.Add(new RuleWindowsVersion());
            cmbBxRules.Items.Add(new RuleWindowsLanguage());
            cmbBxRules.Items.Add(new RuleFileExists());
            cmbBxRules.Items.Add(new RuleFileExistsPrependRegSz());
            cmbBxRules.Items.Add(new RuleFileVersion());
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            GenericRule frmRule;

            frmRule = GetSelectedForm();
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Controls.Add(frmRule);
            frm.Size = new Size(frmRule.Width + 20, frmRule.Height + 2*SystemInformation.CaptionHeight);
            frmRule.Dock = DockStyle.Fill;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _currentGroup.AddFrm(frmRule);
                frm.Hide();
                frm.Dispose();
                frm = null;
            }
            rulesViewer1.Rules = _currentGroup;            
        }

        private GenericRule GetSelectedForm()
        {
            Object selectedRule = cmbBxRules.SelectedItem;
            Type ruleType = selectedRule.GetType();
            System.Reflection.Assembly assembly = ruleType.Assembly;
            return (GenericRule)assembly.CreateInstance(ruleType.FullName);            
        }

        internal string GetXmlFormattedRule()
        {
            return _masterGroup.GetXmlFormattedRule();
        }
    }
}
