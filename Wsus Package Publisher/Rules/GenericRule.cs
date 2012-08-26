using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;

namespace Wsus_Package_Publisher
{
    internal abstract partial class GenericRule : UserControl
    {
        private bool _isSelected = false;
        private System.Guid _guid;
        private Dictionary<string, string> _unsupportedAttributes = new Dictionary<string, string>();

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

        internal abstract bool ReverseRule { get; set; }

        internal abstract string XmlElementName
        {
            get;
        }

        internal Dictionary<string, string> UnsupportedAttributes
        {
            get { return _unsupportedAttributes; }
        }

        #endregion

        #region (Methods - Méthodes)

        internal void print(RichTextBox rTxtBx, System.Drawing.Font font, Color color, string text)
        {
            rTxtBx.SelectionFont = font;
            rTxtBx.SelectionColor = color;
            rTxtBx.SelectedText += text;
        }

        internal abstract string GetRtfFormattedRule();

        internal string GetXmlFormattedRule()
        {
            RichTextBox rTxtBxTemp = new RichTextBox();
            rTxtBxTemp.Rtf = GetRtfFormattedRule();
            return rTxtBxTemp.Text;
        }

        internal abstract void InitializeWithAttributes(Dictionary<string, string> attributes);

        internal abstract GenericRule Clone();

        public abstract override string ToString();

        #endregion


    }
}
