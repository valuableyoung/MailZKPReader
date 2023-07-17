using ALogic.Logic.Old.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AForm.Forms.Old.Parsers
{
    public partial class WFListLoadedPrice : DevExpress.XtraEditors.XtraForm
    {
        public WFListLoadedPrice()
        {
            InitializeComponent();
            this.Tag = "Список загруженных прайсов поставщиков";

            gridControl1.DataSource = DBLogParserPriceLoad.getlogParserPrice();
        }

        private void btnStockSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ext = saveFileDialog1.FileName.Split('.').Last().ToUpper();
                //if (ext == "XLS")
                //{
                //    gridControl1.ExportToXls(saveFileDialog1.FileName);
                //}

                if (ext == "XLSX")
                {
                    gridControl1.ExportToXlsx(saveFileDialog1.FileName);
                }

                if (ext == "PDF")
                {
                    gridControl1.ExportToPdf(saveFileDialog1.FileName);
                }

                if (ext == "TXT")
                {
                    gridControl1.ExportToText(saveFileDialog1.FileName);
                }
            }
        }
    }
}
