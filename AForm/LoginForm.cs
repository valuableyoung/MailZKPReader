using AForm.ExelForm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForm.Base;
using ALogic.DBConnector;
using ALogic.Logic.SPR;


namespace AForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();         
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ReadCurrentUserValues();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            etLogin.Clear();
            etPassword.Clear();
            ClearValidation();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            ClearValidation();

            bool flag = false;

            if (etLogin.Text == "")
            {
                lbllogin.Visible = true;
                flag = true;
            }

            if (etPassword.Text == "")
            {
                lblpass.Visible = true;
                flag = true;
            }

            if (flag) return;

            if (User.LoginUser(etLogin.Text, etPassword.Text))
            {
                RememberMe(etLogin.Text, etPassword.Text);
                lblAttenshion.Visible = false;
                //MainForm mainform = new MainForm();

                WFMain mainform = new WFMain();

                mainform.FormClosed += Mainform_FormClosed;
                WindowOpener.MainForm = mainform;

                this.Hide();
                mainform.Show();
            }
            else
            {
                lblAttenshion.Visible = true;
            }

        }

        private string regkey = @"SOFTWARE\Kis3.0";

        private void RememberMe(string login, string password)
        {
            if (ckRememberMe.Checked)
            {
                using (var key = Registry.CurrentUser.CreateSubKey(regkey))
                {
                    key.SetValue("login", login);
                    key.SetValue("password", password);
                }
            }
            else
            {
                Registry.CurrentUser.DeleteSubKey(regkey);
            }
        }

        private void ReadCurrentUserValues()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(regkey))
            {
                if (key != null)
                {
                    etLogin.Text =  key.GetValue("login").ToString();
                    etPassword.Text = key.GetValue("password").ToString();
                }
            }
        }


        private void Mainform_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Show();
            this.Close();
        }
        private void ClearValidation()
        {
            lblpass.Visible = false;
            lbllogin.Visible = false;
            lblAttenshion.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
