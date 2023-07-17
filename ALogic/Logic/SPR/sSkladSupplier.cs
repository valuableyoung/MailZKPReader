using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR
{
    public class sSkladSupplier
    {
        public int idSkladSupplier { get; set; }
        public int idSupplier { get; set; }
        public string nSkladSupplier { get; set; }
        public int dayDelivery { get; set; }
        public double maxSumOrder { get; set; }

    }


    public static class sSkladSupplierData
    {

        public static sSkladSupplier getDataByIdSkladSupplier(int idSkladSupplier)
        {
            string sql = "select * from sSkladSupplier where idSkladSupplier  = " + idSkladSupplier;
            return sql.ExecuteSingle();
        }

        public static List<sSkladSupplier> getDataBySupplier(int idSupplier)
        {
            string sql = "select * from sSkladSupplier where idSupplier = " + idSupplier;
            var data = sql.ExecuteList();
            data.Insert(0, new sSkladSupplier { nSkladSupplier = "-- Выберите склад поставщика --" });
            return data;
        }

        public static bool InsertOrUpdate(sSkladSupplier sSklad)
        {
            if (sSklad.idSkladSupplier == 0) Insert(sSklad);
            else Update(sSklad);

            return true;

        }

        private static void Update(sSkladSupplier sSklad)
        {
            string sql = $"update sSkladSupplier set nSkladSupplier = '{sSklad.nSkladSupplier}', dayDelivery = {sSklad.dayDelivery}, maxSumOrder = {sSklad.maxSumOrder} where idSkladSupplier = {sSklad.idSkladSupplier}  ";
            sql.Execute();
        }

        private static void Insert(sSkladSupplier sSklad)
        {
            string sql = $"insert into sSkladSupplier(idSupplier, nSkladSupplier, dayDelivery, maxSumOrder) values({sSklad.idSupplier},'{sSklad.nSkladSupplier}',{sSklad.dayDelivery},{sSklad.maxSumOrder})";
            sql.Execute();
            sql = "select top(1) * from sSkladSupplier order by idSkladSupplier desc";
            var res = sql.ExecuteSingle();
            sSklad.idSkladSupplier = res.idSkladSupplier;
        }

        public static bool Remove(int idSkladSupplier)
        {
            string sql = "delete from sSkladSupplier where idSkladSupplier = " + idSkladSupplier;
            return sql.Execute();
        }




    }



    static class UtilExtension
    {
        public static bool Execute(this string sql)
        {
            return DBConnector.DBExecutor.ExecuteQuery(sql);
        }

        public static List<sSkladSupplier> ExecuteList(this string sql)
        {
            List<sSkladSupplier> supplier = new List<sSkladSupplier>();

            var data =  DBConnector.DBExecutor.SelectTable(sql);

            foreach (DataRow item in data.Rows)
            {
                supplier.Add(RowToObject(item));
            }

            return supplier;
        }

        public static sSkladSupplier ExecuteSingle(this string sql)
        {
            var item = DBConnector.DBExecutor.SelectTable(sql).Rows[0];

            return RowToObject(item);
        }

        private static sSkladSupplier RowToObject(DataRow item)
        {
            return new sSkladSupplier { idSkladSupplier = int.Parse(item["idSkladSupplier"].ToString()), idSupplier = int.Parse(item["idSupplier"].ToString()), nSkladSupplier = item["nSkladSupplier"].ToString(), dayDelivery = int.Parse(item["dayDelivery"].ToString()), maxSumOrder = double.Parse(item["maxSumOrder"].ToString()) };
        }

    }

}
