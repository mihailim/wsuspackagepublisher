﻿using System;
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
            cmbBxRules.Items.Add(resManager.GetString("MsiProductInstalled"));
            cmbBxRules.Items.Add(resManager.GetString("Processor"));
            cmbBxRules.Items.Add(resManager.GetString("WindowsVersion"));
            cmbBxRules.Items.Add(resManager.GetString("WindowsLanguage"));
            cmbBxRules.Items.Add(resManager.GetString("FileExists"));
            cmbBxRules.Items.Add(resManager.GetString("FileExistsPrependRegSz"));

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
            switch (cmbBxRules.SelectedIndex)
            {
                case 0:
                    return new RuleMsiProductInstalled();
                case 1:
                    return new RuleProcessorArchitecture();
                case 2:
                    return new RuleWindowsVersion();
                case 3:
                    return new RuleWindowsLanguage();
                case 4:
                    return new RuleFileExists();
                case 5:
                    return new RuleFileExistsPrependRegSz();
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
