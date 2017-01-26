using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

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

            int UserId = LoginPhase();            
            if (UserId > 0)
            {
                OpenSocket();
                MainboardPhase(UserId);
            }
            return;
        }

        private static int LoginPhase()
        {
            frmLogin form = new frmLogin();
            form.ShowDialog();
            //Application.Run(form);

            if (form.DialogResult == DialogResult.OK)
            {
                return form.UserId;
            }
            return 0;
        }

        private static void OpenSocket()
        {
            //SocketServer.StartListening();
            SocketClient.StartClient();
        }

        private static void MainboardPhase(int UserId)
        {
            frmMainboard form = new frmMainboard(UserId);
            form.ShowDialog();

            return;
        }
    }
}
