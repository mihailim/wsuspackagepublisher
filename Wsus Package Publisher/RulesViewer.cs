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
    internal partial class RulesViewer : UserControl
    {
        RulesGroup _rules;
        int tab = 0;
        internal static Font normalFont = new Font("Arial", 8, FontStyle.Regular);
        internal static Font elementAndAttributeFont = new Font("Arial", 9, FontStyle.Regular);
        internal static Font boldFont = new Font("Arial", 9, FontStyle.Bold);
        internal static Color green = System.Drawing.Color.ForestGreen;
        internal static Color black = System.Drawing.Color.Black;
        internal static Color red = System.Drawing.Color.Red;
        internal static Color blue = System.Drawing.Color.RoyalBlue;
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RulesViewer).Assembly);


        public RulesViewer()
        {
            InitializeComponent();
        }

        #region (Methods - Méthodes)

        private void DisplayRules()
        {
            tab = 0;

            rtxtBxRulesViewer.SuspendLayout();
            rtxtBxRulesViewer.Text = "";

            DisplayGroup(Rules);

            rtxtBxRulesViewer.ResumeLayout();
        }

        private void DisplayGroup(RulesGroup group)
        {
            RulesGroup.GroupLogicalOperator logicalOperator = group.GroupType;

            if (logicalOperator == RulesGroup.GroupLogicalOperator.And)
            {
                print(normalFont, green, resManager.GetString("GroupStart"));
                print(boldFont, black, resManager.GetString("RuleAnd"));
                print(normalFont, green, ">");
                rtxtBxRulesViewer.SelectedText += "\r\n";
            }
            else
            {
                print(normalFont, green, resManager.GetString("GroupStart"));
                print(boldFont, black, resManager.GetString("RuleOR"));
                print(normalFont, green, ">");
                rtxtBxRulesViewer.SelectedText += "\r\n";
            }

            tab += 3;

            foreach (Object rule in group.FormList.Values)
            {
                if (typeof(Rule) == typeof(RulesGroup))
                    DisplayGroup((RulesGroup)rule);
                else
                    DisplayRule((GenericRule)rule);
            }

            if (logicalOperator == RulesGroup.GroupLogicalOperator.And)
            {
                print(normalFont, green, resManager.GetString("GroupEnd"));
                print(boldFont, black, resManager.GetString("RuleAnd"));
                print(normalFont, green, ">");
                rtxtBxRulesViewer.SelectedText += "\r\n";
            }
            else
            {
                print(normalFont, black, resManager.GetString("GroupEnd"));
                print(boldFont, black, resManager.GetString("RuleOR"));
                print(normalFont, black, ">");
                rtxtBxRulesViewer.SelectedText += "\r\n";
            }

            tab -= 3;
        }

        private void DisplayRule(GenericRule rule)
        {
            rtxtBxRulesViewer.Rtf = rule.GetRtfFormattedRule(rtxtBxRulesViewer.Rtf, tab);
            rtxtBxRulesViewer.Select(rtxtBxRulesViewer.Text.Length - 1, 1);
        }

        private void print(System.Drawing.Font font, Color color, string text)
        {
            rtxtBxRulesViewer.SelectionFont = font;
            rtxtBxRulesViewer.SelectionColor = color;
            rtxtBxRulesViewer.SelectedText += text;
        }


        #endregion

        #region (Properties - Propriétées)

        internal RulesGroup Rules
        {
            get { return _rules; }
            set
            {
                _rules = value;
                DisplayRules();
            }
        }

        #endregion

    }
}
