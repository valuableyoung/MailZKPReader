using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.Parsers
{
    public class ParserSettingsColumn
    {
        public int idParserSettings { get; set; }
        public int idTypeParserSettingsColumn { get; set; }
        public int ColumnNumber { get; set; }
        public int id { get; set; }
        public int sheetN { get; set; }

        public TypeParserSettingsColumn TypeColumn { get { return (TypeParserSettingsColumn)idTypeParserSettingsColumn; } }

        public string GetColumnName()
        {
            switch (TypeColumn)
            {
                case TypeParserSettingsColumn.Артикул: return "idTovOEM";
                case TypeParserSettingsColumn.Бренд: return "brend";
                case TypeParserSettingsColumn.Количество: return "kol";
                case TypeParserSettingsColumn.МинПартия: return "minpart";
                case TypeParserSettingsColumn.Наименование: return "ntovsupplier";
                case TypeParserSettingsColumn.РекЦена: return "pricetm";
                case TypeParserSettingsColumn.Цена: return "priceCur";
                case TypeParserSettingsColumn.ABC: return "abcsupplier";
            }
            return "";
        }

        public ParserSettingsColumn()
        {

        }

        public ParserSettingsColumn(int idTypeParserSettingsColumn, int columnNumber, int sheetN)
        {
            this.idTypeParserSettingsColumn = idTypeParserSettingsColumn;
            ColumnNumber = columnNumber;
            this.sheetN = sheetN;
        }
    }

    public enum TypeParserSettingsColumn
    {
        Артикул = 1
       , Бренд = 2
       , Цена = 3
       , Количество = 4
       , Наименование = 5
       , РекЦена = 6
       , МинПартия = 7
       , ABC = 8
    }
}
