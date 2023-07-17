using ALogic.Logic.SPR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AForm.Forms.Spr
{
    public partial class FormTestPrice : Form
    {
        public FormTestPrice()
        {
            InitializeComponent();
            this.Tag = "123123123";
        }

        //static DataSet _ds;
        static int count = 0;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

            Thread t = new Thread(mythread1);
            Thread t2 = new Thread(mythread1);
            Thread t3 = new Thread(mythread1);
            Thread t4 = new Thread(mythread1);
            Thread t5 = new Thread(mythread1);
            Thread t6 = new Thread(mythread1);
            Thread t7 = new Thread(mythread1);
            Thread t8 = new Thread(mythread1);
            Thread t9 = new Thread(mythread1);
            Thread t10 = new Thread(mythread1);

            t.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();
            t9.Start();
            t10.Start();
        }


        static void mythread1()
        {
            SqlConnection conn = new SqlConnection(ALogic.DBConnector.Connection.ConnectionString);
            conn.Open();

            DataSet ds = new DataSet();

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

            SqlDataAdapter da = new SqlDataAdapter(str, conn);         
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(ds);

            //_ds = ALogic.Logic.SPR.TestPrice.Test();
            Export2ExcelClass.CreateWorkbook(@"H:\xxx\pr3" + count++ + ".xls", ds);
        }
    }
}
