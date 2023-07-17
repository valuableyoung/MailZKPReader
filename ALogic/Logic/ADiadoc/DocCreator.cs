using ALogic.Logic.SPR;
using Diadoc.Api.Proto;
/*using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Organizations;
using Diadoc.Api.Proto.Invoicing.Signers;*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diadoc.Api.DataXml.Utd820;
using Diadoc.Api.DataXml;
using ALogic.Logic.Old;
using System.Xml.Serialization;
using System.IO;
using ALogic.Logic.Reload1C;

namespace ALogic.Logic.ADiadoc
{
    public static class DocCreator
    {
        public static DocData GetDocData(object idDoc)
        {
            //Заберем шапку документа из базы
            var result = new DocData();
            var rowDoc = DBDiadoc.GetDocHeader(idDoc);
            if (rowDoc == null)
            {
                Logger.SendMessage("Не получены данные по документу");
                return null;
            }

            string Inn = rowDoc["SellerInn"].ToString().Replace(" ", "");
            string Kpp = rowDoc["SellerKpp"].ToString().Replace(" ", "");

            if (!Sertificate.DctSertificate.ContainsKey(Inn))
            {
                Logger.SendMessage("Ключ-сертификат для ИНН " + Inn + "  не указан.");
                return null;
            }

            var conn = Sertificate.DctSertificate[Inn];

            Logger.SendMessage("Начало обработки заказа №" + rowDoc["NomDoc"].ToString() + " от " + rowDoc["DateDoc"]);

            result.Date = DateTime.Parse(rowDoc["DateSF"].ToString()).ToShortDateString(); // дата СФ
            result.Nom = rowDoc["NomSF"].ToString() + "p" + rowDoc["NomSF2"].ToString(); // номер СФ

            if (conn.Inn == "7725310764")
                result.Nom = DateTime.Parse(rowDoc["DateSF"].ToString()).ToShortDateString().Replace(".", "").Remove(4,2) + "-" + rowDoc["NomSF2"].ToString();

            var rowKor = DBDiadoc.GetDocKorInfo(idDoc);

            if (rowKor != null)
            {
                result.CorrectionDate = DateTime.Parse(rowKor["DateSF"].ToString()).ToShortDateString(); // дата СФ
                result.CorrectionNom = rowKor["NomSF"].ToString() + "p" + rowKor["NomSF2"].ToString(); // номер СФ

                if (conn.Inn == "7725310764")
                    result.CorrectionNom = DateTime.Parse(rowKor["DateSF"].ToString()).ToShortDateString().Replace(".", "").Remove(4, 2) + "-" + rowKor["NomSF2"].ToString();
            }

            if (result.Date == "" || result.Nom == "")
            {
                Logger.SendMessage("Не найдена счет-фактура по документу");
                return null;
            }

            //Определим отганизацию - отправителя. Реквизиты организации получим через Диадок по ИНН-КПП
            try
            {                
                result.Seller = conn.Api.GetOrganizationByInnKpp(conn.Inn, conn.Kpp);
            }
            catch (Exception ex)
            {
                Logger.SendMessage("Ошибка при определении организации-отправителя. Необходимо проверить ИНН-КПП. Строка для программиста: " + ex.Message);
                return null;
            }

            //Определим организацию - получателя. Если тестовый режим то подставим Тестовую
            try
            {
                result.Buyer = conn.Api.GetOrganizationByInnKpp(rowDoc["BuyerInn"].ToString(), rowDoc["BuyerKpp"].ToString());
            }
            catch (Exception ex)
            {
                Logger.SendMessage("Ошибка при определении организации-получателя. Необходимо проверить ИНН-КПП. Строка для программиста: " + ex.Message);
                return null;
            }

            try
            {
                if (rowDoc["ShipperKpp"].ToString() != "0" && rowDoc["ShipperInn"].ToString().Trim() != "")
                    result.Shipper = conn.Api.GetOrganizationByInnKpp(rowDoc["ShipperInn"].ToString(), rowDoc["ShipperKpp"].ToString());
            }
            catch (Exception ex)
            {
                Logger.SendMessage("Ошибка при определении организации-грузоотправителя. Необходимо проверить ИНН-КПП. Строка для программиста: " + ex.Message);
                return null;
            }

            //Если ИНН грузополучателя указан, определим Грузополучателя

            if (rowDoc["ConsigneeInn"].ToString() != "")
            {
                try
                {
                    result.Consignee = conn.Api.GetOrganizationByInnKpp(rowDoc["ConsigneeInn"].ToString(), rowDoc["ConsigneeKpp"].ToString());
                }
                catch (Exception ex)
                {
                    Logger.SendMessage("Ошибка при определении организации-грузополучателя. Необходимо проверить ИНН-КПП. Строка для программиста: " + ex.Message);
                    return null;
                }
            }


            //Получим курс валюты из БД
            var rowCur = DBSprCur.GetSprCurByCurId(rowDoc["IdCur"]);
            result.Currency = rowCur == null ? "643" : rowCur["id1c"].ToString();    // валюта (код)           
            result.CurrencyRate = rowDoc["RateCur"].ToString();
            result.ConsigneeId = int.Parse(rowDoc["IdKontrFor"].ToString());
            result.TransferBase = rowDoc["TransferBase"].ToString();
            return result;
        }

        public static UniversalTransferDocument CreateUPD(object idDoc, out string BoxId)
        {
            try
            {
                Logger.SendMessage("Загрузка данных по накладной ID = ");
                var updData = GetDocData(idDoc);
                if (updData == null)
                {
                    BoxId = "";
                    return null;
                }

                Logger.SendMessage("Создание УПД для накладной ID = ");

                UniversalTransferDocument result = new UniversalTransferDocument();
                result.Function = UniversalTransferDocumentFunction.СЧФДОП;
                result.DocumentName = "Документ №" + updData.Nom;
                result.DocumentDate = updData.Date;
                result.DocumentNumber = updData.Nom;
                result.Sellers = new ExtendedOrganizationInfo[1] { new ExtendedOrganizationInfo() };
                result.Sellers[0].Item = GetOrganisationExtendedInfo(updData.Seller);
                result.Buyers = new ExtendedOrganizationInfo[1] { new ExtendedOrganizationInfo() };
                result.Buyers[0].Item = GetOrganisationExtendedInfo(updData.Buyer);
                result.Shippers = new UniversalTransferDocumentShipper[1] { new UniversalTransferDocumentShipper() };
                result.Shippers[0].Item = updData.Shipper == null ? GetOrganisationExtendedInfo(updData.Seller) : GetOrganisationExtendedInfo(updData.Shipper);
                result.Shippers[0].SameAsSeller = updData.Shipper == null ? UniversalTransferDocumentShipperSameAsSeller.True : UniversalTransferDocumentShipperSameAsSeller.False;
                result.Consignees = new ExtendedOrganizationInfo[1] { new ExtendedOrganizationInfo() };
                result.Consignees[0].Item = updData.Consignee == null ? GetOrganisationExtendedInfo(updData.Buyer) : GetOrganisationExtendedInfo(updData.Consignee);

                result.Signers = new object[] { GetSignerExt(updData.Seller.Inn) };     //подпись        

                Logger.SendMessage("Загрузка товарной части для накладной ID = " + idDoc.ToString());
                var tableItems = GetExtendedDocItems(idDoc);

                if (tableItems == null)
                {
                    UniLogger.WriteLog("", 0, "tableItems return NULL");
                    BoxId = "";
                    return null;
                }

                result.Table = new InvoiceTable();
                //UniLogger.WriteLog("", 0, "new InvoiceTable() - OK");
                result.Table.Item = tableItems.ToArray();


                result.Table.TotalWithVatExcluded = Math.Round(result.Table.Item.Sum(p => decimal.Parse(p.SubtotalWithVatExcluded.ToString())), 2, MidpointRounding.AwayFromZero);           // сумма без учета налога
                result.Table.TotalWithVatExcludedSpecified = true;
                result.Table.Vat = Math.Round(result.Table.Item.Sum(p => decimal.Parse(p.Vat.ToString())), 2, MidpointRounding.AwayFromZero);
                result.Table.VatSpecified = true;
                result.Table.Total = Math.Round(result.Table.Item.Sum(p => decimal.Parse(p.Subtotal.ToString())), 2, MidpointRounding.AwayFromZero);
                result.Table.TotalSpecified = true;

                result.Currency = updData.Currency;
                result.CurrencyRate = decimal.Parse(updData.CurrencyRate);
                result.CurrencyRateSpecified = true;

                result.AdditionalInfoId = new AdditionalInfoId();
                result.AdditionalInfoId.AdditionalInfo = new AdditionalInfo[1] { new AdditionalInfo { Id = "1", Value = idDoc.ToString() } };

                result.DocumentCreator = (result.Sellers[0].Item as ExtendedOrganizationDetails).OrgName + ", ИНН/КПП " + updData.Seller.Inn + "/" + updData.Seller.Kpp;

                var tInfo = new TransferInfo();
                var sert = Sertificate.DctSertificate[updData.Seller.Inn];
                var updTransferParams = DBDiadoc.GetUpdTransferParams(idDoc);

                //пункт 5а 01,07,2021

                //result.DocumentShipments[0].Date = updData.Date;
                //result.DocumentShipments[0].Number = updData.Nom;
                //result.DocumentShipments[0].Name = "№";

                try
                {
                    var rowCount = DBDiadoc.GetCountRowInTov(idDoc);
                    Logger.SendMessage(" rowCount = " + rowCount + "- кол-во строк   ");

                    string nName = "№ п/п 1 - " + rowCount;
                    Logger.SendMessage(" " + nName + " ");

                    result.DocumentShipments = new UniversalTransferDocumentDocumentShipment[1] { new UniversalTransferDocumentDocumentShipment()
                        {
                            Date = updData.Date,
                            Number = updData.Nom,
                            Name = nName
                        }
                    };
                }
                catch (Exception ex)
                {
                    UniLogger.WriteLog("", 1, "Ошибка в DocumentShipment: " + ex.Message);
                }

                tInfo.Employee = new Employee()
                {
                    Position = "Директор",
                    FirstName = sert.Name,
                    LastName = sert.Surname,
                    MiddleName = sert.Patronymic
                };

                tInfo.TransferBases = new TransferBase820[1] {  new TransferBase820()
            {
                BaseDocumentName = updTransferParams["nameContract"].ToString(),
                BaseDocumentNumber = updTransferParams["NomContract"].ToString(),
                BaseDocumentDate = DateTime.Parse(updTransferParams["DateContract"].ToString()).ToShortDateString()
            } };

                tInfo.TransferTextInfo = "№" + updTransferParams["NomDoc"].ToString() + " от " + DateTime.Parse(updTransferParams["DateDoc"].ToString()).ToShortDateString() + ", Масса груза (брутто) " +
                                           updTransferParams["Weight"].ToString() + " кг";

                tInfo.OperationInfo = "Отгрузка товара";
                tInfo.OperationType = "Отгрузка товара";
                tInfo.TransferDate = DateTime.Parse(updTransferParams["DateDoc"].ToString()).ToShortDateString();

                result.TransferInfo = tInfo;

                BoxId = updData.Buyer.Boxes[0].BoxId;
                /*string BoxIdWithoutDomain = BoxId.Replace("@diadoc.ru", "");
                //Logger.WriteDebugMessage(BoxId + " " + BoxIdWithoutDomain);
                var id = Guid.ParseExact(BoxIdWithoutDomain, "N");
                //Logger.WriteDebugMessage(id.ToString());
                BoxId = id.ToString();*/

                return result;
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("", 1, "Ошибка в методе CreateUPD: " + ex.Message);
                BoxId = "";
                return null;
            }
            finally
            {
                //UniLogger.Flush();
            }
        }
                        

        private static List<InvoiceTableItem> GetExtendedDocItems(object idDoc)
        {
            try
            {
                List<InvoiceTableItem> result = new List<InvoiceTableItem>();
                Logger.SendMessage("Загрузка товарной части");
                var tableItems = DBDiadoc.GetDocItems(idDoc);
                if (tableItems == null || tableItems.Rows.Count == 0)
                {
                    Logger.SendMessage("Не удалось получить товарную часто документа" + idDoc.ToString());
                    return null;
                }
                //StringBuilder sl = new StringBuilder();
                //UniLogger.WriteLog("", 0, "GetDocItems - OK");
                foreach (var rowItem in tableItems.AsEnumerable())
                {
                    var item = new InvoiceTableItem();
                    
                    item.Product = rowItem["NTov"].ToString();            // наименование товара
                    item.Unit = "796";  // rowItem["nUnitShort"].ToString().ToUpper();    // единицы измерения товара (код)
                    item.UnitName = "шт";

                    decimal price = decimal.Parse(rowItem["Price"].ToString());  //  наща цена с налогом
                    decimal nds = decimal.Parse(rowItem["Nds"].ToString());
                    decimal kol = decimal.Parse(rowItem["Kol"].ToString());
                    //decimal pricePerOne = ((price * kol - price * kol * nds / (1 + nds)) / kol);  Ошибка с округлением копеек 04062020 Кожевников
                    //decimal pricePerOne = price - Math.Round((price * nds ) / (1+nds),2, MidpointRounding.AwayFromZero);

                    item.Quantity = kol;                                                      // количество единиц товара
                    item.QuantitySpecified = true;
                    //item.Price = Math.Round(pricePerOne, 2, MidpointRounding.AwayFromZero);    // цена за единицу товара
                    item.PriceSpecified = true;
                    item.Excise = 0;
                    item.ExciseSpecified = true;
                    item.TaxRate = GetNds(nds);

                    //item.SubtotalWithVatExcluded = Math.Round(pricePerOne * kol, 2, MidpointRounding.AwayFromZero);   // сумма без учета налога
                    //item.SubtotalWithVatExcludedSpecified = true;
                    //item.Vat = Math.Round( price * nds / (1 + nds), 2, MidpointRounding.AwayFromZero) *kol;
                    //item.VatSpecified = true;
                    //item.Subtotal = Math.Round(kol * price, 2, MidpointRounding.AwayFromZero);
                    /*
                     * Изменим порядок и формулу расчета, см. ошибку http://sdsrv/redmine/issues/5009 */
                    //шаг1
                    item.Subtotal = Math.Round(kol * price, 2, MidpointRounding.AwayFromZero);
                    item.SubtotalSpecified = true;
                    //шаг2
                    item.Vat = Math.Round(item.Subtotal / (1 + nds) * nds, 2, MidpointRounding.AwayFromZero);
                    item.VatSpecified = true;
                    //шаг3
                    item.SubtotalWithVatExcluded = Math.Round(item.Subtotal - item.Vat, 2, MidpointRounding.AwayFromZero);   // сумма без учета налога
                    item.SubtotalWithVatExcludedSpecified = true;
                    //шаг4
                    item.Price = kol != 0 ? Math.Round(item.SubtotalWithVatExcluded / kol, 2, MidpointRounding.AwayFromZero) : 0;

                    string madeIn = rowItem["MadeIn"].ToString();
                    madeIn = (madeIn == "0" || madeIn == "978") ? "" : madeIn.Length == 1 ? "00" + madeIn : madeIn.Length == 2 ? "0" + madeIn : madeIn;
                    if (madeIn != "" && rowItem["Declaration"].ToString() != "")
                        item.CustomsDeclarations = new CustomsDeclaration[1] { new CustomsDeclaration() { Country = madeIn, DeclarationNumber = rowItem["Declaration"].ToString() } };

                    ////item.ItemVendorCode = rowItem["Oem"].ToString();
                    item.ItemVendorCode = rowItem["Oem"].ToString().Trim() == "" ? "нет артикула" : rowItem["Oem"].ToString().Trim();

                    result.Add(item);
                    //for debug
                    //var serializer = new XmlSerializer(item.GetType());
                    //var stringwriter = new System.IO.StringWriter();
                    //serializer.Serialize(stringwriter, item);
                    //Logger.WriteErrorMessage("сохраняем item :" + stringwriter.ToString());

                    //for debug debug
                    /*XmlSerializer xmlitem = new XmlSerializer(typeof(InvoiceTableItem));
                    var wayToFile = Directory.GetCurrentDirectory() + @"\log\debug" + DateTime.Now.Ticks + ".xml";
                    using (FileStream fs = new FileStream(wayToFile, FileMode.OpenOrCreate))
                    {
                        xmlitem.Serialize(fs, item);
                    }*/
                }
                var sum_totalwve = Math.Round(result.Sum(p => p.SubtotalWithVatExcluded), 2, MidpointRounding.AwayFromZero);
                var sum_vat = Math.Round(result.Sum(p => p.Vat), 2, MidpointRounding.AwayFromZero);
                var sum_total = Math.Round(result.Sum(p => p.Subtotal), 2, MidpointRounding.AwayFromZero);
                return result;
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("", 1, "Ошибка метода GetExtendedDocItems: " + ex.Message);
                
                return null;
            }
            finally
            {
                //UniLogger.Flush();
            }
        }

        private static ExtendedOrganizationDetails GetOrganisationExtendedInfo(Organization org)
        {
            var addr = new Diadoc.Api.DataXml.Utd820.Address();          

            object addrN;

            if (org.Address.RussianAddress != null)
            {
                Diadoc.Api.Proto.RussianAddress oldAddr = org.Address.RussianAddress;

                addrN = new Diadoc.Api.DataXml.Utd820.RussianAddress()
                {
                    Apartment = oldAddr.Apartment,
                    Block = oldAddr.Block,
                    Building = oldAddr.Building,
                    City = oldAddr.City,
                    Locality = oldAddr.Locality,
                    Region = oldAddr.Region,
                    Street = oldAddr.Street,
                    Territory = oldAddr.Territory,
                    ZipCode = oldAddr.ZipCode.Length > 0 ? oldAddr.ZipCode : "000000"
                };
                //Logger.WriteDebugMessage(org.FullName + " addrn=" + (addrN as Diadoc.Api.DataXml.Utd820.RussianAddress).ZipCode);
            }
            else
            {
                Diadoc.Api.Proto.ForeignAddress oldAddr = org.Address.ForeignAddress;
                addrN = new Diadoc.Api.DataXml.Utd820.ForeignAddress()
                {
                    Address = oldAddr.Address
                      ,
                    Country = oldAddr.Country
                };
            }

            addr.Item = addrN;

            var result = new ExtendedOrganizationDetails()
            {
                Address = addr
                ,
                OrgName = org.FullName
                ,
                Inn = org.Inn
                ,
                Kpp = org.Kpp.Length > 0 ? org.Kpp : null
                ,
                FnsParticipantId = org.FnsParticipantId
                ,
                OrgType = org.Kpp.Length > 0 ? OrganizationType.LegalEntity : OrganizationType.IndividualEntity

            };

            if (addrN.GetType() == typeof(Diadoc.Api.DataXml.Utd820.RussianAddress))
            {
                var addrR = (Diadoc.Api.DataXml.Utd820.RussianAddress)addrN;
                if (addrR.Apartment != "" && addrR.Apartment.Length < 15)
                    addrR.Apartment = "кв./оф. " + addrR.Apartment;
                if (addrR.Block != "" && addrR.Block.Length < 14)
                    addrR.Block = "корп. " + addrR.Block;
                if (addrR.Building != "" && addrR.Building.Length < 14)
                    addrR.Building = "д. " + addrR.Building;

                if (addrR.Building == "")
                    addrR.Building = null;
                if (addrR.Apartment == "")
                    addrR.Apartment = null;
                if (addrR.Locality == "")
                    addrR.Locality = null;
                if (addrR.Block == "")
                    addrR.Block = null;
                if (addrR.City == "")
                    addrR.City = null;
                if (addrR.Territory == "")
                    addrR.Territory = null;
                if (addrR.Street == "")
                    addrR.Street = null;
            }

            return result;
        }

       private static TaxRateWithTwentyPercentAndTaxedByAgent GetNds(decimal nds)
        {
            if (nds == (decimal)0.18)
                return TaxRateWithTwentyPercentAndTaxedByAgent.EighteenPercent;
            if (nds == (decimal)0.2)
                return TaxRateWithTwentyPercentAndTaxedByAgent.TwentyPercent;
            if (nds == (decimal)0.1)
                return TaxRateWithTwentyPercentAndTaxedByAgent.TenPercent;
            return TaxRateWithTwentyPercentAndTaxedByAgent.WithoutVat;
        }

        //Получим подпись
        public static Diadoc.Api.Proto.Invoicing.Signer GetSigner(string inn)
        {
            var sert = Sertificate.DctSertificate[inn];
            return new Diadoc.Api.Proto.Invoicing.Signer()
            {
                SignerCertificate = sert.FileContent
                ,
                SignerCertificateThumbprint = "11"
                ,
                SignerDetails = new Diadoc.Api.Proto.Invoicing.SignerDetails() { FirstName = sert.Name, Inn = inn, Patronymic = sert.Patronymic, Surname = sert.Surname }
            };
        }

        public static ExtendedSignerDetails_SellerTitle GetSignerExt(string inn)
        {            
            var sert = Sertificate.DctSertificate[inn];

            if (inn == "366305506529") //ИП Высоцкая В.Е.
            {
                return new ExtendedSignerDetails_SellerTitle()
                {
                    FirstName = sert.Name,
                    Inn = sert.Inn,
                    LastName = sert.Surname,
                    MiddleName = sert.Patronymic,
                    Position = "Директор",
                    SignerStatus = ExtendedSignerDetailsSignerStatus.SellerEmployee,
                    SignerType = ExtendedSignerDetailsBaseSignerType.IndividualEntity
                };
            }
            else
            {
                return new ExtendedSignerDetails_SellerTitle()
                {
                    FirstName = sert.Name,
                    Inn = sert.Inn,
                    LastName = sert.Surname,
                    MiddleName = sert.Patronymic,
                    Position = "Директор",
                    SignerStatus = ExtendedSignerDetailsSignerStatus.SellerEmployee,
                    SignerType = ExtendedSignerDetailsBaseSignerType.LegalEntity
                };
            }
        }         
    }
}

