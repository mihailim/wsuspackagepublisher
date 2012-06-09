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
        
        internal FrmUpdateRulesWizard()
        {
            InitializeComponent();
            _currentGroup = _masterGroup;
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            GenericRule frmRule;

            frmRule = GetSelectedForm();
            //RuleMsiProductInstalled MsiRule = new RuleMsiProductInstalled();
            //frmRule = MsiRule;
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
            switch (cmbBxRules.SelectedItem.ToString())
            {
                case "Produit MSI Installé":
                    return new RuleMsiProductInstalled();
                case "Architecture du processeur":
                    return new RuleProcessorArchitecture();
                default:
                    return new RuleMsiProductInstalled();
            }
        }

        internal string GetXmlFormattedRule()
        {
            return _masterGroup.GetXmlFormattedRule();
        }
    }
}
