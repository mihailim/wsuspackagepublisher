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
    internal partial class RuleMsiProductInstalled : GenericRule
    {
         Dictionary<string, int> _languagesByString = new Dictionary<string, int>
        {   {"Arabic", 1},
            {"Chinese_HK_SAR", 3076},
            {"Chinese_simplified", 4},
            {"Chinese_traditional",31748},
            {"Czech", 5},
            {"Danish", 6}, 
            {"Dutch", 19},
            {"English", 9},
            {"Finnish", 11}, 
            {"French", 12},    
            {"German", 7},   
            {"Greek", 8},   
            {"Hebrew", 13},            
            {"Hungarian", 14},   
            {"Italian", 16},              
            {"Japanese", 17},   
            {"Korean", 18}, 
            {"Norwegian", 20},
            {"Polish", 21},
            {"Portugese", 22}, 
            {"Portugese_Brazil", 1046}, 
            {"Russian", 25},
            {"Spanish", 10}, 
            {"Swedish", 29}, 
            {"Turkish" , 31}
        };
        Dictionary<int, string> _languaguesByInt = new Dictionary<int, string>
        {
            {1, "Arabic"},
            {3076, "Chinese_HK_SAR"},
            {4, "Chinese_simplified"},
            {31748, "Chinese_traditional"},
            {5, "Czech"},
            {6, "Danish"}, 
            {19, "Dutch"},
            {9, "English"},
            {11, "Finnish"}, 
            {12, "French"},    
            {7, "German"},   
            {8, "Greek"},   
            {13, "Hebrew"},            
            {14, "Hungarian"},   
            {16, "Italian"},              
            {17, "Japanese"},   
            {18, "Korean"}, 
            {20, "Norwegian"},
            {21, "Polish"},
            {22, "Portugese"}, 
            {1046, "Portugese_Brazil"}, 
            {25, "Russian"},
            {10, "Spanish"}, 
            {29, "Swedish"}, 
            {31, "Turkish"}
        };

        Guid _guid = Guid.NewGuid();
        Guid _msiCode;
        int _language = -1;
        bool _useVersionMax = false;
        bool _useVersionMin = false;
        bool _reversRule = false;
        System.Text.RegularExpressions.Regex regExp = new System.Text.RegularExpressions.Regex("^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$");

        internal RuleMsiProductInstalled()
        {
            InitializeComponent();
            foreach (string language in _languagesByString.Keys)
            {
                cmbBxLanguage.Items.Add(language);
            }
            txtBxMsiCode.Select();
        }

        #region(Methods - Méthodes)

        /// <summary>
        /// Determines whether or not the string passed in parameters is compliant with the RegExp : "^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$}"
        /// </summary>
        /// <param name="version">The string to check against Regexp.</param>
        /// <returns>True if the string match, else false.</returns>
        private bool IsVersionStringCorrectlyformatted(string version)
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

        internal override string GetRtfFormattedRule(string rtf, int tabulation)
        {
            RichTextBox rTxtBx = new RichTextBox();
            string tab = new string(' ', tabulation);
            rTxtBx.Rtf += rtf;
            rTxtBx.Select(rTxtBx.Text.Length-1, 1);

            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, tab);

            if (ReverseRule)
            {
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.green, "<lar:");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, "Not");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.green, ">\r\n" + tab + tab);
            }
            
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "<msiar:");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.red, "MsiProductInstalled");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " ProductCode");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"{");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, MsiProductCode.ToString());
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "}\"");

            if (UseVersionMax)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " VersionMax");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, VersionMax);
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

            if (UseVersionMin)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " VersionMin");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, VersionMin);
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

            if (Language != -1)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Language");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Language.ToString());
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

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

            result += "<msiar:MsiProductInstalled ProductCode=\"{" + MsiProductCode.ToString() + "}\"";

            if (UseVersionMax)
            {
                result += " VersionMax=\"" + VersionMax + "\"";
            }

            if (UseVersionMin)
            {
                result += " VersionMin=\"" + VersionMin + "\"";
            }

            if (Language != -1)
            {
                result += " Language=\"" + Language.ToString() + "\"";
            }

            result += "/>\r\n";

            if (ReverseRule)
            {
                result += "</lar:Not>\r\n";
            }

            return result;
        }

        #endregion

        #region(Properties - propriétés)

        internal Guid GetGuid
        {
            get { return _guid; }
        }

        internal override GenericRule.ObjectType TypeOfObject
        {
            get { return GenericRule.ObjectType.Rule; }
        }

        internal override string RuleType
        {
            get { return "MsiProductInstalled"; }
        }

        internal override GenericRule.GroupLogicalOperator GroupType
        {
            get { return GroupLogicalOperator.None; }
            set { }
        }

        internal Guid MsiProductCode
        {
            get { return _msiCode; }
            set { _msiCode = value; }
        }

        internal bool ReverseRule
        {
            get { return _reversRule; }
            set
            {
                _reversRule = value;
                chkBxReverseRule.Checked = value;
            }
        }

        internal string VersionMax
        {
            get { return nupVersionMax1.Value + "." + nupVersionMax2.Value + "." + nupVersionMax3.Value + "." + nupVersionMax4.Value; }
            set
            {
                if (IsVersionStringCorrectlyformatted(value))
                {
                    nupVersionMax1.Value = GetVersionNumber(value, 0);
                    nupVersionMax2.Value = GetVersionNumber(value, 1);
                    nupVersionMax3.Value = GetVersionNumber(value, 2);
                    nupVersionMax4.Value = GetVersionNumber(value, 3);
                }
                else
                {
                    chkBxIncludeMaxVersion.Checked = false;
                }
            }
        }

        internal string VersionMin
        {
            get { return nupVersionMin1.Value + "." + nupVersionMin2.Value + "." + nupVersionMin3.Value + "." + nupVersionMin4.Value; }
            set
            {
                if (IsVersionStringCorrectlyformatted(value))
                {
                    nupVersionMin1.Value = GetVersionNumber(value, 0);
                    nupVersionMin2.Value = GetVersionNumber(value, 1);
                    nupVersionMin3.Value = GetVersionNumber(value, 2);
                    nupVersionMin4.Value = GetVersionNumber(value, 3);
                }
                else
                {
                    chkBxIncludeMinVersion.Checked = false;
                }
            }
        }

        /// <summary>
        /// Get or Set if the Version Max. Should be include in the MetaData.
        /// </summary>
        internal bool UseVersionMax
        {
            get { return _useVersionMax; }
            set
            {
                _useVersionMax = value;
                chkBxIncludeMaxVersion.Checked = value;
            }
        }

        /// <summary>
        /// Get or Set if the Version Max. Should be include in the MetaData.
        /// </summary>
        internal bool UseVersionMin
        {
            get { return _useVersionMin; }
            set
            {
                _useVersionMin = value;
                chkBxIncludeMinVersion.Checked = value;
            }
        }

        /// <summary>
        /// Get or set the language of the update.
        /// </summary>
        internal int Language
        {
            get { return _language; }
            set
            {
                if (_languaguesByInt.ContainsKey(value))
                {
                    _language = value;
                    cmbBxLanguage.SelectedItem = _languaguesByInt[value];
                }

            }
        }

        #endregion

        #region(Responses to events - Réponses aux événements)

        private void txtBxMsiCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxMsiCode.Text) && regExp.IsMatch(txtBxMsiCode.Text))
            {
                MsiProductCode = new Guid(txtBxMsiCode.Text);
                btnOk.Enabled = true;
            }
            else
                btnOk.Enabled = false;
        }

        private void chkBxReverseRule_CheckedChanged(object sender, EventArgs e)
        {
            ReverseRule = chkBxReverseRule.Checked;
        }

        private void chkBxIncludeMaxVersion_CheckedChanged(object sender, EventArgs e)
        {
            nupVersionMax1.Enabled = chkBxIncludeMaxVersion.Checked;
            nupVersionMax2.Enabled = chkBxIncludeMaxVersion.Checked;
            nupVersionMax3.Enabled = chkBxIncludeMaxVersion.Checked;
            nupVersionMax4.Enabled = chkBxIncludeMaxVersion.Checked;

            _useVersionMax = chkBxIncludeMaxVersion.Checked;
        }

        private void chkBxIncludeMinVersion_CheckedChanged(object sender, EventArgs e)
        {
            nupVersionMin1.Enabled = chkBxIncludeMinVersion.Checked;
            nupVersionMin2.Enabled = chkBxIncludeMinVersion.Checked;
            nupVersionMin3.Enabled = chkBxIncludeMinVersion.Checked;
            nupVersionMin4.Enabled = chkBxIncludeMinVersion.Checked;

            _useVersionMin = chkBxIncludeMinVersion.Checked;
        }

        private void cmbBxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language = _languagesByString[cmbBxLanguage.SelectedItem.ToString()];
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbBxLanguage.Text) || !_languagesByString.ContainsKey(cmbBxLanguage.Text))
                Language = -1;
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Abort;
        }

        private void nupVersionMax1_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, 5);
        }

        #endregion

    }
}
