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
using ALogic.Logic.SPR;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using ALogic.Model.EntityFrame;

namespace AForm.Forms.Kontr
{
    public partial class CompanyEdit3 : AWindow
    {
        //CompanyEntity _entity;
        public event EventHandler OnChange;
        public CompanyEdit3( int idEntity )
        {
            InitializeComponent();
            this.Tag = this.Text = "Редактирование сущности";
            //_entity = null;
        }

        public override void LoadControls()
        {           
            fLegalLookUpEdit.Properties.DataSource = SprLogic.GetUrFiz();
            idSegmentLookUpEdit.Properties.DataSource = SprLogic.GetSegment();

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

            tMaliTime.AddColumn("Time", "Время");
            tMaliTime.AddColumn("Pn", "Пн", new ColParam() {  Repository = new RepositoryItemCheckEdit() });
            tMaliTime.AddColumn("Vt", "Вт", new ColParam() { Repository = new RepositoryItemCheckEdit() });
            tMaliTime.AddColumn("Sr", "Ср", new ColParam() { Repository = new RepositoryItemCheckEdit() });
            tMaliTime.AddColumn("Cht", "Чт", new ColParam() { Repository = new RepositoryItemCheckEdit() });
            tMaliTime.AddColumn("Pt", "Пт", new ColParam() { Repository = new RepositoryItemCheckEdit() });
            tMaliTime.AddColumn("Sb", "Сб", new ColParam() { Repository = new RepositoryItemCheckEdit() });
            tMaliTime.AddColumn("Vs", "Вс", new ColParam() { Repository = new RepositoryItemCheckEdit() });
            tMaliTime.AddColumn("Kat", "Категория");
            tMaliTime.AddColumn("Name", "Наименование прайса");
            tMaliTime.AddColumn("Text", "Текст письма с прайс листом");

            tMail.AddColumn("Type", "Вид");
            tMail.AddColumn("eMail", "Email");
            tMail.AddColumn("Mask", "Маска файла");
            tMail.AddColumn("Mask2", "Маска темы письма");
            tMail.AddColumn("NameFile", "Название файла с првйсом");
            tMail.AddColumn("NameFileAsia", "Название от Азия ойл");
        }     

        public override void LoadData(object sender, EventArgs e)
        {
           // _entity = ALogic.Logic.Kontr.CompanyLogic.Get();
          
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
            tPerson.DataSource = tablePerson;

            DataTable tableMailTime = new DataTable();
            tableMailTime.Columns.Add("Time");
            tableMailTime.Columns.Add("Pn", typeof(bool));
            tableMailTime.Columns.Add("Vt", typeof(bool));
            tableMailTime.Columns.Add("Sr", typeof(bool));
            tableMailTime.Columns.Add("Cht", typeof(bool));
            tableMailTime.Columns.Add("Pt", typeof(bool));
            tableMailTime.Columns.Add("Sb", typeof(bool));
            tableMailTime.Columns.Add("Vs", typeof(bool));
            tableMailTime.Columns.Add("Kat");
            tableMailTime.Columns.Add("Name");
            tableMailTime.Columns.Add("Text");
            tableMailTime.Rows.Add("7:00", true, true, true, true, true, false, false, "Категория 1", "Прайс клиенту", "Привет!!!!");
            tMaliTime.DataSource = tableMailTime;

            DataTable tableMail = new DataTable();
            tableMail.Columns.Add("Type");
            tableMail.Columns.Add("eMail");
            tableMail.Columns.Add("Mask");
            tableMail.Columns.Add("Mask2");
            tableMail.Columns.Add("NameFile");
            tableMail.Columns.Add("NameFileAsia");

            tableMail.Rows.Add("eMail для отправки сверок", "turishev@mail.ru", "", "", "", "");

            tMail.DataSource = tableMail;

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
            /*
            var row = tElems.FocusedRow;
            if (row != null)
            {
                if (row["fLegal"] != DBNull.Value)
                {
                    xtpHos.PageVisible = (bool)row["fLegal"];
                    xtpEmeil.PageVisible = xtpHos.PageVisible;
                    xtpTD.PageVisible = xtpHos.PageVisible;
                    xtpContract.PageVisible = xtpHos.PageVisible;
                }
                else
                {
                    xtpHos.PageVisible = false;
                    xtpEmeil.PageVisible = xtpHos.PageVisible;
                    xtpTD.PageVisible = xtpHos.PageVisible;
                    xtpContract.PageVisible = xtpHos.PageVisible;
                }

                if (row["fDept"] != DBNull.Value)
                {
                    xtpDept.PageVisible = (bool)row["fDept"];
                    xtpPotential.PageVisible = xtpDept.PageVisible;
                    xtpWork.PageVisible = xtpDept.PageVisible;
                }
                else
                {
                    xtpDept.PageVisible = false;
                    xtpPotential.PageVisible = false;
                    xtpWork.PageVisible = false;
                }
            }
            */
        }     

        private void fStructDeptCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            lgcSP.Visibility = (sender as CheckEdit).Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

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

        private void textEdit11_Properties_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtpContact;

            tPerson.DataSource.Rows.Add("Иванив Иван Иванович");
            fLPRCheckEdit.Checked = true;

            textEdit11.Text = "Иванив Иван Иванович";
        }
    }
}
