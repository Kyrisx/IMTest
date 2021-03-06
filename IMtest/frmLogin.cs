﻿using System;
using System.Windows.Forms;

namespace IMtest
{
    public partial class frmLogin : Form
    {
        private int _userId;

        public frmLogin()
        {
            _userId = 0;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // DEBUG -------------------
            textBox1.Text = "kyrisx";
            textBox2.Text = "gregory1";
            // DEBUG -------------------

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;

            clsUser login = new clsUser(textBox1.Text, textBox2.Text);
            ConnectivityBLL connBll = new ConnectivityBLL(login);

            try
            {
                _userId =  connBll.UserLogin();
                label5.Visible = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch(UserException ex)
            {
                switch(ex.Number)
                {
                    case 0:
                        label3.Visible = true;
                        break;
                    case 1:
                        label4.Visible = true;
                        break;
                    case 2:
                        label3.Visible = true;
                        label4.Visible = true;
                        break;
                }
                textBox1.Enabled = true;
                textBox2.Enabled = true;

            }
        }

        public int UserId
        {
            get
            {
                return _userId;
            }
        }
    }
}
