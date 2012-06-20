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
    internal partial class RuleFileExistsPrependRegSz : GenericRule
    {
        private Dictionary<string, int> _languagesByString = new Dictionary<string, int>
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
        private Dictionary<int, string> _languaguesByInt = new Dictionary<int, string>
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
        private Dictionary<string, int> _csidlByName = new Dictionary<string, int>()
        {
            {"COMMON_ADMINTOOLS" , 0x2F},
            {"COMMON_ALTSTARTUP", 0x1E},
            {"COMMON_APPDATA", 0x23},
            {"COMMON_DESKTOPDIRECTORY", 0x19},
            {"COMMON_DOCUMENTS", 0x2E},
            {"COMMON_FAVORITES", 0x1F},
            {"COMMON_PROGRAMS", 0x17},
            {"COMMON_STARTMENU", 0x16},
            {"COMMON_STARTUP", 0x18},
            {"COMMON_TEMPLATES", 0x2D},
            {"CONTROLS", 0x3},
            {"DRIVES", 0x11},
            {"FONTS", 0x14},
            {"PRINTERS", 0x4},
            {"PROGRAM_FILES", 0x26},
            {"PROGRAM_FILES_COMMON", 0x2B},
            {"PROGRAM_FILES_COMMONX86", 0x2C},
            {"PROGRAM_FILESX86", 0x2A},
            {"PROGRAMS", 0x2},
            {"SYSTEM", 0x25},
            {"SYSTEMX86", 0x29},
            {"WINDOWS", 0x24}
        };

        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager(typeof(RuleFileExistsPrependRegSz));

        internal RuleFileExistsPrependRegSz()
        {
            InitializeComponent();

            nupFileSize.Maximum = long.MaxValue;
            foreach (string language in _languagesByString.Keys)
            {
                cmbBxLanguage.Items.Add(language);
            }

            txtBxDescription.Text = resMan.GetString("DescriptionFileExistsPrependRegSz");
            txtBxFolderPath.Focus();
        }

        #region {Methods - Méthodes}

        private void ValidateData()
        {
            btnOk.Enabled = false;

            if (!string.IsNullOrEmpty(txtBxSubKey.Text) && !string.IsNullOrEmpty(txtBxValue.Text) && !string.IsNullOrEmpty(txtBxFolderPath.Text))
                if (txtBxSubKey.Text.Length >= 1 && txtBxSubKey.Text.Length <= 255 && txtBxValue.Text.Length >= 0 && txtBxValue.Text.Length <= 16383)
                    btnOk.Enabled = true;
        }

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
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.red, "FileExistsPrependRegSz");
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


            if (UseVersion)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Version");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Version);
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

            if (UseModifiedDate)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Modified");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, ModifiedDate.ToString());
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

            if (UseCreationDate)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Created");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, CreationDate.ToString());
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

            if (UseSize)
            {
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Size");
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Size.ToString());
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
            }

            if (UseLanguage)
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

            result += "<bar:FileExistsPrependRegSz Key=\"HKEY_LOCAL_MACHINE\"";
            result += " Subkey=\"" + SubKey + "\"";
            result += " Value=\"" + Value + "\"";
            result += " Path=\"" + FilePath + "\"";

            if (RegType32)
            {
                result += " RegType32=\"" + RegType32.ToString() + "\"";
            }

            if (UseVersion)
            {
                result += " Version=\"" + Version + "\"";
            }

            if (UseCreationDate)
            {
                result += " Created=\"" + CreationDate + "\"";
            }

            if (UseModifiedDate)
            {
                result += " Modified=\"" + ModifiedDate.ToString() + "\"";
            }

            if (UseSize)
            {
                result += " Size=\"" + Size.ToString() + "\"";
            }

            if (UseLanguage)
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

        #region {Properties - Propriétés}

        internal override GenericRule.ObjectType TypeOfObject
        {
            get { return GenericRule.ObjectType.Rule; }
        }

        internal override string RuleType
        {
            get { return "FileExists"; }
        }

        internal override GenericRule.GroupLogicalOperator GroupType
        {
            get { return GroupLogicalOperator.None; }
            set { }
        }

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
            get { return txtBxValue.Text; }
            set { txtBxValue.Text = Value; }
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
            get { return txtBxFolderPath.Text; }
            set { txtBxFolderPath.Text = value; }
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
        /// Get or Set if the file version informations should be used.
        /// </summary>
        internal bool UseVersion
        {
            get { return chkBxFileVersion.Checked; }
            set { chkBxFileVersion.Checked = value; }
        }

        /// <summary>
        /// Get or Set if the Creation Date informations should be used.
        /// </summary>
        internal bool UseCreationDate
        {
            get { return chkBxCreationDate.Checked; }
            set { chkBxCreationDate.Checked = value; }
        }

        /// <summary>
        /// Get or Set if the file Modified Date informations should be used.
        /// </summary>
        internal bool UseModifiedDate
        {
            get { return chkBxModifiedDate.Checked; }
            set { chkBxModifiedDate.Checked = value; }
        }

        /// <summary>
        /// Get or Set if the file sIze informations should be used.
        /// </summary>
        internal bool UseSize
        {
            get { return chkBxFileSize.Checked; }
            set { chkBxFileSize.Checked = value; }
        }

        /// <summary>
        /// Get or Set if the file language informations should be used.
        /// </summary>
        internal bool UseLanguage
        {
            get { return chkBxLanguage.Checked; }
            set { chkBxLanguage.Checked = value; }
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
                    chkBxFileVersion.Checked = true;
                }
                else
                {
                    chkBxFileVersion.Checked = false;
                }
            }
        }

        /// <summary>
        /// Get or Set the Creation Date of the file.
        /// </summary>
        internal DateTime CreationDate
        {
            get { return dtPCreationDate.Value; }
            set
            {
                dtPCreationDate.Value = value;
                chkBxCreationDate.Checked = true;
            }
        }

        /// <summary>
        /// Get or Set the Modified Date of the file.
        /// </summary>
        internal DateTime ModifiedDate
        {
            get { return dtPModifiedDate.Value; }
            set
            {
                dtPModifiedDate.Value = value;
                chkBxModifiedDate.Checked = true;
            }
        }

        /// <summary>
        /// Get or Set the size of the file.
        /// </summary>
        internal int FileSize
        {
            get { return (int)nupFileSize.Value; }
            set
            {
                nupFileSize.Value = value;
                chkBxFileSize.Checked = true;
            }
        }

        /// <summary>
        /// Get or Set the language of the file.
        /// </summary>
        internal int Language
        {
            get { return _languagesByString[cmbBxLanguage.SelectedItem.ToString()]; }
            set { cmbBxLanguage.SelectedItem = _languaguesByInt[value]; }
        }


        #endregion

        #region {Response to Events - Réponses aux évènements}

        private void chkBxFileVersion_CheckedChanged(object sender, EventArgs e)
        {
            nupVersion1.Enabled = chkBxFileVersion.Checked;
            nupVersion2.Enabled = chkBxFileVersion.Checked;
            nupVersion3.Enabled = chkBxFileVersion.Checked;
            nupVersion4.Enabled = chkBxFileVersion.Checked;
        }

        private void chkBxCreationDate_CheckedChanged(object sender, EventArgs e)
        {
            dtPCreationDate.Enabled = chkBxCreationDate.Checked;
        }

        private void chkBxModifiedDate_CheckedChanged(object sender, EventArgs e)
        {
            dtPModifiedDate.Enabled = chkBxModifiedDate.Checked;
        }

        private void chkBxFileSize_CheckedChanged(object sender, EventArgs e)
        {
            nupFileSize.Enabled = chkBxFileSize.Checked;
        }

        private void chkBxLanguage_CheckedChanged(object sender, EventArgs e)
        {
            cmbBxLanguage.Enabled = chkBxLanguage.Checked;
        }

        private void txtBxFolderPath_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = false;

            if (!string.IsNullOrEmpty(txtBxFolderPath.Text))
                if (txtBxFolderPath.Text.Length < 260)
                    btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void txtBxSubKey_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxValue_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxFolderPath_TextChanged_1(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion
    }
}
