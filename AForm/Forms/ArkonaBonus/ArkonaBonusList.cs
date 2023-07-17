using AForm.Base;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AForm.Forms.ArkonaBonus
{
    public partial class ArkonaBonusList : AWindow
    {
        public ArkonaBonusList()
        {
            InitializeComponent();
            this.Text = "Баллы А+ по категориям, %";
            this.Tag = "Баллы А+ по категориям, %";
        }

        public override void LoadControls()
        {
            RepositoryItemLookUpEdit rBonus = new RepositoryItemLookUpEdit();
            rBonus.DataSource = ALogic.Logic.SPR.SprLogic.GetBrands();
            rBonus.DisplayMember = "nBrand";
            rBonus.ValueMember = "idBrand";
            tMain.GV.OptionsView.ColumnAutoWidth = true;
            tMain.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            tMain.GV.PopulateColumns();
            tMain.GC.Font = new System.Drawing.Font("Arial", 9.75F);
            //tMain.AddColumn("idBrand", "Бренд", new ColParam() {  Repository = rBonus, fReadOnly = true });
            //tMain.AddColumn("retailBonus21", "Розница 1, %", new ColParam() {  AfterPoint = 2 });
            //tMain.AddColumn("retailBonus22", "Розница 2, %", new ColParam() { AfterPoint = 2 });
            //tMain.AddColumn("retailBonus23", "Розница 3, %", new ColParam() { AfterPoint = 2 });
            ////tMain.AddColumn("optBonus", "Опт, %", new ColParam() { AfterPoint = 2 });
            //tMain.AddColumn("retailASKUBonus21", "Розница 1 ASKU %", new ColParam() { AfterPoint = 2 });
            //tMain.AddColumn("retailASKUBonus22", "Розница 1 ASKU %", new ColParam() { AfterPoint = 2 });
            //tMain.AddColumn("retailASKUBonus23", "Розница 3 ASKU %", new ColParam() { AfterPoint = 2 });
            ////tMain.AddColumn("optASKUBonus", "Опт ASKU %", new ColParam { AfterPoint = 2 });
            ////коммент до релиза
            //tMain.AddColumn("retailBonus20", "Розница 0, %", new ColParam { AfterPoint = 2 });
            //tMain.AddColumn("opt1Bonus", "ОПТ 1, %", new ColParam { AfterPoint = 2 });
            //tMain.AddColumn("opt2Bonus", "ОПТ 2, %", new ColParam { AfterPoint = 2 });
            //tMain.AddColumn("retailASKUBonus20", "Розница 0 ASKU, %", new ColParam { AfterPoint = 2 });
            //tMain.AddColumn("opt1ASKUBonus", "ОПТ 1 ASKU, %", new ColParam { AfterPoint = 2 });
            //tMain.AddColumn("opt2ASKUBonus", "ОПТ 2 ASKU, %", new ColParam { AfterPoint = 2 });

            //tMain.AddColumn("dates", "Начало акции" );
            //tMain.AddColumn("datee", "Конец акции" );

            tMain.EventLoad += TMain_EventLoad;
            //tMain.EventSave += TMain_EventSave;
            tMain.GV.OptionsBehavior.Editable = false;
            //SetTableEditAll(tMain, false);
        }

      
        private void TMain_EventLoad(object sender, EventArgs e)
        {
            tMain.DataSource = ALogic.Logic.ArkonaBonus.ArkonaBonusLogic.GetAll();
        }

        public override void LoadData(object sender, EventArgs e)
        {
            TMain_EventLoad(this, null);
        }
    }
}
