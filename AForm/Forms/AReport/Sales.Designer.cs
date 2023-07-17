namespace AForm.Forms.AReport
{
    partial class Sales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pgc = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.Agent = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Kontr = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Brand = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Group = new DevExpress.XtraPivotGrid.PivotGridField();
            this.dateD = new DevExpress.XtraPivotGrid.PivotGridField();
            this.sumSale1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.profitSale1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.SaleAll1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.profitAll1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nnZakTov = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nnViewMotivation = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nnCompany = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nnDay = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nnMonth = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nidKontr = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nnSegment = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nCategory1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nTov = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nArt = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nOblast = new DevExpress.XtraPivotGrid.PivotGridField();
            this.nCountSales = new DevExpress.XtraPivotGrid.PivotGridField();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.deEnd = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.deStart = new DevExpress.XtraEditors.DateEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bLoad = new DevExpress.XtraBars.BarButtonItem();
            this.bSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.YY = new DevExpress.XtraPivotGrid.PivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.pgc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // pgc
            // 
            this.pgc.ActiveFilterString = "";
            this.pgc.Appearance.Cell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.Cell.Options.UseFont = true;
            this.pgc.Appearance.ColumnHeaderArea.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.ColumnHeaderArea.Options.UseFont = true;
            this.pgc.Appearance.ColumnHeaderArea.Options.UseTextOptions = true;
            this.pgc.Appearance.ColumnHeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pgc.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.CustomTotalCell.Options.UseFont = true;
            this.pgc.Appearance.DataHeaderArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pgc.Appearance.DataHeaderArea.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.DataHeaderArea.Options.UseBackColor = true;
            this.pgc.Appearance.DataHeaderArea.Options.UseFont = true;
            this.pgc.Appearance.DataHeaderArea.Options.UseTextOptions = true;
            this.pgc.Appearance.DataHeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pgc.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.Empty.Options.UseFont = true;
            this.pgc.Appearance.ExpandButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.ExpandButton.Options.UseFont = true;
            this.pgc.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.FieldHeader.Options.UseFont = true;
            this.pgc.Appearance.FieldValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.FieldValue.Options.UseFont = true;
            this.pgc.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.pgc.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.FieldValueTotal.Options.UseFont = true;
            this.pgc.Appearance.FilterHeaderArea.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.FilterHeaderArea.Options.UseFont = true;
            this.pgc.Appearance.FilterSeparator.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.FilterSeparator.Options.UseFont = true;
            this.pgc.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.FocusedCell.Options.UseFont = true;
            this.pgc.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.GrandTotalCell.Options.UseFont = true;
            this.pgc.Appearance.HeaderArea.BackColor = System.Drawing.Color.Transparent;
            this.pgc.Appearance.HeaderArea.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.HeaderArea.Options.UseBackColor = true;
            this.pgc.Appearance.HeaderArea.Options.UseFont = true;
            this.pgc.Appearance.HeaderArea.Options.UseTextOptions = true;
            this.pgc.Appearance.HeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pgc.Appearance.HeaderFilterButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.HeaderFilterButton.Options.UseFont = true;
            this.pgc.Appearance.HeaderFilterButtonActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.HeaderFilterButtonActive.Options.UseFont = true;
            this.pgc.Appearance.HeaderGroupLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.HeaderGroupLine.Options.UseFont = true;
            this.pgc.Appearance.Lines.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.Lines.Options.UseFont = true;
            this.pgc.Appearance.RowHeaderArea.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.RowHeaderArea.Options.UseFont = true;
            this.pgc.Appearance.SelectedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.SelectedCell.Options.UseFont = true;
            this.pgc.Appearance.TotalCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pgc.Appearance.TotalCell.Options.UseFont = true;
            this.pgc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgc.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.Agent,
            this.Kontr,
            this.Brand,
            this.Group,
            this.dateD,
            this.sumSale1,
            this.profitSale1,
            this.SaleAll1,
            this.profitAll1,
            this.nnZakTov,
            this.nnViewMotivation,
            this.nnCompany,
            this.nnDay,
            this.nnMonth,
            this.nidKontr,
            this.nnSegment,
            this.nCategory1,
            this.nTov,
            this.nArt,
            this.nOblast,
            this.nCountSales,
            this.YY});
            this.pgc.Location = new System.Drawing.Point(0, 86);
            this.pgc.Name = "pgc";
            this.pgc.Size = new System.Drawing.Size(1038, 407);
            this.pgc.TabIndex = 0;
            this.pgc.Click += new System.EventHandler(this.pgc_Click);
            // 
            // Agent
            // 
            this.Agent.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.Agent.AreaIndex = 0;
            this.Agent.Caption = "Агент";
            this.Agent.FieldName = "nAgent";
            this.Agent.Name = "Agent";
            this.Agent.Width = 269;
            // 
            // Kontr
            // 
            this.Kontr.AreaIndex = 2;
            this.Kontr.Caption = "Покупатель";
            this.Kontr.FieldName = "nKontr";
            this.Kontr.MinWidth = 100;
            this.Kontr.Name = "Kontr";
            this.Kontr.Width = 244;
            // 
            // Brand
            // 
            this.Brand.AreaIndex = 0;
            this.Brand.Caption = "Бренд";
            this.Brand.FieldName = "nBrand";
            this.Brand.Name = "Brand";
            this.Brand.Width = 116;
            // 
            // Group
            // 
            this.Group.AreaIndex = 1;
            this.Group.Caption = "Товарная группа";
            this.Group.FieldName = "nGroup";
            this.Group.Name = "Group";
            this.Group.Width = 194;
            // 
            // dateD
            // 
            this.dateD.Appearance.Value.Options.UseTextOptions = true;
            this.dateD.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateD.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.dateD.AreaIndex = 0;
            this.dateD.Caption = "Дата";
            this.dateD.FieldName = "dateDoc";
            this.dateD.Name = "dateD";
            this.dateD.Width = 239;
            // 
            // sumSale1
            // 
            this.sumSale1.Appearance.Header.ForeColor = System.Drawing.Color.Green;
            this.sumSale1.Appearance.Header.Options.UseForeColor = true;
            this.sumSale1.Appearance.Value.Options.UseTextOptions = true;
            this.sumSale1.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sumSale1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.sumSale1.AreaIndex = 0;
            this.sumSale1.Caption = "Объем продаж";
            this.sumSale1.CellFormat.FormatString = "### ### ### ##0.00";
            this.sumSale1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sumSale1.FieldName = "sumSale";
            this.sumSale1.Name = "sumSale1";
            this.sumSale1.Width = 109;
            // 
            // profitSale1
            // 
            this.profitSale1.Appearance.Header.ForeColor = System.Drawing.Color.Green;
            this.profitSale1.Appearance.Header.Options.UseForeColor = true;
            this.profitSale1.Appearance.Header.Options.UseTextOptions = true;
            this.profitSale1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.profitSale1.Appearance.Value.Options.UseTextOptions = true;
            this.profitSale1.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.profitSale1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.profitSale1.AreaIndex = 1;
            this.profitSale1.Caption = "ВМД";
            this.profitSale1.CellFormat.FormatString = "### ### ### ##0.00";
            this.profitSale1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.profitSale1.FieldName = "profitSale";
            this.profitSale1.Name = "profitSale1";
            // 
            // SaleAll1
            // 
            this.SaleAll1.Appearance.Header.ForeColor = System.Drawing.Color.Green;
            this.SaleAll1.Appearance.Header.Options.UseForeColor = true;
            this.SaleAll1.Appearance.Header.Options.UseTextOptions = true;
            this.SaleAll1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SaleAll1.Appearance.Value.Options.UseTextOptions = true;
            this.SaleAll1.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SaleAll1.AreaIndex = 3;
            this.SaleAll1.Caption = "Итого";
            this.SaleAll1.CellFormat.FormatString = "### ### ### ##0.00";
            this.SaleAll1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SaleAll1.FieldName = "SaleAll";
            this.SaleAll1.Name = "SaleAll1";
            // 
            // profitAll1
            // 
            this.profitAll1.Appearance.Header.BackColor = System.Drawing.Color.White;
            this.profitAll1.Appearance.Header.BackColor2 = System.Drawing.Color.White;
            this.profitAll1.Appearance.Header.BorderColor = System.Drawing.Color.White;
            this.profitAll1.Appearance.Header.ForeColor = System.Drawing.Color.Green;
            this.profitAll1.Appearance.Header.Options.UseBackColor = true;
            this.profitAll1.Appearance.Header.Options.UseBorderColor = true;
            this.profitAll1.Appearance.Header.Options.UseForeColor = true;
            this.profitAll1.Appearance.Header.Options.UseTextOptions = true;
            this.profitAll1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.profitAll1.Appearance.Value.Options.UseTextOptions = true;
            this.profitAll1.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.profitAll1.AreaIndex = 4;
            this.profitAll1.Caption = "Итого ВМД";
            this.profitAll1.CellFormat.FormatString = "### ### ### ##0.00";
            this.profitAll1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.profitAll1.FieldName = "profitAll";
            this.profitAll1.Name = "profitAll1";
            // 
            // nnZakTov
            // 
            this.nnZakTov.AreaIndex = 5;
            this.nnZakTov.Caption = "Наличие";
            this.nnZakTov.FieldName = "nZakTov";
            this.nnZakTov.Name = "nnZakTov";
            // 
            // nnViewMotivation
            // 
            this.nnViewMotivation.AreaIndex = 6;
            this.nnViewMotivation.Caption = "Вид для мотивации";
            this.nnViewMotivation.FieldName = "nViewMotivation";
            this.nnViewMotivation.Name = "nnViewMotivation";
            // 
            // nnCompany
            // 
            this.nnCompany.AreaIndex = 7;
            this.nnCompany.Caption = "Компания";
            this.nnCompany.FieldName = "nCompany";
            this.nnCompany.Name = "nnCompany";
            // 
            // nnDay
            // 
            this.nnDay.Appearance.Value.Options.UseTextOptions = true;
            this.nnDay.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nnDay.AreaIndex = 8;
            this.nnDay.Caption = "День недели";
            this.nnDay.FieldName = "nDay";
            this.nnDay.Name = "nnDay";
            // 
            // nnMonth
            // 
            this.nnMonth.Appearance.Header.Options.UseTextOptions = true;
            this.nnMonth.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nnMonth.Appearance.Value.Options.UseTextOptions = true;
            this.nnMonth.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nnMonth.AreaIndex = 9;
            this.nnMonth.Caption = "Месяц";
            this.nnMonth.FieldName = "nMonth";
            this.nnMonth.Name = "nnMonth";
            // 
            // nidKontr
            // 
            this.nidKontr.AreaIndex = 10;
            this.nidKontr.Caption = "Код покупателя";
            this.nidKontr.FieldName = "idKontr";
            this.nidKontr.Name = "nidKontr";
            // 
            // nnSegment
            // 
            this.nnSegment.AreaIndex = 11;
            this.nnSegment.Caption = "Канал сбыта";
            this.nnSegment.FieldName = "nSegment";
            this.nnSegment.Name = "nnSegment";
            // 
            // nCategory1
            // 
            this.nCategory1.AreaIndex = 12;
            this.nCategory1.Caption = "Ценовая категория";
            this.nCategory1.FieldName = "nCategory";
            this.nCategory1.Name = "nCategory1";
            // 
            // nTov
            // 
            this.nTov.AreaIndex = 14;
            this.nTov.Caption = "Наименование товара";
            this.nTov.FieldName = "nGoods";
            this.nTov.Name = "nTov";
            // 
            // nArt
            // 
            this.nArt.AreaIndex = 13;
            this.nArt.Caption = "Артикул";
            this.nArt.FieldName = "vendorCode";
            this.nArt.Name = "nArt";
            // 
            // nOblast
            // 
            this.nOblast.AreaIndex = 15;
            this.nOblast.Caption = "Область";
            this.nOblast.FieldName = "nDistrict";
            this.nOblast.Name = "nOblast";
            // 
            // nCountSales
            // 
            this.nCountSales.Appearance.Value.Options.UseTextOptions = true;
            this.nCountSales.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.nCountSales.AreaIndex = 16;
            this.nCountSales.Caption = "Количество продаж";
            this.nCountSales.CellFormat.FormatString = "Numeric \"### ### ### ##0\"";
            this.nCountSales.FieldName = "numberOfSales";
            this.nCountSales.Name = "nCountSales";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.deEnd);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.deStart);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1038, 38);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(238, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(16, 17);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "по";
            // 
            // deEnd
            // 
            this.deEnd.EditValue = null;
            this.deEnd.Location = new System.Drawing.Point(283, 7);
            this.deEnd.Name = "deEnd";
            this.deEnd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.deEnd.Properties.Appearance.Options.UseFont = true;
            this.deEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Size = new System.Drawing.Size(145, 24);
            this.deEnd.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Дата с";
            // 
            // deStart
            // 
            this.deStart.EditValue = null;
            this.deStart.Location = new System.Drawing.Point(76, 7);
            this.deStart.Name = "deStart";
            this.deStart.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.deStart.Properties.Appearance.Options.UseFont = true;
            this.deStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Size = new System.Drawing.Size(147, 24);
            this.deStart.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.standaloneBarDockControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 38);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1038, 48);
            this.panelControl2.TabIndex = 2;
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(2, 2);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1034, 43);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bLoad,
            this.bSave});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Пользовательская 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.bSave)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Пользовательская 1";
            // 
            // bLoad
            // 
            this.bLoad.Caption = "Обновить";
            this.bLoad.Id = 0;
            this.bLoad.ImageOptions.Image = global::AForm.Properties.Resources.update;
            this.bLoad.Name = "bLoad";
            this.bLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bLoad_ItemClick);
            // 
            // bSave
            // 
            this.bSave.Caption = "Сохранить";
            this.bSave.Id = 1;
            this.bSave.ImageOptions.Image = global::AForm.Properties.Resources.save_as;
            this.bSave.Name = "bSave";
            this.bSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1038, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 493);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1038, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 493);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1038, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 493);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "*.xls|XLS|*.xlsx|XLSX";
            // 
            // YY
            // 
            this.YY.AreaIndex = 17;
            this.YY.Caption = "Год";
            this.YY.FieldName = "yy";
            this.YY.Name = "YY";
            // 
            // Sales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 493);
            this.Controls.Add(this.pgc);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Sales";
            this.Text = "Sales";
            ((System.ComponentModel.ISupportInitialize)(this.pgc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridControl pgc;
        private DevExpress.XtraPivotGrid.PivotGridField Agent;
        private DevExpress.XtraPivotGrid.PivotGridField Kontr;
        private DevExpress.XtraPivotGrid.PivotGridField Brand;
        private DevExpress.XtraPivotGrid.PivotGridField Group;
        private DevExpress.XtraPivotGrid.PivotGridField dateD;
        private DevExpress.XtraPivotGrid.PivotGridField sumSale1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit deEnd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deStart;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bLoad;
        private DevExpress.XtraBars.BarButtonItem bSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraPivotGrid.PivotGridField profitSale1;
        private DevExpress.XtraPivotGrid.PivotGridField SaleAll1;
        private DevExpress.XtraPivotGrid.PivotGridField profitAll1;
        private DevExpress.XtraPivotGrid.PivotGridField nnZakTov;
        private DevExpress.XtraPivotGrid.PivotGridField nnViewMotivation;
        private DevExpress.XtraPivotGrid.PivotGridField nnCompany;
        private DevExpress.XtraPivotGrid.PivotGridField nnDay;
        private DevExpress.XtraPivotGrid.PivotGridField nnMonth;
        private DevExpress.XtraPivotGrid.PivotGridField nidKontr;
        private DevExpress.XtraPivotGrid.PivotGridField nnSegment;
        private DevExpress.XtraPivotGrid.PivotGridField nCategory1;
        private DevExpress.XtraPivotGrid.PivotGridField nTov;
        private DevExpress.XtraPivotGrid.PivotGridField nArt;
        private DevExpress.XtraPivotGrid.PivotGridField nOblast;
        private DevExpress.XtraPivotGrid.PivotGridField nCountSales;
        private DevExpress.XtraPivotGrid.PivotGridField YY;
    }
}