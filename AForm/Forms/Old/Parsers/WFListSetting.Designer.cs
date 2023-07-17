namespace AForm.Forms.Old.Parsers
{
    partial class WFListSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFListSetting));
            this.bm = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bAdd = new DevExpress.XtraBars.BarButtonItem();
            this.bEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.image = new DevExpress.Utils.ImageCollection(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idParserSettings = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nParserSettings = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fPriceOnline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fArmRtk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fPriceCompetitor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fCorrectCoeff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fAutoload = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
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
            this.bDelete});
            this.bm.MainMenu = this.bar2;
            this.bm.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Главное меню";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.bEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.bDelete)});
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
            this.bAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bAdd_ItemClick);
            // 
            // bEdit
            // 
            this.bEdit.Caption = "Редактировать";
            this.bEdit.Id = 1;
            this.bEdit.ImageOptions.ImageIndex = 2;
            this.bEdit.Name = "bEdit";
            this.bEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bEdit_ItemClick);
            // 
            // bDelete
            // 
            this.bDelete.Caption = "Удалить";
            this.bDelete.Id = 2;
            this.bDelete.ImageOptions.ImageIndex = 0;
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(50, 50);
            this.bDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bm;
            this.barDockControlTop.Size = new System.Drawing.Size(887, 50);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 525);
            this.barDockControlBottom.Manager = this.bm;
            this.barDockControlBottom.Size = new System.Drawing.Size(887, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 50);
            this.barDockControlLeft.Manager = this.bm;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 475);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(887, 50);
            this.barDockControlRight.Manager = this.bm;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 475);
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
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 50);
            this.gcMain.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gcMain.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Silver;
            this.gcMain.LookAndFeel.UseWindowsXPTheme = true;
            this.gcMain.MainView = this.gvMain;
            this.gcMain.MenuManager = this.bm;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(887, 475);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idParserSettings,
            this.nParserSettings,
            this.nSupplier,
            this.nSource,
            this.fPriceOnline,
            this.fArmRtk,
            this.fPriceCompetitor,
            this.fCorrectCoeff,
            this.fAutoload});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            // 
            // idParserSettings
            // 
            this.idParserSettings.Caption = "Код";
            this.idParserSettings.FieldName = "idParserSettings";
            this.idParserSettings.Name = "idParserSettings";
            this.idParserSettings.Visible = true;
            this.idParserSettings.VisibleIndex = 0;
            // 
            // nParserSettings
            // 
            this.nParserSettings.Caption = "Наименование";
            this.nParserSettings.FieldName = "nParserSettings";
            this.nParserSettings.Name = "nParserSettings";
            this.nParserSettings.Visible = true;
            this.nParserSettings.VisibleIndex = 1;
            // 
            // nSupplier
            // 
            this.nSupplier.Caption = "Поставщик";
            this.nSupplier.FieldName = "nSupplier";
            this.nSupplier.Name = "nSupplier";
            this.nSupplier.Visible = true;
            this.nSupplier.VisibleIndex = 2;
            // 
            // nSource
            // 
            this.nSource.Caption = "Источник";
            this.nSource.FieldName = "nSource";
            this.nSource.Name = "nSource";
            this.nSource.Visible = true;
            this.nSource.VisibleIndex = 3;
            // 
            // fPriceOnline
            // 
            this.fPriceOnline.Caption = "Заказной товар";
            this.fPriceOnline.FieldName = "fPriceOnline";
            this.fPriceOnline.Name = "fPriceOnline";
            this.fPriceOnline.Visible = true;
            this.fPriceOnline.VisibleIndex = 4;
            // 
            // fArmRtk
            // 
            this.fArmRtk.Caption = "АРМ РТК";
            this.fArmRtk.FieldName = "fArmRtk";
            this.fArmRtk.Name = "fArmRtk";
            this.fArmRtk.Visible = true;
            this.fArmRtk.VisibleIndex = 5;
            // 
            // fPriceCompetitor
            // 
            this.fPriceCompetitor.Caption = "Ценообразование";
            this.fPriceCompetitor.FieldName = "fPriceCompetitor";
            this.fPriceCompetitor.Name = "fPriceCompetitor";
            this.fPriceCompetitor.Visible = true;
            this.fPriceCompetitor.VisibleIndex = 6;
            // 
            // fCorrectCoeff
            // 
            this.fCorrectCoeff.Caption = "Корректировочный";
            this.fCorrectCoeff.FieldName = "fCorrectCoeff";
            this.fCorrectCoeff.Name = "fCorrectCoeff";
            this.fCorrectCoeff.Visible = true;
            this.fCorrectCoeff.VisibleIndex = 7;
            // 
            // fAutoload
            // 
            this.fAutoload.Caption = "Автозагрузка";
            this.fAutoload.FieldName = "fAutoload";
            this.fAutoload.Name = "fAutoload";
            this.fAutoload.Visible = true;
            this.fAutoload.VisibleIndex = 8;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = ((long)(1));
            this.repositoryItemCheckEdit1.ValueUnchecked = ((long)(0));
            // 
            // WFListSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 525);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "WFListSetting";
            this.Text = "Настройки парсинга";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFListSetting_FormClosing);
            this.Load += new System.EventHandler(this.WFListSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager bm;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bAdd;
        private DevExpress.XtraBars.BarButtonItem bEdit;
        private DevExpress.XtraBars.BarButtonItem bDelete;
        private DevExpress.Utils.ImageCollection image;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn idParserSettings;
        private DevExpress.XtraGrid.Columns.GridColumn nParserSettings;
        private DevExpress.XtraGrid.Columns.GridColumn nSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn nSource;
        private DevExpress.XtraGrid.Columns.GridColumn fPriceOnline;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn fArmRtk;
        private DevExpress.XtraGrid.Columns.GridColumn fPriceCompetitor;
        private DevExpress.XtraGrid.Columns.GridColumn fCorrectCoeff;
        private DevExpress.XtraGrid.Columns.GridColumn fAutoload;

    }
}