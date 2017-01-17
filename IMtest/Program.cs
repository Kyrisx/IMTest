using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMtest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (LoginPhase())
            {

            }
        }
        private static bool LoginPhase()
        {
            frmLogin form = new frmLogin();
            form.ShowDialog();
            //Application.Run(form);

            if (form.DialogResult == DialogResult.OK)
            {
                return true;
            }
            return false;
        }
    }
}
