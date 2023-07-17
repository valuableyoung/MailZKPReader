using AForm.Base;
using System;
using System.Diagnostics;
using System.Linq;

namespace AForm.Forms.Reload1C
{
    public partial class Load1C : AWindow
    {
        public Load1C()
        {
            InitializeComponent();
            this.Tag = this.Text = "Полная выгрузка в 1с";
        }

        private void Load1C_Load(object sender, EventArgs e)
        {
            rtb.Text = "";
            Reload();
        }

        private void Reload()
        {
            int val = int.Parse(ALogic.DBConnector.DBAppParam.GetAppParamValue(435).ToString());

            if (val != 0)
            {
                SendMessage("Выгрузка в 1с уже ведется!");
                return;
            }

            ALogic.DBConnector.DBAppParam.SetAppParamValue(435, 1);
            SendMessage("Отметка о начале выгрузки");

            Process prZak1 = Process.Start(@"D:\Base1C\ex\4Test\bay.exe", "DESIGNDOC");
            SendMessage(@"Запущен процесс D:\Base1C\ex\4Test\bay.exe DESIGNDOC");
            prZak1.WaitForExit();

            if (!prZak1.HasExited)
            {
                SendMessage("Проведение перезакупок №1 не удалось");
                return;
            }

            Process prZak2 = Process.Start(@"D:\Base1C\ex\4Test\sale.exe", "DESIGNDOC");
            SendMessage(@"Запущен процесс D:\Base1C\ex\4Test\sale.exe DESIGNDOC");
            prZak2.WaitForExit();

            if (!prZak2.HasExited)
            {
                SendMessage("Проведение перезакупок №2 не удалось");
                return;
            }

            Process prZak3 = Process.Start(@"D:\Base1C\ex\4Test\bay.exe", "WORKDOC");
            SendMessage(@"Запущен процесс D:\Base1C\ex\4Test\bay.exe WORKDOC");
            prZak3.WaitForExit();

            if (!prZak3.HasExited)
            {
                SendMessage("Проведение перезакупок №3 не удалось");
                return;
            }

            Process prZak4 = Process.Start(@"D:\Base1C\ex\4Test\sale.exe", "WORKDOC");
            SendMessage(@"Запущен процесс D:\Base1C\ex\4Test\sale.exe WORKDOC");
            prZak4.WaitForExit();

            if (!prZak4.HasExited)
            {
                SendMessage("Проведение перезакупок №4 не удалось");
                return;
            }

            SendMessage(ALogic.Logic.Reload1C.Reload1CLogic.LoadAll());

            ALogic.DBConnector.DBAppParam.SetAppParamValue(435, 0);
        }

        private void SendMessage(string p)
        {
            rtb.Text += DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " : " + p;
        }
    }
}
