using ALogic.DBConnector;
using ALogic.Logic.Heplers;
using ALogic.Logic.Reload1C;
using ALogic.Logic.SPR;
using ALogic.Logic.SPR.Old;
using ExcelLibrary.SpreadSheet;
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
    public static  class MailZKPReader
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
                    currentMail = ProjectProperty.MailUserZKPParser;
                    currentPassword = ProjectProperty.MailUserZKPParserPassword;
                    break;
                case MailType.A1:
                    currentMail = ProjectProperty.MailUserPriceParserA1;
                    currentPassword = ProjectProperty.MailUserPriceParserPasswordA1;
                    break;
            }
        }
        
        public static  string FindidZKP(string subject)
        {
            int startIndex = subject.IndexOf("№");
            string idZKP = subject.Substring(startIndex+1);
            if(idZKP != null)
            {
                return idZKP;
            }
            else
            {
                return "";
            }
        }

        public static string CheckParse(string ColumnVal,int flag)
        {
            if ((ColumnVal != null) && (ColumnVal != ""))
            {
                switch (flag)
                {
                    case 1: return ColumnVal; // Артикул

                    case 2:
                        {
                            int count = 0;
                            if (int.TryParse(ColumnVal, out count))
                            {
                                return ColumnVal;
                            }
                            else
                            {
                                return "";
                            }
                        }
                    default: return "";
                }                         
 
            }
            else { return ""; }
        }

        public static int CheckColumn (string StringVal)
        {
            if(StringVal == "")
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static DataTable FillDataTable(DataTableCollection dtcollection)
        {
            DataTable dtGetZKP = new DataTable();
            dtGetZKP.Columns.Add("idTovOEM");
            dtGetZKP.Columns.Add("needcount");
            dtGetZKP.Columns.Add("countSup");
            dtGetZKP.Columns.Add("priceSup");



            int checktemp = 0;
            foreach (DataTable dt in dtcollection)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {

                    string[] s = new string[4];
                    s[0] = CheckParse(dt.Rows[i][1].ToString(), 1);
                    s[1] = CheckParse(dt.Rows[i][3].ToString(), 2);
                    s[2] = CheckParse(dt.Rows[i][4].ToString(), 2);
                    s[3] = CheckParse(dt.Rows[i][5].ToString(), 2);
                    
                    checktemp += CheckColumn(s[0]);
                    checktemp += CheckColumn(s[1]);
                    checktemp += CheckColumn(s[2]);
                    checktemp += CheckColumn(s[3]);

                    if (checktemp == 4)
                    {
                        DataRow row = dtGetZKP.NewRow();
                        row["idTovOEM"] = s[0];
                        row["needcount"]= s[1];
                        row["countSup"] = s[2];
                        row["priceSup"] = s[3];
                        dtGetZKP.Rows.Add(row);
                    }
                    checktemp = 0;

                }

            }
            return dtGetZKP;
        }


        public static void ExportData(DataTable dt , int idZKP)
        {
            var vdatatable = dt;
            //string sql = $@"EXEC [dbo].[up_InsertReceiveZKP] {vdatatable} , {idZKP}";
            //DBExecutor.ExecuteQuery(sql);
            //using (var command = new SqlCommand("InsertTable") { CommandType = CommandType.StoredProcedure })
            //{
            //    var dt = new DataTable(); //create your own data table
            //    command.Parameters.Add(new SqlParameter("@myTableType", dt));
            //    SqlHelper.Exec(command);
            //}
            SqlParameter par = new SqlParameter("GetZKPIn", SqlDbType.Structured);
            par.Value = dt;
            par.TypeName = "GetZKP";
            SqlParameter par2 = new SqlParameter("idZKP", SqlDbType.Int);
            par2.Value = idZKP;
            int a = dt.Rows.Count;
            DBExecutor.ExeciteProcedure("up_InsertReceiveZKP", par, par2);

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
            //LoadOneForDay();

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
            imap.Select("Test"); //Inbox
             
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
                        var mess = new MailBuilder().CreateFromEml(imap.GetMessageByUID(idMess));//.Subject.Contains(ProjectProperty.SubjectSubstringZKP);
                        int count = collectionMess.Count();
                        string address, subject;
                        int idZKP = 0;
                        cnt++;
                        mess.Subject = "Re: Тема ЗКП11 №132";
                        if (mess.Subject != null) 
                        {
                        //if (mess.Subject.Contains(ProjectProperty.SubjectSubstringZKP))
                        //{
                        //    address = mess.From[0].Address;
                        //    address = Regex.Replace(address, "(?i)['<>]", "");

                        //    subject = mess.Subject;  //ins 21.01.22 Semenkina Считываем Тему письма для идентификации по маске кода Склада поставщика
                        //    subject = Regex.Replace(subject, "(?i)['<>]", "");

                        //    idZKP = int.Parse(FindidZKP(subject));  
                        //}
                        //else
                        //{
                        //    UniLogger.WriteLog("", 0, "По данному письму не было найдено сопоставление, письмо перемещено в папку Прочие");
                        //    imap.MoveByUID(idMess, ProjectProperty.FolderForSimpleMessage);
                        //    continue;
                        //}
                        idZKP = 138;
                        }
 




                        //address = "price@rossko.ru"; //----------------------------------------------------------------------------------------------ins 21.01.22 Semenkina для ТЕСТА, УДАЛИТЬ!!!!!!!!!!!!!!!!!!!!!!!!!!
                        //subject = "testMoskvrtrtrtwewew";// УДАЛИТЬ!!!!!!!!!!!

                        //Logger.ShowMessage("Получено письмо: " + mess.From[0].Name + " от: " + address);
                        // UniLogger.WriteLog("", 0, $"Получено письмо: " + mess.From[0].Name + " от: " + address + $" {cnt} из {collectionMess.Count()}");

                        //ins 21.01.22 Semenkina Тема письма subject добавлена в параметры - для определения Склада по Маске
                        //var settings = ParserSettingsHelper.GetCurrentSettingByEmail(address, subject);

                        //var setting = new ParserSettings();
                        //List<ParserSettings> settings;
                        //upd 16.01.2023 тема письма для складов по маске
                        // settings = GetSettingOfMailByParams(mailType, address, out setting, subject);


                        //setting = settings.FirstOrDefault();
                        //if (setting == null)
                        //{
                        //    UniLogger.WriteLog("", 0, "По данному письму не было найдено сопоставление, письмо перемещено в папку Прочие");
                        //    imap.MoveByUID(idMess, ProjectProperty.FolderForSimpleMessage);
                        //    continue;
                        //}

                        //Logger.ShowMessage("Сопоставлен контрагент ID=" + setting.idSupplier.ToString());

                        //change 21.01.22 Semenkina Добавлен код Склада поставщика
                        // UniLogger.WriteLog("", 0, "Сопоставлен контрагент ID=" + setting.idSupplier.ToString());
                        //UniLogger.WriteLog("", 0, "Сопоставлен контрагент ID=" + setting.idSupplier.ToString() + ", ID Склада = " + setting.idSkladSupplier.ToString());
                        //end 21.01.22 Semenkina

                        //if (setting.fAutoload != 1)
                        //{
                        //    UniLogger.WriteLog("", 0, "Автозагрузка прайса была отключена. Парсинг не произведен!!!");wfmain
                        //    imap.MoveByUID(idMess, ProjectProperty.FolderForSimpleMessage);
                        //    continue;
                        //}
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
                                //setting = settings.FirstOrDefault();
                                UniLogger.WriteLog("", 0, "Чтение содержимого письма");
                                if (attachment.FileName == null)
                                {
                                    UniLogger.WriteLog("", 0, "некорректное имя файла вложения");
                                    continue;
                                }

                                //if (setting.MailFileMask != "")
                                //{
                                //    setting = settings.FirstOrDefault(p => attachment.FileName.ToLower().IndexOf(p.MailFileMask.ToLower()) != -1);
                                //    if (setting == null)
                                //    {
                                //        UniLogger.WriteLog("", 0, "некорректная маска файла");
                                //        continue;
                                //    }
                                //}

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

                                DataTableCollection dtParceFile = ReadFileAllFormat(filePath);

                                if (dtParceFile == null)
                                {
                                    UniLogger.WriteLog("", 1, "ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. IDZKP=" + idZKP.ToString());
                                    Logger.WriteErrorMessage("ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. IDZKP=" + idZKP.ToString());
                                    File.Delete(filePath);
                                    continue;
                                }
                                else
                                {
                                    DataTable GetZKP = FillDataTable(dtParceFile);
                                    ExportData(GetZKP, idZKP);
                                }
                                
                            


 
                        

                            

                                    //UniLogger.WriteLog("", 0, "файл прочитан, продолжаю...");

                                //    if (dtParceFile == null)
                                //{
                                //    UniLogger.WriteLog("", 1, "ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. ID_KONTR=" + setting.idSupplier.ToString());
                                //    Logger.WriteErrorMessage("ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. ID_KONTR=" + setting.idSupplier.ToString());
                                //    File.Delete(filePath);
                                //    continue;
                                //}

                                ////SaveDataTableWithParams(dtParceFile, setting, ProjectProperty.BotUserId);

                                GC.Collect();
                                //int pfcnt = dtParceFile[0].Rows.Count;
                                //dtParceFile.Clear();
                                ////DBMailProperty.UpdatePriceOnlineTemp(ProjectProperty.BotUserId, setting.idParserSettings, dtParceFile[0].Rows.Count);
                                //DBMailProperty.UpdatePriceOnlineTemp(ProjectProperty.BotUserId, setting.idParserSettings, pfcnt, setting.fCompetitorType);

                                parceAnuFile = true;
                                UniLogger.WriteLog("", 0, "Парсинг выполнен, файл будет удален.");
                                File.Delete(filePath);
                                //if (arhievePath != "")
                                    //Directory.Delete(arhievePath, true);

                                //break;
                        

                            if (parceAnuFile)
                                imap.MoveByUID(idMess, ProjectProperty.FolderForReadedMessages);
                            else
                                imap.MoveByUID(idMess, ProjectProperty.FolderForSimpleMessage);
                                UniLogger.WriteLog("", 0, "письмо переместили в обработанные");
                            }// foreach attachments 
                    }
                    catch (Exception ex)
                    {
                        err++;
                        MessageBox.Show(ex.Message + ex.StackTrace);

                        imap.MoveByUID(idMess, ProjectProperty.FolderForErrorMessage);
                        //UniLogger.WriteLog("", 1, "При чтении файла возникли ошибки. Idkontr=" + setting.idSupplier + " " + mess.From[0].Name + " от: "
                        //                                + address + " Строка для программиста: " + (ex.Message.Length > 500 ? ex.Message.Remove(500) : ex.Message));
                        //Logger.WriteErrorMessage("При чтении файла возникли ошибки. Idkontr=" + setting.idSupplier + " " + mess.From[0].Name + " от: "
                        //                                + address + " Строка для программиста: " + (ex.Message.Length > 500 ? ex.Message.Remove(500) : ex.Message));
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

        public static DataTableCollection ReadFileAllFormat(string filePath)   //ParserSettings setting
        {
            //UniLogger.WriteLog("", 0, "Флаг ручной загрузки: " + setting.fHardLoad.ToString());
            //if (setting.fHardLoad == 1)
            //{
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); //создаем COM-объект Excel
                excel.Visible = true;// false; //true - показывать / false - не показывать приложения Excel.

                Microsoft.Office.Interop.Excel.Workbook fileExcel = excel.Workbooks.Open(filePath); //Открыть существующую книгу Excel

                // string newWay = filePath.Remove(filePath.Length - 5) + ".txt";

                //fileExcel.SaveAs(newWay, Microsoft.Office.Interop.Excel.XlFileFormat.xlTextWindows);
                 
                fileExcel.Save();
             
                fileExcel.Saved = true;

                //FileStream fstream = new FileStream(filePath, FileMode.Open);
                //Workbook workbook = new Workbook();
                //workbook.Fill(fstream);
                //Worksheet worksheet = workbook.Worksheets[0];
                 
                fileExcel.Close();
                excel.Quit(); //Закрытие приложения Excel.

                //Обнуляем созданые объекты
                fileExcel = null;
                excel = null;

                //Вызываем сборщик мусора для их уничтожения и освобождения памяти
                GC.Collect();
                //filePath = newWay;
            //}

            var extension = filePath.Split('.').Last();

            var result = FileReader.Read(filePath, ""); //setting.Separator
            if (result == null)
            {
                Logger.ShowMessage("Файл имеет неизвестное расширение " + extension);
                return null;
            }
            return result;
        }
    }
}
