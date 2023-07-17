using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.Parsers
{
    public class ParserSettings
    {

        public ParserSettings()
        {
            parserSettingsColumns = new List<ParserSettingsColumn>();
        }

        public int idParserSettings { get; set; }
        public string nParserSettings { get; set; }

        public int idSupplier { get; set; }

        public int idSource { get; set; }
        public int fArmRtk { get; set; }

        public int fPriceOnline { get; set; }

        public int fPriceCompetitor { get; set; }

        public int fCorrectCoeff { get; set; }

        public int fAutoload { get; set; }

        public int idTypeSource { get; set; }

        public string Mail { get; set; }

        public string MailFileMask { get; set; }

        public string FtpLogin { get; set; }

        public string FtpPass { get; set; }

        public string FtpServer { get; set; }

        public int StartRow { get; set; }
        public int fNds { get; set; }

        public int PricePercent { get; set; }
        public int fBrend { get; set; }

        public string nBrend { get; set; }

        public int fAllList { get; set; }

        public int fHardLoad { get; set; }

        public string Separator { get; set; }

        public int fMarketDiscount { get; set; }

        public int IdCur { get; set; }

        public int fDelOldPrice { get; set; }
        public int KolDayActual { get; set; }
        public int fOvveridePerPrice { get; set; }
        public int idSkladSupplier { get; set; }
        public int fCompetitorType { get; set; }


        public List<ParserSettingsColumn> parserSettingsColumns { get; set; }

        public void SaveSettingsColumn(List<ParserSettingsColumn> list, int sheetN)
        {
            parserSettingsColumns.RemoveAll(n => n.sheetN == sheetN);
            parserSettingsColumns.AddRange(list);

        }

        public void ShowAll(object o)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(o);
                Console.WriteLine("{0} = {1}", name, value);
            }
        }


    }
}
