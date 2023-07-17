using Diadoc.Api.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.ADiadoc
{

    public class DocData
    {
        public object idDoc { get; set; }
        public string Date { get; set; }
        public string Nom { get; set; }
        public Organization Seller { get; set; }
        public Organization Buyer { get; set; }
        public Organization Shipper { get; set; }
        public Organization Consignee { get; set; }
        public int ConsigneeId { get; set; }
        public string Currency { get; set; }

        public string CorrectionDate { get; set; }
        public string CorrectionNom { get; set; }

        public string CurrencyRate { get; set; }

        public string TransferBase { get; set; }
    }
}
