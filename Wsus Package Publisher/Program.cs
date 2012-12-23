using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            try
            {
                Application.Run(new FrmWsusPackagePublisher());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
#else
            Application.Run(new FrmWsusPackagePublisher());
#endif

        }
    }
}
