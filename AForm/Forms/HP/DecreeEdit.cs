using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForm.Base;
using ALogic.Model.EntityFrame;
using ALogic.Logic.Base;
using ALogic.Logic.SPR;
using DevExpress.XtraEditors.Repository;
using FastReport;

namespace AForm.Forms.HP
{
    public partial class DecreeEdit : AWindow
    {
        public event EventHandler OnChange;
        int _idDecree;
        Decree _entity;
        

        public DecreeEdit(int idDecree = 0)
        {
            InitializeComponent();
            this.Tag = this.Text = "Приказ №" + idDecree;
            _idDecree = idDecree;
        }

        public override void LoadControls()
        {
            RepositoryItemLookUpEdit r = new RepositoryItemLookUpEdit();
            r.DataSource = SprLogic.GetEmployers();
            r.DisplayMember = "nKontr";
            r.ValueMember = "idKontr";

            tDetail.AddColumn("IdKontr", "Сотрудник", new ColParam() {  Repository = r });
            tDetail.AddColumn("fSee", "На ознакомление");
            tDetail.AddColumn("fDossier", "В досье");

            nTypeSignLookUpEdit.Properties.DataSource = SprLogic.GetTypeSing();
            nTypeSignLookUpEdit.Properties.DisplayMember = "Value";
            nTypeSignLookUpEdit.Properties.ValueMember = "Id";


            SetGotFocusColorWhite(_dlc);
        }

        public override void LoadData(object sender, EventArgs e)
        {
            _entity = ALogic.Logic.HP.DecreeLogic.Get(_idDecree);
            _dlc.DataSource = _entity;
            tDetail.DataSource = _entity.listrKontrDecree.ConvertToDataTable();
            this.Text = _entity.nDecree;
            rte.RtfText = _entity.Content;
        }

        private void bClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData(this, null);
        }

        private void bSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            if (SaveData())
            {
                if (OnChange != null)
                    OnChange(_idDecree, null);
                DataChanged = false;
                Close();
            }
        }

        private bool SaveData()
        {
            AcceptAllTable(this);
            panelControl1.Focus();
            _entity.Content = rte.RtfText;

            _entity.listrKontrDecree = tDetail.DataSource.DataTableToList<rKontrDecree>().ToList();
            _idDecree = ALogic.Logic.HP.DecreeLogic.Save(_entity);

            if (_entity.ErrorList.Count > 0)
            {
                string message = "Не корректные данные" + "\n";
                foreach (var val in _entity.ErrorList.Values)
                {
                    message += val + ";" + "\n";
                }

                MessageBox.Show(message);
                SetErrComponentRed(_dlc, _entity.ErrorList.Keys.ToList());
                return false;
            }           
         
            return true;
        }

        private void bSee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            Report report = new Report();
            report.Load(@"S:\FRX\Приказ.frx");
            report.SetParameterValue("idDecree", _entity.IdDecree);
            report.Show();
        }
    }
}
