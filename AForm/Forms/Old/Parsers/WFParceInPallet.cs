using ALogic.DBConnector;
using ALogic.Logic.Heplers;
using ALogic.Logic.Old.Parsers;
using ALogic.Logic.SPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace AForm.Forms.Old.Parsers
{
    public partial class WFParceInPallet : DevExpress.XtraEditors.XtraForm
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
        public WFParceInPallet(int idParserSettings = 0)
        {
            InitializeComponent();
            this.Tag = "Парсинг кол-ва в паллете";
            prevSheetN = 0;

        }

        /// <summary>
        /// Очистка формы
        /// </summary>
        private void ResetForm()
        {

            gridFile.DataSource = null;
            tbFilePath.Text = string.Empty;
            btnClear.Visible = false;
            nartikulColSel.Text = string.Empty;
            nbrandColSel.Text = string.Empty;
            nQtyColSel.Text = string.Empty;
            ckSetBrand.Checked = false;
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
                btnClear.Visible = true;
            }
        }


        // GRID НАСТРОЙКИ
        //###########################################################################################################################
        private static string ExcelColumnIndexToName(int Index)
        {
            string range = string.Empty;
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
                nbrandColSel.Text = string.Empty;
                ParserSettingsHelper.fillBrand(fBrend);
                fBrend.Enabled = true;
                nbrandColSel.Enabled = false;
                brandColSel.Enabled = false;

            }
            else
            {
                nbrandColSel.Text = string.Empty;
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
                nQtyColSel.Text = ExcelColumnIndexToName(gridFile.CurrentCell.ColumnIndex);

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
                    if (textBox.Text != string.Empty)
                    {
                        condition.Add(new ParserSettingsColumn(int.Parse(textBox.Tag.ToString()), ExcelColumnNameToIndex(textBox.Text), prevSheetN));

                    }
                }
            }

            return condition;
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            var table = gridFile.DataSource as DataTable;

            int idArt = ExcelColumnNameToIndex(nartikulColSel.Text);
            int idBrend = ckSetBrand.Checked ? 0 : ExcelColumnNameToIndex(nbrandColSel.Text);
            int idKol = ExcelColumnNameToIndex(nQtyColSel.Text);

            string tBrend = ckSetBrand.Checked ? fBrend.Text.ToString() : string.Empty;

            for (int i = (int)StartRow.Value - 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                string nArt = row[idArt].ToString();
                nArt = StringWorker.DelSymbols(nArt, '/', '.', '-', ' ', '\\', '|', '&', '^', '*', ';', ':', '>', '<', ',', '+', '"', '?', '=', '(', ')');
                if (nArt == string.Empty)
                    continue;

                string nBrend = ckSetBrand.Checked ? tBrend : row[idBrend].ToString();
                string nKol = StringWorker.DelSymbols(row[idKol].ToString(), '/', '.', '-', ' ', '\\', '|', '&', '^', '*', ';', ':', '>', '<', ',', '+', '"', '?', '=', '(', ')');
                int kol;

                if (!int.TryParse(nKol, out kol))
                    continue;

                int fStock = cbStock.Checked ? 1 : 0;
                string sql = "insert into ParceNupTemp (idUser, nArt, nTm, kol, fStock)";
                sql += "values(" + User.CurrentUserId.ToString() + ",'" + nArt + "','" + nBrend + "'," + kol.ToString() + "," + fStock.ToString() + ")";

                DBGroupSaver.AddRow(sql);
            }

            DBGroupSaver.SaveAll();

            string query = "exec up_LoadParceNupTemp " + User.CurrentUserId.ToString();
            gridFile.DataSource = DBExecutor.SelectTable(query);


        }
    }
}

