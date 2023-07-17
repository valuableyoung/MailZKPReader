namespace AForm.Forms.Kontr
{
    partial class CompanyList
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.tCompany = new AForm.Base.ATable();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.tUnit = new AForm.Base.ATable();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tStructure = new AForm.Base.ATable();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1044, 48);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 18.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(460, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 29);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Фильтры";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.tCompany);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl2.Location = new System.Drawing.Point(0, 48);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(503, 373);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Список компаний";
            // 
            // tCompany
            // 
            this.tCompany._ButtonEdit = true;
            this.tCompany._ButtonSave = false;
            this.tCompany.DataSource = null;
            this.tCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tCompany.FocusedRowNumber = -2147483648;
            this.tCompany.FullName = null;
            this.tCompany.Location = new System.Drawing.Point(2, 20);
            this.tCompany.Name = "tCompany";
            this.tCompany.Size = new System.Drawing.Size(499, 351);
            this.tCompany.TabIndex = 0;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.tUnit);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl3.Location = new System.Drawing.Point(0, 426);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1044, 258);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "Нераспределенные подразделения";
            // 
            // tUnit
            // 
            this.tUnit._ButtonDelete = false;
            this.tUnit._ButtonEdit = true;
            this.tUnit._ButtonLoad = false;
            this.tUnit._ButtonSave = false;
            this.tUnit.DataSource = null;
            this.tUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tUnit.FocusedRowNumber = -2147483648;
            this.tUnit.FullName = null;
            this.tUnit.Location = new System.Drawing.Point(2, 20);
            this.tUnit.Name = "tUnit";
            this.tUnit.Size = new System.Drawing.Size(1040, 236);
            this.tUnit.TabIndex = 1;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 421);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(1044, 5);
            this.splitterControl1.TabIndex = 3;
            this.splitterControl1.TabStop = false;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Location = new System.Drawing.Point(503, 48);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(5, 373);
            this.splitterControl2.TabIndex = 4;
            this.splitterControl2.TabStop = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.tStructure);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(508, 48);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(536, 373);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Структура компании";
            // 
            // tStructure
            // 
            this.tStructure._ButtonDelete = false;
            this.tStructure._ButtonEdit = true;
            this.tStructure._ButtonLoad = false;
            this.tStructure._ButtonSave = false;
            this.tStructure.DataSource = null;
            this.tStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tStructure.FocusedRowNumber = -2147483648;
            this.tStructure.FullName = null;
            this.tStructure.Location = new System.Drawing.Point(2, 20);
            this.tStructure.Name = "tStructure";
            this.tStructure.Size = new System.Drawing.Size(532, 351);
            this.tStructure.TabIndex = 1;
            // 
            // CompanyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 684);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.splitterControl2);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.panelControl1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CompanyList";
            this.Text = "Список компаний";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private Base.ATable tCompany;
        private Base.ATable tUnit;
        private Base.ATable tStructure;
    }
}