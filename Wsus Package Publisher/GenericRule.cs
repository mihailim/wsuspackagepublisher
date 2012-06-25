using System.Drawing;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal abstract partial class GenericRule : UserControl
    {
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

        internal abstract string GetRtfFormattedRule(string rtf, int tabulation);

        internal abstract string GetXmlFormattedRule();

        public abstract override string ToString();
    }
}
