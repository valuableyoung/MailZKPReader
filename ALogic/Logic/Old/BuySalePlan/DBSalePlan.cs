using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ALogic.DBConnector;

namespace ALogic.Logic.Old.BuySalePlan
{
    public static class DBSalePlan
    {
        public static DataTable GetPlanData(int NomYear )
        {
            string sql = @"
                            select 
	                             ps.idBrand as idbrend	                          
	                            ,ps.idGroupSegmen as idSegm	                           
	                            ,Marginality as Marga
                                ,NomYear as nYear
	                            ,NomMonth as nMonth
	                            ,Oborot as vProd
                                ,idRegion as idRegion	
                                ,naklRasx as naklRasx

                            from PlanSaleSegment ps		                          
                            where NomMonth > 0 and NomYear = @NomYear
                           ";
            SqlParameter NomYearPar = new SqlParameter( "NomYear", NomYear);
            return DBExecutor.SelectTable(sql, NomYearPar);
        }

        public static DataTable GetNaklRasx()
        {
            string sql = @"
                            select 
		                            id_tm, AVG(tov_doc.ab_cost) as value
                            from 
	                            tov_doc (nolock)
		                            inner join tov (nolock) on tov_doc.id_doc = tov.id_doc
		                            inner join spr_tov (nolock) on spr_tov.id_tov = tov.id_tov
		
                            where date_doc between DATEADD(YY, -1, GETDATE()) and GETDATE()
		                            and id_type_doc in (8, 19) and id_status_doc in (20, 30) and in_tax in (0, 11)
		                            and spr_tov.id_tm in (select tm_id from spr_tm where fInPlan = 1)
                            group by id_tm
                        ";
            return DBExecutor.SelectTable(sql);
        }

        public static DataTable GetSalePlanByData(int idBrend, int idSegment, int nomYear)
        {
            string sql = @"
                          select   Oborot
	                              ,Profit
	                              ,Marginality
                                  ,NomMonth
                          from PlanSaleSegment
	                      where idBrand = @idBrend and idGroupSegmen = @idSegment and NomYear = @nomYear
                         ";
            SqlParameter idBrendPar = new SqlParameter("idBrend", idBrend);
            SqlParameter idSegmentPar = new SqlParameter("idSegment", idSegment);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);

            return DBExecutor.SelectTable(sql, idBrendPar, idSegmentPar, nomYearPar);
        }

        public static void SavePlanOborot(int idBrand, int idSegment, int idRegion, int nomYear, int nomMonth, decimal oborot, decimal naklRasx, decimal profit = 0)
        {
            string sql = @"
                            if exists (select * from PlanSaleSegment where idBrand = @idBrand and idGroupSegmen = @idSegment and NomYear = @nomYear and NomMonth = @nomMonth and idRegion = @idRegion)
	                            update PlanSaleSegment
	                            set  Oborot = @oborot		                   
                                    ,naklRasx = @naklRasx
                                    ,Profit = @Profit
	                            where idBrand = @idBrand and idGroupSegmen = @idSegment and NomYear = @nomYear and NomMonth = @nomMonth and idRegion = @idRegion
	                        else
	                            insert into PlanSaleSegment(NomYear,NomWeek,Oborot,Profit,idBrand,idTypeProduct,idGroupSegmen,idSeason,NomMonth,RatioToSalesMonth, idRegion, naklRasx)
	                            values(@nomYear, 0, @oborot, @Profit, @idBrand, 0,  @idSegment,  0, @nomMonth, 0, @idRegion, @naklRasx)
                        ";

            SqlParameter idBrendPar = new SqlParameter("idBrand", idBrand);
            SqlParameter idSegmentPar = new SqlParameter("idSegment", idSegment);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);
            SqlParameter nomMonthPar = new SqlParameter("nomMonth", nomMonth);
            SqlParameter oborotPar = new SqlParameter("oborot", oborot);
            SqlParameter idRegionPar = new SqlParameter("idRegion", idRegion);
            SqlParameter naklRasxpar = new SqlParameter("naklRasx", naklRasx);
            SqlParameter profitpar = new SqlParameter("Profit", profit);

            DBExecutor.ExecuteQuery(sql, idBrendPar, idSegmentPar, nomYearPar, nomMonthPar, oborotPar, idRegionPar, naklRasxpar, profitpar);
        }


        public static void SavePlanMarga(int idBrand, int idSegment, int idRegion, int nomYear, int nomMonth, decimal marga, decimal profit = 0)
        {
            string sql = @"
                              if exists (select * from PlanSaleSegment where idBrand = @idBrand and idGroupSegmen = @idSegment and NomYear = @nomYear and NomMonth = @nomMonth and idRegion = @idRegion)
	                            update PlanSaleSegment
	                            set  Marginality = @marga		                   
                                    ,Profit = @Profit
	                            where idBrand = @idBrand and idGroupSegmen = @idSegment and NomYear = @nomYear and NomMonth = @nomMonth and idRegion = @idRegion
	                        else
	                            insert into PlanSaleSegment(NomYear,NomWeek,Profit,Marginality,idBrand,idTypeProduct,idGroupSegmen,idSeason,NomMonth,RatioToSalesMonth, idRegion)
	                            values(@nomYear, 0, @Profit, @Marga, @idBrand, 0,  @idSegment,  0, @nomMonth, 0, @idRegion)
                        ";

            SqlParameter idBrendPar = new SqlParameter("idBrand", idBrand);
            SqlParameter idSegmentPar = new SqlParameter("idSegment", idSegment);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);
            SqlParameter nomMonthPar = new SqlParameter("nomMonth", nomMonth);
            SqlParameter Marginality = new SqlParameter("marga", marga/100);
            SqlParameter idRegionPar = new SqlParameter("idRegion", idRegion);
            SqlParameter profitpar = new SqlParameter("Profit", profit);


            DBExecutor.ExecuteQuery(sql, idBrendPar, idSegmentPar, nomYearPar, nomMonthPar, Marginality, idRegionPar, profitpar);
        }

        public static void DelPlan(int idBrand, int idSegment, int idRegion, int nomYear)
        {
            string sql = @"delete from PlanSaleSegment where idBrand = @idBrand and idGroupSegmen = @idSegment and NomYear = @nomYear and idRegion = @idRegion";
            SqlParameter idBrendPar = new SqlParameter("idBrand", idBrand);
            SqlParameter idSegmentPar = new SqlParameter("idSegment", idSegment);
            SqlParameter nomYearPar = new SqlParameter("nomYear", nomYear);        
            SqlParameter idRegionPar = new SqlParameter("idRegion", idRegion);

            DBExecutor.ExecuteQuery(sql, idBrendPar, idSegmentPar, nomYearPar, idRegionPar);
        }

        public static DataTable GetPlanDataTest()
        {
            string sql = @"
                             select 
	                             t.tm_name as Brend	                          
	                            ,LTRIM(RTRIM(cast(v.nGroup as varchar(100)))) as Segment	                           
                                ,NomYear as Year
	                            ,NomMonth as Month
	                            ,Oborot as vProd
                                ,p.n_position as Region  
                                ,'План' as TypeV                       

                            from PlanSaleSegment ps		
                                    inner join spr_tm t on t.tm_id = ps.idBrand
                                    inner join v_rNameGroupForSegm v on v.idGroup = ps.idGroupSegmen
                                    left join spr_position p on p.id_position = ps.idRegion                          
                            where NomMonth > 0  and NomYear = 2018 

                            union all
                            
                                select  
                                         cast(spr_tm.tm_name as varchar(100))  as Brend	 
                                        ,LTRIM(RTRIM(cast(v_rNameGroupSegm.ngroup as varchar(100)))) as Segment	  
                                        ,year(v_sales.date_doc)  as Year
                                        ,month(v_sales.date_doc) as Month
                                        ,isNull(Sum(v_sales.sum_rub), 0) / 1000  as vProd
                                        ,'Воронежская область' as Region  
                                        ,'Факт' as TypeV   
                                from v_Sales
                                         Inner Join spr_tov as tov (nolock) On tov.id_tov = v_sales.id_tov
                                         Inner Join spr_tm(nolock) On v_sales.id_tm = spr_tm.tm_id and spr_tm.fDefault=1
                                         Left Outer Join v_rNameGroupSegm (nolock) On v_sales.id_kontr = v_rNameGroupSegm.idkontr
                                         inner join spr_kontr (nolock) on spr_kontr.id_kontr = v_Sales.id_kontr

                                where		v_sales.date_doc between '01.01.18' and '01.01.19'
                                        and v_sales.type_doc <> 84 and v_sales.id_direct = 60
                                        and (spr_tm.fInPlan = 1 )

                                group by  spr_tm.tm_name
                                        ,v_rNameGroupSegm.ngroup
                                        ,year(v_sales.date_doc) 
                                        ,month(v_sales.date_doc) 

                                having Sum(v_sales.kol_tov) <> 0

                           ";
            
            return DBExecutor.SelectTable(sql);
        }      

        public static DataTable GetPlanDataTest2()
        {
            string sql = @" 
                                select id_kontr as Brend, id_cur as Segment, MONTH(date_doc) as Month, summa as vProd, id_type_doc as Region, idTerritory as  TypeV 
                                from v_sales where date_doc between '01.01.18' and '12.01.18'";

            return DBExecutor.SelectTable(sql);
        }

       
    }
}
