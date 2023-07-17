namespace AForm.Forms.Old.Other
{
    partial class WFRepNaklHabCompl
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
            this.dts = new System.Windows.Forms.DateTimePicker();
            this.dtpo = new System.Windows.Forms.DateTimePicker();
            this.btLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcMain = new AForm.Base.ATable();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dts
            // 
            this.dts.Location = new System.Drawing.Point(32, 12);
            this.dts.Name = "dts";
            this.dts.Size = new System.Drawing.Size(130, 20);
            this.dts.TabIndex = 1;
            // 
            // dtpo
            // 
            this.dtpo.Location = new System.Drawing.Point(195, 12);
            this.dtpo.Name = "dtpo";
            this.dtpo.Size = new System.Drawing.Size(130, 20);
            this.dtpo.TabIndex = 2;
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(331, 12);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(93, 23);
            this.btLoad.TabIndex = 3;
            this.btLoad.Text = "Обновить";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "C";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "По";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dts);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btLoad);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(923, 42);
            this.panel1.TabIndex = 6;
            // 
            // gcMain
            // 
           
            this.gcMain._ButtonAdd = false;
            this.gcMain._ButtonDelete = false;
            this.gcMain._ButtonSave = false;
            this.gcMain._ButtonSaveAs = true;            
            this.gcMain.DataSource = null;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.FocusedRowNumber = -2147483648;
            this.gcMain.FullName = null;
            this.gcMain.Location = new System.Drawing.Point(0, 42);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(923, 500);
            this.gcMain.TabIndex = 7;
         //   this.gcMain.Load += new System.EventHandler(this.gcMain_Load);
            // 
            // WFRepNaklHabCompl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 542);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Name = "WFRepNaklHabCompl";
            this.Text = "Отчет";
           // this.Load += new System.EventHandler(this.WFRepNaklHabCompl_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dts;
        private System.Windows.Forms.DateTimePicker dtpo;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private Base.ATable gcMain;
    }
}