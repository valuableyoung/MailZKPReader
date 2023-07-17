namespace AForm.Base
{
    partial class ATable
    {
        /// <summary> 
        /// Требуется переменная конструктора.
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ATable));
            this.bar = new DevExpress.XtraBars.BarManager(this.components);
            this.barMain = new DevExpress.XtraBars.Bar();
            this.bAdd = new DevExpress.XtraBars.BarButtonItem();
            this.bEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bDel = new DevExpress.XtraBars.BarButtonItem();
            this.bSave = new DevExpress.XtraBars.BarButtonItem();
            this.bSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.bLoad = new DevExpress.XtraBars.BarButtonItem();
            this.bGroup = new DevExpress.XtraBars.BarButtonItem();
            this.bFilter = new DevExpress.XtraBars.BarButtonItem();
            this.bFound = new DevExpress.XtraBars.BarButtonItem();
            this.bDesign = new DevExpress.XtraBars.BarSubItem();
            this.bDesignSave = new DevExpress.XtraBars.BarButtonItem();
            this.bSaveDesignDeffault = new DevExpress.XtraBars.BarButtonItem();
            this.bDesignLoadDefault = new DevExpress.XtraBars.BarButtonItem();
            this.bDesignLoad = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.image = new DevExpress.Utils.ImageCollection(this.components);
            this.bAdata = new DevExpress.XtraBars.BarButtonItem();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvTable = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvBanded = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.sdMain = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBanded)).BeginInit();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMain});
            this.bar.DockControls.Add(this.barDockControlTop);
            this.bar.DockControls.Add(this.barDockControlBottom);
            this.bar.DockControls.Add(this.barDockControlLeft);
            this.bar.DockControls.Add(this.barDockControlRight);
            this.bar.Form = this;
            this.bar.Images = this.image;
            this.bar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bGroup,
            this.bFilter,
            this.bFound,
            this.bDesign,
            this.bDesignSave,
            this.bDesignLoadDefault,
            this.bDesignLoad,
            this.bSaveDesignDeffault,
            this.bAdd,
            this.bEdit,
            this.bDel,
            this.bLoad,
            this.bSave,
            this.bSaveAs,
            this.bAdata});
            this.bar.MaxItemId = 23;
            // 
            // barMain
            // 
            this.barMain.BarName = "mMain";
            this.barMain.DockCol = 0;
            this.barMain.DockRow = 0;
            this.barMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMain.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.bEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.bDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.bSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.bSaveAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.bLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.bGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.bFilter),
            new DevExpress.XtraBars.LinkPersistInfo(this.bFound),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bDesign, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barMain.OptionsBar.AllowQuickCustomization = false;
            this.barMain.OptionsBar.DrawBorder = false;
            this.barMain.OptionsBar.DrawDragBorder = false;
            this.barMain.OptionsBar.UseWholeRow = true;
            this.barMain.Text = "mMain";
            // 
            // bAdd
            // 
            this.bAdd.Caption = "Добавить";
            this.bAdd.Id = 16;
            this.bAdd.ImageOptions.ImageIndex = 2;
            this.bAdd.Name = "bAdd";
            this.bAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bAdd_ItemClick);
            // 
            // bEdit
            // 
            this.bEdit.Caption = "Редактировать";
            this.bEdit.Id = 17;
            this.bEdit.ImageOptions.ImageIndex = 55;
            this.bEdit.Name = "bEdit";
            this.bEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bEdit_ItemClick);
            // 
            // bDel
            // 
            this.bDel.Caption = "Удалить";
            this.bDel.Id = 18;
            this.bDel.ImageOptions.ImageIndex = 30;
            this.bDel.Name = "bDel";
            this.bDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bDel_ItemClick);
            // 
            // bSave
            // 
            this.bSave.Caption = "Сохранить";
            this.bSave.Id = 20;
            this.bSave.ImageOptions.ImageIndex = 31;
            this.bSave.Name = "bSave";
            this.bSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bSave_ItemClick);
            // 
            // bSaveAs
            // 
            this.bSaveAs.Caption = "Сохранить как...";
            this.bSaveAs.Id = 21;
            this.bSaveAs.ImageOptions.ImageIndex = 62;
            this.bSaveAs.Name = "bSaveAs";
            this.bSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bSaveAs_ItemClick);
            // 
            // bLoad
            // 
            this.bLoad.Caption = "Обновить";
            this.bLoad.GroupIndex = 2;
            this.bLoad.Id = 19;
            this.bLoad.ImageOptions.ImageIndex = 74;
            this.bLoad.Name = "bLoad";
            this.bLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bLoad_ItemClick);
            // 
            // bGroup
            // 
            this.bGroup.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.bGroup.Caption = "Панель группировки";
            this.bGroup.Id = 3;
            this.bGroup.ImageOptions.ImageIndex = 79;
            this.bGroup.Name = "bGroup";
            this.bGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bGroup_ItemClick);
            // 
            // bFilter
            // 
            this.bFilter.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.bFilter.Caption = "Строка фильтра";
            this.bFilter.Id = 4;
            this.bFilter.ImageOptions.ImageIndex = 36;
            this.bFilter.Name = "bFilter";
            this.bFilter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bFilter_ItemClick);
            // 
            // bFound
            // 
            this.bFound.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bFound.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.bFound.Caption = "Поиск";
            this.bFound.GroupIndex = 1;
            this.bFound.Id = 5;
            this.bFound.ImageOptions.ImageIndex = 51;
            this.bFound.Name = "bFound";
            this.bFound.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bFound_ItemClick);
            // 
            // bDesign
            // 
            this.bDesign.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bDesign.Id = 11;
            this.bDesign.ImageOptions.Image = global::AForm.Properties.Resources.dots;
            this.bDesign.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bDesignSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.bSaveDesignDeffault),
            new DevExpress.XtraBars.LinkPersistInfo(this.bDesignLoadDefault),
            new DevExpress.XtraBars.LinkPersistInfo(this.bDesignLoad)});
            this.bDesign.Name = "bDesign";
            // 
            // bDesignSave
            // 
            this.bDesignSave.Caption = "Сохранить дизайн";
            this.bDesignSave.Id = 12;
            this.bDesignSave.Name = "bDesignSave";
            this.bDesignSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bDesignSave_ItemClick);
            // 
            // bSaveDesignDeffault
            // 
            this.bSaveDesignDeffault.Caption = "Сохранить по умолчанию";
            this.bSaveDesignDeffault.Id = 15;
            this.bSaveDesignDeffault.Name = "bSaveDesignDeffault";
            this.bSaveDesignDeffault.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bSaveDesignDeffault_ItemClick);
            // 
            // bDesignLoadDefault
            // 
            this.bDesignLoadDefault.Caption = "Загрузить по умолчанию";
            this.bDesignLoadDefault.Id = 13;
            this.bDesignLoadDefault.Name = "bDesignLoadDefault";
            this.bDesignLoadDefault.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bDesignLoadDefault_ItemClick);
            // 
            // bDesignLoad
            // 
            this.bDesignLoad.Caption = "Загрузить пользвательский";
            this.bDesignLoad.Id = 14;
            this.bDesignLoad.Name = "bDesignLoad";
            this.bDesignLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bDesignLoad_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bar;
            this.barDockControlTop.Size = new System.Drawing.Size(593, 47);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 293);
            this.barDockControlBottom.Manager = this.bar;
            this.barDockControlBottom.Size = new System.Drawing.Size(593, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 47);
            this.barDockControlLeft.Manager = this.bar;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 246);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(593, 47);
            this.barDockControlRight.Manager = this.bar;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 246);
            // 
            // image
            // 
            this.image.ImageSize = new System.Drawing.Size(32, 32);
            this.image.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("image.ImageStream")));
            this.image.Images.SetKeyName(0, "accept.png");
            this.image.Images.SetKeyName(1, "acceptall.png");
            this.image.Images.SetKeyName(2, "add.png");
            this.image.Images.SetKeyName(3, "aol_messenger.png");
            this.image.Images.SetKeyName(4, "application.png");
            this.image.Images.SetKeyName(5, "application_add.png");
            this.image.Images.SetKeyName(6, "application_cascade.png");
            this.image.Images.SetKeyName(7, "application_delete.png");
            this.image.Images.SetKeyName(8, "application_double.png");
            this.image.Images.SetKeyName(9, "application_edit.png");
            this.image.Images.SetKeyName(10, "application_form_add.png");
            this.image.Images.SetKeyName(11, "application_form_magnify.png");
            this.image.Images.SetKeyName(12, "application_from_storage.png");
            this.image.Images.SetKeyName(13, "application_go.png");
            this.image.Images.SetKeyName(14, "billiard_marker.png");
            this.image.Images.SetKeyName(15, "box.png");
            this.image.Images.SetKeyName(16, "box_down.png");
            this.image.Images.SetKeyName(17, "box_open.png");
            this.image.Images.SetKeyName(18, "calculator.png");
            this.image.Images.SetKeyName(19, "calendar.png");
            this.image.Images.SetKeyName(20, "cancel.png");
            this.image.Images.SetKeyName(21, "card_money.png");
            this.image.Images.SetKeyName(22, "cart_delete.png");
            this.image.Images.SetKeyName(23, "cart_go.png");
            this.image.Images.SetKeyName(24, "cart_reload.png");
            this.image.Images.SetKeyName(25, "clock_add.png");
            this.image.Images.SetKeyName(26, "cog.png");
            this.image.Images.SetKeyName(27, "cog_go.png");
            this.image.Images.SetKeyName(28, "coins.png");
            this.image.Images.SetKeyName(29, "contact_email.png");
            this.image.Images.SetKeyName(30, "delete.png");
            this.image.Images.SetKeyName(31, "disk.png");
            this.image.Images.SetKeyName(32, "document_import.png");
            this.image.Images.SetKeyName(33, "document_move.png");
            this.image.Images.SetKeyName(34, "document_signature.png");
            this.image.Images.SetKeyName(35, "door_in.png");
            this.image.Images.SetKeyName(36, "filter.png");
            this.image.Images.SetKeyName(37, "filter_add.png");
            this.image.Images.SetKeyName(38, "filter_app.PNG");
            this.image.Images.SetKeyName(39, "filter_delete.png");
            this.image.Images.SetKeyName(40, "finance.png");
            this.image.Images.SetKeyName(41, "find.png");
            this.image.Images.SetKeyName(42, "flag_russia.png");
            this.image.Images.SetKeyName(43, "flag_yellow.png");
            this.image.Images.SetKeyName(44, "inbox_download.png");
            this.image.Images.SetKeyName(45, "information.png");
            this.image.Images.SetKeyName(46, "interface_preferences.png");
            this.image.Images.SetKeyName(47, "lightning_add.png");
            this.image.Images.SetKeyName(48, "lightning_delete.png");
            this.image.Images.SetKeyName(49, "livejournal.png");
            this.image.Images.SetKeyName(50, "lorry.png");
            this.image.Images.SetKeyName(51, "magnifier.png");
            this.image.Images.SetKeyName(52, "no_requirements.png");
            this.image.Images.SetKeyName(53, "page_copy.png");
            this.image.Images.SetKeyName(54, "page_delete.png");
            this.image.Images.SetKeyName(55, "pencil.png");
            this.image.Images.SetKeyName(56, "pencil_add.png");
            this.image.Images.SetKeyName(57, "pencil_delete.png");
            this.image.Images.SetKeyName(58, "price_comparison.png");
            this.image.Images.SetKeyName(59, "printer.png");
            this.image.Images.SetKeyName(60, "prohibition_button.png");
            this.image.Images.SetKeyName(61, "radioactivity.png");
            this.image.Images.SetKeyName(62, "save_as.png");
            this.image.Images.SetKeyName(63, "save_exit.png");
            this.image.Images.SetKeyName(64, "save_money.png");
            this.image.Images.SetKeyName(65, "sort_ascending.png");
            this.image.Images.SetKeyName(66, "sort_columns.png");
            this.image.Images.SetKeyName(67, "stamp_pattern.png");
            this.image.Images.SetKeyName(68, "stop.png");
            this.image.Images.SetKeyName(69, "table_refresh.png");
            this.image.Images.SetKeyName(70, "table_save.png");
            this.image.Images.SetKeyName(71, "tick_ok.png");
            this.image.Images.SetKeyName(72, "to_do_list.png");
            this.image.Images.SetKeyName(73, "to_do_list_cheked_all.png");
            this.image.Images.SetKeyName(74, "update.png");
            this.image.Images.SetKeyName(75, "user_delete.png");
            this.image.Images.SetKeyName(76, "user_green.png");
            this.image.Images.SetKeyName(77, "group_box.png");
            this.image.Images.SetKeyName(78, "grid.png");
            this.image.Images.SetKeyName(79, "application_side_tree.png");
            this.image.Images.SetKeyName(80, "database_yellow.png");
            // 
            // bAdata
            // 
            this.bAdata.Caption = "Связь с БД";
            this.bAdata.Id = 22;
            this.bAdata.ImageOptions.ImageIndex = 80;
            this.bAdata.Name = "bAdata";
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 47);
            this.gcMain.MainView = this.gvTable;
            this.gcMain.MenuManager = this.bar;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(593, 246);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTable,
            this.gvBanded});
            this.gcMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gcMain_KeyPress);
            // 
            // gvTable
            // 
            this.gvTable.GridControl = this.gcMain;
            this.gvTable.Name = "gvTable";
            this.gvTable.OptionsView.RowAutoHeight = true;
            this.gvTable.OptionsView.ShowFooter = true;
            this.gvTable.OptionsView.ShowGroupPanel = false;
            this.gvTable.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvTable_FocusedRowChanged);
            this.gvTable.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvTable_FocusedColumnChanged);
            this.gvTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvTable_KeyDown);
            this.gvTable.DoubleClick += new System.EventHandler(this.gvTable_DoubleClick);
            // 
            // gvBanded
            // 
            this.gvBanded.GridControl = this.gcMain;
            this.gvBanded.Name = "gvBanded";
            this.gvBanded.OptionsView.ShowFooter = true;
            this.gvBanded.OptionsView.ShowGroupPanel = false;
            this.gvBanded.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvBanded_KeyDown);
            // 
            // sdMain
            // 
            this.sdMain.Filter = "XLS|*.xls|PDF|*.pdf|XLSX|*.xlsx";
            // 
            // ATable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ATable";
            this.Size = new System.Drawing.Size(593, 293);
            this.Load += new System.EventHandler(this.ATable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBanded)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager bar;
        private DevExpress.XtraBars.Bar barMain;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.Utils.ImageCollection image;
        private DevExpress.XtraBars.BarButtonItem bGroup;
        private DevExpress.XtraBars.BarButtonItem bFilter;
        private DevExpress.XtraBars.BarButtonItem bFound;
        private DevExpress.XtraBars.BarSubItem bDesign;
        private DevExpress.XtraBars.BarButtonItem bDesignSave;
        private DevExpress.XtraBars.BarButtonItem bDesignLoadDefault;
        private DevExpress.XtraBars.BarButtonItem bDesignLoad;
        private DevExpress.XtraBars.BarButtonItem bSaveDesignDeffault;
        private DevExpress.XtraBars.BarButtonItem bAdd;
        private DevExpress.XtraBars.BarButtonItem bEdit;
        private DevExpress.XtraBars.BarButtonItem bDel;
        private DevExpress.XtraBars.BarButtonItem bLoad;
        private DevExpress.XtraBars.BarButtonItem bSave;
        private DevExpress.XtraBars.BarButtonItem bSaveAs;
        private System.Windows.Forms.SaveFileDialog sdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTable;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvBanded;
        private DevExpress.XtraBars.BarButtonItem bAdata;
    }
}
