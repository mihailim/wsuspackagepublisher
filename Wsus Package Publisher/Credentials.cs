using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class Credentials : Form
    {        
        internal Credentials()
        {
            InitializeComponent();
        }

        internal string Login { get; private set; }

        internal string Password { get; private set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Login = txtBxLogin.Text;
            Password = txtBxPassword.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
