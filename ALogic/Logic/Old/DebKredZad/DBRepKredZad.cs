using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.DebKredZad
{
    public static class DBRepKredZad
    {


        public static DataTable GetKontrForRep()
        {
            string sql = @"
select
	 k.id_kontr as idKontr
	,k.n_kontr as nKontr
	,ag.n_kontr as nAgent
	,c.nom_contract  as nomContract
    ,c.idConvention as idConv
	,c.id_firm as idFirm
	,d.Abbreviation as Abbr
	,isnull(c.SumCred, 0) as SumCred
	,isnull((select top 1 isnull(KolDay, 0) from sRequestForPaymentTemplate where idConvention = c.idConvention and idTypeCalcRequest = 2 and  isnull(idOrderbuy,0) = 0), 0) as KolDay
	,t.id_cur as idCur
	,cast(round(t.summa, 0) as int)  as oborot62
    ,(select max(date_doc) from tov_doc where id_kontr_kr = k.id_kontr and id_type_doc = 3 and id_status_doc in (20, 30) and in_tax <> 10) as dateLast
	
from 
	spr_kontr k
		inner join spr_kontr ag on ag.id_kontr = k.id_agent
		left join
            (select min(idConvention) as idConv, id_kontr from contract where id_contract in (3, 4, 11) and fDefault = 1 and idStatusContract in (10, 20) group by id_kontr ) q on q.id_kontr = k.id_kontr
        left join contract c on q.idConv = c.idConvention
		left join DetailOurFirm d on d.IdFirm = c.id_firm
		
		inner join 
		(
	select 
		 id_kontr
		,id_cur
		,sum(summa) as summa	
	from
		oborot 
	where schet = '62' and summa <> 0  and in_tax <> 10 and oborot.id_direct = 60
	group by id_kontr,id_cur
	having sum(summa) <> 0 
		) t on t.id_kontr = k.id_kontr
		
where
	k.supplier = 1 and k.firma = 0 
";

            return DBExecutor.SelectTable(sql);

        }


        public static DataTable GetDocForRep()
        {
            string sql = @" 
select
	idSupplier, idFirm, SumDoc, idCur, CurRec, dateDoc
	,case when SumDoc > 0 then ISNULL(l.SumLink, 0) else -ISNULL(l.SumLink, 0) end as  SumLinc	
	,idConv
	
 from  
	 (select 
		 tov_doc.id_doc as idDoc
		,sum(tov_in.kol * tov_in.price)	as SumDoc
		,tov_doc.id_cur as idCur
		,tov_doc.cur_rec as CurRec
		,tov_doc.date_doc as dateDoc
		,tov_doc.id_kontr_kr as idSupplier
		,tov_doc.id_firm as idFirm
        ,tov_doc.idConvention as idConv
	from
		tov_doc
			inner join tov_in on tov_doc.id_doc = tov_in.id_doc	          
	where id_type_doc in (select id_type_doc from spr_type_doc where kr = '62')		
		and id_status_doc in (20, 30)
		and in_tax <> 10  and tov_doc.id_direct = 60
	group by 
		 tov_doc.id_doc 		
		,tov_doc.id_cur 
		,tov_doc.cur_rec 
		,tov_doc.date_doc 
		,tov_doc.id_kontr_kr 
		,tov_doc.id_firm 
		,tov_doc.idConvention
		
	union all 
	
	select 
		 tov_doc.id_doc as idDoc
		,sum(tov.kol * tov.price)	as SumDoc
		,tov_doc.id_cur as idCur
		,tov_doc.cur_rec as CurRec
		,tov_doc.date_doc as dateDoc
		,tov_doc.id_kontr_kr as idSupplier
		,tov_doc.id_firm as idFirm
		,tov_doc.idConvention as idConv
	from
		tov_doc
			inner join tov on tov_doc.id_doc = tov.id_doc	          
	where id_type_doc in (select id_type_doc from spr_type_doc where kr = '62')		
		and id_status_doc in (20, 30)
		and in_tax <> 10  and tov_doc.id_direct = 60
	group by 
		 tov_doc.id_doc 		
		,tov_doc.id_cur 
		,tov_doc.cur_rec 
		,tov_doc.date_doc 
		,tov_doc.id_kontr_kr 
		,tov_doc.id_firm 
		,tov_doc.idConvention
		
	union all 
	
	select 
		 fin_doc.id_doc as idDoc
		,fin_doc.sum_kr	as SumDoc
		,fin_doc.id_cur_kr as idCur
		,fin_doc.cur_rec_kr as CurRec
		,fin_doc.date_doc as dateDoc
		,fin_doc.id_kontr_kr as idSupplier
		,fin_doc.id_firm as idFirm
		,fin_doc.idConvention
	from
		fin_doc	
	where id_type_doc in (select id_type_doc from spr_type_doc where kr = '62')		
		and id_status_doc in (20, 30)
		and in_tax <> 10  and fin_doc.id_direct = 60
	
	union all
	
	select 
		 fin_doc.id_doc as idDoc
		,-fin_doc.sum_db	as SumDoc
		,fin_doc.id_cur_db as idCur
		,fin_doc.cur_rec_db as CurRec
		,fin_doc.date_doc as dateDoc
		,fin_doc.id_kontr_db as idSupplier
		,fin_doc.id_firm as idFirm
		,fin_doc.idConvention
	from
		fin_doc	
	where id_type_doc in (select id_type_doc from spr_type_doc where db = '62')		
		and id_status_doc in (20, 30)
		and in_tax <> 10  and fin_doc.id_direct = 60
			
	) t
	
left join 
	(
		select 
			SUM(SumLink) as SumLink, idDoc
		from 
			v_SumLinkDoc
		group by idDoc
	) l on l.idDoc = t.idDoc
	
	where abs(round(SumDoc, 0)) <> ABS( ROUND( ISNULL(l.SumLink, 0), 0) )


";
            return DBExecutor.SelectTable(sql);
        }


        public static DataTable GetDebZad(object date)
        {
            //            string sql = @"
            //select 
            //	rest.id_kontr kodKa
            //	,spr_kontr.n_kontr naimenovanieKA
            //	,v_rNameGroupSegm.ngroup kanalSbyta
            //	,agent_fio.n_kontr Agent
            //     ,agent_fio.id_kontr idAgent
            //	,spr_agent_kontr.id_direct idDirectAgent
            //	,DetailOurFirm.Abbreviation PreficsFirmy
            //	,contract.id_firm  idFirm
            //	,contract.nom_contract NomerDogovora
            //	,contract.idconvention IdDogovora
            //	,contract.date_contract DateDogovora
            //	,isnull(contract.sumcred, 0) SummKreditLemitDogovora
            //	,contract.DaysKred OtsrochDogovora
            //	,case Isnull(contract.idtypeDayskred,0) when 10 then 'Р' when 40 then 'К' else '' end TypeOtsroch
            //	,isnull(rest.summa, 0) CurrentDZ
            //	,acc_group.[0-7]+acc_group.[8-15]+acc_group.[16-21]+acc_group.[22-30]+acc_group.[свыше 31]  SymmProsrochDZ
            //	,(acc_group.[0-7]+acc_group.[8-15]+acc_group.[16-21]+acc_group.[22-30]+acc_group.[свыше 31])/rest.summa * 100  PersentPDZinCurrentDZ
            //	,acc_group.[0-7] id07
            //	,acc_group.[8-15] id815
            //	,acc_group.[16-21] id1621
            //	,acc_group.[22-30] id2230
            //	,acc_group.[свыше 31] id31
            //	,MAX(tov_doc.date_doc) lastUpload
            //from
            //	(select oborot_days.id_kontr as id_kontr,		
            //        sum(oborot_days.summa) summa,
            //        oborot_days.id_addition2 as idconvention 
            //    from oborot_days (nolock)  
            //	where
            //		@dates	 < cast (getdate() as date)  and
            //		oborot_days.date_rest = @dates	 and
            //		oborot_days.schet = '60' and
            //		oborot_days.in_tax in (0,11)
            //		and oborot_days.id_direct = 60
            //    group by 
            //		oborot_days.id_kontr ,	
            //		oborot_days.id_addition2 
            //    having sum(oborot_days.summa) <> 0

            //	union all	
            //	select oborot.id_kontr as id_kontr,		
            //		sum(oborot.summa) summa,
            //		oborot.id_addition2 as idconvention 
            //	from oborot (nolock)  
            //	where
            //		@dates	 = cast (getdate() as date)  and
            //		oborot.schet = '60' and
            //		oborot.in_tax in (0,11)
            //		and oborot.id_direct = 60
            //	group by 
            //		oborot.id_kontr ,	
            //		oborot.id_addition2  
            //	having sum(oborot.summa) <> 0

            //	)rest 

            //	left outer join
            //	(select  
            //		neopl_acc.id_kontr,		
            //		neopl_acc.idconvention,		
            //		sum(case when neopl_acc.day_PDZ > 0 and neopl_acc.day_PDZ <= 7 then neopl_acc.sum_dolg else 0 end)as '0-7',
            //		sum(case when neopl_acc.day_PDZ > 7 and neopl_acc.day_PDZ <= 14 then neopl_acc.sum_dolg else 0 end)as '8-15',
            //		sum(case when neopl_acc.day_PDZ > 15 and neopl_acc.day_PDZ <= 21 then neopl_acc.sum_dolg else 0 end)as '16-21',
            //		sum(case when neopl_acc.day_PDZ > 21 and neopl_acc.day_PDZ <= 30 then neopl_acc.sum_dolg else 0 end)as '22-30',
            //		sum(case when neopl_acc.day_PDZ > 30 then neopl_acc.sum_dolg else 0 end)as 'свыше 31'

            //	from
            //		(select tov_doc.id_doc,   
            //			v_sum_out_tov_doc_rub.sum_doc,			
            //			v_sum_out_tov_doc_rub.sum_doc - sum(isnull(link_doc_new.sum_link,0)) sum_dolg,
            //		    /*datediff( dd, (tov_doc.date_doc), v_today.tdate) - case isnull(contract.idtypedayskred,0) when 10 then dbo.uf_kolfreeday(tov_doc.date_doc, v_today.tdate) else 0 end day_dolg,*/
            //			/*Количество дней просрочки с учетом типа дней кредитования*/
            //			/*datediff( dd, (tov_doc.date_doc), v_today.tdate) - case isnull(contract.idtypedayskred,0) when 10 then dbo.uf_kolfreeday(tov_doc.date_doc, v_today.tdate) else 0 end - isnull(contract.DaysKred,0) day_PDZ,*/
            //			datediff( dd, (tov_doc.date_doc),@dates	) - case isnull(contract.idtypedayskred,0) when 10 then dbo.uf_kolfreeday(tov_doc.date_doc, @dates	) else 0 end - isnull(contract.DaysKred,0) day_PDZ,	
            //			tov_doc.id_kontr_db id_kontr,
            //			tov_doc.idconvention  as idconvention 
            //		from tov_doc(nolock) 		
            //		inner join   v_sum_out_tov_doc_rub  
            //		on 
            //			tov_doc.id_doc = v_sum_out_tov_doc_rub.id_doc
            //		left outer join contract (nolock)
            //		on 
            //			tov_doc.idconvention = contract.idconvention
            //		left outer join link_doc_new (nolock)
            //		on 
            //			tov_doc.id_doc = link_doc_new.id_doc_1
            //		,v_today 
            //		where tov_doc.id_type_doc in (8,19)  and  
            //			tov_doc.in_tax in (0,11)  and  
            //			tov_doc.id_status_doc in (20,30,40) 
            //			and tov_doc.id_direct = 60
            //		group by tov_doc.id_doc,   
            //			v_sum_out_tov_doc_rub.sum_doc,  
            //			tov_doc.date_doc,
            //			tov_doc.id_kontr_db,
            //			tov_doc.idconvention, 
            //			v_today.tdate, 
            //			isnull(contract.idtypedayskred,0),
            //			contract.DaysKred
            //		having v_sum_out_tov_doc_rub.sum_doc > sum(isnull(link_doc_new.sum_link,0))   
            //		)neopl_acc
            //	group by neopl_acc.id_kontr,				
            //				neopl_acc.idconvention		
            //	) acc_group
            //	on rest.id_kontr = acc_group.id_kontr and
            //	--rest.id_agent = acc_group.id_agent and
            //	rest.idconvention = acc_group.idconvention


            //	inner join spr_kontr(nolock) 
            //		on spr_kontr.id_kontr = rest.id_kontr
            //	inner join v_rnamegroupsegm(nolock)
            //		on v_rnamegroupsegm.idkontr = spr_kontr.id_kontr
            //	inner join contract (nolock) 
            //		on rest.idconvention = contract.idconvention	
            //	inner join detailourfirm(nolock)
            //		on detailourfirm.idfirm = contract.id_firm 
            //	inner join tov_doc on
            //	tov_doc.id_kontr_db = rest.id_kontr	
            //		and acc_group.idconvention = tov_doc.idConvention
            //		and tov_doc.id_type_doc  in (8,19)
            //		and tov_doc.id_status_doc in (20,30)
            //		and tov_doc.id_direct = 60
            //	left outer join spr_agent_kontr (nolock)
            //		on  rest.id_kontr = spr_agent_kontr.id_kontr 
            //		and spr_agent_kontr.id_direct = 60
            //	inner join spr_kontr as agent_fio
            //		on agent_fio.id_kontr = spr_agent_kontr.id_agent	

            //where 	
            //	 (
            //	 spr_kontr.seller_bayer in (20,30)	
            //	 or spr_kontr.employer = 1
            //	 )
            //	 and spr_kontr.id_cond = 10
            //	 and contract.idStatusContract in (10,20,30)	
            //	 and tov_doc.date_doc <= @dates	


            //	 group by 
            //		rest.id_kontr 
            //		,spr_kontr.n_kontr 
            //		,v_rNameGroupSegm.ngroup 
            //		,agent_fio.id_kontr
            //		,agent_fio.n_kontr 
            //		,DetailOurFirm.Abbreviation 
            //		,contract.nom_contract  
            //		,contract.id_firm 
            //		,contract.idconvention 
            //		,contract.date_contract
            //		,contract.sumcred 
            //		,contract.DaysKred 
            //		,case Isnull(contract.idtypeDayskred,0) when 10 then 'Р' when 40 then 'К' else '' end 
            //		,rest.summa 		
            //		,acc_group.[0-7]
            //		,acc_group.[8-15]
            //		,acc_group.[16-21]
            //		,acc_group.[22-30]
            //		,acc_group.[свыше 31]
            //		,spr_agent_kontr.id_direct
            //";

            SqlParameter par = new SqlParameter("dates", date);
            // return DBExecutor.SelectTable(sql, par);
            return DBExecutor.ExecuteProcedureTable("up_RepForDebt", par);
        }
    }
}
