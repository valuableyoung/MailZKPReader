using ALogic.Model.EntityFrame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AForm.Forms.Kontr
{
    public partial class CompanySimple : Form
    {
        Company _company;
        public CompanySimple(Company company)
        {
            InitializeComponent();

            _company = company;
        }

        public void LoadData()
        {
            _dlc.DataSource = _company;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelControl1.Focus();
            

            var c = Context.Get();
            c.CleverAttach(_company);
            c.SaveChanges();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CompanySimple_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
