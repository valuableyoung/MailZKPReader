using ALogic.DBConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{

    public class BrandComparer : IEqualityComparer<Brand>
    {
        public bool Equals(Brand x, Brand y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Brand obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Brand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            Brand b = obj as Brand;

            if (b != null && b.Id == Id && b.Name == Name) return true;

            return false;
        }

    }

    public static class DBSprBrend
    {
        static DataTable listOfBrands = GetBrends();
        public static DataTable GetBrends()
        {
            string query = "select tm_id, tm_name from spr_tm (nolock) where idStatus<>60 and spr_tm.fDefault = 1 order by tm_name";
            return DBExecutor.SelectTable(query);
        }


        public static DataTable GetBrendsBonus()
        {
            string query = @"select * from spr_tm (nolock) where tm_id in (
                                select idTm from rKontrTitleTm (nolock)
                            )";
            return DBExecutor.SelectTable(query);
        }

        public static List<Brand> GetBrandsById(int idSupplier)
        {

            List<Brand> brandList = new List<Brand>();


            string query = @"select tm_id, tm_name from spr_tm (nolock) where idStatus<>60 and tm_id in (
	                            select ktm.idtm from rKontrTitleTm ktm (nolock)
                                inner join rKontrTitleKontr ktk (nolock) on ktk.idKontrTitle = ktm.idKontrTitle  and ktk.fActual = 1
                                where ktk.idKontr = " + idSupplier + @"
                            )
                            order by tm_name"
;

            foreach (DataRow item in DBExecutor.SelectTable(query).Rows)
            {
                brandList.Add(new Brand(int.Parse(item["tm_id"].ToString()), item["tm_name"].ToString()));
            }

            return brandList;
        }

        public static DataTable GetBrendsByIdKontr(int idSupplier)
        {
            string query = @"select tm_id, tm_name 
                            from spr_tm (nolock) 
                            where idStatus<>60
                            and tm_id in (select idTm from rKontrTitleTm (nolock) where idKontrTitle = " + idSupplier + @"
                            )
                            order by tm_name";

            return DBExecutor.SelectTable(query);
        }

        public static List<Brand> GetBrandsList(int idSupplier)
        {
            List<Brand> brandList = new List<Brand>();
            brandList.Add(new Brand(0, "-- Выберите бренд --"));

            if (idSupplier > 0)
            {
                foreach (DataRow item in GetBrendsByIdKontr(idSupplier).Rows)
                {
                    brandList.Add(new Brand(int.Parse(item["tm_id"].ToString()), item["tm_name"].ToString()));
                }
            }
            else
            {
                foreach (DataRow item in GetBrends().Rows)
                {
                    brandList.Add(new Brand(int.Parse(item["tm_id"].ToString()), item["tm_name"].ToString()));
                }
            }



            return brandList.OrderBy(p=>p.Name).ToList();
        }

        public static DataTable GetBrendsForPlan()
        {
            string query = "select tm_id, tm_name from spr_tm (nolock) where fDefault = 1 order by tm_name";
            var result = DBExecutor.SelectTable(query);
            result.Rows.Add(0, "Прочее");
            return result;
        }

        public static int getBrandIDByName(string Brandname)
        {
            foreach (DataRow item in listOfBrands.Rows)
            {
                if (Brandname != "" && item[1].ToString().ToLower().Contains(Brandname.ToLower())) return (int)item[0];
            }

            return -1;
        }

    }
}
