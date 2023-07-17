namespace AForm.Forms.Old.Other
{
    partial class WFPerDebZad
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
            this.pMain = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.deMain = new DevExpress.XtraEditors.DateEdit();
            this.tMain = new AForm.Base.ATable();
            this.pMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deMain.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deMain.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.labelControl1);
            this.pMain.Controls.Add(this.deMain);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(994, 45);
            this.pMain.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(97, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Отчетная дата";
            // 
            // deMain
            // 
            this.deMain.EditValue = null;
            this.deMain.Location = new System.Drawing.Point(124, 12);
            this.deMain.Name = "deMain";
            this.deMain.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.deMain.Properties.Appearance.Options.UseFont = true;
            this.deMain.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deMain.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deMain.Size = new System.Drawing.Size(144, 24);
            this.deMain.TabIndex = 0;
            // 
            // tMain
            // 
            this.tMain._ButtonSaveAs = true;
            this.tMain.DataSource = null;
            this.tMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tMain.FocusedRowNumber = -2147483648;
            this.tMain.FullName = "WFPerDebZad_tMain";
            this.tMain.Location = new System.Drawing.Point(0, 45);
            this.tMain.Name = "tMain";
            this.tMain.Size = new System.Drawing.Size(994, 470);
            this.tMain.TabIndex = 0;
            // 
            // WFPerDebZad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 515);
            this.Controls.Add(this.tMain);
            this.Controls.Add(this.pMain);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "WFPerDebZad";
            this.Text = "Мониторинг дебиторской задолженности";
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deMain.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deMain.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Base.ATable tMain;
        private System.Windows.Forms.Panel pMain;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deMain;
    }
}