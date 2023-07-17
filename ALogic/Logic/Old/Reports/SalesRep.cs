using ALogic.Logic.SPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Reports
{
    public static class SalesRep
    {
        public static DataTable GetData( DateTime dates, DateTime datee)
        {
//            string sql = @"

//select 
//	 idAgent
//	,nAgent
//	,nKontr
//    ,t.idKontr as idKontr
//	,nBrand
//	,t.nGroup as nGroup
//	,dateDoc 
//    ,cast(year(dateDoc) as varchar(4)) +'.'+ case when month(dateDoc) < 10 then '0' else '' end + cast(month(dateDoc) as varchar(2)) as nMonth
//    ,case datepart(weekday,dateDoc)
//         when 1 then 'ВС'
//         when 2 then 'ПН'
//         when 3 then 'ВТ'
//         when 4 then 'СР'
//         when 5 then 'ЧТ'
//         when 6 then 'ПТ'
//         when 7 then 'СБ' end as nDay

//	,round(sum(sumSale), 2) as sumSale
//	,round(sum(profitSale), 2) as profitSale	
//    ,isnull(round(sum(sumSale), 2), 0) + isnull(round(sum(suminWay), 2), 0)  as SaleAll
//    ,isnull(round(sum(profitSale), 2), 0) + isnull(round(sum(profitinWay), 2), 0)  as profitAll
//    ,nZakTov
//    ,nViewMotivation
//    ,nCompany
//    --,l1.nSegment as nSegment
//    ,v_rNameGroupSegm.ngroup as nSegment
//from 
//(
//select
//	 ag.id_kontr as idAgent
//	,ag.n_kontr as nAgent
//	,kl.n_kontr as nKontr
//    ,kl.id_kontr as idKontr
//	,tm.tm_name as nBrand
//	,t4.tov_name as nGroup
//	,v_sales.date_doc as dateDoc 
//	,sum(v_sales.sum_rub) as sumSale
//    ,isnull(Sum((isnull(v_sales.sum_rub,0) / v_sales.kol_tov - isnull(v_sales.sebest_rub,0)) * v_sales.kol_tov),0) As profitSale
//	,null as suminWay
//    ,null as profitinWay
//    ,ag.id_Post
//    ,case when exists (select * from tov where  tov.fOrderTov <> 1 and tov.id_doc = v_sales.id_doc and tov.id_batch = v_sales.id_batch and tov.id_tov = v_sales.id_tov and tov.id_doc_in = v_sales.tov_in_id_doc )
//		  then 'Постоянного наличия' else 'Кроссдокинг' end as nZakTov
//    ,isnull(vm.nViewMotivation, vm1.nViewMotivation) as nViewMotivation
//    ,isnull(km.n_kontr, kl.n_kontr) as nCompany
//from v_sales
//	inner join spr_kontr ag (nolock) on v_sales.id_agent = ag.id_kontr  and v_sales.id_direct = 60
//	left join spr_kontr kl  (nolock) on kl.id_kontr = v_sales.id_kontr
//	left join spr_tov t  (nolock) on t.id_tov = v_sales.id_tov
//	left join spr_tm tm  (nolock) on tm.tm_id = T.id_tm
//	left join spr_tov_level4  t4 (nolock) on t4.tov_id = t.id_tov4   
//    left join rKontrEntity  (nolock) on rKontrEntity.id_kontr = kl.id_kontr
//    left join spr_kontr km  (nolock) on km.id_kontr = rKontrEntity.idKOntr 
//    left join rKontrViewMotivation rm  (nolock) on rm.mm = datepart(mm, dateadd(mm, -1, v_sales.date_doc)) and rm.yy = datepart(yy, dateadd(mm, -1,  v_sales.date_doc)) and rm.idkontr =  km.id_kontr 
//    left join sViewMotivation vm  (nolock) on vm.idViewMotivation = rm.idViewMotivation
//    left join rKontrViewMotivation rm1  (nolock) on rm1.mm = datepart(mm, dateadd(mm, -1, v_sales.date_doc)) and rm1.yy = datepart(yy, dateadd(mm, -1,  v_sales.date_doc)) and rm1.idkontr =  kl.id_kontr
//    left join sViewMotivation vm1  (nolock) on vm1.idViewMotivation = rm1.idViewMotivation

//where date_doc between @dates and  @datee 
//group by  ag.n_kontr 
//	,kl.n_kontr 
//	,tm.tm_name
//	,t4.tov_name
//	,v_sales.date_doc	
//	,ag.id_kontr
//	,ag.id_Post
//	,v_sales.id_doc
//	,v_sales.id_tov
//	,v_sales.id_batch
//	,v_sales.tov_in_id_doc
//    ,isnull(vm.nViewMotivation, vm1.nViewMotivation) 
//    ,km.n_kontr
//    ,kl.id_kontr
//union all

//select 
//	 ag.id_kontr as idAgent
//	,ag.n_kontr as nAgent
//	,kl.n_kontr as nKontr
//    ,kl.id_kontr as idKontr
//	,tm.tm_name as nBrand
//	,t4.tov_name as nGroup
//	,tov_doc.date_doc as dateDoc 
//	,null as sumSale
//    ,null as profitSale
//	,sum(tov.kol * tov.priceB) as suminWay
//    ,Sum((tov.priceB - spr_price.sebest) * tov.kol) As profitinWay
//    ,ag.id_Post
//    ,case when tov.fOrderTov <> 1 then  'Постоянного наличия' else 'Кроссдокинг' end as nZakTov
//    ,isnull(vm.nViewMotivation, vm1.nViewMotivation) as nViewMotivation
//    ,isnull(km.n_kontr, kl.n_kontr) as nCompany
//from 
//	tov_doc  (nolock)
//		inner join tov  (nolock) on tov_doc.id_doc = tov.id_doc
//		inner join spr_price  (nolock) on spr_price.id_tov = tov.id_tov and spr_price.id_direct = 60
//		inner join spr_kontr kl  (nolock) on kl.id_kontr = tov_doc.id_kontr_db 
//		inner join spr_agent_kontr rag  (nolock) on rag.id_direct = 60 and rag.id_kontr = kl.id_kontr
//		inner join spr_kontr ag  (nolock) on ag.id_kontr = rag.id_agent
//		inner join spr_tov t  (nolock) on  t.id_tov = tov.id_tov
//		inner join spr_tm tm  (nolock) on tm.tm_id = T.id_tm
//		INNER join spr_tov_level4 t4  (nolock) on t4.tov_id = t.id_tov4       
//        left join rKontrEntity  (nolock) on rKontrEntity.id_kontr = kl.id_kontr
//        left join spr_kontr km  (nolock) on km.id_kontr = rKontrEntity.idKOntr 
//        left join rKontrViewMotivation rm  (nolock) on rm.mm = datepart(mm, dateadd(mm, -1, tov_doc.date_doc)) and rm.yy = datepart(yy, dateadd(mm, -1,  tov_doc.date_doc)) and rm.idkontr =  km.id_kontr 
//        left join sViewMotivation vm  (nolock) on vm.idViewMotivation = rm.idViewMotivation
//        left join rKontrViewMotivation rm1  (nolock) on rm1.mm = datepart(mm, dateadd(mm, -1, tov_doc.date_doc)) and rm1.yy = datepart(yy, dateadd(mm, -1,  tov_doc.date_doc)) and rm1.idkontr =  kl.id_kontr
//        left join sViewMotivation vm1  (nolock) on vm1.idViewMotivation = rm1.idViewMotivation
//where 
//		tov_doc.id_type_doc = 19
//    and tov_doc.id_direct = 60
//	and id_status_doc = 0
//	and date_doc between @dates and  @datee
//	and in_tax <> 10
//    and len(id_trans) > 0
    
//group by  ag.n_kontr 
//	,kl.n_kontr 
//	,tm.tm_name
//	,t4.tov_name
//	,tov_doc.date_doc
//	,ag.id_kontr
//	,ag.id_Post
//	,tov.fOrderTov
//    ,isnull(vm.nViewMotivation, vm1.nViewMotivation) 
//    ,km.n_kontr
//    ,kl.id_kontr
//) t
///*
//    left join rKontrSegment sg on sg.idKontr = t.idKontr
//    left join sSegment l2 on l2.idSegment = sg.idsegment
//    left join sSegment l1 on l2.idTopLevel = l1.idsegment
//*/
//   inner join  v_rNameGroupSegm
//            on t.idKontr =  v_rNameGroupSegm.idkontr 
//where  
//    @idPost in (96, 142, 28, 29, 26, 27, 13, 76, 71, 162)  or (@idPost in ( 146, 122, 97, 21, 155, 96, 158, 163) and t.id_post in (139, 93, 159)) 
//or (@idPost in(155, 153, 96, 158) and  t.id_post in(150, 152, 148, 151,138, 157)) or (t.idAgent = @idAgent) 
//group by 
//	 idAgent
//	,nAgent
//	,nKontr
//	,nBrand
//	,t.nGroup
//	,dateDoc 
//    ,nZakTov
//    ,nViewMotivation
//    ,nCompany
//    ,t.idKontr
//    --,l1.nSegment
//    ,v_rNameGroupSegm.ngroup
//";
            var idUser = User.CurrentUserId;
       //     var idPost = ALogic.Logic.SPR.User.GetPostByUserId(idUser);

            SqlParameter parDateS = new SqlParameter("dates", dates);
            SqlParameter parDateE = new SqlParameter("datee", datee);
         //   SqlParameter paridPost = new SqlParameter("idPost", idPost);
            SqlParameter paridAgent = new SqlParameter("idAgent", idUser);

            //var tbl = DBConnector.DBExecutor.SelectTable(sql, parDateS, parDateE, paridPost, paridAgent);
            //return tbl;
            return DBConnector.DBExecutor.ExecuteProcedureTable("up_GetSaleReport", parDateS, parDateE, paridAgent);
        }      
    }
}
