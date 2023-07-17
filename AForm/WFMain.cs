using AForm.Base;
using AForm.Forms.HP;
using AForm.Forms.Old.BuySalePlan;
using AForm.Forms.Old.Other;
using AForm.Forms.Old.Parsers;
using ALogic.Logic.SPR;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AForm
{
    public partial class WFMain : Form
    {
        eForm fname = eForm.Пусто;
        public WFMain(object x = null)
        {
            InitializeComponent();
            this.Text = Text + ", Сервер - " + ConfigurationManager.AppSettings["DBServer"] + ", База - " + ConfigurationManager.AppSettings["DBBase"] + ", Пользователь - " + User.Current.NKontrFull;
            fname = x == null ? eForm.Пусто : (eForm)(int.Parse(x.ToString()));
        }

        public void OpenWindow(Form wind)
        {
            if (wind.Tag == null)
            {
                MessageBox.Show("У окна не указан Тэг!!!");
                return;
            }
            var w = this.MdiChildren.FirstOrDefault(p => p.Tag.ToString() == wind.Tag.ToString());

            if (w != null)
            {
                w.Focus();
                return;
            }

            wind.MdiParent = this;
            wind.Show();
        }

        private void WFMain_Load(object sender, EventArgs e)
        {
            Form wind = null;
            switch (fname)
            {
                case  eForm.ПарсингПрайсов:
                    wind = new PriceParser();
                    WindowOpener.OpenWindow(wind);
                    break; 

                case  eForm.НастройкиПарсингаПрайсов:
                    wind = new WFListSetting();
                    WindowOpener.OpenWindow(wind);

                    break;

                case  eForm.ЗагруженныеПрайсы:
                    wind = new WFListLoadedPrice();
                    WindowOpener.OpenWindow(wind);

                    break;

                case  eForm.ПрасингАссортиментаМск:
                    wind = new ParserMskProductLine();
                    WindowOpener.OpenWindow(wind);

                    break;

                case  eForm.ПарсингНаличияПоставщика:
                    wind = new WFParceKol();
                    WindowOpener.OpenWindow(wind);

                    break;

                case eForm.ПланПродаж:
                    wind = new SalePlan();
                    WindowOpener.OpenWindow(wind);

                    break;

                case eForm.ПланЗакупок:
                    wind = new BuyPlan();
                    WindowOpener.OpenWindow(wind);

                    break;

                case eForm.ОтчетНаборкиКомплектации:
                    wind = new WFRepNaklHabCompl();
                    WindowOpener.OpenWindow(wind);

                    break;

                case  eForm.ПарсингКолвоНаПаллете:
                    wind = new WFParceInPallet();
                    WindowOpener.OpenWindow(wind);

                    break;

                case  eForm.ПарсингБухОстатков:
                    wind = new WFLoadBuhRems();
                    WindowOpener.OpenWindow(wind);

                    break;

                case  eForm.ОтчетКредиторскаяЗадолженность:
                    wind = new WFPerKredZad();
                    WindowOpener.OpenWindow(wind);
                    break;

                case eForm.ОтчетДебиторскаяЗадолженность:
                    wind = new WFPerDebZad();
                    WindowOpener.OpenWindow(wind);
                    break;

                case  eForm.СправочникПриказов:
                    wind = new DecreeList();
                    WindowOpener.OpenWindow(wind);
                    break;

                case  eForm.СправочникСотрудников:
                    wind = new EmployeeCardList();
                    WindowOpener.OpenWindow(wind);
                    break;

                case eForm.РоботВыгрузкаВ1С:
                    load();
                    break;

                case eForm.ПеревыгрузкаВ1С:
                    reload();
                    break;

                case eForm.Тест:
                    test();
                    break;

                case eForm.РоботПарсингаПрайсов:
                    BotParcePrice();
                    break;

                case eForm.РоботПарсингаПрайсовA1:
                    BotParcePriceA1();
                    break;

                case eForm.РоботПарсингаЗКП:
                    BotParceZKP();
                    break;

                case eForm.РоботДиадок:
                    BotDiadoc();
                    break;
                case eForm.РоботОтветовПоставщика:
                    BotSupplierAnswer();
                    break;

                case eForm.НастройкаДоставкиНаправления:
                    wind = new Forms.Spr.DeliveryAndRoad();
                    WindowOpener.OpenWindow(wind);
                    break;
                case eForm.ОтчетоПродажах:
                    wind = new Forms.AReport.Sales();
                    WindowOpener.OpenWindow(wind);
                    break;
                /*case eForm.БонусыАрконаНастройки:
                    wind = new Forms.ArkonaBonus.BonusSettings();
                    WindowOpener.OpenWindow(wind);
                    break;*/
                case eForm.БонусыАрконаУправление:
                    wind = new Forms.ArkonaBonus.ArkonaBonusArm();
                    WindowOpener.OpenWindow(wind);
                    break;
                case eForm.ОтчетОбъемыПродаж:
                    wind = new Forms.AReport.SalesReport();
                    WindowOpener.OpenWindow(wind);
                    break;
                case eForm.БонусыАрконаСписок:
                    wind = new Forms.ArkonaBonus.ArkonaBonusList();
                    WindowOpener.OpenWindow(wind);
                    break;

                case eForm.ПарсингСоСрокомДействия:
                    wind = new Forms.Old.Parsers.PriceFuture();
                    WindowOpener.OpenWindow(wind);
                    break;

                case eForm.БонусыАрконаНастройкиНовые:
                    wind = new Forms.ArkonaBonus.ArkonaBonusSettingsNew();
                    WindowOpener.OpenWindow(wind);
                    break;

                case eForm.ПарсингАВСПоставщика:
                    wind = new WFParseABC();
                    WindowOpener.OpenWindow(wind);

                    break;
            }
        }

        private void BotSupplierAnswer()
        {
            ALogic.Logic.Old.Parsers.MailAnsReader.Start();
            Close();
        }

        private void BotDiadoc()
        {
            ALogic.Logic.ADiadoc.DiadocFunc.OneSession();
            Close();
        }

        private void BotParcePrice()
        {
            ALogic.Logic.Old.Parsers.MailPriceReader.Start(ALogic.Logic.Old.Parsers.MailPriceReader.MailType.Simple);
            Close();
        }
        private void BotParceZKP()
        {
            ALogic.Logic.Old.Parsers.MailZKPReader.Start(ALogic.Logic.Old.Parsers.MailZKPReader.MailType.Simple);
            Close();
        }


        private void BotParcePriceA1()
        {
            ALogic.Logic.Old.Parsers.MailPriceReader.Start(ALogic.Logic.Old.Parsers.MailPriceReader.MailType.A1);
            Close();
        }



        private void test()
        {


            // var wind = new Forms.Kontr.CompanyList();
            //  WindowOpener.OpenWindow(wind);

            //ALogic.Logic.ADiadoc.DiadocFunc.GetAllMessagesToSave();


            //var tbl = ALogic.Logic.Heplers.FileReader.Read_CSV(@"H:\Price.txt");
            //ALogic.Logic.ADiadoc.DiadocFunc.SendUpdXml("60/41 24.01.20 1249 11 550861");
            // ALogic.Logic.ADiadoc.DiadocFunc.GetAllMessagesToSave();           
            // ALogic.Logic.ADiadoc.DiadocFunc.GetAllMessagesToSave();           

            ALogic.Logic.ADiadoc.DiadocFunc.RegSert("3665090338");
        }

        private void load()
        {
            ALogic.Logic.Reload1C.Reload1CLogic.FullLoad();
            Close();
        }

        private void reload()
        {
            var wind = new Forms.Reload1C.Reload1CList();
            WindowOpener.OpenWindow(wind);
        }
    }

    public enum eForm
    {
        Пусто = 0,
        ПарсингПрайсов = 1,
        НастройкиПарсингаПрайсов = 2,
        ЗагруженныеПрайсы = 3,
        ПрасингАссортиментаМск = 4,
        ПарсингНаличияПоставщика = 5,
        ПланПродаж = 6,
        ПланЗакупок = 7,
        ОтчетНаборкиКомплектации = 8,
        ПарсингКолвоНаПаллете = 9,
        ПарсингБухОстатков = 10,
        ОтчетКредиторскаяЗадолженность = 11,
        СправочникПриказов = 12,
        СправочникСотрудников = 13,
        ОтчетДебиторскаяЗадолженность = 14,
        РоботВыгрузкаВ1С = 15,
        ПеревыгрузкаВ1С = 16,
        Тест = 17,
        РоботПарсингаПрайсов = 18,
        РоботДиадок = 19,
        РоботОтветовПоставщика = 20,
        НастройкаДоставкиНаправления = 21,
        ОтчетоПродажах = 22,
        //БонусыАрконаНастройки = 23, заменены на новые настройки
        БонусыАрконаУправление = 24,
        ОтчетОбъемыПродаж = 25,
        БонусыАрконаСписок = 26,
        ПарсингСоСрокомДействия = 27,
        БонусыАрконаНастройкиНовые = 23,
        РоботПарсингаПрайсовA1 = 29,
        ПарсингАВСПоставщика = 30,
        РоботПарсингаЗКП = 31
    }
}
