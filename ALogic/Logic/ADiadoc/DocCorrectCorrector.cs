using ALogic.Logic.Reload1C;
using Diadoc.Api.Proto.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.ADiadoc
{
    public static class DocCorrectCorrector
    {
        public static void Correct(UniversalCorrectionDocumentSellerTitleInfo doc)
        {
            switch (doc.Buyer.Inn)
            {
                case "7714345645": CorrectForAutoDoc(doc); break;
                case "7729462060": CorrectForCBAuto(doc); break;
                case "7721568730": CorrectForAutoStels(doc); break;
                case "7702655917": CorrectForAE(doc); break;
                case "7706442758": CorrectAdeoPro(doc); break;
                case "6234176360": CorrecVitral(doc); break;
            }
        }

        private static void CorrecVitral(UniversalCorrectionDocumentSellerTitleInfo doc)
        {
            doc.Buyer.Kpp = "771845001";
        }

        private static void CorrectAdeoPro(UniversalCorrectionDocumentSellerTitleInfo doc)
        {
            try
            {
                doc.Function = FunctionType.InvoiceAndBasic;
                doc.Buyer.OrgName = @"ООО ""АДЕО.ПРО""";
                var russAddrBuyers = doc.Buyer.Address.RussianAddress;

                russAddrBuyers.Territory = null;
                russAddrBuyers.Street = "проспект Ленинский";
                russAddrBuyers.Building = "6";
                russAddrBuyers.Block = "строение 7";
                russAddrBuyers.Apartment = "эт4 пIII к10 оф19";

                UniLogger.WriteLog("", 0, "CorrectAdeoPro - OK");
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("", 1, "CorrectDocumentSellerTitleInfo CorrectAdeoPro - error: " + ex.Message);
            }
        }

        private static void CorrectForAE(UniversalCorrectionDocumentSellerTitleInfo doc)
        {           
            doc.Buyer.Address.RussianAddress.Street = "ул. " + doc.Buyer.Address.RussianAddress.Street;
        }
        private static void CorrectForAutoStels(UniversalCorrectionDocumentSellerTitleInfo doc)
        {
            doc.Buyer.OrgName = @"Общество с ограниченной ответственностью ""Автостэлс""";
            doc.Buyer.Kpp = "772045002";
            if (doc.Buyer.Address.RussianAddress != null)
                doc.Buyer.Address.RussianAddress.Apartment = "Офис " + doc.Buyer.Address.RussianAddress.Apartment;  
            
        }

        private static void CorrectForCBAuto(UniversalCorrectionDocumentSellerTitleInfo doc)
        {
            doc.Buyer.BoxId = "a86df985-86bf-4a98-ac08-6659041e6197";
            doc.Buyer.FnsParticipantId = "2ae3f4be91c-3a94-4b0a-8873-5f85ab48ff0f";
        }     

        private static void CorrectForAutoDoc(UniversalCorrectionDocumentSellerTitleInfo doc)
        {    
            doc.Buyer.OrgName = doc.Buyer.OrgName.Replace("Автодок", "АВТОДОК");  
            doc.Buyer.Kpp = "500345001";
        }       
    }
}
