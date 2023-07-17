using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForm.Base;
using FastReport;

namespace AForm.Forms.HP
{
    public partial class DecreeList : AWindow
    {
        bool _ToCheck;

        public event EventHandler OnCheck;

        public DecreeList(bool ToCheck = false)
        {
            InitializeComponent();
            this.Tag = this.Text = "Список приказов";
            _ToCheck = ToCheck;
        }

        public override void LoadControls()
        {
            tMain.AddColumn("IdDecree", "Код");
            tMain.AddColumn("nDecree", "Наименование");
            tMain.AddColumn("TitleDecree", "Заголовок");
            tMain.AddColumn("DateDecree", "Дата");
            tMain.AddColumn("NomDecree", "Номер");

            tMain.EventEdit += new EventHandler(tMain_EventEdit);
            tMain.EventAdd += new EventHandler(tMain_EventAdd);
            tMain.EventLoad += new EventHandler(tMain_EventLoad);
            tMain.EventDelete += new EventHandler(tMain_EventDelete);

            tMain.AddButton("Просмотр", Properties.Resources.find, tMain_EventFind);

            if (_ToCheck)
            {
                tMain.SetTableCheck();
                tMain.GV.DoubleClick +=new EventHandler(tMain_DoubleClick);
            }

            SetTableEditAll(tMain, false);    
        }
        void tMain_EventFind(object sender, EventArgs e)
        {
            var row = tMain.FocusedRow;
            if (row != null)
            {
                int IdDecree = int.Parse(row["IdDecree"].ToString());
                Report report = new Report();
                report.Load(@"S:\FRX\Приказ.frx");
                report.SetParameterValue("idDecree", IdDecree);
                report.Show();
            }
        }

        void  tMain_DoubleClick(object sender, EventArgs e)
        {
            if (OnCheck != null)
            {
                var row = tMain.FocusedRow;
                if (row != null)
                {
                    OnCheck(row["idDecree"], null);
                    Close();
                }
            }
        }

        void tMain_EventDelete(object sender, EventArgs e)
        {
            var row = tMain.FocusedRow;
            if (row != null)
                ALogic.Logic.HP.DecreeLogic.Delete((int)row["IdDecree"]);
            row.Delete();
        }

        void tMain_EventLoad(object sender, EventArgs e)
        {
            tMain.DataSource = ALogic.Logic.HP.DecreeLogic.GetAll();
        }

        void tMain_EventAdd(object sender, EventArgs e)
        {
            var wind = new DecreeEdit();
            WindowOpener.OpenWindow(wind);
            wind.OnChange += tMain_EventLoad;
        }

        void tMain_EventEdit(object sender, EventArgs e)
        {
            var row = tMain.FocusedRow;
            if (row != null)
            {
                var wind = new DecreeEdit((int)row["idDecree"]);
                WindowOpener.OpenWindow(wind);
                wind.OnChange += tMain_EventLoad;
            }
        }
    }
}
