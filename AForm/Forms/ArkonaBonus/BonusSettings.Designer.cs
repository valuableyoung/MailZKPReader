namespace AForm.Forms.ArkonaBonus
{
    partial class BonusSettings
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridMain = new AForm.Base.ATable();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnRetrive = new DevExpress.XtraEditors.SimpleButton();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.cbBrand = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSupplier = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridBonus = new AForm.Base.ATable();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gridAsku = new AForm.Base.ATable();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridMinBrand = new AForm.Base.ATable();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridMain);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(0, 297);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1302, 474);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Таблица цены без бонусов и МинБренда";
            // 
            // gridMain
            // 
            this.gridMain._ButtonAdd = false;
            this.gridMain._ButtonDelete = false;
            this.gridMain._ButtonLoad = false;
            this.gridMain._ButtonSave = false;
            this.gridMain._ButtonSaveAs = true;
            this.gridMain.DataSource = null;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.FocusedRowNumber = -2147483648;
            this.gridMain.FullName = null;
            this.gridMain.Location = new System.Drawing.Point(3, 18);
            this.gridMain.Name = "gridMain";
            this.gridMain.Size = new System.Drawing.Size(1296, 453);
            this.gridMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnRetrive);
            this.panel1.Controls.Add(this.btnAccept);
            this.panel1.Controls.Add(this.cbBrand);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbSupplier);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1302, 61);
            this.panel1.TabIndex = 13;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Location = new System.Drawing.Point(1168, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(122, 45);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Выход";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.ImageOptions.Image = global::AForm.Properties.Resources.disk;
            this.btnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSave.Location = new System.Drawing.Point(576, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 45);
            this.btnSave.TabIndex = 8;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClear.ImageOptions.Image = global::AForm.Properties.Resources.Cancel_32x32;
            this.btnClear.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClear.Location = new System.Drawing.Point(522, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(48, 45);
            this.btnClear.TabIndex = 7;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRetrive
            // 
            this.btnRetrive.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRetrive.Appearance.Options.UseFont = true;
            this.btnRetrive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRetrive.ImageOptions.Image = global::AForm.Properties.Resources.update;
            this.btnRetrive.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRetrive.Location = new System.Drawing.Point(470, 8);
            this.btnRetrive.Name = "btnRetrive";
            this.btnRetrive.Size = new System.Drawing.Size(46, 45);
            this.btnRetrive.TabIndex = 6;
            this.btnRetrive.Click += new System.EventHandler(this.btnRetrive_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAccept.Appearance.Options.UseFont = true;
            this.btnAccept.ImageOptions.Image = global::AForm.Properties.Resources.accept;
            this.btnAccept.Location = new System.Drawing.Point(655, 8);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(122, 45);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "ПРИНЯТЬ";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // cbBrand
            // 
            this.cbBrand.FormattingEnabled = true;
            this.cbBrand.Location = new System.Drawing.Point(99, 33);
            this.cbBrand.Name = "cbBrand";
            this.cbBrand.Size = new System.Drawing.Size(361, 24);
            this.cbBrand.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(44, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Бренд";
            // 
            // cbSupplier
            // 
            this.cbSupplier.FormattingEnabled = true;
            this.cbSupplier.Location = new System.Drawing.Point(99, 6);
            this.cbSupplier.Name = "cbSupplier";
            this.cbSupplier.Size = new System.Drawing.Size(361, 24);
            this.cbSupplier.TabIndex = 1;
            this.cbSupplier.SelectedIndexChanged += new System.EventHandler(this.cbSupplier_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Поставщик";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridBonus);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 231);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Процент по бренду";
            // 
            // gridBonus
            // 
            this.gridBonus._ButtonFound = false;
            this.gridBonus._ButtonLoad = false;
            this.gridBonus._ButtonSave = false;
            this.gridBonus.DataSource = null;
            this.gridBonus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBonus.FocusedRowNumber = -2147483648;
            this.gridBonus.FullName = null;
            this.gridBonus.Location = new System.Drawing.Point(3, 18);
            this.gridBonus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridBonus.Name = "gridBonus";
            this.gridBonus.Size = new System.Drawing.Size(379, 210);
            this.gridBonus.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gridAsku);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(814, 61);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(488, 231);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Список ASKU";
            // 
            // gridAsku
            // 
            this.gridAsku._ButtonAdd = false;
            this.gridAsku._ButtonDelete = false;
            this.gridAsku._ButtonFound = false;
            this.gridAsku._ButtonLoad = false;
            this.gridAsku._ButtonSave = false;
            this.gridAsku.DataSource = null;
            this.gridAsku.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAsku.FocusedRowNumber = -2147483648;
            this.gridAsku.FullName = null;
            this.gridAsku.Location = new System.Drawing.Point(3, 18);
            this.gridAsku.Name = "gridAsku";
            this.gridAsku.Size = new System.Drawing.Size(482, 210);
            this.gridAsku.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridMinBrand);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(390, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 231);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Процент МинБренда";
            // 
            // gridMinBrand
            // 
            this.gridMinBrand._ButtonAdd = false;
            this.gridMinBrand._ButtonDelete = false;
            this.gridMinBrand._ButtonFound = false;
            this.gridMinBrand._ButtonLoad = false;
            this.gridMinBrand._ButtonSave = false;
            this.gridMinBrand.DataSource = null;
            this.gridMinBrand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMinBrand.FocusedRowNumber = -2147483648;
            this.gridMinBrand.FullName = null;
            this.gridMinBrand.Location = new System.Drawing.Point(3, 18);
            this.gridMinBrand.Margin = new System.Windows.Forms.Padding(4);
            this.gridMinBrand.Name = "gridMinBrand";
            this.gridMinBrand.Size = new System.Drawing.Size(413, 210);
            this.gridMinBrand.TabIndex = 0;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 292);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(1302, 5);
            this.splitterControl1.TabIndex = 20;
            this.splitterControl1.TabStop = false;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Location = new System.Drawing.Point(385, 61);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(5, 231);
            this.splitterControl2.TabIndex = 21;
            this.splitterControl2.TabStop = false;
            // 
            // splitterControl3
            // 
            this.splitterControl3.Location = new System.Drawing.Point(809, 61);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(5, 231);
            this.splitterControl3.TabIndex = 22;
            this.splitterControl3.TabStop = false;
            // 
            // BonusSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 771);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.splitterControl3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.splitterControl2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "BonusSettings";
            this.Tag = "BonusSettings";
            this.Text = "BonusSettings";
            this.Load += new System.EventHandler(this.BonusSettings_Load);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private Base.ATable gridMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbBrand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSupplier;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private System.Windows.Forms.GroupBox groupBox1;
        private Base.ATable gridBonus;
        private DevExpress.XtraEditors.SimpleButton btnRetrive;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private System.Windows.Forms.GroupBox groupBox4;
        private Base.ATable gridAsku;
        private System.Windows.Forms.GroupBox groupBox2;
        private Base.ATable gridMinBrand;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraEditors.SimpleButton btnExit;
    }
}