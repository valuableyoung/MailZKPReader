using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Reports
{
    public class DataSale
    {
        public static DataTable getSalePeriod(DateTime start, DateTime end)
        {
            //                var sql = @"Select Sum(v_sales.sum_rub) As sumSale, Sum(v_sales.kol_tov) As kolSale, sTerritory.nTerritoryShort,
            //                              Sum((v_sales.sum_rub / v_sales.kol_tov - v_sales.sebest_rub) *
            //                              v_sales.kol_tov) As Profit, Sum((v_sales.sum_rub / v_sales.kol_tov -
            //                              v_sales.sebest_transport) * v_sales.kol_tov) As Profittrunsport, DatePart(mm,
            //                              v_sales.date_doc) As MonthSale, DatePart(yyyy, v_sales.date_doc) As YearSale,
            //                              isnull(spr_tov_level4.tov_name, 'Не назначен') As nTypeTov, sTovGroup.n_tov As
            //                              NameGroup, isnull(spr_agent.n_kontr, 'не закреплен') As nAgent,
            //                              spr_tov.n_tov As nPodgrup, spr_kontr.n_kontr As nKontr, spr_tm.tm_name,
            //                              oblast.n_position As Oblast, isnull(v_rNameGroupSegm.ngroup, 'не закреплен')
            //                              As Segment, v_rTovLine.n_tov_line As TovLine, isnull(tovgroup.tov_name,
            //                              'не назначен') As TOV_NAME, tov_doc.in_tax, Case
            //                                When isnull(tov_doc.fordertov, 0) = 1 Then 'кросс-докинг' Else 'склад'
            //                              End As forder,
            //                              isnull(sCategory.nCategory, 'не определена') nCategory
            //                            From v_sales(nolock) Inner Join
            //                              spr_kontr(nolock) On v_sales.id_kontr = spr_kontr.id_kontr And
            //                                 v_sales.date_doc>='" + start.Year + "." + start.Month + "." + start.Day + @"' and v_sales.date_doc<='" + end.Year + "." + end.Month + "." + end.Day + @"' 
            //	                           And v_sales.id_direct = 60 And
            //                                v_sales.type_doc <> 84 Inner Join
            //                              spr_tov(nolock) On v_sales.id_podgr = spr_tov.id_tov Inner Join
            //                              spr_tov As sTovGroup On sTovGroup.id_tov = spr_tov.id_top_level Inner Join
            //                              spr_tm(nolock) On v_sales.id_tm = spr_tm.tm_id Inner Join
            //                              tov_doc(nolock) On tov_doc.id_doc = v_sales.id_doc Left Outer Join
            //                              spr_tov_level4(nolock) On v_sales.id_tov4 = spr_tov_level4.tov_id
            //                              Left Outer Join spr_kontr As spr_agent On v_sales.id_agent = spr_agent.id_kontr
            //                              Left Outer Join spr_position(nolock) On v_sales.id_position = spr_position.id_position
            //                              Left Outer Join spr_position As oblast On spr_position.id_top_level = oblast.id_position
            //                              Left Outer Join
            //                              spr_tov As litraz On litraz.id_tov = v_sales.id_tov Inner Join
            //                              spr_tov As tovar On v_sales.id_tov = tovar.id_tov Left Outer Join
            //                              sassortment(nolock) On sassortment.idAssortment = tovar.idAssortment
            //                              Left Outer Join
            //                              v_rNaprRabot On v_rNaprRabot.idkontr = spr_kontr.id_kontr Left Outer Join
            //                              v_rTovLine On spr_kontr.id_kontr = v_rTovLine.id_kontr Left Outer Join
            //                              v_rNameGroupSegm On spr_kontr.id_kontr = v_rNameGroupSegm.idkontr
            //                              Left Outer Join spr_tov_level4 As tovgroup On tovgroup.tov_id = spr_tov_level4.tov_id_top_level And tovgroup.tov_id_level = 1
            //							  left join sTerritory on sTerritory.idTerritory = v_sales.idTerritory
            //                              left join spr_agent_kontr on spr_agent_kontr.id_kontr = v_sales.id_kontr
            //                              left join sCategory on sCategory.Category = spr_agent_kontr.category
            //                            Group By DatePart(mm, v_sales.date_doc), DatePart(yyyy, v_sales.date_doc),
            //                              sTovGroup.n_tov, spr_tov.n_tov, spr_kontr.n_kontr, spr_tm.tm_name,
            //                              oblast.n_position, v_rTovLine.n_tov_line, tov_doc.in_tax,
            //                              spr_tov_level4.tov_name, v_rNameGroupSegm.ngroup, spr_agent.n_kontr,
            //                              spr_kontr.id_kontr, tovar.id_tov, tovgroup.tov_name, tov_doc.fordertov, sTerritory.nTerritoryShort, sCategory.nCategory
            //                            option (hash group)
            //";

            //return DBConnector.DBExecutor.SelectTable(sql);

            SqlParameter par_datefrom = new SqlParameter("datefrom", start);
            SqlParameter par_dateto = new SqlParameter("dateto", end);
            //return DBConnector.DBExecutor.SelectTable(sql, paridAgent, pardate);
            return DBConnector.DBExecutor.ExecuteProcedureTable("up_Manage_SalesReport", par_datefrom, par_dateto);
        }

        public static List<ALogic.DBConnector.SLTableRow> getSalePeriodList(DateTime start, DateTime end, int IdUser)
        {
            SqlParameter par_datefrom = new SqlParameter("datefrom", start);
            SqlParameter par_dateto = new SqlParameter("dateto", end);
            SqlParameter par_iduser = new SqlParameter("iduser", IdUser);
            //return DBConnector.DBExecutor.ExecuteProcedureTableReadOnly("up_Manage_SalesReport_U", par_datefrom, par_dateto);
            DBConnector.DBExecutor.ExeciteProcedure("up_Manage_SalesReport_U", par_datefrom, par_dateto, par_iduser);
            return null;
        }
    }
}

