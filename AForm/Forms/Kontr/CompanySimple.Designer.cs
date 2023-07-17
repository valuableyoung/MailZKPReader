namespace AForm.Forms.Kontr
{
    partial class CompanySimple
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
            this.bOK = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this._dlc = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.idCompanyTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForidCompany = new DevExpress.XtraLayout.LayoutControlItem();
            this.nCompanyTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemFornCompany = new DevExpress.XtraLayout.LayoutControlItem();
            this.nCityTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemFornCity = new DevExpress.XtraLayout.LayoutControlItem();
            this.companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dlc)).BeginInit();
            this._dlc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idCompanyTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForidCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCompanyTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemFornCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCityTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemFornCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(381, 115);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 24);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // bClose
            // 
            this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bClose.Location = new System.Drawing.Point(472, 115);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 24);
            this.bClose.TabIndex = 5;
            this.bClose.Text = "Отмена";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this._dlc);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(560, 104);
            this.panelControl1.TabIndex = 6;
            // 
            // _dlc
            // 
            this._dlc.Controls.Add(this.idCompanyTextEdit);
            this._dlc.Controls.Add(this.nCompanyTextEdit);
            this._dlc.Controls.Add(this.nCityTextEdit);
            this._dlc.DataSource = this.companyBindingSource;
            this._dlc.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dlc.Location = new System.Drawing.Point(2, 2);
            this._dlc.Name = "_dlc";
            this._dlc.Root = this.layoutControlGroup1;
            this._dlc.Size = new System.Drawing.Size(556, 100);
            this._dlc.TabIndex = 0;
            this._dlc.Text = "dataLayoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(556, 100);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForidCompany,
            this.ItemFornCompany,
            this.ItemFornCity});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(536, 80);
            // 
            // idCompanyTextEdit
            // 
            this.idCompanyTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.companyBindingSource, "idCompany", true));
            this.idCompanyTextEdit.Location = new System.Drawing.Point(89, 12);
            this.idCompanyTextEdit.Name = "idCompanyTextEdit";
            this.idCompanyTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.idCompanyTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.idCompanyTextEdit.Properties.Mask.EditMask = "N0";
            this.idCompanyTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.idCompanyTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.idCompanyTextEdit.Size = new System.Drawing.Size(455, 20);
            this.idCompanyTextEdit.StyleController = this._dlc;
            this.idCompanyTextEdit.TabIndex = 4;
            // 
            // ItemForidCompany
            // 
            this.ItemForidCompany.Control = this.idCompanyTextEdit;
            this.ItemForidCompany.Location = new System.Drawing.Point(0, 0);
            this.ItemForidCompany.Name = "ItemForidCompany";
            this.ItemForidCompany.Size = new System.Drawing.Size(536, 24);
            this.ItemForidCompany.Text = "Код";
            this.ItemForidCompany.TextSize = new System.Drawing.Size(73, 13);
            // 
            // nCompanyTextEdit
            // 
            this.nCompanyTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.companyBindingSource, "nCompany", true));
            this.nCompanyTextEdit.Location = new System.Drawing.Point(89, 36);
            this.nCompanyTextEdit.Name = "nCompanyTextEdit";
            this.nCompanyTextEdit.Size = new System.Drawing.Size(455, 20);
            this.nCompanyTextEdit.StyleController = this._dlc;
            this.nCompanyTextEdit.TabIndex = 5;
            // 
            // ItemFornCompany
            // 
            this.ItemFornCompany.Control = this.nCompanyTextEdit;
            this.ItemFornCompany.Location = new System.Drawing.Point(0, 24);
            this.ItemFornCompany.Name = "ItemFornCompany";
            this.ItemFornCompany.Size = new System.Drawing.Size(536, 24);
            this.ItemFornCompany.Text = "Наименование";
            this.ItemFornCompany.TextSize = new System.Drawing.Size(73, 13);
            // 
            // nCityTextEdit
            // 
            this.nCityTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.companyBindingSource, "nCity", true));
            this.nCityTextEdit.Location = new System.Drawing.Point(89, 60);
            this.nCityTextEdit.Name = "nCityTextEdit";
            this.nCityTextEdit.Size = new System.Drawing.Size(455, 20);
            this.nCityTextEdit.StyleController = this._dlc;
            this.nCityTextEdit.TabIndex = 6;
            // 
            // ItemFornCity
            // 
            this.ItemFornCity.Control = this.nCityTextEdit;
            this.ItemFornCity.Location = new System.Drawing.Point(0, 48);
            this.ItemFornCity.Name = "ItemFornCity";
            this.ItemFornCity.Size = new System.Drawing.Size(536, 32);
            this.ItemFornCity.Text = "Город";
            this.ItemFornCity.TextSize = new System.Drawing.Size(73, 13);
            // 
            // companyBindingSource
            // 
            this.companyBindingSource.DataSource = typeof(ALogic.Model.EntityFrame.Company);
            // 
            // CompanySimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 151);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bOK);
            this.Name = "CompanySimple";
            this.Text = "Введите имя компании";
            this.Load += new System.EventHandler(this.CompanySimple_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dlc)).EndInit();
            this._dlc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idCompanyTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForidCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCompanyTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemFornCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCityTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemFornCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bClose;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraDataLayout.DataLayoutControl _dlc;
        private DevExpress.XtraEditors.TextEdit idCompanyTextEdit;
        private System.Windows.Forms.BindingSource companyBindingSource;
        private DevExpress.XtraEditors.TextEdit nCompanyTextEdit;
        private DevExpress.XtraEditors.TextEdit nCityTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForidCompany;
        private DevExpress.XtraLayout.LayoutControlItem ItemFornCompany;
        private DevExpress.XtraLayout.LayoutControlItem ItemFornCity;
    }
}