using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ALogic.Logic.Heplers;
using ALogic.DBConnector;

namespace AForm.Forms.Old.Other
{
    public partial class ParserMskProductLine : Form
    {
        DataTable _data;
        public ParserMskProductLine()
        {
            InitializeComponent();
            this.Tag = "Загрузка ассортимента МСК";
        }

        private void ParserMskProductLine_Load(object sender, EventArgs e)
        {

        }

        private void bLoadFile_Click(object sender, EventArgs e)
        {
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var nFile = dlg.FileName;
                _data = FileReader.Read(nFile)[0];

                dgv.DataSource = _data; 
            }
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            if (_data == null)
            {
                MessageBox.Show("Сначала выберите файл");
                return;
            }

            if (rbDelAndLoad.Checked)
            {
                DBExecutor.ExecuteQuery("delete from MskProductLine");
            }

            foreach (var row in _data.AsEnumerable())
            {
                int id_tov;
                if (!int.TryParse(row[0].ToString(), out id_tov))
                    continue;
                int kol;
                if (!int.TryParse(row[1].ToString(), out kol))
                    continue;

                SqlParameter par1 = new SqlParameter("id_tov", id_tov);
                SqlParameter par2 = new SqlParameter("kol", kol);

                string sql = @"
if exists(select * from MskProductLine where idTov = @id_tov)
    update MskProductLine set kol = @kol where idTov = @id_tov
else
    insert into MskProductLine (idTov, kol) values(@id_tov, @kol)
";

                DBExecutor.ExecuteQuery(sql, par1, par2);
            }
        }
    }
}
