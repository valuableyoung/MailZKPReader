using ALogic.Logic.Old;
using ALogic.Logic.Old.Parsers;
using ALogic.Logic.SPR;
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
    public partial class SuplierAnswerRobot : DevExpress.XtraEditors.XtraForm
    {
        public SuplierAnswerRobot()
        {
            InitializeComponent();
            Tag = "Робот ответов поставщика";          
            Logger.ErrorCode = (int)LogAction.PriceParcerError;
        }     

        private void bTest_Click(object sender, EventArgs e)
        {
            MailAnsReader.Start();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            int sek = RobotAnswer.GetPeriod(6) * 60;
            MailTimer.Interval = sek * 1000;
            btStart.Enabled = false;
            btStop.Enabled = true;
            MailTimer.Start();       
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            MailTimer.Stop();
            btStart.Enabled = true;
            btStop.Enabled = false;
        }

        private void MailTimer_Tick(object sender, EventArgs e)
        {
            MailAnsReader.Start();  
        }

        private void FMain_Load(object sender, EventArgs e)
        {

        }       
    }
}
