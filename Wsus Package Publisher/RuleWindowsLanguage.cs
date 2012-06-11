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

        internal RuleWindowsLanguage()
        {
            InitializeComponent();

            txtBxDescription.Text = resManager.GetString("DescriptionWindowsLanguage");
            cmbBxLanguage.Focus();
        }

        #region (Properties - Propriétés)

        internal override GenericRule.ObjectType TypeOfObject
        {
            get { return GenericRule.ObjectType.Rule; }
        }

        internal override string RuleType
        {
            get { return "WindowsLanguage"; }
        }

        internal override GenericRule.GroupLogicalOperator GroupType
        {
            get { return GroupLogicalOperator.None; }
            set { }
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

        #endregion

        #region (Methods - Méthodes)

        internal override string GetRtfFormattedRule(string rtf, int tabulation)
        {
            RichTextBox rTxtBx = new RichTextBox();
            string tab = new string(' ', tabulation);
            rTxtBx.Rtf += rtf;
            rTxtBx.Select(rTxtBx.Text.Length - 1, 1);

            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, tab);

            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "<bar:");
            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.red, "WindowsLanguage");

            print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Language");
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
            print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Language);
            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");

            print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "/>\r\n");

            return rTxtBx.Rtf;
        }

        internal override string GetXmlFormattedRule()
        {
            return "<bar:WindowsLanguage Language=\"" + Language + "\"/>\r\n";
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
