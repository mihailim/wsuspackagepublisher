using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Wsus_Package_Publisher
{
    internal partial class RuleWindowsLanguage : GenericRule
    {
        private Dictionary<string, string> _languagesByName = new Dictionary<string, string>
        {   {"Arabic", "ar"},
            {"Chinese_HK_SAR", "zh-HK"},
            {"Chinese_simplified", "zh-CHS"},
            {"Chinese_traditional","zh-CHT"},
            {"Czech", "cs"},
            {"Danish", "da"}, 
            {"Dutch", "nl"},
            {"English", "en"},
            {"Finnish", "fi"}, 
            {"French", "fr"},    
            {"German", "de"},   
            {"Greek", "el"},   
            {"Hebrew", "he"},            
            {"Hungarian", "hu"},   
            {"Italian", "it"},              
            {"Japanese", "ja"},   
            {"Korean", "ko"}, 
            {"Norwegian", "no"},
            {"Polish", "pl"},
            {"Portugese", "pt"}, 
            {"Portugese_Brazil", "pt-br"}, 
            {"Russian", "ru"},
            {"Spanish", "es"}, 
            {"Swedish", "sv"}, 
            {"Turkish", "tr"}
        };

        private Dictionary<string, int> _languagesByLCID = new Dictionary<string, int>
        {   {"ar",0},
            {"zh-HK", 1},
            {"zh-CHS", 2},
            {"zh-CHT", 3},
            {"cs", 4},
            {"da", 5},
            {"nl", 6},
            {"en", 7},
            {"fi", 8},
            {"fr", 9},   
            {"de", 10},  
            {"el", 11},  
            {"he", 12},           
            {"hu", 13},  
            {"it", 14},             
            {"ja", 15},  
            {"ko", 16},
            {"no", 17},
            {"pl", 18},
            {"pt", 19},
            {"pt-br", 20},
            {"ru", 21},
            {"es", 22},
            {"sv", 23},
            {"tr", 24}
        };

        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleWindowsVersion).Assembly);

        public RuleWindowsLanguage()
            : base()
        {
            InitializeComponent();

            foreach (string name in _languagesByName.Keys)
            {
                cmbBxLanguage.Items.Add(name);
            }
            txtBxDescription.Text = resManager.GetString("DescriptionWindowsLanguage");
            cmbBxLanguage.Focus();
        }

        #region (Properties - Propriétés)

        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        internal string Language
        {
            get
            {
                if (cmbBxLanguage.SelectedIndex != -1)
                    return _languagesByName[cmbBxLanguage.SelectedItem.ToString()];
                else
                    return "";
            }
            set
            {
                if (_languagesByLCID.ContainsKey(value))
                    cmbBxLanguage.SelectedIndex = _languagesByLCID[value];
            }
        }

        internal override string XmlElementName
        {
            get { return "WindowsLanguage"; }
        }

        #endregion

        #region (Methods - Méthodes)

        internal override string GetRtfFormattedRule()
        {
            RichTextBox rTxtBx = new RichTextBox();

            if (ReverseRule)
            {
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "<lar:");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">\r\n");
            }

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "<bar:");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "WindowsLanguage");

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Language");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Language);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "/>");

            if (ReverseRule)
            {
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\r\n");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "</lar:");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">");
            }

            return rTxtBx.Rtf;
        }

        internal override GenericRule Clone()
        {
            RuleWindowsLanguage clone = new RuleWindowsLanguage();

            clone.ReverseRule = this.ReverseRule;
            clone.Language = this.Language;

            return clone;
        }

        public override string ToString()
        {
            return resManager.GetString("WindowsLanguage");
        }

        internal override void InitializeWithAttributes(Dictionary<string,string> attributes)
        {
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "Language":
                        this.Language = pair.Value;
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        #endregion

        #region (Responses to Events - Réponses aux évènements)

        private void cmbBxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxLanguage.SelectedIndex != -1)
                btnOk.Enabled = true;
            else
                btnOk.Enabled = false;
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
