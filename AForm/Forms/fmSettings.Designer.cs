namespace AForm.Forms
{
    partial class fmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmSettings));
            this.btnSaveSettings = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadSettings = new DevExpress.XtraEditors.SimpleButton();
            this.leLoadSettings = new DevExpress.XtraEditors.LookUpEdit();
            this.teSaveSettings = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.leLoadSettings.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSaveSettings.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSettings.Appearance.Options.UseFont = true;
            this.btnSaveSettings.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSettings.AppearanceDisabled.Options.UseFont = true;
            this.btnSaveSettings.AppearanceHovered.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSettings.AppearanceHovered.Options.UseFont = true;
            this.btnSaveSettings.AppearancePressed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSettings.AppearancePressed.Options.UseFont = true;
            this.btnSaveSettings.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveSettings.ImageOptions.Image")));
            this.btnSaveSettings.Location = new System.Drawing.Point(12, 12);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(234, 35);
            this.btnSaveSettings.TabIndex = 0;
            this.btnSaveSettings.Text = "Сохранить текущие настройки";
            this.btnSaveSettings.ToolTip = "Сохранить текущие настройки";
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // btnLoadSettings
            // 
            this.btnLoadSettings.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadSettings.Appearance.Options.UseFont = true;
            this.btnLoadSettings.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadSettings.AppearanceDisabled.Options.UseFont = true;
            this.btnLoadSettings.AppearanceHovered.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadSettings.AppearanceHovered.Options.UseFont = true;
            this.btnLoadSettings.AppearancePressed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadSettings.AppearancePressed.Options.UseFont = true;
            this.btnLoadSettings.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadSettings.ImageOptions.Image")));
            this.btnLoadSettings.Location = new System.Drawing.Point(12, 62);
            this.btnLoadSettings.Name = "btnLoadSettings";
            this.btnLoadSettings.Size = new System.Drawing.Size(234, 35);
            this.btnLoadSettings.TabIndex = 1;
            this.btnLoadSettings.Text = "Загрузить настройки";
            this.btnLoadSettings.ToolTip = "Загрузить ранее сохраненные настройки";
            this.btnLoadSettings.Click += new System.EventHandler(this.btnLoadSettings_Click);
            // 
            // leLoadSettings
            // 
            this.leLoadSettings.Location = new System.Drawing.Point(252, 70);
            this.leLoadSettings.Name = "leLoadSettings";
            this.leLoadSettings.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leLoadSettings.Properties.Appearance.Options.UseFont = true;
            this.leLoadSettings.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leLoadSettings.Properties.AppearanceDisabled.Options.UseFont = true;
            this.leLoadSettings.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leLoadSettings.Properties.AppearanceDropDown.Options.UseFont = true;
            this.leLoadSettings.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leLoadSettings.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.leLoadSettings.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leLoadSettings.Properties.AppearanceFocused.Options.UseFont = true;
            this.leLoadSettings.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leLoadSettings.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.leLoadSettings.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leLoadSettings.Properties.DropDownRows = 10;
            this.leLoadSettings.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.leLoadSettings.Properties.ShowHeader = false;
            this.leLoadSettings.Size = new System.Drawing.Size(234, 22);
            this.leLoadSettings.TabIndex = 2;
            // 
            // teSaveSettings
            // 
            this.teSaveSettings.Location = new System.Drawing.Point(252, 19);
            this.teSaveSettings.Name = "teSaveSettings";
            this.teSaveSettings.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teSaveSettings.Properties.Appearance.Options.UseFont = true;
            this.teSaveSettings.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teSaveSettings.Properties.AppearanceDisabled.Options.UseFont = true;
            this.teSaveSettings.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teSaveSettings.Properties.AppearanceFocused.Options.UseFont = true;
            this.teSaveSettings.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teSaveSettings.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.teSaveSettings.Size = new System.Drawing.Size(234, 22);
            this.teSaveSettings.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Location = new System.Drawing.Point(491, 70);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(24, 24);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.ToolTip = "Удалить выбранные настройки";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // fmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 114);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.teSaveSettings);
            this.Controls.Add(this.leLoadSettings);
            this.Controls.Add(this.btnLoadSettings);
            this.Controls.Add(this.btnSaveSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmSettings";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.fmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.leLoadSettings.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSaveSettings.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSaveSettings;
        private DevExpress.XtraEditors.SimpleButton btnLoadSettings;
        private DevExpress.XtraEditors.LookUpEdit leLoadSettings;
        private DevExpress.XtraEditors.TextEdit teSaveSettings;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}