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
    internal partial class RuleFileExists : GenericRule
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

        private Dictionary<int, string> _csidlByCode = new Dictionary<int, string>()
        {
            {0x2F,"COMMON_ADMINTOOLS"},
            {0x1E ,"COMMON_ALTSTARTUP" },
            {0x23,"COMMON_APPDATA" },
            {0x19 ,"COMMON_DESKTOPDIRECTORY" },
            {0x2E ,"COMMON_DOCUMENTS" },
            {0x1F ,"COMMON_FAVORITES" },
            {0x17 ,"COMMON_PROGRAMS" },
            {0x16 ,"COMMON_STARTMENU" },
            {0x18 ,"COMMON_STARTUP" },
            {0x2D ,"COMMON_TEMPLATES" },
            {0x3 ,"CONTROLS" },
            {0x11 ,"DRIVES" },
            {0x14 ,"FONTS" },
            {0x4 ,"PRINTERS" },
            {0x26 ,"PROGRAM_FILES" },
            {0x2B ,"PROGRAM_FILES_COMMON" },
            {0x2C ,"PROGRAM_FILES_COMMONX86" },
            {0x2A ,"PROGRAM_FILESX86" },
            {0x2 ,"PROGRAMS" },
            {0x25 ,"SYSTEM" },
            {0x29 ,"SYSTEMX86" },
            {0x24 ,"WINDOWS" }
        };
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleFileExists).Assembly);

        public RuleFileExists():base()
        {
            InitializeComponent();

            nupFileSize.Maximum = long.MaxValue;
            foreach (string knownFolder in _csidlByName.Keys)
            {
                cmbBxKnowFolders.Items.Add(knownFolder);
            }
            foreach (string language in _languagesByString.Keys)
            {
                cmbBxLanguage.Items.Add(language);
            }

            txtBxDescription.Text = resMan.GetString("DescriptionFileExists");
            txtBxFolderPath.Focus();
        }

        #region {Methods - Méthodes}

        internal override string GetRtfFormattedRule(string rtf, int tabulation)
        {
            RichTextBox rTxtBx = new RichTextBox();
            string tab = new string(' ', tabulation);

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, tab);

            if (ReverseRule)
            {
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "<lar:");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">\r\n" + tab + tab);
            }

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "<bar:");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "FileExists");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Path");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, FilePath);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            if (UseCsidl)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Csidl");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Csidl.ToString());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }
            if (UseVersion)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Version");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Version);
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

            if (UseModifiedDate)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Modified");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, ModifiedDate.ToString());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

            if (UseCreationDate)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Created");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, CreationDate.ToString());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

            if (UseSize)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Size");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, FileSize.ToString());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }
                        
            if (UseLanguage)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Language");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Language.ToString());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "/>");

            if (ReverseRule)
            {
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\r\n");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, tab);
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "<lar:");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">");
            }

            return rTxtBx.Rtf;
        }

        internal override string GetXmlFormattedRule()
        {
            string result = "";

            if (ReverseRule)
                result += "<lar:Not>\r\n";

            result += "<bar:FileExists Path=\"" + FilePath + "\"";

            if (UseCsidl)
                result += " Csidl=\"" + Csidl.ToString() + "\"";

            if (UseVersion)
                result += " Version=\"" + Version + "\"";

            if (UseCreationDate)
                result += " Created=\"" + CreationDate + "\"";

            if (UseModifiedDate)
                result += " Modified=\"" + ModifiedDate.ToString() + "\"";

            if (UseSize)
                result += " Size=\"" + FileSize.ToString() + "\"";

            if (UseLanguage)
                result += " Language=\"" + Language.ToString() + "\"";

            result += "/>\r\n";

            if (ReverseRule)
                result += "</lar:Not>\r\n";

            return result;
        }

        internal override GenericRule Clone()
        {
            RuleFileExists clone = new RuleFileExists();

            clone.UseCsidl = this.UseCsidl;
            if (UseCsidl)
                clone.Csidl = this.Csidl;
            clone.FilePath = this.FilePath;
            clone.ReverseRule = this.ReverseRule;
            clone.UseVersion = this.UseVersion;
            if (UseVersion)
                clone.Version = this.Version;
            clone.UseCreationDate = this.UseCreationDate;
            if (UseCreationDate)
                clone.CreationDate = this.CreationDate;
            clone.UseModifiedDate = this.UseModifiedDate;
            if (UseModifiedDate)
                clone.ModifiedDate = this.ModifiedDate;
            clone.UseSize = this.UseSize;
            if (UseSize)
                clone.FileSize = this.FileSize;
            clone.UseLanguage = this.UseLanguage;
            if (UseLanguage)
                clone.Language = this.Language;

            return clone;
        }

        public override string ToString()
        {
            return resMan.GetString("FileExists");
        }

        /// <summary>
        /// Determines whether or not the string passed in parameters is compliant with the RegExp : "^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$}"
        /// </summary>
        /// <param name="version">The string to check against Regexp.</param>
        /// <returns>True if the string match, else false.</returns>
        private bool IsVersionStringCorrectlyformated(string version)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$");

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
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\d{1,5}");
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

        /// <summary>
        /// Get or Set the wellknown folder Id.
        /// </summary>
        internal int Csidl
        {
            get
            {
                if (chkBxKnownFolder.Checked)
                    return _csidlByName[cmbBxKnowFolders.SelectedItem.ToString()];
                else
                    return 0;
            }
            set
            {
                if (_csidlByCode.ContainsKey(value))
                {
                    cmbBxKnowFolders.SelectedItem = _csidlByCode[value];
                    chkBxKnownFolder.Checked = true;
                }
            }

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
        /// Get or Set if the Csidl should be use.
        /// </summary>
        internal bool UseCsidl
        {
            get { return chkBxKnownFolder.Checked; }
            set { chkBxKnownFolder.Checked = value; }
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

        private void chkBxKnownFolder_CheckedChanged(object sender, EventArgs e)
        {
            cmbBxKnowFolders.Enabled = chkBxKnownFolder.Checked;
        }

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

        private void nupVersion1_Enter(object sender, EventArgs e)
        {
            NumericUpDown nup = (NumericUpDown)sender;
            nup.Select(0, nup.Value.ToString().Length);
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
