using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{

    public class Kontr
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Kontr(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }


    public static class DBSprSupplier
    {
        public static DataTable GetSuppliers()
        {
            string query = "select " + "\n";
            query += "    spr_kontr.id_kontr, " + "\n";
            query += "    spr_kontr.n_kontr" + "\n";
            query += "from spr_kontr " + "\n";
            query += "where supplier = 1 and id_cond <> 30" + "\n";
            query += "order by spr_kontr.n_kontr" + "\n";
            return DBExecutor.SelectTable(query);
        }


        public static DataTable GetSuppliersTitle()
        {
            //string query = "select idKontrTitle, nKontrTitle from sKontrTitle";
            string query = @"select idKontrTitle, nKontrTitle from sKontrTitle " +
                "where (select count(*) from rKontrTitleKontr where idKontrTitle = sKontrTitle.idkontrtitle and fActual = 1) > 0";
            return DBExecutor.SelectTable(query);
        }


        public static List<Kontr> GetSuppliersList()
        {
            List<Kontr> listKontr = new List<Kontr>();
            listKontr.Add(new Kontr(0, "-- Выберите поставщика --"));

            foreach (DataRow item in GetSuppliersTitle().Rows)
            {
                listKontr.Add(new Kontr(int.Parse(item["idKontrTitle"].ToString()), item["nKontrTitle"].ToString()));
            }
            return listKontr.OrderBy(p=>p.Name).ToList();
        }


    }
}
