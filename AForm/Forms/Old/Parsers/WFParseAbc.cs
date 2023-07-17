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
using ALogic.DBConnector;
using ALogic.Logic.Heplers;
using ALogic.Logic.Old.Parsers;
using ALogic.Logic.SPR;
using ALogic.Logic.Reload1C;

namespace AForm.Forms.Old.Parsers
{
    public partial class WFParseABC : DevExpress.XtraEditors.XtraForm
    {
        //ParserSettings currentSetting;
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
        public WFParseABC(int idParserSettings = 0, int idsupplier = 0)
        {
            InitializeComponent();
            this.Tag = "Парсинг ABC поставщика";
            prevSheetN = 0;
            ParserSettingsHelper.fillSupplier(idSupplier);
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
            nABC.Text = "";
            ckSetBrand.Checked = false;
            idSupplier.SelectedIndex = 0;
            fAllList.Checked = false;
            StartRow.Value = 1;
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
                tbFilePath.Text = openFileDialog.FileName;
                sheets = FileReader.Read(openFileDialog.FileName);

                gridFile.DataSource = sheets[0];

                if (gridFile.DataSource != null)
                {
                    for (int i = 0; i < gridFile.Columns.Count; i++)
                    {
                        gridFile.Columns[i].HeaderText = ExcelColumnIndexToName(i);
                    }
                }
                //btnClear.Visible = true;
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
            }
        }

        private bool ValidateColumnCondition()
        {
            //ТОДО дописать

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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

        private void QtyColSel_Click(object sender, EventArgs e)
        {
            if (gridFile.CurrentCell != null)
                nABC.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);

        }

        // ПРОВЕРКИ
        //###########################################################################################################################


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

        private void bLoad_Click(object sender, EventArgs e)
        {
            int errcnt = 0;
            int abccnt = 0;

            DataTable table =  new DataTable();
            
            try
            {
                Cursor = Cursors.WaitCursor;

                table = gridFile.DataSource as DataTable;
                
                int idKontr = int.Parse(idSupplier.SelectedValue.ToString());
                int idArt = ExcelColumnNameToIndex(nartikulColSel.Text);
                int idBrend = ckSetBrand.Checked ? 0 : ExcelColumnNameToIndex(nbrandColSel.Text);
                int idAbc = ExcelColumnNameToIndex(nABC.Text);

                string tBrend = ckSetBrand.Checked ? fBrend.Text.ToString() : "";

                string sqlTempTable = $@"if object_id('tempdb..#tempParseAbc') is not null drop table #tempParseAbc
                                        CREATE TABLE #tempParseAbc(id_kontr INT, id_tov_oem_short VARCHAR(50), tm_name varchar(255), abc VARCHAR(5));
                                        ";
                DBGroupSaver.AddRow(sqlTempTable);

                for (int i = (int)StartRow.Value - 1; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];

                    string nArt = row[idArt].ToString();
                    nArt = StringWorker.DelSymbols(nArt, '/', '.', '-', ' ', '\\', '|', '&', '^', '*', ';', ':', '>', '<', ',', '+', '"', '?', '=', '(', ')');
                    if (nArt == "")
                        continue;

                    string nBrend = ckSetBrand.Checked ? tBrend : row[idBrend].ToString();

                    // string nAbc = StringWorker.DelSymbols(row[idAbc].ToString(), '/', '.', '-', ' ', '\\', '|', '&', '^', '*', ';', ':', '>', '<', ',', '+', '"', '?', '=', '(', ')');
                    string nAbc = row[idAbc].ToString().Trim();

                    if (nAbc == "")
                        continue;

                    if (nAbc.Length > 5)
                    {
                        nAbc = nAbc.Substring(0, 5);
                    }
                    //int fStock = cbStock.Checked ? 1 : 0;

                    string sql = $@"INSERT INTO #tempParseAbc VALUES({idKontr}, '{nArt}', '{nBrend}', '{nAbc}');
                                   ";
                    DBGroupSaver.AddRow(sql);

                    abccnt++;
                }

                string updateQuery = $@"update kontr_tov_price set abcSupplier = #tempParseAbc.abc
                                        from kontr_tov_price ktp (nolock)
                                        inner join #tempParseAbc on #tempParseAbc.id_kontr = ktp.id_kontr
                                        inner join spr_tm (nolock) on spr_tm.tm_id = ktp.id_tm
                                        where lower(ktp.id_tov_oem_short) = lower(#tempParseAbc.id_tov_oem_short)
                                        and lower(spr_tm.tm_name) = lower(#tempParseAbc.tm_name)
                                        and ktp.date_price = (select max(date_price) from kontr_tov_price k (nolock) where k.id_kontr = #tempParseAbc.id_kontr and lower(k.id_tov_oem_short) = lower(ktp.id_tov_oem_short)
                                        and k.id_tm = ktp.id_tm)";

                DBGroupSaver.AddRow(updateQuery);

                DBGroupSaver.SaveAll();

                errcnt = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: " + ex.Message);
                UniLogger.WriteLog("", 1, "Ошибка: " + ex.Message);
                errcnt++;
            }
            finally
            {
                Cursor = Cursors.Default;

                if (errcnt > 0)
                {
                    MessageBox.Show($"Ошибки при обработке файла: {errcnt}", "Результат обработки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    RefrCntAbc();
                    MessageBox.Show($@"Загрузка завершена. Количество строк в исходном файле: {table.Rows.Count}, товаров с АВС: {abccnt}", "Результат обработки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void RefrCntAbc()
        {
            if (int.Parse(idSupplier.SelectedIndex.ToString()) <= 0) { return; }

            int idKontr = int.Parse(idSupplier.SelectedValue.ToString());

            string sqlQuery = $@"select count(*) cnt from kontr_tov_price (nolock) where id_kontr = {idKontr} and abcSupplier is not null and len(trim(abcsupplier)) > 0";

            var row = DBExecutor.SelectRow(sqlQuery);

            labelControl1.Text = $"Количество товаров с АВС поставщика: {(int)row["cnt"]}";
        }

        private void idSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefrCntAbc();
        }
    }
}

