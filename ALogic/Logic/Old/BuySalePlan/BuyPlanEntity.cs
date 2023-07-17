using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ALogic.Logic.Old.BuySalePlan
{
    public class BuyPlanEntity
    {
        DataTable _deletedData;
        DataTable _data;
        DataTable _dataKvartal;

        public DataTable _salerPlanData;
        DataTable _delSalerPlanData;

        public int Year { get; set; }

        public DataTable LoadData(string year)
        {
            Console.WriteLine(year);
            if (year == null || year.ToString() == "")
                return null;

            Year = int.Parse(year.ToString());

            _data = BuyPlanLogic.GetBuyPlan(Year);
            _deletedData = _data.Clone();

            _salerPlanData = BuyPlanLogic.GetSalerPlanData(Year);
            _delSalerPlanData = _salerPlanData.Clone();

            LoadCustomMethod();

            FillSalerPlan();
            return _data;
        }

        private void LoadCustomMethod()
        {
            _dataKvartal = BuyPlanLogic.GetSalerPlanCustomBonus(Year);
        }

        public void FillSalerPlan()
        {
            var listBrend = _data.AsEnumerable().Where(p=>p["idBrend"] != DBNull.Value).Select(p => int.Parse(p["idbrend"].ToString())).Distinct().ToList();
            var listcalcBrend = _salerPlanData.AsEnumerable().Where(p=>p["idTm"] != DBNull.Value).Select(p => int.Parse(p["idTm"].ToString())).Distinct().ToList();
            listBrend = listBrend.Where(p => listcalcBrend.IndexOf(p) >= 0).ToList();
            foreach (var idBrend in listBrend)
            {
                FillSalerPlanBrend(idBrend);
            }
        }

        public void FillSalerPlanBrend(int idBrend)
        {
            var rows = _data.AsEnumerable().Where(p => p["idBrend"].ToString() == idBrend.ToString()).ToList();
            var plans = _salerPlanData.AsEnumerable().Where(p => p["idTm"].ToString() == idBrend.ToString()).ToList();

            var summKv1 = rows.Select(p => decimal.Parse(p["k1"].ToString()) + decimal.Parse(p["k2"].ToString()) + decimal.Parse(p["k3"].ToString())).Sum();
            var summKv2 = rows.Select(p => decimal.Parse(p["k4"].ToString()) + decimal.Parse(p["k5"].ToString()) + decimal.Parse(p["k6"].ToString())).Sum();
            var summKv3 = rows.Select(p => decimal.Parse(p["k7"].ToString()) + decimal.Parse(p["k8"].ToString()) + decimal.Parse(p["k9"].ToString())).Sum();
            var summKv4 = rows.Select(p => decimal.Parse(p["k10"].ToString()) + decimal.Parse(p["k11"].ToString()) + decimal.Parse(p["k12"].ToString())).Sum();

            var sumGod = summKv1 + summKv2 + summKv3 + summKv4;

            var lperckv1 = plans.Where(p => p["nomKv"].ToString() == "1" && decimal.Parse(p["vProd"].ToString()) < summKv1).Select(q => decimal.Parse(q["Perc"].ToString())).ToList();
            var lperckv2 = plans.Where(p => p["nomKv"].ToString() == "2" && decimal.Parse(p["vProd"].ToString()) < summKv2).Select(q => decimal.Parse(q["Perc"].ToString())).ToList();
            var lperckv3 = plans.Where(p => p["nomKv"].ToString() == "3" && decimal.Parse(p["vProd"].ToString()) < summKv3).Select(q => decimal.Parse(q["Perc"].ToString())).ToList();
            var lperckv4 = plans.Where(p => p["nomKv"].ToString() == "4" && decimal.Parse(p["vProd"].ToString()) < summKv4).Select(q => decimal.Parse(q["Perc"].ToString())).ToList();
            
            var lpercGod = plans.Where(p => p["nomKv"].ToString() == "0" && decimal.Parse(p["vProd"].ToString()) < sumGod).Select(q => decimal.Parse(q["Perc"].ToString())).ToList();

            var perckv1 = lperckv1.Count == 0 ? 0 : lperckv1.Max();
            var perckv2 = lperckv2.Count == 0 ? 0 : lperckv2.Max();
            var perckv3 = lperckv3.Count == 0 ? 0 : lperckv3.Max();
            var perckv4 = lperckv4.Count == 0 ? 0 : lperckv4.Max();

            var percGod = lpercGod.Count == 0 ? 0 : lpercGod.Max();

            var custombonus = (from  DataRow i in _dataKvartal.AsEnumerable() where i.Field<int>("idTm") == idBrend select i).ToList();

            if (custombonus.Count> 0)
            {

                foreach (var row in rows)
                {
                    row["p1"] = (from i in custombonus where i.Field<int>("nomKv") == 1 select i["bonus"]).FirstOrDefault()??0;
                    row["p2"] = (from i in custombonus where i.Field<int>("nomKv") == 2 select i["bonus"]).FirstOrDefault()??0;
                    row["p3"] = (from i in custombonus where i.Field<int>("nomKv") == 3 select i["bonus"]).FirstOrDefault()??0;
                    row["p4"] = (from i in custombonus where i.Field<int>("nomKv") == 4 select i["bonus"]).FirstOrDefault()??0;

                    row["pf"] = ((from i in custombonus where i.Field<int>("nomKv") == 0 select i["bonus"]).FirstOrDefault())??0;
                }

            }
            else
            {
                foreach (var row in rows)
                {
                    row["p1"] = perckv1* summKv1/100;
                    row["p2"] = perckv2 * summKv2 / 100;
                    row["p3"] = perckv3 * summKv3 / 100;
                    row["p4"] = perckv4 * summKv4 / 100;

                    row["pf"] = percGod* sumGod / 100;
                }
            }
        }

        public  void insertNewCustomBonus(int idTm, int nomKv, decimal bonus)
        {
            _dataKvartal = BuyPlanLogic.InsertSalerPlanCustomBonus(idTm, nomKv, bonus, Year);
            //_dataKvartal.Rows.Remove()
            //DataRow row = _dataKvartal.NewRow();
            //row["idTm"] = idTm;
            //row["bonus"] = bonus;
            //row["nomKv"] = nomKv;
            //row["Year"] = Year;
            //_dataKvartal.Rows.Add(row);
            

            //Console.WriteLine(idTm + " "+nomKv+ " "+bonus);
        }

        public void SaveData()
        {
            var newData = _data.AsEnumerable().Where(p => p.RowState == DataRowState.Added || p.RowState == DataRowState.Modified).ToList();
            
            foreach (var row in newData)
            {

                BuyPlanLogic.SaveData(row, Year);
            }

            foreach (var row in _deletedData.AsEnumerable())
            {
                BuyPlanLogic.DelData(row, Year);
            }

            var newDataPlan = _salerPlanData.AsEnumerable().Where(p => p.RowState == DataRowState.Added || p.RowState == DataRowState.Modified).ToList();
            foreach (var row in newDataPlan)
            {
               BuyPlanLogic.SaveDataPlan(row);
            }

            foreach (var row in _delSalerPlanData.AsEnumerable())
            {
                BuyPlanLogic.DelDataPlan(row);
            }

        }

        public void DelData(DataRow dataRow)
        {
            _deletedData.Rows.Add(dataRow.ItemArray);
        }

        public void FillNewRow(DataRow dataRow)
        {
            dataRow["idBrend"] = 0;
            dataRow["idSegm"] = 0;
            dataRow["marga"] = 0;
            for (int i = 1; i < 13; i++)
                dataRow["k" + i.ToString()] = 0;
            for (int i = 1; i < 4; i++)
                dataRow["p" + i.ToString()] = 0;
        }

        public DataTable GetPlan()
        {
            return _salerPlanData;
        }       

        public void DelDataPlan(DataRow dataRow)
        {
            _delSalerPlanData.Rows.Add(dataRow.ItemArray);
        }

        public DataTable GetPlanCurren(int idtm)
        {
            return BuyPlanLogic.GetSalerPlanDataByTm(Year, idtm);
        }

        public bool CheckForPlan(int idBrend)
        {
            if (_salerPlanData.AsEnumerable().Where(p => int.Parse(p["idTm"].ToString()) == idBrend).ToList().Count > 0)
                return true;

            AddDefaultPlan(idBrend);
            return false;
        }


        private void AddDefaultPlan(int idTm)
        {
            DataRow row = _salerPlanData.NewRow();
            row[0] = idTm;
            row[1] = 0;
            row[2] = 0;
            row[3] = 0;
            row[4] = Year;
            _salerPlanData.Rows.Add(row);

            row = _salerPlanData.NewRow();
            row[0] = idTm;
            row[1] = 0;
            row[2] = 0;
            row[3] = 1;
            row[4] = Year;
            _salerPlanData.Rows.Add(row);

            row = _salerPlanData.NewRow();
            row[0] = idTm;
            row[1] = 0;
            row[2] = 0;
            row[3] = 2;
            row[4] = Year;
            _salerPlanData.Rows.Add(row);

            row = _salerPlanData.NewRow();
            row[0] = idTm;
            row[1] = 0;
            row[2] = 0;
            row[3] = 3;
            row[4] = Year;
            _salerPlanData.Rows.Add(row);

            row = _salerPlanData.NewRow();
            row[0] = idTm;
            row[1] = 0;
            row[2] = 0;
            row[3] = 4;
            row[4] = Year;
            _salerPlanData.Rows.Add(row);

        }

        public void ClearCustomBonus(int idTm)
        {
            BuyPlanLogic.ClearCustomBonus(idTm, Year);
            LoadCustomMethod();
        }
    }
}
