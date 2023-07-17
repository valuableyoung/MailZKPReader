namespace AForm.Forms.Old.Parsers
{
    partial class StockSetting
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
            this.tbnSklad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nuDayDelivery = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMaxSumOrder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSupplierName = new System.Windows.Forms.Label();
            this.btnStockSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nuDayDelivery)).BeginInit();
            this.SuspendLayout();
            // 
            // tbnSklad
            // 
            this.tbnSklad.Location = new System.Drawing.Point(13, 126);
            this.tbnSklad.Margin = new System.Windows.Forms.Padding(4);
            this.tbnSklad.Name = "tbnSklad";
            this.tbnSklad.Size = new System.Drawing.Size(370, 23);
            this.tbnSklad.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(13, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Наименование склада";
            // 
            // nuDayDelivery
            // 
            this.nuDayDelivery.Location = new System.Drawing.Point(605, 127);
            this.nuDayDelivery.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nuDayDelivery.Name = "nuDayDelivery";
            this.nuDayDelivery.Size = new System.Drawing.Size(54, 23);
            this.nuDayDelivery.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(412, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Количество дней доставки";
            // 
            // tbMaxSumOrder
            // 
            this.tbMaxSumOrder.Location = new System.Drawing.Point(688, 129);
            this.tbMaxSumOrder.Margin = new System.Windows.Forms.Padding(4);
            this.tbMaxSumOrder.Name = "tbMaxSumOrder";
            this.tbMaxSumOrder.Size = new System.Drawing.Size(198, 23);
            this.tbMaxSumOrder.TabIndex = 8;
            this.tbMaxSumOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMaxSumOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMaxSumOrder_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(688, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Максимальная сумма заказа";
            // 
            // tbSupplierName
            // 
            this.tbSupplierName.AutoSize = true;
            this.tbSupplierName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSupplierName.Location = new System.Drawing.Point(13, 75);
            this.tbSupplierName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tbSupplierName.Name = "tbSupplierName";
            this.tbSupplierName.Size = new System.Drawing.Size(74, 20);
            this.tbSupplierName.TabIndex = 1;
            this.tbSupplierName.Text = "-------------";
            // 
            // btnStockSave
            // 
            this.btnStockSave.BackColor = System.Drawing.Color.Transparent;
            this.btnStockSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockSave.ForeColor = System.Drawing.Color.Transparent;
            this.btnStockSave.Image = global::AForm.Properties.Resources.disk;
            this.btnStockSave.Location = new System.Drawing.Point(12, 12);
            this.btnStockSave.Name = "btnStockSave";
            this.btnStockSave.Size = new System.Drawing.Size(45, 43);
            this.btnStockSave.TabIndex = 39;
            this.btnStockSave.UseVisualStyleBackColor = false;
            this.btnStockSave.Click += new System.EventHandler(this.btnStockSave_Click);
            // 
            // StockSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 206);
            this.Controls.Add(this.btnStockSave);
            this.Controls.Add(this.tbSupplierName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMaxSumOrder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nuDayDelivery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbnSklad);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StockSetting";
            this.Tag = "Stock";
            this.Text = "Настройка склада поставщика";
            ((System.ComponentModel.ISupportInitialize)(this.nuDayDelivery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbnSklad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nuDayDelivery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMaxSumOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label tbSupplierName;
        private System.Windows.Forms.Button btnStockSave;
    }
}