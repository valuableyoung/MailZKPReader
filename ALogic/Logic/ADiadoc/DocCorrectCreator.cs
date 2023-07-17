using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Organizations;
using Diadoc.Api.Proto.Invoicing.Signers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ALogic.Logic.Old;

namespace ALogic.Logic.ADiadoc
{
    public static class DocCorrectCreator
    {
        public static UniversalCorrectionDocumentSellerTitleInfo CreateUKD(object idDoc, out string BoxId)
        {            
            Logger.SendMessage("Загрузка данных по накладной ID = ");
            var updData = DocCreator.GetDocData(idDoc);
            if (updData == null)
            {
                BoxId = "";
                return null;
            }
                        
            Logger.SendMessage("Создание УПД для накладной ID = ");
            var result = new UniversalCorrectionDocumentSellerTitleInfo();

            result.Function = FunctionType.Invoice;
       
            result.DocumentName = "Документ №" + updData.Nom;
            result.DocumentDate = updData.Date;
            result.DocumentNumber = updData.Nom;
            
            InvoiceForCorrectionInfo infoCorr = new InvoiceForCorrectionInfo();
            infoCorr.InvoiceDate = updData.CorrectionDate;
            infoCorr.InvoiceNumber = updData.CorrectionNom;

            result.Invoices.Add(infoCorr);

            result.Seller = GetOrganisationExtendedInfo(updData.Seller);
            result.Buyer = GetOrganisationExtendedInfo(updData.Buyer);

            result.AddSigner(GetSignerExt(result.Seller.Inn));     //подпись         

            result.EventContent = new EventContent();
            result.EventContent.NotificationDate = updData.Date;

            var updTransferParams = DBDiadoc.GetUpdTransferParams(idDoc);

            result.EventContent.CorrectionBase.Add(new CorrectionBase()
            {
                BaseDocumentDate = DateTime.Parse(updTransferParams["DateContract"].ToString()).ToShortDateString(),
                BaseDocumentName = updTransferParams["nameContract"].ToString(),
                BaseDocumentNumber = updTransferParams["NomContract"].ToString()
            });

            result.EventContent.TransferDocDetails = "Универсальный передаточный документ №" + updData.CorrectionNom + " от " + updData.CorrectionDate;
            result.EventContent.OperationContent = "Предлагаю изменить стоимость";

            Logger.SendMessage("Загрузка товарной части для накладной ID = " + idDoc.ToString());
            var tableItems = GetCorrectionItem(idDoc);
            if (tableItems == null)
            {
                BoxId = "";
                return null;                
            }

            result.InvoiceCorrectionTable = new InvoiceCorrectionTable();
            tableItems.ForEach(p => result.InvoiceCorrectionTable.AddItem(p));

            result.InvoiceCorrectionTable.TotalsDec = new InvoiceTotalsDiff()
            {
                Total = result.InvoiceCorrectionTable.Items.Sum(p => decimal.Parse(p.AmountsDec.Subtotal)).ToString(),
                TotalWithVatExcluded = result.InvoiceCorrectionTable.Items.Sum(p => decimal.Parse(p.AmountsDec.SubtotalWithVatExcluded)).ToString(),
                Vat = result.InvoiceCorrectionTable.Items.Sum(p => decimal.Parse(p.AmountsDec.Vat)).ToString()
            };

            result.Currency = updData.Currency;
            result.CurrencyRate = updData.CurrencyRate;

            result.AdditionalInfoId = new AdditionalInfoId();
            result.AdditionalInfoId.AddAdditionalInfo(new AdditionalInfo() { Id = "1", Value = idDoc.ToString() });

            result.DocumentCreator = result.Seller.OrgName + ", ИНН/КПП " + result.Seller.Inn + "/" + result.Seller.Kpp;

            result.DocumentCreatorBase = "Универсальный передаточный документ №" + updData.CorrectionNom + " от " + updData.CorrectionDate;
                
                /*GovernmentContractInfo 
            */

            BoxId = updData.Buyer.Boxes[0].BoxId;
            return result;
        }
        private static ExtendedOrganizationInfo GetOrganisationExtendedInfo(Organization org)
        {
            var addr = new Address();
            if (org.Address.RussianAddress != null)
            {
                var oldAddr = org.Address.RussianAddress;

                var addrN = new RussianAddress()
                {
                    Apartment = oldAddr.Apartment,
                    Block = oldAddr.Block,
                    Building = oldAddr.Building,
                    City = oldAddr.City,
                    Locality = oldAddr.Locality,
                    Region = oldAddr.Region,
                    Street = oldAddr.Street,
                    Territory = oldAddr.Territory,
                    ZipCode = oldAddr.ZipCode
                };
                addr.RussianAddress = addrN;
            }
            else
            {
                addr.ForeignAddress = org.Address.ForeignAddress;
            }
            var result = new ExtendedOrganizationInfo()
            {
                Address = addr
                ,
                OrgName = org.FullName
                ,
                Inn = org.Inn
                ,
                Kpp = org.Kpp
                ,
                FnsParticipantId = org.FnsParticipantId
                ,
                OrgType = org.Kpp.Length > 0 ? OrgType.LegalEntity : OrgType.IndividualEntity

            };

            if (addr.RussianAddress != null)
            {
                var addrR = addr.RussianAddress;
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
            }

            return result;
        }



        private static List<ExtendedInvoiceCorrectionItem> GetCorrectionItem(object idDoc)
        {
            List<ExtendedInvoiceCorrectionItem> result = new List<ExtendedInvoiceCorrectionItem>();
            Logger.SendMessage("Загрузка товарной части");
            var tableItems = DBDiadoc.GetDocCorrItems(idDoc);
            if (tableItems == null || tableItems.Rows.Count == 0)
            {
                Logger.SendMessage("Не удалось получить товарную часто документа" + idDoc.ToString());
                return null;
            }

            foreach (var rowItem in tableItems.AsEnumerable())
            {
                decimal price = decimal.Parse(rowItem["Price"].ToString());
                decimal nds = decimal.Parse(rowItem["Nds"].ToString());
                decimal kolOld = decimal.Parse(rowItem["oldKol"].ToString());
                decimal kolNew = decimal.Parse(rowItem["newKol"].ToString());
                //decimal pricePerOne = ((price * kolOld - price * kolOld * nds / (1 + nds)) / kolOld);
                //decimal pricePerOne = ((price * kol - price * kol * nds / (1 + nds)) / kol);  Ошибка с округлением копеек 04062020 Кожевников
                //decimal pricePerOne = price - Math.Round((price * nds) / (1 + nds), 2);
                decimal pricePerOne = price - Math.Round((price * nds) / (1 + nds), 2, MidpointRounding.AwayFromZero);
                var item = new ExtendedInvoiceCorrectionItem();

                item.Product = rowItem["NTov"].ToString();
                item.OriginalValues = new CorrectableInvoiceItemFields();
                item.OriginalValues.Quantity = Math.Round(kolOld, 0).ToString();
                item.OriginalValues.TaxRate = GetTaxRate(nds);
                /*item.OriginalValues.Price = Math.Round(pricePerOne, 2, MidpointRounding.AwayFromZero).ToString();
                item.OriginalValues.Subtotal = Math.Round(kolOld * price, 2, MidpointRounding.AwayFromZero).ToString();
                item.OriginalValues.SubtotalWithVatExcluded = Math.Round(pricePerOne * kolOld, 2, MidpointRounding.AwayFromZero).ToString();
                item.OriginalValues.Vat = (Math.Round( price * nds / (1 + nds), 2, MidpointRounding.AwayFromZero) * kolOld).ToString();
                */
                //Изменим порядок и формулу расчета, см. ошибку http://sdsrv/redmine/issues/5009 */
                decimal orig_subtotal, orig_vat, orig_subtotalwve, orig_pricewve = 0;
                orig_subtotal = Math.Round(price * kolOld, 2, MidpointRounding.AwayFromZero);
                orig_vat = Math.Round(orig_subtotal / (1 + nds) * nds, 2, MidpointRounding.AwayFromZero);
                orig_subtotalwve = Math.Round(orig_subtotal - orig_vat, 2, MidpointRounding.AwayFromZero);
                orig_pricewve = kolOld != 0 ? Math.Round(orig_subtotalwve / kolOld, 2, MidpointRounding.AwayFromZero) : 0;

                item.OriginalValues.Subtotal = orig_subtotal.ToString();
                item.OriginalValues.Vat = orig_vat.ToString();
                item.OriginalValues.SubtotalWithVatExcluded = orig_subtotalwve.ToString();
                item.OriginalValues.Price = orig_pricewve.ToString();

                item.CorrectedValues = new CorrectableInvoiceItemFields();
                item.CorrectedValues.Quantity = Math.Round(kolNew, 0).ToString();
                item.CorrectedValues.TaxRate = GetTaxRate(nds);

                //item.CorrectedValues.Price = Math.Round(pricePerOne, 2, MidpointRounding.AwayFromZero).ToString();
                //item.CorrectedValues.Subtotal = Math.Round(kolNew * price, 2, MidpointRounding.AwayFromZero).ToString();
                //item.CorrectedValues.SubtotalWithVatExcluded = Math.Round(pricePerOne * kolNew, 2, MidpointRounding.AwayFromZero).ToString();
                //item.CorrectedValues.Vat = (Math.Round(kolNew * price * nds / (1 + nds), 2, MidpointRounding.AwayFromZero) * kolNew).ToString();
                //Изменим порядок и формулу расчета, см. ошибку http://sdsrv/redmine/issues/5009 */
                decimal correct_subtotal, correct_vat, correct_subtotalwve, correct_pricewve = 0;
                correct_subtotal = Math.Round(price * kolNew, 2, MidpointRounding.AwayFromZero);
                correct_vat = Math.Round(correct_subtotal / (1 + nds) * nds, 2, MidpointRounding.AwayFromZero);
                correct_subtotalwve = Math.Round(correct_subtotal - correct_vat, 2, MidpointRounding.AwayFromZero);
                correct_pricewve = kolNew != 0 ? Math.Round(correct_subtotalwve / kolNew, 2, MidpointRounding.AwayFromZero) : 0;

                item.CorrectedValues.Subtotal = correct_subtotal.ToString();
                item.CorrectedValues.Vat = correct_vat.ToString();
                item.CorrectedValues.SubtotalWithVatExcluded = correct_subtotalwve.ToString();
                item.CorrectedValues.Price = correct_pricewve.ToString();

                //item.AmountsDec = new InvoiceItemAmountsDiff()
                //{
                //    Subtotal = (decimal.Parse(item.OriginalValues.Subtotal) - decimal.Parse(item.CorrectedValues.Subtotal)).ToString(),
                //    SubtotalWithVatExcluded = (decimal.Parse(item.OriginalValues.SubtotalWithVatExcluded) - decimal.Parse(item.CorrectedValues.SubtotalWithVatExcluded)).ToString(),
                //    Vat = (decimal.Parse(item.OriginalValues.Vat) - decimal.Parse(item.CorrectedValues.Vat)).ToString()
                //};  
                item.AmountsDec = new InvoiceItemAmountsDiff()
                {
                    Subtotal = (orig_subtotal - correct_subtotal).ToString(),
                    SubtotalWithVatExcluded = (orig_subtotalwve - correct_subtotalwve).ToString(),
                    Vat = (orig_vat - correct_vat).ToString()
                };


                result.Add(item);
            }
            //for debug
            var test_sum = Math.Round(result.Sum(p => decimal.Parse(p.AmountsDec.SubtotalWithVatExcluded)), 2, MidpointRounding.AwayFromZero);
            var test_vsum = Math.Round(result.Sum(p => decimal.Parse(p.AmountsDec.Vat)), 2, MidpointRounding.AwayFromZero);
            var test_stsum = Math.Round(result.Sum(p => decimal.Parse(p.AmountsDec.Subtotal)), 2, MidpointRounding.AwayFromZero);

            return result;
        }

        private static TaxRate GetTaxRate(decimal nds)
        {
            /* на старых версиях VS не работает switch с decimal
             * switch (nds)
            {
                case 0.2M: return TaxRate.Percent_20;
                case 0.1M: return TaxRate.Percent_10;
                case 0: return TaxRate.NoVat;
            }*/
            if (nds == 0.2M) { return TaxRate.Percent_20; }
            if (nds == 0.1M) { return TaxRate.Percent_10; }
            if (nds == 0) { return TaxRate.NoVat; }
            return TaxRate.NoVat;
        }    

        public static ExtendedSigner GetSignerExt(string inn)
        {
            var sert = Sertificate.DctSertificate[inn];
            return new ExtendedSigner()
            {
                SignerCertificate = sert.FileContent,
                SignerCertificateThumbprint = "11",

                SignerDetails = new ExtendedSignerDetails()
                {
                    FirstName = sert.Name,
                    Inn = inn,
                    Patronymic = sert.Patronymic,
                    Surname = sert.Surname,
                    SignerType = SignerType.LegalEntity,
                    SignerStatus = SignerStatus.SellerEmployee,
                    JobTitle = "Директор"
                }
            };
        }
    }
}
