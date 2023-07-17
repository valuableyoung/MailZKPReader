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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraDataLayout;
using ALogic.Logic.HP;
using ALogic.Logic.SPR;
using ALogic.Model.Entites.HP;
using ALogic.Logic.Heplers;
using ALogic.Logic.Base;
using ALogic.Model.EntityFrame;



namespace AForm.Forms.HP
{
    public partial class EmployeeCardEdit : AWindow
    {
        EmployeeCardEntity _entity;
        int _idKontr;
        public event EventHandler OnChange;

        public EmployeeCardEdit(int idKontr = 0 )
        {
            InitializeComponent();
            this.Tag = "Редактирование сотрудника " + idKontr.ToString();          
            _idKontr = idKontr;
        }     

        public override void LoadControls()
        {
            id_firmLookUpEdit.Properties.DataSource = SprLogic.GetFirms();
            id_firmLookUpEdit.Properties.DisplayMember = "nFirm";
            id_firmLookUpEdit.Properties.ValueMember = "idFirm";

            id_torgLookUpEdit.Properties.DataSource = SprLogic.GetSprUnit();
            id_torgLookUpEdit.Properties.DisplayMember = "nUnit";
            id_torgLookUpEdit.Properties.ValueMember = "idUnit";

            id_directLookUpEdit.Properties.DataSource = SprLogic.GetSprDirect();
            id_directLookUpEdit.Properties.DisplayMember = "nDirect";
            id_directLookUpEdit.Properties.ValueMember = "idDirect";

            investor_shareholderLookUpEdit.Properties.DataSource = SprLogic.GetInvestorShareholder();
            investor_shareholderLookUpEdit.Properties.DisplayMember = "Value";
            investor_shareholderLookUpEdit.Properties.ValueMember = "Id";

            id_postLookUpEdit.Properties.DataSource = SprLogic.GetPost();
            id_postLookUpEdit.Properties.DisplayMember = "nPost";
            id_postLookUpEdit.Properties.ValueMember = "idPost";          

            salary_typeLookUpEdit.Properties.DataSource = SprLogic.GetSalaryType();
            salary_typeLookUpEdit.Properties.DisplayMember = "Value";
            salary_typeLookUpEdit.Properties.ValueMember = "Id";

            idCondEmplLookUpEdit.Properties.DataSource = SprLogic.GetCondEmpl();
            idCondEmplLookUpEdit.Properties.DisplayMember = "Value";
            idCondEmplLookUpEdit.Properties.ValueMember = "Id";

            id_condLookUpEdit.Properties.DataSource = SprLogic.GetCondKontr();
            id_condLookUpEdit.Properties.DisplayMember = "nCond";
            id_condLookUpEdit.Properties.ValueMember = "idCond";

            tAuto.AddColumn("nAuto", "Наименование");
            tAuto.AddColumn("RegNumber", "Гос. номер");
            tAuto.AddColumn("Tonnage", "Масса груза", new ColParam() {  AfterPoint = 0 });
            tAuto.AddColumn("Volume", "Объем груза", new ColParam() { AfterPoint = 0 });

            tChildren.AddColumn("nChild", "ФИО");
            tChildren.AddColumn("DateBirth", "Дата рождения");
            tChildren.AddColumn("Comment", "Комментарий");

            RepositoryItemLookUpEdit reTabelRowType = new RepositoryItemLookUpEdit();
            reTabelRowType.DataSource = SprLogic.GetTabelRowType();
            reTabelRowType.DisplayMember = "nTypeRow";
            reTabelRowType.ValueMember = "idTypeRow";

            tTabel.AddColumn("type_row", "Тип", new ColParam() { Repository = reTabelRowType });
            tTabel.AddColumn("time_start", "Начало");
            tTabel.AddColumn("time_end", "Конец");
            tTabel.AddColumn("commentary", "Комментарий");

            RepositoryItemLookUpEdit rePost = new RepositoryItemLookUpEdit();
            rePost.DataSource = SprLogic.GetPost();
            rePost.DisplayMember = "nPost";
            rePost.ValueMember = "idPost";

            RepositoryItemLookUpEdit reUnit = new RepositoryItemLookUpEdit();
            reUnit.DataSource = SprLogic.GetSprUnit();
            reUnit.DisplayMember = "nUnit";
            reUnit.ValueMember = "idUnit";

            RepositoryItemLookUpEdit reFirm = new RepositoryItemLookUpEdit();
            reFirm.DataSource = SprLogic.GetFirms();
            reFirm.DisplayMember = "nFirm";
            reFirm.ValueMember = "idFirm";

            tStafMove.AddColumn("idPost", "Должность", new ColParam { Repository = rePost });
            tStafMove.AddColumn("DateS", "Поступил");
            tStafMove.AddColumn("DateE", "Ушел");
            tStafMove.AddColumn("idUnit", "Подразделение", new ColParam { Repository = reUnit });
            tStafMove.AddColumn("nDecree", "Приказ");
            tStafMove.AddColumn("idFirm", "Юр. лицо", new ColParam { Repository = reFirm });

            reDecree = new RepositoryItemLookUpEdit();
            reDecree.DataSource = SprLogic.GetDecree();
            reDecree.DisplayMember = "nDecree";
            reDecree.ValueMember = "idDecree";

            tDecree.AddColumn("IdDecree", "Наименование", new ColParam() {  Repository = reDecree });              

            tDecree.EventAdd += new EventHandler(tDecree_EventAdd);
            tDecree.EventEdit += new EventHandler(tDecree_EventEdit);
            tDecree.AddButton("Найти", AForm.Properties.Resources.find, tDecree_eventFound, true); 

            SetGotFocusColorWhite(_dlcMain);
            SetTableEditAll(tDecree, false);
            SetTableEditAll(tTabel, false);    
        }

        private RepositoryItemLookUpEdit reDecree;

        void tDecree_eventFound(object sender, EventArgs e)
        {
            var wind = new DecreeList(true);
            wind.OnCheck += new EventHandler(wind_OnCheck);
            WindowOpener.OpenWindow(wind);
        }

        void wind_OnCheck(object sender, EventArgs e)
        {
           var row = tDecree.DataSource.NewRow();
           row["idDecree"] = (int)sender;
           row["fSee"] = false;
           row["fDossier"] = true;
           tDecree.DataSource.Rows.Add(row);
           reDecree.DataSource = SprLogic.GetDecree();
        }  

        void tDecree_EventAdd(object sender, EventArgs e)
        {
            var wind = new DecreeEdit();
            wind.OnChange += new EventHandler(wind_OnCheck);
            WindowOpener.OpenWindow(wind);
        }       

        void tDecree_EventEdit(object sender, EventArgs e)
        {
            var row = tDecree.FocusedRow;
            if (row != null)
            {
                int idDecree = int.Parse(row["idDecree"].ToString());
                var wind = new DecreeEdit(idDecree);
                WindowOpener.OpenWindow(wind);
            }           
        }

        public override void LoadData(object sender, EventArgs e)
        {           
            _entity = EmployeeCardLogic.GetEmployee(_idKontr);
            _dlcMain.DataSource = _entity.spr_kontr;         
            
            tChildren.DataSource = _entity.listChildren.ConvertToDataTable();   
            tAuto.DataSource = _entity.listAvto.ConvertToDataTable();
            tDecree.DataSource = _entity.listKontrDecree.ConvertToDataTable();
            tTabel.DataSource = _entity.listTabel.ConvertToDataTable();
            tStafMove.DataSource = _entity.listStaffMove.ConvertToDataTable();
                      
            this.Text = _entity.spr_kontr.n_kontr_full == null ? "Новый сотрудник" : _entity.spr_kontr.n_kontr_full;
            DataChanged = false;
        }   

        private void bLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData(this, null);
        }

        private void bClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            Close();
        }

        private void bSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {                     
            AcceptAllTable(this);

            panel1.Focus();     
         
            _entity.listChildren = tChildren.DataSource.DataTableToList<sChildren>().ToList();
            _entity.listStaffMove = tStafMove.DataSource.DataTableToList<StaffMove>().ToList();
            _entity.listKontrDecree = tDecree.DataSource.DataTableToList<rKontrDecree>().ToList();
            _entity.listAvto = tAuto.DataSource.DataTableToList<sAutoDelivery>().ToList();

            EmployeeCardLogic.Save(_entity);
            _idKontr = int.Parse(_entity.spr_kontr.id_kontr.ToString());
           
            if (_entity.ErrorList.Count > 0)
            {
                string message = "Не корректные данные" + "\n";
                foreach (var val in _entity.ErrorList.Values)
                {
                    message += val + ";" + "\n";
                }

                MessageBox.Show(message);
                SetErrComponentRed(_dlcMain, _entity.ErrorList.Keys.ToList());
                return;
            }

            if (OnChange != null)
                OnChange(this, null);

            LoadData(this, null);
        }
    }
}
