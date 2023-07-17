namespace AForm.Base
{
    partial class WCheck
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
            this.tMain = new AForm.Base.ATable();
            this.SuspendLayout();
            // 
            // tMain
            // 
            this.tMain._ButtonAdd = false;
            this.tMain._ButtonDelete = false;
            this.tMain._ButtonFound = false;
            this.tMain._ButtonLoad = false;
            this.tMain._ButtonSave = false;
            this.tMain.DataSource = null;
            this.tMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tMain.FocusedRowNumber = -2147483648;
            this.tMain.FullName = null;
            this.tMain.Location = new System.Drawing.Point(0, 0);
            this.tMain.Name = "tMain";
            this.tMain.Size = new System.Drawing.Size(800, 450);
            this.tMain.TabIndex = 0;
            // 
            // WCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tMain);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "WCheck";
            this.Text = "Выбор элементов";
            this.ResumeLayout(false);

        }

        #endregion

        private ATable tMain;
    }
}