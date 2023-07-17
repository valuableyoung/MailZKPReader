using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AForm.Base
{
    public partial class WCheck : AWindow
    {
        DataTable _tCheck;

        public DataTable TCheck { get { return _tCheck; } }
        public WCheck( DataTable table)
        {
            InitializeComponent();
            _tCheck = table;
        }

        public override void LoadControls()
        {
            var repository = new RepositoryItemCheckEdit();
            repository.ValueChecked = 1;
            repository.ValueUnchecked = 0;

            tMain.AddColumn("fCheck", " ", new ColParam() { Repository = repository });
            tMain.AddColumn("Id", "Код");
            tMain.AddColumn("Name", "Наименование");

            tMain.AddButton("Принять", Properties.Resources.accept, Accept);
            this.DialogResult = DialogResult.None;
        }

        public void Accept(object sender, EventArgs e)
        {
            AcceptAllTable(this);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        public override void LoadData(object sender, EventArgs e)
        {
            tMain.DataSource = _tCheck;
        }

        public override void AWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

    }
}
