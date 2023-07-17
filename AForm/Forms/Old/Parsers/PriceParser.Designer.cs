namespace AForm.Forms.Old.Parsers
{
    partial class PriceParser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceParser));
            this.btOpenFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.cbFirm = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bLoad = new System.Windows.Forms.Button();
            this.dlgMain = new System.Windows.Forms.OpenFileDialog();
            this.lbText = new System.Windows.Forms.Label();
            this.lbLog = new System.Windows.Forms.Label();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSetting = new System.Windows.Forms.ComboBox();
            this.btAddSetting = new System.Windows.Forms.Button();
            this.btEditSetting = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.deDatePrice = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.deDatePrice.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDatePrice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btOpenFile
            // 
            this.btOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btOpenFile.Image")));
            this.btOpenFile.Location = new System.Drawing.Point(485, 32);
            this.btOpenFile.Name = "btOpenFile";
            this.btOpenFile.Size = new System.Drawing.Size(38, 37);
            this.btOpenFile.TabIndex = 9;
            this.btOpenFile.UseVisualStyleBackColor = true;
            this.btOpenFile.Click += new System.EventHandler(this.btOpenFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Файл:";
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(84, 39);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(395, 21);
            this.tbFile.TabIndex = 7;
            // 
            // cbFirm
            // 
            this.cbFirm.FormattingEnabled = true;
            this.cbFirm.Location = new System.Drawing.Point(84, 12);
            this.cbFirm.Name = "cbFirm";
            this.cbFirm.Size = new System.Drawing.Size(395, 21);
            this.cbFirm.TabIndex = 11;
            this.cbFirm.SelectedIndexChanged += new System.EventHandler(this.cbFirm_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Поставщик:";
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(404, 167);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(75, 23);
            this.bLoad.TabIndex = 14;
            this.bLoad.Text = "Загрузить";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // dlgMain
            // 
            this.dlgMain.FileName = "openFileDialog1";
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(23, 177);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(78, 13);
            this.lbText.TabIndex = 15;
            this.lbText.Text = "Подождите...";
            this.lbText.Visible = false;
            // 
            // lbLog
            // 
            this.lbLog.AutoSize = true;
            this.lbLog.Location = new System.Drawing.Point(107, 179);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(0, 13);
            this.lbLog.TabIndex = 16;
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(23, 202);
            this.pbMain.Maximum = 30;
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(456, 23);
            this.pbMain.TabIndex = 17;
            this.pbMain.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Настройка:";
            // 
            // cbSetting
            // 
            this.cbSetting.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSetting.FormattingEnabled = true;
            this.cbSetting.Location = new System.Drawing.Point(84, 83);
            this.cbSetting.Name = "cbSetting";
            this.cbSetting.Size = new System.Drawing.Size(395, 22);
            this.cbSetting.TabIndex = 19;
            this.cbSetting.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbSetting_DrawItem);
            this.cbSetting.SelectedIndexChanged += new System.EventHandler(this.cbSetting_SelectedIndexChanged);
            // 
            // btAddSetting
            // 
            this.btAddSetting.Image = ((System.Drawing.Image)(resources.GetObject("btAddSetting.Image")));
            this.btAddSetting.Location = new System.Drawing.Point(485, 75);
            this.btAddSetting.Name = "btAddSetting";
            this.btAddSetting.Size = new System.Drawing.Size(38, 39);
            this.btAddSetting.TabIndex = 20;
            this.btAddSetting.UseVisualStyleBackColor = true;
            this.btAddSetting.Click += new System.EventHandler(this.btAddSetting_Click);
            // 
            // btEditSetting
            // 
            this.btEditSetting.Image = ((System.Drawing.Image)(resources.GetObject("btEditSetting.Image")));
            this.btEditSetting.Location = new System.Drawing.Point(526, 74);
            this.btEditSetting.Name = "btEditSetting";
            this.btEditSetting.Size = new System.Drawing.Size(41, 40);
            this.btEditSetting.TabIndex = 21;
            this.btEditSetting.UseVisualStyleBackColor = true;
            this.btEditSetting.Click += new System.EventHandler(this.btEditSetting_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Настройка:";
            this.label4.Visible = false;
            // 
            // deDatePrice
            // 
            this.deDatePrice.EditValue = null;
            this.deDatePrice.Location = new System.Drawing.Point(84, 127);
            this.deDatePrice.Name = "deDatePrice";
            this.deDatePrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDatePrice.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDatePrice.Size = new System.Drawing.Size(395, 20);
            this.deDatePrice.TabIndex = 23;
            this.deDatePrice.Visible = false;
            // 
            // PriceParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 229);
            this.Controls.Add(this.deDatePrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btEditSetting);
            this.Controls.Add(this.btAddSetting);
            this.Controls.Add(this.cbSetting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.cbFirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btOpenFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFile);
            this.Name = "PriceParser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Парсинг прайсов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PriceParser_FormClosing);
            this.Load += new System.EventHandler(this.FHandWork_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deDatePrice.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDatePrice.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOpenFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.ComboBox cbFirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.OpenFileDialog dlgMain;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Label lbLog;
        private System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSetting;
        private System.Windows.Forms.Button btAddSetting;
        private System.Windows.Forms.Button btEditSetting;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.DateEdit deDatePrice;
    }
}