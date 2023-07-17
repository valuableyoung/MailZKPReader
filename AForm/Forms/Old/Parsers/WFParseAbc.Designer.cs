
namespace AForm.Forms.Old.Parsers
{
    partial class WFParseABC
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.idSupplier = new System.Windows.Forms.ComboBox();
            this.gbSupplier = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnFindFile = new System.Windows.Forms.Button();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.CellSelect = new System.Windows.Forms.Button();
            this.StartRow = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.gridFile = new System.Windows.Forms.DataGridView();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fAllList = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ckSetBrand = new System.Windows.Forms.CheckBox();
            this.fBrend = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.gpParseSetting = new System.Windows.Forms.GroupBox();
            this.cbStock = new System.Windows.Forms.CheckBox();
            this.nABC = new System.Windows.Forms.TextBox();
            this.nbrandColSel = new System.Windows.Forms.TextBox();
            this.nartikulColSel = new System.Windows.Forms.TextBox();
            this.QtyColSel = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.brandColSel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.artikulColSel = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.bLoad = new System.Windows.Forms.Button();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.gbSupplier.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StartRow)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFile)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.gpParseSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView3);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(168, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(577, 91);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Корректировочный";
            this.groupBox4.Visible = false;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(18, 52);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(546, 74);
            this.dataGridView3.TabIndex = 1;
            // 
            // idSupplier
            // 
            this.idSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.idSupplier.FormattingEnabled = true;
            this.idSupplier.Location = new System.Drawing.Point(11, 23);
            this.idSupplier.Name = "idSupplier";
            this.idSupplier.Size = new System.Drawing.Size(604, 24);
            this.idSupplier.TabIndex = 0;
            this.idSupplier.SelectedIndexChanged += new System.EventHandler(this.idSupplier_SelectedIndexChanged);
            // 
            // gbSupplier
            // 
            this.gbSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSupplier.Controls.Add(this.labelControl1);
            this.gbSupplier.Controls.Add(this.idSupplier);
            this.gbSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbSupplier.Location = new System.Drawing.Point(393, 109);
            this.gbSupplier.Name = "gbSupplier";
            this.gbSupplier.Size = new System.Drawing.Size(626, 83);
            this.gbSupplier.TabIndex = 2;
            this.gbSupplier.TabStop = false;
            this.gbSupplier.Text = "Поставщик";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.btnFindFile);
            this.groupBox5.Controls.Add(this.tbFilePath);
            this.groupBox5.Location = new System.Drawing.Point(6, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(362, 69);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Файл";
            // 
            // btnFindFile
            // 
            this.btnFindFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindFile.Image = global::AForm.Properties.Resources.find;
            this.btnFindFile.Location = new System.Drawing.Point(310, 23);
            this.btnFindFile.Name = "btnFindFile";
            this.btnFindFile.Size = new System.Drawing.Size(37, 34);
            this.btnFindFile.TabIndex = 34;
            this.btnFindFile.UseVisualStyleBackColor = true;
            this.btnFindFile.Click += new System.EventHandler(this.LoadFile);
            // 
            // tbFilePath
            // 
            this.tbFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbFilePath.Location = new System.Drawing.Point(12, 24);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(297, 32);
            this.tbFilePath.TabIndex = 33;
            this.tbFilePath.Click += new System.EventHandler(this.LoadFile);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(530, 43);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 34);
            this.btnClear.TabIndex = 35;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // CellSelect
            // 
            this.CellSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CellSelect.Location = new System.Drawing.Point(173, 33);
            this.CellSelect.Name = "CellSelect";
            this.CellSelect.Size = new System.Drawing.Size(31, 27);
            this.CellSelect.TabIndex = 37;
            this.CellSelect.Text = "+";
            this.CellSelect.UseVisualStyleBackColor = true;
            this.CellSelect.Click += new System.EventHandler(this.CellSelect_Click);
            // 
            // StartRow
            // 
            this.StartRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartRow.Location = new System.Drawing.Point(129, 33);
            this.StartRow.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.StartRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StartRow.Name = "StartRow";
            this.StartRow.Size = new System.Drawing.Size(40, 26);
            this.StartRow.TabIndex = 36;
            this.StartRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(7, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Старт со строки:";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.gridFile);
            this.groupBox7.Controls.Add(this.groupBox5);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox7.Location = new System.Drawing.Point(13, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(374, 1007);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Просмотр файла";
            // 
            // gridFile
            // 
            this.gridFile.AllowUserToAddRows = false;
            this.gridFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFile.Location = new System.Drawing.Point(18, 97);
            this.gridFile.Name = "gridFile";
            this.gridFile.Size = new System.Drawing.Size(334, 896);
            this.gridFile.TabIndex = 36;
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox10.Controls.Add(this.label1);
            this.groupBox10.Controls.Add(this.fAllList);
            this.groupBox10.Controls.Add(this.groupBox4);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox10.Location = new System.Drawing.Point(1056, 760);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(400, 112);
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Доп. настройки";
            this.groupBox10.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Разделитель";
            // 
            // fAllList
            // 
            this.fAllList.AutoSize = true;
            this.fAllList.Location = new System.Drawing.Point(8, 50);
            this.fAllList.Name = "fAllList";
            this.fAllList.Size = new System.Drawing.Size(215, 21);
            this.fAllList.TabIndex = 27;
            this.fAllList.Text = "Считывать все листы файла";
            this.fAllList.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.btnClear);
            this.groupBox6.Controls.Add(this.ckSetBrand);
            this.groupBox6.Controls.Add(this.fBrend);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox6.Location = new System.Drawing.Point(393, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(626, 87);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Для парсинга";
            // 
            // ckSetBrand
            // 
            this.ckSetBrand.AutoSize = true;
            this.ckSetBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckSetBrand.Location = new System.Drawing.Point(11, 23);
            this.ckSetBrand.Name = "ckSetBrand";
            this.ckSetBrand.Size = new System.Drawing.Size(148, 21);
            this.ckSetBrand.TabIndex = 32;
            this.ckSetBrand.Text = "Подставить бренд";
            this.ckSetBrand.UseVisualStyleBackColor = true;
            this.ckSetBrand.CheckedChanged += new System.EventHandler(this.ckSetBrand_CheckedChanged);
            // 
            // fBrend
            // 
            this.fBrend.Enabled = false;
            this.fBrend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fBrend.FormattingEnabled = true;
            this.fBrend.Location = new System.Drawing.Point(11, 49);
            this.fBrend.Name = "fBrend";
            this.fBrend.Size = new System.Drawing.Size(399, 24);
            this.fBrend.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.Location = new System.Drawing.Point(865, 968);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(143, 37);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "ОТМЕНА";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gpParseSetting
            // 
            this.gpParseSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gpParseSetting.Controls.Add(this.cbStock);
            this.gpParseSetting.Controls.Add(this.nABC);
            this.gpParseSetting.Controls.Add(this.nbrandColSel);
            this.gpParseSetting.Controls.Add(this.nartikulColSel);
            this.gpParseSetting.Controls.Add(this.CellSelect);
            this.gpParseSetting.Controls.Add(this.StartRow);
            this.gpParseSetting.Controls.Add(this.label9);
            this.gpParseSetting.Controls.Add(this.QtyColSel);
            this.gpParseSetting.Controls.Add(this.label13);
            this.gpParseSetting.Controls.Add(this.brandColSel);
            this.gpParseSetting.Controls.Add(this.label11);
            this.gpParseSetting.Controls.Add(this.artikulColSel);
            this.gpParseSetting.Controls.Add(this.label10);
            this.gpParseSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gpParseSetting.Location = new System.Drawing.Point(393, 198);
            this.gpParseSetting.Name = "gpParseSetting";
            this.gpParseSetting.Size = new System.Drawing.Size(314, 222);
            this.gpParseSetting.TabIndex = 34;
            this.gpParseSetting.TabStop = false;
            this.gpParseSetting.Text = "Настройки листа";
            // 
            // cbStock
            // 
            this.cbStock.AutoSize = true;
            this.cbStock.Location = new System.Drawing.Point(210, 100);
            this.cbStock.Name = "cbStock";
            this.cbStock.Size = new System.Drawing.Size(58, 21);
            this.cbStock.TabIndex = 62;
            this.cbStock.Text = "Сток";
            this.cbStock.UseVisualStyleBackColor = true;
            this.cbStock.Visible = false;
            // 
            // nABC
            // 
            this.nABC.Location = new System.Drawing.Point(125, 182);
            this.nABC.MaxLength = 2;
            this.nABC.Name = "nABC";
            this.nABC.Size = new System.Drawing.Size(40, 23);
            this.nABC.TabIndex = 60;
            this.nABC.Tag = "4";
            // 
            // nbrandColSel
            // 
            this.nbrandColSel.Location = new System.Drawing.Point(125, 141);
            this.nbrandColSel.MaxLength = 2;
            this.nbrandColSel.Name = "nbrandColSel";
            this.nbrandColSel.Size = new System.Drawing.Size(40, 23);
            this.nbrandColSel.TabIndex = 58;
            this.nbrandColSel.Tag = "2";
            // 
            // nartikulColSel
            // 
            this.nartikulColSel.Location = new System.Drawing.Point(125, 97);
            this.nartikulColSel.MaxLength = 2;
            this.nartikulColSel.Name = "nartikulColSel";
            this.nartikulColSel.Size = new System.Drawing.Size(40, 23);
            this.nartikulColSel.TabIndex = 57;
            this.nartikulColSel.Tag = "1";
            // 
            // QtyColSel
            // 
            this.QtyColSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QtyColSel.Location = new System.Drawing.Point(173, 180);
            this.QtyColSel.Name = "QtyColSel";
            this.QtyColSel.Size = new System.Drawing.Size(31, 26);
            this.QtyColSel.TabIndex = 47;
            this.QtyColSel.Text = "+";
            this.QtyColSel.UseVisualStyleBackColor = true;
            this.QtyColSel.Click += new System.EventHandler(this.QtyColSel_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(85, 185);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 17);
            this.label13.TabIndex = 45;
            this.label13.Text = "ABC";
            // 
            // brandColSel
            // 
            this.brandColSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brandColSel.Location = new System.Drawing.Point(173, 139);
            this.brandColSel.Name = "brandColSel";
            this.brandColSel.Size = new System.Drawing.Size(31, 26);
            this.brandColSel.TabIndex = 41;
            this.brandColSel.Text = "+";
            this.brandColSel.UseVisualStyleBackColor = true;
            this.brandColSel.Click += new System.EventHandler(this.brandColSel_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(71, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 17);
            this.label11.TabIndex = 39;
            this.label11.Text = "Бренд";
            // 
            // artikulColSel
            // 
            this.artikulColSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.artikulColSel.Location = new System.Drawing.Point(173, 95);
            this.artikulColSel.Name = "artikulColSel";
            this.artikulColSel.Size = new System.Drawing.Size(31, 26);
            this.artikulColSel.TabIndex = 38;
            this.artikulColSel.Text = "+";
            this.artikulColSel.UseVisualStyleBackColor = true;
            this.artikulColSel.Click += new System.EventHandler(this.artikulColSel_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(58, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Артикул";
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bLoad.Location = new System.Drawing.Point(716, 968);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(143, 37);
            this.bLoad.TabIndex = 35;
            this.bLoad.Text = "ЗАГРУЗИТЬ";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(11, 64);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 16);
            this.labelControl1.TabIndex = 37;
            this.labelControl1.Text = "...";
            // 
            // WFParseABC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 1025);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.gpParseSetting);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.gbSupplier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "WFParseABC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Парсер ABC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.gbSupplier.ResumeLayout(false);
            this.gbSupplier.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StartRow)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFile)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.gpParseSetting.ResumeLayout(false);
            this.gpParseSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox idSupplier;
        private System.Windows.Forms.GroupBox gbSupplier;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox fAllList;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox ckSetBrand;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox fBrend;
        private System.Windows.Forms.Button btnFindFile;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.NumericUpDown StartRow;
        private System.Windows.Forms.Button CellSelect;
        private System.Windows.Forms.GroupBox gpParseSetting;
        private System.Windows.Forms.Button artikulColSel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button brandColSel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button QtyColSel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox nABC;
        private System.Windows.Forms.TextBox nbrandColSel;
        private System.Windows.Forms.TextBox nartikulColSel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView gridFile;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.CheckBox cbStock;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}

