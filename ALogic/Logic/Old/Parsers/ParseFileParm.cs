using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.Parsers
{
    public class ParseFileParm
    {
        public string nColumnDW { get; set; }
        public string nColumnFile { get; set; }
        public int fPiece { get; set; }
        public string PieceDelimiter { get; set; }
        public int ColumnNumber { get; set; }
        public string nColumnActual { get; set; }

        public static int ParceNomColumn(string nColumn)
        {
            if (nColumn.Length == 1)
                return (int)char.Parse(nColumn) - 65;
            else
                return ((int)nColumn[0] - 64) * 26 + (int)nColumn[1] - 65;
        }

        public ColType type { get; set; }

        public ParseFileParm(string nColumnDW, string nColumnFile, int fPiece, string PieceDelimiter)
        {
            this.nColumnDW = nColumnDW;
            this.nColumnFile = nColumnFile;
            this.fPiece = fPiece;
            this.PieceDelimiter = PieceDelimiter;
            this.ColumnNumber = ParceNomColumn(nColumnFile);

            switch (nColumnDW)
            {
                case "pricestring": { type = ColType.Цена; nColumnActual = "priceCur"; } break;
                case "kolstring": { type = ColType.Количество; nColumnActual = "kol"; } break;
                case "idTovOEM": { type = ColType.Артикул; nColumnActual = "idTovOEM"; } break;
                case "brend": { type = ColType.Бренд; nColumnActual = "brend"; } break;
                case "ntovsupplier": { type = ColType.Наименование; nColumnActual = "ntovsupplier"; } break;
                case "StockNum": { type = ColType.СтокНомер; nColumnActual = "StockNum"; } break;
                case "pricetmstring": { type = ColType.ЦенаБренда; nColumnActual = "pricetm"; } break;
                case "minpart": { type = ColType.МинПартия; nColumnActual = "minpart"; } break;

                case "summaCur": { type = ColType.Сумма; nColumnActual = "summaCur"; } break;
                case "madeIn": { type = ColType.СтранаПроисхождения; nColumnActual = "madeIn"; } break;
                case "declaration": { type = ColType.Декларация; nColumnActual = "declaration"; } break;

                case "oemTov": { type = ColType.Артикул; nColumnActual = "idTovOEM"; } break;
                case "kol": { type = ColType.Количество; nColumnActual = "kol"; } break;
                case "priceTov": { type = ColType.Цена; nColumnActual = "priceCur"; } break;
                case "brendTov": { type = ColType.Бренд; nColumnActual = "brend"; } break;

                case "price": { type = ColType.Сумма; nColumnActual = "summaCur"; } break;
                case "nCountryDomain": { type = ColType.СтранаПроисхождения; nColumnActual = "madeIn"; } break;
                default: type = ColType.Пропустить; break;
            }
        }
    }

    public enum ColType
    {
        Пропустить
        , Артикул
        , Бренд
        , Цена
        , Количество
        , Наименование
        , СтокНомер
        , ЦенаБренда
        , МинПартия
        , Сумма
        , СтранаПроисхождения
        , Декларация
    }


    public enum ParseType
    {
        ПрайсПОставщикаСЦенойИКоличеством = 1
        , ПодтверждениеОтПоставщика = 12
        , УПДотПоставщика = 13
    }
}
