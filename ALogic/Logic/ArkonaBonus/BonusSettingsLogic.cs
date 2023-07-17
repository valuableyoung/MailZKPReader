using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.ArkonaBonus
{
    public class BonusSettingsLogic
    {

        public static DataTable GetTovTable(int idKontrTitle = 0, int idTm = 0)
        {
            string sql = @"
                            select t.id_tov, t.id_tov_oem, t.OrderN, t.n_tov, tm.tm_name, kp.price, kp.pricetm, p.sebest, 
                                        md.PriceARetail,
                                        md.PriceARetail - md.PriceARetail*b.retailBonus21 as roz21, 
                                        md.PriceARetail - md.PriceARetail*b.retailBonus22 as roz22, 
                                        md.PriceARetail - md.PriceARetail*b.retailBonus23 as roz23, 
                                        md.PriceARetail - md.PriceARetail*b.retailASKUBonus21 as rozASKU21, 
                                        md.PriceARetail - md.PriceARetail*b.retailASKUBonus22 as rozASKU22, 
                                        md.PriceARetail - md.PriceARetail*b.retailASKUBonus23 as rozASKU23, 
                                        md.PriceAOpt - md.PriceAOpt*b.optASKUBonus as optASKU, 
                                        
                                        md.PriceARetail - md.PriceARetail*b.retailBonus20 as roz0,
                                        md.PriceAOpt - md.PriceAOpt*b.opt1Bonus as newopt1, 
                                        md.PriceAOpt - md.PriceAOpt*b.opt2Bonus as newopt2, 
                                        md.PriceARetail - md.PriceARetail*b.retailASKUBonus20 as rozASKU0,
                                        md.PriceAOpt - md.PriceAOpt*b.opt1ASKUBonus as newopt1ASKU, 
                                        md.PriceAOpt - md.PriceAOpt*b.opt2ASKUBonus as newopt2ASKU,
                                        p.MinSebestRetail,
                                        md.PriceAOpt - md.PriceAOpt*b.optBonus as Opt1, 
                                        p.MinSebestOpt,
                                        md.PriceAOpt,
                                        case when t.idAdvancement = 3 then 1 else 0 end as fR                                       

                                        from spr_price p
		                                        inner join spr_tov t on t.id_tov = p.id_tov
		                                        inner join spr_tm tm on tm.tm_id = t.id_tm
		                                        inner join rKontrTitleTm tkm on tkm.idTm = t.id_tm and (t.id_tm = @idTm or @idTm = 0)
		                                        inner join rKontrTitleKontr tk on tk.idKontrTitle= tkm.idKontrTitle and (tk.idKontrTitle = @idKontrTitle or @idKontrTitle = 0) and fActual = 1
		                                        left join kontr_tov_price kp on kp.id_tov = p.id_tov and kp.id_kontr = tk.idKontr
		                                        left join sArkonaBonus b on b.idBrand = tkm.idTm
		                                        left join spriceModele md on md.idTov = t.id_tov
		                                        where
		                                        p.id_direct = 60 and 
		                                        t.id_top_level not in (2701663, 2711517) and
		                                        ISNULL(p.sebest,0) <> 0
";

            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);

            var tbl = DBExecutor.SelectTable(sql, ParamidKontrTitle, ParamidTm);

            tbl.Columns.Add("StError", typeof(bool));

            foreach (DataRow item in tbl.Rows)
            {
                item["StError"] = item["roz21"] == DBNull.Value || item["MinSebestRetail"] == DBNull.Value || (decimal)item["roz21"] < (decimal)item["MinSebestRetail"] || item["roz22"] == DBNull.Value || item["MinSebestRetail"] == DBNull.Value || (decimal)item["roz22"] < (decimal)item["MinSebestRetail"] || item["roz23"] == DBNull.Value || item["MinSebestRetail"] == DBNull.Value || (decimal)item["roz23"] < (decimal)item["MinSebestRetail"] || item["Opt1"] == DBNull.Value || (decimal)item["Opt1"] < (decimal)item["MinSebestOpt"];
            }


            return tbl;

        }

        public static DataTable GetArkonaBonus(int idKontrTitle = 0, int idTm = 0)
        {
          

            string sql = @"                                    
                select  tm.tm_id, 
                        tm.tm_name, 
                        cast(s.retailBonus21*100 as decimal(18,2)) as P1, 
                        cast(s.retailBonus22*100 as decimal(18,2)) as P2, 
                        cast(s.retailBonus23*100 as decimal(18,2)) as P3, 
                        cast(s.retailMarkup*100 as decimal(18,2)) as retailMarkup,
                        cast(s.optMarkup*100 as decimal(18,2)) as optMarkup,
                        cast(s.optBonus*100 as decimal(18,2)) as optBonus,
        
                        cast(s.optASKUBonus*100 as decimal(18,2)) as optASKUBonus,
                        cast(s.retailASKUBonus21*100 as decimal(18,2)) as retailASKUBonus21,
                        cast(s.retailASKUBonus22*100 as decimal(18,2)) as retailASKUBonus22,
                        cast(s.retailASKUBonus23*100 as decimal(18,2)) as retailASKUBonus23,
                        cast(s.retailBonus20*100 as decimal(18,2)) as P0,        
                        cast(s.opt1Bonus*100 as decimal(18,2)) as O1,
                        cast(s.opt2Bonus*100 as decimal(18,2)) as O2,
                        cast(s.retailASKUBonus20*100 as decimal(18,2)) as P0ASKU,        
                        cast(s.opt1ASKUBonus*100 as decimal(18,2)) as O1ASKU,
                        cast(s.opt2ASKUBonus*100 as decimal(18,2)) as O2ASKU
                from sArkonaBonus s
                    join spr_tm tm on tm.tm_id = s.idBrand
                    join rKontrTitleTm rt on rt.idTm = tm.tm_id
                                 
                where 
                (rt.idKontrTitle = @idKontrTitle or @idKontrTitle = 0) and
                (rt.idTm = @idTm or @idTm = 0)
                ";
            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);

            return DBExecutor.SelectTable(sql, ParamidKontrTitle, ParamidTm);
        }

        public static void SaveAsku(DataRow row)
        {
            if (row.RowState == DataRowState.Deleted)
            {
                SqlParameter paridBrand = new SqlParameter("id_tov", row["id_tov", DataRowVersion.Original]);
                string sql = "update spr_price set fMinPercASKU = 0 where id_tov = @id_tov and id_direct = 60";
                DBExecutor.ExecuteQuery(sql, paridBrand);
            }
            else
            {
                SqlParameter id_tov = new SqlParameter("id_tov", row["id_tov"]);
                string sql = "update spr_price set fMinPercASKU = 1 where id_tov = @id_tov and id_direct = 60";
                DBExecutor.ExecuteQuery(sql, id_tov);
            }
        }

        public static DataTable getNotTovAsku(int idKontrTitle, int idTm)
        {
            string sql = @"
	select t.id_tov as Id, t.n_tov as Name,t.id_tov_oem, 0 as fCheck 
from spr_price p
	join spr_tov t on t.id_tov = p.id_tov
	join rKontrTitleTm rt on rt.idTm = t.id_tm
	where fMinPercASKU = 0 and p.id_direct = 60 and 
                                    (rt.idKontrTitle = @idKontrTitle or @idKontrTitle = 0) and
                                    (rt.idTm = @idTm or @idTm = 0)
";

            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);

            return DBExecutor.SelectTable(sql, ParamidKontrTitle, ParamidTm);

        }

        public static void SaveMinBrand(DataRow row)
        {
            if (row.RowState == DataRowState.Deleted)
            {
                SqlParameter paridBrand = new SqlParameter("idBrand", row["idBrand", DataRowVersion.Original]);
                string sql = "delete from sPercToSebest where idBrand =  @idBrand and idCat = 2";
                DBExecutor.ExecuteQuery(sql, paridBrand);
            }
            else
            {
                SqlParameter paridBrand = new SqlParameter("idBrand", row["idBrand"]);
                SqlParameter MinPercAll = new SqlParameter("MinPercAll", row["MinPercAll"]);
                SqlParameter MinPercASKU = new SqlParameter("MinPercASKU", row["MinPercASKU"]);

                string sql = @"
                                    if exists(select * from sPercToSebest where idBrand = @idBrand and idCat = 2)
    								    update sPercToSebest 
									        set MinPercAll = @MinPercAll/100,
                                                MinPercASKU = @MinPercASKU/100
									        where idBrand = @idBrand  and idCat = 2 
                                    else
                                        insert into sPercToSebest(idBrand, idCat, MinPercAll, MinPercASKU)
                                        values (@idBrand, 2, @MinPercAll/100, @MinPercAll/100)
";
                DBExecutor.ExecuteQuery(sql, paridBrand, MinPercAll, MinPercASKU);

            }
        }

        public static void SaveABonus(DataRow row)
        {
            if (row.RowState == DataRowState.Deleted)
            {
                SqlParameter paridBrand = new SqlParameter("idBrand", row["tm_id", DataRowVersion.Original]);
                string sql = "delete from sArkonaBonus where idBrand =  @idBrand";
                DBExecutor.ExecuteQuery(sql, paridBrand);
            }
            else
            {
                SqlParameter paridBrand = new SqlParameter("idBrand", row["tm_id"]);
                SqlParameter retailBonus21 = new SqlParameter("retailBonus21", row["P1"]);
                SqlParameter retailBonus22 = new SqlParameter("retailBonus22", row["P2"]);
                SqlParameter retailBonus23 = new SqlParameter("retailBonus23", row["P3"]);
                SqlParameter retailMarkup = new SqlParameter("retailMarkup", row["retailMarkup"]);
                SqlParameter optMarkup = new SqlParameter("optMarkup", row["optMarkup"]);
                /*SqlParameter optBonus = new SqlParameter("optBonus", row["optBonus"]);
                SqlParameter optASKUBonus = new SqlParameter("optASKUBonus", row["optASKUBonus"]);*/
                SqlParameter optBonus = new SqlParameter("optBonus", row["O2"]);
                SqlParameter optASKUBonus = new SqlParameter("optASKUBonus", row["O2"]);
                SqlParameter retailASKUBonus21 = new SqlParameter("retailASKUBonus21", row["retailASKUBonus21"]);
                SqlParameter retailASKUBonus22 = new SqlParameter("retailASKUBonus22", row["retailASKUBonus22"]);
                SqlParameter retailASKUBonus23 = new SqlParameter("retailASKUBonus23", row["retailASKUBonus23"]);
                SqlParameter retailBonus20 = new SqlParameter("P0", row["P0"]);
                SqlParameter opt1Bonus = new SqlParameter("O1", row["O1"]);
                SqlParameter opt2Bonus = new SqlParameter("O2", row["O2"]);
                SqlParameter retailASKUBonus20 = new SqlParameter("P0ASKU", row["P0ASKU"]);
                SqlParameter opt1ASKUBonus = new SqlParameter("O1ASKU", row["O1ASKU"]);
                SqlParameter opt2ASKUBonus = new SqlParameter("O2ASKU", row["O2ASKU"]);

                    string sql = @"
                                    if exists(select * from sArkonaBonus where idBrand = @idBrand)
    								    update sArkonaBonus 
									        set retailBonus21 = @retailBonus21/100,
									        retailBonus22 = @retailBonus22/100,
									        retailBonus23 = @retailBonus23/100,
                                            retailMarkup = @retailMarkup/100,
                                            optMarkup = @optMarkup/100,
                                            optBonus = @optBonus/100,
                                            optASKUBonus = @optASKUBonus/100,
                                            retailASKUBonus21 = @retailASKUBonus21/100,
                                            retailASKUBonus22 = @retailASKUBonus22/100,
                                            retailASKUBonus23 = @retailASKUBonus23/100,
                                            retailBonus20 = @P0/100,
                                            opt1Bonus = @O1/100,
                                            opt2Bonus = @O2/100,
                                            retailASKUBonus20 = @P0ASKU/100,
                                            opt1ASKUBonus = @O1ASKU/100,
                                            opt2ASKUBonus = @O2ASKU/100
									    where idBrand = @idBrand
                                    else
                                        insert into sArkonaBonus(idBrand, retailBonus21, retailBonus22, retailBonus23, retailMarkup, 
                                        optBonus, optMarkup, optASKUBonus, retailASKUBonus21, retailASKUBonus22, retailASKUBonus23,
                                        retailBonus20, opt1Bonus, opt2Bonus, retailASKUBonus20, opt1ASKUBonus, opt2ASKUBonus)
                                        values (@idBrand, @retailBonus21/100, @retailBonus22/100, @retailBonus23/100, @retailMarkup/100, 
                                        @optBonus/100, @optMarkup/100, @optASKUBonus/100, @retailASKUBonus21/100, @retailASKUBonus22/100, @retailASKUBonus23/100,
                                        @P0/100, @O1/100, @O2/100, @P0ASKU/100, @O1ASKU/100, @O2ASKU/100)
";
                DBExecutor.ExecuteQuery(sql, paridBrand, retailBonus21, retailBonus22, retailBonus23, retailMarkup, 
                    optBonus, optMarkup, optASKUBonus, retailASKUBonus21, retailASKUBonus22, retailASKUBonus23,
                    retailBonus20, opt1Bonus, opt2Bonus, retailASKUBonus20, opt1ASKUBonus, opt2ASKUBonus);

            }

        }

        public static DataTable GetMinBrand(int idKontrTitle, int idTm)
        {
            /*
            string sql = @"
select 
    sb.idPercToSebest, 
    sb.idBrand, 
    tm.tm_name,
    cast(sb.MinPercAll*100 as decimal(18,2)) as MinPercAll, 
    cast(MinPercASKU*100 as decimal(18,2)) as MinPercASKU
from sPercToSebest sb
    join spr_tm tm on tm.tm_id = sb.idBrand
    join rKontrTitleTm rt on rt.idTm = tm.tm_id
where 
    (rt.idKontrTitle = @idKontrTitle or @idKontrTitle = 0) and
    (rt.idTm = @idTm or @idTm = 0)  and 
    sb.idCat = 2 
";

    */

            string sql = @"


select
--r.idPercToSebest,
r.idBrand, 
r.tm_name,
sum(isnull(r.all4, 0))*100 MinPercAllImg,
sum(isnull(r.asku4, 0))*100 MinPercASKUImg,
sum(isnull(r.all3, 0))*100 MinPercAllOpt,
sum(isnull(r.asku3, 0))*100 MinPercASKUOpt,
sum(isnull(r.all2, 0))*100 MinPercAllRetail,
sum(isnull(r.asku2, 0))*100 MinPercASKURetail
from
(
select
--sPercToSebest.idPercToSebest,
sPercToSebest.idBrand,
spr_tm.tm_name,
case when sPercToSebest.idcat = 4 then minpercall end as all4,
case when sPercToSebest.idcat = 4 then minpercasku end as asku4,
case when sPercToSebest.idcat = 3 then minpercall end as all3,
case when sPercToSebest.idcat = 3 then minpercasku end as asku3,
case when sPercToSebest.idcat = 2 then minpercall end as all2,
case when sPercToSebest.idcat = 2 then minpercasku end as asku2
from sPercToSebest (nolock)
inner join spr_tm (nolock) on spr_tm.tm_id = sPercToSebest.idBrand
inner join rKontrTitleTm rt on rt.idTm = spr_tm.tm_id
) r
where r.idbrand = @idTm

group by
--r.idPercToSebest,
r.idBrand, 
r.tm_name
order by r.tm_name



";


            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);

            return DBExecutor.SelectTable(sql, ParamidKontrTitle, ParamidTm);

        }

        public static DataTable GetTovAsku(int idKontrTitle, int idTm)
        {
            string sql = @"
                        select t.id_tov, t.id_tov_oem, t.n_tov from spr_price p
	                        join spr_tov t on t.id_tov = p.id_tov
	                        join rKontrTitleTm rt on rt.idTm = t.id_tm
                        where fMinPercASKU = 1 and p.id_direct = 60 and 
                                                            (rt.idKontrTitle = @idKontrTitle or @idKontrTitle = 0) and
                                                            (rt.idTm = @idTm or @idTm = 0)
";

            SqlParameter ParamidKontrTitle = new SqlParameter("idKontrTitle", idKontrTitle);
            SqlParameter ParamidTm = new SqlParameter("idTm", idTm);

            return DBExecutor.SelectTable(sql, ParamidKontrTitle, ParamidTm);
        }
    }
}
