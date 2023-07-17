using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ALogic.DBConnector;

namespace ALogic.Logic.Old.BuySalePlan
{
    public static class DBBuyPlan
    {
        public static DataTable GetPlanData(int NomYear)
        {
            string sql =
@"
    SELECT 
       NomYear
      ,NomMonth
      ,NomWeek
      ,idBrand
      ,idGroupSegment
      ,SumBuy
      ,RestMonthRub
      ,RestWeekRub
      ,Marginality
      ,idCur
  FROM PlanBuy
  where  NomYear = @NomYear
";
            SqlParameter NomYearPar = new SqlParameter("NomYear", NomYear);
            return DBExecutor.SelectTable(sql, NomYearPar);
        }

        public static DataTable GetNaklRasx()
        {
            string sql =
@"
    select 1 as id_tm, 1 as value 
";
            return DBExecutor.SelectTable(sql);
        }

        public static DataTable GetSalerPlan(int NomYear)
        {
            string sql =
@"
    SELECT      
       idTm
      ,vProd
      ,Perc
      ,nomKv
      ,nomYear
  FROM BrandSellerPlan
  where  NomYear = @NomYear
";
            SqlParameter NomYearPar = new SqlParameter("NomYear", NomYear);
            return DBExecutor.SelectTable(sql, NomYearPar);
        }

        public static DataTable GetSalerPlanByTm(int NomYear, int idtm)
        {
            string sql =
@"
    SELECT      
       idTm
      ,vProd
      ,Perc
      ,nomKv
  FROM BrandSellerPlan
  where  NomYear = @NomYear and idTm = @idtm
";
            SqlParameter NomYearPar = new SqlParameter("NomYear", NomYear);
            SqlParameter idTmp = new SqlParameter("idTm", idtm);
            return DBExecutor.SelectTable(sql, NomYearPar, idTmp);
        }


        public static void SavePlan(int idBrand, int idSegment, int idCur, int nomYear, int nomMonth, decimal SumBuy, decimal marga)
        {
            string sql = @"
                            if exists (select * from PlanBuy where idBrand = @idBrand and idGroupSegment = @idSegment and NomYear = @nomYear and NomMonth = @nomMonth )
	                            update PlanBuy
	                            set  SumBuy = @SumBuy
		                            ,Marginality = @Marga
                                    ,idCur = @idCur
	                            where idBrand = @idBrand and idGroupSegment = @idSegment and NomYear = @nomYear and NomMonth = @nomMonth 
	                        else
	                            insert into PlanBuy(NomYear,NomMonth,NomWeek,idBrand,idGroupSegment,SumBuy,Marginality,idCur)
                                values(@nomYear, @nomMonth, 0, @idBrand,  @idSegment, @SumBuy,@Marga, @idCur)
                        ";

            SqlParameter idBrendPar = new SqlParameter("idBrand", idBrand);
            SqlParameter idSegmentPar = new SqlParameter("idSegment", idSegment);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);
            SqlParameter nomMonthPar = new SqlParameter("nomMonth", nomMonth);
            SqlParameter oborotPar = new SqlParameter("SumBuy", SumBuy);
            SqlParameter margaPar = new SqlParameter("marga", marga);
            SqlParameter idRegionPar = new SqlParameter("idCur", idCur);

            DBExecutor.ExecuteQuery(sql, idBrendPar, idSegmentPar, nomYearPar, nomMonthPar, oborotPar, margaPar, idRegionPar);
        }

        public static DataTable GetSalerPlanCustomBonus(int year)
        {
            string sql = "select * from BrandSellerPlanCustomBonus where year = @year";
            SqlParameter yearp = new SqlParameter("@year", year);

            return DBExecutor.SelectTable(sql, yearp);
        }

        public static void DelPlan(int idBrand, int idSegment, int nomYear)
        {
            string sql = @"delete from PlanBuy where idBrand = @idBrand and idGroupSegmen = @idSegment and NomYear = @nomYear";
            SqlParameter idBrendPar = new SqlParameter("idBrand", idBrand);
            SqlParameter idSegmentPar = new SqlParameter("idSegment", idSegment);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);
         
            DBExecutor.ExecuteQuery(sql, idBrendPar, idSegmentPar, nomYearPar);
        }
       
        public static void SaveSalerPlan(object idTm, object vProd, object Perc, object nomKv, object nomYear)
        {
            string sql = @"
                            if exists (select * from BrandSellerPlan where idTm = @idTm and vProd = @vProd and nomYear = @nomYear and nomKv = @nomKv )
	                            update BrandSellerPlan
	                            set  Perc = @Perc
		                        where idTm = @idTm and vProd = @vProd and nomYear = @nomYear and nomKv = @nomKv
	                        else
	                            insert into BrandSellerPlan(idTm,vProd,Perc,nomKv,nomYear)
                                values(@idTm,@vProd,@Perc,@nomKv,@nomYear)
                        ";

            SqlParameter idTmPar = new SqlParameter("idTm", idTm);
            SqlParameter vProdPar = new SqlParameter("vProd", vProd);
            SqlParameter PercPar = new SqlParameter("Perc", Perc);
            SqlParameter nomKvPar = new SqlParameter("nomKv", nomKv);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);

            DBExecutor.ExecuteQuery(sql, idTmPar, vProdPar, PercPar, nomKvPar, nomYearPar);
        }

        public static void DeleteSalerPlan(object idTm, object vProd, object nomKv, object nomYear)
        {
            string sql = @"
                            delete from BrandSellerPlan
	                        where idTm = @idTm and vProd = @vProd and nomYear = @nomYear and nomKv = @nomKv
                        ";

            SqlParameter idTmPar = new SqlParameter("idTm", idTm);
            SqlParameter vProdPar = new SqlParameter("vProd", vProd);
            SqlParameter nomKvPar = new SqlParameter("nomKv", nomKv);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);

            DBExecutor.ExecuteQuery(sql, idTmPar, vProdPar, nomKvPar, nomYearPar);
        }

        public static DataTable InsertSalerPlanCustomBonus(int idTm, int nomKv, decimal bonus, int year)
        {
            SqlParameter idTmPar = new SqlParameter("idTm", idTm);
            SqlParameter bonusPar = new SqlParameter("bonus", bonus);
            SqlParameter nomKvPar = new SqlParameter("nomKv", nomKv);
            SqlParameter nomYearPar = new SqlParameter("year", year);
            string sql = @"
                            delete from BrandSellerPlanCustomBonus where idTm=@idTm and nomKv = @nomKv and year = @year;
                            insert into BrandSellerPlanCustomBonus values(@idTm, @nomKv, @bonus, @year);";
            DBExecutor.ExecuteQuery(sql, idTmPar, nomKvPar, nomYearPar, bonusPar);

            return GetSalerPlanCustomBonus(year);
        }

        public static void ClearCustomBonus(int idTm, int year)
        {
            SqlParameter idTmPar = new SqlParameter("idTm", idTm);
            SqlParameter nomYearPar = new SqlParameter("year", year);
            string sql = @"
                            delete from BrandSellerPlanCustomBonus where idTm=@idTm and year = @year";
            DBExecutor.ExecuteQuery(sql, idTmPar, nomYearPar);
        }
    }
}
