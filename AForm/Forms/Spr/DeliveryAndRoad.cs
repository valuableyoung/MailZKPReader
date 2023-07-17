using AForm.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AForm.Forms.Spr
{
    public partial class DeliveryAndRoad :AWindow
    {
        public DeliveryAndRoad()
        {
            InitializeComponent();
            this.Tag = "Направления и районы";
        }

        DataTable _dtNewData;

        public override void LoadControls()
        {
            tRoad.AddColumn("nRoute", "Направление доставки");
            tRoad.AddColumn("idRoad", "Код доставки");

            var re = new RepositoryItemCheckEdit();
            re.ValueChecked = 1;
            re.ValueUnchecked = 0;

            re.CheckedChanged += Re_CheckedChanged;

            tPosition.AddColumn("fCheck", " ", new ColParam() { Repository = re });
            tPosition.AddColumn("nGroup", "Регион");
            tPosition.AddColumn("nPosition", "Район");
            tPosition.AddColumn("idPosition", "Код доставки");

            _dtNewData = ALogic.Logic.SPR.DeliveryAndRoad.GetCheck();         

            SetTableEditAll(tRoad, false);
            SetTableEditAll(tPosition, false);

            tPosition.GV.Columns["fCheck"].OptionsColumn.AllowEdit = true;

            tRoad.EventAdd += TRoad_EventAdd;
            tRoad.GV.FocusedRowChanged += GV_FocusedRowChanged;
            tRoad.GV.CustomDrawCell += GV_CustomDrawCell;
            tRoad.EventSave += TRoad_EventSave;
            tRoad.EventDelete += TRoad_EventDelete;

            tPosition.GV.CustomDrawCell += GV_CustomDrawCell1;
        }

        private void TRoad_EventDelete(object sender, EventArgs e)
        {
            var row = tRoad.FocusedRow;
            var idRoute = int.Parse(row["idRoute"].ToString());

            foreach (var r in _dtNewData.AsEnumerable().Where(p => int.Parse(p["idRoute"].ToString()) == idRoute))
                r["fCheck"] = 0;

            row.Delete();
        }

        private void TRoad_EventSave(object sender, EventArgs e)
        {
            if (!ALogic.Logic.SPR.DeliveryAndRoad.Save(_dtNewData))
            {
                MessageBox.Show("Нельзя назначать одному району 2 разных направления! Проверьте красные строки и внесите данные правильно!");          
            }
        }

        private void GV_CustomDrawCell1(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var row = tPosition.GV.GetDataRow(e.RowHandle);

            if (row == null)
                return;

            var idPosition = int.Parse(row["idPosition"].ToString());
            bool err = false;
            foreach (var pos in _dtNewData.AsEnumerable().Where(p => int.Parse(p["idPosition"].ToString()) == idPosition && int.Parse(p["fCheck"].ToString()) == 1).ToList())
            {
                if (_dtNewData.AsEnumerable().Where(p => int.Parse(p["fCheck"].ToString()) == 1 && int.Parse(p["idPosition"].ToString()) == int.Parse(pos["idPosition"].ToString())).Count() > 1)
                    err = true;
            }

            if (err)
                e.Cache.FillRectangle(Color.FromArgb(255, 0, 0), e.Bounds);
        }

        private void GV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var row = tRoad.GV.GetDataRow(e.RowHandle);

            if (row == null)
                return;

            var idRoute = int.Parse(row["idRoute"].ToString());
            bool err = false;

            var lisrPosition = _dtNewData.AsEnumerable().Where(p => int.Parse(p["idRoute"].ToString()) == idRoute && int.Parse(p["fCheck"].ToString()) == 1).ToList();
            foreach (var pos in lisrPosition)
            {
                if (_dtNewData.AsEnumerable().Where(p =>  int.Parse(p["fCheck"].ToString()) == 1 && int.Parse(p["idPosition"].ToString()) == int.Parse(pos["idPosition"].ToString())).Count() > 1)
                    err = true;
            }

            if (err)
                e.Cache.FillRectangle(Color.FromArgb(255, 0, 0), e.Bounds);    
            

            if (lisrPosition.Count == 0)
                e.Cache.FillRectangle(Color.FromArgb(210, 210, 210), e.Bounds);

        }

        private void Re_CheckedChanged(object sender, EventArgs e)
        {
            var idRoute = int.Parse(tRoad.FocusedRow["idRoute"].ToString());
            var idPosition = int.Parse(tPosition.FocusedRow["idPosition"].ToString());
            var check = (sender as CheckEdit).Checked ? 1 : 0; 

            var oldRow = _dtNewData.AsEnumerable().Where(p => int.Parse(p["idRoute"].ToString()) == idRoute && int.Parse(p["idPosition"].ToString()) == idPosition).FirstOrDefault();
            if (oldRow != null)
                oldRow["fCheck"] = check;
            else
                _dtNewData.Rows.Add(idRoute, idPosition, check);


            tRoad.Refresh();

        }

        private void GV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadPosition();
        }

        private void TRoad_EventAdd(object sender, EventArgs e)
        {
            DataTable dtAll = ALogic.Logic.SPR.DeliveryAndRoad.GetRoadAll();

            foreach (var row in dtAll.AsEnumerable())
                if (tRoad.DataSource.AsEnumerable().Where(p => int.Parse(p["idRoute"].ToString())== int.Parse(row["Id"].ToString())).FirstOrDefault() != null)
                    row["fCheck"] = 1;

            var wind = new WCheck(dtAll);
            wind.WindowState = FormWindowState.Normal; 
            if (wind.ShowDialog() == DialogResult.OK)
            {
                foreach(var row in wind.TCheck.AsEnumerable().Where(p=>(int)p["fCheck"] == 1))
                {
                    if (tRoad.DataSource.AsEnumerable().Where(p => int.Parse(p["idRoute"].ToString()) == int.Parse(row["Id"].ToString())).FirstOrDefault() == null)
                    {
                        tRoad.DataSource.Rows.Add(row["Id"], row["Name"], row["idRoad"]);
                    }
                }
            }
        }

        public override void LoadData(object sender, EventArgs e)
        {
            tRoad.DataSource = ALogic.Logic.SPR.DeliveryAndRoad.GetRoad();

            LoadPosition();
        }

        private void LoadPosition()
        {
            var row = tRoad.FocusedRow;
            if (row == null)
                return;

            int idRoute = int.Parse(row["idRoute"].ToString());

            tPosition.DataSource = ALogic.Logic.SPR.DeliveryAndRoad.GetPosition(idRoute, _dtNewData);
            tPosition.GV.ExpandAllGroups();
        }
    }
}
