using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR
{
    public static class DocLogic
    {
        public static string NextNom(object idFirm, object inTax, object idTypeDoc)
        {           
            SqlParameter paridFirm = new SqlParameter("idFirm", idFirm);
            SqlParameter parinTax = new SqlParameter("inTax", inTax);
            SqlParameter paridTypeDoc = new SqlParameter("idTypeDoc", idTypeDoc);
            SqlParameter paryear = new SqlParameter("year", DateTime.Now.Year);

            string sqlNom = @"
if not exists (select * from last_nom where id_firm = @idFirm and id_tax = @inTax and id_type_doc = @idTypeDoc and year = @year )
    insert into last_nom(last_nom,id_firm,id_tax,id_type_doc,year)
    values(0, @idFirm,  @inTax, @idTypeDoc, @year)

declare @nextnom int = 0
select @nextnom = last_nom + 1
from last_nom
where id_firm = @idFirm and id_tax = @inTax and id_type_doc = @idTypeDoc and year = @year

update last_nom
set last_nom = @nextnom
where id_firm = @idFirm and id_tax = @inTax and id_type_doc = @idTypeDoc and year = @year

select @nextnom
";
            return DBConnector.DBExecutor.SelectSchalar(sqlNom, paridFirm, parinTax, paridTypeDoc, paryear).ToString();
        }

        internal static string GetIdDoc(object nomDoc, object idFirm, object inTax, object idTypeDoc)
        {    
            switch (int.Parse(idTypeDoc.ToString()))
            {
                case 188:
                    return "20b60 " + DateTime.Now.ToShortDateString() + " " + nomDoc.ToString() + " " + inTax.ToString() + " " + idFirm.ToString();
                case 189:
                    return "20b50 " + DateTime.Now.ToShortDateString() + " " + nomDoc.ToString() + " " + inTax.ToString() + " " + idFirm.ToString();
            }
            return null;
        }
        public static void GenerateFinDocBonus(object idDoc, object nomDoc, object idFirm, object inTax, object idTypeDoc, object idKontrDb, object idKontrKr,  object idConvention, decimal sum, string AgentName)
        {
            string sql = @"
insert into fin_doc
(
id_doc,date_doc,nom_doc,id_type_doc,id_status_doc,id_status_buh,id_cond_doc,id_kontr_db,id_kontr_kr,id_addition_db,id_addition_kr
,id_addition2_db,id_addition2_kr,in_tax,id_firm,sum_db,sum_kr,sum_f,id_cur_db,id_cur_kr,cur_rec_kr,cur_rec_db,id_doc_db,id_doc_kr
,status_buh,id_art,expenses_period,com,com_pr,sum_nds,pay_period,day_pay_must,ocher_pay,method,move,date_load
,com_gni,id_filial,id_filial_db,id_filial_kr,to_filial,date_sf,nom_sf,nom_sf2,text_for_sf,id_direct,in_link,id_kontr_for,presence_doc
,bonus,loading,id_project,id_trans,from_req,date_posting,id_filial_doc,fix_buh_date,KolHead,Sign,Date_Sign,OnDoc,DateDoc,NomDoc,StatusPayer
,ident_doc,idTerritory,TaxNds,TaxProfit,f1c,idConvention,idcard,idemployee,idStatusSbOnline
)
values
(
@idDoc, @dateDoc, @nomDoc, @idTypeDoc, 0, 0, 0, @idKontrDb, @idKontrKr, 0, 1
,0, 0, @inTax, @idFirm,  @sum, @sum, @sum, 0, 0, 1, 1, null, null
,0, 211, @dateDoc,  '"+ AgentName + @" Автогенерация корректировки баланса по бонусам', null, 0, null, null, null, null, 0, @dateDoc 
,null, 36, 0, 0, 0, null, null, 0, null, 60, 0, 0, 0
,0, 1, null, null, 0, @dateDoc, 36, 0, 0, 'no', null, null, null, null, null
,1, 1, 1, 1, 0, @idConvention, null, null, null
)
";
            //@AgentName + 
            //Автогенерация корректировки баланса по бонусам
            SqlParameter paridDoc = new SqlParameter("idDoc", idDoc);
            SqlParameter parnomDoc = new SqlParameter("nomDoc", nomDoc);
            SqlParameter parDateDoc = new SqlParameter("dateDoc", DateTime.Now.Date);
            SqlParameter paridFirm = new SqlParameter("idFirm", idFirm);
            //SqlParameter parAgentName = new SqlParameter("AgentName", AgentName);
            SqlParameter parinTax = new SqlParameter("inTax", inTax);
            SqlParameter paridTypeDoc = new SqlParameter("idTypeDoc", idTypeDoc);
            SqlParameter paridConvention = new SqlParameter("idConvention", idConvention);
            SqlParameter paridKontrDb = new SqlParameter("idKontrDb", idKontrDb);
            SqlParameter paridKontrKr = new SqlParameter("idKontrKr", idKontrKr);
            SqlParameter parsum = new SqlParameter("sum", sum);
            //SqlParameter parnUser = new SqlParameter("nUser", User.Current.NKontrFull);

            DBConnector.DBExecutor.ExecuteQuery(sql, paridDoc, parnomDoc, parDateDoc, paridFirm, parinTax, paridTypeDoc, paridConvention, paridKontrDb, paridKontrKr, parsum/*, parAgentName*/);
        }

        public static object GetidConventionBonus(object idKontr)
        {
            string sql = @"
select
    top 1 idConvention
from contract 
where id_kontr = @idKontr and fArkonaBonus = 1 and idStatusContract in (10, 20) 
";

            SqlParameter parIdKontr = new SqlParameter("idKontr", idKontr);
            return DBConnector.DBExecutor.SelectSchalar(sql, parIdKontr);
        }

        internal static object GetidConventionBonusFor(object idKontr, object idFirm)
        {
            string sql = @"

SELECT contract.idConvention
   FROM contract (nolock)
  WHERE contract.id_kontr = @idkontr and 
    contract.id_firm = @idFirm and
    contract.idstatuscontract in (10,20) and 
    lower(contract.fmodul) = 'sale'  and 
    contract.id_contract = 12  and 
    contract.fDefault = 1

";

            SqlParameter parIdKontr = new SqlParameter("idKontr", idKontr);
            SqlParameter paridFirm = new SqlParameter("idFirm", idFirm);
            return DBConnector.DBExecutor.SelectSchalar(sql, parIdKontr, paridFirm);
        }
    }
}
