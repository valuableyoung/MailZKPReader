using AForm.Base;
using ALogic.Logic.SPR;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace AForm.Forms.ArkonaBonus
{
    public partial class ArkonaBonusArm : AWindow
    {
        public ArkonaBonusArm()
        {
            InitializeComponent();
            this.Tag = "Управление баллами А+";
            this.Text = "Управление баллами А+";
            setDateTimePicker();

        }

        private void setDateTimePicker()
        {
            var date = DateTime.Now.AddMonths(-2).AddDays(1-DateTime.Now.Day);
            dtPicker.Value = date;
            dtPicker.Value = Convert.ToDateTime("01.02.2020");
        }

        public override void LoadControls()
        {
            var parForNumber = new ColParam() { fGroupSummary = true, fSummary = true, SummaryItemType = DevExpress.Data.SummaryItemType.Sum, AfterPoint = 2 };
            var parForNumberR2 = new ColParam() { fGroupSummary = true, fSummary = true, SummaryItemType = DevExpress.Data.SummaryItemType.Sum, AfterPoint = 2 };

            tCustomer.AddColumn("idKontr", "Код");
            tCustomer.AddColumn("nKontr", "Покупатель");
            tCustomer.AddColumn("nTypeArkonaBonus", "Прогр. лояльности");
            tCustomer.AddColumn("sumDoc", "Сумма накладных", parForNumberR2);
            tCustomer.AddColumn("sumBonus", "Всего баллов", new ColParam { AfterPoint = 2 });
            tCustomer.AddColumn("sumBonusNo", "Не оплачено", new ColParam { AfterPoint = 2 });
            tCustomer.AddColumn("sumBonusN", "К выдаче", new ColParam { AfterPoint = 2 });
            tCustomer.AddColumn("sumBonusPay", "Использовано", new ColParam { AfterPoint = 2 });
            tCustomer.AddColumn("Agent", "Агент");



            //tCustomer.AddButton("Погасить задолженность", Properties.Resources.coins, CreateBonusDoc);
            //tCustomer.AddButton("Пересчитать бонусы", Properties.Resources.calculator, RecalcBonus);

            tCustomer.GV.OptionsView.ShowFooter = true;
            tCustomer.GV.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            tCustomer.GV.OptionsView.ColumnAutoWidth = false;
            tCustomer.GV.OptionsView.RowAutoHeight = true;


            tCustomer.EventLoad += LoadCustomer;
            tCustomer.GV.FocusedRowChanged += GV_FocusedRowChanged;

            tBonus.AddColumn("dateDoc", "Дата");
            tBonus.AddColumn("nomDoc", "Номер", new ColParam { HorzAlignment = DevExpress.Utils.HorzAlignment.Center });
            tBonus.AddColumn("sumDoc", "Сумма накладной", parForNumberR2);
            tBonus.AddColumn("sumPay", "Сумма оплаты", parForNumberR2);
            tBonus.AddColumn("sumBonus", "Сумма балла", new ColParam { AfterPoint = 2 });
            tBonus.AddColumn("SumBonusPay", "Списано балла", parForNumber);
            tBonus.AddColumn("FinDateDoc", "Дата док.баллов");
            tBonus.AddColumn("FinNomDoc", "Номер Док.баллов");


            //tBonus.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            tCustomer.GV.OptionsView.ColumnAutoWidth = false;
            tCustomer.GV.OptionsView.RowAutoHeight = true;


            //Todo:Вернуться
            //tBonus.GV.

            var reCh = new RepositoryItemLookUpEdit();

            DataTable dtChb = new DataTable();
            dtChb.Columns.Add("Id", typeof(int));
            dtChb.Columns.Add("Name", typeof(string));
            dtChb.Rows.Add(0, "");
            dtChb.Rows.Add(1, "Подтвержден");

            reCh.DataSource = dtChb;
            reCh.DisplayMember = "Name";
            reCh.ValueMember = "Id";

            tBonus.AddColumn("fcheck", "Под-ние", new ColParam() { Repository = reCh });

            tBonus.GV.OptionsView.ShowFooter = true;
            tBonus.GV.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;

            tBonus.EventLoad += LoadBonus;
            tBonus.GV.FocusedRowChanged += GV_FocusedRowChanged1;

            tBonusDetail.AddColumn("nbrand", "Бренд");
            tBonusDetail.AddColumn("sumbrand", "Сумма по бренду", parForNumberR2);
            tBonusDetail.AddColumn("sumbrandbonus", "Сумма балла", parForNumber);

            tBonusDetail.GV.OptionsView.ShowFooter = true;
            tBonusDetail.GV.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;


            tBonusDetail.EventLoad += LoadBonusDetail;

            SetTableEditAll(tCustomer, false);
            SetTableEditAll(tBonus, false);
            SetTableEditAll(tBonusDetail, false);


            tCustomer.EventSave += btnSave_Click;

            toolTip1.SetToolTip(btUpdate, "Обновить");
            toolTip1.SetToolTip(btnGroup, "Сгруппировать");
            toolTip1.SetToolTip(btnFilter, "Фильтрация");
            toolTip1.SetToolTip(btnFind, "Поиск");
            toolTip1.SetToolTip(btnSave, "Сохранить");
            toolTip1.SetToolTip(btnCreateBonus, "Списать баллы");
            toolTip1.SetToolTip(btnRecalcBonus, "Пересчитать баллы");

        }

        public void RecalcBonus(object sender, EventArgs e)
        {
            if (!CanChangeData())
            {
                MessageBox.Show("Нет прав для выполнения данной операции");
                return;
            }

            var row = tCustomer.FocusedRow;
            if (row == null)
                return;

            SqlParameter paridKontr = new SqlParameter("idKontr", row["idKontr"]);
            SqlParameter pardates = new SqlParameter("dates", DateTime.Now.Date.AddYears(-1));

            //ALogic.DBConnector.DBExecutor.ExecuteQuery("exec up_RecalcArconaBonus @idKontr, @dates", paridKontr, pardates);

            LoadData(null, null);
        }

        public void CreateBonusDoc(object sender, EventArgs e)
        {
            if (!CanChangeData())
            {
                MessageBox.Show("Нет прав для выполнения данной операции");
                return;
            }

            var row = tCustomer.FocusedRow;
            if (row == null)
                return;

            object idKontr = row["idKontr"];
            decimal sum = row["sumBonusN"] == DBNull.Value ? 0 : decimal.Parse(row["sumBonusN"].ToString());

            if (sum <= 0)
            {
                MessageBox.Show("Нет бонусов для использования!");
                return;
            }

            string result = ALogic.Logic.ArkonaBonus.ArkonaBonusLogic.GenerateDocCorrBalanse(idKontr, sum, row["nTypeArkonaBonus"].ToString(), row["Agent"].ToString());
            MessageBox.Show(result);
            LoadData(this, null);
        }

        private bool CanChangeData()
        {
            var idUser = User.CurrentUserId;

            if (ALogic.Logic.SPR.User.InRole(idUser, "Developers"))
                return true;

            if (ALogic.Logic.SPR.User.InRole(idUser, "CanPayBonus"))
                return true;

            return false;
        }

        private void GV_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadBonusDetail(null, null);
        }

        private void GV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadBonus(null, null);
        }

        public override void LoadData(object sender, EventArgs e)
        {
            LoadCustomer(null, null);
            LoadBonus(null, null);
        }

        private void LoadCustomer(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                tCustomer.DataSource = ALogic.Logic.ArkonaBonus.ArkonaBonusLogic.GetAllCustomerForBonus(dtPicker.Value);

                var tCustomersumDoc = tCustomer.GV.Columns["sumDoc"];
                tCustomersumDoc.SummaryItem.DisplayFormat = "{0:n2}";

                var tBonusSumDoc = tBonus.GV.Columns["sumDoc"];
                tBonusSumDoc.SummaryItem.DisplayFormat = "{0:n2}";

                var tBonusSumPay = tBonus.GV.Columns["sumPay"];
                tBonusSumPay.SummaryItem.DisplayFormat = "{0:n2}";

                var tBonussumBonus = tBonus.GV.Columns["sumBonus"];
                tBonussumBonus.SummaryItem.DisplayFormat = "{0:n2}";

                var tCustomersumBonus = tBonus.GV.Columns["sumBonus"];
                tCustomersumBonus.SummaryItem.DisplayFormat = "{0:n2}";

                tBonus.GV.ColumnPanelRowHeight = 35;
                tBonus.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                tCustomer.GV.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                LoadBonus(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void LoadBonus(object sender, EventArgs e)
        {
            var row = tCustomer.FocusedRow;
            if (row != null)
            {
                var idKontr = row["idKontr"];
                //var idPromo = row["idPromo"];

                //tBonus.DataSource = ALogic.Logic.ArkonaBonus.ArkonaBonusLogic.GetBonusForCustomer(idKontr, dtPicker.Value, idPromo);
                tBonus.DataSource = ALogic.Logic.ArkonaBonus.ArkonaBonusLogic.GetBonusForCustomer(idKontr, dtPicker.Value);
                LoadBonusDetail(null, null);
            }
        }

        private void LoadBonusDetail(object sender, EventArgs e)
        {
            var row = tBonus.FocusedRow;
            if (row != null)
            {
                var idDoc = row["idDoc"];
                //var idPromo = row["idPromo"];

                //tBonusDetail.DataSource = ALogic.Logic.ArkonaBonus.ArkonaBonusLogic.GetBonusDetailForDoc(idDoc, idPromo);
                tBonusDetail.DataSource = ALogic.Logic.ArkonaBonus.ArkonaBonusLogic.GetBonusDetailForDoc(idDoc);
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            LoadCustomer(null, null);

            //для возможности переноса строк при изменении размеров столбца
            tCustomer.GC.BeginUpdate();
            RepositoryItemMemoEdit Rme = new RepositoryItemMemoEdit
            {
                WordWrap = true,
                AutoHeight = true
            };

            //tCustomer.GV.OptionsView.ColumnAutoWidth = false;
            tCustomer.GV.OptionsView.RowAutoHeight = true;
            foreach (GridColumn c in tCustomer.GV.Columns) // для каждой колонки грида
            {
                c.ColumnEdit = Rme; // назначаем редактором Rme
                //c.BestFit(); // инициализируем автоподбор ширины колонки
            }
            tCustomer.GC.EndUpdate();

        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
                tCustomer.GV.OptionsView.ShowGroupPanel = !tCustomer.GV.OptionsView.ShowGroupPanel;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
                tCustomer.GV.OptionsView.ShowAutoFilterRow = !tCustomer.GV.OptionsView.ShowAutoFilterRow;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            tCustomer.GV.OptionsFind.AlwaysVisible = !tCustomer.GV.OptionsFind.AlwaysVisible;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ext = saveFileDialog1.FileName.Split('.').Last().ToUpper();
                if (ext == "XLS")
                {
                    tCustomer.GV.ExportToXls(saveFileDialog1.FileName);
                }

                if (ext == "XLSX")
                {
                    tCustomer.GV.ExportToXlsx(saveFileDialog1.FileName);
                }

                if (ext == "PDF")
                {
                    tCustomer.GV.ExportToPdf(saveFileDialog1.FileName);
                }

                if (ext == "TXT")
                {
                    tCustomer.GV.ExportToText(saveFileDialog1.FileName);
                }
            }
        }

        private void btnCreateBonus_Click(object sender, EventArgs e)
        {
            CreateBonusDoc(this, null);
        }

        private void btnRecalcBonus_Click(object sender, EventArgs e)
        {
            RecalcBonus(this, null);
        }

        private void ArkonaBonusArm_Load(object sender, EventArgs e)
        {
            //tCustomer.GV.OptionsView.ShowGroupPanel = true;
            //tCustomer.GV.Columns["nKontr"].Group();
            //tCustomer.GV.ExpandAllGroups();
        }
    }
}
