namespace AForm.Forms.Reload1C
{
    partial class Reload1CList
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
            this.leTypeDoc = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.leFirm = new DevExpress.XtraEditors.LookUpEdit();
            this.fFin = new System.Windows.Forms.CheckBox();
            this.fTov = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deEnd = new System.Windows.Forms.DateTimePicker();
            this.deStart = new System.Windows.Forms.DateTimePicker();
            this.tMain = new AForm.Base.ATable();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.bdClose = new System.Windows.Forms.Button();
            this.deDateClose = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.leTypeDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leFirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDateClose.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateClose.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // leTypeDoc
            // 
            this.leTypeDoc.Location = new System.Drawing.Point(590, 44);
            this.leTypeDoc.Name = "leTypeDoc";
            this.leTypeDoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leTypeDoc.Size = new System.Drawing.Size(216, 20);
            this.leTypeDoc.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(484, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Тип документа";
            // 
            // leFirm
            // 
            this.leFirm.Location = new System.Drawing.Point(590, 16);
            this.leFirm.Name = "leFirm";
            this.leFirm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leFirm.Size = new System.Drawing.Size(216, 20);
            this.leFirm.TabIndex = 7;
            // 
            // fFin
            // 
            this.fFin.AutoSize = true;
            this.fFin.Location = new System.Drawing.Point(304, 43);
            this.fFin.Name = "fFin";
            this.fFin.Size = new System.Drawing.Size(149, 17);
            this.fFin.TabIndex = 6;
            this.fFin.Text = "Финансовые документы";
            this.fFin.UseVisualStyleBackColor = true;
            // 
            // fTov
            // 
            this.fTov.AutoSize = true;
            this.fTov.Location = new System.Drawing.Point(304, 18);
            this.fTov.Name = "fTov";
            this.fTov.Size = new System.Drawing.Size(136, 17);
            this.fTov.TabIndex = 5;
            this.fTov.Text = "Товарные документы";
            this.fTov.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Фирма";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ПО";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "C";
            // 
            // deEnd
            // 
            this.deEnd.Location = new System.Drawing.Point(71, 43);
            this.deEnd.Name = "deEnd";
            this.deEnd.Size = new System.Drawing.Size(216, 21);
            this.deEnd.TabIndex = 1;
            // 
            // deStart
            // 
            this.deStart.Location = new System.Drawing.Point(71, 15);
            this.deStart.Name = "deStart";
            this.deStart.Size = new System.Drawing.Size(216, 21);
            this.deStart.TabIndex = 0;
            this.deStart.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // tMain
            // 
            this.tMain._ButtonAdd = false;
            this.tMain._ButtonDelete = false;
            this.tMain._ButtonSave = false;
            this.tMain.DataSource = null;
            this.tMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tMain.FocusedRowNumber = -2147483648;
            this.tMain.FullName = null;
            this.tMain.Location = new System.Drawing.Point(0, 78);
            this.tMain.Name = "tMain";
            this.tMain.Size = new System.Drawing.Size(1276, 485);
            this.tMain.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.leTypeDoc);
            this.panelControl1.Controls.Add(this.deStart);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.deEnd);
            this.panelControl1.Controls.Add(this.leFirm);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.fFin);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.fTov);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1276, 78);
            this.panelControl1.TabIndex = 10;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.bdClose);
            this.groupControl1.Controls.Add(this.deDateClose);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(828, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(446, 74);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Запрет на выгрузку более ранних документов";
            // 
            // bdClose
            // 
            this.bdClose.Location = new System.Drawing.Point(264, 26);
            this.bdClose.Name = "bdClose";
            this.bdClose.Size = new System.Drawing.Size(117, 33);
            this.bdClose.TabIndex = 2;
            this.bdClose.Text = "Запретить";
            this.bdClose.UseVisualStyleBackColor = true;
            this.bdClose.Click += new System.EventHandler(this.bdClose_Click);
            // 
            // deDateClose
            // 
            this.deDateClose.EditValue = null;
            this.deDateClose.Location = new System.Drawing.Point(65, 33);
            this.deDateClose.Name = "deDateClose";
            this.deDateClose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateClose.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateClose.Size = new System.Drawing.Size(177, 20);
            this.deDateClose.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Дата";
            // 
            // Reload1CList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 563);
            this.Controls.Add(this.tMain);
            this.Controls.Add(this.panelControl1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Reload1CList";
            this.Text = "Reload1CList";
            ((System.ComponentModel.ISupportInitialize)(this.leTypeDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leFirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDateClose.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateClose.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker deEnd;
        private System.Windows.Forms.DateTimePicker deStart;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LookUpEdit leFirm;
        private System.Windows.Forms.CheckBox fFin;
        private System.Windows.Forms.CheckBox fTov;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LookUpEdit leTypeDoc;
        private System.Windows.Forms.Label label4;
        private Base.ATable tMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Button bdClose;
        private DevExpress.XtraEditors.DateEdit deDateClose;
    }
}