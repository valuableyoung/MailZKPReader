using ALogic.DBConnector;
using ALogic.Logic.Heplers;
using ALogic.Logic.Reload1C;
using ALogic.Logic.SPR;
using ALogic.Logic.SPR.Old;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.Headers;
using Limilabs.Mail.MIME;
using SevenZip;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace ALogic.Logic.Old.Parsers
{
    public static class MailPriceReader
    {
        static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        static CancellationToken token = cancelTokenSource.Token;

        static string currentMail = "";
        static string currentPassword = "";

        public enum MailType
        {
            Simple = 1,
            A1 = 2
        }

        static void SetCurrentMailParams(MailType mailType)
        {
            switch (mailType)
            {
                case MailType.Simple:
                    currentMail = ProjectProperty.MailUserPriceParser;
                    currentPassword = ProjectProperty.MailUserPriceParserPassword;
                    break;
                case MailType.A1:
                    currentMail = ProjectProperty.MailUserPriceParserA1;
                    currentPassword = ProjectProperty.MailUserPriceParserPasswordA1;
                    break;
            }
        }

        static string GetAdresFromMailA1(string messContext, ref string address)
        {
            Regex regex = new Regex(@"&lt;\D*\d*@\D*\d*\.\D*\d*&gt;", RegexOptions.IgnoreCase);

            if (regex.IsMatch(messContext))
            {
                MatchCollection matches = regex.Matches(messContext);
                return address = matches[0].ToString().Replace(@"&lt;", "").Replace(@"&gt;", "");
            }
            return address = "";
        }

        static List<ParserSettings> GetSettingOfMailByParams(MailType mailType, string address, out ParserSettings setting, string subject = "")
        {
            List<ParserSettings> settings;

            switch (mailType)
            {
                case MailType.Simple:
                    settings = ParserSettingsHelper.GetCurrentSettingByEmail(address, subject);
                    setting = settings.FirstOrDefault();
                    return settings;

                case MailType.A1:
                    settings = ParserSettingsHelper.GetCurrentSettingByEmailA1(address);
                    setting = settings.FirstOrDefault();
                    return settings;

            }
            setting = null;
            return null;
        }


        public static void Start(MailType mailType)
        {
            int err = 0;

            SetCurrentMailParams(mailType);

            //if (DateTime.Now.Hour < 4)
            //{
            //    UniLogger.WriteLog("", 0, "сплю...");
            //    RobotAnswer.SendAnswer(1);
            //    return;
            //}

            //Загрузка некоторых нестандартных прайсов
            LoadOneForDay();

            //Logger.ShowMessage("Подключаюсь к почте...");
            UniLogger.WriteLog("", 0, "подключаюсь к почте");
            var imap = new Limilabs.Client.IMAP.Imap();
            try
            {
                imap.Connect(ProjectProperty.MailServer);
                //imap.Login(ProjectProperty.MailUserPriceParser, ProjectProperty.MailUserPriceParserPassword);
                imap.Login(currentMail, currentPassword);
            }
            catch (Exception ex)
            {
                err++;
                //Logger.SendMessage("Неудачное подключение к почтовому серверу. Строка для программиста: " + ex.Message);
                UniLogger.WriteLog("", 1, "Неудачное подключение к почтовому серверу. Строка для программиста: " + ex.Message);
                Logger.WriteErrorMessage("Неудачное подключение к почтовому серверу. Строка для программиста: " + ex.Message);
                return;
            }
            imap.Select("Inbox");
            UniLogger.WriteLog("", 0, "выбор рабочей папки inbox. загружаю сообщения...");
            //Logger.ShowMessage("Загружаю сообщения...");
            int countForParser = Convert.ToInt32(ProjectProperty.MailCountForParser);
            var collectionMess = imap.GetAll().Take(countForParser);
            
            UniLogger.WriteLog("", 0, "загружено сообщений: " + collectionMess.Count());
            //Logger.ShowMessage("Сообщений загружено: " + collectionMess.Count());
            try
            {
                int cnt = 0;
                foreach (var idMess in collectionMess)
                {
                    var mess = new MailBuilder().CreateFromEml(imap.GetMessageByUID(idMess));

                    cnt++;

                    var address = mess.From[0].Address;
                    address = Regex.Replace(address, "(?i)['<>]", "");

                    var subject = mess.Subject;  //ins 21.01.22 Semenkina Считываем Тему письма для идентификации по маске кода Склада поставщика
                    subject = Regex.Replace(subject, "(?i)['<>]", "");

                    //address = "price@rossko.ru"; //----------------------------------------------------------------------------------------------ins 21.01.22 Semenkina для ТЕСТА, УДАЛИТЬ!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //subject = "testMoskvrtrtrtwewew";// УДАЛИТЬ!!!!!!!!!!!

                    //Logger.ShowMessage("Получено письмо: " + mess.From[0].Name + " от: " + address);
                    UniLogger.WriteLog("", 0, $"Получено письмо: " + mess.From[0].Name + " от: " + address + $" {cnt} из {collectionMess.Count()}");

                    //ins 21.01.22 Semenkina Тема письма subject добавлена в параметры - для определения Склада по Маске
                    //var settings = ParserSettingsHelper.GetCurrentSettingByEmail(address, subject);

                    var setting = new ParserSettings();
                    List<ParserSettings> settings;
                    //upd 16.01.2023 тема письма для складов по маске
                    settings = GetSettingOfMailByParams(mailType, address, out setting, subject);


                    setting = settings.FirstOrDefault();
                    if (setting == null)
                    {
                        UniLogger.WriteLog("", 0, "По данному письму не было найдено сопоставление, письмо перемещено в папку Прочие");
                        imap.MoveByUID(idMess, ProjectProperty.FolderForSimpleMessage);
                        continue;
                    }

                    //Logger.ShowMessage("Сопоставлен контрагент ID=" + setting.idSupplier.ToString());

                    //change 21.01.22 Semenkina Добавлен код Склада поставщика
                    // UniLogger.WriteLog("", 0, "Сопоставлен контрагент ID=" + setting.idSupplier.ToString());
                    UniLogger.WriteLog("", 0, "Сопоставлен контрагент ID=" + setting.idSupplier.ToString() + ", ID Склада = " + setting.idSkladSupplier.ToString());
                    //end 21.01.22 Semenkina

                    if (setting.fAutoload != 1)
                    {
                        UniLogger.WriteLog("", 0, "Автозагрузка прайса была отключена. Парсинг не произведен!!!");
                        imap.MoveByUID(idMess, ProjectProperty.FolderForSimpleMessage);
                        continue;
                    }
                    try
                    {
                        bool parceAnuFile = false;
                        var attachments = mess.Attachments.ToList();
                        if (attachments.Count > 0)
                        {
                            UniLogger.WriteLog("", 0, "обнаружено вложений: " + attachments.Count.ToString());
                        }
                        else
                        {
                            UniLogger.WriteLog("", 1, "нет вложений!!!, обработка письма закончена");
                        }

                        foreach (var attachment in attachments)
                        {
                            setting = settings.FirstOrDefault();
                            UniLogger.WriteLog("", 0, "Чтение содержимого письма");
                            if (attachment.FileName == null)
                            {
                                UniLogger.WriteLog("", 0, "некорректное имя файла вложения");
                                continue;
                            }

                            if (setting.MailFileMask != "")
                            {
                                setting = settings.FirstOrDefault(p => attachment.FileName.ToLower().IndexOf(p.MailFileMask.ToLower()) != -1);
                                if (setting == null)
                                {
                                    UniLogger.WriteLog("", 0, "некорректная маска файла");
                                    continue;
                                }
                            }

                            string filePath = ProjectProperty.FolderXls + "\\" + attachment.FileName + (attachment.FileName.IndexOf('.') == -1 ? ".zip" : "");

                            attachment.Save(filePath);
                            UniLogger.WriteLog("", 0, "Сохранен файл " + filePath + " Начинаю парсинг файла.");

                            var extension = filePath.Split('.').Last();
                            
                           string arhievePath = "";
                            if (extension == "zip" || extension == "rar")
                            {
                                SevenZipExtractor.SetLibraryPath(ProjectProperty.PathTo7Zip);
                                SevenZip.SevenZipExtractor ex = new SevenZipExtractor(filePath);

                                arhievePath = ProjectProperty.FolderXls + "\\" + DateTime.Now.ToString().Replace(".", "").Replace(":", "");
                                ex.ExtractArchive(arhievePath);

                                File.Delete(filePath);
                                var filenames = Directory.GetFiles(arhievePath);
                                filePath = filenames.First();
                            }

                            extension = filePath.Split('.').Last().ToLower();
                            UniLogger.WriteLog("", 0, "Расширение файла: " + extension);

                            if (extension != "xls" && extension != "xlsx" && extension != "xlsm" && extension != "csv" && extension != "txt")
                            {
                                UniLogger.WriteLog("", 0, "некорректное расширение файла");
                                continue;
                            }

                            DataTableCollection dtParceFile = ReadFileAllFormat(filePath, setting);
                            
                            //UniLogger.WriteLog("", 0, "файл прочитан, продолжаю...");
                            
                            if (dtParceFile == null)
                            {
                                UniLogger.WriteLog("", 1, "ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. ID_KONTR=" + setting.idSupplier.ToString());
                                Logger.WriteErrorMessage("ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. ID_KONTR=" + setting.idSupplier.ToString());
                                File.Delete(filePath);
                                continue;
                            }

                            SaveDataTableWithParams(dtParceFile, setting, ProjectProperty.BotUserId);
                            
                            GC.Collect();
                            int pfcnt = dtParceFile[0].Rows.Count;
                            dtParceFile.Clear();
                            //DBMailProperty.UpdatePriceOnlineTemp(ProjectProperty.BotUserId, setting.idParserSettings, dtParceFile[0].Rows.Count);
                            DBMailProperty.UpdatePriceOnlineTemp(ProjectProperty.BotUserId, setting.idParserSettings, pfcnt, setting.fCompetitorType);

                            parceAnuFile = true;
                            UniLogger.WriteLog("", 0, "Парсинг выполнен, файл будет удален.");
                            File.Delete(filePath);
                            if (arhievePath != "")
                                Directory.Delete(arhievePath, true);

                            break;
                        }

                        if (parceAnuFile)
                            imap.MoveByUID(idMess, ProjectProperty.FolderForReadedMessages);
                        else
                            imap.MoveByUID(idMess, ProjectProperty.FolderForSimpleMessage);
                        //UniLogger.WriteLog("", 0, "письмо переместили в обработанные");
                    }
                    catch (Exception ex)
                    {
                        err++;

                        imap.MoveByUID(idMess, ProjectProperty.FolderForErrorMessage);
                        UniLogger.WriteLog("", 1, "При чтении файла возникли ошибки. Idkontr=" + setting.idSupplier + " " + mess.From[0].Name + " от: "
                                                        + address + " Строка для программиста: " + (ex.Message.Length > 500 ? ex.Message.Remove(500) : ex.Message));
                        Logger.WriteErrorMessage("При чтении файла возникли ошибки. Idkontr=" + setting.idSupplier + " " + mess.From[0].Name + " от: "
                                                        + address + " Строка для программиста: " + (ex.Message.Length > 500 ? ex.Message.Remove(500) : ex.Message));
                        continue;
                    }
                }
                imap.Close();
                RobotAnswer.SendAnswer(1);
               
            }
            catch (Exception exa)
            {
                err++;
                Logger.WriteErrorMessage("Ошибка метода Start класс  MailPriceReader: " + exa.Message);
            }
            finally
            {
                UniLogger.WriteLog("", 0, "Работа закончена...");
                //Logger.WriteMessage();
                UniLogger.Flush();
                
                GC.Collect();
            }
        }

        public static void LoadOneForDay()
        {
            //change Smolyanina 28.03.2022 забираем с ftp 4 раза
            bool load = false;

            if ((DateTime.Now.TimeOfDay >= new TimeSpan(6, 30, 0)) && (DateTime.Now.TimeOfDay < new TimeSpan(7, 0, 0)))
                load = true;
            if ((DateTime.Now.TimeOfDay >= new TimeSpan(11, 30, 0)) && (DateTime.Now.TimeOfDay < new TimeSpan(12, 0, 0)))
                load = true;
            if ((DateTime.Now.TimeOfDay >= new TimeSpan(15, 30, 0)) && (DateTime.Now.TimeOfDay < new TimeSpan(16, 0, 0)))
                load = true;
            if ((DateTime.Now.TimeOfDay >= new TimeSpan(20, 30, 0)) && (DateTime.Now.TimeOfDay < new TimeSpan(21, 0, 0)))
                load = true;

            if (load)
            {
                UniLogger.WriteLog("", 0, "Начата загрузка файла по времени : " + DateTime.Now.TimeOfDay.ToString());

                var listSettings = ParserSettingsHelper.GetALLSettingsFTP();

                foreach (var setting in listSettings)
                    LoadSettingsFTP(setting);
            }

            //if ((DateTime.Now.TimeOfDay >= new TimeSpan(4, 0, 0)) && (DateTime.Now.TimeOfDay < new TimeSpan(5, 0, 0)))
            //{
            //    var listSettings = ParserSettingsHelper.GetALLSettingsFTP();

            //    foreach (var setting in listSettings)
            //        LoadSettingsFTP(setting);
            //}
        }

        private static void LoadSettingsFTP(ParserSettings setting)
        {
            try
            {
                UniLogger.WriteLog("", 0, "Загрузка файла по настройке: " + setting.nParserSettings);
                DataTableCollection table = ReadFileForFTP(setting);
                UniLogger.WriteLog("", 0, "Сохранение в базу данных");
                
                SaveDataTableWithParams(table, setting, ProjectProperty.BotUserId);
                UniLogger.WriteLog("", 0, "Сопоставление артикулов и брендов");
                DBMailProperty.UpdatePriceOnlineTemp(ProjectProperty.BotUserId, setting.idParserSettings, table[0].Rows.Count, setting.fCompetitorType);
                UniLogger.WriteLog("", 0, "Успешно загружен");
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("", 0, "При загрузке возникла ошибка: " + (ex.Message.Length > 500 ? ex.Message.Remove(500) : ex.Message));
            }
            
        }

        private static DataTableCollection ReadFileForFTP(ParserSettings setting)
        {
            /*WebProxy pinfo = new WebProxy(); //Это переменная параметров.
            pinfo.Address = new Uri(@"http://192.168.1.7:3128");
            pinfo.Credentials = new NetworkCredential("EasZakTov", "ddfi3)es");
            */

            FtpWebRequest frpWebRequest = (FtpWebRequest)FtpWebRequest.Create(setting.FtpServer);
            //frpWebRequest.Credentials = new NetworkCredential(setting.FtpLogin, setting.FtpPass);
            var llogin = setting.FtpPass;
            var lpass = setting.FtpLogin;
            frpWebRequest.Credentials = new NetworkCredential(llogin, lpass);
            frpWebRequest.KeepAlive = true;
            frpWebRequest.UsePassive = true;
            frpWebRequest.UseBinary = true;
            frpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            //frpWebRequest.Proxy = pinfo;
            frpWebRequest.Proxy = null;

            FtpWebResponse response = (FtpWebResponse)frpWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            List<byte> list = new List<byte>();
            int b;


            while ((b = stream.ReadByte()) != -1)
                list.Add((byte)b);

            var fileWay = ProjectProperty.FolderXls + "\\" + "StokParts.txt";
            File.WriteAllBytes(fileWay, list.ToArray());
            UniLogger.WriteLog("", 0, "файл parts_major.txt загружен");
            var table = ReadFileAllFormat(fileWay, setting);

            File.Delete(fileWay);
            return table;
        }

        public static void ReadSingleFile(string filePath, ParserSettings setting, int UserId, DateTime datePrice)
        {
            try
            {
                Messenger.Send("Выполняется парсинг файла", 0);
                DataTableCollection dtParceFile = ReadFileAllFormat(filePath, setting);

                if (dtParceFile == null)
                {
                    Messenger.Send("ВНИМАНИЕ! Не удалось конвертировать файл в таблицу", -1);
                    Logger.WriteErrorMessage("Метод ReadSingleFile - ВНИМАНИЕ! Не удалось конвертировать файл в таблицу");
                    return;
                }

                Messenger.Send("Сохранение данных в базу данных", 10);
                SaveDataTableWithParams(dtParceFile, setting, UserId, datePrice);
                Messenger.Send("Сопоставление артикулов и брендов", 20);

                DBMailProperty.UpdatePriceOnlineTemp(UserId, setting.idParserSettings, dtParceFile[0].Rows.Count, setting.fCompetitorType);


                if (setting.fArmRtk == 1)
                {
                    var brands = DBSprBrend.GetBrandsById(setting.idSupplier);
                    foreach (var item in brands)
                    {
                        SqlParameter IdBrand1 = new SqlParameter("idBrand", item.Id);
                        SqlParameter IdBrand2 = new SqlParameter("idBrand", item.Id);
                        /*
                        DBExecutor.ExecuteQuery("exec [dbo].up_RecalcPriceA @idBrand", IdBrand1);
                        DBExecutor.ExecuteQuery("exec [dbo].up_ApplyPriceA @idBrand", IdBrand2);
                        */
                        var idkontrtitle = DBExecutor.SelectSchalar("select cast(idKontrTitle as int) from rKontrTitleTm (nolock) where idTm = " + item.Id.ToString());

                        //DBExecutor.ExecuteQuery("exec [dbo].up_CalcPriceAFraction @idKontrTitle, @idtm", new SqlParameter("idKontrTitle", (idkontrtitle)), new SqlParameter("idtm", item.Id));
                        DBExecutor.ExecuteQuery("exec [dbo].up_ApplyPriceAFraction @idBrand, @idKontrTitle", new SqlParameter("idBrand", item.Id), new SqlParameter("idKontrTitle", (int)idkontrtitle));
                    }
                }



                Messenger.Send("Завершено", 30);
                var row = DBParserSettings.GetLogForKontr(setting.idSupplier);
                var str = "Количество строк в файле: " + row["kolInFile"].ToString() + '\n';
                str += "Загружено в базу данных: " + row["kolInBase"].ToString() + '\n';
                str += "Не распознан бренд: " + row["kolNotBrend"].ToString() + '\n';
                str += "Не распознан артикул: " + row["kolNotArt"].ToString() + '\n';
                str += "Не корректная цена: " + row["kolBadPrice"].ToString() + '\n';
                str += "Не корректное количество: " + row["kolBadKol"].ToString() + '\n';
                str += "Строк загружено для Заказного товара: " + row["kolInPriceOnline"].ToString() + '\n';
                str += "Строк загружено для АРМ РТК: " + row["kolInRTK"].ToString() + '\n';
                str += "Строк загружено для Ценообразования: " + row["kolInCompetitor"].ToString() + '\n';
                str += "Строк загружено для Корректировочного коеффициента: " + row["kolInCorrKoeff"].ToString() + '\n';
                MessageBox.Show(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        public static DataTableCollection ReadFileAllFormat(string filePath, ParserSettings setting)
        {
            UniLogger.WriteLog("", 0, "Флаг ручной загрузки: " + setting.fHardLoad.ToString());
            if (setting.fHardLoad == 1)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); //создаем COM-объект Excel
                excel.Visible = true;// false; //true - показывать / false - не показывать приложения Excel.

                Microsoft.Office.Interop.Excel.Workbook fileExcel = excel.Workbooks.Open(filePath); //Открыть существующую книгу Excel

                // string newWay = filePath.Remove(filePath.Length - 5) + ".txt";

                //fileExcel.SaveAs(newWay, Microsoft.Office.Interop.Excel.XlFileFormat.xlTextWindows);

                fileExcel.Save();
                fileExcel.Saved = true;
                fileExcel.Close();
                excel.Quit(); //Закрытие приложения Excel.

                //Обнуляем созданые объекты
                fileExcel = null;
                excel = null;

                //Вызываем сборщик мусора для их уничтожения и освобождения памяти
                GC.Collect();
                //filePath = newWay;
            }

            var extension = filePath.Split('.').Last();

            var result = FileReader.Read(filePath, setting.Separator);
            if (result == null)
            {
                Logger.ShowMessage("Файл имеет неизвестное расширение " + extension);
                return null;
            }
            return result;
        }

        public static void SaveDataTableWithParams(DataTableCollection dataTable, ParserSettings setting, int UserId, DateTime? date = null, bool fFuture = false)
        {
            DBDate.Date = DateTime.Now.ToUniversalTime();
            int wildcnt = 0;

            //bool fToFuture = date == null || date.Value.Date <= DateTime.Now.Date ? false : true;
            //bool fToFuture = false;
            //upd 01.03.2023 принудительно добавлен флаг загрузки прайса со сроком актуальности
            bool fToFuture = fFuture;

            for (int sheetn = 0; sheetn <= setting.parserSettingsColumns.Max(p => p.sheetN); sheetn++)
            {
                if (dataTable.Count <= sheetn)
                    return;
                int shtncnt = dataTable[sheetn].Rows.Count;
                //for (int i = setting.StartRow - 1; i < dataTable[sheetn].Rows.Count; i++)
                for (int i = setting.StartRow - 1; i < shtncnt; i++)
                {
                    string query = fToFuture ? ParserSettingsHelper.GetHeadStrParserSaveDataFuture(setting, UserId, sheetn, date.Value) :
                    ParserSettingsHelper.GetHeadStrParserSaveData(setting, UserId, sheetn);

                    bool correctValue = true;

                    foreach (var elem in setting.parserSettingsColumns.Where(p => p.TypeColumn != TypeParserSettingsColumn.Бренд && p.sheetN == sheetn))
                    {
                        string value = dataTable[sheetn].Rows[i][elem.ColumnNumber].ToString();
                        value = value.Replace("\"", "").Replace((char)160, ' ').Replace("--", "").Trim();

                        if (elem.TypeColumn == TypeParserSettingsColumn.Наименование)
                        {
                            value = value.Replace(' ', ' ').Trim();
                        }

                        if (elem.TypeColumn == TypeParserSettingsColumn.Количество || elem.TypeColumn == TypeParserSettingsColumn.Цена
                                || elem.TypeColumn == TypeParserSettingsColumn.РекЦена || elem.TypeColumn == TypeParserSettingsColumn.МинПартия)
                        {
                             // del Smolyanina 23.04.2021 Бренд партс парсится с миллионными ценами
                            //if (value.IndexOf(',') < value.Length - 3 && value.IndexOf(',') >= 0)
                            //    value = value.Replace(",", "");
                            value = value.Replace("+","").Replace(" ", "").Replace("руб.", "").Replace("р.", "").Replace(",", ".").Replace("<", "").Replace(">", "").Replace(((char)160).ToString(), "").Split('-').First();
                            int countPoint = value.Count(q => q == '.');
                            for (int ind = 1; ind < countPoint; ind++)
                                value = value.Remove(value.IndexOf('.'), 1).Trim();

                            if (countPoint > 0 && (elem.TypeColumn == TypeParserSettingsColumn.Количество || elem.TypeColumn == TypeParserSettingsColumn.МинПартия))
                            {
                                value = value.Remove(value.IndexOf('.')).Trim();
                            }

                            if (elem.TypeColumn == TypeParserSettingsColumn.МинПартия)
                            {
                                value = (value.Length == 0) ? "1" : value.Trim();
                            }

                            decimal test;
                            //if (!decimal.TryParse(value, out test))

                            if (!decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out test))
                            {
                                correctValue = false;
                            }
                            else
                            {
                                if (elem.TypeColumn == TypeParserSettingsColumn.Цена)
                                {
                                    if (setting.fNds == 1)
                                        test = test * (decimal)1.2;

                                    if (setting.PricePercent != 0)
                                        test = test + test * (decimal)setting.PricePercent / 100;

                                    //value = test.ToString();
                                    value = test.ToString().Replace(",", ".").Trim();
                                }
                            }
                        }
                        else
                        {
                            if (value.Length > 1 && value[0] == ' ')
                                value = value.Remove(0, 1);
                            if (value.Length > 80)
                                value = value.Remove(80);

                            value = value.Replace("'", "''").Trim();
                        }

                        if (elem.TypeColumn == TypeParserSettingsColumn.ABC)
                        {
                            if (value.Trim().Length > 5)
                            {
                                value = value.Trim().Substring(0, 5);
                            }
                            else
                            {
                                value = value.Trim();
                            }

                            //switch (value.ToLower())
                            //{
                            //    case "a": value = "Aa"; break;
                            //    case "b": value = "Ba"; break;
                            //    case "c": value = "Ca"; break;
                            //    case "d": value = ""; break;
                            //    case "arc": value = ""; break;
                            //    case "out": value = ""; break;
                            //    case "new": value = "Zz"; break;
                            //    case "new*": value = "Zz"; break;
                            //}
                        }

                        if (value == "")
                            correctValue = false;

                        query += "'" + value + "',";
                    }

                    if (setting.fBrend == 1)
                        //query += "'" + setting.nBrend.Replace(((Char)39).ToString(), "");
                        query += "'" + setting.nBrend.Replace(((Char)39).ToString(), "") + "', '" + setting.nBrend.Replace(((Char)39).ToString(), "").Trim();
                    else
                    {
                        var brend = dataTable[sheetn].Rows[i][setting.parserSettingsColumns.First(p => p.TypeColumn == TypeParserSettingsColumn.Бренд).ColumnNumber].ToString().Replace("\"", "").Replace((char)160, ' ').Replace("'", "").Replace("--", "").Trim();
                        if (brend.Length > 49)
                            brend = brend.Remove(49);
                        //query += "'" + brend;
                        query += "'" + brend.Trim() + "', '" + brend.Trim();
                    }
                    query += "')" + '\n';

                    wildcnt++;
                    if (wildcnt % 10000 == 0)
                    {
                        GC.Collect();
                    }

                    if (query.Length == 0)
                    {
                        UniLogger.WriteLog("", 1, "пустой запрос для вставки данных во временную таблицу");
                    }

                    if (correctValue)
                        DBGroupSaver.AddRow(query);

                }
                UniLogger.WriteLog("", 0, "сформировано строк для вставки во временную таблицу: " + wildcnt.ToString());
                DBGroupSaver.SaveAll();
            }
        }
    }
}
