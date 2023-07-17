using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AForm.Forms.Old.Parsers
{
    public partial class PriceFutureSettinName : Form
    {
        Action<string> action;
        public PriceFutureSettinName(Action<string> action)
        {
            InitializeComponent();
            this.action = action;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                action(txtName.Text);
                Close();
            }
        }
    }
}
