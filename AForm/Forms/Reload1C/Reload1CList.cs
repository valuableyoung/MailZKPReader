using AForm.Base;
using ALogic.DBConnector;
using ALogic.Logic.Reload1C;
using ALogic.Logic.SPR;
using DevExpress.XtraEditors.Repository;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AForm.Forms.Reload1C
{
    public partial class Reload1CList : AWindow
    {
        public Reload1CList()
        {
            InitializeComponent();
            this.Tag = this.Text = "Выгрузка в 1с";
            ProjectProperty.LoadDataAppConfig();
        }

        public override void LoadControls()
        {
            deStart.Value = DateTime.Now.Date;
            deEnd.Value = DateTime.Now.Date;

            fTov.Checked = true;
            fFin.Checked = true;

            leFirm.Properties.DataSource = SprLogic.GetFirms();
            leFirm.Properties.DisplayMember = "nFirm";
            leFirm.Properties.ValueMember = "idFirm";

            leTypeDoc.Properties.DataSource = SprLogic.GetTypeDocTo1c();
            leTypeDoc.Properties.DisplayMember = "nTypeDoc";
            leTypeDoc.Properties.ValueMember = "idTypeDoc";

            leTypeDoc.EditValue = 0;

            tMain.AddColumn("CH", "Выбор", new ColParam() { Repository = new RepositoryItemCheckEdit() });
            tMain.AddColumn("NomDoc", "Номер");
            tMain.AddColumn("DateDoc", "Дата");
            tMain.AddColumn("nKontrDB", "Контрагент дебет");
            tMain.AddColumn("nKontrKR", "Контрагент кредит");
            tMain.AddColumn("F1c", "Выгружен");


            tMain.AddButton("Выбрать все", Properties.Resources.to_do_list_cheked_all, tMain_EventCheckAll);
            tMain.AddButton("Снять выбор", Properties.Resources.to_do_list, tMain_EventUnCheckAll);
            tMain.AddButton("Перевыгрузить", Properties.Resources.tick_ok, tMain_EventReload1C);

            tMain.EventLoad += new EventHandler(tMain_EventLoad);

            deDateClose.EditValue = DateTime.Parse(ALogic.DBConnector.DBAppParam.GetAppParamYN(455).ToString());
        }

        void tMain_EventReload1C(object sender, EventArgs e)
        {
            int val = int.Parse(ALogic.DBConnector.DBAppParam.GetAppParamValue(435).ToString());

            if (val != 0)
            {
                MessageBox.Show("Выгрузка в 1с уже ведется!");
                return;
            }

            ALogic.DBConnector.DBAppParam.SetAppParamValue(435, 1);
            
            AcceptAllTable(this);
            ALogic.Logic.Reload1C.Reload1CLogic.ReloadChecked((int)leFirm.EditValue, tMain.DataSource);
            
            ALogic.DBConnector.DBAppParam.SetAppParamValue(435, 0);
        }

        void tMain_EventUnCheckAll(object sender, EventArgs e)
        {
            foreach (var row in tMain.DataSource.AsEnumerable())
                row["CH"] = false;
        }

        void tMain_EventCheckAll(object sender, EventArgs e)
        {
            foreach (var row in tMain.DataSource.AsEnumerable())
                row["CH"] = true;
        }

        void tMain_EventLoad(object sender, EventArgs e)
        {
            if (leFirm.EditValue == null)
            {
                MessageBox.Show("Выберите фирму");
                return;
            }
            Reload1CParams par = new Reload1CParams();
            par.DateS = deStart.Value;
            par.DateE = deEnd.Value;
            par.FinDoc = fFin.Checked;
            par.TovDoc = fTov.Checked;
            par.idFirm = (int)leFirm.EditValue;
            par.idTypeDoc = (int)leTypeDoc.EditValue;

            tMain.DataSource = ALogic.Logic.Reload1C.Reload1CLogic.GetDocs(par);
        }





        public override void LoadData(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bdClose_Click(object sender, EventArgs e)
        {
            ALogic.DBConnector.DBAppParam.SetAppParamYN(455, deDateClose.EditValue);
        }
    }
}
