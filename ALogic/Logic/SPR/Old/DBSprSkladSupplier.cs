using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{

    public class SkladSupplier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SkladSupplier(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }


    public static class DBSprSkladSupplier
    {
        public static DataTable GetSkladSuppliersByIdKontr(int idSupplier)
        {
            //SqlParame
            string query = "select " + "\n";
            query += "    idSkladSupplier,   nSkladSupplier from sSkladSupplier (nolock) where idsupplier = " + idSupplier + " order by idSkladSupplier";
            return DBExecutor.SelectTable(query);
        }

        public static DataTable GetSkladSuppliers()
        {
            string query = "select " + "\n";
            query += "    idSkladSupplier, " + "\n";
            query += "    nSkladSupplier" + "\n";
            query += "from sSkladSupplier (nolock) order by idSkladSupplier" ;
            return DBExecutor.SelectTable(query);
        }

       
        public static List<SkladSupplier> GetSkladSuppliersList(int idSupplier)
        {
            List<SkladSupplier> SkladSupplier = new List<SkladSupplier>();
            SkladSupplier.Add(new SkladSupplier(0, "-- Выберите склад поставщика --"));

            if (idSupplier > 0)
            {
                foreach (DataRow item in GetSkladSuppliersByIdKontr(idSupplier).Rows)
                {
                    SkladSupplier.Add(new SkladSupplier(int.Parse(item["idSkladSupplier"].ToString()), item["nSkladSupplier"].ToString()));
                }
            }
            else
            {
                foreach (DataRow item in GetSkladSuppliers().Rows)
                {
                    SkladSupplier.Add(new SkladSupplier(int.Parse(item["idSkladSupplier"].ToString()), item["nSkladSupplier"].ToString()));
                }
            }

            return SkladSupplier.OrderBy(p => p.Name).ToList();
        }

    }
}

