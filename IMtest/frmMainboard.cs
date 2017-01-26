using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMtest
{
    public partial class frmMainboard : Form
    {
        private int _UserId;
        public frmMainboard(int UserId)
        {
            _UserId = UserId;
            InitializeComponent();

            LoadUser();
            
        }

        private void LoadUser()
        {
            LoadUserBLL bll = new LoadUserBLL(_UserId);
            List<clsContactBasic> cb = new List<clsContactBasic>();

            bll.LoadUserContacts(cb);

            foreach (clsContactBasic item in cb)
            {
                listBox1.Items.Add(item.UserName);
            }
        }

        private void addContactToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                if (!tabControl1.Enabled) tabControl1.Enabled = true;
                tabControl1.TabPages.Add(listBox1.Items[index].ToString());
            }
        }
    }
}
