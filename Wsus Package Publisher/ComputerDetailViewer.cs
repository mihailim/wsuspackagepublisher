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
    public partial class ComputerDetailViewer : UserControl
    {
        public ComputerDetailViewer()
        {
            InitializeComponent();
        }

        internal void AddText(string text)
        {
            txtBxDetail.Text += text;
        }

        internal void ClearText()
        {
            txtBxDetail.Text = "";
        }
    }
}
