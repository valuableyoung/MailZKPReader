using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using AForm.Base;
using ALogic.Logic.Old.DebKredZad;

namespace AForm.Forms.Old.Other
{
    public partial class WFPerDebZad : AWindow
    {
        public WFPerDebZad()
        {
            InitializeComponent();
            this.Tag = "Мониторинг бкдиторской задолженности";          
        }

        public override void LoadControls()
        {
            deMain.EditValue = DateTime.Now.Date;

            tMain.AddColumn("kodKa", "Код КА");
            tMain.AddColumn("naimenovanieKA", "Наименование КА");
            tMain.AddColumn("kanalSbyta", "КС");
            tMain.AddColumn("Agent", "Агент");
            tMain.AddColumn("PreficsFirmy", "Префикс фирмы");
            tMain.AddColumn("NomerDogovora", "Договор");
            tMain.AddColumn("SummKreditLemitDogovora", "Кредит-лимит", new ColParam() { AfterPoint = 0 });        
            tMain.AddColumn("OtsrochDogovora", "Отсрочка платежа");
            tMain.AddColumn("TypeOtsroch", "Тип дней отсрочки");
            tMain.AddColumn("CurrentDZ", "Текущая ДЗ", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tMain.AddColumn("PersentPDZinCurrentDZ", "Доля ПДЗ в ДЗ", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 2, SummaryItemType = SummaryItemType.Custom });
            tMain.AddColumn("SymmProsrochDZ", "Текущая ПДЗ", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tMain.AddColumn("id07", "Период 0-7", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tMain.AddColumn("id815", "Период 8-15", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tMain.AddColumn("id1621", "Период 16-21", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tMain.AddColumn("id2230", "Период 22-30", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tMain.AddColumn("id31", "Период свыше 31", new ColParam() { fGroupSummary = true, fSummary = true, AfterPoint = 0 });
            tMain.AddColumn("lastUpload", "Последняя отгрузка");

            decimal rkz = 0;
            decimal rkzp = 0;
            tMain.GV.CustomSummaryCalculate += (sender, e) =>
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
                            rkz += decimal.Parse(view.GetDataRow(e.RowHandle)["CurrentDZ"].ToString());
                            rkzp += decimal.Parse(view.GetDataRow(e.RowHandle)["SymmProsrochDZ"].ToString());
                            break;
                        //final summary value 
                        case CustomSummaryProcess.Finalize:
                            e.TotalValue = (decimal)rkzp / Math.Max(rkz, 1) * 100;
                            break;
                    }
                }
            };

            tMain.EventLoad += new EventHandler(tMain_EventLoad);

            foreach (var col in tMain.GV.Columns.AsEnumerable())
                col.OptionsColumn.AllowEdit = false;

            tMain.GV.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GV_CustomDrawCell);

            tMain.GV.OptionsView.ShowFooter = true;
            tMain.GV.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;

            FormDesigner.DesignGrid(tMain.GV);               
        }

        void GV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {         
            var row = tMain.GV.GetDataRow(e.RowHandle);

            if (row == null)
                return;

            if (decimal.Parse(row["SummKreditLemitDogovora"].ToString()) < decimal.Parse(row["CurrentDZ"].ToString()))
                e.Cache.FillRectangle(Color.FromArgb(128,254, 160, 10), e.Bounds); 
          
              if (decimal.Parse(row["id1621"].ToString()) > 0 ||decimal.Parse(row["id2230"].ToString()) > 0||decimal.Parse(row["id31"].ToString()) > 0)
                  e.Appearance.ForeColor = Color.FromArgb(255, 0, 0);

           
        }

        void tMain_EventLoad(object sender, EventArgs e)
        {
            tMain.DataSource = RepKredZadLogic.GetReportDZ(deMain.EditValue);

            foreach (var col in tMain.GV.Columns.AsEnumerable())
            {
                col.AppearanceCell.Font = new Font("Arial", 12);
                col.AppearanceHeader.Font = new Font("Arial", 12);
            }

            tMain.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            tMain.GV.OptionsView.RowAutoHeight = true;     
        }        
    }
}
