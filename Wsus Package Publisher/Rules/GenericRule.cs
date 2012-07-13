using System.Drawing;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal abstract partial class GenericRule : UserControl
    {
        private bool _isSelected = false;
        private System.Guid _guid;

        internal GenericRule()
        {
            InitializeComponent();
            _guid = System.Guid.NewGuid();
        }

        #region (Properties - Propriétés)

        internal bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        internal System.Guid Id
        {
            get { return _guid; }
        }

        #endregion

        internal void print(RichTextBox rTxtBx, System.Drawing.Font font, Color color, string text)
        {
            rTxtBx.SelectionFont = font;
            rTxtBx.SelectionColor = color;
            rTxtBx.SelectedText += text;
        }

        internal abstract string GetRtfFormattedRule(string rtf, int tabulation);

        internal abstract string GetXmlFormattedRule();

        internal abstract GenericRule Clone();

        public abstract override string ToString();


    }
}
