using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForm.Base;
using ALogic.Logic.Heplers;
using ALogic.Logic.Old.Parsers;
using ALogic.Logic.SPR;

namespace AForm.Forms.Old.Parsers
{
    public partial class Setting : DevExpress.XtraEditors.XtraForm
    {
        ParserSettings currentSetting;
        //List<ParserSettingsColumn> columnConditions = new List<ParserSettingsColumn>();
        DataTableCollection sheets;
        public int prevSheetN { get; set; }
        public int idParserSettings { get; set; }

        // ФОРМА
        //###########################################################################################################################

        /// <summary>
        /// Конструктор фомы
        /// </summary>
        /// <param name="idParserSettings">Ключ настроек</param>
        public Setting(int idParserSettings = 0, int idsupplier = 0)
        {
            InitializeComponent();
            this.Tag = "Настройка парсинга ИД=" + idParserSettings.ToString();
            this.idParserSettings = idParserSettings;
            prevSheetN = 0;
            ParserSettingsHelper.fillSupplier(idSupplier);
            ParserSettingsHelper.fillCurrency(idCur);
                      
            //   ParserSettingsHelper.FillConditions(columnConditions);

            if (idParserSettings > 0)
            {
                currentSetting = ParserSettingsHelper.GetCurrentSettingById(idParserSettings);
                LoadSettings();
            }
            else
            {
                currentSetting = new ParserSettings();
                if (idsupplier > 0)
                    idSupplier.SelectedValue = idsupplier;
            }
        }

        /// <summary>
        /// Очистка формы
        /// </summary>
        private void ResetForm()
        {

            gridFile.DataSource = null;
            tbFilePath.Text = "";
            btnClear.Visible = false;
            nartikulColSel.Text = "";
            nbrandColSel.Text = "";
            npriceColSel.Text = "";
            nQtyColSel.Text = "";
            nnameColSel.Text = "";
            nadvColSel.Text = "";
            nminPriceColSel.Text = "";
            nParserSettings.Text = "";
            rbDiscount.Checked = false;
            rbMarkup.Checked = false;
            ckSetBrand.Checked = false;
            fNds.Checked = false;
            fMarketDiscount.Checked = false;
            idSupplier.SelectedIndex = 0;
            fPriceCompetitor.Checked = false;
            fPriceOnline.Checked = false;
            fArmRtk.Checked = false;
            ckAutoProc.Checked = false;
            MailFileMask.Text = "";
            Mail.Text = "";
            FtpLogin.Text = "";
            FtpPass.Text = "";
            FtpServer.Text = "";
            fHardLoad.Checked = false;
            fAllList.Checked = false;
            Separator.Text = "";
            StartRow.Value = 1;
            tbDiscount.Value = 0;
            tbDiscount.Enabled = false;
            tbMarkup.Value = 0;
            tbMarkup.Enabled = false;
            idCur.SelectedIndex = 0;
            fDelOldPrice0.Checked = false;
            fDelOldPrice1.Checked = false;
            KolDayActual.Value = 0;
            fOvveridePerPrice.Checked = false;
            nabcSupplierColSel.Text = "";
            cbPriceA1.Checked = false;
        }

        /// <summary>
        /// Очищаем форму по нажатию кнопки
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        // ЗАГРУЗКА ФАЙЛА
        //###########################################################################################################################
        /// <summary>
        /// Загруззка фала
        /// </summary>
        private void LoadFile(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.xls; *.xlsx; *.csv; *.txt | *.xls; *.xlsx; *.csv; *.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {


                //ResetForm();

                tbFilePath.Text = openFileDialog.FileName;
                sheets = FileReader.Read(openFileDialog.FileName, Separator.Text);

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
                //LoadComboboxheader(sender, e);





            }
        }

        private void TrimSettingsColumns(int sheetSize)
        {

            currentSetting.parserSettingsColumns = (from i in currentSetting.parserSettingsColumns where i.sheetN < sheetSize select i).ToList();
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

        // ЗАГРУЗКА НАСТРОЕК
        //###########################################################################################################################
        /// <summary>
        /// Загрузка настроек
        /// </summary>
        private void LoadSettings()
        {
            nParserSettings.Text = currentSetting.nParserSettings;
            idSupplier.SelectedValue = currentSetting.idSupplier;

            //ins 31.01.22 Semenkina Заполнение Складов поставщика
            ParserSettingsHelper.fillSkladSupplier(idSkladSupplier, int.Parse(idSupplier.SelectedValue.ToString())); 
            idSkladSupplier.SelectedValue = currentSetting.idSkladSupplier;  //ins 31.01.22

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



            fNds.Checked = currentSetting.fNds == 1 ? true : false;
            fMarketDiscount.Checked = currentSetting.fMarketDiscount == 1 ? true : false;
            fPriceCompetitor.Checked = currentSetting.fPriceCompetitor == 1 ? true : false;
            
            fCorrectCoeff.Checked = currentSetting.fCorrectCoeff == 1;

            if (currentSetting.fPriceOnline == 1)
            {
                fPriceOnline.Checked = true;
                LoadStockSetting(currentSetting.idSupplier, currentSetting.idSkladSupplier);
            }


            if (currentSetting.fAutoload == 1)
            {
                ckAutoProc.Checked = true;
                if (currentSetting.idTypeSource == 1)
                {
                    rbEmail.Checked = true;
                    Mail.Text = currentSetting.Mail;
                    MailFileMask.Text = currentSetting.MailFileMask;
                }
                else if (currentSetting.idTypeSource == 2)
                {
                    rbFtp.Checked = true;
                    FtpLogin.Text = currentSetting.FtpLogin;
                    FtpPass.Text = currentSetting.FtpPass;
                    FtpServer.Text = currentSetting.FtpServer;
                }
            }

            StartRow.Value = currentSetting.StartRow;
            fAllList.Checked = currentSetting.fAllList == 1 ? true : false;
            fHardLoad.Checked = currentSetting.fHardLoad == 1 ? true : false;
            Separator.Text = currentSetting.Separator;
            idCur.SelectedValue = currentSetting.IdCur;

            if (currentSetting.fArmRtk == 1)
            {
                fArmRtk.Checked = true;
                KolDayActual.Value = currentSetting.KolDayActual;

                if (currentSetting.fDelOldPrice == 0) fDelOldPrice0.Checked = true;
                else if (currentSetting.fDelOldPrice == 1) fDelOldPrice1.Checked = true;

            }
            else
                fArmRtk.Checked = false;

            fOvveridePerPrice.Checked = currentSetting.fOvveridePerPrice == 1 ? true : false;
            cbPriceA1.Checked = currentSetting.fCompetitorType == 2 ? true : false;



            int sheetCount = currentSetting.parserSettingsColumns.AsEnumerable().Select(c => c.sheetN).Distinct().Count();
            FillSheetNumbers(sheetCount);

            LoadSettingsColumn();

            if (currentSetting.fBrend == 1)
            {
                ckSetBrand.Checked = true;
                fBrend.Text = currentSetting.nBrend;

            }




        }


        private void LoadSettingsColumn()
        {
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

        // GRID НАСТРОЙКИ
        //###########################################################################################################################
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
        private void CellSelect_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
            {
                StartRow.Value = gridFile.CurrentCell.RowIndex + 1;
                currentSetting.StartRow = (int)StartRow.Value;
            }
        }


        // КНОПКИ УППРАВЛЕНИЯ
        //###########################################################################################################################
        private void btnSave_Click(object sender, EventArgs e)
        {
            currentSetting.nParserSettings = nParserSettings.Text;
            currentSetting.idSupplier = int.Parse(idSupplier.SelectedValue.ToString());
            currentSetting.nBrend = fBrend.SelectedIndex > 0 ? fBrend.Text : "";

            if (rbDiscount.Checked) currentSetting.PricePercent = -((int)tbDiscount.Value);

            currentSetting.idSkladSupplier = fPriceOnline.Checked ? int.Parse(idSkladSupplier.SelectedValue.ToString()) : 0;

            
            if (rbMarkup.Checked) currentSetting.PricePercent = (int)tbMarkup.Value;

            if (ckSetBrand.Checked)
            {
                if (fBrend.SelectedIndex == 0)
                {
                    MessageBox.Show("Укажите бренд ", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fBrend.DroppedDown = true;
                    return;
                }
                currentSetting.fBrend = 1;
                currentSetting.nBrend = fBrend.Text;

            }
            else
            {
                currentSetting.fBrend = 0;
                currentSetting.nBrend = null;

            }
            currentSetting.fNds = fNds.Checked ? 1 : 0;
            currentSetting.fMarketDiscount = fMarketDiscount.Checked ? 1 : 0;
            currentSetting.fPriceCompetitor = fPriceCompetitor.Checked ? 1 : 0;
            currentSetting.fArmRtk = fArmRtk.Checked ? 1 : 0;
            currentSetting.fPriceOnline = fPriceOnline.Checked ? 1 : 0;
            currentSetting.fCorrectCoeff = fCorrectCoeff.Checked ? 1 : 0;
            currentSetting.IdCur = int.Parse(idCur.SelectedValue.ToString());
            currentSetting.fCompetitorType = cbPriceA1.Checked ? 2 : 1;


            currentSetting.StartRow = (int)StartRow.Value;
            currentSetting.fAllList = fAllList.Checked ? 1 : 0;

            if (ckAutoProc.Checked)
            {
                currentSetting.fAutoload = 1;
                if (rbEmail.Checked)
                {
                    currentSetting.idTypeSource = 1;
                    currentSetting.MailFileMask = MailFileMask.Text;
                    currentSetting.Mail = Mail.Text;


                    currentSetting.FtpServer = null;
                    currentSetting.FtpLogin = null;
                    currentSetting.FtpPass = null;

                }
                else if (rbFtp.Checked)
                {
                    currentSetting.idTypeSource = 2;
                    currentSetting.FtpLogin = FtpLogin.Text;
                    currentSetting.FtpPass = FtpPass.Text;
                    currentSetting.FtpServer = FtpServer.Text;

                    currentSetting.Mail = null;
                    currentSetting.MailFileMask = null;
                }
            }
            else
            {
                currentSetting.fAutoload = 0;
            }



            if (currentSetting.nParserSettings == "")
            {
                MessageBox.Show("Укажите наименование настроек", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nParserSettings.Focus();
                return;

            }

            gbSupplier.BackColor = SystemColors.Control;

            if (currentSetting.idSupplier == 0)
            {
                MessageBox.Show("Укажите поставщика", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gbSupplier.BackColor = Color.LightPink;
                idSupplier.DroppedDown = true;
                return;

            }

            if (currentSetting.idSupplier > 0 && currentSetting.idSkladSupplier == 0 && fPriceOnline.Checked)
            {
                MessageBox.Show("Укажите Склад поставщика", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gbSupplier.BackColor = Color.LightPink;
                idSkladSupplier.DroppedDown = true;
                return;

            }

            if (ckSetBrand.Checked && fBrend.SelectedIndex == 0)
            {
                MessageBox.Show("Укажите бренд", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fBrend.DroppedDown = true;
                return;

            }

            if (ckAutoProc.Checked)
            {
                gbEmail.BackColor = SystemColors.Control;
                gbFtp.BackColor = SystemColors.Control;
                if (rbEmail.Checked)
                {
                    if (Mail.Text.Length == 0)
                    {
                        gbEmail.BackColor = Color.LightPink;
                        MessageBox.Show("Укажите настройки email", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (rbFtp.Checked)
                {
                    if (FtpServer.Text.Length == 0 || FtpPass.Text.Length == 0 || FtpLogin.Text.Length == 0)
                    {
                        gbFtp.BackColor = Color.LightPink;
                        MessageBox.Show("Укажите настройки FTP", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Укажите настройки автообработки", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
            }


            if (fDelOldPrice0.Checked) currentSetting.fDelOldPrice = 0;
            if (fDelOldPrice1.Checked) currentSetting.fDelOldPrice = 1;
            currentSetting.KolDayActual = (int)KolDayActual.Value;

            if (ValidateColumnCondition()) SaveSettingsColumn();
            else return;

            currentSetting.Separator = Separator.Text;
            currentSetting.fHardLoad = fHardLoad.Checked ? 1 : 0;
            currentSetting.fOvveridePerPrice = fOvveridePerPrice.Checked?1:0;
            
            /*if(fPriceOnline.Checked && idSupplier.SelectedIndex>0)
            {
                currentSetting.idSkladSupplier = int.Parse(cbStockList.SelectedValue.ToString());
            }*/


            ParserSettingsHelper.SaveCurrentSetting(currentSetting, 0);

            if (AEvents.PriceParserEvents.SaveParserSetting != null)
                AEvents.PriceParserEvents.SaveParserSetting(this, null);

            Close();
        }       

        private bool ValidateColumnCondition()
        {
            List<ParserSettingsColumn> columnConditions = GetColumnConditions();


            if (fPriceOnline.Checked || fPriceCompetitor.Checked || fArmRtk.Checked || fCorrectCoeff.Checked)
            {
                ResetColoredCondition();
                gbTable.BackColor = SystemColors.Control;

                string strcondition = "";

                if (columnConditions.Count == 0 && currentSetting.parserSettingsColumns.Count > 0) return true;

                if (ckSetBrand.Checked) columnConditions.Add(new ParserSettingsColumn(2, -20, sheetN.SelectedIndex));

                if (fPriceOnline.Checked && !ParserSettingsHelper.ValidateColumnCondition(columnConditions, "fPriceOnline", ref strcondition))
                {
                    MessageBox.Show("Укажите все поля для Заказной товар\n" + strcondition, "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ColoredCondition(strcondition);
                    return false;
                }

                if (fPriceCompetitor.Checked && !ParserSettingsHelper.ValidateColumnCondition(columnConditions, "fPriceCompetitor", ref strcondition))
                {
                    MessageBox.Show("Укажите все поля для Ценообразования\n" + strcondition, "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ColoredCondition(strcondition);

                    return false;

                }


                if (fArmRtk.Checked && !ParserSettingsHelper.ValidateColumnCondition(columnConditions, "fArmRtk", ref strcondition))
                {
                    MessageBox.Show("Укажите все поля для АРМРТК\n" + strcondition, "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ColoredCondition(strcondition);

                    return false;

                }


                if (fCorrectCoeff.Checked && !ParserSettingsHelper.ValidateColumnCondition(columnConditions, "fCorrectCoeff", ref strcondition))
                {
                    MessageBox.Show("Укажите все поля для Корректировки коэффициентов \n" + strcondition, "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ColoredCondition(strcondition);

                    return false;

                }


            }
            else
            {
                gbTable.BackColor = Color.LightPink;
                MessageBox.Show("Укажите предназначение файла", "Ошибка, не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;


            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {          
            Close();
        }


        // СОБЫТИЯ ФОРМЫ
        //###########################################################################################################################
        private void rbEmail_CheckedChanged(object sender, EventArgs e)
        {
            gbEmail.Visible = true;
            gbFtp.Visible = false;
        }

        private void rbFtp_CheckedChanged(object sender, EventArgs e)
        {

            gbFtp.Visible = true;
            gbEmail.Visible = false;


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
        private void ckAutoProc_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAutoProc.Checked)
            {
                gbAutoobrabotka.Enabled = true;

            }
            else
            {
                //TODO не забудь очищать поля 
                gbAutoobrabotka.Enabled = false;


                gbEmail.BackColor = SystemColors.Control;
                gbFtp.BackColor = SystemColors.Control;
                rbEmail.Checked = false;
                rbFtp.Checked = false;
                gbFtp.Visible = false;
                gbEmail.Visible = false;
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

        private void abcSupplierColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nabcSupplierColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);

        }


        // ПРОВЕРКИ
        //###########################################################################################################################
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

        private void Setting_Load(object sender, EventArgs e)
        {

        }

        private void nParserSettings_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void fMarketDiscount_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fHardLoad_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gbAutoobrabotka_Enter(object sender, EventArgs e)
        {

        }

        private void fCorrectCoeff_CheckedChanged(object sender, EventArgs e)
        {
            if (fCorrectCoeff.Checked)
            {
                fPriceCompetitor.CheckedChanged -= fPriceCompetitor_CheckedChanged;
                fPriceOnline.CheckedChanged -= fPriceOnline_CheckedChanged;
                fArmRtk.CheckedChanged -= fArmRtk_CheckedChanged;
                fPriceCompetitor.Checked = false;
                fPriceOnline.Checked = false;
                fArmRtk.Checked = false;
                fPriceCompetitor.CheckedChanged += fPriceCompetitor_CheckedChanged;
                fPriceOnline.CheckedChanged += fPriceOnline_CheckedChanged;
                fArmRtk.CheckedChanged += fArmRtk_CheckedChanged;
            }
        }

        private void fPriceCompetitor_CheckedChanged(object sender, EventArgs e)
        {
            if (fPriceCompetitor.Checked)
                fCorrectCoeff.Checked = false;
            cbPriceA1.Enabled = fPriceCompetitor.Checked;
            cbPriceA1.Checked = false;
        }

        private void fPriceOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (fCorrectCoeff.Checked)
            {
                fCorrectCoeff.Checked = false;

            }

            if (fPriceOnline.Checked && idSupplier.SelectedIndex > 0)
            {
                btnStockAdd.Enabled = true;
                    LoadStockSetting(int.Parse(idSupplier.SelectedValue.ToString()));
                    gbStock.Enabled = true;

                    //ins 28.01.22 Semenkina Склад поставщика
                    ParserSettingsHelper.fillSkladSupplier(idSkladSupplier, int.Parse(idSupplier.SelectedValue.ToString()));
                    idSkladSupplier.SelectedValue = currentSetting.idSkladSupplier;//ins 28.01.22 Semenkina Склад поставщика
            }
            else
            {
                btnStockAdd.Enabled = false;
                cbStockList.DataSource = null;
                gbStock.Enabled = false;

                idSkladSupplier.DataSource = null; // при снятии галки - зачистка списка Складов
                                                   //ins 28.01.22 Semenkina Склад поставщика
                 }
        }

        private void LoadStockSetting(int selectedValue,  int idSkladSuplier = 0)
        {
           /* var res  = sSkladSupplierData.getDataBySupplier(selectedValue);
            cbStockList.DataSource = res;
            cbStockList.ValueMember = "idSkladSupplier";
            cbStockList.DisplayMember = "nSkladSupplier";

            if (idSkladSuplier != 0)
            {
                cbStockList.SelectedValue = idSkladSuplier;
            }
            else cbStockList.SelectedValue = 0;*/
        }

        private void fArmRtk_CheckedChanged(object sender, EventArgs e)
        {
            gpRtkPrA.Enabled = fArmRtk.Checked;

            if (fCorrectCoeff.Checked)
            {
                fCorrectCoeff.Checked = false;

            }
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
                sheetN.SelectedIndexChanged += sheetN_SelectedIndexChanged;

            }



        }

        private void SaveSettingsColumn()
        {
            currentSetting.SaveSettingsColumn(GetColumnConditions(), prevSheetN);
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
            nabcSupplierColSel.Text = "";
        }



        private void fNds_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnStockAdd_Click(object sender, EventArgs e)
        {
            if (idSupplier.SelectedIndex > 0)
            {
                var stockSetting = new StockSetting(int.Parse(idSupplier.SelectedValue.ToString()), idSupplier.Text);
                stockSetting.ReloadEvent += LoadStockSetting;
                WindowOpener.OpenWindow(stockSetting);
            }
        }

        private void btnStockEdit_Click(object sender, EventArgs e)
        {
            if (cbStockList.SelectedIndex > 0)
            {
                var stockSetting = new StockSetting(int.Parse(cbStockList.SelectedValue.ToString()), int.Parse(idSupplier.SelectedValue.ToString()), idSupplier.Text);
                stockSetting.ReloadEvent += LoadStockSetting;
                WindowOpener.OpenWindow(stockSetting);
            }
        }

        private void btnStockDelete_Click(object sender, EventArgs e)
        {
            if(DialogResult.OK == MessageBox.Show("Вы действительно хоитите удалить настройки", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)){

                ALogic.Logic.SPR.sSkladSupplierData.Remove(int.Parse(cbStockList.SelectedValue.ToString()));
                LoadStockSetting(int.Parse(idSupplier.SelectedValue.ToString()));

            }
        }

        private void idSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fPriceOnline.Checked && idSupplier.SelectedIndex > 0)
            {
                gbStock.Enabled = true;
                btnStockAdd.Enabled = true;
                LoadStockSetting(int.Parse(idSupplier.SelectedValue.ToString()));

                //ins 28.01.22 Semenkina Склад поставщика
                ParserSettingsHelper.fillSkladSupplier(idSkladSupplier, int.Parse(idSupplier.SelectedValue.ToString())); 
                idSkladSupplier.SelectedValue = currentSetting.idSkladSupplier;//ins 28.01.22 Semenkina Склад поставщика

            }
            else {
                gbStock.Enabled = false;
                btnStockAdd.Enabled = false;
                cbStockList.DataSource = null; }
        }

        //ins 28.01.22 Semenkina Добавлен выбор Склада поставщика
        private void idSkladSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fPriceOnline.Checked && idSkladSupplier.SelectedIndex > 0)
            {
                gbStock.Enabled = true;
                btnStockAdd.Enabled = true;
               // LoadStockSetting(int.Parse(idSkladSupplier.SelectedValue.ToString()), int.Parse(idSupplier.SelectedValue.ToString()));
                   
            }
            else
            {
                gbStock.Enabled = false;
                btnStockAdd.Enabled = false;
                cbStockList.DataSource = null;
            }
        }

        private void cbStockList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStockList.SelectedIndex > 0)
            {
                btnStockEdit.Enabled = true;
                btnStockDelete.Enabled = true;
            }
            else
            {
                btnStockDelete.Enabled = false;
                btnStockEdit.Enabled = false;
            }
        }

        private void idCur_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void alternatLoad_Enter(object sender, EventArgs e)
        {

        }

        private void cbPriceA1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPriceA1.Checked)
            {
                fPriceOnline.Enabled = false;
                fCorrectCoeff.Enabled = false;
                fArmRtk.Enabled = false;

                fPriceOnline.Checked = false;
                fCorrectCoeff.Checked = false;
                fArmRtk.Checked = false;
            }
            else
            {
                fPriceOnline.Enabled = true;
                fCorrectCoeff.Enabled = true;
                fArmRtk.Enabled = true;
            }
        }

        //Mearge with origin



        //private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (Control item in gridFile.Controls)
        //    {
        //        if ((item is ComboBox))
        //        {
        //            ComboBox comboBox = item as ComboBox;
        //            comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
        //            if (comboBox.SelectedIndex == 0)
        //            {
        //                comboBox.DataSource = null;
        //                comboBox.DataSource = formattingList(sender);
        //                comboBox.DisplayMember = "name";
        //                comboBox.ValueMember = "id";
        //            }
        //            else if (comboBox.SelectedIndex > 0)
        //            {
        //                List<ColumnCondition> myLists = formattingList(sender);
        //                int _id = (int)comboBox.SelectedValue;
        //                string _name = comboBox.Text;
        //                myLists.Insert(1, new ColumnCondition { id = _id, name = _name });
        //                comboBox.DataSource = null;
        //                comboBox.DataSource = myLists;


        //                comboBox.DisplayMember = "name";
        //                comboBox.ValueMember = "id";
        //                comboBox.Text = _name;


        //            }
        //            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

        //        }
        //    }

        //}


        //private List<ColumnCondition> formattingList(object sender)
        //{
        //    List<ColumnCondition> templateList = new List<ColumnCondition>(columnConditions);

        //    foreach (Control item in gridFile.Controls)
        //    {

        //        if (item is ComboBox)
        //        {
        //            ComboBox comboBox = item as ComboBox;


        //            if (comboBox.SelectedIndex > 0)
        //            {
        //                templateList.Remove(new ColumnCondition { name = comboBox.Text });
        //            }

        //        }

        //    }

        //    return templateList;
        //}

        //private void LoadComboboxheader(object sender, EventArgs e)
        //{
        //    gridFile.Controls.Clear();

        //    for (int i = 0; i < gridFile.Columns.Count; i++)
        //    {
        //        ComboBox comboBox = new ComboBox()
        //        {
        //            Name = "Combobox" + i,
        //            DataSource = new List<ColumnCondition>(columnConditions),
        //            Location = gridFile.GetCellDisplayRectangle(i, -1, true).Location,
        //            Size = gridFile.Columns[i].HeaderCell.Size,
        //            DisplayMember = "name",
        //            ValueMember = "id",
        //            DropDownStyle = ComboBoxStyle.DropDownList
        //        };

        //        comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

        //        gridFile.Controls.Add(comboBox);

        //    }
        //}





    }


}

