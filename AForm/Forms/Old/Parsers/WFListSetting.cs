using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForm.Base;
using ALogic.Logic.Old.Parsers;

namespace AForm.Forms.Old.Parsers
{
    public partial class WFListSetting : DevExpress.XtraEditors.XtraForm
    {
        public WFListSetting()
        {
            InitializeComponent();
            this.Tag = "Список настроек парсинга прайсов";
            AEvents.PriceParserEvents.SaveParserSetting += LoadData;
        }

        private void WFListSetting_Load(object sender, EventArgs e)
        {           
            LoadData(this, null);
        }

        private void bAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wind = new Setting();           
            WindowOpener.OpenWindow(wind);            
        }

        public void LoadData(object sender, EventArgs e)
        {
            var handle = gvMain.FocusedRowHandle;
            var table = DBParserSettings.GetAllParserSettings();
            gcMain.DataSource = table;
            if (gvMain.RowCount > handle)
                gvMain.FocusedRowHandle = handle;
            else
                gvMain.FocusedRowHandle = gvMain.RowCount-1;
        }

        private void WFListSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            AEvents.PriceParserEvents.SaveParserSetting -= LoadData;
        }

        private void bEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gvMain.GetFocusedDataRow();           
            var idSetting = int.Parse(row["idParserSettings"].ToString());
            var wind = new Setting(idSetting);
            WindowOpener.OpenWindow(wind);
        }

        private void bDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить настройку?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                var row = gvMain.GetFocusedDataRow();
                var idSetting = int.Parse(row["idParserSettings"].ToString());
                DBParserSettings.DeleteParserSetting(idSetting);
                LoadData(this, null);
            }
        }
    }
}
