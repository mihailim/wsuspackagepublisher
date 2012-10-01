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
    public partial class FrmWaiting : Form
    {
        private bool _goOn = true;
        private string _description = "";

        internal FrmWaiting()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
        }

        internal bool GoOn
        {
            get
            { return _goOn; }
            set
            { _goOn = value; }
        }

        internal string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                lblDescription.Text = _description;
            }
        }

        internal void ShowForm()
        {
            this.Show();
            this.Refresh();

            while (GoOn)
            {
                System.Threading.Thread.Sleep(100);
                pctBxWaiting.Refresh();
            }
            this.Close();
        }

        //    System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
        //    FrmWaiting waiter = new FrmWaiting();
        //    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(waiter.ShowForm));
        //    t.Priority = System.Threading.ThreadPriority.AboveNormal;
        //    t.Start();
        //    this.Refresh();
        //    System.Threading.Thread.Sleep(200);

        // Do some Work

        //    waiter.GoOn = false;
        //    t.Join(500);
        //    t = null;
        //    waiter = null;
        //    System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
        //    this.Focus();
    }
}
