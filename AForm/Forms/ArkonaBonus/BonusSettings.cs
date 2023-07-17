using AForm.Base;
using ALogic.DBConnector;
using ALogic.Logic.SPR.Old;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.ComboBox;

namespace AForm.Forms.ArkonaBonus
{
    public partial class BonusSettings : AWindow
    {

        bool flagSave;
        public BonusSettings()
        {
            InitializeComponent();
            this.Tag = "Настройка балльной программы";
            this.Text = "Настройка балльной программы";
        }

        public override void LoadControls()
        {
            //gridMain._Banded = true;

            gridMain.AddColumn("id_tov", "Код", new ColParam() { HorzAlignment = DevExpress.Utils.HorzAlignment.Near });
            gridMain.AddColumn("id_tov_oem", "Артикул");
            gridMain.AddColumn("OrderN", "Сток");
            gridMain.AddColumn("n_tov", "Наименование");
            gridMain.AddColumn("tm_name", "Бренд");

            gridMain.AddColumn("price", "Цена поставщика", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("sebest", "УЦ 41", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("pricetm", "РРЦ", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("PriceARetail", "Цена А+ Розница", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("PriceAOpt", "Цена А+ Опт", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("roz21", "Цена без баллов P1", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("roz22", "Цена без баллов P2", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("roz23", "Цена без баллов P3", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("MinSebestRetail", "МинБренда Розница", new ColParam { AfterPoint = 2 });
            //gridMain.AddColumn("Opt1", "Цена без баллов Опт", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("MinSebestOpt", "МинБренда Опт", new ColParam { AfterPoint = 2 });
            

            gridMain.AddColumn("rozASKU21", "Цена без баллов ASKU Р1", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("rozASKU22", "Цена без баллов ASKU Р2", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("rozASKU23", "Цена без баллов ASKU Р3", new ColParam { AfterPoint = 2 });
            //gridMain.AddColumn("optASKU", "Цена без баллов ASKU Опт", new ColParam { AfterPoint = 2 });

            //коммент до релиза
           
            gridMain.AddColumn("roz0", "Цена без баллов Р0", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("newopt1", "Цена без баллов ОПТ1", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("newopt2", "Цена без баллов ОПТ2", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("rozASKU0", "Цена без баллов ASKU Р0", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("newopt1ASKU", "Цена без баллов ASKU ОПТ1", new ColParam { AfterPoint = 2 });
            gridMain.AddColumn("newopt2ASKU", "Цена без баллов ASKU ОПТ2", new ColParam { AfterPoint = 2 });
           
            gridMain.AddColumn("StError", "Проблема");

            gridMain.GV.ColumnPanelRowHeight = 35;
            SetTableEditAll(gridMain, false);

            gridMain.GV.CustomDrawCell += GV_CustomDrawCell;

            //gridBonus.GV.Columns.Clear();
            gridBonus.GV.ShownEditor += GV_ShownEditor;


            gridBonus.AddColumn("tm_id", "Код", new ColParam() { HorzAlignment = DevExpress.Utils.HorzAlignment.Near });
            gridBonus.AddColumn("tm_name", "Бренд");

            gridBonus.AddColumn("P1", "Балл P1 %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("P2", "Балл P2 %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("P3", "Балл P3 %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("retailMarkup", "Наценка Розница %", new ColParam { AfterPoint = 2 }, "Наценка Розн -  % к Цене Поставщика, если по СКЮ не укаан РРЦ");
            gridBonus.AddColumn("optMarkup", "Наценка Опт %", new ColParam { AfterPoint = 2 }, "Наценка Опт -  % к Цене Поставщика, если по СКЮ не укаан РРЦ");
            //gridBonus.AddColumn("optBonus", "Балл Опт %", new ColParam { AfterPoint = 2 });

            //gridBonus.AddColumn("optASKUBonus", "Балл Опт ASKU %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("retailASKUBonus21", "Балл Р1 ASKU %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("retailASKUBonus22", "Балл Р2 ASKU %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("retailASKUBonus23", "Балл Р3 ASKU %", new ColParam { AfterPoint = 2 });
            //коммент до релиза
            
            gridBonus.AddColumn("P0", "Балл P0 %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("O1", "Балл ОПТ1 %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("O2", "Балл ОПТ2 %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("P0ASKU", "Балл P0 ASKU %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("O1ASKU", "Балл ОПТ1 ASKU %", new ColParam { AfterPoint = 2 });
            gridBonus.AddColumn("O2ASKU", "Балл ОПТ2 ASKU %", new ColParam { AfterPoint = 2 });
            
            gridBonus.GV.Columns["tm_id"].OptionsColumn.AllowEdit = false;
            gridBonus.GV.Columns["tm_name"].OptionsColumn.AllowEdit = false;
            gridBonus.GV.ColumnPanelRowHeight = 35;

            gridMinBrand.AddColumn("tm_name", "Бренд");
            gridMinBrand.AddColumn("MinPercAllRetail", "Мин % Бренда Розница", new ColParam { AfterPoint = 2 });
            gridMinBrand.AddColumn("MinPercASKURetail", "Мин % ASKU Розница", new ColParam { AfterPoint = 2 });
            gridMinBrand.AddColumn("MinPercAllOpt", "Мин % Бренда Опт", new ColParam { AfterPoint = 2 });
            gridMinBrand.AddColumn("MinPercASKUOpt", "Мин % ASKU Опт", new ColParam { AfterPoint = 2 });


            gridMinBrand.GV.Columns["tm_name"].OptionsColumn.AllowEdit = false;
            gridMinBrand.GV.ColumnPanelRowHeight = 35;

            gridAsku.AddColumn("id_tov", "Код", new ColParam() { HorzAlignment = DevExpress.Utils.HorzAlignment.Near });
            gridAsku.AddColumn("id_tov_oem", "Артикул");
            gridAsku.AddColumn("n_tov", "Наименование");
            gridAsku.GV.ColumnPanelRowHeight = 35;

            SetTableEditAll(gridAsku, false);


            gridBonus.EventAdd += GridBonus_EventAdd;
            gridBonus.EventDelete += GridBonus_EventDelete;
            //gridBonus.EventSave += GridBonus_EventSave;

            gridMinBrand.EventAdd += GridMinBrand_EventAdd;
            gridMinBrand.EventDelete += GridMinBrand_EventDelete;
            //gridMinBrand.EventSave += GridMinBrand_EventSave;


            gridAsku.EventAdd += GridAsku_EventAdd;
            gridAsku.EventDelete += GridAsku_EventDelete;
            //gridAsku.EventSave += GridAsku_EventSave; 
            gridMain.GV.OptionsView.ColumnAutoWidth = false;
            gridMain.HorizontalScroll.Enabled = true;
            
        }

        private void GV_ShownEditor(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                gridBonus.GV.ActiveEditor.SelectAll();
            }));
        }

        private void GridAsku_EventDelete(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Вы хотите удалить " + gridAsku.FocusedRow["n_tov"], "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                gridAsku.FocusedRow.Delete();
            }
        }

        private void GridAsku_EventSave()
        {

            foreach (var row in gridAsku.DataSource.AsEnumerable())
            {
                ALogic.Logic.ArkonaBonus.BonusSettingsLogic.SaveAsku(row);
            }

        }

        private void GridAsku_EventAdd(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex > 0)
            {
                var data = ALogic.Logic.ArkonaBonus.BonusSettingsLogic.getNotTovAsku((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);
                WCheck check = new WCheck(data);
                check.WindowState = FormWindowState.Normal;
                check.StartPosition = FormStartPosition.CenterParent;
                if (check.ShowDialog() == DialogResult.OK)
                {
                    foreach (var row in check.TCheck.AsEnumerable().Where(p => (int)p["fCheck"] == 1))
                    {

                        gridAsku.DataSource.Rows.Add(row["Id"], row["id_tov_oem"], row["Name"]);
                    }
                }
            }
        }

        void GV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var row = gridMain.GV.GetDataRow(e.RowHandle);

            if (row == null)
                return;

            if (int.Parse(row["fR"].ToString()) == 1)
                e.Appearance.BackColor = Color.FromArgb(255, 255, 128);

            if (e.Column.Name == "roz21")
                if (e.CellValue == DBNull.Value || row["MinSebestRetail"] == DBNull.Value || decimal.Parse(e.CellValue.ToString()) < decimal.Parse(row["MinSebestRetail"].ToString()))
                    e.Appearance.BackColor = Color.FromArgb(250, 0, 0);

            if (e.Column.Name == "roz22")
                if (e.CellValue == DBNull.Value || row["MinSebestRetail"] == DBNull.Value || decimal.Parse(e.CellValue.ToString()) < decimal.Parse(row["MinSebestRetail"].ToString()))
                    e.Appearance.BackColor = Color.FromArgb(250, 0, 0);

            if (e.Column.Name == "roz23")
                if (e.CellValue == DBNull.Value || row["MinSebestRetail"] == DBNull.Value || decimal.Parse(e.CellValue.ToString()) < decimal.Parse(row["MinSebestRetail"].ToString()))
                    e.Appearance.BackColor = Color.FromArgb(250, 0, 0);

            if (e.Column.Name == "Opt1")
                if (e.CellValue == DBNull.Value || row["MinSebestOpt"] == DBNull.Value || decimal.Parse(e.CellValue.ToString()) < decimal.Parse(row["MinSebestOpt"].ToString()))
                    e.Appearance.BackColor = Color.FromArgb(250, 0, 0);

            //if (e.Column.Name == "optASKU")
            //    if (e.CellValue == DBNull.Value || row["MinSebestOpt"] == DBNull.Value || decimal.Parse(e.CellValue.ToString()) < decimal.Parse(row["MinSebestOpt"].ToString()))
            //        e.Appearance.BackColor = Color.FromArgb(250, 0, 0);

            //if (e.Column.Name == "rozASKU")
            //    if (e.CellValue == DBNull.Value || row["MinSebestRetail"] == DBNull.Value || decimal.Parse(e.CellValue.ToString()) < decimal.Parse(row["MinSebestRetail"].ToString()))
            //        e.Appearance.BackColor = Color.FromArgb(250, 0, 0);


        }


        private void GridBonus_EventAdd(object sender, EventArgs e)
        {
            if (cbBrand.SelectedIndex == 0 && cbSupplier.SelectedIndex == 0)
                return;

            if (gridBonus.DataSource == null)
            {
                MessageBox.Show("Сначала обновить информацию");
                return;
            }

            if (cbBrand.SelectedIndex > 0)
            {
                var gridItem = from DataRow i in gridBonus.DataSource.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted) select new Brand((int)i["tm_id"], i["tm_name"].ToString());

                if (gridItem.Count(p => p.Id == (int)cbBrand.SelectedValue) > 0)
                    return;

                //gridBonus.DataSource.Rows.Add(cbBrand.SelectedValue, cbBrand.SelectedText, 0, 0, 0, 0);
                gridBonus.DataSource.Rows.Add(cbBrand.SelectedValue, cbBrand.Text, 0, 0, 0, 0);
                return;
            }

            if (cbSupplier.SelectedIndex > 0)
            {
                var cbItems = from i in cbBrand.Items.Cast<Brand>() where i.Id != 0 select i;
                var gridItem = from DataRow i in gridBonus.DataSource.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted) select new Brand((int)i["tm_id"], i["tm_name"].ToString());
                var Uni = cbItems.Select(a => a.Id).Except(gridItem.Select(a => a.Id));//cbItems.Except(gridItem, new BrandComparer());
                var res = from i in cbItems where Uni.Contains(i.Id) select i;
                foreach (var item in res)
                {
                    gridBonus.DataSource.Rows.Add(item.Id, item.Name, 0, 0, 0, 0);
                }
            }
        }

        private void GridMinBrand_EventAdd(object sender, EventArgs e)
        {
            var cbItems = from i in cbBrand.Items.Cast<Brand>() where i.Id != 0 select i;
            var gridItem = from DataRow i in gridMinBrand.DataSource.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted) select new Brand((int)i["idBrand"], i["tm_name"].ToString());
            var Uni = cbItems.Select(a => a.Id).Except(gridItem.Select(a => a.Id));//cbItems.Except(gridItem, new BrandComparer());
            var res = from i in cbItems where Uni.Contains(i.Id) select i;
            foreach (var item in res)
            {
                gridMinBrand.DataSource.Rows.Add(0, item.Id, item.Name, 0);
            }
        }

        private void GridMinBrand_EventSave()
        {
            foreach (var row in gridMinBrand.DataSource.AsEnumerable())
            {
                ALogic.Logic.ArkonaBonus.BonusSettingsLogic.SaveMinBrand(row);
            }
        }

        private void GridMinBrand_EventDelete(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Вы хотите удалить бренд " + gridMinBrand.FocusedRow["tm_name"], "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                gridMinBrand.FocusedRow.Delete();
            }
        }



        private void GridBonus_EventSave()
        {

            var rows = gridBonus.DataSource.AsEnumerable();//.Where(r => r.RowState != DataRowState.Deleted);
            
            if (rows.Count() == 0) { return; }

            flagSave = false;

            foreach (var item in rows)
            {
                if (
                    item["tm_id"].ToString() == "" ||
                    item["P1"].ToString() == "" ||
                    item["P2"].ToString() == "" ||
                    item["P3"].ToString() == "" ||
                    item["retailMarkup"].ToString() == "" ||
                    item["optMarkup"].ToString() == "" ||
                    /*item["optBonus"].ToString() == "" ||
                    item["optASKUBonus"].ToString() == "" ||*/
                    item["retailASKUBonus21"].ToString() == "" ||
                    item["retailASKUBonus22"].ToString() == "" ||
                    item["retailASKUBonus23"].ToString() == "" ||
                    item["P0"].ToString() == "" ||
                    item["O1"].ToString() == "" ||
                    item["O2"].ToString() == "" ||
                    item["P0ASKU"].ToString() == "" ||
                    item["O1ASKU"].ToString() == "" ||
                    item["O2ASKU"].ToString() == ""
                    )
                {
                    flagSave = true;
                    MessageBox.Show("Не заполнена одна из ячеек настройки,\nпожалуйста, заполните все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }

            if (flagSave) return;

            foreach (var item in rows)
            {
                ALogic.Logic.ArkonaBonus.BonusSettingsLogic.SaveABonus(item);
            }

            flagSave = false;


        }

        private void GridBonus_EventDelete(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Вы хотите удалить бренд " + gridBonus.FocusedRow["tm_name"], "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                gridBonus.FocusedRow.Delete();
                var rows = gridBonus.DataSource.AsEnumerable();
                foreach(var item in rows)
                {
                    ALogic.Logic.ArkonaBonus.BonusSettingsLogic.SaveABonus(item);
                }
                btnRetrive_Click(sender, e);
            }
        }



        private void BonusSettings_Load(object sender, EventArgs e)
        {
            LoadBrands();
            LoadSuppliers();
        }

        private void LoadBrands(int idSupplier = 0)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            cbSupplier.SelectedIndex = 0;
            cbBrand.DataSource = null;
            gridMain.DataSource = null;
            gridBonus.DataSource = null;
            gridMinBrand.DataSource = null;
            gridAsku.DataSource = null;
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex > 0)
            {
                LoadBrands(int.Parse(cbSupplier.SelectedValue.ToString()));
            }
            else
            {
                //LoadBrands(0);  
                Clear();
            }
        }

        private void btnRetrive_Click(object sender, EventArgs e)
        {
            LoadTables();

            gridBonus.GV.OptionsView.ColumnAutoWidth = false;
            gridBonus.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridBonus.GV.Columns["tm_id"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridBonus.GV.Columns["tm_name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            gridMain.GV.OptionsView.ColumnAutoWidth = false;
            gridMain.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridMain.GV.Columns["tm_name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            
            gridMain.GV.Columns["id_tov"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridMain.GV.Columns["id_tov_oem"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridMain.GV.Columns["OrderN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridMain.GV.Columns["n_tov"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            //для возможности переноса строк при изменении размеров столбца
            gridMain.GC.BeginUpdate();
            RepositoryItemMemoEdit Rme = new RepositoryItemMemoEdit
            {
                WordWrap = true,
                AutoHeight = true
            };

            //gridMain.GV.OptionsView.ColumnAutoWidth = false;
            gridMain.GV.OptionsView.RowAutoHeight = true;
            foreach (GridColumn c in gridMain.GV.Columns) // для каждой колонки грида
            {
                c.ColumnEdit = Rme; // назначаем редактором Rme
                //c.BestFit(); // инициализируем автоподбор ширины колонки
            }
            gridMain.GC.EndUpdate();
            DataChanged = false;
        }

        private void LoadTables()
        {
            if (cbSupplier.SelectedIndex == 0 && cbBrand.SelectedIndex == 0)
                return;

            if (cbSupplier.SelectedIndex > 0)
            {
                if (cbBrand.SelectedIndex > 0)
                {
                    Brand item = (Brand)cbBrand.SelectedItem;
                    SqlParameter idBrand = new SqlParameter("idBrand", item.Id);
//                    ALogic.DBConnector.DBExecutor.ExecuteQuery("exec [dbo].up_RecalcPriceA @idBrand", idBrand);
                    var idkontrtitle = DBExecutor.SelectSchalar("select cast(idKontrTitle as int) from rKontrTitleTm (nolock) where idTm = " + item.Id.ToString());
                    DBExecutor.ExecuteQuery("exec [dbo].up_CalcPriceAFraction @idKontrTitle, @idtm", new SqlParameter("idKontrTitle", (idkontrtitle)), new SqlParameter("idtm", item.Id));
                    
                }
                else
                {
                    foreach (Brand item in cbBrand.Items)
                    {
                        SqlParameter idBrand = new SqlParameter("idBrand", item.Id);
                        //                        ALogic.DBConnector.DBExecutor.ExecuteQuery("exec [dbo].up_RecalcPriceA @idBrand", idBrand);
                        var idkontrtitle = DBExecutor.SelectSchalar("select cast(idKontrTitle as int) from rKontrTitleTm (nolock) where idTm = " + item.Id.ToString());
                        DBExecutor.ExecuteQuery("exec [dbo].up_CalcPriceAFraction @idKontrTitle, @idtm", new SqlParameter("idKontrTitle", (idkontrtitle)), new SqlParameter("idtm", item.Id));

                    }
                }
            }
            else
            {
                Brand item = (Brand)cbBrand.SelectedItem;
                SqlParameter idBrand = new SqlParameter("idBrand", item.Id);
                //                ALogic.DBConnector.DBExecutor.ExecuteQuery("exec [dbo].up_RecalcPriceA @idBrand", idBrand);
                var idkontrtitle = DBExecutor.SelectSchalar("select cast(idKontrTitle as int) from rKontrTitleTm (nolock) where idTm = " + item.Id.ToString());
                DBExecutor.ExecuteQuery("exec [dbo].up_CalcPriceAFraction @idKontrTitle, @idtm", new SqlParameter("idKontrTitle", (idkontrtitle)), new SqlParameter("idtm", item.Id));
            }

            var tableTov = ALogic.Logic.ArkonaBonus.BonusSettingsLogic.GetTovTable((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);
            var tableBonus = ALogic.Logic.ArkonaBonus.BonusSettingsLogic.GetArkonaBonus((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);
            var tableMinBrand = ALogic.Logic.ArkonaBonus.BonusSettingsLogic.GetMinBrand((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);
            var tableExec = ALogic.Logic.ArkonaBonus.BonusSettingsLogic.GetTovAsku((int)cbSupplier.SelectedValue, (int)cbBrand.SelectedValue);

            gridMain.DataSource = tableTov;
            gridBonus.DataSource = tableBonus;
            gridMinBrand.DataSource = tableMinBrand;
            gridAsku.DataSource = tableExec;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex > 0)
            {
                if (cbBrand.SelectedIndex > 0)
                {
                    Brand item = (Brand)cbBrand.SelectedItem;
                    SqlParameter idBrand = new SqlParameter("idBrand", item.Id);
                    //ALogic.DBConnector.DBExecutor.ExecuteQuery("exec [dbo].up_ApplyPriceA @idBrand", idBrand);
                    var idkontrtitle = DBExecutor.SelectSchalar("select cast(idKontrTitle as int) from rKontrTitleTm (nolock) where idTm = " + item.Id.ToString());
                    DBExecutor.ExecuteQuery("exec [dbo].up_ApplyPriceAFraction @idBrand, @idKontrTitle", new SqlParameter("idBrand", item.Id), new SqlParameter("idKontrTitle", (int)idkontrtitle));
                }
                else
                {
                    foreach (Brand item in cbBrand.Items)
                    {
                        if (item.Id > 0)
                        {
                            SqlParameter idBrand = new SqlParameter("idBrand", item.Id);
                            //        ALogic.DBConnector.DBExecutor.ExecuteQuery("exec [dbo].up_ApplyPriceA @idBrand", idBrand);
                            var idkontrtitle = DBExecutor.SelectSchalar("select cast(idKontrTitle as int) from rKontrTitleTm (nolock) where idTm = " + item.Id.ToString());
                            DBExecutor.ExecuteQuery("exec [dbo].up_ApplyPriceAFraction @idBrand, @idKontrTitle", new SqlParameter("idBrand", item.Id), new SqlParameter("idKontrTitle", (int)idkontrtitle));
                        }
                    }
                }

                MessageBox.Show("Перерасчет цен завершен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex > 0)
            {
                AcceptAllTable(this);
                GridBonus_EventSave();
                //GridMinBrand_EventSave();
                GridAsku_EventSave();
                DataChanged = false; //ins Smolyanina 23.11.2022 
                if(!flagSave) MessageBox.Show("Ваши данные сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
