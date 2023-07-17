using ALogic.Logic.Heplers;
using ALogic.Logic.Old.Parsers;
using ALogic.Logic.SPR;
using ALogic.Logic.Reload1C;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace AForm.Forms.Old.Parsers
{
    public partial class PriceFuture : Form
    {
        ParserSettings currentSetting = new ParserSettings();
        //List<ParserSettingsColumn> columnConditions = new List<ParserSettingsColumn>();
        DataTableCollection sheets;
        public int prevSheetN { get; set; }
        public int idParserSettings { get; set; }
        DateTime _datefrom;
        ParserFuture parserFuture = new ParserFuture();
        BackgroundWorker worker;
        BackgroundWorker analizWorker;
        DataTable analizTable;
        DataTable notconfirm;
        DataTable notconfirmdetail;
        int _idSupplier;
        int idPf;
        int loadType;
        ToolTip hint;

        public PriceFuture()
        {
            InitializeComponent();
        }

        private bool CanConfirm()
        {
            var idUser = User.CurrentUserId;

            if (ALogic.Logic.SPR.User.InRole(idUser, "Developers"))
                return true;

            if (ALogic.Logic.SPR.User.InRole(idUser, "CanConfirmPriceFuture"))
                return true;

            return false;
        }

        private void PriceFuture_Load(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            analizWorker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            analizWorker.DoWork += AnalizWorker_DoWork;
            analizWorker.RunWorkerCompleted += AnalizWorker_RunWorkerCompleted;
            analizWorker.WorkerSupportsCancellation = true;
            analizWorker.WorkerReportsProgress = true;

            prevSheetN = 0;

            ParserSettingsHelper.fillSupplier(idSupplier);
            ParserSettingsHelper.fillCurrency(idCur);
            ParserSettingsHelper.FillFutureLoadType(cbLoadType);

            gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            //tpConfirmLoad.Parent = null;
            btnConfirm.Enabled = CanConfirm();

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(this.btnConfirm, "Подтверждение выбранного прайса");
            toolTip1.SetToolTip(this.btnRefresh, "Обновить список");
            toolTip1.SetToolTip(this.btnSaveExcel, "Сохранить перечень товаров в Excel");

            hint = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                //ToolTipIcon = ToolTipIcon.Info,
                ShowAlways = true
            };
        }

        private void ActivateSaveBtn()
        {
            if (cbSetting.SelectedIndex > 0 && gridFile.DataSource != null)
            {
                btnSave.Enabled = true;
            }
            else btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbLoadType.SelectedIndex == 0)
            {
                MessageBox.Show("Пожалуйста, укажите тип загрузки для прайса со сроком действия", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            loadType = int.Parse(cbLoadType.SelectedIndex.ToString());

            if (_datefrom > DateTime.Now.Date)
            {
                var idPf = parserFuture.CheckFutureheader(currentSetting.idSupplier, _datefrom, loadType);
                if (idPf > 0)
                {
                    if (DialogResult.Yes != MessageBox.Show("Прайс лист на указанную дату уже загружен, продолжить?", "Внимание!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
                ppLoad.Visible = true;
                worker.RunWorkerAsync();
            }
            else MessageBox.Show("Укажите корректную дату загрузки, дата должна быть больше текущей", "Внимание!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ppLoad.Visible = false;


        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                idPf = parserFuture.CheckFutureheader(currentSetting.idSupplier, _datefrom, loadType);
                if (idPf == 0)
                {
                    idPf = parserFuture.CreateFutureHeader(currentSetting.idSupplier, currentSetting, User.CurrentUserId, _datefrom, loadType);
                }
                else parserFuture.UpdateFutureHeaderUser(idPf, User.CurrentUserId);

                //TODO: реализовать сравнение старой и новой настройки

                MailPriceReader.SaveDataTableWithParams(sheets, currentSetting, User.CurrentUserId, datefrom.Value.Date, true);


                parserFuture.LoadSKU(idPf, sheets[0].Rows.Count);
                //parserFuture.deleteFromPriceOnlineTemp(_idSupplier);
                string str = parserFuture.GetLoadedLog(_idSupplier, idPf);

                var dtconf = parserFuture.CheckNotConfirmed(idPf);

                parserFuture.SendEmail(idPf, _idSupplier, ref dtconf);
                
                MessageBox.Show(str, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var dt = parserFuture.CheckNotConfirmed(idPf);
                if (dt.Rows.Count > 0)
                {
                    parserFuture.SendEmailForConfirm(idPf, _idSupplier, ref dt);
                    parserFuture.SetUnsetConfirm(idPf, 0, 0);
                }
                else
                {
                    parserFuture.SetUnsetConfirm(idPf, 1, User.CurrentUserId);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, отправьте скрин программистам:\n" + ex.ToString(), "Внимание ошибка!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {  }

        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && _idSupplier > 0)
            {
                ppAnaliz.Visible = true;
                if (!analizWorker.IsBusy)
                    analizWorker.RunWorkerAsync();
            }
            else
            {
                analizGrid.DataSource = null;
            }
        }
        private void AnalizWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ppAnaliz.Visible = false;
            analizGrid.DataSource = analizTable;
        }

        private void NotConfirmedPrices(List<int> list)
        {
            try
            {
                notconfirm = parserFuture.GetNotConfirmedLoads(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных по неподтвержденным прайсам: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotConfirmedPricesDetail()
        {
            try
            {
                notconfirmdetail = parserFuture.GetNotConfirmedLoadsDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных по неподтвержденным прайсам: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnalizWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Thread.Sleep(1500);
                analizTable = parserFuture.GeDataAnaliz(_idSupplier);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, отправьте скрин программистам:\n" + ex.ToString(), "Внимание ошибка!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveSettings(string str)
        {
            if (idSupplier.SelectedIndex > 0 && ValidateColumnCondition())
            {
                SaveSettingsColumn();
                //currentSetting = new ParserSettings();
                //currentSetting.idSupplier =  int.Parse(idSupplier.SelectedValue.ToString());
                currentSetting.nParserSettings = str;
                //currentSetting.parserSettingsColumns = GetColumnConditions();
                currentSetting.StartRow = (int)StartRow.Value;

                if (ckSetBrand.Checked)
                {
                    currentSetting.fBrend = 1;
                    currentSetting.nBrend = fBrend.Text;
                }

                currentSetting.IdCur = int.Parse(idCur.SelectedValue.ToString());
                if (rbDiscount.Checked) currentSetting.PricePercent = -((int)tbDiscount.Value);
                if (rbMarkup.Checked) currentSetting.PricePercent = (int)tbMarkup.Value;

                currentSetting.fNds = fNds.Checked ? 1 : 0;
                currentSetting.fMarketDiscount = fMarketDiscount.Checked ? 1 : 0;
                ParserSettingsHelper.SaveCurrentSetting(currentSetting, 1);

                var settings = DBParserSettings.GetParserSettingByidSupplier(currentSetting.idSupplier);
                /*cbSetting.DataSource = settings;
                cbSetting.DisplayMember = "nParserSettings";
                cbSetting.ValueMember = "idParserSettings";*/
                cbSetting.DataSource = null;
                cbSetting.Items.Clear();
                List<PSettings> pl = new List<PSettings>();

                foreach (DataRow r in settings.AsEnumerable())
                {
                    pl.Add(new PSettings(r["nParserSettings"].ToString(),
                        int.Parse(r["idParserSettings"].ToString()),
                        int.Parse(r["fPriceWithDateActual"].ToString())));
                }

                cbSetting.DataSource = pl;
                cbSetting.DisplayMember = "m_Name";
                cbSetting.ValueMember = "m_Value";
                cbSetting.Text = str;

            }

        }
        private void SaveSettingsColumn()
        {
            currentSetting.SaveSettingsColumn(GetColumnConditions(), prevSheetN);
        }


        private void btnFindFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.xls; *.xlsx; *.csv; *.txt | *.xls; *.xlsx; *.csv; *.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {


                //ResetForm();

                tbFilePath.Text = openFileDialog.FileName;
                sheets = FileReader.Read(openFileDialog.FileName);

                FillSheetNumbers(sheets.Count);
                TrimSettingsColumns(sheets.Count);
                gridFile.DataSource = sheets[0];
                //LoadSettings();

                //для Андрея тестов
                //currentSetting = new ParserSettings(Path.GetFileNameWithoutExtension(openFileDialog.FileName));
                /////////////////////////


                if (gridFile.DataSource != null)
                {
                    for (int i = 0; i < gridFile.Columns.Count; i++)
                    {
                        gridFile.Columns[i].HeaderText = ExcelColumnIndexToName(i);
                    }

                }
                btnClear.Visible = true;
                ActivateSaveBtn();
            }
        }

        private void FillSheetNumbers(int sheetNumber)
        {
            if (sheetNumber > 0)
            {
                sheetN.SelectedIndexChanged -= sheetN_SelectedIndexChanged;

                sheetN.Items.Clear();
                for (int i = 0; i < sheetNumber; i++)
                {
                    sheetN.Items.Add(new SheetNumber { No = i, Text = "Лист " + (i + 1) });
                }
                sheetN.DisplayMember = "Text";
                sheetN.ValueMember = "No";
                sheetN.SelectedIndex = 0;
                sheetN.SelectedIndexChanged += sheetN_SelectedIndexChanged;
            }
        }

        private void TrimSettingsColumns(int sheetSize)
        {
            if (currentSetting != null)
                currentSetting.parserSettingsColumns = (from i in currentSetting.parserSettingsColumns where i.sheetN < sheetSize select i).ToList();
        }




        private void sheetN_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ValidateColumnCondition())
            {
                SaveSettingsColumn();
                ResetSettingsColumn();
                prevSheetN = sheetN.SelectedIndex;
                LoadSettingsColumn();

                if (gridFile.DataSource != null)
                {
                    gridFile.DataSource = sheets[prevSheetN];
                }

            }
            else
            {
                sheetN.SelectedIndexChanged -= sheetN_SelectedIndexChanged;
                sheetN.SelectedIndex = prevSheetN;

                //if (gridFile.DataSource != null)
                //{
                //    gridFile.DataSource = sheets[prevSheetN];
                //}

                sheetN.SelectedIndexChanged += sheetN_SelectedIndexChanged;

            }

            if (gridFile.DataSource != null)
            {
                for (int i = 0; i < gridFile.Columns.Count; i++)
                {
                    gridFile.Columns[i].HeaderText = ExcelColumnIndexToName(i);
                }

            }

        }


        private bool ValidateColumnCondition()
        {


            //if (GetColumnConditions().Count > 0)
            //{
            //    return true;
            //}

            List<ParserSettingsColumn> columnConditions = GetColumnConditions();

            if (columnConditions.Count > 0)
            {

                ResetColoredCondition();

                string strcondition = "";

                if (columnConditions.Count == 0 && currentSetting.parserSettingsColumns.Count > 0) return true;

                if (ckSetBrand.Checked) columnConditions.Add(new ParserSettingsColumn(2, -20, sheetN.SelectedIndex));

                if (!ParserSettingsHelper.ValidateColumnCondition(columnConditions, "fFuture", ref strcondition))
                {
                    MessageBox.Show("Укажите все поля для Прайса со сроком действия\n" + strcondition, "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ColoredCondition(strcondition);
                    return false;
                }

                return true;
            }
            var res = currentSetting;

            return true;
        }


        private void ResetColoredCondition()
        {

            foreach (Control item in gpParseSetting.Controls)
            {
                if (item is Label)
                {
                    Label label = item as Label;

                    label.ForeColor = SystemColors.ControlText;

                }
            }
        }

        private void ColoredCondition(string strcondition)
        {
            string[] cond = strcondition.Split(',');

            foreach (string str in cond)
            {

                foreach (Control item in gpParseSetting.Controls)
                {
                    if (item is Label)
                    {
                        Label label = item as Label;
                        if (label.Text == str.Replace("\n", ""))
                        {
                            label.ForeColor = Color.Red;

                        }
                    }
                }

            }
        }




        private List<ParserSettingsColumn> GetColumnConditions()
        {
            List<ParserSettingsColumn> condition = new List<ParserSettingsColumn>();

            foreach (Control item in gpParseSetting.Controls)
            {
                if (item is TextBox)
                {
                    TextBox textBox = item as TextBox;
                    if (textBox.Text != "")
                    {
                        condition.Add(new ParserSettingsColumn(int.Parse(textBox.Tag.ToString()), ExcelColumnNameToIndex(textBox.Text), prevSheetN));

                    }
                }
            }

            return condition;
        }



        private void ResetSettingsColumn()
        {

            nartikulColSel.Text = "";
            nbrandColSel.Text = "";
            npriceColSel.Text = "";
            nQtyColSel.Text = "";
            nnameColSel.Text = "";
            nadvColSel.Text = "";
            nminPriceColSel.Text = "";
        }



        private void CellSelect_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
            {
                StartRow.Value = gridFile.CurrentCell.RowIndex + 1;
                currentSetting.StartRow = (int)StartRow.Value;
            }
        }

        private void artikulColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nartikulColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);
        }

        private void brandColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nbrandColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);
        }

        private void priceColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                npriceColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);
        }

        private void QtyColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nQtyColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);
        }

        private void nameColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nnameColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);
        }

        private void advColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nadvColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);
        }

        private void minPriceColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nminPriceColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);
        }

        private void ckSetBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSetBrand.Checked)
            {
                nbrandColSel.Text = "";
                ParserSettingsHelper.fillBrand(fBrend);
                fBrend.Enabled = true;
                nbrandColSel.Enabled = false;
                brandColSel.Enabled = false;

            }
            else
            {
                nbrandColSel.Text = "";
                fBrend.DataSource = null;
                fBrend.Enabled = false;
                nbrandColSel.Enabled = true;
                brandColSel.Enabled = true;
            }
        }

        private void idSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

            var elem = idSupplier.SelectedValue;

            int idKontr;

            if (elem != null && int.TryParse(elem.ToString(), out idKontr))
            {
                int pos = 0;
                //pos = cbSetting.SelectedValue == null ? 0 : int.Parse(cbSetting.SelectedValue.ToString());
                var settings = DBParserSettings.GetParserSettingByidSupplier(idKontr);
                /*cbSetting.DataSource = settings;
                cbSetting.DisplayMember = "nParserSettings";
                cbSetting.ValueMember = "idParserSettings";
                */
                cbSetting.DataSource = null;
                cbSetting.Items.Clear();
                List<PSettings> pl = new List<PSettings>();
                
                foreach (DataRow r in settings.AsEnumerable())
                {
                    pl.Add(new PSettings(r["nParserSettings"].ToString(),
                        int.Parse(r["idParserSettings"].ToString()),
                        int.Parse(r["fPriceWithDateActual"].ToString())));
                }

                cbSetting.DataSource = pl;
                cbSetting.DisplayMember = "m_Name";
                cbSetting.ValueMember = "m_Value";

                currentSetting = new ParserSettings();
                currentSetting.StartRow = 1;
                currentSetting.idSupplier = idKontr;
                btnSettingSave.Enabled = true;
                cbSetting.SelectedValue = pos;
            }

            if (idSupplier.SelectedIndex == 0)
            {
                btnSettingSave.Enabled = false;
            }

            if (idSupplier.SelectedIndex > 0)
            {
                _idSupplier = int.Parse(idSupplier.SelectedValue.ToString());
            }
            else
            {
                _idSupplier = 0;
            }
        }

        private void cbSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSetting.parserSettingsColumns.Clear();
            currentSetting.idParserSettings = 0;
            currentSetting.nBrend = "";
            currentSetting.fBrend = 0;
            currentSetting.fNds = 0;
            currentSetting.fMarketDiscount = 0;
            currentSetting.PricePercent = 0;
            ResetForm();

            if (cbSetting.SelectedIndex > 0)
            {
                idParserSettings = int.Parse(cbSetting.SelectedValue.ToString());
                var _currentSetting = ParserSettingsHelper.GetCurrentSettingById(idParserSettings);
                if (_currentSetting != null) currentSetting = _currentSetting;
                btnSettingSave.Enabled = false;
                LoadSettings();
            }
            else
            {


                //currentSetting.StartRow = 1;

                btnSettingSave.Enabled = true;

            }

            ActivateSaveBtn();
        }


        private void ResetForm()
        {

            //gridFile.DataSource = null;
            //tbFilePath.Text = "";
            btnClear.Visible = false;
            nartikulColSel.Text = "";
            nbrandColSel.Text = "";
            npriceColSel.Text = "";
            nQtyColSel.Text = "";
            nnameColSel.Text = "";
            nadvColSel.Text = "";
            nminPriceColSel.Text = "";
            ckSetBrand.Checked = false;
            StartRow.Value = 1;
            fNds.Checked = false;
            fMarketDiscount.Checked = false;
            rbDiscount.Checked = false;
            rbMarkup.Checked = false;
            tbDiscount.Enabled = false;
            tbMarkup.Enabled = false;
            tbDiscount.Value = 0;
            tbMarkup.Value = 0;
        }


        private void LoadSettings()
        {
            idSupplier.SelectedValue = currentSetting.idSupplier;

            StartRow.Value = currentSetting.StartRow;

            int sheetCount = currentSetting.parserSettingsColumns.AsEnumerable().Select(c => c.sheetN).Distinct().Count();
            FillSheetNumbers(sheetCount);

            LoadSettingsColumn();

            if (currentSetting.fBrend == 1)
            {
                ckSetBrand.Checked = true;
                fBrend.Text = currentSetting.nBrend;

            }
            else
            {
                ckSetBrand.Checked = false;
                LoadSettingsColumn();
            }

            fNds.Checked = currentSetting.fNds == 1 ? true : false;
            fMarketDiscount.Checked = currentSetting.fMarketDiscount == 1 ? true : false;

            if (currentSetting.PricePercent > 0)
            {
                rbMarkup.Checked = true;
                tbMarkup.Value = currentSetting.PricePercent;
            }
            else if (currentSetting.PricePercent < 0)
            {
                rbDiscount.Checked = true;
                tbDiscount.Value = Math.Abs(currentSetting.PricePercent);
            }

            idCur.SelectedValue = currentSetting.IdCur;


        }


        private void LoadSettingsColumn()
        {


            foreach (Control item in gpParseSetting.Controls)
            {
                if (item is TextBox)
                {
                    TextBox textBox = item as TextBox;
                    textBox.Text = "";

                }
            }

            foreach (Control item in gpParseSetting.Controls)
            {
                if (item is TextBox)
                {
                    TextBox textBox = item as TextBox;


                    var res = from i in currentSetting.parserSettingsColumns where i.sheetN == prevSheetN select i;

                    foreach (var column in res)
                    {
                        if (item.Tag.ToString() == column.idTypeParserSettingsColumn.ToString())
                        {
                            item.Text = ExcelColumnIndexToName(column.ColumnNumber);
                        }
                    }
                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gridFile.DataSource = null;
            tbFilePath.Text = "";
            sheetN.Items.Clear();
            btnClear.Visible = false;
            ActivateSaveBtn();
        }




        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            PriceFutureSettinName name = new PriceFutureSettinName(SaveSettings);
            name.ShowDialog();
        }


        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            _datefrom = datefrom.Value.Date;

        }



        private static string ExcelColumnIndexToName(int Index)
        {
            string range = "";
            if (Index < 0) return range;
            for (int i = 1; Index + i > 0; i = 0)
            {
                range = ((char)(65 + Index % 26)).ToString() + range;
                Index /= 26;
            }
            if (range.Length > 1) range = ((char)((int)range[0] - 1)).ToString() + range.Substring(1);
            return range;
        }
        public static int ExcelColumnNameToIndex(string colAdress)
        {
            int[] digits = new int[colAdress.Length];
            for (int i = 0; i < colAdress.Length; ++i)
            {
                digits[i] = Convert.ToInt32(colAdress[i]) - 64;
            }
            int mul = 1; int res = -1;
            for (int pos = digits.Length - 1; pos >= 0; --pos)
            {
                res += digits[pos] * mul;
                mul *= 26;
            }
            return res;
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ext = saveFileDialog1.FileName.Split('.').Last().ToUpper();
                if (ext == "XLS")
                {
                    analizGrid.ExportToXls(saveFileDialog1.FileName);
                }

                if (ext == "XLSX")
                {
                    analizGrid.ExportToXlsx(saveFileDialog1.FileName);
                }

                if (ext == "PDF")
                {
                    analizGrid.ExportToPdf(saveFileDialog1.FileName);
                }

                if (ext == "TXT")
                {
                    analizGrid.ExportToText(saveFileDialog1.FileName);
                }
            }
        }

        private void rbDiscount_CheckedChanged(object sender, EventArgs e)
        {
            tbDiscount.Enabled = true;
            tbMarkup.Enabled = false;
        }

        private void rbMarkup_CheckedChanged(object sender, EventArgs e)
        {
            tbMarkup.Enabled = true;
            tbDiscount.Enabled = false;
        }

        private void btnTestSendEmail_Click(object sender, EventArgs e)
        {
            //_datefrom = datefrom.Value.Date;
            //loadType = int.Parse(cbLoadType.SelectedIndex.ToString());
            //var idPf = parserFuture.CheckFutureheader(currentSetting.idSupplier, _datefrom, loadType);

            //parserFuture.SendRatioEmail(_idSupplier);
            var dtconf = parserFuture.CheckNotConfirmed(136);

            parserFuture.SendEmail(136, 556119, ref dtconf);

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gridNotConfirmedDetail.DataSource = null;
            gridNotConfirmed.DataSource = null;

            NotConfirmedPricesDetail();
            gridNotConfirmedDetail.DataSource = notconfirmdetail;
            List<int> l = new List<int>();

            foreach (DataRow r in notconfirmdetail.Rows)
            {
                if (!l.Exists(x => x == Convert.ToInt32(r["idpf"])))
                {
                    l.Add(Convert.ToInt32(r["idpf"]));
                }
            }

            if (l.Count > 0)
            {
                NotConfirmedPrices(l);
                gridNotConfirmed.DataSource = notconfirm;
            }
        }


        private void gridViewNotConfirmed_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = gridViewNotConfirmed.GetFocusedDataRow();
            if (row != null)
            {
                //btnConfirm.Enabled = (row != null);
                label5.Text = row["idpf"].ToString();
                var idpf = Convert.ToInt32(row["idpf"]);

                var results = from DataRow r in notconfirmdetail.Rows where (int)r["idpf"] == idpf select r;
                
                DataTable dt = new DataTable();
                dt = notconfirmdetail.Clone();
                foreach (DataRow dr in results.AsEnumerable())
                {
                    dt.ImportRow(dr);
                }
                gridNotConfirmedDetail.DataSource = dt;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var row = gridViewNotConfirmed.GetFocusedDataRow();
            if (row != null)
            {
                if (DialogResult.Yes != MessageBox.Show("Подтвердить прайс-лист ?", "Внимание!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }
                parserFuture.SetUnsetConfirm(Convert.ToInt32(row["idpf"]), 2, User.CurrentUserId);
                MessageBox.Show("Прайс лист подтвержден", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRefresh_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Не выбран или отсутствует прайс лист для подтверждения", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            //private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
            //{
            //    var row = gridView1.GetFocusedDataRow();
            //    if (row != null)
            //    {
            //        var data = parserFuture.PriceFutereAnalisesDetail(int.Parse(row["idtov"].ToString()), DateTime.Parse(row["datefrom"].ToString()));
            //        gridAnaliseDetail.DataSource = data;
            //    }
            //}
        }

        private void btnSaveExcel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ext = saveFileDialog1.FileName.Split('.').Last().ToUpper();
                if (ext == "XLS")
                {
                    gridNotConfirmedDetail.ExportToXls(saveFileDialog1.FileName);
                }

                if (ext == "XLSX")
                {
                    gridNotConfirmedDetail.ExportToXlsx(saveFileDialog1.FileName);
                }

                if (ext == "PDF")
                {
                    gridNotConfirmedDetail.ExportToPdf(saveFileDialog1.FileName);
                }

                if (ext == "TXT")
                {
                    gridNotConfirmedDetail.ExportToText(saveFileDialog1.FileName);
                }
            }
        }

        private void btnDeleteSetting_Click(object sender, EventArgs e)
        {
            if (cbSetting.SelectedIndex == -1 || cbSetting.SelectedIndex == 0) { return; }

            DialogResult dr = MessageBox.Show("Вы хотите удалить выбранную настройку?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No) { return; }
            try
            {
                var idParserSettings = int.Parse(cbSetting.SelectedValue.ToString());
                SqlParameter paridParserSettings = new SqlParameter("idParserSettings", idParserSettings);
                var result = ALogic.DBConnector.DBExecutor.SelectTable("delete from ParserSettings where idParserSettings = @idParserSettings", paridParserSettings);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка удаления настройки парсинга:  " + ex.Message);
            }
            finally
            {
                idSupplier_SelectedIndexChanged(sender, new EventArgs());
            }
        }

        private void cbLoadType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSettingSave_MouseEnter(object sender, EventArgs e)
        {
            hint.SetToolTip(btnSettingSave, "Сохранить настройку");
        }

        private void btnDeleteSetting_MouseEnter(object sender, EventArgs e)
        {
            hint.SetToolTip(btnDeleteSetting, "Удалить настройку");
        }

        private void cbSetting_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                Graphics g = e.Graphics;

                Color SelectedBackColor;

                var vtag = ((PSettings)cbSetting.Items[e.Index]).m_Tag;
                var vname = ((PSettings)cbSetting.Items[e.Index]).m_Name;

                switch (vtag) 
                {
                    case 0: SelectedBackColor = e.BackColor; break;
                    case 1: SelectedBackColor = Color.LightGreen; break;
                    default: SelectedBackColor = e.BackColor; break;
                }

                Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? new SolidBrush(e.BackColor) : new SolidBrush(SelectedBackColor);
                Brush tBrush = new SolidBrush(Color.Black);

                g.FillRectangle(brush, e.Bounds);
                e.Graphics.DrawString(vname, e.Font, tBrush, e.Bounds, StringFormat.GenericDefault);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && vtag == 1)
                {
                   hint.Show("Настройка для парсинга прайсов со сроком действия", cbSetting, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    hint.Hide(cbSetting);
                }

                brush.Dispose();
                tBrush.Dispose();
            }
            e.DrawFocusRectangle();
        }
    }
}
