using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ALogic.Logic.SPR;
namespace AForm.Forms.Old.Parsers
{

    public partial class StockSetting : Form
    {
        public delegate void ReloadStockHandler(int idSupplier, int idSkladSupplier);

        public event ReloadStockHandler ReloadEvent;

        int idSupplier;
        sSkladSupplier sSklad = new sSkladSupplier();
        public StockSetting(int idSupplier, string SupName)
        {
            InitializeComponent();
            tbSupplierName.Text = SupName;
            this.idSupplier = idSupplier;
        }

        public StockSetting(int idSkladSupplier, int idSupplier, string SupName):this(idSupplier, SupName)
        {
            sSklad.idSkladSupplier = idSkladSupplier;
            LoadStockSetting(idSkladSupplier);
        }

        private void LoadStockSetting(int idSkladSupplier)
        {
            var skladSetting = sSkladSupplierData.getDataByIdSkladSupplier(idSkladSupplier);
            tbnSklad.Text = skladSetting.nSkladSupplier;
            nuDayDelivery.Value = skladSetting.dayDelivery;
            tbMaxSumOrder.Text = skladSetting.maxSumOrder.ToString();
        }

        private void btnStockSave_Click(object sender, EventArgs e)
        {
            if (tbnSklad.Text != "" && tbMaxSumOrder.Text != "")
            {

                sSklad.idSupplier = idSupplier;
                sSklad.nSkladSupplier = tbnSklad.Text;
                sSklad.dayDelivery = (int)nuDayDelivery.Value;
                sSklad.maxSumOrder = double.Parse(tbMaxSumOrder.Text);
                sSkladSupplierData.InsertOrUpdate(sSklad);
                ReloadEvent(idSupplier, sSklad.idSkladSupplier);
                MessageBox.Show("Спасибо, настройки сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tbMaxSumOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.') && (tbMaxSumOrder.Text.IndexOf(".") == -1) && (tbMaxSumOrder.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
