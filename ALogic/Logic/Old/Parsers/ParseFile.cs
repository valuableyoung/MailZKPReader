using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.Parsers
{
    public class ParseFile
    {
        public int idParseFile { get; set; }
        public int IdKontr { get; set; }
        public string FileType { get; set; }
        public string FileDetail { get; set; }
        public int IdTypeEDI { get; set; }
        public int StartRow { get; set; }
        public string Separator { get; set; }
        public int IdCur { get; set; }
        public string NomSF { get; set; }
        public string DateSF { get; set; }
        public int FPacking { get; set; }
        public int FCalcWithNDS { get; set; }
        public decimal RateNDS { get; set; }
        public string NomSFmask { get; set; }
        public string DateSFMask { get; set; }
        public int fRewriteByOffice { get; set; }

        public int NomList { get; set; }

        public DataRow Row { get; set; }

        public List<ParseFileParm> ListParms { get; set; }

        public ParseFile(object idKontr, object idTypeEdi)
        {
            Row = DBMailProperty.SelectParseFile(idKontr, idTypeEdi);

            if (Row != null)
            {
                idParseFile = Int32.Parse(Row["idParseFile"].ToString());
                IdKontr = Int32.Parse(Row["idKontr"].ToString());
                FileType = Row["FileType"].ToString();
                FileDetail = Row["FileDetail"].ToString();
                IdTypeEDI = Int32.Parse(Row["IdTypeEDI"].ToString());
                StartRow = Int32.Parse(Row["StartRow"].ToString());
                Separator = Row["Separator"].ToString();
                IdCur = Int32.Parse(Row["IdCur"].ToString());
                NomSF = Row["DopInfo"] == DBNull.Value ? "" : Row["DopInfo"].ToString();
                DateSF = Row["DatePars"] == DBNull.Value ? "" : Row["DatePars"].ToString();
                FPacking = Int32.Parse(Row["FPacking"].ToString());
                FCalcWithNDS = Row["FCalcWithNDS"] == DBNull.Value ? 0 : Int32.Parse(Row["FCalcWithNDS"].ToString());
                RateNDS = Row["RateNDS"] == DBNull.Value ? 0 : decimal.Parse(Row["RateNDS"].ToString());
                NomSFmask = Row["NomSFmask"] == DBNull.Value ? "" : Row["NomSFmask"].ToString();
                DateSFMask = Row["DateSFMask"] == DBNull.Value ? "" : Row["DateSFMask"].ToString();
                fRewriteByOffice = Row["fRewriteByOffice"] == DBNull.Value ? 0 : int.Parse(Row["fRewriteByOffice"].ToString());

                int tempnom = 1;
                if (Row["fileDetail"] != DBNull.Value)
                    int.TryParse(Row["fileDetail"].ToString(), out tempnom);

                NomList = tempnom > 0 ? tempnom - 1 : 0;

                var table = DBMailProperty.SelectsParseFileParm(idParseFile);
                ListParms = new List<ParseFileParm>();

                foreach (var parRow in table.AsEnumerable())
                {
                    ListParms.Add(new ParseFileParm(parRow["nColumnDW"].ToString(), parRow["nColumnFile"].ToString(), int.Parse(parRow["fPiece"].ToString()), parRow["PieceDelimiter"].ToString()));
                }

                ListParms = ListParms.Where(p => p.nColumnActual != null && p.nColumnActual != "").Where(p => p.type != ColType.Пропустить).ToList();
            }
        }


    }
}
