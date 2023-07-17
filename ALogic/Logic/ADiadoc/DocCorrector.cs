
using Diadoc.Api.DataXml.Utd820;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.ADiadoc
{
    public static class DocCorrector
    {
        public static void Correct(UniversalTransferDocument doc)
        {
            switch ((doc.Buyers[0].Item as ExtendedOrganizationDetails).Inn)
            {
                case "7714345645": CorrectForAutoDoc(doc); break;
                case "7729462060": CorrectForCBAuto(doc); break;
                case "7721568730": CorrectForAutoStels(doc); break;
                case "7702655917": CorrectForAE(doc); break;
                case "7706442758": CorrectAdeoPro(doc); break;
                case "6234176360": CorrecVitral(doc); break;
                case "7806460052": CorrectFort(doc); break;
            }
        }

        private static void CorrectFort(UniversalTransferDocument doc)
        {
            var russAddrCons = ((doc.Consignees[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);

            russAddrCons.Territory = null;
            russAddrCons.City = "г. Москва, поселение Мосрентген, поселок завода Мосрентген";
            russAddrCons.Street = "ул. Героя России Соломатина";
            russAddrCons.Locality = null;
            russAddrCons.Block = null;
            russAddrCons.Building = "двлд. 6";
            russAddrCons.Apartment = "к. 10";
            russAddrCons.ZipCode = "108820";

            (doc.Consignees[0].Item as ExtendedOrganizationDetails).OrgName = @"ООО ""Форт""";
        }


        private static void CorrecVitral(UniversalTransferDocument doc)
        {
            var russAddrCons = ((doc.Consignees[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);

            russAddrCons.Region = "50";
            russAddrCons.Territory = null;
            russAddrCons.City = "г. Москва";
            russAddrCons.Street = "ул. Курганская";
            russAddrCons.Locality = null;
            russAddrCons.Block = "стр. 1";
            russAddrCons.Building = "д. 3А";
            russAddrCons.Apartment = null;
            russAddrCons.ZipCode = "107065";

            (doc.Buyers[0].Item as ExtendedOrganizationDetails).Kpp = "771845001";
        }

        private static void CorrectAdeoPro(UniversalTransferDocument doc)
        {
            try
            {
                (doc.Buyers[0].Item as ExtendedOrganizationDetails).OrgName = @"ООО ""АДЕО.ПРО""";
                (doc.Consignees[0].Item as ExtendedOrganizationDetails).OrgName = @"ООО ""АДЕО.ПРО""";

                var russAddrCons = ((doc.Consignees[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);

                russAddrCons.ZipCode = "109316";
                russAddrCons.Territory = null;
                russAddrCons.Street = "ул. Талалихина ";
                //russAddrCons.Building = "д.41";
                russAddrCons.Building = "41";
                russAddrCons.Block = "стр.1";
                russAddrCons.Apartment = null;

                var russAddrBuyers = ((doc.Buyers[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);

                russAddrBuyers.Territory = null;
                russAddrBuyers.Street = "проспект Ленинский";
                //russAddrBuyers.Building = "дом 6";
                russAddrBuyers.Building = "6";
                russAddrBuyers.Block = "строение 7";
                russAddrBuyers.Apartment = "эт4 пIII к10 оф19";

                ALogic.Logic.Reload1C.UniLogger.WriteLog("", 0, "CorrectAdeoPro: - OK");
            }
            catch (Exception ex)
            {
                ALogic.Logic.Reload1C.UniLogger.WriteLog("", 1, "Ошибка метода CorrectAdeoPro: " + ex.Message);
            }
        }

        private static void CorrectForAE(UniversalTransferDocument doc)
        {
            var rusAddr = ((doc.Consignees[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);
            rusAddr.Street = "ул. " + rusAddr.Street;

            var rusAddrBuyer = ((doc.Buyers[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);
            rusAddrBuyer.Street = "ул. " + rusAddrBuyer.Street;
        }

        private static void CorrectForAutoStels(UniversalTransferDocument doc)
        {
            (doc.Buyers[0].Item as ExtendedOrganizationDetails).OrgName = @"Общество с ограниченной ответственностью ""Автостэлс""";
            (doc.Buyers[0].Item as ExtendedOrganizationDetails).Kpp = "772045002";
            var rusAddr = ((doc.Buyers[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);

            if ( rusAddr != null )
                rusAddr.Apartment = "Офис " + rusAddr.Apartment;

            (doc.Consignees[0].Item as ExtendedOrganizationDetails).OrgName = @"Общество с ограниченной ответственностью ""Автостэлс""";
            var russAddrCons = ((doc.Consignees[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);

            if (russAddrCons != null)
            {
                russAddrCons.ZipCode = "111141";
                russAddrCons.Street = "3-й проезд Перова Поля";
                russAddrCons.Building = "дом №8";
                russAddrCons.Block = "стр. 1";
                russAddrCons.Apartment = null;
                russAddrCons.Locality = null;
            }
            else
            {
                var enAddr = ((doc.Consignees[0].Item as ExtendedOrganizationDetails).Address.Item as ForeignAddress);
                if (enAddr != null)
                {
                    enAddr.Address = "111141, г.Москва, Перова Поля 3-й проезд, дом №8, строение 1";
                }
            }

        }

        private static void CorrectForCBAuto(UniversalTransferDocument doc)
        {
            //(doc.Buyers[0].Item as ExtendedOrganizationDetails).BoxId = "a86df985-86bf-4a98-ac08-6659041e6197";
            (doc.Buyers[0].Item as ExtendedOrganizationDetails).FnsParticipantId = "2ae3f4be91c-3a94-4b0a-8873-5f85ab48ff0f";
        }

        private static void CorrectForAutoDoc(UniversalTransferDocument doc)
        {
            var russAddrCons = ((doc.Consignees[0].Item as ExtendedOrganizationDetails).Address.Item as RussianAddress);

            russAddrCons.Region = "50";
            russAddrCons.Territory = "Ленинский р-н";
            russAddrCons.City = "г-п Видное";
            russAddrCons.Street = "д. Апаринки";
            russAddrCons.Locality = null;
            russAddrCons.Block = null;
            russAddrCons.Building = null;
            russAddrCons.Apartment = null;
            russAddrCons.ZipCode = "142715";

            (doc.Buyers[0].Item as ExtendedOrganizationDetails).OrgName = (doc.Buyers[0].Item as ExtendedOrganizationDetails).OrgName.Replace("Автодок", "АВТОДОК");
            (doc.Consignees[0].Item as ExtendedOrganizationDetails).OrgName = "ООО \"АВТОДОК\" г-п Видное";

            (doc.Buyers[0].Item as ExtendedOrganizationDetails).Kpp = "500345001";
        }    
    }
}
