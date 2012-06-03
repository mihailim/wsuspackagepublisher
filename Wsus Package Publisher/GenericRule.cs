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
    internal partial class GenericRule : UserControl
    {
        public enum ObjectType { Group, Rule }
        public enum GroupLogicalOperator {And, Or, None}

        internal GenericRule()
        {
            InitializeComponent();
        }

        internal void print(RichTextBox rTxtBx, System.Drawing.Font font, Color color, string text)
        {
            rTxtBx.SelectionFont = font;
            rTxtBx.SelectionColor = color;
            rTxtBx.SelectedText += text;
        }

        internal virtual string GetRtfFormattedRule(string rtf, int tabulation)
        {
            return "";
        }

        internal virtual string GetXmlFormattedRule()
        {
            return "";
        }

        #region (Properties - Propriétés)

        internal virtual ObjectType TypeOfObject
        {
            get { return ObjectType.Group; }
        }

        internal virtual string RuleType
        {
            get { return ""; }
        }

        internal virtual GroupLogicalOperator GroupType
        {
            get { return GroupLogicalOperator.None; }
            set { }
        }

        #endregion

    }
}
