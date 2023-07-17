using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using AForm.Base;
using ALogic.Logic.Old.BuySalePlan;
using ALogic.Logic.SPR.Old;
using ALogic.Logic.Base;
using ALogic.DBConnector;

namespace AForm.Forms.Old.BuySalePlan
{
    public partial class BuyPlan : Form, ITableForm
    {
        BuyPlanEntity _entity;
        //int idtmFornewRow;
        //private bool isbonusSourceDB = false;
        //DataTable dtbonus;

        public BuyPlan()
        {
            InitializeComponent();
            this.Tag = "План закупок";
            InitControls();
            _entity = new BuyPlanEntity();
        }

        private void BuyPlan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void bLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void bAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddRow();
        }

        private void bSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            SaveData();
        }

        private void bSaveDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveTableXml();
        }

        //TODO: Думаю что данный функционал уже не нужен
        private void bLoadDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadTableXmlDefault();
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                AcceptEditor();
            }
        }

        private void bDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DelData();
        }

        private void gvMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnKeyEnter();
            }
        }

        private void gvMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue != null && e.CellValue.ToString() == "0")
                e.DisplayText = "";
        }

        private void beYear_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        /*-------------------------------Обработчики событий------------------------------------------*/
        public void InitControls()
        {
            gvMain.OptionsMenu.EnableFooterMenu = true;
            for (int i = 1; i < 13; i++)
            {
                var column = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                column.FieldName = "k" + i.ToString();
                column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                column.Name = "k" + i.ToString();
                column.Caption = i.ToString();
                column.Visible = true;
                column.Width = 50;
                column.OptionsColumn.FixedWidth = true;
                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

                gbK.Columns.Add(column);
                gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "k" + i.ToString(), column);
            }

            for (int i = 0; i < gbK.Columns.Count; i++)
                gbK.Columns[i].OptionsColumn.FixedWidth = false;

            string kalk = "";
            for (int i = 1; i < 13; i++)
                kalk += "[k" + i.ToString() + "]+";
            kalk = kalk.Remove(kalk.Length - 1);

            string pSumFurmula = "([k1]+[k2]+[k3])*[p1]/100+([k4]+[k5]+[k6])*[p2]/100+([k7]+[k8]+[k9])*[p3]/100+([k10]+[k11]+[k12])*[p4]/100+[vBuy]*[pf]/100";

            vBuy.UnboundExpression = kalk;
            pSum.UnboundExpression = pSumFurmula;

            gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "vBuy", vBuy);
            //gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "vSalerPlan", vSalerPlan);
            gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "pSum", pSum);
            gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "tovOstS", tovOstS);
            gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "tovOstE", tovOstE);

            reBrand.DataSource = DBSprBrend.GetBrendsForPlan();
            reBrand.DisplayMember = "tm_name";
            reBrand.ValueMember = "tm_id";

            replBrand.DataSource = DBSprBrend.GetBrendsForPlan();
            replBrand.DisplayMember = "tm_name";
            replBrand.ValueMember = "tm_id";

            reSegment.DataSource = DBSprSegment.GetSegmentForPlan();
            reSegment.DisplayMember = "nGroup";
            reSegment.ValueMember = "idGroup";

            //reYear.DataSource = DBConnector.Spr.DBTime.GetYears(-5, +5);
            //reYear.DisplayMember = "year";
            //reYear.ValueMember = "year";

            beYear.DataSource = DBTime.GetYears(-5, +5);
            beYear.DisplayMember = "year";
            beYear.ValueMember = "year";


            replKvartal.DataSource = DBTime.GetPeriod();
            replKvartal.DisplayMember = "name";
            replKvartal.ValueMember = "nomKv";

            //idTm.DataSource = DBConnector.Spr.DBSprBrend.GetBrends();
            //idTm.DisplayMember = "tm_name";
            //idTm.ValueMember = "tm_id";


            FormDesigner.DesignGrid(gvMain);

            LoadTableXml();
        }

        public void AcceptEditor()
        {
            if (gvMain.ActiveEditor != null)
            {
                var row = gvMain.GetFocusedDataRow();
                if (row.Table.Columns.Contains(gvMain.FocusedColumn.FieldName))
                {
                    try
                    {
                        row[gvMain.FocusedColumn.FieldName] = gvMain.ActiveEditor.EditValue;
                                               
                        gvMain.UpdateCurrentRow();
                        var idbrend = int.Parse(row["idbrend"].ToString());
                        int pKv = -1;

                        switch (gvMain.FocusedColumn.FieldName)
                        {


                            case "p1":
                                pKv = 1;
                                break;

                            case "p2":
                                pKv = 2;
                                break;

                            case "p3":
                                pKv = 3;
                                break;

                            case "p4":
                                pKv = 4;
                                break;

                            case "pf":
                                pKv = 0;
                                break;

                            default:
                                break;
                        }

                        if(pKv != -1) _entity.insertNewCustomBonus(idbrend, pKv, decimal.Parse(gvMain.ActiveEditor.EditValue.ToString()));

  



                        _entity.FillSalerPlanBrend(idbrend);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            }


            if (gvPlan.ActiveEditor != null)
            {
                var row = gvPlan.GetFocusedDataRow();
                if (row.Table.Columns.Contains(gvPlan.FocusedColumn.FieldName))
                {
                    try
                    {
                        row[gvPlan.FocusedColumn.FieldName] = gvPlan.ActiveEditor.EditValue;
                        gvPlan.UpdateCurrentRow();
                        var idbrend = int.Parse(row["idtm"].ToString());
                        _entity.FillSalerPlanBrend(idbrend);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            if (beYear.SelectedIndex > 0)
            {
                var saveHandle = gvMain.FocusedRowHandle;
                gcMain.DataSource = _entity.LoadData(beYear.Text);
                gvMain.FocusedRowHandle = saveHandle;

                gcPlan.DataSource = _entity.GetPlan();

                LoadPlanData();
            }
            else
            {
                gcMain.DataSource = null;
                gcPlan.DataSource = null;
            }

            this.Cursor = Cursors.Default;

        }

        private void LoadPlanData()
        {


            this.Cursor = Cursors.WaitCursor;

            var focusedRow = gvMain.GetFocusedDataRow();

            if (focusedRow == null)
                return;

            if (idBrend != null)
            {
                var idBrendp = int.Parse(focusedRow["idBrend"].ToString());
                //Console.WriteLine(idBrend);
                _entity.CheckForPlan(idBrendp);
                gvPlan.SetAutoFilterValue(plidTm, idBrendp, DevExpress.XtraGrid.Columns.AutoFilterCondition.Equals);
            }

            this.Cursor = Cursors.Default;

        }

        private void FilltemlateBonus(int idbrand)
        {
            

        }

        public void SaveData()
        {
            this.Cursor = Cursors.WaitCursor;
            AcceptEditor();
            _entity.SaveData();
            this.Cursor = Cursors.Default;

        }

        public void DelData()
        {
            if (gvMain.GetFocusedDataRow().RowState != DataRowState.Added)
                _entity.DelData(gvMain.GetFocusedDataRow());
            gvMain.DeleteRow(gvMain.FocusedRowHandle);
        }

        public void AddRow()
        {
            gvMain.AddNewRow();
            _entity.FillNewRow(gvMain.GetFocusedDataRow());
        }

        public void OnKeyEnter()
        {
            if (gvMain.FocusedColumn.FieldName == "Marga")
            {
                gvMain.FocusedColumn = gvMain.Columns.First(p => p.FieldName == "k1");
                return;
            }
            if (gvMain.FocusedColumn.FieldName == "k12")
            {
                gvMain.FocusedColumn = gvMain.Columns.First(p => p.FieldName == "Marga");
                gvMain.FocusedRowHandle++;
                return;
            }
            if (gvMain.FocusedColumn.FieldName[0] == 'k')
            {
                int p = int.Parse(gvMain.FocusedColumn.FieldName.Replace("k", "")) + 1;
                gvMain.FocusedColumn = gvMain.Columns.First(q => q.FieldName == (gvMain.FocusedColumn.FieldName[0] + p.ToString()));
            }



        }

        public void SaveTableXml()
        {
            this.Cursor = Cursors.WaitCursor;
            MemoryStream stream = new MemoryStream();
            gvMain.SaveLayoutToStream(stream);
            TableDesignLogic.SaveDesign(this.Name + "_" + gcMain.Name, stream);
            this.Cursor = Cursors.Default;

        }

        public void LoadTableXml()
        {
            MemoryStream stream = TableDesignLogic.GetDesign(this.Name + "_" + gcMain.Name);
            if (stream.Capacity > 0 )
                gvMain.RestoreLayoutFromStream(stream);
            else LoadTableXmlDefault();


        }

        public void LoadTableXmlDefault()
        {
            MemoryStream stream = TableDesignLogic.GetDesignDefault(this.Name + "_" + gcMain.Name);
            if (stream.Capacity > 0 )
                gvMain.RestoreLayoutFromStream(stream);

        }

        private void gcMain_Click(object sender, EventArgs e)
        {

        }

        private void bAddPlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvPlan.AddNewRow();
            var idBrend = gvPlan.GetAutoFilterValue(plidTm);
            gvPlan.GetFocusedDataRow()["idTm"] = idBrend;
            gvPlan.GetFocusedDataRow()["nomYear"] = _entity.Year;
            gvMain.RefreshData();



            //if (idtmFornewRow > 0)
            //{
            //    if (!isbonusSourceDB)
            //    {
            //        int index = dataGridView1.Rows.Add();
            //        dataGridView1.Rows[index].Cells[0].Value = idtmFornewRow;
            //    }

            //    else
            //    {
            //        DataRow row = dtbonus.NewRow();
            //        row[0] = idtmFornewRow;
            //        dtbonus.Rows.Add(row);
            //    }
            //}

        }

        private void bDelPlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvPlan.GetFocusedDataRow().RowState != DataRowState.Added)
                _entity.DelDataPlan(gvPlan.GetFocusedDataRow());
            gvPlan.DeleteRow(gvMain.FocusedRowHandle);
        }

        private void gvMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            LoadPlanData();
        }
        //TODO: Реааизована кнопка ресета, возврат к первоначальным настройкам
        private void bResetDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // gvMain.RestoreLayoutFromStream(DBTableDesign.getResetProperties(this.Name + "_" + gcMain.Name));

            MemoryStream stream = new MemoryStream();
            gvMain.SaveLayoutToStream(stream);
            DBTableDesign.saveDefaultProperties(this.Name + "_" + gcMain.Name, Encoding.UTF8.GetString(stream.ToArray()));

        }

        private void barListItem2_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e)
        {
            barListItem2.Caption = (e.Item as BarListItem).Strings[e.Index];

        }

        private void barListItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void barListItem2_ItemPress(object sender, ItemClickEventArgs e)
        {
        }

        private void beYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void gvMain_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            
        }

        private void gvMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
            //MessageBox.Show(e.Column + " "+ e.Value);
            //if (e.Column.Name == "p1")
            //{
            //    DataRow row = gvMain.GetFocusedDataRow();
            //    _entity.insertNewCustomBonus(gvMain.GetFocusedDataRow()["idTm"].ToString(), "1", e.Value.ToString());
            //}
        }

        private void btnReload_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            if (gvPlan.GetFocusedDataRow() != null)
            {
                _entity.ClearCustomBonus((int)gvPlan.GetFocusedDataRow()["idTm"]);
                AcceptEditor();
            }
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gvMain_ShownEditor(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                gvMain.ActiveEditor.SelectAll();
            }));
        }
    }
}
