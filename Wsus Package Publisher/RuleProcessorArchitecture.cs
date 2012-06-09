using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RuleProcessorArchitecture : GenericRule
    {
        private ushort _processorArchitecture;
        private bool _reverseRule;

        internal RuleProcessorArchitecture()
        {
            InitializeComponent();
        }

        #region Methods - Méthodes

        internal override string GetRtfFormattedRule(string rtf, int tabulation)
        {
            RichTextBox rTxtBx = new RichTextBox();
            string tab = new string(' ', tabulation);
            rTxtBx.Rtf += rtf;
            rTxtBx.Select(rTxtBx.Text.Length - 1, 1);

            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, tab);

            if (ReverseRule)
            {
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.green, "<lar:");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, "Not");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.green, ">\r\n" + tab + tab);
            }

            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "<bar:");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.red, "Processor");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Architecture");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, ProcessorArchitecture.ToString());
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "/>\r\n");

            if (ReverseRule)
            {
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, tab);
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.green, "<lar:");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, "Not");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.green, ">\r\n");
            }

            return rTxtBx.Rtf;
        }

        internal override string GetXmlFormattedRule()
        {
            string result = "";

            if (ReverseRule)
            {
                result += "<lar:Not>\r\n";
            }

            result += "<bar:Processor Architecture=\"" + ProcessorArchitecture.ToString() + "\"/>\r\n";

            if (ReverseRule)
            {
                result += "</lar:Not>\r\n";
            }

            return result;
        }

        #endregion

        #region Properties - Propriétés

        internal override GenericRule.ObjectType TypeOfObject
        {
            get { return GenericRule.ObjectType.Rule; }
        }

        internal override string RuleType
        {
            get { return "RuleProcessorArchitecture"; }
        }

        internal override GenericRule.GroupLogicalOperator GroupType
        {
            get { return GroupLogicalOperator.None; }
            set { }
        }

        /// <summary>
        /// Get or Set the processor architecture. 0 : x86, 6 : IA64, 9 : x64
        /// </summary>
        internal ushort ProcessorArchitecture
        {
            get { return _processorArchitecture; }
            set { _processorArchitecture = value; }
        }

        /// <summary>
        /// Get or set if the rule is reverse.
        /// </summary>
        internal bool ReverseRule
        {
            get { return _reverseRule; }
            set { _reverseRule = value; }
        }

        #endregion

        #region Response to Events - Réponses aux évènements

        private void cmbBxProcessorArchitecture_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBxProcessorArchitecture.SelectedIndex)
            {
                case 0:
                    ProcessorArchitecture = 0;
                    break;
                case 1:
                    ProcessorArchitecture = 9;
                    break;
                case 2:
                    ProcessorArchitecture = 6;
                    break;
                default:
                    break;
            }
            btnOk.Enabled = true;
        }

        private void chkBxInverseRule_CheckedChanged(object sender, EventArgs e)
        {
            ReverseRule = chkBxInverseRule.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
