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
using ALogic.Logic.SPR.Old;
using ALogic.DBConnector;

namespace AForm.Forms.Old.Other
{
    public partial class WFLoadBuhRems : Form
    {
        public WFLoadBuhRems()
        {
            InitializeComponent();
            this.Tag = "Загрузка бухгалтерских остатков";
        }    

        private void btOpenFile_Click(object sender, EventArgs e)
        {
            if (dlgMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbFile.Text = dlgMain.FileName;

                gridControl1.DataSource = FileReader.Read_EXCEL(dlgMain.FileName, false)[0];
            }
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            dtDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);            
            DataTable dt = DBOurFirm.GetOurFirm();
            cbFirm.DataSource = dt;
            cbFirm.DisplayMember = "n_kontr";
            cbFirm.ValueMember = "IdFirm";

            dgvMail.DataSource = DBOurFirm.GetOurFirmRems(dtDate.Value);
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            try
            {
                DBDate.Date = dtDate.Value;
                string idFirm = cbFirm.SelectedValue.ToString();
                string wayToFile = tbFile.Text;

                DataTable dtContain = FileReader.Read_EXCEL(wayToFile, false)[0];

                SqlParameter parIdKontr = new SqlParameter("id_kontr", idFirm);
                SqlParameter parDate = new SqlParameter("date", DBDate.StrDate);

                DBExecutor.ExecuteQuery("delete from stockbase where id_kontr = @id_kontr and date_rest = @date", parIdKontr, parDate);

                if (dtContain.Rows.Count == 0)
                {
                    MessageBox.Show("Не удалось распарсить файл.");
                    return;
                }

                foreach (var row in dtContain.AsEnumerable())
                {
                    int idTov;
                    if (!(int.TryParse(row[0].ToString(), out idTov)))
                        continue;
                    decimal count;
                    if (!(decimal.TryParse(row[2].ToString(), out count)))
                        continue;

                    string query = "insert into stockbase values( " + idFirm + ", null, " + idTov.ToString() + ", '" + DBDate.StrDate + "', " + count.ToString() + ")";
                    DBGroupSaver.AddRow(query);
                }

                DBGroupSaver.SaveAll();


                dgvMail.DataSource = DBOurFirm.GetOurFirmRems(dtDate.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
