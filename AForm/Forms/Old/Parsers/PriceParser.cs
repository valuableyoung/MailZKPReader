using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using AForm.Base;
using ALogic.DBConnector;
using ALogic.Logic.SPR.Old;
using ALogic.Logic.Heplers;
using ALogic.Logic.Old.Parsers;
using ALogic.Logic.Old;

namespace AForm.Forms.Old.Parsers
{
    public partial class PriceParser : DevExpress.XtraEditors.XtraForm
    {
        private object idUser;
        int idSetting;
        DateTime datePrice;
        ToolTip hint;

        public PriceParser()
        {
            InitializeComponent();
            this.Tag = "Парсинг прайсов";
            hint = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                //ToolTipIcon = ToolTipIcon.Info,
                ShowAlways = true
            };

            Logger.ErrorCode = (int)LogAction.PriceParcerError;
            AEvents.PriceParserEvents.SaveParserSetting += RefreshSettings;        
        }

        private void PriceParser_FormClosing(object sender, FormClosingEventArgs e)
        {
            AEvents.PriceParserEvents.SaveParserSetting -= RefreshSettings;
        }

        private void btOpenFile_Click(object sender, EventArgs e)
        {
            if (dlgMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbFile.Text = dlgMain.FileName;
            }
        }

        private void FHandWork_Load(object sender, EventArgs e)
        {
            idUser = DBUsers.GetCurrentWindowUserId();
            if (idUser == null)
            {
                MessageBox.Show("Пользователь не идентифицирован, обратитесь к программистам.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bLoad.Enabled = false;
            }

            DataTable dtSuppliers = DBSprSupplier.GetSuppliers();
            cbFirm.DataSource = dtSuppliers;
            cbFirm.DisplayMember = "n_kontr";
            cbFirm.ValueMember = "id_kontr";
            
            cbFirm.SelectedValue = 0;

            deDatePrice.EditValue = DateTime.Now.Date;

         //   this.Text = "Загрузка прайсов; База данных: "+DBConnector.DBConnectorProperty.DBBase + "; Авторизация: " + DBConnector.Spr.DBUsers.GetCurrentWindowUserName().ToString();
            Messenger.ActionMessage += AddMessage;
            Messenger.ActionProgress += AddProgress;
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            //bLoad.Enabled = false;
            lbText.Visible = true;
            lbLog.Visible = true;

            if (cbFirm.SelectedValue == null )
            {
                MessageBox.Show("Выберите поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tbFile.Text == "")
            {
                MessageBox.Show("Выберите файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ( ((DateTime)deDatePrice.EditValue).Date < DateTime.Now.Date )
            {
                MessageBox.Show("Загрузка на прошлую дату бессмысленна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            idSetting = int.Parse(cbSetting.SelectedValue.ToString());
            datePrice = (DateTime)deDatePrice.EditValue;
            Thread t = new Thread(StartMethod);
            t.Start();          
        }

        public void AddMessage(object sender, EventArgs e)
        {

            this.BeginInvoke(new Action(delegate ()
             {
                 lbLog.Text = sender.ToString();
             }));}

        public void AddProgress(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(delegate()
            {
                pbMain.Value = int.Parse( sender.ToString());
                if (pbMain.Value == 0)
                    pbMain.Visible = true;
                if (pbMain.Value == 30)
                {
                    Thread.Sleep(300);
                    pbMain.Visible = false;
                }
            }));
        }

   

   

        public void StartMethod()
        {
            var setting = ParserSettingsHelper.GetCurrentSettingById(idSetting);
            MailPriceReader.ReadSingleFile(tbFile.Text, setting, int.Parse( idUser.ToString()), datePrice);

            this.BeginInvoke(new Action(delegate()
            {
                bLoad.Enabled = true;
                lbText.Visible = false;
                lbLog.Visible = false;
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void cbSetting_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbFirm_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSettings(this, null);           
        }

        private void RefreshSettings( object sender, EventArgs x )
        {
            var elem = cbFirm.SelectedValue;
            int idKontr;

            if (elem != null && int.TryParse(elem.ToString(), out idKontr))
            {
                int pos = 0;
                pos = cbSetting.SelectedValue == null ? 0 : int.Parse(cbSetting.SelectedValue.ToString());

                var settings = DBParserSettings.GetParserSettingByidSupplier(idKontr);
                /*cbSetting.DataSource = settings;
                cbSetting.DisplayMember = "nParserSettings";
                cbSetting.ValueMember = "idParserSettings";*/

                cbSetting.DataSource = null;
                cbSetting.Items.Clear();
                List<PSettings> pl = new List<PSettings>();

                foreach (DataRow r in settings.AsEnumerable())
                {
                    pl.Add(new PSettings(r["nParserSettings"].ToString(),
                        int.Parse(r["idParserSettings"].ToString()),
                        int.Parse(r["fPriceWithDateActual"].ToString())));
                }

                cbSetting.DataSource = pl;
                cbSetting.DisplayMember = "m_Name";
                cbSetting.ValueMember = "m_Value";

                cbSetting.SelectedValue = pos;
            }
        }

        private void btAddSetting_Click(object sender, EventArgs e)
        {
            Setting wind;
            if (cbFirm.SelectedIndex > 0)
               wind = new Setting(0, int.Parse(cbFirm.SelectedValue.ToString()));
            else
               wind = new Setting();
            
            WindowOpener.OpenWindow(wind);  
        }       

        private void btEditSetting_Click(object sender, EventArgs e)
        {
            if (cbSetting.SelectedValue == null)
            {
                MessageBox.Show("Не выбрана настройка");
                return;
            }

            int idSetting = int.Parse(cbSetting.SelectedValue.ToString());
            if (idSetting == 0)
            {
                MessageBox.Show("Не выбрана настройка");
                return;
            }
            var wind = new Setting(idSetting);
            WindowOpener.OpenWindow(wind);  
        }

        private void cbSetting_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                Graphics g = e.Graphics;

                Color SelectedBackColor;

                var vtag = ((PSettings)cbSetting.Items[e.Index]).m_Tag;
                var vname = ((PSettings)cbSetting.Items[e.Index]).m_Name;

                switch (vtag)
                {
                    case 0: SelectedBackColor = e.BackColor; break;
                    case 1: SelectedBackColor = Color.LightGreen; break;
                    default: SelectedBackColor = e.BackColor; break;
                }

                Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? new SolidBrush(e.BackColor) : new SolidBrush(SelectedBackColor);
                Brush tBrush = new SolidBrush(Color.Black);

                g.FillRectangle(brush, e.Bounds);
                e.Graphics.DrawString(vname, e.Font, tBrush, e.Bounds, StringFormat.GenericDefault);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && vtag == 1)
                {
                    hint.Show("Настройка для парсинга прайсов со сроком действия", cbSetting, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    hint.Hide(cbSetting);
                }

                brush.Dispose();
                tBrush.Dispose();
            }
            e.DrawFocusRectangle();
        }
    }
}
