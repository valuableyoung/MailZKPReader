using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ALogic;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using AForm.Base;
using ALogic.Logic.Old.BuySalePlan;
using ALogic.Logic.SPR.Old;
using ALogic.DBConnector;
using ALogic.Logic.Base;
using ALogic.Logic.SPR;

namespace AForm.Forms.Old.BuySalePlan
{
    public partial class SalePlan : Form, ITableForm
    {
        SalePlanEntity _entity;      

        public SalePlan()
        {
            InitializeComponent();
            this.Tag = "План продаж введение данных";
            InitControls();
        }

        private void SalePlan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void bLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if(!_entity.TableChanged() || MessageBox.Show("Обнаружены несохраненные данные, продолжить?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                LoadData();
        }

        private void bAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddRow();
        }

        private void bSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(_entity.TableChanged() && MessageBox.Show("Сохранить внесенные изменения?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SaveData();
        }

        private void bSaveDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveTableXml();
        }

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

        private void beYear_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();

        }
        private void gvMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue != null && e.CellValue.ToString() == "0")
                e.DisplayText = "";
        }

        /*-------------------------------------ОБработчики-------------------------------------------------*/

        public void InitControls()
        {
            gvMain.OptionsMenu.EnableFooterMenu = true;
            for (int i = 1; i < 13; i++)
            {
                var column = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                column.FieldName = "k" + i.ToString();
                column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal; column.Name = "k" + i.ToString();
                column.Caption = i.ToString();
                column.Visible = true;
                column.Width = 50;
                column.OptionsColumn.FixedWidth = true;


                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;


                gbK.Columns.Add(column);
                gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, "k" + i.ToString(), column);
                //gvMain.OptionsView.ShowFooter = true;

            }


            for (int i = 0; i < gbK.Columns.Count; i++)
                gbK.Columns[i].OptionsColumn.FixedWidth = false;

            string kalk = "";
            for (int i = 1; i < 13; i++)
                kalk += "[k" + i.ToString() + "]+";
            kalk = kalk.Remove(kalk.Length - 1);

            vSale.UnboundExpression = kalk;
            gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "vProd", vSale);
            gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "vProfit", vProfit);


            reBrand.DataSource = DBSprBrend.GetBrendsForPlan();
            reBrand.DisplayMember = "tm_name";
            reBrand.ValueMember = "tm_id";

            reSegment.DataSource = DBSprSegment.GetSegmentForPlan();
            reSegment.DisplayMember = "nGroup";
            reSegment.ValueMember = "idGroup";

            beYearSale.DataSource = DBTime.GetYears(-5, +5);
            beYearSale.DisplayMember = "year";
            beYearSale.ValueMember = "year";

            reRegion.DataSource = DBRegion.GetRegionsForPlan();
            reRegion.DisplayMember = "nRegion";
            reRegion.ValueMember = "idRegion";


            gvMain.CustomSummaryCalculate += GvMain_CustomSummaryCalculate1;


            FormDesigner.DesignGrid(gvMain);
            _entity = new SalePlanEntity();          

            LoadTableXml();
        }

        double sum;
        //double totlal;

        private void GvMain_CustomSummaryCalculate1(object sender, CustomSummaryEventArgs e)
        {

            GridView view = sender as GridView;

            GridSummaryItem item = e.Item as GridSummaryItem;
            int summaryID = Convert.ToInt32(item.Tag);
            switch (e.SummaryProcess)
            {

                case CustomSummaryProcess.Start:
                    sum = 0;
                    break;
                case CustomSummaryProcess.Calculate:
                    string shouldSum = (string)view.GetRowCellValue(e.RowHandle, "type");
                    if (shouldSum == "Объем продаж")
                    {
                        sum += double.Parse(e.FieldValue.ToString());
                    }
                    break;
                case CustomSummaryProcess.Finalize:
                    e.TotalValue = sum;
                    break;
            }

            item.DisplayFormat = "{0:#.00}";

        }

        private void GvMain_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Logger_changeProgress(object sender, EventArgs e)
        {
            ppLoad.Caption = sender.ToString();
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

            if (beYearSale.SelectedIndex > 0)
            {
                var saveHandle = gvMain.FocusedRowHandle;
                gcMain.DataSource = _entity.LoadData(beYearSale.Text, checkBox1.Checked);
                btnLoadExcelFile.Enabled = true;
                bSave.Enabled = true;
                gvMain.FocusedRowHandle = saveHandle;
            }
            else
            {
                bSave.Enabled = false;
                btnLoadExcelFile.Enabled = false;
                gcMain.DataSource = null;
            }

            this.Cursor = Cursors.Default;

        }

        public void SaveData()
        {
            this.Cursor = Cursors.WaitCursor;

            if (!backgroundWorker1.IsBusy)
            {
                bSave.Enabled = false;
                ppLoad.Visible = true;
                AcceptEditor();
                backgroundWorker1.RunWorkerAsync();
            }
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
            //gvMain.SaveLayoutToXml(ProjectProperty.FolderTableDesign + "\\SalePlan_gvMain.xml");
            MemoryStream stream = new MemoryStream();
            gvMain.SaveLayoutToStream(stream);
            DBTableDesign.saveProperties(this.Name + "_" + gcMain.Name, Encoding.UTF8.GetString(stream.ToArray()), User.Current.IdUser);
        }

        public void LoadTableXml()
        {
            MemoryStream stream = TableDesignLogic.GetDesign(this.Name + "_" + gcMain.Name);
            if (stream.Capacity > 0)
                gvMain.RestoreLayoutFromStream(stream);
        }

        public void LoadTableXmlDefault()
        {
            MemoryStream stream = TableDesignLogic.GetDesignDefault(this.Name + "_" + gcMain.Name);
            if (stream.Capacity > 0)
                gvMain.RestoreLayoutFromStream(stream);
        }

        private void btnLoadExcelFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (DialogResult.OK == dialog.ShowDialog())
            {
                gcMain.DataSource = _entity.LoadDataFromFile(dialog.FileName);
            }
        }

        private void beYear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void beYearSale_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _entity.SaveData();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ppLoad.Visible = false;
            bSave.Enabled = true;
            _entity.AcceptChanges();
        }

        private void SalePlan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_entity.TableChanged() && MessageBox.Show("Сохранить внесенные изменения?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SaveData();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ppLoad.Caption = "Пожалуйста подождите: " + e.ProgressPercentage.ToString() + "%";
        }

        private void btnSaveDefault_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            gvMain.SaveLayoutToStream(stream);
            DBTableDesign.saveDefaultProperties(this.Name + "_" + gcMain.Name, Encoding.UTF8.GetString(stream.ToArray()));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            checkBox1.Enabled = false;
            if (beYearSale.SelectedIndex > 0)
            {
                if (checkBox1.Checked)
                {
                    gcMain.DataSource = _entity.LoadData(beYearSale.Text, true);

                }
                else
                    gcMain.DataSource = _entity.LoadData(beYearSale.Text);
            }
            checkBox1.Enabled = true;
            this.Cursor = Cursors.Default;

        }

        private void gvMain_ShownEditor(object sender, EventArgs e)
        {
            // gvMain.ActiveEditor.SelectAll();
            BeginInvoke(new Action(() =>
            {
                gvMain.ActiveEditor.SelectAll();
            }));
        }

        private void gvMain_Click(object sender, EventArgs e)
        {
            //gvMain.ActiveEditor.SelectAll();
        }
    }
}
