using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForm.Base;
using ALogic.Logic.Old.DebKredZad;
using ALogic.Logic.SPR;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace AForm.Forms.Old.Other
{
    public partial class WFPerKredZad : AWindow
    {
        public WFPerKredZad()
        {
            InitializeComponent();
            this.Tag = "МонКредЗад"; 
        }

        public override void LoadControls()
        {
            tblMain.AddColumn("idKontr", "Код");
            tblMain.AddColumn("nKontr", "Наименование");
            tblMain.AddColumn("nAgent", "РТК");
            tblMain.AddColumn("Abbr", "Преф.");
            tblMain.AddColumn("nomContract", "Договор.");
            tblMain.AddColumn("SumCred", "Кредит-лимит", new ColParam() { AfterPoint = 0 });
            tblMain.AddColumn("KolDay", "Отсрочка платежа");

            var repVal = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            repVal.DataSource = DBSprCur.GetAllSprCur();
            repVal.DisplayMember = "n_curr_short";
            repVal.ValueMember = "id_cur";

            tblMain.AddColumn("idCur", "Валюта", new ColParam() { Repository = repVal });
            tblMain.AddColumn("CurRec", "Курс", new ColParam() { AfterPoint = 4 });
            tblMain.AddColumn("DZ", "Текущая ДЗ", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("KZ", "Текущая КЗ", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("oborot62", "Оборот на 62", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("PKZp", "Доля ПКЗ", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 2, SummaryItemType = SummaryItemType.Custom });
            tblMain.AddColumn("PKZ", "ПКЗ", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("k07", "0-7", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("k815", "8-15", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("k1621", "16-21", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("k2230", "22-30", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("k31", "свыше 31", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tblMain.AddColumn("dateLast", "Последняя поставка");

            int rkz = 0;
            int rkzp = 0;
            tblMain.GV.CustomSummaryCalculate += (sender, e) =>
            {
                if (e.IsTotalSummary || e.IsGroupSummary)
                {
                    GridView view = sender as GridView;
                    switch (e.SummaryProcess)
                    {
                        //calculation entry point 
                        case CustomSummaryProcess.Start:
                            rkz = 0;
                            rkzp = 0;
                            break;
                        //consequent calculations 
                        case CustomSummaryProcess.Calculate:
                            rkz += int.Parse(view.GetDataRow(e.RowHandle)["KZ"].ToString());
                            rkzp += int.Parse(view.GetDataRow(e.RowHandle)["PKZ"].ToString());
                            break;
                        //final summary value 
                        case CustomSummaryProcess.Finalize:
                            e.TotalValue = (decimal)rkzp / Math.Max(rkz, 1) * 100;
                            break;
                    }
                }
            };

            tblMain.EventLoad += new EventHandler(tblMain_EventLoad);

            foreach (var col in tblMain.GV.Columns.AsEnumerable())
                col.OptionsColumn.AllowEdit = false;

            tblMain.GV.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GV_CustomDrawCell);
         
            tblMain.GV.OptionsView.ShowFooter = true;
            tblMain.GV.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;

            FormDesigner.DesignGrid(tblMain.GV);
        }

           

        void tblMain_EventLoad(object sender, EventArgs e)
        {
            tblMain.DataSource =  RepKredZadLogic.GetReport();

            foreach (var col in tblMain.GV.Columns.AsEnumerable())
            {
                col.AppearanceCell.Font = new Font("Arial", 12);
                col.AppearanceHeader.Font = new Font("Arial", 12); 
            }

            tblMain.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            tblMain.GV.OptionsView.RowAutoHeight = true;               
        }


        void GV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var row = tblMain.GV.GetDataRow(e.RowHandle);

            if (row == null)
                return;

            //if (decimal.Parse(row["SumCred"].ToString()) < decimal.Parse(row["KZ"].ToString()))
            //    e.Cache.FillRectangle(Color.FromArgb(128,254, 160, 10), e.Bounds);

            if (row["k1621"].ToString() != "0" || row["k2230"].ToString() != "0" || row["k31"].ToString() != "0")
                e.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
        }      
    }
}
