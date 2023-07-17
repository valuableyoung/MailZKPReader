using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AForm.Forms.ArkonaBonus
{
    public partial class ArkonaBonusSettingsNew : Form
    {
        public ArkonaBonusSettingsNew()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Загрузка списков брендов, поставщиков. Заполнение полей с бонусами в промежуточной таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                LoadBrands();
                LoadSuppliers();
                LoadTovGroups(-1);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        public DataTable LoadTovGroups(int idbrand)
        {
            //string sql = @"select cast(spr_tov_level4.tov_id as int) tov_id,  spr_tov_level4.tov_name from spr_tov_level4 (nolock) where spr_tov_level4.tov_id_level = 1 order by spr_tov_level4.tov_name";
            string sql = @"select distinct cast(gr.tov_id as int) tov_id, gr.tov_name from spr_tm (nolock) inner join spr_tov (nolock) on spr_tov.id_tm = spr_tm.tm_id
                inner join spr_tov_level4 wt (nolock)on wt.tov_id = spr_tov.id_tov4
                inner join spr_tov_level4 gr(nolock) on gr.tov_id = wt.tov_id_top_level and gr.tov_id_level = 1
                where spr_tm.tm_id = @idtm or @idtm = -1 
                order by gr.tov_name";
            SqlParameter paridtm = new SqlParameter("idtm", idbrand);
            var dt = DBExecutor.SelectTable(sql, paridtm);
            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = " -- Все товарные группы --";
            dt.Rows.InsertAt(row, 0);
            cbTovGroup.DataSource = dt;
            cbTovGroup.DisplayMember = "tov_name";
            cbTovGroup.ValueMember = "tov_id";
            
            return dt;
        }

        public void LoadBrands(int idSupplier = 0)
        {
            cbBrand.DataSource = ALogic.Logic.SPR.Old.DBSprBrend.GetBrandsList(idSupplier);
            cbBrand.DisplayMember = "Name";
            cbBrand.ValueMember = "Id";
        }

        private void LoadSuppliers()
        {
            cbSupplier.DataSource = ALogic.Logic.SPR.Old.DBSprSupplier.GetSuppliersList();
            cbSupplier.DisplayMember = "Name";
            cbSupplier.ValueMember = "Id";
        }

        private void ArkonaBonusSettingsNew_Load(object sender, EventArgs e)
        {
            btnRefreshLoadData_Click(sender, e);
        }

        private void BrandListClear()
        {
            cbSupplier.SelectedIndex = 0;
            cbBrand.DataSource = null;
            
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex > 0)
            {
                LoadBrands(int.Parse(cbSupplier.SelectedValue.ToString()));
            }
            else
            {
                BrandListClear();
            }
        }

        public static DataTable GetArkonaBonusFraction(int idKontrTitle = 0, int idTm = 0, int idTovGr = -1)
        {
            string sql = @"select s.id, tm.tm_id, tm.tm_name, 
                cast(s.minLevelRRP * 100 as decimal(18,2)) as minLevelRRP, 
                cast(s.fractionPercent * 100 as decimal(18,2)) as fractionPercent,
                cast(s.BrandMarkup * 100 as decimal(18,2)) as BrandMarkup,
                s.tip, s.tipname, s.step
                from 
                sArkonaBonusFraction s (nolock)
                inner join spr_tm tm (nolock) on tm.tm_id = s.idBrand
                inner join rKontrTitleTm rt (nolock) on rt.idTm = tm.tm_id
                where 
                (rt.idKontrTitle = @idKontrTitle or @idKontrTitle = 0) and
                (rt.idTm = @idTm or @idTm = 0) and s.tovgr = @tovgr
                ";
            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);
            SqlParameter Paramtovgr = new SqlParameter("tovgr", idTovGr);

            return DBExecutor.SelectTable(sql, ParamidKontrTitle, ParamidTm, Paramtovgr);
        }

        public static DataTable GetArkonaBonusFractionForTovGr(int idKontrTitle = 0, int idTm = 0)
        {
            string sql = @"select s.id, tm.tm_id, l4.tov_name tovgrname, 
                        cast(s.minLevelRRP* 100 as decimal(18,2)) as minLevelRRP, 
                        cast(s.fractionPercent* 100 as decimal(18,2)) as fractionPercent,
                        cast(s.BrandMarkup* 100 as decimal(18,2)) as BrandMarkup,
                        datefrom,
                        dateto,
                        s.tovgr, s.tip, s.tipname, s.step
                        from
                        sArkonaBonusFraction s(nolock)
                        inner join spr_tm tm(nolock) on tm.tm_id = s.idBrand
                        inner join rKontrTitleTm rt (nolock) on rt.idTm = tm.tm_id
                        inner join spr_tov_level4 l4 (nolock) on l4.tov_id = s.tovgr
                        where 
                        (rt.idKontrTitle = @idKontrTitle or @idKontrTitle = 0) and
                        (rt.idTm = @idTm or @idTm = 0) and s.tovgr <> -1
                        order by s.tip, tovgrname";
            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);

            return DBExecutor.SelectTable(sql, ParamidKontrTitle, ParamidTm);
        }

        public static DataTable GetArkonaBonusFractionCalcPrices(int idKontrTitle = 0, int idTm = 0)
        {
            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);

            return DBExecutor.ExecuteProcedureTable("up_CalcPriceAFraction", ParamidKontrTitle, ParamidTm);
        }

        private void btnLoadTables_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex == 0 && cbBrand.SelectedIndex == 0)
                return;

            if (cbSupplier.SelectedIndex > 0 && cbBrand.SelectedIndex > 0)
            {
                var idx = gridViewBrandPricesBySKU.FocusedRowHandle;
                try
                {
                    //Cursor = Cursors.WaitCursor;

                    gridControlBrandSettingsNew.BeginUpdate();
                    gridControlBonusSettingsForTovGroup.BeginUpdate();
                    gridControlBrandPricesBySKU.BeginUpdate();

                    gridControlBrandSettingsNew.DataSource = GetArkonaBonusFraction((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue, -1);
                    gridControlBonusSettingsForTovGroup.DataSource = GetArkonaBonusFractionForTovGr((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);
                    //gridControlBrandPricesBySKU.DataSource = GetArkonaBonusFractionCalcPrices((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);
                }
                finally
                {
                    gridControlBrandSettingsNew.EndUpdate();
                    gridControlBonusSettingsForTovGroup.EndUpdate();
                    gridControlBrandPricesBySKU.EndUpdate();

                    gridViewBrandPricesBySKU.FocusedRowHandle = idx;
                    //Cursor = Cursors.Default;
                }
            }
        }

        private void btnAddBonusFractionSettings_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex == 0 && cbBrand.SelectedIndex == 0)
                return;

            if (cbBrand.SelectedIndex == 0 || cbBrand.SelectedValue == null)
            {
                MessageBox.Show("Не выбран бренд!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var idTm = (int)cbBrand.SelectedValue;

                string sql = $@"select count(*) cnt from sArkonaBonusFraction (nolock) where idbrand = {idTm} and tovgr = -1";
                var row = DBExecutor.SelectRow(sql);
                var cnt = row["cnt"];
                if ((int)cnt == 0)
                {
                    string sqlins = @"insert into sArkonaBonusFraction(idbrand, minlevelrrp, fractionpercent, brandmarkup, datefrom, dateto, tovgr, tip, tipname, step) 
                        values(@idtm, 0.0, 0.0, 0.0, getdate(), getdate(), -1, 1, 'А+', 4);
                            insert into sArkonaBonusFraction(idbrand, minlevelrrp, fractionpercent, brandmarkup, datefrom, dateto, tovgr, tip, tipname, step) 
                        values(@idtm, 0.0, 0.0, 0.0, getdate(), getdate(), -1, 2, 'Экзист', 4);";
                    SqlParameter partmid = new SqlParameter("idTm", idTm);
                    DBExecutor.ExecuteQuery(sqlins, partmid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Произошла ошибка: {ex.Message}");
                return;
            }
            finally
            {
                //btnLoadTables_Click(sender, e);
                gridControlBrandSettingsNew.DataSource = GetArkonaBonusFraction((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue, -1);
            }

        }

        private void btnSaveBonusFractionSettings_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex == 0 && cbBrand.SelectedIndex == 0)
                return;

            if (cbBrand.SelectedIndex == 0 || cbBrand.SelectedValue == null)
            {
                MessageBox.Show("Не выбран бренд!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var idTm = (int)cbBrand.SelectedValue;

                var rows = (gridViewBrandSettingsNew.GridControl.DataSource as DataTable).AsEnumerable();

                //int id = (int)gridViewBrandSettingsNew.GetFocusedDataRow()["id"];

                float minLevelRRP = 0.0f;
                float fractionPercent = 0.0f;
                float BrandMarkup = 0.0f;

                foreach (var item in rows/*.Where(p => (int)p["id"] == id)*/)
                {
                    minLevelRRP = Convert.ToSingle(item["minLevelRRP"]);
                    fractionPercent = Convert.ToSingle(item["fractionPercent"]);
                    BrandMarkup = Convert.ToSingle(item["BrandMarkup"]);
                    int id = Convert.ToInt32(item["id"]);

                    string sqlins = $@"update sArkonaBonusFraction 
                    set minLevelRRP = @minLevelRRP / 100.0, 
                    fractionPercent = @fractionPercent / 100.0, 
                    BrandMarkup = @BrandMarkup / 100.0 where idBrand = @idTm and tovgr = -1 and id = @id";
                    SqlParameter parminlevel = new SqlParameter("minLevelRRP", minLevelRRP);
                    SqlParameter parfracperc = new SqlParameter("fractionPercent", fractionPercent);
                    SqlParameter parbrandmark = new SqlParameter("BrandMarkup", BrandMarkup);
                    SqlParameter partmid = new SqlParameter("idTm", idTm);
                    SqlParameter parid = new SqlParameter("id", id);
                    var b = DBExecutor.ExecuteQuery(sqlins, parminlevel, parfracperc, parbrandmark, partmid, parid);
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Произошла ошибка: {ex.Message}");
                return;
            }
            finally
            {
                btnLoadTables_Click(sender, e);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnAddBonusSettingsByTovGr_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex == 0 && cbBrand.SelectedIndex == 0)
                return;

            if (cbBrand.SelectedIndex == 0 || cbBrand.SelectedValue == null)
            {
                MessageBox.Show("Не выбран бренд!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbTovGroup.SelectedIndex == 0 || cbTovGroup.SelectedValue == null)
            {
                MessageBox.Show("Не выбрана товарная группа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int idTm = (int)cbBrand.SelectedValue;
                int idTovGr = (int)cbTovGroup.SelectedValue;

                string sqlt = $@"select count(*) cnt from sArkonaBonusFraction (nolock) where idbrand = {idTm} and tovgr = -1";
                var rowt = DBExecutor.SelectRow(sqlt);
                var cntt = rowt["cnt"];
                if ((int)cntt == 0)
                {
                    MessageBox.Show("Не заданы настройки для бренда в целом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string sql = $@"select count(*) cnt from sArkonaBonusFraction (nolock) where idbrand = {idTm} and tovgr = {idTovGr}";
                var row = DBExecutor.SelectRow(sql);
                var cnt = row["cnt"];
                if ((int)cnt == 0)
                {
                    string sqlins = @"insert into sArkonaBonusFraction(idbrand, minlevelrrp, fractionpercent, brandmarkup, datefrom, dateto, tovgr, tip, tipname, step) 
                        values(@idtm, 0.0, 0.0, 0.0, getdate(), getdate(), @tovgr, 1, 'А+', 4);
                                    insert into sArkonaBonusFraction(idbrand, minlevelrrp, fractionpercent, brandmarkup, datefrom, dateto, tovgr, tip, tipname, step) 
                        values(@idtm, 0.0, 0.0, 0.0, getdate(), getdate(), @tovgr, 2, 'Экзист', 4)";
                    SqlParameter partmid = new SqlParameter("idTm", idTm);
                    SqlParameter partovgr = new SqlParameter("tovgr", idTovGr);
                    DBExecutor.ExecuteQuery(sqlins, partmid, partovgr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Произошла ошибка: {ex.Message}");
                return;
            }
            finally
            {
                //btnLoadTables_Click(sender, e);
                gridControlBonusSettingsForTovGroup.DataSource = GetArkonaBonusFractionForTovGr((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);
            }
        }

        private void btnSaveBonusSettingsByTovGr_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex == 0 && cbBrand.SelectedIndex == 0)
                return;

            if (cbBrand.SelectedIndex == 0 || cbBrand.SelectedValue == null)
            {
                MessageBox.Show("Не выбран бренд!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            try
            {
                int idTm = (int)cbBrand.SelectedValue;
                DataRow dr = gridViewBonusSettingsForTovGroup.GetFocusedDataRow();
                if (dr == null) { return; }
                int idTovGr = Convert.ToInt32(dr["tovgr"]);

                var rows = (gridViewBonusSettingsForTovGroup.GridControl.DataSource as DataTable).AsEnumerable().Where(p => p["tovgr"].Equals(idTovGr));

                if (rows.Count() == 0) { return; }

                float minLevelRRP = 0.0f;
                float fractionPercent = 0.0f;
                float BrandMarkup = 0.0f;
                
                foreach (var item in rows)
                {
                    minLevelRRP = Convert.ToSingle(item["minLevelRRP"]);
                    fractionPercent = Convert.ToSingle(item["fractionPercent"]);
                    BrandMarkup = Convert.ToSingle(item["BrandMarkup"]);
                    int id = Convert.ToInt32(item["id"]);

                    string sqlins = $@"update sArkonaBonusFraction 
                    set minLevelRRP = @minLevelRRP / 100.0, 
                    fractionPercent = @fractionPercent / 100.0, 
                    BrandMarkup = @BrandMarkup / 100.0 where idBrand = @idTm and tovgr = @tovgr and id = @id";
                    SqlParameter parminlevel = new SqlParameter("minLevelRRP", minLevelRRP);
                    SqlParameter parfracperc = new SqlParameter("fractionPercent", fractionPercent);
                    SqlParameter parbrandmark = new SqlParameter("BrandMarkup", BrandMarkup);
                    SqlParameter partmid = new SqlParameter("idTm", idTm);
                    SqlParameter partovgr = new SqlParameter("tovgr", idTovGr);
                    SqlParameter parid = new SqlParameter("id", id);
                    var b = DBExecutor.ExecuteQuery(sqlins, parminlevel, parfracperc, parbrandmark, partmid, partovgr, parid);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Произошла ошибка: {ex.Message}");
                return;
            }
            finally
            {
                btnLoadTables_Click(sender, e);
            }
        }

        private void cbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBrand.SelectedIndex != 0 && cbBrand.SelectedValue != null)
            {
                int idTm = (int)cbBrand.SelectedValue;
                LoadTovGroups(idTm);
            }
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex == 0 && cbBrand.SelectedIndex == 0)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                if (cbSupplier.SelectedIndex > 0 && cbBrand.SelectedIndex > 0)
                {
                    //в продуктиве не нужно
                    //if (MessageBox.Show("Применить настройки?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) { return; }

                    btnSaveBonusFractionSettings_Click(sender, e);
                    btnSaveBonusSettingsByTovGr_Click(sender, e);

                    int idTm = (int)cbBrand.SelectedValue;
                    int idKontrTitle = (int)cbSupplier.SelectedValue;

                    string sql = $@"select count(*) cnt from sArkonaBonusFraction (nolock) where idbrand = {idTm}";
                    var row = DBExecutor.SelectRow(sql);
                    var cnt = row["cnt"];
                    if ((int)cnt == 0)
                    {
                        MessageBox.Show("Нельзя рассчитать цены А+, не внесены показатели для расчета!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlParameter parBrand = new SqlParameter("idBrand", idTm);
                    SqlParameter parKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);

                    //не требуется предварительный расчет, поскольку "глазами никто уровень цен проверять не будет"
                    //DBExecutor.ExecuteQuery("exec [dbo].up_CalcPriceAFraction @idkontrTitle, @idTm", ParamidKontrTitle, ParamidTm);
                    DBExecutor.ExecuteQuery("exec [dbo].up_ApplyPriceAFraction @idBrand, @idkontrtitle", parBrand, parKontrTitle);
                    
                    //не нужно сообщение
                    //MessageBox.Show("Расчет цен А+ по выбранному бренду завершен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnDeleteBonusFractionSettings_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridViewBrandSettingsNew.GetFocusedDataRow();
                
                if (row != null)
                {
                    if (MessageBox.Show("Удалить все настройки по бренду?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) { return; }
                    int idtm = Convert.ToInt32(row["tm_id"]);
                    SqlParameter paridtm = new SqlParameter("idtm", idtm);
                    DBExecutor.ExecuteQuery("delete from sArkonaBonusFraction where idbrand = @idtm", paridtm);
                    btnLoadTables_Click(sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка удаления настроек: " + ex.Message);

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridViewBonusSettingsForTovGroup.GetFocusedDataRow();

                if (row != null)
                {
                    if (MessageBox.Show("Удалить настройки для выбранной товарной группы?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) { return; }
                    int idtm = Convert.ToInt32(row["tm_id"]);
                    int tovgr = Convert.ToInt32(row["tovgr"]);
                    SqlParameter paridtm = new SqlParameter("idtm", idtm);
                    SqlParameter partovgr = new SqlParameter("tovgr", tovgr);
                    DBExecutor.ExecuteQuery("delete from sArkonaBonusFraction where idbrand = @idtm and tovgr = @tovgr", paridtm, partovgr);
                    btnLoadTables_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления настроек для товарной группы: " + ex.Message);

            }
        }
    }
}
