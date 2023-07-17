using ALogic.Logic.SPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.DebKredZad
{
    public static class RepKredZadLogic
    {
        public static DataTable GetReport()
        {
            var kontr = DBRepKredZad.GetKontrForRep();
            var docs = DBRepKredZad.GetDocForRep();

            kontr.Columns.Add("CurRec", typeof(decimal));
            kontr.Columns.Add("DZ", typeof(int));

            kontr.Columns.Add("KZ", typeof(int));
            kontr.Columns.Add("PKZp", typeof(decimal));
            kontr.Columns.Add("PKZ", typeof(int));

            kontr.Columns.Add("k07", typeof(int));
            kontr.Columns.Add("k815", typeof(int));
            kontr.Columns.Add("k1621", typeof(int));
            kontr.Columns.Add("k2230", typeof(int));
            kontr.Columns.Add("k31", typeof(int));

            docs.Columns.Add("dayP", typeof(int));

            var curValueCB = ValuteLogic.GetCurCBR();

            kontr.AsEnumerable().Where(p => p["idCur"].ToString() == "0").ToList().ForEach(p => p["CurRec"] = 1);
            kontr.AsEnumerable().Where(p => p["idCur"].ToString() == "1").ToList().ForEach(p => p["CurRec"] = curValueCB.USD);
            kontr.AsEnumerable().Where(p => p["idCur"].ToString() == "4").ToList().ForEach(p => p["CurRec"] = curValueCB.EURO);
            kontr.AsEnumerable().Where(p => p["idCur"].ToString() == "6").ToList().ForEach(p => p["CurRec"] = curValueCB.JPY);

            foreach (var row in kontr.AsEnumerable())
            {
                var docKr = docs.AsEnumerable().Where(p => int.Parse(p["idSupplier"].ToString()) == int.Parse(row["idKontr"].ToString()) //&& int.Parse(p["idConv"].ToString()) == int.Parse(row["idConv"].ToString()) 
                                                         && int.Parse(p["idCur"].ToString()) == int.Parse(row["idCur"].ToString()) && decimal.Parse(p["SumDoc"].ToString()) > 0).ToList();


                var docDb = docs.AsEnumerable().Where(p => int.Parse(p["idSupplier"].ToString()) == int.Parse(row["idKontr"].ToString()) //&& int.Parse(p["idConv"].ToString()) == int.Parse(row["idConv"].ToString()) 
                                                         && int.Parse(p["idCur"].ToString()) == int.Parse(row["idCur"].ToString()) && decimal.Parse(p["SumDoc"].ToString()) < 0).ToList();

                if (docKr.Count == 0 && docDb.Count == 0)
                {
                    row.Delete();
                    continue;
                }

                int dayPay = int.Parse(row["KolDay"].ToString());


                docKr.ForEach(p => p["dayP"] = (DateTime.Now.Date - ((DateTime)p["DateDoc"])).Days - dayPay);

                row["DZ"] = -Math.Round(docDb.Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString())), 0);

                row["KZ"] = Math.Round(docKr.Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString())), 0);
                row["PKZ"] = docKr.Where(p => decimal.Parse(p["dayP"].ToString()) > 0).Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString()));

                if (decimal.Parse(row["KZ"].ToString()) > 0)
                    row["PKZp"] = Math.Round(decimal.Parse(row["PKZ"].ToString()) / decimal.Parse(row["KZ"].ToString()) * 100, 2);

                row["k07"] = Math.Round(docKr.Where(p => int.Parse(p["dayP"].ToString()) > 0 && int.Parse(p["dayP"].ToString()) <= 7).Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString())), 0);
                row["k815"] = Math.Round(docKr.Where(p => int.Parse(p["dayP"].ToString()) > 7 && int.Parse(p["dayP"].ToString()) <= 15).Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString())), 0);
                row["k1621"] = Math.Round(docKr.Where(p => int.Parse(p["dayP"].ToString()) > 15 && int.Parse(p["dayP"].ToString()) <= 22).Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString())), 0);
                row["k2230"] = Math.Round(docKr.Where(p => int.Parse(p["dayP"].ToString()) > 22 && int.Parse(p["dayP"].ToString()) <= 31).Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString())), 0);
                row["k31"] = Math.Round(docKr.Where(p => int.Parse(p["dayP"].ToString()) > 31).Sum(p => decimal.Parse(p["SumDoc"].ToString()) - decimal.Parse(p["SumLinc"].ToString())), 0);

            }

            return kontr;
        }

        public static DataTable GetReportDZ(object date)
        {
            return DBRepKredZad.GetDebZad(date);
        }
    }
}
