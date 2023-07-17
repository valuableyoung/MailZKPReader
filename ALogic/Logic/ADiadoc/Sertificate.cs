using Diadoc.Api;
using Diadoc.Api.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ALogic.DBConnector;
using System.IO;
using ALogic.Logic.Old;

namespace ALogic.Logic.ADiadoc
{
    public class Sertificate
    {
        private static Dictionary<string, Sertificate> _dctSertificate;
        public static Dictionary<string, Sertificate> DctSertificate { get { return _dctSertificate; } }

        static Sertificate()
        {
            try
            {
                _dctSertificate = new Dictionary<string, Sertificate>();
                LoadRealSertificates();
            }
            catch (Exception e)
            {
                Logger.WriteErrorMessage("Время: " + DateTime.Now.ToString() +
                    " Ошибка в конструкторе класса Sertificate: " + e.Message + "\n");
            }
        }

        public byte[] FileContent { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public string AuthTokenCert { get; set; }		                            // полученный токен сессии Диадок      
        public DiadocApi Api { get; set; }
        public WinApiCrypt Crypt { get; set; }
        public string BoxId { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }

        public static void ReloadRealSertificates()
        {
            try
            { 
                _dctSertificate.Clear();
                LoadRealSertificates();
            }
            catch (Exception e)
            {
                Logger.WriteErrorMessage("Время: " + DateTime.Now.ToString() +
                    " Ошибка на методе ReloadRealSertificates класса Sertificate: " + e.Message + "\n");
            }
        }

        public static void LoadRealSertificates()
        {
            var tableSert = DBDiadoc.GetSertificates();
            foreach (var row in tableSert.AsEnumerable())
            {
                var fio = row["FIO"].ToString().Split(' ');
                if (fio.Count() < 3)
                {
                    Logger.SendMessage("Для компании Inn=" + row["Inn"] + "не верно настроен сертификат - fio.Count < 3");
                    Logger.WriteErrorMessage("Для компании Inn=" + row["Inn"] + "не верно настроен сертификат - fio.Count < 3");
                    continue;
                }
                var sert = new Sertificate();
                sert.Surname = fio[0];
                sert.Name = fio[1];
                sert.Patronymic = fio[2];

                try
                {
                    sert.FileContent = GetFileSertificate(row["Way"].ToString());
                    //sert.FileContent = GetFileSertificate(@"C:\Test1\sertDiad\AD.cer"); 
                }
                catch (Exception ex)
                {
                    Logger.SendMessage("Для компании Inn=" + row["Inn"].ToString() + " файл сертификата не найден или не доступен. Путь: " + row["Way"].ToString() + " Текст дла программиста: " + ex.Message);
                    Logger.WriteErrorMessage("Для компании Inn=" + row["Inn"].ToString() + " файл сертификата не найден или не доступен. Путь: " + row["Way"].ToString() + " Текст дла программиста: " + ex.Message);
                    continue;
                }

                try
                {
                    sert.Crypt = new WinApiCrypt();
                    sert.Api = new DiadocApi(ProjectProperty.DiadocTokenAPI, ProjectProperty.DiadocUrl, new WinApiCrypt());
                    Logger.SendMessage("подключение к DiadocAPI...");

                    //sert.Api.SetProxyCredentials(ProjectProperty.ProxyLogin, ProjectProperty.ProxyPass);
                    //sert.Api.SetProxyUri(ProjectProperty.ProxyUri);

                    sert.AuthTokenCert = sert.Api.Authenticate(sert.FileContent);
                    Logger.SendMessage("токен для " + row["Inn"].ToString() + " получен");
                    sert.Inn = row["Inn"].ToString().Replace(" ", "");
                    sert.Kpp = row["Kpp"].ToString().Replace(" ", "");
                    sert.BoxId = sert.Api.GetOrganizationByInnKpp(sert.Inn, sert.Kpp).Boxes[0].BoxId;
                    _dctSertificate.Add(sert.Inn, sert);
                }
                catch (Exception ex)
                {
                    Logger.SendMessage("Ошибка подключения к Диадок. ИНН: " + row["Inn"].ToString() + " Строка для разработчика: " + ex.Message);
                    Logger.WriteErrorMessage("Ошибка подключения к Диадок. ИНН: " + row["Inn"].ToString() + " Строка для разработчика: " + ex.Message);
                    continue;
                }
            }
        }

        public static byte[] GetFileSertificate(string path)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                var buffer = new MemoryStream();
                var data = new byte[8000];
                int count;
                while ((count = file.Read(data, 0, data.Length)) > 0)
                {
                    buffer.Write(data, 0, count);
                }
                return buffer.ToArray();
            }
        }

        public static string Test()
        {
            var row = DBDiadoc.GetSertificates().AsEnumerable().First();

            var fio = row["FIO"].ToString().Split(' ');

            var sert = new Sertificate();
            sert.Surname = fio[0];
            sert.Name = fio[1];
            sert.Patronymic = fio[2];

            sert.FileContent = GetFileSertificate(row["Way"].ToString());

            sert.Crypt = new WinApiCrypt();

            sert.Api = new DiadocApi(ProjectProperty.DiadocTokenAPI, ProjectProperty.DiadocUrl, sert.Crypt);


            sert.Api.SetProxyCredentials(ProjectProperty.ProxyLogin, ProjectProperty.ProxyPass);
            sert.Api.SetProxyUri(ProjectProperty.ProxyUri);

            try
            {
                sert.AuthTokenCert = sert.Api.Authenticate(sert.FileContent);
                sert.Inn = row["Inn"].ToString().Replace(" ", "");
                sert.Kpp = row["Kpp"].ToString().Replace(" ", "");
                sert.BoxId = sert.Api.GetOrganizationByInnKpp(sert.Inn, sert.Kpp).Boxes[0].BoxId;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "!!!!";
        }
    }
}
