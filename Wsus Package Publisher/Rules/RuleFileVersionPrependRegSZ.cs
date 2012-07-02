﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RuleFileVersionPrependRegSZ : GenericRule
    {
        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleFileVersionPrependRegSZ).Assembly);

        public RuleFileVersionPrependRegSZ()
        {
            InitializeComponent();

            txtBxDescription.Text = resMan.GetString("DescriptionFileVersionPrependRegSz");
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThan"));
            txtBxFilePath.Focus();
        }

        #region (Methods - Méthodes}

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
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.red, "FileVersionPrependRegSz");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Path");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, FilePath);
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");

            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Key");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, "HKEY_LOCAL_MACHINE");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Subkey");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, SubKey);
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Value");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Value);
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");

            if (RegType32)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " RegType32");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, RegType32.ToString());
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Comparison");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Comparison);
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");


            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Version");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Version);
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

            result += "<bar:FileVersionPrependRegSz Path=\"" + FilePath + "\"";
            result += " Comparison=\"" + Comparison + "\"";
            result += " Version=\"" + Version + "\"";

            result += " Key=\"HKEY_LOCAL_MACHINE\"";
            result += " Subkey=\"" + SubKey + "\"";
            result += " Value=\"" + Value + "\"";

            if (RegType32)
            {
                result += " RegType32=\"" + RegType32.ToString() + "\"";
            }
                        
            result += "/>\r\n";

            if (ReverseRule)
            {
                result += "</lar:Not>\r\n";
            }

            return result;
        }

        public override string ToString()
        {
            return resMan.GetString("FileVersionPrependRegSz");
        }

        private void ValidateData()
        {
            if (!string.IsNullOrEmpty(txtBxSubKey.Text) && !string.IsNullOrEmpty(txtBxRegistryValue.Text) && !string.IsNullOrEmpty(txtBxFilePath.Text) && cmbBxComparison.SelectedIndex != -1)
                btnOk.Enabled = true;
            else
                btnOk.Enabled = false;
        }

        /// <summary>
        /// Determines whether or not the string passed in parameters is compliant with the RegExp : "^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$}"
        /// </summary>
        /// <param name="version">The string to check against Regexp.</param>
        /// <returns>True if the string match, else false.</returns>
        private bool IsVersionStringCorrectlyformated(string version)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$}");

            if (regex.IsMatch(version))
                return true;
            return false;
        }

        /// <summary>
        /// Return a part of the string, corresponding of a sub-version number. Eg : For version = "2.12.0.202" and rank = 3, return "202"
        /// </summary>
        /// <param name="version">The full version string.</param>
        /// <param name="rank">Determine witch sub-version number to return (starting at 0).</param>
        /// <returns>Return a Integer corresponding to the sub-version number</returns>
        private int GetVersionNumber(string version, int rank)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$}");
            string number;
            int result;

            number = (regex.Matches(version)[rank]).ToString();
            if (int.TryParse(number, out result))
                return result;
            else
                return 0;
        }

        #endregion

        #region (Properties - Propriétés)

        /// <summary>
        /// Get or Set the SubKey
        /// </summary>
        internal string SubKey
        {
            get { return txtBxSubKey.Text; }
            set { txtBxSubKey.Text = value; }
        }

        /// <summary>
        /// Get or Set the Registry Key value
        /// </summary>
        internal string Value
        {
            get { return txtBxRegistryValue.Text; }
            set { txtBxRegistryValue.Text = Value; }
        }

        /// <summary>
        /// Get or Set if the Registry key is 32 bit
        /// </summary>
        internal bool RegType32
        {
            get { return chkBxRegType32.Checked; }
            set { chkBxRegType32.Checked = value; }

        }

        /// <summary>
        /// Get or Set the file path.
        /// </summary>
        internal string FilePath
        {
            get { return txtBxFilePath.Text; }
            set { txtBxFilePath.Text = value; }
        }

        internal string Comparison
        {
            get
            {
                switch (cmbBxComparison.SelectedIndex)
                {
                    case 0:
                        return "LessThan";
                    case 1:
                        return "LessThanOrEqualTo";
                    case 2:
                        return "EqualTo";
                    case 3:
                        return "GreaterThanOrEqualTo";
                    case 4:
                        return "GreaterThan";
                    default:
                        return "LessThan";
                }
            }
            set
            {
                switch (value)
                {
                    case "LessThan":
                        cmbBxComparison.SelectedIndex = 0;
                        break;
                    case "LessThanOrEqualTo":
                        cmbBxComparison.SelectedIndex = 1;
                        break;
                    case "EqualTo":
                        cmbBxComparison.SelectedIndex = 2;
                        break;
                    case "GreaterThanOrEqualTo":
                        cmbBxComparison.SelectedIndex = 3;
                        break;
                    case "GreaterThan":
                        cmbBxComparison.SelectedIndex = 4;
                        break;
                    default:
                        cmbBxComparison.SelectedIndex = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// Get or Set if the rule should be reverse.
        /// </summary>
        internal bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        /// <summary>
        /// Get or Set the file version informations. Format "xxxxx.xxxxx.xxxxx.xxxxx" where 'x' is a digit.
        /// </summary>
        internal string Version
        {
            get { return nupVersion1.Value + "." + nupVersion2.Value + "." + nupVersion3.Value + "." + nupVersion4.Value; }
            set
            {
                if (IsVersionStringCorrectlyformated(value))
                {
                    nupVersion1.Value = GetVersionNumber(value, 0);
                    nupVersion2.Value = GetVersionNumber(value, 1);
                    nupVersion3.Value = GetVersionNumber(value, 2);
                    nupVersion4.Value = GetVersionNumber(value, 3);
                }
            }
        }


        #endregion

        #region (responses to Events - Réponses aux événements)

        private void txtBxSubKey_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        #endregion

    }
}
