namespace AForm.Forms.Old.Other
{
    partial class ParserMskProductLine
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
            this.label1 = new System.Windows.Forms.Label();
            this.bLoadFile = new System.Windows.Forms.Button();
            this.bLoad = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dlg = new System.Windows.Forms.OpenFileDialog();
            this.rbLoad = new System.Windows.Forms.RadioButton();
            this.rbDelAndLoad = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(30, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите файл с ассортиментом Московского склада. ";
            // 
            // bLoadFile
            // 
            this.bLoadFile.Location = new System.Drawing.Point(12, 63);
            this.bLoadFile.Name = "bLoadFile";
            this.bLoadFile.Size = new System.Drawing.Size(108, 23);
            this.bLoadFile.TabIndex = 1;
            this.bLoadFile.Text = "Выбрать файл";
            this.bLoadFile.UseVisualStyleBackColor = true;
            this.bLoadFile.Click += new System.EventHandler(this.bLoadFile_Click);
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(126, 63);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(125, 23);
            this.bLoad.TabIndex = 2;
            this.bLoad.Text = "Загрузить";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 112);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(555, 566);
            this.dgv.TabIndex = 3;
            // 
            // dlg
            // 
            this.dlg.FileName = "openFileDialog1";
            // 
            // rbLoad
            // 
            this.rbLoad.AutoSize = true;
            this.rbLoad.Checked = true;
            this.rbLoad.Location = new System.Drawing.Point(259, 63);
            this.rbLoad.Name = "rbLoad";
            this.rbLoad.Size = new System.Drawing.Size(143, 17);
            this.rbLoad.TabIndex = 4;
            this.rbLoad.TabStop = true;
            this.rbLoad.Text = "Обновить ассортимент";
            this.rbLoad.UseVisualStyleBackColor = true;
            // 
            // rbDelAndLoad
            // 
            this.rbDelAndLoad.AutoSize = true;
            this.rbDelAndLoad.Location = new System.Drawing.Point(259, 89);
            this.rbDelAndLoad.Name = "rbDelAndLoad";
            this.rbDelAndLoad.Size = new System.Drawing.Size(286, 17);
            this.rbDelAndLoad.TabIndex = 5;
            this.rbDelAndLoad.Text = "Заменить ассортимент(удалит все старые данные)";
            this.rbDelAndLoad.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(30, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(540, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Требования к файлу: 1 столбец(Column0) - код товара 2-й столбец(Column1) - количе" +
                "ство";
            // 
            // ParserMskProductLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 680);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbDelAndLoad);
            this.Controls.Add(this.rbLoad);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.bLoadFile);
            this.Controls.Add(this.label1);
            this.Name = "ParserMskProductLine";
            this.Text = "Загрузка ассортимента МСК";
            this.Load += new System.EventHandler(this.ParserMskProductLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bLoadFile;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.OpenFileDialog dlg;
        private System.Windows.Forms.RadioButton rbLoad;
        private System.Windows.Forms.RadioButton rbDelAndLoad;
        private System.Windows.Forms.Label label2;
    }
}