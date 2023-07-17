using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ALogic.Logic.SPR.Old;

namespace ALogic.Logic.Old.BuySalePlan
{
    public static class SalePlanLogic
    {
        public static DataTable GetSalePlan(int NomYear, bool onlyentered)
        {
            var dtData = DBSalePlan.GetPlanData(NomYear);
            var dtNaklRasx = DBSalePlan.GetNaklRasx();

            var result = new DataTable();
            result.Columns.Add("idRegion", typeof(int));
            result.Columns.Add("idbrend", typeof(int));
            result.Columns.Add("idSegm", typeof(int));
            result.Columns.Add("Marga", typeof(decimal));
            result.Columns.Add("naklRasx", typeof(decimal));
            result.Columns.Add("type", typeof(string));

            for (int i = 1; i < 13; i++)
            { 
                result.Columns.Add("k" + i.ToString(), typeof(decimal));
            }

            //for (int i = 1; i < 13; i++)
            //{
            //    result.Columns.Add("mg" + i.ToString(), typeof(decimal));

            //}


            var listBrend = DBSprBrend.GetBrendsForPlan().AsEnumerable().Where(p => p["tm_id"].ToString() != "0").Select(p => p["tm_id"].ToString()).ToList();
            var listRegion = DBRegion.GetRegionsForPlan().AsEnumerable().Where(p => p["idRegion"].ToString() != "0").Select(p => p["idRegion"].ToString()).ToList();
            var listSegment = DBSprSegment.GetSegmentForPlan().AsEnumerable().Select(p => p["idGroup"].ToString()).ToList();

            
            FillDataSalePlan(dtData, dtNaklRasx, listBrend, listRegion, listSegment, result, true);
            if (onlyentered)
                return result;

           var ttemp = dtData.AsEnumerable().Where(p => listBrend.FirstOrDefault(q => q == p["idbrend"].ToString()) == null ||
                                                                   listRegion.FirstOrDefault(q => q == p["idRegion"].ToString()) == null ||
                                                                   listSegment.FirstOrDefault(q => q == p["idSegm"].ToString()) == null);
            if (ttemp.Count() > 0)
            {
                DataTable dtDopData = ttemp.CopyToDataTable();

                var listDopBrend = dtDopData.AsEnumerable().Select(p => p["idbrend"].ToString()).Distinct().ToList();
                var listDopRegion = dtDopData.AsEnumerable().Select(p => p["idRegion"].ToString()).Distinct().ToList();
                var listDopSegment = dtDopData.AsEnumerable().Select(p => p["idSegm"].ToString()).Distinct().ToList();

                FillDataSalePlan(dtDopData, dtNaklRasx, listDopBrend, listDopRegion, listDopSegment, result, false);
            }
           
            result = CalculateVMD(result);
            foreach (var row in result.AsEnumerable())
                row.AcceptChanges();
            return result;          
        }


        

        private static void FillDataSalePlan(DataTable dtData, DataTable dtNaklRasx, List<string> listBrend, List<string> listRegion, List<string> listSegment, DataTable result, bool full)
        {
            foreach (var brand in listBrend)
            {
                var rowNaklRasx = dtNaklRasx.AsEnumerable().FirstOrDefault(p => p["id_tm"].ToString() == brand);
                decimal naklRasx = rowNaklRasx == null ? 0 : decimal.Parse(rowNaklRasx["value"].ToString());

                foreach (var region in listRegion)
                {
                    foreach (var segm in listSegment)
                    {
                        var rows = dtData.AsEnumerable().Where(p => p["idSegm"].ToString() == segm && p["idbrend"].ToString() == brand && p["idRegion"].ToString() == region).ToList();
                        
                       
                        if (!full && rows.Count == 0)
                            continue;

                        var rvProd = result.NewRow();
                        var rvMarg = result.NewRow();

                        rvProd["idRegion"] = region;
                        rvProd["idbrend"] = brand;
                        rvProd["idSegm"] = segm;
                        rvProd["type"] = "Объем продаж";

                        rvMarg["idRegion"] = region;
                        rvMarg["idbrend"] = brand;
                        rvMarg["idSegm"] = segm;
                        rvMarg["type"] = "Маржинальность";
                    
                        if (rows.Count == 0)
                        {
                            rvProd["naklRasx"] = Math.Round(naklRasx * 100, 1);
                        }
                        else
                        {
                            if (rows.First()["naklRasx"] != DBNull.Value)
                            {
                                rvProd["naklRasx"] =
                                    Math.Round(decimal.Parse(rows.First()["naklRasx"].ToString()) * 100, 1);
                            }
                            else
                            {
                                rvProd["naklRasx"] = Math.Round(naklRasx * 100, 1);
                            }
                        }

                        for (int i = 1; i < 13; i++)
                        {
                            var row = rows.FirstOrDefault(p => p["nMonth"].ToString() == i.ToString());
                            if (row == null)
                                rvProd["k" + i.ToString()] = 0;
                            else
                                rvProd["k"+i.ToString()] = row["vProd"];
                        }

                        for (int i = 1; i < 13; i++)
                        {
                            var row = rows.FirstOrDefault(p => p["nMonth"].ToString() == i.ToString());
                            if (row == null)
                                rvMarg["k" + i.ToString()] = 0;
                            else
                                rvMarg["k" + i.ToString()] = Math.Round(Decimal.Parse(row["Marga"].ToString()) * 100, 2);
                        }

                        result.Rows.Add(rvProd);
                        result.Rows.Add(rvMarg);
                    }
                }
            }
        }


        private static DataTable CalculateVMD(DataTable res1)
        {
            res1.Columns.Add("CalculatedVMD");

            var res = (from i in res1.AsEnumerable()
                group i by new {Sigment = i.Field<int>("idSegm"), Brand = i.Field<int>("idbrend")}
                into r
                select r).ToList();

            foreach (var r in res)
            {
                Console.WriteLine(r.Key.Brand);
                double k1 = 1;
                double k2 = 1;
                double k3 = 1;
                double k4 = 1;
                double k5 = 1;
                double k6 = 1;
                double k7 = 1;
                double k8 = 1;
                double k9 = 1;
                double k10 = 1;
                double k11 = 1;
                double k12 = 1;

                foreach (var rr in r)
                {
                    if (rr["type"].ToString() == "Объем продаж")
                    {
                        k1 = Double.Parse(rr["k1"].ToString());
                        k2 = Double.Parse(rr["k2"].ToString());
                        k3 = Double.Parse(rr["k3"].ToString());
                        k4 = Double.Parse(rr["k4"].ToString());
                        k5 = Double.Parse(rr["k5"].ToString());
                        k6 = Double.Parse(rr["k6"].ToString());
                        k7 = Double.Parse(rr["k7"].ToString());
                        k8 = Double.Parse(rr["k8"].ToString());
                        k9 = Double.Parse(rr["k9"].ToString());
                        k10 = Double.Parse(rr["k10"].ToString());
                        k11 = Double.Parse(rr["k11"].ToString());
                        k12 = Double.Parse(rr["k12"].ToString());

                    }
                    else if (rr["type"].ToString() == "Маржинальность")
                    {
                        k1 = k1 == 0 ? 0 : (k1 * Double.Parse(rr["k1"].ToString())) / (100 + Double.Parse(rr["k1"].ToString()));
                        k2 = k2 == 0 ? 0 : (k2 * Double.Parse(rr["k2"].ToString())) / (100 + Double.Parse(rr["k2"].ToString()));
                        k3 = k3 == 0 ? 0 : (k3 * Double.Parse(rr["k3"].ToString())) / (100 + Double.Parse(rr["k3"].ToString()));
                        k4 = k4 == 0 ? 0 : (k4 * Double.Parse(rr["k4"].ToString())) / (100 + Double.Parse(rr["k4"].ToString()));
                        k5 = k5 == 0 ? 0 : (k5 * Double.Parse(rr["k5"].ToString())) / (100 + Double.Parse(rr["k5"].ToString()));
                        k6 = k6 == 0 ? 0 : (k6 * Double.Parse(rr["k6"].ToString())) / (100 + Double.Parse(rr["k6"].ToString()));
                        k7 = k7 == 0 ? 0 : (k7 * Double.Parse(rr["k7"].ToString())) / (100 + Double.Parse(rr["k7"].ToString()));
                        k8 = k8 == 0 ? 0 : (k8 * Double.Parse(rr["k8"].ToString())) / (100 + Double.Parse(rr["k8"].ToString()));
                        k9 = k9 == 0 ? 0 : (k9 * Double.Parse(rr["k9"].ToString())) / (100 + Double.Parse(rr["k9"].ToString()));
                        k10 = k10 == 0 ? 0 : (k10 * Double.Parse(rr["k10"].ToString())) / (100 + Double.Parse(rr["k10"].ToString()));
                        k11 = k11 == 0 ? 0 : (k11 * Double.Parse(rr["k11"].ToString())) / (100 + Double.Parse(rr["k11"].ToString()));
                        k12 = k12 == 0 ? 0 : (k12 * Double.Parse(rr["k12"].ToString())) / (100 + Double.Parse(rr["k12"].ToString()));
                    }


                }

                var vdm = Math.Round(k1 + k2 + k3 + k4 + k5 + k6 + k7 + k8 + k9 + k10 + k11 + k12, 2);

                for (int i = 0; i < res1.Rows.Count; i++)
                {
                    if (res1.Rows[i]["idbrend"].ToString() == r.Key.Brand.ToString() &&
                        res1.Rows[i]["idSegm"].ToString() == r.Key.Sigment.ToString())
                        res1.Rows[i]["CalculatedVMD"] = vdm;

                }
            }

            return res1;
        }

        public static void SaveData(DataRow row, int Year)
        {
            int idBrand = row["idBrend"] == null || row["idBrend"].ToString() == "" ? 0 :  int.Parse(row["idBrend"].ToString());
            int idSegm = row["idSegm"] == null || row["idSegm"].ToString() == "" ? 0 : int.Parse(row["idSegm"].ToString());
            int idRegion = row["idRegion"] == null || row["idRegion"].ToString() == "" ? 0 : int.Parse(row["idRegion"].ToString());
            decimal naklRasx = row["naklRasx"] == null || row["naklRasx"].ToString() == "" ? 0 : decimal.Parse(row["naklRasx"].ToString());

            if ((idBrand == -1)||(idSegm == -1)||(idRegion == -1)) return;

            //decimal marga = row["marga"] == null || row["marga"].ToString() == "" ? 0 : decimal.Parse(row["marga"].ToString());

            for (int i = 1; i < 13; i++)
            {
                if (row["type"].ToString() != "Маржинальность")
                {
                    var oborot = row["k" + i.ToString()] == null || row["k" + i.ToString()].ToString() == "" ? 0 : decimal.Parse(row["k" + i.ToString()].ToString());
                    DBSalePlan.SavePlanOborot(idBrand, idSegm, idRegion, Year, i, oborot, naklRasx / 100);

                }
                else
                {
                    var marga = row["k" + i.ToString()] == null || row["k" + i.ToString()].ToString() == "" ? 0 : decimal.Parse(row["k" + i.ToString()].ToString());
                    DBSalePlan.SavePlanMarga(idBrand, idSegm, idRegion, Year, i, marga);
                }
            }
        }

        public static void DelData(DataRow row, int Year)
        {
            int idBrand = row["idBrend"] == null || row["idBrend"].ToString() == "" ? 0 : int.Parse(row["idBrend"].ToString());
            int idSegm = row["idSegm"] == null || row["idSegm"].ToString() == "" ? 0 : int.Parse(row["idSegm"].ToString());
            int idRegion = row["idRegion"] == null || row["idRegion"].ToString() == "" ? 0 : int.Parse(row["idRegion"].ToString());

            DBSalePlan.DelPlan(idBrand, idSegm, idRegion, Year);

        }





    }
}
