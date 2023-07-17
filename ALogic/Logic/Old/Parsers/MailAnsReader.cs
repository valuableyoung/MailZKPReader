using ALogic.DBConnector;
using ALogic.Logic.Heplers;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using SevenZip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ALogic.SMTP;


namespace ALogic.Logic.Old.Parsers
{
    public static class MailAnsReader
    {
        public static void Start()
        {
            int errcnt = 0;
            Logger.ShowMessage("Подключаюсь к базе данных...");
            DataTable dtSeMailKontr = DBMailProperty.SelectSeMailKontrForAnswer();
            if (dtSeMailKontr == null)
            {
                Logger.ShowMessage("Неудачное подключение к БД.");
                return;
            }

            Logger.ShowMessage("Подключаюсь к почте...");
            var imap = new Limilabs.Client.IMAP.Imap();
            try
            {
                imap.Connect(ProjectProperty.MailServer);
                imap.Login(ProjectProperty.MailUserAnswer, ProjectProperty.MailUserAnswerPassword);
            }
            catch (Exception ex)
            {
                //Logger.SendMessage("Неудачное подключение к почтовому серверу. Строка для программиста: " + ex.Message);
                imap.Close();
                MailSMTPSend.SendEmail(ProjectProperty.MailUserAnswer, ProjectProperty.MailUserAnswerPassword, ProjectProperty.MailToAnswer, "Бот ответов поставщику : ошибка подключения", ex.Message);
                return;
            }
            imap.Select("Inbox");
            Logger.ShowMessage("Загружаю сообщения...");

            //тут бы вставить проверки что папки вообще существуют, а то ппц

            //var folders = imap.GetSubscribedFolders("INBOX");
            //считываем вообще все папки, ибо после ввода в строй нового почтовика возникли проблемы с проверкой папок
            var folders = imap.GetFolders(Limilabs.Client.IMAP.SubFolders.All);

            try
            {
                string ms = $"В почтовом ящике {ProjectProperty.MailServer} отсутствуют папки: ";
                int err = 0;
                var folder = folders.Where(p => p.Name.ToLower().Trim() == ProjectProperty.FolderForReadedMessages.ToLower().Trim()).ToList();
                if (folder.Count() == 0)
                {
                    err++;
                    ms += $" \n {ProjectProperty.FolderForReadedMessages} ";
                }

                folder = folders.Where(p => p.Name.ToLower().Trim() == ProjectProperty.FolderForErrorMessage.ToLower().Trim()).ToList();
                if (folder.Count() == 0)
                {
                    err++;
                    ms += $"\n {ProjectProperty.FolderForErrorMessage} ";
                }

                folder = folders.Where(p => p.Name.ToLower().Trim() == ProjectProperty.FolderForSimpleMessage.ToLower().Trim()).ToList();
                if (folder.Count() == 0)
                {
                    err++;
                    ms += $"\n {ProjectProperty.FolderForSimpleMessage} ";
                }

                folder = folders.Where(p => p.Name.ToLower().Trim() == ProjectProperty.FolderForHandMessage.ToLower().Trim()).ToList();
                if (folder.Count() == 0)
                {
                    err++;
                    ms += $"\n {ProjectProperty.FolderForHandMessage} ";
                }

                if (err > 0)
                {
                    MailSMTPSend.SendEmail(ProjectProperty.MailUserAnswer, ProjectProperty.MailUserAnswerPassword, ProjectProperty.MailToAnswer, "Бот ответов поставщику : ошибка проверки папок в ящике", ms);
                    imap.Close();
                    
                    return;
                }
            }
           catch(Exception ex)
           {
                imap.Close();
                MailSMTPSend.SendEmail(ProjectProperty.MailUserAnswer, ProjectProperty.MailUserAnswerPassword, ProjectProperty.MailToAnswer, "Бот ответов поставщику : ошибка подключения к папкам", ex.Message);
                return;
           }
            //провекрки закончены
            var collectionMess = imap.GetAll();
            Logger.ShowMessage("Сообщений загружено: " + collectionMess.Count());
            try
            {
                foreach (var idMess in collectionMess)
                {
                    var mess = new MailBuilder().CreateFromEml(imap.GetMessageByUID(idMess));
                    var address = mess.From[0].Address;
                    Logger.ShowMessage("Получено письмо: " + mess.From[0].Name + " от: " + address);

                    if (address == "OrderFromSale@arkona36.ru")
                    {
                        imap.MarkMessageUnseenByUID(idMess);
                        imap.MoveByUID(idMess, ProjectProperty.FolderForErrorMessage);
                        continue;
                    }

                    if (address.ToLower() == ProjectProperty.MailUserAnswer.ToLower())
                    {
                        Logger.ShowMessage("Обнаружено письмо робота. Перемещаем в специальную папку");
                        imap.MarkMessageUnseenByUID(idMess);
                        imap.MoveByUID(idMess, "Отправленные ботом сообщения");
                        continue;
                    }

                    if (mess.Attachments.Count == 0)
                    {
                        Logger.ShowMessage("У письма отсутствует прикрепленный файл, письмо перемещено в папку Прочие");
                        imap.MarkMessageUnseenByUID(idMess);
                        imap.MoveByUID(idMess, ProjectProperty.FolderForHandMessage);
                        errcnt++;
                        continue;
                    }
                    
                    var tableKontrProp = dtSeMailKontr.AsEnumerable().Where(p => p["eMail"].ToString().ToLower().Trim() == address.ToLower().Trim()).ToList();
                    DataRow kontrProp = null;
                    MimeData attachment = null;

                    foreach (var row in tableKontrProp.AsEnumerable())
                    {
                        string fileMask = row["nFileMask"] == DBNull.Value ? "" : row["nFileMask"].ToString();
                        string subjectMask = row["nSubjectMask"] == DBNull.Value ? "" : row["nSubjectMask"].ToString();

                        foreach (var xatt in mess.Attachments)
                        {
                            var att = xatt;
                            if (att == null)
                            {
                                errcnt++;
                                continue;
                            }

                            if (att.FileName == null)
                            {
                                try
                                {
                                    att = ((Limilabs.Mail.MIME.MimeRfc822)att).Message.Attachments[0];
                                }
                                catch
                                {
                                    errcnt++;
                                    continue;
                                }
                            }

                            if (fileMask != "" && att.FileName.IndexOf(fileMask) == -1)
                            {
                                errcnt++;
                                continue;
                            }

                            var ext = att.FileName.Split('.').Last().ToLower();
                            if (ext != "zip" && ext != "rar" && ext != "xls" && ext != "xlsx" && ext != "xlsm" && ext != "csv" && ext != "txt")
                            {
                                errcnt++;
                                continue;
                            }

                            attachment = att;
                            kontrProp = row;
                            break;
                        }

                        if (attachment != null)
                            break;
                    }

                    if (kontrProp == null)
                    {
                        Logger.ShowMessage("По данному письму не было найдено сопоставление, письмо перемещено в папку Прочие");
                        imap.MarkMessageUnseenByUID(idMess);
                        imap.MoveByUID(idMess, ProjectProperty.FolderForHandMessage);
                        continue;
                    }

                    var idKontr = kontrProp["idKontr"];
                    Logger.ShowMessage("Сопоставлен контрагент ID=" + idKontr.ToString());

                    try
                    {
                        Logger.ShowMessage("Чтение содержимого письма");
                        string filePath = ProjectProperty.FolderXls + "\\" + attachment.FileName.Replace("\\", "").Replace("/", "") + (attachment.FileName.IndexOf('.') == -1 ? ".zip" : "");

                        attachment.Save(filePath);
                        Logger.ShowMessage("Сохранен файл " + filePath + " Начинаю парсинг файла.");
                        if (attachment.Size == 0)
                        {
                            Logger.WriteErrorMessage("Пустой файл во вложении либо ошибка сохранения файла. Контрагент ID = " + idKontr.ToString());
                        }

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

                        if (extension != "xls" && extension != "xlsx" && extension != "xlsm" && extension != "csv" && extension != "txt")
                        {
                            Logger.ShowMessage("Файл с данным расширением не обрабатывается. Письмо перемещено для ручной обработки");
                            File.Delete(filePath);
                            imap.MarkMessageUnseenByUID(idMess);
                            imap.MoveByUID(idMess, ProjectProperty.FolderForHandMessage);
                            continue;
                        }

                        int idTypeEdi = int.Parse(kontrProp["idTypeEdi"].ToString());

                        var dtParceFile = FileReader.Read(filePath);

                        if (dtParceFile == null || dtParceFile.Count == 0)
                        {
                            Logger.SendMessage("ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. ID_KONTR=" + idKontr.ToString());
                            Logger.WriteErrorMessage("ВНИМАНИЕ! Не удалось конвертировать файл в таблицу. ID_KONTR=" + idKontr.ToString());
                            File.Delete(filePath);
                            imap.MarkMessageUnseenByUID(idMess);
                            imap.MoveByUID(idMess, ProjectProperty.FolderForErrorMessage);
                            errcnt++;
                            continue;
                        }

                        //если внутри метода не было ошибок - письмо перемещаем в Loaded, иначе в Handwork
                        if (SupplierAnswer.LoadTableAns(dtParceFile, (int)idKontr, idTypeEdi, filePath))
                        {
                            Logger.ShowMessage("Парсинг выполнен успешно, файл будет удален.");
                            File.Delete(filePath);

                            if (arhievePath != "")
                                Directory.Delete(arhievePath, true);
                            imap.MarkMessageUnseenByUID(idMess);
                            imap.MoveByUID(idMess, ProjectProperty.FolderForReadedMessages);
                        }
                        else
                        {
                            File.Delete(filePath);

                            if (arhievePath != "")
                                Directory.Delete(arhievePath, true);
                            //Logger.ShowMessage("ВНИМАНИЕ! Парсинг НЕ выполнен, произошла ОШИБКА в методе LoadTableAns. Письмо перемещено в папку HandWork");
                            Logger.SendMessage("ВНИМАНИЕ! Парсинг НЕ выполнен, произошла ОШИБКА в методе LoadTableAns. Письмо перемещено в папку HandWork");
                            imap.MarkMessageUnseenByUID(idMess);
                            imap.MoveByUID(idMess, ProjectProperty.FolderForHandMessage);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        imap.MarkMessageUnseenByUID(idMess);
                        imap.MoveByUID(idMess, ProjectProperty.FolderForErrorMessage);
                        Logger.SendMessage("При чтении файла возникли ошибки. Idkontr=" + idKontr + " " + mess.From[0].Name + " от: "
                                                        + address + " Строка для программиста: " + (ex.Message.Length > 500 ? ex.Message.Remove(500) : ex.Message));
                        Logger.WriteErrorMessage("При чтении файла возникли ошибки. Idkontr=" + idKontr + " " + mess.From[0].Name + " от: "
                                                        + address + " Строка для программиста: " + (ex.Message.Length > 500 ? ex.Message.Remove(500) : ex.Message));
                        errcnt++;
                    }
                }
                //imap.Close();
                Logger.ShowMessage("Работа закончена...");
                Logger.WriteMessage();
            }
            catch (Exception exa)
            {
                Logger.WriteErrorMessage("Ошибка метода Start класс  MailAnsReader: " + exa.Message);
            }
            finally { imap.Close(); }
        }
    }


}
