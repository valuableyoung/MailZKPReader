namespace AForm.Forms.Old.BuySalePlan
{ 
    partial class SalePlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalePlan));
            this.bm = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bAdd = new DevExpress.XtraBars.BarButtonItem();
            this.bDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bSave = new DevExpress.XtraBars.BarButtonItem();
            this.bLoad = new DevExpress.XtraBars.BarButtonItem();
            this.bSaveDesign = new DevExpress.XtraBars.BarButtonItem();
            this.bLoadDesign = new DevExpress.XtraBars.BarButtonItem();
            this.btnLoadExcelFile = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveDefault = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.image = new DevExpress.Utils.ImageCollection();
            this.bEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.reYear = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.idRegion = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.reRegion = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.idSegm = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.reSegment = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.idbrend = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.reBrand = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.naklRasx = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.type = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbK = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.vSale = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.vProfit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CalculatedVMD = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Marga = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.beYearSale = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ppLoad = new DevExpress.XtraWaitForm.ProgressPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pFilter = new DevExpress.XtraEditors.PanelControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSegment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pFilter)).BeginInit();
            this.pFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // bm
            // 
            this.bm.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.bm.DockControls.Add(this.barDockControlTop);
            this.bm.DockControls.Add(this.barDockControlBottom);
            this.bm.DockControls.Add(this.barDockControlLeft);
            this.bm.DockControls.Add(this.barDockControlRight);
            this.bm.Form = this;
            this.bm.Images = this.image;
            this.bm.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bAdd,
            this.bEdit,
            this.bDelete,
            this.bSaveDesign,
            this.bLoadDesign,
            this.bSave,
            this.bLoad,
            this.barButtonItem3,
            this.barStaticItem1,
            this.btnLoadExcelFile,
            this.btnSaveDefault});
            this.bm.MainMenu = this.bar2;
            this.bm.MaxItemId = 12;
            this.bm.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reYear});
            // 
            // bar2
            // 
            this.bar2.BarName = "Главное меню";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.bDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.bSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.bLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.bSaveDesign),
            new DevExpress.XtraBars.LinkPersistInfo(this.bLoadDesign),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLoadExcelFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveDefault)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MinHeight = 45;
            this.bar2.OptionsBar.RotateWhenVertical = false;
            this.bar2.Text = "Главное меню";
            // 
            // bAdd
            // 
            this.bAdd.Caption = "Добавить";
            this.bAdd.Id = 0;
            this.bAdd.ImageOptions.ImageIndex = 4;
            this.bAdd.Name = "bAdd";
            this.bAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bAdd_ItemClick);
            // 
            // bDelete
            // 
            this.bDelete.Caption = "Удалить";
            this.bDelete.Id = 2;
            this.bDelete.ImageOptions.ImageIndex = 0;
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(50, 50);
            this.bDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bDelete_ItemClick);
            // 
            // bSave
            // 
            this.bSave.Caption = "Сохранить";
            this.bSave.Enabled = false;
            this.bSave.Id = 5;
            this.bSave.ImageOptions.ImageIndex = 5;
            this.bSave.Name = "bSave";
            this.bSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bSave_ItemClick);
            // 
            // bLoad
            // 
            this.bLoad.Caption = "Обновить";
            this.bLoad.Id = 6;
            this.bLoad.ImageOptions.ImageIndex = 6;
            this.bLoad.Name = "bLoad";
            this.bLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bLoad_ItemClick);
            // 
            // bSaveDesign
            // 
            this.bSaveDesign.Caption = "Сохранить дизайн";
            this.bSaveDesign.Id = 3;
            this.bSaveDesign.ImageOptions.ImageIndex = 7;
            this.bSaveDesign.Name = "bSaveDesign";
            this.bSaveDesign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bSaveDesign_ItemClick);
            // 
            // bLoadDesign
            // 
            this.bLoadDesign.Caption = "Загрузить дизайн по умолчанию";
            this.bLoadDesign.Id = 4;
            this.bLoadDesign.ImageOptions.ImageIndex = 8;
            this.bLoadDesign.Name = "bLoadDesign";
            this.bLoadDesign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bLoadDesign_ItemClick);
            // 
            // btnLoadExcelFile
            // 
            this.btnLoadExcelFile.Caption = "Загрузить Excel файл";
            this.btnLoadExcelFile.Enabled = false;
            this.btnLoadExcelFile.Hint = "Выберите файл";
            this.btnLoadExcelFile.Id = 10;
            this.btnLoadExcelFile.ImageOptions.ImageIndex = 9;
            this.btnLoadExcelFile.Name = "btnLoadExcelFile";
            this.btnLoadExcelFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLoadExcelFile_ItemClick);
            // 
            // btnSaveDefault
            // 
            this.btnSaveDefault.Caption = "Сохранить настройки по умолчанию";
            this.btnSaveDefault.Id = 11;
            this.btnSaveDefault.ImageOptions.ImageIndex = 10;
            this.btnSaveDefault.Name = "btnSaveDefault";
            this.btnSaveDefault.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveDefault_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bm;
            this.barDockControlTop.Size = new System.Drawing.Size(1229, 50);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 695);
            this.barDockControlBottom.Manager = this.bm;
            this.barDockControlBottom.Size = new System.Drawing.Size(1229, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 50);
            this.barDockControlLeft.Manager = this.bm;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 645);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1229, 50);
            this.barDockControlRight.Manager = this.bm;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 645);
            // 
            // image
            // 
            this.image.ImageSize = new System.Drawing.Size(32, 32);
            this.image.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("image.ImageStream")));
            this.image.Images.SetKeyName(0, "delete.png");
            this.image.Images.SetKeyName(1, "door_in.png");
            this.image.Images.SetKeyName(2, "pencil.png");
            this.image.Images.SetKeyName(3, "accept.png");
            this.image.Images.SetKeyName(4, "add.png");
            this.image.Images.SetKeyName(5, "disk.png");
            this.image.Images.SetKeyName(6, "update.png");
            this.image.Images.SetKeyName(7, "table_save.png");
            this.image.Images.SetKeyName(8, "table_refresh.png");
            this.image.InsertGalleryImage("tableconverttorange_32x32.png", "images/spreadsheet/tableconverttorange_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/spreadsheet/tableconverttorange_32x32.png"), 9);
            this.image.Images.SetKeyName(9, "tableconverttorange_32x32.png");
            this.image.InsertGalleryImage("saveandnew_32x32.png", "images/save/saveandnew_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/save/saveandnew_32x32.png"), 10);
            this.image.Images.SetKeyName(10, "saveandnew_32x32.png");
            // 
            // bEdit
            // 
            this.bEdit.Caption = "Редактировать";
            this.bEdit.Id = 1;
            this.bEdit.ImageOptions.ImageIndex = 2;
            this.bEdit.Name = "bEdit";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Новый в2";
            this.barButtonItem3.Id = 7;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "ГОД";
            this.barStaticItem1.Id = 8;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // reYear
            // 
            this.reYear.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.reYear.Appearance.Options.UseFont = true;
            this.reYear.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.reYear.AppearanceDropDown.Options.UseFont = true;
            this.reYear.AutoHeight = false;
            this.reYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.reYear.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("year", "Год", 10, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending, DevExpress.Utils.DefaultBoolean.Default)});
            this.reYear.MaxLength = 30;
            this.reYear.Name = "reYear";
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Font = new System.Drawing.Font("Arial", 10F);
            this.gcMain.Location = new System.Drawing.Point(0, 79);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.MinimumSize = new System.Drawing.Size(0, 100);
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reBrand,
            this.reSegment,
            this.reRegion});
            this.gcMain.Size = new System.Drawing.Size(1229, 616);
            this.gcMain.TabIndex = 6;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.gvMain.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMain.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.gvMain.Appearance.Row.Options.UseFont = true;
            this.gvMain.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2,
            this.gbK,
            this.gridBand7});
            this.gvMain.ColumnPanelRowHeight = 50;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.idRegion,
            this.idbrend,
            this.idSegm,
            this.Marga,
            this.naklRasx,
            this.vSale,
            this.vProfit,
            this.type,
            this.CalculatedVMD});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvMain.OptionsView.ShowFooter = true;
            this.gvMain.ShownEditor += new System.EventHandler(this.gvMain_ShownEditor);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyUp);
            this.gvMain.Click += new System.EventHandler(this.gvMain_Click);
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridBand2.Columns.Add(this.idRegion);
            this.gridBand2.Columns.Add(this.idSegm);
            this.gridBand2.Columns.Add(this.idbrend);
            this.gridBand2.Columns.Add(this.naklRasx);
            this.gridBand2.Columns.Add(this.type);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 0;
            this.gridBand2.Width = 432;
            // 
            // idRegion
            // 
            this.idRegion.Caption = "Область";
            this.idRegion.ColumnEdit = this.reRegion;
            this.idRegion.FieldName = "idRegion";
            this.idRegion.Name = "idRegion";
            this.idRegion.Visible = true;
            // 
            // reRegion
            // 
            this.reRegion.AutoHeight = false;
            this.reRegion.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.reRegion.Name = "reRegion";
            // 
            // idSegm
            // 
            this.idSegm.Caption = "Канал сбыта";
            this.idSegm.ColumnEdit = this.reSegment;
            this.idSegm.FieldName = "idSegm";
            this.idSegm.Name = "idSegm";
            this.idSegm.RowCount = 2;
            this.idSegm.Visible = true;
            this.idSegm.Width = 76;
            // 
            // reSegment
            // 
            this.reSegment.AutoHeight = false;
            this.reSegment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.reSegment.DisplayMember = "nGroup";
            this.reSegment.Name = "reSegment";
            this.reSegment.ValueMember = "idGroup";
            // 
            // idbrend
            // 
            this.idbrend.Caption = "Бренд";
            this.idbrend.ColumnEdit = this.reBrand;
            this.idbrend.FieldName = "idbrend";
            this.idbrend.Name = "idbrend";
            this.idbrend.RowCount = 2;
            this.idbrend.Visible = true;
            this.idbrend.Width = 87;
            // 
            // reBrand
            // 
            this.reBrand.AutoHeight = false;
            this.reBrand.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.reBrand.Name = "reBrand";
            // 
            // naklRasx
            // 
            this.naklRasx.AppearanceHeader.Options.UseTextOptions = true;
            this.naklRasx.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.naklRasx.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.naklRasx.Caption = "Накладные расходы (План), %";
            this.naklRasx.FieldName = "naklRasx";
            this.naklRasx.Name = "naklRasx";
            this.naklRasx.RowCount = 2;
            this.naklRasx.Visible = true;
            this.naklRasx.Width = 119;
            // 
            // type
            // 
            this.type.Caption = "Тип";
            this.type.FieldName = "type";
            this.type.Name = "type";
            this.type.Visible = true;
            // 
            // gbK
            // 
            this.gbK.AppearanceHeader.Options.UseTextOptions = true;
            this.gbK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbK.Caption = "Объем продаж, тыс. руб (план)";
            this.gbK.Name = "gbK";
            this.gbK.VisibleIndex = 1;
            this.gbK.Width = 75;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridBand7.Columns.Add(this.vSale);
            this.gridBand7.Columns.Add(this.vProfit);
            this.gridBand7.Columns.Add(this.CalculatedVMD);
            this.gridBand7.Columns.Add(this.Marga);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 2;
            this.gridBand7.Width = 194;
            // 
            // vSale
            // 
            this.vSale.AppearanceHeader.Options.UseTextOptions = true;
            this.vSale.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.vSale.Caption = "Итого объем продаж за год (План), тыс. руб.";
            this.vSale.FieldName = "vProd";
            this.vSale.Name = "vSale";
            this.vSale.OptionsColumn.AllowEdit = false;
            this.vSale.RowCount = 4;
            this.vSale.ShowUnboundExpressionMenu = true;
            this.vSale.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "vProd", "{0:#.##}")});
            this.vSale.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.vSale.Visible = true;
            this.vSale.Width = 119;
            // 
            // vProfit
            // 
            this.vProfit.AppearanceCell.Options.UseTextOptions = true;
            this.vProfit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.vProfit.AppearanceHeader.Options.UseTextOptions = true;
            this.vProfit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.vProfit.AutoFillDown = true;
            this.vProfit.Caption = "ВМД (план), тыс. руб.";
            this.vProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.vProfit.FieldName = "vProfit";
            this.vProfit.Name = "vProfit";
            this.vProfit.OptionsColumn.AllowEdit = false;
            this.vProfit.RowCount = 2;
            this.vProfit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "vProfit", "{0:#.00}")});
            this.vProfit.UnboundExpression = "Round([vProd] / (1 + [Marga]) * [Marga], 2)";
            this.vProfit.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.vProfit.Width = 127;
            // 
            // CalculatedVMD
            // 
            this.CalculatedVMD.Caption = "ВМД (план), тыс. руб.";
            this.CalculatedVMD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.CalculatedVMD.FieldName = "CalculatedVMD";
            this.CalculatedVMD.Name = "CalculatedVMD";
            this.CalculatedVMD.OptionsColumn.AllowEdit = false;
            this.CalculatedVMD.Visible = true;
            // 
            // Marga
            // 
            this.Marga.AppearanceHeader.Options.UseTextOptions = true;
            this.Marga.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Marga.Caption = "Маржинальность (План), %";
            this.Marga.FieldName = "Marga";
            this.Marga.Name = "Marga";
            this.Marga.RowCount = 2;
            this.Marga.Width = 87;
            // 
            // beYearSale
            // 
            this.beYearSale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.beYearSale.Font = new System.Drawing.Font("Arial", 10F);
            this.beYearSale.FormattingEnabled = true;
            this.beYearSale.Location = new System.Drawing.Point(45, 2);
            this.beYearSale.Name = "beYearSale";
            this.beYearSale.Size = new System.Drawing.Size(63, 24);
            this.beYearSale.TabIndex = 18;
            this.beYearSale.SelectedIndexChanged += new System.EventHandler(this.beYearSale_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "ГОД";
            // 
            // ppLoad
            // 
            this.ppLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ppLoad.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ppLoad.Appearance.Options.UseBackColor = true;
            this.ppLoad.BarAnimationElementThickness = 2;
            this.ppLoad.Caption = "Пожалуйста подождите";
            this.ppLoad.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ppLoad.Description = "Сохранение данных ...";
            this.ppLoad.Location = new System.Drawing.Point(494, 295);
            this.ppLoad.Name = "ppLoad";
            this.ppLoad.Size = new System.Drawing.Size(274, 95);
            this.ppLoad.TabIndex = 23;
            this.ppLoad.Text = "progressPanel1";
            this.ppLoad.Visible = false;
            this.ppLoad.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Line;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // pFilter
            // 
            this.pFilter.Controls.Add(this.checkBox1);
            this.pFilter.Controls.Add(this.label1);
            this.pFilter.Controls.Add(this.beYearSale);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilter.Location = new System.Drawing.Point(0, 50);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(1229, 29);
            this.pFilter.TabIndex = 28;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.checkBox1.Location = new System.Drawing.Point(127, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(164, 20);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "Только для прогноза";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // SalePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 695);
            this.Controls.Add(this.ppLoad);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pFilter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SalePlan";
            this.Text = "План продаж";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalePlan_FormClosing);
            this.Load += new System.EventHandler(this.SalePlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSegment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pFilter)).EndInit();
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager bm;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bAdd;
        private DevExpress.XtraBars.BarButtonItem bEdit;
        private DevExpress.XtraBars.BarButtonItem bDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bSaveDesign;
        private DevExpress.XtraBars.BarButtonItem bLoadDesign;
        private DevExpress.Utils.ImageCollection image;
        private DevExpress.XtraBars.BarButtonItem bSave;
        private DevExpress.XtraBars.BarButtonItem bLoad;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reYear;
        private DevExpress.XtraGrid.GridControl gcMain;
        public DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn idRegion;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reRegion;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn idSegm;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reSegment;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn idbrend;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reBrand;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn Marga;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn naklRasx;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn vSale;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn vProfit;
        private DevExpress.XtraBars.BarButtonItem btnLoadExcelFile;
        private System.Windows.Forms.ComboBox beYearSale;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraWaitForm.ProgressPanel ppLoad;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.PanelControl pFilter;
        private DevExpress.XtraBars.BarButtonItem btnSaveDefault;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn type;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CalculatedVMD;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbK;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
    }
}