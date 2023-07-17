using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ALogic.Logic.SPR.Old;

namespace ALogic.Logic.Old.BuySalePlan
{
    public static class BuyPlanLogic
    {
        public static DataTable GetBuyPlan(int NomYear)
        {
            var dtData = DBBuyPlan.GetPlanData(NomYear);
            var dtNaklRasx = DBBuyPlan.GetNaklRasx();
            var customBonus = DBBuyPlan.GetSalerPlanCustomBonus(NomYear);
            var result = new DataTable();

            result.Columns.Add("idbrend", typeof(int));
            result.Columns.Add("idSegm", typeof(int));
            result.Columns.Add("Marga", typeof(decimal));
            result.Columns.Add("naklRasx", typeof(decimal));
            result.Columns.Add("idCur", typeof(int));

            result.Columns.Add("tovOstS", typeof(decimal));
            result.Columns.Add("tovOstE", typeof(decimal));

            for (int i = 1; i < 13; i++)
                result.Columns.Add("k" + i.ToString(), typeof(int));

            //result.Columns.Add("vSalerPlan", typeof(decimal));
            result.Columns.Add("p1", typeof(decimal));
            result.Columns.Add("p2", typeof(decimal));
            result.Columns.Add("p3", typeof(decimal));
            result.Columns.Add("p4", typeof(decimal));
            result.Columns.Add("pf", typeof(decimal));
            // result.Columns.Add("pSum", typeof(decimal));



            var listBrend = DBSprBrend.GetBrendsForPlan().AsEnumerable().Where(p => p["tm_id"].ToString() != "0").Select(p => p["tm_id"].ToString()).ToList();
            var listSegment = DBSprSegment.GetSegmentForPlan().AsEnumerable().Select(p => p["idGroup"].ToString()).ToList();

            FillDataBayPlan(dtData, dtNaklRasx, listBrend, listSegment, result, true, customBonus);

            var ttemp = dtData.AsEnumerable().Where(p => listBrend.FirstOrDefault(q => q == p["idbrand"].ToString()) == null ||
                                                         listSegment.FirstOrDefault(q => q == p["idGroupSegment"].ToString()) == null);
            if (ttemp.Count() > 0)
            {
                DataTable dtDopData = ttemp.CopyToDataTable();

                var listDopBrend = dtDopData.AsEnumerable().Select(p => p["idbrand"].ToString()).Distinct().ToList();
                var listDopSegment = dtDopData.AsEnumerable().Select(p => p["idGroupSegment"].ToString()).Distinct().ToList();
                FillDataBayPlan(dtDopData, dtNaklRasx, listDopBrend, listDopSegment, result, false, customBonus);
            }

            foreach (var row in result.AsEnumerable())
                row.AcceptChanges();
            return result;
        }

        public static DataTable GetSalerPlanCustomBonus(int year)
        {

            return DBBuyPlan.GetSalerPlanCustomBonus(year);   //"select * from BrandSellerPlanCustomBonus where year = @year and idtm = @idtm"
        }

        private static void FillDataBayPlan(DataTable dtData, DataTable dtNaklRasx, List<string> listBrend, List<string> listSegment, DataTable result, bool full, DataTable CustomBonus)
        {
            foreach (var brend in listBrend)
            {
                var rowNaklRasx = dtNaklRasx.AsEnumerable().FirstOrDefault(p => p["id_tm"].ToString() == brend);
                decimal naklRasx = rowNaklRasx == null ? 0 : decimal.Parse(rowNaklRasx["value"].ToString());


                var customBrandList = (from DataRow i in CustomBonus.AsEnumerable() where i.Field<int>("idTm").ToString() == brend select i).ToList();



                foreach (var segm in listSegment)
                {
                    var rows = dtData.AsEnumerable().Where(p => p["idGroupSegment"].ToString() == segm && p["idbrand"].ToString() == brend).ToList();

                    if (!full && rows.Count == 0)
                        continue;

                    var newRow = result.NewRow();
                    newRow["idbrend"] = brend;
                    newRow["idSegm"] = segm;
                    newRow["naklRasx"] = naklRasx;

                    var rowData = rows.FirstOrDefault();
                    if (rowData == null)
                    {
                        newRow["Marga"] = 0;
                        newRow["idCur"] = 0;
                    }
                    else
                    {
                        newRow["Marga"] = rowData["Marginality"];
                        newRow["idCur"] = rowData["idCur"];
                    }

                    newRow["tovOstS"] = 100;
                    newRow["tovOstE"] = 100;

                    for (int i = 1; i < 13; i++)
                    {
                        var row = rows.FirstOrDefault(p => p["nomMonth"].ToString() == i.ToString());
                        if (row == null)
                            newRow["k" + i.ToString()] = 0;
                        else
                            newRow["k" + i.ToString()] = row["SumBuy"];
                    }

                    newRow["p1"] = (from i in customBrandList where i.Field<int>("nomKv") == 1 select i["bonus"]).FirstOrDefault() ?? 0;
                    newRow["p2"] = (from i in customBrandList where i.Field<int>("nomKv") == 2 select i["bonus"]).FirstOrDefault() ?? 0;
                    newRow["p3"] = (from i in customBrandList where i.Field<int>("nomKv") == 3 select i["bonus"]).FirstOrDefault() ?? 0;
                    newRow["p4"] = (from i in customBrandList where i.Field<int>("nomKv") == 4 select i["bonus"]).FirstOrDefault() ?? 0;
                    newRow["pf"] = (from i in customBrandList where i.Field<int>("nomKv") == 0 select i["bonus"]).FirstOrDefault() ?? 0;


                    result.Rows.Add(newRow);
                }
            }
        }

        public static void SaveData(System.Data.DataRow row, int year)
        {
            int idBrand = row["idBrend"] == null || row["idBrend"].ToString() == "" ? 0 : int.Parse(row["idBrend"].ToString());
            int idSegm = row["idSegm"] == null || row["idSegm"].ToString() == "" ? 0 : int.Parse(row["idSegm"].ToString());
            int idCur = row["idCur"] == null || row["idCur"].ToString() == "" ? 0 : int.Parse(row["idCur"].ToString());
            decimal marga = row["marga"] == null || row["marga"].ToString() == "" ? 0 : decimal.Parse(row["marga"].ToString());

            for (int i = 1; i < 13; i++)
            {
                var oborot = row["k" + i.ToString()] == null || row["k" + i.ToString()].ToString() == "" ? 0 : decimal.Parse(row["k" + i.ToString()].ToString());
                DBBuyPlan.SavePlan(idBrand, idSegm, idCur, year, i, oborot, marga / 100);
            }
        }

        internal static DataTable InsertSalerPlanCustomBonus(int idTm, int nomKv, decimal bonus, int year)
        {
           return DBBuyPlan.InsertSalerPlanCustomBonus(idTm, nomKv, bonus, year);
        }

        public static void DelData(System.Data.DataRow row, int year)
        {
            int idBrand = row["idBrend"] == null || row["idBrend"].ToString() == "" ? 0 : int.Parse(row["idBrend"].ToString());
            int idSegm = row["idSegm"] == null || row["idSegm"].ToString() == "" ? 0 : int.Parse(row["idSegm"].ToString());

            for (int i = 1; i < 13; i++)
            {
                DBBuyPlan.DelPlan(idBrand, idSegm, year);
            }
        }

        internal static DataTable GetSalerPlanData(int year)
        {
            return DBBuyPlan.GetSalerPlan(year);
        }

        internal static DataTable GetSalerPlanDataByTm(int year, int idtm)
        {
            return DBBuyPlan.GetSalerPlanByTm(year, idtm);
        }

        internal static void SaveDataPlan(DataRow row)
        {
            object idTm = row["idTm"];
            object vProd = row["vProd"];
            object Perc = row["Perc"];
            object nomKv = row["nomKv"];
            object nomYear = row["nomYear"];
            if (int.Parse(vProd.ToString()) != 0 && int.Parse(Perc.ToString()) != 0)
                DBBuyPlan.SaveSalerPlan(idTm, vProd, Perc, nomKv, nomYear);
        }

        internal static void DelDataPlan(DataRow row)
        {
            object idTm = row["idTm"];
            object vProd = row["vProd"];
            object Perc = row["Perc"];
            object nomKv = row["nomKv"];
            object nomYear = row["nomYear"];

            DBBuyPlan.DeleteSalerPlan(idTm, vProd, nomKv, nomYear);
        }

        internal static void ClearCustomBonus(int idTm, int year)
        {
            DBBuyPlan.ClearCustomBonus(idTm, year);
        }
    }
}
