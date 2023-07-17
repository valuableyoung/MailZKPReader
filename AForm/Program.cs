using AForm.Base;
using ALogic.DBConnector;
using ALogic.Logic.SPR;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AForm.ExelForm
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //при сборке ботов оставляем пользователя EasZakTov и раскомментируем вызов конкретного бота в args[2]
            //при сборке десктопных приложений комментим вообще все внутри директивы #Region 
            #region Закомментировать все перед релизом

            args = new string[3];

            //args[0] = "EasZakTov";
            //args[1] = "ddfi3)es";

            args[0] = "MuhinAN";
            args[1] = "MuhinAN1017";

            //args[0] = "ZolotuhinAS";
            //args[1] = "ZolotuhinAS490";

            //args[0] = "KubrakovMA";
            //args[1] = "KubrakovMA291";

            //args[0] = "dubininaa";
            //args[1] = "dubininaa187";

            //args[0] = "gospodarikovanm";
            //args[1] = "natali123";

            //args[0] = "CheremushkinaTA";
            //args[1] = "CheremushkinaTA787";

            //args[0] = "ArhipovaEA";
            //args[1] = "ArhipovaEA143";

            // args[0] = "fedchenkoas";
            // args[1] = "kigh701";

            //args[0] = "lazarevop";
            //args[1] = "lazarevop122";

            //args[0] = "natali";
            //args[1] = "natali123";

            //args[0] = "bugakovaov";
            //args[1] = "bugakova549";

            //args[0] = "TurishevAV";
            //args[1] = "TurishevAV312";

            //args[0] = "BaranovaUS";
            //args[1] = "dontwork";

            //args[0] = "DavydenkoAV";
            //args[1] = "DavydenkoAV64";

            //args[0] = "BoykovDV";
            //args[1] = "boykovdv53";

            //args[0] = "kravtsovaa";
            // args[1] = "Zaq123";

            args[2] = ((int)eForm.РоботПарсингаЗКП).ToString();
            //args[2] = ((int)eForm.РоботПарсингаПрайсов).ToString();
            //args[2] = ((int)eForm.РоботОтветовПоставщика).ToString();
            // args[2] = ((int)eForm.РоботДиадок).ToString();       //01.08.2021 вынесен в отдельный проект, здесь его уже нет
            //args[2] = ((int)eForm.РоботВыгрузкаВ1С).ToString();
            //args[2] = ((int)eForm.РоботПарсингаПрайсовA1).ToString();

            //args[2] = ((int)eForm.ОтчетоПродажах).ToString();
            //args[2] = ((int)eForm.ОтчетОбъемыПродаж).ToString();

            //args[2] = ((int)eForm.ПарсингСоСрокомДействия).ToString();
            //args[2] = ((int)eForm.ПарсингПрайсов).ToString();
            //args[2] = ((int)eForm.ПланПродаж).ToString();
            //args[2] = ((int)eForm.ПарсингАВСПоставщика).ToString();

            //args[2] = ((int)eForm.БонусыАрконаСписок).ToString();
            //args[2] = ((int)eForm.БонусыАрконаНастройки).ToString(); //c 01.01.2023 не используется, есть новые настройки
            //args[2] = ((int)eForm.БонусыАрконаУправление).ToString();
            //args[2] = ((int)eForm.СправочникСотрудников).ToString();
            //args[2] = ((int)eForm.ЗагруженныеПрайсы).ToString();
            //args[2] = ((int)eForm.ПеревыгрузкаВ1С).ToString();

            //args[2] = ((int)eForm.БонусыАрконаНастройкиНовые).ToString();

            //args[2] = ((int)eForm.ПарсингБухОстатков).ToString();


            #endregion


            ProjectProperty.LoadDataAppConfig();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                if (User.LoginUser(args[0], args[1]))
                {
                    Form form = new WFMain(args[2]);
                    WindowOpener.MainForm = (WFMain)form;
                    Application.Run(form);
                }
            }
        }
    }
}
