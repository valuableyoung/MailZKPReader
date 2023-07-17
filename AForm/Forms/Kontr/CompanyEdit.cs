using AForm.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ALogic.Logic.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;

namespace AForm.Forms.Kontr
{
    public partial class CompanyEdit : AWindow
    {
        //CompanyEntity _entity;
        public CompanyEdit()
        {
            InitializeComponent();
            this.Tag = this.Text = "Редактирование клиента";
        }

        public override void LoadControls()
        {
            //tElems.AddColumn("idKontrEntity", "Код");
            tElems.AddColumn("nKontrEntity", "Наименование");

            RepositoryItemCheckEdit reUr = new RepositoryItemCheckEdit();
            reUr.CheckedChanged += ReUr_CheckedChanged;

            RepositoryItemCheckEdit reDept = new RepositoryItemCheckEdit();
            reDept.CheckedChanged += ReUr_CheckedChanged1;

            tElems.AddColumn("fLegal", "Юр лицо", new ColParam() {  Repository = reUr });
            tElems.AddColumn("fDept", "Подразденение", new ColParam() { Repository = reDept });

            fLegalLookUpEdit.Properties.DataSource = ALogic.Spr.SprLogic.GetUrFiz();
            idSegmentLookUpEdit.Properties.DataSource = ALogic.Spr.SprLogic.GetSegment();

            fLegalLookUpEdit.Properties.DisplayMember = "Value";
            fLegalLookUpEdit.Properties.ValueMember = "Id";

            idSegmentLookUpEdit.Properties.DisplayMember = "Value";
            idSegmentLookUpEdit.Properties.ValueMember = "Id";

            idSegmentLookUpEdit.EditValueChanged += IdSegmentLookUpEdit_EditValueChanged;

            tContract.AddColumn("idContract", "Код");
            tContract.AddColumn("nContract", "Наименование");
            tContract.AddColumn("nKontr", "Контрагент");
            tContract.AddColumn("nType", "Тип договора");
            tContract.AddColumn("nNom", "Номер договора");

            tPerson.AddColumn("nContr", "Наименование");


            tElems.GV.FocusedRowChanged += GV_FocusedRowChanged;
        }

      

        public override void LoadData(object sender, EventArgs e)
        {
           // _entity = ALogic.Logic.Kontr.CompanyLogic.Get();
          //  tElems.DataSource = _entity.listKontrEntity.ConvertToDataTable();

            tElems.DataSource.Rows.Add(1, 0, "Головное подразделение", 1, 1, 1);

            DataTable tableConvention = new DataTable();
            tableConvention.Columns.Add("idContract");
            tableConvention.Columns.Add("nContract");
            tableConvention.Columns.Add("nKontr");
            tableConvention.Columns.Add("nType");
            tableConvention.Columns.Add("nNom");

            tableConvention.Rows.Add("1", "Договор", "Автодоставка", "Поставка закупки", "244-099");

            tContract.DataSource = tableConvention;

            DataTable tablePerson = new DataTable();
            tablePerson.Columns.Add("nContr");
            tablePerson.Rows.Add("Иванив Иван Иванович");

            tPerson.DataSource = tablePerson;
        }

        private void IdSegmentLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            var value = (int)(sender as LookUpEdit).EditValue;
            string text = "";
            switch (value)
            {
                case 1:
                    text = "Кол-во слесарей";
                    break;
                case 2:
                    text = "Кол-во продавцов";
                    break;
                case 3:
                    text = "Кол-во автомобилей";
                    break;
                case 4:
                    text = "Кол-во продавцов";
                    break;
            }

            lciVal.Text = text;
            lciVal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lciExpert.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lciPotential.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        private void GV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = tElems.FocusedRow;
            if (row != null)
            {
                if (row["fLegal"] != DBNull.Value)
                    xtraTabPage1.PageVisible = (bool)row["fLegal"];
                else
                    xtraTabPage1.PageVisible = false;

                if (row["fDept"] != DBNull.Value)
                    xtraTabPage2.PageVisible = (bool)row["fDept"];
                else
                    xtraTabPage2.PageVisible = false;
            }
        }

        private void ReUr_CheckedChanged1(object sender, EventArgs e)
        {
            xtraTabPage2.PageVisible = (sender as DevExpress.XtraEditors.CheckEdit).Checked;
        }

        private void ReUr_CheckedChanged(object sender, EventArgs e)
        {
            xtraTabPage1.PageVisible = (sender as DevExpress.XtraEditors.CheckEdit).Checked;
        }

      

        private void fStructDeptCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            lgcSP.Visibility = (sender as CheckEdit).Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void fDeliveryPointCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            lgcTD.Visibility = (sender as CheckEdit).Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void fVisitPointCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            lgcTP.Visibility = (sender as CheckEdit).Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void textEdit20_EditValueChanged(object sender, EventArgs e)
        {
            string val = (sender as TextEdit).EditValue.ToString();
            int kol = 0;

            if (int.TryParse(val, out kol))
            {
                int type = (int)idSegmentLookUpEdit.EditValue;
                int calc = 0;
                switch (type)
                {
                    case 1:
                        calc = kol * 200000;
                        break;
                    case 2:
                        calc = kol * 500000;
                        break;
                    case 3:
                        calc = kol * 2500;
                        break;
                    case 4:
                        calc = kol * 1500000;
                        break;
                }

                tePotential.Text = calc.ToString() + "р.";
            }
        }
    }
}
