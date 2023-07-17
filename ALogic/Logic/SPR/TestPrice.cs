using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;

namespace ALogic.Logic.SPR
{
    public static class TestPrice
    {
        public static DataSet Test()
        {
            string str = @"
exec dbo.sp_GetPriceImag 
555323,
 4,
 60,
 0,
 2,
 1,
 0 ,
'',
'',
'',
'',
'',
''
";
            DataSet table = DBConnector.DBExecutor.SelectDataSet(str);


            return table;



        }
    }

 
    public static class Export2ExcelClass
    {
        //export Excel from DataSet
        public static void CreateWorkbook(String filePath, DataSet dataset)
        {
            if (dataset.Tables.Count == 0)
                throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");

            Workbook workbook = new Workbook();
            foreach (DataTable dt in dataset.Tables)
            {
                Worksheet worksheet = new Worksheet(dt.TableName);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    // Add column header
                    worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);

                    // Populate row data
                    for (int j = 0; j < dt.Rows.Count; j++)
                        //Если нулевые значения, заменяем на пустые строки
                        worksheet.Cells[j + 1, i] = new Cell(dt.Rows[j][i] == DBNull.Value ? "" : dt.Rows[j][i]);
                }
                workbook.Worksheets.Add(worksheet);
            }
            workbook.Save(filePath);
        }  
    }

   
}
