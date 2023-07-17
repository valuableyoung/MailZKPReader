using AForm.Base;
using DevExpress.XtraPrinting;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AForm.Forms.AReport
{
    //ОтчетоПродажах 25
    public partial class Sales : AWindow
    {
        public Sales()
        {
            InitializeComponent();
            this.Tag = "Отчет о продажах";
            this.Text = "Отчет о продажах";

            deStart.EditValue = DateTime.Now.Date.AddDays(-7);
            deEnd.EditValue = DateTime.Now.Date;

            
               // pgc.DataSource = ALogic.Logic.Reports.SalesRep.GetData(deStart.DateTime, deEnd.DateTime);

                pgc.Appearance.Cell.Font = new Font("Arial", 10);
                pgc.Appearance.ColumnHeaderArea.Font = new Font("Arial", 10);
                pgc.Appearance.CustomTotalCell.Font = new Font("Arial", 10, FontStyle.Bold);
                pgc.Appearance.DataHeaderArea.Font = new Font("Arial", 10);
                pgc.Appearance.HeaderArea.Font = new Font("Arial", 10);
                pgc.Appearance.FieldHeader.Font = new Font("Arial", 10);
                pgc.Appearance.HeaderGroupLine.Font = new Font("Arial", 10);
                pgc.Appearance.FieldValue.Font = new Font("Arial", 10);
                //pgc.Appearance.HeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                ///pgc.Appearance.FilterHeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //pgc.Appearance.FieldValue.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void bLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                pgc.BeginUpdate();
                pgc.DataSource = ALogic.Logic.Reports.SalesRep.GetData(deStart.DateTime, deEnd.DateTime);
                pgc.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                DataChanged = false;
            }
        }

        private void bSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //var opt = new XlsExportOptionsEx();
                    var opt = new XlsxExportOptionsEx();

                    opt.AllowGrouping = DevExpress.Utils.DefaultBoolean.False;
                    opt.AllowFixedColumnHeaderPanel = DevExpress.Utils.DefaultBoolean.False;
                    opt.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                    opt.RawDataMode = true;

                    // pgc.ExportToXls(saveFileDialog1.FileName + ".xls", opt);
                    pgc.ExportToXlsx(saveFileDialog1.FileName + ".xlsx", opt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения в Excel: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pgc_Click(object sender, EventArgs e)
        {

        }
    }
}
