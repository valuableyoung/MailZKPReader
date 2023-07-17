namespace AForm.Forms.Old.Other
{
    partial class WFPerKredZad
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
            this.tblMain = new AForm.Base.ATable();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain._ButtonAdd = false;
            this.tblMain._ButtonDelete = false;
            this.tblMain._ButtonSave = false;
            this.tblMain._ButtonSaveAs = true;
            this.tblMain.DataSource = null;
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.FocusedRowNumber = -2147483648;
            this.tblMain.FullName = "WFPerKredZad_tblMain";
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.Size = new System.Drawing.Size(816, 504);
            this.tblMain.TabIndex = 25;
            // 
            // WFPerKredZad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 504);
            this.Controls.Add(this.tblMain);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "WFPerKredZad";
            this.Text = "Мониторинг кредиторской задолженности";
            this.Controls.SetChildIndex(this.tblMain, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Base.ATable tblMain;
    }
}