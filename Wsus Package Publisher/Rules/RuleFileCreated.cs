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
    internal partial class RuleFileCreated : GenericRule
    {
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

        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleFileCreated).Assembly);

        public RuleFileCreated():base()
        {
            InitializeComponent();

            foreach (string knownFolder in _csidlByName.Keys)
            {
                cmbBxCsidl.Items.Add(knownFolder);
            }

            txtBxDescription.Text = resMan.GetString("DescriptionFileCreated");
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThan"));
            txtBxFilePath.Focus();
        }
        
        #region (Methods - Méthodes)

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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "FileCreated");
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

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Comparison");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Comparison);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Created");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, GetFormatedDate());
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

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
            {
                result += "<lar:Not>\r\n";
            }

            result += "<bar:FileCreated Path=\"" + FilePath + "\"";

            if (UseCsidl)
            {
                result += " Csidl=\"" + Csidl.ToString() + "\"";
            }

            result += " Comparison=\"" + Comparison + "\"";

            result += " Created=\"" + GetFormatedDate() + "\"";

            result += "/>\r\n";

            if (ReverseRule)
            {
                result += "</lar:Not>\r\n";
            }

            return result;
        }

        internal override GenericRule Clone()
        {
            RuleFileCreated clone = new RuleFileCreated();

            clone.UseCsidl = this.UseCsidl;
            clone.Csidl = this.Csidl;
            clone.FilePath = this.FilePath;
            clone.ReverseRule = this.ReverseRule;
            clone.Comparison = this.Comparison;
            clone.CreationDate = this.CreationDate;
            clone.Hour = this.Hour;
            clone.Minute = this.Minute;
            clone.Second = this.Second;
            return clone;
        }

        private string GetFormatedDate()
        {
            DateTime tempDate = new DateTime(CreationDate.Year, CreationDate.Month, CreationDate.Day, Hour, Minute, Second);

            return string.Format("{0:s}", tempDate);        
        }

        public override string ToString()
        {
            return resMan.GetString("FileCreated");
        }
        
        private void ValidateData()
        {
            if ((chkBxUseCsidl.Checked == false || (chkBxUseCsidl.Checked && cmbBxCsidl.SelectedIndex != -1)) && !string.IsNullOrEmpty(txtBxFilePath.Text) && cmbBxComparison.SelectedIndex != -1)
                btnOk.Enabled = true;
            else
                btnOk.Enabled = false;
        }

        #endregion

        #region (Properties - Propriétés)

        /// <summary>
        /// Get or Set if the Csidl should be use.
        /// </summary>
        internal bool UseCsidl
        {
            get { return chkBxUseCsidl.Checked; }
            set { chkBxUseCsidl.Checked = value; }
        }

        /// <summary>
        /// Get or Set the wellknown folder Id.
        /// </summary>
        internal int Csidl
        {
            get
            {
                if (chkBxUseCsidl.Checked)
                    return _csidlByName[cmbBxCsidl.SelectedItem.ToString()];
                else
                    return 0;
            }
            set
            {
                if (_csidlByCode.ContainsKey(value))
                {
                    cmbBxCsidl.SelectedItem = _csidlByCode[value];
                    chkBxUseCsidl.Checked = true;
                }
            }

        }

        /// <summary>
        /// Get or Set the file path.
        /// </summary>
        internal string FilePath
        {
            get { return txtBxFilePath.Text; }
            set { txtBxFilePath.Text = value; }
        }

        /// <summary>
        /// Get or Set if the rule should be reverse.
        /// </summary>
        internal bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
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
        /// Get or Set the Creation Date of the file.
        /// </summary>
        internal DateTime CreationDate
        {
            get { return dtpCreationDate.Value; }
            set { dtpCreationDate.Value = value; }
        }

        internal int Hour
        {
            get { return (int)nupHour.Value; }
            set { nupHour.Value = value; }
        }

        internal int Minute
        {
            get { return (int)nupMinute.Value; }
            set { nupMinute.Value = value; }
        }

        internal int Second
        {
            get { return (int)nupSeconde.Value; }
            set { nupSeconde.Value = value; }
        }

        #endregion

        #region (responses to Events - réponses aux événements)

        private void chkBxUseCsidl_CheckedChanged(object sender, EventArgs e)
        {
            cmbBxCsidl.Enabled = chkBxUseCsidl.Checked;
            ValidateData();
        }

        private void txtBxFilePath_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxCsidl_SelectedValueChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void nupHour_Enter(object sender, EventArgs e)
        {
            NumericUpDown nup = (NumericUpDown)sender;
            nup.Select(0, nup.Value.ToString().Length);
        }

        private void bntCancel_Click(object sender, EventArgs e)
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
