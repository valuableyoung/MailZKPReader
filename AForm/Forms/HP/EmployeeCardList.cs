using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForm.Base;
using DevExpress.XtraEditors.Repository;
using ALogic.Logic.HP;
using ALogic.Logic.SPR;

namespace AForm.Forms.HP
{
    public partial class EmployeeCardList : AWindow
    {
        public EmployeeCardList()
        {
            InitializeComponent();
            this.Tag = "Список сотрудников";
        }


        public override void LoadControls()
        {
            tMain.AddColumn("id_kontr", "Код");
            tMain.AddColumn("n_kontr_full", "ФИО");
            tMain.AddColumn("tel3", "Телефон");

            RepositoryItemLookUpEdit reUnit = new RepositoryItemLookUpEdit();
            reUnit.DataSource = SprLogic.GetSprUnit();
            reUnit.DisplayMember = "nUnit";
            reUnit.ValueMember = "idUnit";

            tMain.AddColumn("id_torg", "Отдел", new ColParam() {  Repository = reUnit });

            RepositoryItemLookUpEdit rePost = new RepositoryItemLookUpEdit();
            rePost.DataSource = SprLogic.GetPost();
            rePost.DisplayMember = "nPost";
            rePost.ValueMember = "idPost";

            tMain.AddColumn("id_post", "Должность", new ColParam() { Repository = rePost });
            tMain.AddColumn("b_day", "Дата рождения", new ColParam() {  HorzAlignment = DevExpress.Utils.HorzAlignment.Center });
            tMain.AddColumn("Age", "Возраст");
            tMain.AddColumn("last_compare", "Дата поступления", new ColParam() {  HorzAlignment = DevExpress.Utils.HorzAlignment.Center});

            RepositoryItemLookUpEdit reCondEmpl = new RepositoryItemLookUpEdit();
            reCondEmpl.DataSource = SprLogic.GetCondEmpl();
            reCondEmpl.DisplayMember = "Value";
            reCondEmpl.ValueMember = "Id";

            tMain.AddColumn("idCondEmpl", "СтатусСотр", new ColParam() { Repository = reCondEmpl });
            tMain.AddColumn("adress_fact", "Адрес");
            tMain.AddColumn("PassWithCar", "Авто");
            tMain.AddColumn("WorkTime", "Стаж работы");



            RepositoryItemLookUpEdit reFirm = new RepositoryItemLookUpEdit();
            reFirm.DataSource = SprLogic.GetFirms();
            reFirm.DisplayMember = "nFirm";
            reFirm.ValueMember = "idFirm";

            tMain.AddColumn("id_firm", "Фирма", new ColParam() {  Repository = reFirm });

            tMain.EventLoad += new EventHandler(tMain_EventLoad);
            tMain.EventAdd += new EventHandler(tMain_EventAdd);
            tMain.EventEdit += new EventHandler(tMain_EventEdit);

            SetTableEditAll(tMain, false);           
        }       

        void tMain_EventEdit(object sender, EventArgs e)
        {
            if (tMain.FocusedRow != null)
            {
                int idKontr = int.Parse(tMain.FocusedRow["id_kontr"].ToString());
                var wind = new EmployeeCardEdit(idKontr);
                wind.OnChange += tMain_EventLoad;
                WindowOpener.OpenWindow(wind);
            }
        }

        void tMain_EventAdd(object sender, EventArgs e)
        {
            var wind = new EmployeeCardEdit();
            wind.OnChange += tMain_EventLoad;
            WindowOpener.OpenWindow(wind);
        }

        void tMain_EventLoad(object sender, EventArgs e)
        {
            


            tMain.DataSource = EmployeeCardLogic.GetEmployees();

            var col = tMain.GV.Columns["b_day"];

            var x = col.AppearanceCell.TextOptions.HAlignment;
            //var a = col.App

        }
    }
}
