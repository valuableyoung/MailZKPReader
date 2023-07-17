using ALogic.Model.Entites.Invoice;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Diadoc.Api;
using ALogic.Logic.SPR;
using Diadoc.Api.DataXml.Utd820;
using System.IO;
using ALogic.Logic.Old;
using System.Xml.Serialization;

namespace ALogic.Logic.ADiadoc
{
    public static class DiadocFunc
    {       

        public static void RegSert(string Inn)
        {
            var info = new FnsRegistrationMessageInfo();

            info.AddCertificate(Sertificate.DctSertificate[Inn].FileContent);
         

            Sertificate.DctSertificate[Inn].Api.SendFnsRegistrationMessage(Sertificate.DctSertificate[Inn].AuthTokenCert, Sertificate.DctSertificate[Inn].BoxId, info);
        }

        //Отправить УПД в диадок
        public static bool SendUpdXml(object idDoc)
        {
            //Создаем документ Диадок
            string BoxId;
            var invoice = DocCreator.CreateUPD(idDoc, out BoxId);
            if (invoice == null)
                return false;
            
            DocCorrector.Correct(invoice);

            //Получим ИНН отправителя из документа
            string Inn = (invoice.Sellers[0].Item as ExtendedOrganizationDetails).Inn;

            //ALogic.Logic.Reload1C.UniLogger.WriteLog("", 0, "Inn: " + Inn);
            try
            {
                Logger.SendMessage("Генерируем файл с УПД для накладной ID = " + idDoc.ToString());
                
                //var file = Sertificate.DctSertificate[Inn].Api.GenerateUniversalTransferDocumentXmlForSeller(Sertificate.DctSertificate[Inn].AuthTokenCert, invoice);

                var file = Sertificate.DctSertificate[Inn].Api.GenerateSenderTitleXml
                   (Sertificate.DctSertificate[Inn].AuthTokenCert, Sertificate.DctSertificate[Inn].BoxId, "UniversalTransferDocument", "СЧФДОП","utd820_05_01_01", invoice.SerializeToXml());

                //Logger.WriteDebugMessage(invoice.ToString());

                Logger.SendMessage("Генерируем прикрепление к сообщению с подписью для накладной ID = " + idDoc.ToString());
                var messageAttachment = new XmlDocumentAttachment();
                messageAttachment.SignedContent = new SignedContent();
                messageAttachment.SignedContent.Content = file.Content;
                messageAttachment.SignedContent.Signature = Sertificate.DctSertificate[Inn].Crypt.Sign(file.Content, Sertificate.DctSertificate[Inn].FileContent);

                Logger.SendMessage("Генерируем само сообщение для накладной ID = " + idDoc.ToString());

                var messageToPost = new MessageToPost
                {
                    FromBoxId = Sertificate.DctSertificate[Inn].BoxId,
                    ToBoxId = BoxId,
                    UniversalTransferDocumentSellerTitles = { messageAttachment }

                };
                Logger.SendMessage("Отправка сообщения для накладной ID = " + idDoc.ToString());

                var messageAns = Sertificate.DctSertificate[Inn].Api.PostMessage(Sertificate.DctSertificate[Inn].AuthTokenCert, messageToPost);
                DBDiadoc.SaveMessageBuyer(idDoc, messageAns.MessageId, Inn);
                Logger.SendMessage("УПД №" + invoice.DocumentNumber + " от " + invoice.DocumentDate + " отправлено успешно.");
            }
            catch (Exception ex)
            {
                Logger.SendMessage("Не удалось отправить УПД №" + invoice.DocumentNumber + " от " + invoice.DocumentDate + " Строка для программиста: " + ex.Message);
                return false;
            }
            return true;
        }
        //Отправить УКД в диадок
        
        public static bool SendUKDXml(object idDoc)
        {
            //Создаем документ Диадок
            string BoxId;
            var invoice = DocCorrectCreator.CreateUKD(idDoc, out BoxId);
            if (invoice == null)
                return false;

            DocCorrectCorrector.Correct(invoice);

            //Получим ИНН отправителя из документа
            string Inn = invoice.Seller.Inn;

            //ALogic.Logic.Reload1C.UniLogger.WriteLog("", 0, "Inn: " + Inn);
            try
            {
                Logger.SendMessage("Генерируем файл с УПД для накладной ID = " + idDoc.ToString());
                var file = Sertificate.DctSertificate[Inn].Api.GenerateUniversalCorrectionDocumentXmlForSeller(Sertificate.DctSertificate[Inn].AuthTokenCert, invoice);

                //Logger.WriteDebugMessage(invoice.ToString());

                Logger.SendMessage("Генерируем прикрепление к сообщению с подписью для накладной ID = " + idDoc.ToString());
                var messageAttachment = new XmlDocumentAttachment
                {
                    SignedContent = new SignedContent
                    {
                        Content = file.Content,
                        //Подпись отправителя, см. "Как авторизоваться в системе"
                        Signature = Sertificate.DctSertificate[Inn].Crypt.Sign(file.Content, Sertificate.DctSertificate[Inn].FileContent)
                    }
                };

                Logger.SendMessage("Генерируем само сообщение для накладной ID = " + idDoc.ToString());

                var messageToPost = new MessageToPost
                {
                    FromBoxId = Sertificate.DctSertificate[Inn].BoxId,
                    ToBoxId = BoxId,
                    UniversalTransferDocumentSellerTitles = { messageAttachment }

                };
                Logger.SendMessage("Отправка сообщения для накладной ID = " + idDoc.ToString());

                var messageAns = Sertificate.DctSertificate[Inn].Api.PostMessage(Sertificate.DctSertificate[Inn].AuthTokenCert, messageToPost);
                DBDiadoc.SaveMessageBuyer(idDoc, messageAns.MessageId, Inn);
                Logger.SendMessage("УПД №" + invoice.DocumentNumber + " от " + invoice.DocumentDate + " Клиент: " + invoice.Buyer.OrgName + " ИНН: " + invoice.Buyer.Inn + " отправлено успешно.");
            }
            catch (Exception ex)
            {
                Logger.SendMessage("Не удалось отправить УПД №" + invoice.DocumentNumber + " от " + invoice.DocumentDate + " Клиент: " + invoice.Buyer.OrgName + " ИНН: " + invoice.Buyer.Inn + " Строка для программиста: " + ex.Message);
                return false;
            }
            return true;
        }
       
        //Загрузить УПД в нашу базу
        private static void SaveUPDToBase(UniversalTransferDocumentSellerTitleInfo upd, string messageId)
        {
            if (upd.TransferInfo == null || upd.TransferInfo.TransferBase.Count == 0 || upd.TransferInfo.TransferBase[0].BaseDocumentDate == "")
            {
                Logger.SendMessage("Письмо без данных о договоре");
                return;
            }

            var nConv = upd.TransferInfo.TransferBase[0].BaseDocumentNumber;
            var dateConv = DateTime.Parse(upd.TransferInfo.TransferBase[0].BaseDocumentDate);

            if (nConv == "")
                nConv = upd.TransferInfo.TransferBase[0].BaseDocumentName.Split('№').Last().Trim().Split(' ').First().Trim();

            var kontr = DBDiadoc.GetSupplierByInnKpp(upd.Seller.Inn, upd.Seller.Kpp);
            string strMess = "Получено УПД. Фирма: " + upd.Buyer.OrgName + ". Контрагент ИНН:" + upd.Seller.Inn + " КПП: " + upd.Seller.Kpp + " Наименование: " + upd.Seller.OrgName + "\r\n";
            if (kontr == null)
            {
                strMess += "Не удалось определить контрагента в справочнике" + "\r\n";
                Logger.SendMessage(strMess);
                return;
            }

            var idKontr = int.Parse(kontr["id_kontr"].ToString());
            var fSupplier = int.Parse(kontr["supplier"].ToString());
            if (fSupplier != 1)
            {
                strMess += "Контрагент " + idKontr.ToString() + " не является поставщиком" + "\r\n";
                Logger.SendMessage(strMess);
                return;
            }

            var nKontr = kontr["n_kontr"].ToString();
            strMess += "Определен контрагент: " + idKontr.ToString() + " " + nKontr + "\n";
            var convention = InvoiceBuy.GetConventionWithSeller(idKontr, nConv, dateConv);
            int idConvention = -1;

            if (convention == null)
            {
                strMess += "Не найден договор контрагента: " + nConv + " от " + dateConv.ToShortDateString() + "\n";
                var otherConv = InvoiceBuy.GetConventionWithSeller(idKontr);
                var listEDI = otherConv.AsEnumerable().Where(p => p["fEDI"].ToString() == "1").ToList();
                if (listEDI.Count == 0)
                {
                    strMess += "У поставщика не найдено договора закупки разрешающих электронный документооборот. УПД не загружен";
                    Logger.SendMessage(strMess);
                    return;
                }

                if (listEDI.Count > 1)
                {
                    strMess += "У поставщика найдено более 1 договора закупки разрешающих электронный документооборот. УПД не загружен";
                    Logger.SendMessage(strMess);
                    return;
                }

                if (listEDI.Count == 1)
                {
                    idConvention = int.Parse(listEDI.First()["idConvention"].ToString());
                    string nConvold = listEDI.First()["nom_contract"].ToString();
                    string dateConvold = DateTime.Parse(listEDI.First()["date_contract"].ToString()).ToShortDateString();
                    strMess += "УПД загружен на договор Код:" + idConvention + "; №" + nConvold + " от " + dateConvold + ". Пожалуйста, проверьте корректность документа.";
                    Logger.SendMessage(strMess);
                }
            }
            else
            {
                var fEDI = int.Parse(convention["fEDI"].ToString());
                if (fEDI != 1)
                {
                    strMess += "По договору, указанному в УПД: Номер=" + nConv + "; Дата=" + dateConv.ToShortDateString() + " не стоит флаг(Доступен электронный документооборот). УДП не загружен";
                    Logger.SendMessage(strMess);
                    return;
                }
                else
                    idConvention = int.Parse(convention["idConvention"].ToString());
            }

            if (idConvention == -1)
                return;

            Logger.SendMessage(strMess + " Проверки пройдены УСПЕШНО");
            var invoice = new InvoiceBuy();

            invoice.IdKontr = idKontr;
            invoice.DateSF = DateTime.Parse(upd.DocumentDate);
            invoice.NomSF = upd.DocumentNumber;
            invoice.Proform = 0;
            invoice.NFile = "Диадок: " + upd.DocumentName;
            invoice.MessageId = messageId;
            invoice.idConvention = idConvention;
            invoice.idTypeSource = 2;

            var firm = DBDiadoc.GetSprKontrByInnKpp(upd.Buyer.Inn, upd.Buyer.Kpp);
            if (firm != null)
                invoice.idFirm = int.Parse(firm["id_kontr"].ToString());
            else
                invoice.idFirm = 0;

            foreach (var tov in upd.InvoiceTable.Items)
            {
                var invoicetov = new InvoiceBuyTov();
                invoicetov.IdArt = GetTovArticle(tov, invoice.IdKontr);

                invoicetov.Brend = "";
                invoicetov.Declaration = tov.CustomsDeclarations.Count == 0 ? "" : tov.CustomsDeclarations[0].DeclarationNumber;
                invoicetov.Kol = Convert.ToInt32(decimal.Parse(tov.Quantity));
                invoicetov.MadeIn = tov.CustomsDeclarations.Count == 0 ? 643 : int.Parse(tov.CustomsDeclarations[0].CountryCode);
                invoicetov.Price = decimal.Parse(tov.Subtotal) / decimal.Parse(tov.Quantity);


                var oldInv = invoice.listTov.Where(p => p.IdArt == invoicetov.IdArt && Math.Round(p.Price, 4) == Math.Round(invoicetov.Price, 4) && p.Declaration == invoicetov.Declaration).FirstOrDefault();
                if (oldInv != null)
                    oldInv.Kol += invoicetov.Kol;
                else
                    invoice.listTov.Add(invoicetov);
            }

            invoice.Save();
        }

        private static void SaveUPDToBase(Diadoc.Api.DataXml.Utd820.Hyphens.UniversalTransferDocumentWithHyphens upd, string messageId)
        {
           
            try
            {
                
                //if (upd.TransferInfo == null || upd.TransferInfo.TransferBases.Count() == 0 || upd.TransferInfo.TransferBases[0].BaseDocumentDate == "" )
                //{
                //    Logger.SendMessage("Письмо без данных о договоре");
                //    return;
                //}

                //var nConv = upd.TransferInfo.TransferBases[0].BaseDocumentNumber;

                
                string mes;
                //mes = "nConv = " + upd.TransferInfo.TransferBases[0].BaseDocumentNumber.ToString();
                //Logger.SendMessage(mes);

                ////var dateConv = DateTime.Parse(upd.TransferInfo.TransferBases[0].BaseDocumentDate);

                
              
                //if (nConv == "" || nConv == null)
                //{
                //    nConv = upd.TransferInfo.TransferBases[0].BaseDocumentName.Split('№').Last().Trim().Split(' ').First().Trim();
                //    mes = "nConv-1 = " + nConv.ToString();
                //    Logger.WriteErrorMessage(mes);
                //}

                var seller = (upd.Sellers[0].Item as Diadoc.Api.DataXml.Utd820.Hyphens.ExtendedOrganizationDetails);

                
                mes = " seller.inn = " + seller.Inn.ToString() + "\n";
                Logger.SendMessage(mes);
                mes = " seller.kpp = " + seller.Kpp.ToString() + "\n";
                Logger.SendMessage(mes);
                mes = " seller.orgname = " + seller.OrgName.ToString() + "\n";
                Logger.SendMessage(mes);


                var buyer = (upd.Buyers[0].Item as Diadoc.Api.DataXml.Utd820.Hyphens.ExtendedOrganizationDetails);
               
                mes = " buyer.inn = " + buyer.Inn.ToString() + "\n";
                Logger.SendMessage(mes);
                mes = " buyer.kpp = " + buyer.Kpp.ToString() + "\n";
                Logger.SendMessage(mes);
                mes = " buyer.orgname = " + buyer.OrgName.ToString() + "\n";
                Logger.SendMessage(mes);


                var kontr = DBDiadoc.GetSupplierByInnKpp(seller.Inn, seller.Kpp);


                // string strMess = "Получено УПД. Фирма: " + buyer.OrgName + ". Контрагент ИНН:" + seller.Inn + " КПП: " + seller.Kpp + " Наименование: " + seller.OrgName + "\n";
                mes = "Получено УПД. Фирма: " + buyer.OrgName + ". Контрагент ИНН:" + seller.Inn + " КПП: " + seller.Kpp + " Наименование: " + seller.OrgName + "\n";
                Logger.SendMessage(mes);

                if (kontr == null)
                {
                    //strMess += "Не удалось определить контрагента в справочнике" + "\n";
                    //Logger.SendMessage(strMess);

                    mes = "Не удалось определить контрагента в справочнике, проверьте ИНН и КПП " + "\n";
                    Logger.SendMessage(mes);
                    return;
                }

                var idKontr = int.Parse(kontr["id_kontr"].ToString());
             

                var fSupplier = int.Parse(kontr["supplier"].ToString());
               
                if (fSupplier != 1)
                {
                    //strMess += "Контрагент " + idKontr.ToString() + " не является поставщиком" + "\n";
                    //Logger.SendMessage(strMess);

                    mes = "Контрагент " + idKontr.ToString() + " не является поставщиком" + "\n";
                    Logger.SendMessage(mes);
                    return;
                }

                var nKontr = kontr["n_kontr"].ToString();

                //strMess += "Определен контрагент: " + idKontr.ToString() + " " + nKontr + "\n";

                mes = "Определен контрагент: " + idKontr.ToString() + "\n";
                Logger.SendMessage(mes);

                //получим параметры договора
                //var convention = InvoiceBuy.GetConventionWithSeller(idKontr, nConv, dateConv);

                DateTime dateConv;
                bool ret = false;
                int idConvention = -1;
                bool findConv = false;

                if (upd.TransferInfo.TransferBases[0].BaseDocumentDate != null && upd.TransferInfo.TransferBases[0].BaseDocumentNumber != null)
                {
                   
                    var nConv = upd.TransferInfo.TransferBases[0].BaseDocumentNumber;
                    mes = "nConv = " + upd.TransferInfo.TransferBases[0].BaseDocumentNumber.ToString();
                    Logger.SendMessage(mes);

                    if (nConv == "" || nConv == null)
                    {
                        nConv = upd.TransferInfo.TransferBases[0].BaseDocumentName.Split('№').Last().Trim().Split(' ').First().Trim();
                        mes = "nConv-1 = " + nConv.ToString() + "\n";
                        Logger.WriteErrorMessage(mes);
                    }
                    ret = DateTime.TryParse(upd.TransferInfo.TransferBases[0].BaseDocumentDate.ToString().Trim(), out dateConv);

                    if (ret && (nConv != "" && nConv != null))
                    {
                        var convention = InvoiceBuy.GetConventionWithSeller(idKontr, nConv, dateConv);

                        if (convention != null)
                        {
                            var fEDI = int.Parse(convention["fEDI"].ToString());

                            if (fEDI != 1)
                            {
                                mes = "По договору, указанному в УПД: Номер=" + nConv + "; Дата=" + dateConv.ToShortDateString() + " не стоит флаг(Доступен электронный документооборот). УДП не загружен" + "\n";
                                Logger.SendMessage(mes);
                                return;
                            }
                            else
                                idConvention = int.Parse(convention["idConvention"].ToString());
                                findConv = true;

                                mes = "Данные по договору определены" + "\n";
                                Logger.SendMessage(mes);
                        }
                        else
                        {
                            mes = "По договору, указанному в УПД: Номер=" + nConv + "; Дата=" + dateConv.ToShortDateString() + "нет данных в базе" + "\n";

                        }
                    }
                    else
                    {
                        if (nConv == "" || nConv == null)
                        {
                            mes = "Ошибка при получении № договора.  \n ";
                            Logger.SendMessage(mes);
                        }
                        else
                        {
                            mes = "Не удалось распарсить дату из " + upd.TransferInfo.TransferBases[0].BaseDocumentDate.ToString() + "\n";
                            Logger.SendMessage(mes);
                        }
                       
                    }

                }
                else
                {
                    mes = "Данные по договору неопределены" + "\n \r";
                    Logger.SendMessage(mes);
                   
                }

                if(!findConv)
                { 
                    var firma = DBDiadoc.GetFirmByInnKpp(buyer.Inn, buyer.Kpp);
                    if (firma == null)
                    {
                        mes = "Не удалось получить фирму " + "\n";
                        Logger.SendMessage(mes);
                        return;
                    }

                    var idFirm = int.Parse(firma["id_kontr"].ToString());

                    mes = "idFirm = " + idFirm.ToString();
                    Logger.SendMessage(mes);


                    var conv = InvoiceBuy.GetConventionWithSeller(idKontr, idFirm);

                    if (conv == null)
                    {
                        mes = "Не удалось получить код договора по фирме " + "\n";
                        Logger.SendMessage(mes);
                        return;
                    }

                    if (conv.Rows.Count == 0 )
                    {
                        mes = "Не удалось найти договор с ЭДО " + "\n";
                        Logger.SendMessage(mes);
                        return;
                    }

                    if (conv.Rows.Count > 1)
                    {
                        mes = " Для контрагента " + nKontr + " и фирмы " + buyer.OrgName.ToString() + " есть несколько договоров ЭДО  " + "\n";
                        Logger.SendMessage(mes);
                        return;
                    }

                    var rRow =conv.Rows[0];
                    ret = int.TryParse(rRow["idConvention"].ToString(), out idConvention);

                    if (!ret)
                    {
                        mes = "Ошибка при получении договора для фирмы" + "\n";
                        Logger.SendMessage(mes);
                        return;
                    }
                    mes = " договор = " + idConvention.ToString() + "\n";
                    Logger.SendMessage(mes);


                }
                
                //*********************
               
                

                //if (convention == null )
                //{
                //    strMess += "Не найден договор контрагента: " + nConv + " от " + dateConv.ToShortDateString() + "\n";
                   
                 // var otherConv = InvoiceBuy.GetConventionWithSeller(idKontr);

                  
                //    var listEDI = otherConv.AsEnumerable().Where(p => p["fEDI"].ToString() == "1").ToList();
                //    if (listEDI.Count == 0)
                //    {
                //        strMess += "У поставщика не найдено договора закупки разрешающих электронный документооборот. УПД не загружен";
                //        Logger.SendMessage(strMess);
                //        return;
                //    }

                //    if (listEDI.Count > 1)
                //    {
                //        strMess += "У поставщика найдено более 1 договора закупки разрешающих электронный документооборот. УПД не загружен";
                //        Logger.SendMessage(strMess);
                //        return;
                //    }

                //    if (listEDI.Count == 1)
                //    {
                //        idConvention = int.Parse(listEDI.First()["idConvention"].ToString());
                //        string nConvold = listEDI.First()["nom_contract"].ToString();
                //        string dateConvold = DateTime.Parse(listEDI.First()["date_contract"].ToString()).ToShortDateString();
                //        strMess += "УПД загружен на договор Код:" + idConvention + "; №" + nConvold + " от " + dateConvold + ". Пожалуйста, проверьте корректность документа.";
                //        Logger.SendMessage(strMess);
                //    }
                //}
                //else
                //{
                     
                //    var fEDI = int.Parse(convention["fEDI"].ToString());
                   
                //    if (fEDI != 1)
                //    {
                //        strMess += "По договору, указанному в УПД: Номер=" + nConv + "; Дата=" + dateConv.ToShortDateString() + " не стоит флаг(Доступен электронный документооборот). УДП не загружен";
                //        Logger.SendMessage(strMess);
                //        return;
                //    }
                //    else
                //        idConvention = int.Parse(convention["idConvention"].ToString());
                //}

                if (idConvention == -1)
                    return;

                //Logger.SendMessage(strMess + " Проверки пройдены УСПЕШНО");

                Logger.SendMessage("Проверки пройдены УСПЕШНО \n");

                mes =  "№ c/ф " + upd.DocumentNumber.ToString() + "\n";
                Logger.SendMessage(mes);
                mes = "Дата с/ф " + upd.DocumentDate.ToString() + "\n";
                Logger.SendMessage(mes);

                var invoice = new InvoiceBuy();

                invoice.IdKontr = idKontr;
                //invoice.DateSF = DateTime.Parse(upd.DocumentDate);

                DateTime dateSF;

                ret = DateTime.TryParse(upd.DocumentDate.ToString().Trim(), out dateSF);
                if (ret)
                {
                    invoice.DateSF = dateSF;
                }
                else
                {
                    mes = "произошла ошибка при конвертации даты с/ф из " + upd.DocumentDate.ToString() + "\n";
                    Logger.SendMessage(mes);
                }

                invoice.NomSF = upd.DocumentNumber;
                invoice.Proform = 0;
                invoice.NFile = "Диадок: " + upd.DocumentName;
                invoice.MessageId = messageId;
                invoice.idConvention = idConvention;
                invoice.idTypeSource = 2;


                var firm = DBDiadoc.GetSprKontrByInnKpp(buyer.Inn, buyer.Kpp);

                if (firm == null)
                {
                    mes = " Произошла ошибка при получении фирмы из GetSprKontrByInnKpp  \n";
                    Logger.SendMessage(mes);
                }

                if (firm != null)
                    invoice.idFirm = int.Parse(firm["id_kontr"].ToString());
                else
                    invoice.idFirm = 0;



                foreach (var tov in upd.Table.Item)
                {
                    var invoicetov = new InvoiceBuyTov();
                    invoicetov.IdArt = GetTovArticle(tov, invoice.IdKontr);

                    invoicetov.Brend = "";
                    invoicetov.Declaration = tov.CustomsDeclarations == null || tov.CustomsDeclarations.Count() == 0 ? "" : tov.CustomsDeclarations[0].DeclarationNumber;
                    invoicetov.Kol = Convert.ToInt32(tov.Quantity);
                    invoicetov.MadeIn = tov.CustomsDeclarations == null || tov.CustomsDeclarations.Count() == 0 ? 643 : int.Parse(tov.CustomsDeclarations[0].Country);
                    invoicetov.Price = tov.Subtotal / tov.Quantity;


                    var oldInv = invoice.listTov.Where(p => p.IdArt == invoicetov.IdArt && Math.Round(p.Price, 4) == Math.Round(invoicetov.Price, 4) && p.Declaration == invoicetov.Declaration).FirstOrDefault();
                    if (oldInv != null)
                        oldInv.Kol += invoicetov.Kol;
                    else
                        invoice.listTov.Add(invoicetov);
                }

                invoice.Save();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorMessage("Ошибка метода SaveUPDToBase (.Hyphens) : messageid = " + messageId + " Сообщение об ошибке " + ex.Message);
            }
        }

        private static void SaveUPDToBase(Diadoc.Api.DataXml.Utd820.UniversalTransferDocument upd, string messageId)
        {
            try
            {
                if (upd.TransferInfo == null || upd.TransferInfo.TransferBases.Count() == 0 || upd.TransferInfo.TransferBases[0].BaseDocumentDate == "")
                {
                    Logger.SendMessage("Письмо без данных о договоре");
                    return;
                }

                var nConv = upd.TransferInfo.TransferBases[0].BaseDocumentNumber;
                var dateConv = DateTime.Parse(upd.TransferInfo.TransferBases[0].BaseDocumentDate);

                if (nConv == "" || nConv == null)
                    nConv = upd.TransferInfo.TransferBases[0].BaseDocumentName.Split('№').Last().Trim().Split(' ').First().Trim();

                var seller = (upd.Sellers[0].Item as Diadoc.Api.DataXml.Utd820.ExtendedOrganizationDetails);
                var buyer = (upd.Buyers[0].Item as Diadoc.Api.DataXml.Utd820.ExtendedOrganizationDetails);

                var kontr = DBDiadoc.GetSupplierByInnKpp(seller.Inn, seller.Kpp);
                string strMess = "Получено УПД. Фирма: " + buyer.OrgName + ". Контрагент ИНН:" + seller.Inn + " КПП: " + seller.Kpp + " Наименование: " + seller.OrgName + "\n";
                if (kontr == null)
                {
                    strMess += "Не удалось определить контрагента в справочнике" + "\n";
                    Logger.SendMessage(strMess);
                    return;
                }

                var idKontr = int.Parse(kontr["id_kontr"].ToString());
                var fSupplier = int.Parse(kontr["supplier"].ToString());
                if (fSupplier != 1)
                {
                    strMess += "Контрагент " + idKontr.ToString() + " не является поставщиком" + "\n";
                    Logger.SendMessage(strMess);
                    return;
                }

                var nKontr = kontr["n_kontr"].ToString();
                strMess += "Определен контрагент: " + idKontr.ToString() + " " + nKontr + "\n";
                var convention = InvoiceBuy.GetConventionWithSeller(idKontr, nConv, dateConv);
                int idConvention = -1;

                if (convention == null)
                {
                    strMess += "Не найден договор контрагента: " + nConv + " от " + dateConv.ToShortDateString() + "\n";
                    var otherConv = InvoiceBuy.GetConventionWithSeller(idKontr);
                    var listEDI = otherConv.AsEnumerable().Where(p => p["fEDI"].ToString() == "1").ToList();
                    if (listEDI.Count == 0)
                    {
                        strMess += "У поставщика не найдено договора закупки разрешающих электронный документооборот. УПД не загружен";
                        Logger.SendMessage(strMess);
                        return;
                    }

                    if (listEDI.Count > 1)
                    {
                        strMess += "У поставщика найдено более 1 договора закупки разрешающих электронный документооборот. УПД не загружен";
                        Logger.SendMessage(strMess);
                        return;
                    }

                    if (listEDI.Count == 1)
                    {
                        idConvention = int.Parse(listEDI.First()["idConvention"].ToString());
                        string nConvold = listEDI.First()["nom_contract"].ToString();
                        string dateConvold = DateTime.Parse(listEDI.First()["date_contract"].ToString()).ToShortDateString();
                        strMess += "УПД загружен на договор Код:" + idConvention + "; №" + nConvold + " от " + dateConvold + ". Пожалуйста, проверьте корректность документа.";
                        Logger.SendMessage(strMess);
                    }
                }
                else
                {
                    var fEDI = int.Parse(convention["fEDI"].ToString());
                    if (fEDI != 1)
                    {
                        strMess += "По договору, указанному в УПД: Номер=" + nConv + "; Дата=" + dateConv.ToShortDateString() + " не стоит флаг(Доступен электронный документооборот). УДП не загружен";
                        Logger.SendMessage(strMess);
                        return;
                    }
                    else
                        idConvention = int.Parse(convention["idConvention"].ToString());
                }

                if (idConvention == -1)
                    return;

                Logger.SendMessage(strMess + " Проверки пройдены УСПЕШНО");
                var invoice = new InvoiceBuy();

                invoice.IdKontr = idKontr;
                invoice.DateSF = DateTime.Parse(upd.DocumentDate);
                invoice.NomSF = upd.DocumentNumber;
                invoice.Proform = 0;
                invoice.NFile = "Диадок: " + upd.DocumentName;
                invoice.MessageId = messageId;
                invoice.idConvention = idConvention;
                invoice.idTypeSource = 2;

                var firm = DBDiadoc.GetSprKontrByInnKpp(buyer.Inn, buyer.Kpp);
                if (firm != null)
                    invoice.idFirm = int.Parse(firm["id_kontr"].ToString());
                else
                    invoice.idFirm = 0;

                foreach (var tov in upd.Table.Item)
                {
                    var invoicetov = new InvoiceBuyTov();
                    invoicetov.IdArt = GetTovArticle(tov, invoice.IdKontr);

                    invoicetov.Brend = "";
                    invoicetov.Declaration = tov.CustomsDeclarations == null || tov.CustomsDeclarations.Count() == 0 ? "" : tov.CustomsDeclarations[0].DeclarationNumber;
                    invoicetov.Kol = Convert.ToInt32(tov.Quantity);
                    invoicetov.MadeIn = tov.CustomsDeclarations == null || tov.CustomsDeclarations.Count() == 0 ? 643 : int.Parse(tov.CustomsDeclarations[0].Country);
                    invoicetov.Price = tov.Subtotal / tov.Quantity;


                    var oldInv = invoice.listTov.Where(p => p.IdArt == invoicetov.IdArt && Math.Round(p.Price, 4) == Math.Round(invoicetov.Price, 4) && p.Declaration == invoicetov.Declaration).FirstOrDefault();
                    if (oldInv != null)
                        oldInv.Kol += invoicetov.Kol;
                    else
                        invoice.listTov.Add(invoicetov);
                }

                invoice.Save();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorMessage("Ошибка метода SaveUPDToBase : messageid = " + messageId + " Сообщение об ошибке " + ex.Message);
            }
        }

        private static string GetTovArticle(Diadoc.Api.DataXml.Utd820.Hyphens.InvoiceTableItem tov, int idSupplier)
        {
            if (idSupplier == 548302)
                return tov.ItemVendorCode.Split('^').Last().Trim();

            if (idSupplier == 553357 || idSupplier == 555568)
                return tov.Product.Split(' ').First();

            if (idSupplier == 556119)
                return tov.Product.Split(' ').Last(); //Для Бриксо

            if (idSupplier == 551237) //манн
            {
                string[] words = tov.Product.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string str = null;

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length == 10)
                        break;
                    str = str + words[i];
                }
                               
                return str;
            }

            if (tov.ItemVendorCode.Replace("-", "") != "")
                return tov.ItemVendorCode;
            else
                 if (tov.AdditionalInfos.Count() == 0)
                return tov.Product.Split(' ').First();


            return tov.AdditionalInfos[0].Value;
        }

        private static string GetTovArticle(Diadoc.Api.DataXml.Utd820.InvoiceTableItem tov, int idSupplier)
        {
            if (idSupplier == 548302)
                return tov.ItemVendorCode.Split('^').Last().Trim();

            if (idSupplier == 553357 || idSupplier == 555568)
                return tov.Product.Split(' ').First();

            if (idSupplier == 556119)
                return tov.Product.Split(' ').Last(); //Для Бриксо

            if (idSupplier == 551237) //манн
            {
                string[] words = tov.Product.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string str = null;

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length == 10)
                        break;
                    str = str + words[i];
                }

                return str;
            }


            if (tov.ItemVendorCode.Replace("-", "") != "")
                return tov.ItemVendorCode;
            else
                if (tov.AdditionalInfos.Count() == 0)
                return tov.Product.Split(' ').First();


            return tov.AdditionalInfos[0].Value;
        }

        private static string GetTovArticle(ExtendedInvoiceItem tov, int idSupplier)
        {
            if (idSupplier == 548302)
                return tov.ItemVendorCode.Split('^').Last().Trim();

            if (idSupplier == 553357 || idSupplier == 555568)
                return tov.Product.Split(' ').First();

            if (idSupplier == 556119)
                return tov.Product.Split(' ').Last(); //Для Бриксо

            if (idSupplier == 551237) //манн
            {
                string[] words = tov.Product.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string str = null;

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length == 10)
                        break;
                    str = str + words[i];
                }

                return str;
            }


            if (tov.ItemVendorCode.Replace("-", "") != "")
                return tov.ItemVendorCode;
            else
                if (tov.AdditionalInfo.Count == 0)
                return tov.Product.Split(' ').First();


            return tov.AdditionalInfo[0].Value;
        }


        //Загрузить все документы в диадок
        public static void LoadAllDocs()
        {
            Logger.SendMessage("Загрузка документов");
            var tabledocs = DBDiadoc.GetAllFreeDoc();
            var docs = tabledocs.AsEnumerable().Select(p => p["id_doc"]).ToList();

            Logger.SendMessage("Обнаружено документов для отправки: " + docs.Count.ToString());
            docs.ForEach(p => DBDiadoc.UpdateDocStatus(p, 2));
            Logger.SendMessage("Статусы документов изменены");

            try
            {

                foreach (var doc in docs)
                    //Если получилось отправить документ
                    if (SendUpdXml(doc))
                        //Изменим его статус на "Отправлен"
                        DBDiadoc.UpdateDocStatus(doc, 3);
                    else
                        //Иначе изменим статус на "Ошибка"
                        DBDiadoc.UpdateDocStatus(doc, 4);
            }
            catch (Exception ex)
            {
                Logger.SendMessage(ex.Message);
            }

            try
            {
                //Временно побудет здесь. Сразу пытаемся считать подтверждения
                foreach (var conn in Sertificate.DctSertificate.Values)
                {
                    var inn = conn.Inn;
                    var messIds = DBDiadoc.GetMessageByStatus(inn, 110, 1);
                    Logger.SendMessage("ИНН: " + inn + " Неподтвержденных документов: " + messIds.Rows.Count);
                    foreach (var row in messIds.AsEnumerable())
                    {
                        var message = conn.Api.GetMessage(conn.AuthTokenCert, conn.BoxId, row["idMessage"].ToString());
                        DicdocMessageAction.ReadDocSign(message, row);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorMessage("Время: " + DateTime.Now.ToString() + " Ошибка считывания подтверждений: " + ex.Message);
            }
        }

        //Загрузить все корректировки(на основании доков на возврат)
       
        private static void LoadAllCorrect()
        {
            Logger.SendMessage("Загрузка корректировочных документов");
            var tabledocs = DBDiadoc.GetAllFreeDocCorrect();
            var docs = tabledocs.AsEnumerable().Select(p => p["id_doc"]).ToList();

            Logger.SendMessage("Обнаружено документов для отправки: " + docs.Count.ToString());
            docs.ForEach(p => DBDiadoc.UpdateDocStatus(p, 2));
            Logger.SendMessage("Статусы документов изменены");

            foreach (var doc in docs)
                //Если получилось отправить документ
                if (SendUKDXml(doc))
                    //Изменим его статус на "Отправлен"
                    DBDiadoc.UpdateDocStatus(doc, 3);
                else
                    //Иначе изменим статус на "Ошибка"
                    DBDiadoc.UpdateDocStatus(doc, 4);
        }
       
        //Сохранить все полученные от поставщика документы
        public static void GetAllMessagesToSave()
        {

            DateTime startDate = DateTime.Now.Date.AddDays(-1);
            var listId = InvoiceBuy.GetListMessageID(startDate);

            foreach (var conn in Sertificate.DctSertificate.Values)
            {
                DocumentsFilter f = new DocumentsFilter();
                f.BoxId = conn.BoxId;
                f.FilterCategory = "Any.Inbound";  //"UniversalTransferDocument.Inbound";
                f.TimestampFrom = startDate;

                var listdoc = conn.Api.GetDocuments(conn.AuthTokenCert, f);

                foreach (var doc in listdoc.Documents)
                {
                    if (listId.Count(p => p == doc.MessageId) == 0)
                    {
                        var m = conn.Api.GetMessage(conn.AuthTokenCert, conn.BoxId, doc.MessageId, true, true);
                                                
                        var file = m.Entities.FirstOrDefault(p => p.EntityType == EntityType.Attachment && ( p.AttachmentType == AttachmentType.UniversalTransferDocument || p.AttachmentType == AttachmentType.Invoice ));
                        if (file != null)
                        {
                            try
                            {
                                var docInf = conn.Api.DetectDocumentTypes(conn.AuthTokenCert, conn.BoxId, file.Content.Data);

                                string ftype = docInf.DocumentTypes[0].TypeNamedId;
                                string fact = docInf.DocumentTypes[0].Function;
                                string nvers = docInf.DocumentTypes[0].Version;

                                var data = conn.Api.ParseTitleXml(conn.AuthTokenCert, conn.BoxId, ftype, fact, nvers, 0, file.Content.Data);

                                if (nvers.IndexOf("hyphen") != -1)
                                {
                                    var serializer = new XmlSerializer(typeof(Diadoc.Api.DataXml.Utd820.Hyphens.UniversalTransferDocumentWithHyphens));
                                    var ms = new MemoryStream(data);

                                    try
                                    {
                                        var upd = (Diadoc.Api.DataXml.Utd820.Hyphens.UniversalTransferDocumentWithHyphens)serializer.Deserialize(ms);
                                        SaveUPDToBase(upd, doc.MessageId);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.WriteErrorMessage("Ошибка при Deserialize и вызове SaveUPDToBase " + ex.Message + " ");

                                    }
                                }
                                else
                                {
                                    var serializer = new XmlSerializer(typeof(Diadoc.Api.DataXml.Utd820.UniversalTransferDocument));
                                    var ms = new MemoryStream(data);
                                    var upd = (Diadoc.Api.DataXml.Utd820.UniversalTransferDocument)serializer.Deserialize(ms);

                                    SaveUPDToBase(upd, doc.MessageId);
                                }
                               
                                DBDiadoc.SaveMessageNoLoad(conn.Inn, doc.MessageId);
                                listId.Add(doc.MessageId);
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorMessage("Ошибка в методе GetAllMessagesToSave: " + ex.Message);
                                continue;
                            }
                            continue;
                        }
                        else
                            DBDiadoc.SaveMessageNoLoad(conn.Inn, doc.MessageId);
                    }
                }
            }
        }

      

        private static void SaveInvoiseToBase(InvoiceInfo sf, string messageId)
        {
            var kontr = DBDiadoc.GetSprKontrByInnKpp(sf.Seller.OrgInfo.Inn, sf.Seller.OrgInfo.Inn);
            string strMess = "Получено УПД. Фирма: " + sf.Buyer.OrgInfo.Name + ". Контрагент ИНН:" + sf.Seller.OrgInfo.Inn + " КПП: " + sf.Seller.OrgInfo.Kpp + " Наименование: " + sf.Seller.OrgInfo.Name + "\n";
            if (kontr == null)
            {
                strMess += "Не удалось определить контрагента в справочнике" + "\n";
                Logger.SendMessage(strMess);
                return;
            }

            var idKontr = int.Parse(kontr["id_kontr"].ToString());
            var fSupplier = int.Parse(kontr["supplier"].ToString());
            if (fSupplier != 1)
            {
                strMess += "Контрагент " + idKontr.ToString() + " не является поставщиком" + "\n";
                Logger.SendMessage(strMess);
                return;
            }

            var nKontr = kontr["n_kontr"].ToString();
            strMess += "Определен контрагент: " + idKontr.ToString() + " " + nKontr + "\n";

            var otherConv =InvoiceBuy.GetConventionWithSeller(idKontr);
            var listEDI = otherConv.AsEnumerable().Where(p => p["fEDI"].ToString() == "1").ToList();

            if (listEDI.Count == 0)
            {
                strMess += "У поставщика не найдено договора закупки разрешающих электронный документооборот. УПД не загружен";
                Logger.SendMessage(strMess);
                return;
            }

            if (listEDI.Count > 1)
            {
                strMess += "У поставщика найдено более 1 договора закупки разрешающих электронный документооборот. УПД не загружен";
                Logger.SendMessage(strMess);
                return;
            }

            int idConvention = int.Parse(listEDI.First()["idConvention"].ToString());
            string nConvold = listEDI.First()["nom_contract"].ToString();
            string dateConvold = DateTime.Parse(listEDI.First()["date_contract"].ToString()).ToShortDateString();
            strMess += "УПД загружен на договор Код:" + idConvention + "; №" + nConvold + " от " + dateConvold + ". Пожалуйста, проверьте корректность документа.";
            Logger.SendMessage(strMess);
            Logger.SendMessage(strMess + " Проверки пройдены УСПЕШНО");
            var invoice = new InvoiceBuy();

            invoice.IdKontr = idKontr;
            invoice.DateSF = DateTime.Parse(sf.InvoiceDate);
            invoice.NomSF = sf.InvoiceNumber;
            invoice.Proform = 0;
            invoice.NFile = "Диадок: " + "Счет фактура";
            invoice.MessageId = messageId;
            invoice.idConvention = idConvention;
            invoice.idTypeSource = 2;

            var firm = DBDiadoc.GetSprKontrByInnKpp(sf.Buyer.OrgInfo.Inn, sf.Buyer.OrgInfo.Kpp);
            if (firm != null)
                invoice.idFirm = int.Parse(firm["id_kontr"].ToString());
            else
                invoice.idFirm = 0;

            DataTable dtArt = idKontr == 549441 ? DBDiadoc.GetTovsForSupplier(idKontr) : null;

            foreach (var tov in sf.Items)
            {
                var invoicetov = new InvoiceBuyTov();
                invoicetov.IdArt = GetTovArticleSF(tov, invoice.IdKontr, dtArt);

                invoicetov.Brend = "";
                invoicetov.Declaration = tov.CustomsDeclarations.Count == 0 ? "" : tov.CustomsDeclarations[0].DeclarationNumber;
                invoicetov.Kol = int.Parse(tov.Quantity);
                invoicetov.MadeIn = tov.CustomsDeclarations.Count == 0 ? 643 : int.Parse(tov.CustomsDeclarations[0].CountryCode);
                invoicetov.Price = decimal.Parse(tov.Subtotal) / decimal.Parse(tov.Quantity);

                invoice.listTov.Add(invoicetov);
            }

            invoice.Save();
        }

        private static string GetTovArticleSF(InvoiceItem tov, int idKontr, DataTable dtArt)
        {
            if (idKontr == 549441)
            {
                var strtemp = tov.Product.Split('/').Last().Split('(').First();
                foreach (var row in dtArt.AsEnumerable())
                {
                    if (strtemp.IndexOf(row["id_tov_oem_short"].ToString()) != -1)
                        return row["id_tov_oem_short"].ToString();
                }
            }
            return tov.Product;
        }


        public static void OneSession()
        {
            Logger.SendMessage("------------------------------Старт службы----------------------------");
            if (DateTime.Now.Hour < 2)
            {
                Logger.SendMessage("Переподключение к службе Диадок");
                Sertificate.ReloadRealSertificates();
                Logger.SendMessage("Переподключение выполнено успешно");
            }
            LoadAllDocs();
            LoadAllCorrect();
            GetAllMessagesToSave();
            //DoAllActions();
            RobotAnswer.SendAnswer(2);
            Logger.SendMessage("---------------------Служба завершила работу---------------------------");
            Logger.WriteMessage();
        }




        //Выполнение действия с сообщением
        public static void DoActionWithMessage(int idStatus, int fProcesed)
        {
            foreach (var conn in Sertificate.DctSertificate.Values)
            {
                var inn = conn.Inn;
                var messIds = DBDiadoc.GetMessageByStatus(inn, idStatus, fProcesed);
                Logger.SendMessage("Для ИНН " + inn + " обнаружено " + messIds.Rows.Count.ToString() + " писем");

                foreach (var row in messIds.AsEnumerable())
                {
                    var message = conn.Api.GetMessage(conn.AuthTokenCert, conn.BoxId, row["idMessage"].ToString());

                    switch (idStatus)
                    {
                        case 110: DicdocMessageAction.ReadDocSign(message, row); break;
                            // case 200: DicdocMessageAction.SignDoc(message, row, conn); break;
                            // case 300: DicdocMessageAction.UnSignDoc(message, row); break;
                            // case 320: { if (fProcesed == 0) DicdocMessageAction.AnulateDoc(message, row); else DicdocMessageAction.RealAnulateDocSign(message, row); }; break;
                            // case 330: DicdocMessageAction.SignAnulate(message, row); break;
                            // case 210: DicdocMessageAction.UnSignAnulate(message, row); break;
                    }
                }
            }
        }
        //Выполнить все заявленные действия с документами
        public static void DoAllActions()
        {
            Logger.SendMessage("Проверка писем ожидающих подписания");
            DoActionWithMessage(110, 1); //Все отправленные покупателю - считываем подписание или отказ подписания   
            /*  DBConnector.Logger.ShowMessage("Подписание полученых УПД");                         
              DoActionWithMessage(200, 0); //Необходимо подписать принятый документ
              DBConnector.Logger.ShowMessage("Отказ подписания полученых УПД"); 
              DoActionWithMessage(300, 0); //Необходимо отклонить подпись документа
              DBConnector.Logger.ShowMessage("Анулирование документов"); 
              DoActionWithMessage(320, 0); //Необходимо поставить документ на анулирование
              DBConnector.Logger.ShowMessage("Проверка подтверждения анулирования документа"); 
              DoActionWithMessage(320, 1); //Необходимо считать подтверждение или отказ анулирования
              DBConnector.Logger.ShowMessage("Подписание анулирования документа"); 
              DoActionWithMessage(330, 0); //Подписать анулирование документа
              DBConnector.Logger.ShowMessage("Подписание отказа анулирования документа"); 
              DoActionWithMessage(210, 0); //Подписать отказ анулирования документа
              DBConnector.Logger.ShowMessage("Чтение заявок на анулирование документа"); 
              ReadAnulate();*/
        }
        //Чтение анулирования
        private static void ReadAnulate()
        {
            throw new NotImplementedException();
        }
        //Получение подтверждения оператора, формирование и отправка извещения о получении
        private static void GetInvoiceConfirmationAndSendReceipt(Message invoiceMessage, Sertificate sert)
        {
            var confirmationEntityId = "";


            foreach (var entity in invoiceMessage.Entities)
            {
                if (entity.AttachmentType == AttachmentType.InvoiceConfirmation)
                {
                    confirmationEntityId = entity.EntityId;
                    break;
                }
            }

            var receipt = sert.Api.GenerateInvoiceDocumentReceiptXml(sert.AuthTokenCert, sert.BoxId, invoiceMessage.MessageId, confirmationEntityId, new Signer()
            {
                //Подпись отправителя, см. "Как авторизоваться в системе"
                SignerCertificate = sert.FileContent,
                SignerDetails = new SignerDetails()
                {
                    FirstName = sert.Name
                     ,
                    Surname = sert.Surname
                     ,
                    Patronymic = sert.Patronymic
                     ,
                    Inn = sert.Inn
                     ,
                    JobTitle = "11"
                }
            });

            var receiptAttachment = new ReceiptAttachment()
            {
                ParentEntityId = confirmationEntityId,
                SignedContent = new SignedContent()
                {
                    Content = receipt.Content,
                    //Подпись отправителя, см. "Как авторизоваться в системе"
                    Signature = sert.Crypt.Sign(receipt.Content, sert.FileContent)
                }
            };

            var receiptPatch = new MessagePatchToPost()
            {
                BoxId = sert.BoxId,
                MessageId = invoiceMessage.MessageId,
                Receipts = { receiptAttachment }
            };

            sert.Api.PostMessagePatch(sert.AuthTokenCert, receiptPatch);
        }
    }
}
