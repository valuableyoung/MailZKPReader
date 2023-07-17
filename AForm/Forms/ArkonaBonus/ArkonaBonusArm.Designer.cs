namespace AForm.Forms.ArkonaBonus
{
    partial class ArkonaBonusArm
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tCustomer = new AForm.Base.ATable();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCreateBonus = new System.Windows.Forms.Button();
            this.btnRecalcBonus = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.tBonusDetail = new AForm.Base.ATable();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.tBonus = new AForm.Base.ATable();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.tCustomer);
            this.groupControl1.Controls.Add(this.panel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(850, 659);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Покупатели в программе А+";
            // 
            // tCustomer
            // 
            this.tCustomer._ButtonAdd = false;
            this.tCustomer._ButtonDelete = false;
            this.tCustomer._ButtonDesign = false;
            this.tCustomer._ButtonFound = false;
            this.tCustomer._ButtonLoad = false;
            this.tCustomer._ButtonSave = false;
            this.tCustomer._HideMenu = true;
            this.tCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tCustomer.DataSource = null;
            this.tCustomer.FocusedRowNumber = -2147483648;
            this.tCustomer.FullName = null;
            this.tCustomer.Location = new System.Drawing.Point(2, 85);
            this.tCustomer.Name = "tCustomer";
            this.tCustomer.Size = new System.Drawing.Size(846, 572);
            this.tCustomer.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCreateBonus);
            this.panel1.Controls.Add(this.btnRecalcBonus);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.btnFilter);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnGroup);
            this.panel1.Controls.Add(this.btUpdate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtPicker);
            this.panel1.Location = new System.Drawing.Point(12, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(832, 56);
            this.panel1.TabIndex = 2;
            // 
            // btnCreateBonus
            // 
            this.btnCreateBonus.FlatAppearance.BorderSize = 0;
            this.btnCreateBonus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateBonus.ForeColor = System.Drawing.Color.Transparent;
            this.btnCreateBonus.Image = global::AForm.Properties.Resources.coins;
            this.btnCreateBonus.Location = new System.Drawing.Point(501, 8);
            this.btnCreateBonus.Name = "btnCreateBonus";
            this.btnCreateBonus.Size = new System.Drawing.Size(38, 39);
            this.btnCreateBonus.TabIndex = 58;
            this.btnCreateBonus.UseVisualStyleBackColor = true;
            this.btnCreateBonus.Click += new System.EventHandler(this.btnCreateBonus_Click);
            // 
            // btnRecalcBonus
            // 
            this.btnRecalcBonus.FlatAppearance.BorderSize = 0;
            this.btnRecalcBonus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecalcBonus.ForeColor = System.Drawing.Color.Transparent;
            this.btnRecalcBonus.Image = global::AForm.Properties.Resources.calculator;
            this.btnRecalcBonus.Location = new System.Drawing.Point(545, 8);
            this.btnRecalcBonus.Name = "btnRecalcBonus";
            this.btnRecalcBonus.Size = new System.Drawing.Size(38, 39);
            this.btnRecalcBonus.TabIndex = 57;
            this.btnRecalcBonus.UseVisualStyleBackColor = true;
            this.btnRecalcBonus.Visible = false;
            this.btnRecalcBonus.Click += new System.EventHandler(this.btnRecalcBonus_Click);
            // 
            // btnFind
            // 
            this.btnFind.FlatAppearance.BorderSize = 0;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.ForeColor = System.Drawing.Color.Transparent;
            this.btnFind.Image = global::AForm.Properties.Resources.magnifier;
            this.btnFind.Location = new System.Drawing.Point(373, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(38, 39);
            this.btnFind.TabIndex = 56;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Image = global::AForm.Properties.Resources.filter;
            this.btnFilter.Location = new System.Drawing.Point(329, 8);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(38, 39);
            this.btnFilter.TabIndex = 55;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.Transparent;
            this.btnSave.Image = global::AForm.Properties.Resources.disk;
            this.btnSave.Location = new System.Drawing.Point(417, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(38, 39);
            this.btnSave.TabIndex = 54;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.FlatAppearance.BorderSize = 0;
            this.btnGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroup.ForeColor = System.Drawing.Color.Transparent;
            this.btnGroup.Image = global::AForm.Properties.Resources.application_side_tree;
            this.btnGroup.Location = new System.Drawing.Point(285, 8);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(38, 39);
            this.btnGroup.TabIndex = 53;
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.FlatAppearance.BorderSize = 0;
            this.btUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUpdate.ForeColor = System.Drawing.Color.Transparent;
            this.btUpdate.Image = global::AForm.Properties.Resources.update;
            this.btUpdate.Location = new System.Drawing.Point(241, 8);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(38, 39);
            this.btUpdate.TabIndex = 52;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Данные с:";
            // 
            // dtPicker
            // 
            this.dtPicker.Enabled = false;
            this.dtPicker.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtPicker.Location = new System.Drawing.Point(82, 18);
            this.dtPicker.Margin = new System.Windows.Forms.Padding(25);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(148, 22);
            this.dtPicker.TabIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.groupControl4);
            this.groupControl2.Controls.Add(this.splitterControl2);
            this.groupControl2.Controls.Add(this.tBonus);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(855, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(382, 659);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Детализация по накладным";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.tBonusDetail);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl4.Location = new System.Drawing.Point(2, 325);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(378, 332);
            this.groupControl4.TabIndex = 3;
            this.groupControl4.Text = "Детализация по брендам для накладной";
            // 
            // tBonusDetail
            // 
            this.tBonusDetail._ButtonAdd = false;
            this.tBonusDetail._ButtonDelete = false;
            this.tBonusDetail._ButtonSave = false;
            this.tBonusDetail.DataSource = null;
            this.tBonusDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBonusDetail.FocusedRowNumber = -2147483648;
            this.tBonusDetail.FullName = null;
            this.tBonusDetail.Location = new System.Drawing.Point(2, 20);
            this.tBonusDetail.Name = "tBonusDetail";
            this.tBonusDetail.Size = new System.Drawing.Size(374, 310);
            this.tBonusDetail.TabIndex = 2;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl2.Location = new System.Drawing.Point(2, 320);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(378, 5);
            this.splitterControl2.TabIndex = 2;
            this.splitterControl2.TabStop = false;
            // 
            // tBonus
            // 
            this.tBonus._ButtonAdd = false;
            this.tBonus._ButtonDelete = false;
            this.tBonus._ButtonSave = false;
            this.tBonus.DataSource = null;
            this.tBonus.Dock = System.Windows.Forms.DockStyle.Top;
            this.tBonus.FocusedRowNumber = -2147483648;
            this.tBonus.FullName = null;
            this.tBonus.Location = new System.Drawing.Point(2, 20);
            this.tBonus.Name = "tBonus";
            this.tBonus.Size = new System.Drawing.Size(378, 300);
            this.tBonus.TabIndex = 1;
            // 
            // splitterControl3
            // 
            this.splitterControl3.Location = new System.Drawing.Point(850, 0);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(5, 659);
            this.splitterControl3.TabIndex = 2;
            this.splitterControl3.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel File 2003 - 2007 (*.xls)|*.xls|Excel File 2010 (*.xlsx)|*.xlsx|Pdf File (*." +
    "pdf)|*.pdf|Text File (*.txt)|*.txt";
            // 
            // ArkonaBonusArm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 659);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.splitterControl3);
            this.Controls.Add(this.groupControl1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ArkonaBonusArm";
            this.Text = "ArkonaBonusArm";
            this.Load += new System.EventHandler(this.ArkonaBonusArm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private Base.ATable tCustomer;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private Base.ATable tBonus;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private Base.ATable tBonusDetail;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnCreateBonus;
        private System.Windows.Forms.Button btnRecalcBonus;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}