namespace AForm.Forms.Spr
{
    partial class DeliveryAndRoad
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
            this.tRoad = new AForm.Base.ATable();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.tPosition = new AForm.Base.ATable();
            this.SuspendLayout();
            // 
            // tRoad
            // 
            this.tRoad._ButtonAdd = false;
            this.tRoad._ButtonDelete = false;
            this.tRoad._ButtonLoad = false;
            this.tRoad.DataSource = null;
            this.tRoad.Dock = System.Windows.Forms.DockStyle.Left;
            this.tRoad.FocusedRowNumber = -2147483648;
            this.tRoad.FullName = null;
            this.tRoad.Location = new System.Drawing.Point(0, 0);
            this.tRoad.Name = "tRoad";
            this.tRoad.Size = new System.Drawing.Size(352, 450);
            this.tRoad.TabIndex = 0;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(352, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 450);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // tPosition
            // 
            this.tPosition._ButtonAdd = false;
            this.tPosition._ButtonDelete = false;
            this.tPosition._ButtonLoad = false;
            this.tPosition._ButtonSave = false;
            this.tPosition.DataSource = null;
            this.tPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPosition.FocusedRowNumber = -2147483648;
            this.tPosition.FullName = null;
            this.tPosition.Location = new System.Drawing.Point(357, 0);
            this.tPosition.Name = "tPosition";
            this.tPosition.Size = new System.Drawing.Size(443, 450);
            this.tPosition.TabIndex = 2;
            // 
            // DeliveryAndRoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tPosition);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.tRoad);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DeliveryAndRoad";
            this.Text = "Направления и районы";
            this.ResumeLayout(false);

        }

        #endregion

        private Base.ATable tRoad;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private Base.ATable tPosition;
    }
}