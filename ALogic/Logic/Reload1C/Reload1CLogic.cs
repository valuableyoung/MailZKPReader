using ALogic.DBConnector;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ALogic.Logic.Reload1C
{
    public static class Reload1CLogic
    {
        public static DataTable GetDocs(Reload1CParams par)
        {
            DataTable result = null;
            if (par.FinDoc)
            {
                result = GetFinTov(par, "fin_doc");
            }

            if (par.TovDoc)
            {
                if (result != null)
                    result.Merge(GetFinTov(par, "tov_doc"));
                else
                    result = GetFinTov(par, "tov_doc");
            }

            return result;
        }

        private static DataTable GetFinTov(Reload1CParams par, string tbl)
        {
            string sql = @"
select
	 d.id_doc as idDoc
	,d.nom_doc as  NomDoc
	,d.date_doc as DateDoc
	,k1.n_kontr as nKontrDB
	,k2.n_kontr as nKontrKR
	,f.n_kontr as nFirm
    ,d.f1c as F1c
from " + tbl + @" d
	inner join spr_kontr k1 on k1.id_kontr = d.id_kontr_db
	inner join spr_kontr k2 on k2.id_kontr = d.id_kontr_kr
	inner join spr_kontr f on f.id_kontr = d.id_firm
where
    (d.in_tax <> 0 or d.id_firm = 553077) and d.id_status_doc = 30 and
    d.date_doc between @dates and @datee and d.id_firm = @idFirm and (d.id_type_doc = @idTypeDoc or @idTypeDoc = 0)

";

            SqlParameter pardateS = new SqlParameter("dates", par.DateS);
            SqlParameter pardateE = new SqlParameter("datee", par.DateE);
            SqlParameter paridFirm = new SqlParameter("idFirm", par.idFirm);
            SqlParameter paridTypeDoc = new SqlParameter("idTypeDoc", par.idTypeDoc);

            var t = DBConnector.DBExecutor.SelectTable(sql, pardateS, pardateE, paridFirm, paridTypeDoc);
            t.Columns.Add("CH", typeof(bool));

            foreach (var row in t.AsEnumerable())
                row["CH"] = true;

            return t;
        }

        public static void ReloadChecked(int idFirm, DataTable tbl)
        {
            SqlParameter parIdFirm = new SqlParameter("idFirm", idFirm);
            DBConnector.DBExecutor.ExecuteQuery("exec up_Set1cAllLoad @idFirm", parIdFirm);

            foreach (var row in tbl.AsEnumerable().Where(p => bool.Parse(p["CH"].ToString())).ToList())
            {
                string id_doc = row["idDoc"].ToString();
                SetDocLoad(id_doc, 0);              
            }

            Reload(true, idFirm, tbl.AsEnumerable().Where(p => bool.Parse(p["CH"].ToString())).Select(p => p["idDoc"].ToString()).ToList());
        }

        private static void Reload(bool fHand, int idFirm, List<string> listidDoc)
        {
            var dtfinDoc = GetFinDoc(idFirm);
            UniLogger.WriteLog("", 0, "метод GetFinDoc выполнен");
            var tdTovDoc = GetTovDoc(idFirm);
            UniLogger.WriteLog("", 0, "метод GetTovDoc выполнен");

            if (!fHand)
            {
                DateTime dateStart = DateTime.Parse(DBConnector.DBAppParam.GetAppParamYN(455).ToString());
                int dateInt = dateStart.Year * 10000 + dateStart.Month * 100 + dateStart.Day;

                foreach (var row in dtfinDoc.AsEnumerable())
                    if (int.Parse(row["date_doc"].ToString()) < dateInt)
                        row.Delete();

                foreach (var row in tdTovDoc.AsEnumerable())
                    if (int.Parse(row["date_doc"].ToString()) < dateInt)
                        row.Delete();               
            }
            else
            {
                foreach (var row in dtfinDoc.AsEnumerable())
                {
                    if (listidDoc.IndexOf(row["id_doc"].ToString()) == -1)
                        row.Delete();
                }

                foreach (var row in tdTovDoc.AsEnumerable())
                {
                    if (listidDoc.IndexOf(row["id_doc"].ToString()) == -1)
                        row.Delete();
                }
            }

            DataTable tDoc = new DataTable();
            tDoc.Columns.Add("idDoc", typeof(string));
            
            foreach(var idDoc in tdTovDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Select(p=>p["id_doc"].ToString()).Distinct().ToList())
            {
                tDoc.Rows.Add(idDoc);
            }

            foreach(var idDoc in dtfinDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Select(p => p["id_doc"].ToString()).Distinct().ToList())
            {
                tDoc.Rows.Add(idDoc);
            }

            var dtTov = GetTov(tDoc);
            dtTov.DataSet.DataSetName = "spr_tov";
            dtTov.TableName = "row";

            var dtDecl = GetDeclaration(tDoc);
            dtDecl.DataSet.DataSetName = "gtd";
            dtDecl.TableName = "row";

            var dtKontr = GetKontr(tDoc);
            dtKontr.DataSet.DataSetName = "spr_kontr";
            dtKontr.TableName = "row";

            var dtBank = GetKontrBank(tDoc);
            dtBank.DataSet.DataSetName = "kontr_bank";
            dtBank.TableName = "row";

            var dtContract = GetContracts(tDoc);
            dtContract.DataSet.DataSetName = "contract";
            dtContract.TableName = "contract_row";

            var way = DBConnector.DBExecutor.SelectSchalar("select top 1 WayTo1cFile from DetailOurFirm where idFirm = @idFirm ", new SqlParameter("idFirm", idFirm));

            if (dtKontr.Rows.Count > 0)
            {
                dtKontr.WriteXml(way + @"\a_spr_kontr " + DateTime.Now.Ticks.ToString() + ".xml");
                UniLogger.WriteLog("", 0, "a_spr_kontr сформирован");
            }

            if (dtTov.Rows.Count > 0)
            {
                dtTov.WriteXml(way + @"\c_spr_tov " + DateTime.Now.Ticks.ToString() + ".xml");
                UniLogger.WriteLog("", 0, "c_spr_kontr сформирован");
            }

            if (dtDecl.Rows.Count > 0)
            {
                dtDecl.WriteXml(way + @"\bz_spr_decl " + DateTime.Now.Ticks.ToString() + ".xml");
                UniLogger.WriteLog("", 0, "bz_spr_decl сформирован");
            }

            if (dtBank.Rows.Count > 0)
            {
                dtBank.WriteXml(way + @"\b_kontr_bank " + DateTime.Now.Ticks.ToString() + ".xml");
                UniLogger.WriteLog("", 0, "b_kontr_bank сформирован");
            }

            if (dtContract.Rows.Count > 0)
            {
                dtContract.WriteXml(way + @"\d_spr_contract " + DateTime.Now.Ticks.ToString() + ".xml");
                UniLogger.WriteLog("", 0, "d_spr_contract сформирован");
            }

            if (dtfinDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Count() > 0)
            {
                dtfinDoc.DataSet.DataSetName = "fin_doc";
                dtfinDoc.TableName = "row";
                dtfinDoc.WriteXml(way + @"\f_fin_doc " + DateTime.Now.Ticks.ToString() + ".xml");
                UniLogger.WriteLog("", 0, "f_fin_doc сформирован");
            }

            if (tdTovDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Count() > 0)
            {
                int countRow = 0;
                var listIdDoc = tdTovDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Select(p => p["id_doc"].ToString()).Distinct().ToList();
                DataTable dttemp = tdTovDoc.Clone();
                foreach (var idDoc in listIdDoc)
                {
                    foreach(var row in tdTovDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted && p["id_doc"].ToString() == idDoc).ToList())
                    {
                        dttemp.Rows.Add(row.ItemArray);
                    }
                    countRow++;

                    if (countRow >= 100)
                    {
                        DataSet dsTovDoc = GetTovDocDs(dttemp);
                        dsTovDoc.WriteXml(way + @"\e_tov_doc " + DateTime.Now.Ticks.ToString() + ".xml");
                        countRow = 0;
                        dttemp.Clear();
                        UniLogger.WriteLog("", 0, "e_tov_doc сформирован, 100 records");
                    }
                }
                
                if (dttemp.Rows.Count > 0)
                {
                    DataSet dsTovDoc = GetTovDocDs(dttemp);
                    dsTovDoc.WriteXml(way + @"\e_tov_doc " + DateTime.Now.Ticks.ToString() + ".xml");
                    UniLogger.WriteLog("", 0, "e_tov_doc сформирован");
                }
            }           

            foreach (var idDoc in tdTovDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Select(p => p["id_doc"]).Distinct())
            {
                SetDocLoad(idDoc, 1);
            }

            foreach (var idDoc in dtfinDoc.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Select(p => p["id_doc"]).Distinct())
            {
                SetDocLoad(idDoc, 1);
            }
        }

        private static void SetDocLoad(object idDoc, int idLoad)
        {
            string sql = @"
update tov_doc set f1c = @idLoad where id_doc = @id_doc
update fin_doc set f1c = @idLoad where id_doc = @id_doc 
";
            SqlParameter paridIdDoc = new SqlParameter("id_doc", idDoc);
            SqlParameter paridIdLoad = new SqlParameter("idLoad", idLoad);
            DBConnector.DBExecutor.ExecuteQuery(sql, paridIdDoc, paridIdLoad);
        }

        private static DataSet GetTovDocDs(DataTable tbl)
        {
            var ds = new DataSet();
            ds.ReadXmlSchema(@"tov_doc.xml");

            int id_row = -1;
            foreach (var id_doc in tbl.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted).Select(p => p["id_doc"].ToString()).Distinct().ToList())
            {
                var detail = tbl.AsEnumerable().Where(p => p["id_doc"].ToString() == id_doc.ToString()).ToList();
                id_row++;

                var newRow = ds.Tables[0].NewRow();
                newRow["id_doc"] = detail[0]["id_doc"];
                newRow["nom_doc"] = detail[0]["nom_doc"] == DBNull.Value ? "" : detail[0]["nom_doc"];
                newRow["date_doc"] = detail[0]["date_doc"] == DBNull.Value ? "" : detail[0]["date_doc"];
                newRow["sf"] = detail[0]["sf"] == DBNull.Value ? "" : detail[0]["sf"];
                newRow["id_kontr"] = detail[0]["id_kontr"] == DBNull.Value ? "" : detail[0]["id_kontr"];
                newRow["nom_doc_kontr"] = detail[0]["nom_doc_kontr"] == DBNull.Value ? "" : detail[0]["nom_doc_kontr"];
                newRow["date_doc_kontr"] = detail[0]["date_doc_kontr"] == DBNull.Value ? "" : detail[0]["date_doc_kontr"];
                newRow["nom_sf_kontr"] = detail[0]["nom_sf_kontr"] == DBNull.Value ? "" : detail[0]["nom_sf_kontr"];
                newRow["date_sf_kontr"] = detail[0]["date_sf_kontr"] == DBNull.Value ? "" : detail[0]["date_sf_kontr"];
                newRow["id_firm"] = detail[0]["id_firm"] == DBNull.Value ? "" : detail[0]["id_firm"];
                newRow["id_cur"] = detail[0]["id_cur"] == DBNull.Value ? "" : detail[0]["id_cur"];
                newRow["cur_rec"] = detail[0]["cur_rec"] == DBNull.Value ? "" : detail[0]["cur_rec"];
                newRow["id_sklad"] = detail[0]["id_sklad"] == DBNull.Value ? "" : detail[0]["id_sklad"];
                newRow["dogovor"] = detail[0]["dogovor"] == DBNull.Value ? "" : detail[0]["dogovor"];
                newRow["idconvention"] = detail[0]["idconvention"] == DBNull.Value ? "" : detail[0]["idconvention"];
                newRow["idtypecontract"] = detail[0]["idtypecontract"] == DBNull.Value ? "" : detail[0]["idtypecontract"];
                newRow["type_doc"] = detail[0]["type_doc"] == DBNull.Value ? "" : detail[0]["type_doc"];
                newRow["type_operation"] = detail[0]["type_operation"] == DBNull.Value ? "" : detail[0]["type_operation"];
                newRow["action"] = detail[0]["action"] == DBNull.Value ? "" : detail[0]["action"];
                newRow["commentary"] = detail[0]["commentary"] == DBNull.Value ? "" : detail[0]["commentary"];
                newRow["schet_rasch"] = detail[0]["schet_rasch"] == DBNull.Value ? "" : detail[0]["schet_rasch"];
                newRow["schet_avans"] = detail[0]["schet_avans"] == DBNull.Value ? "" : detail[0]["schet_avans"];
                newRow["schet_pretenz"] = detail[0]["schet_pretenz"] == DBNull.Value ? "" : detail[0]["schet_pretenz"];
                newRow["schet_dohod"] = detail[0]["schet_dohod"] == DBNull.Value ? "" : detail[0]["schet_dohod"];
                newRow["schet_rashod"] = detail[0]["schet_rashod"] == DBNull.Value ? "" : detail[0]["schet_rashod"];
                newRow["st_dohod_rashod"] = detail[0]["st_dohod_rashod"] == DBNull.Value ? "" : detail[0]["st_dohod_rashod"];
                newRow["idsklad_kr"] = detail[0]["idsklad_kr"] == DBNull.Value ? "" : detail[0]["idsklad_kr"];
                newRow["idsklad_db"] = detail[0]["idsklad_db"] == DBNull.Value ? "" : detail[0]["idsklad_db"];
                newRow["schet_sklad"] = detail[0]["schet_sklad"] == DBNull.Value ? "" : detail[0]["schet_sklad"];
                newRow["schet_cash"] = detail[0]["schet_cash"] == DBNull.Value ? "" : detail[0]["schet_cash"];
                newRow["id_docsale"] = (detail[0]["iddocsale"] == DBNull.Value ? "" : detail[0]["iddocsale"]).ToString().Trim();
                newRow["row_Id"] = id_row;

                ds.Tables[0].Rows.Add(newRow);

                var newRowC = ds.Tables[1].NewRow();
                newRowC["doc_table_rows_Id"] = id_row;
                newRowC["row_Id"] = id_row;

                ds.Tables[1].Rows.Add(newRowC);

                foreach (var row in detail)
                {
                    var rowData = ds.Tables[2].NewRow();
                    rowData["id_tov"] = row["id_tov"];
                    rowData["sebest"] = row["sebest"] == DBNull.Value ? "" : row["sebest"];
                    rowData["summa"] = Math.Round(decimal.Parse(row["kol_tov"].ToString()) * decimal.Parse(row["price_f"].ToString()), 2);
                    rowData["kol_tov"] = row["kol_tov"];
                    rowData["per_nds"] = row["per_nds"];
                    rowData["n_type_pay"] = row["n_type_pay"];
                    rowData["country"] = row["made_in"];
                    rowData["GTD"] = row["declaration"];
                    rowData["doc_table_rows_Id"] = id_row;

                    ds.Tables[2].Rows.Add(rowData);
                }
            }

            return ds;
        }

        public static string LoadAll()
        {
            var listFirm = DBConnector.DBExecutor.SelectTable("select idFirm from DetailOurFirm where f1cnew = 1").AsEnumerable().Select(p => int.Parse(p["idFirm"].ToString())).ToList();

            foreach (var idFirm in listFirm)
            {
                SqlParameter parIdFirm = new SqlParameter("idFirm", idFirm);
                DBConnector.DBExecutor.ExecuteQuery("exec up_Set1cAllLoad @idFirm", parIdFirm);
                UniLogger.WriteLog("", 0, "up_Set1cAllLoad выполнена");
                Reload(false, idFirm, null);
                UniLogger.WriteLog("", 0, "LoadAll Reload по фирме " + idFirm.ToString() + " выполнена");
            }

            return "Выгрузка в 1с завершена";
        }


        public static DataTable GetDeclaration( DataTable tDoc )
        {
            string str = @"
select declaration as declaration, declaration as regnum from
(
	select distinct declaration from tov_in (nolock) where declaration is not null and rtrim(declaration)<>'' 
            and id_doc in (select idDoc from @tDoc)
	union 
	select distinct declaration from spr_tov (nolock) where declaration is not null and rtrim(declaration )<>'' 
                    and id_tov in (select id_tov from tov_in (nolock) where id_doc in (select idDoc from @tDoc))
) t
";
            SqlParameter par = new SqlParameter("tDoc", SqlDbType.Structured);
            par.Value = tDoc;
            par.TypeName = "listIdDoc";

            return DBConnector.DBExecutor.SelectTable(str, par);
        }

        public static DataTable GetFinDoc(int idFirm)
        {
            string str = @"
select 
    id_doc, date_doc, id_kontr, id_firm, dogovor, idconvention,  idtypecontract, summa, id_cur, isnull(commentary, '') as commentary, action , type_doc, type_operation,
    cur_rec, sdds, print_from, print_osnov, print_priloj , schet_uch, isnull(schet, '') as schet, schet_rasch, isnull(schet_avans, '') as schet_avans, bank_account, isnull(cast(cur_cb as varchar(10)) ,'' ) as cur_cb,
    ext_date as extdate, ext_num as extnum, pay_purp as paypurp
from uf_Unload1c_findoc(@idFirm,@dateS,@dateE)
order by type_doc, type_operation, action desc, date_doc, id_doc
";
            SqlParameter pardateS = new SqlParameter("dateS", DateTime.Now.AddYears(-5));
            SqlParameter pardateE = new SqlParameter("dateE", DateTime.Now.AddYears(5));
            SqlParameter paridFirm = new SqlParameter("idFirm", idFirm);

            return DBConnector.DBExecutor.SelectTable(str, pardateS, pardateE, paridFirm);
        }

        public static DataTable GetTovDoc(int idFirm)
        {
            string str = @"
select * from uf_Unload1c_tovdoc(@idFirm,@dateS,@dateE) 
ORDER BY date_doc, ordernum, type_doc, type_operation, action desc, id_doc, nom_doc, id_tov
";
            SqlParameter pardateS = new SqlParameter("dateS", DateTime.Parse("01.01.2019"));
            SqlParameter pardateE = new SqlParameter("dateE", DateTime.Now.Date);
            SqlParameter paridFirm = new SqlParameter("idFirm", idFirm);

            return DBConnector.DBExecutor.SelectTable(str, pardateS, pardateE, paridFirm);
        }

        public static DataTable GetContracts(DataTable tDoc)
        {
            string str = @"
              SELECT 
			max(contract.idConvention) idconvention,   
         contract.id_firm,   
         contract.id_kontr,   
			contract.nom_contract,
			convert(char(10),contract.date_contract,112) as datecontract, 
			case contract.idstatuscontract
				when -10 then
					convert(char(10), v_today.tdate,112)
				when 40 then
					convert(char(10), v_today.tdate,112)
				else
					'00010101'
			end as dateendcontract,
			643 as idcur,
			case contract.id_contract
				when 1 then
					case when contract.id_firm in (550861, 551942) and  contract.id_kontr = 554699 then 'сКомиссионером' else 'сПокупателем' end
				when 2 then
					'сКомиссионером'
				when 3 then
					case when contract.id_firm = 554699 then 'сКомитентом' else 'сПоставщиком' end
				when 4 then
					'сКомитентом'
				when 6 then
					case when contract.id_firm in (550861, 551942) and  contract.id_kontr = 554699 then 'сКомиссионером' else 'сПокупателем' end
				when 8 then
					case when contract.id_firm in (550861, 551942) and  contract.id_kontr = 554699 then 'сКомиссионером' else 'сПокупателем' end
				when 11 then
					case when contract.id_firm = 554699 then 'сКомитентом' else 'сПоставщиком' end
				when 12 then
					case when contract.id_firm in (550861, 551942) and  contract.id_kontr = 554699 then 'сКомиссионером' else 'сПокупателем' end
			end typecontract,			
			'' as commentary		
    FROM contract (nolock),
			v_today    
	WHERE
        contract.idConvention in 
        (
            select idConvention from tov_doc (nolock) where id_doc in (select idDoc from @tDoc)
            union
            select idConvention from fin_doc (nolock) where id_doc in (select idDoc from @tDoc)
        )
group by contract.id_firm,   
         contract.id_kontr,   
			contract.nom_contract,
			convert(char(10),contract.date_contract,112),
			contract.idStatusContract,
			contract.DateEndContract,
			v_today.tdate,
			contract.id_contract,
			ltrim(IsNull(contract.commentaryShort,'') + '  ' + IsNull(contract.commentary,''))
";
            SqlParameter par = new SqlParameter("tDoc", SqlDbType.Structured);
            par.Value = tDoc;
            par.TypeName = "listIdDoc";

            return DBConnector.DBExecutor.SelectTable(str, par);
        }

        public static DataTable GetKontr( DataTable tDoc )
        {
            string str = @"
              SELECT spr_kontr.id_kontr as id_kontr,   
         spr_kontr.n_kontr as n_kontr,   
         spr_kontr.n_kontr_full as n_kontr_full,           
         spr_kontr.adress_ur as adress_ur,   
         spr_kontr.adress_fact as adress_fact,   
         spr_kontr.inn as inn,  
         isnull(spr_kontr.kpp, '') as kpp,   	 
         isnull(spr_kontr.okpo, '') as okpo,   			
         case when spr_kontr.faice = 'f' then 'ФизическоеЛицо' else 'ЮридическоеЛицо' end  as  faice,    
         0 as holding,
			case spr_kontr.employer when 1 then 'Сотрудники' else
				case spr_kontr.retail when 1 then 'Клиенты розницы' else					
				    case when spr_kontr.seller_bayer = 20 and (spr_kontr.supplier = 1 or spr_kontr.supplierservice = 1) then 'Покупатель/ Поставщик'
					     when spr_kontr.seller_bayer = 30 then 'Покупатель/ Поставщик'
					     when spr_kontr.seller_bayer = 20 and spr_kontr.supplier = 0 and spr_kontr.supplierservice = 0 then 'Покупатели'
					     when spr_kontr.seller_bayer <> 20 and spr_kontr.supplier = 1  then 'Поставщики'
					     when spr_kontr.seller_bayer <> 20 and spr_kontr.supplierservice = 1 then 'Поставщик хозяйственный'
					     when spr_kontr.seller_bayer = 10 and spr_kontr.supplier = 0 and spr_kontr.supplierservice = 0 then 'Поставщики'
				    else 
					    'Ни покупатель/ Ни поставщик'
				end 	
			end
		end groupkontr,
			0 isdeleted  
    FROM spr_kontr (nolock)
   WHERE
      spr_kontr.bank = 0
 	  and spr_kontr.id_kontr > 0 
    and ((spr_kontr.id_cond) < 30 or (spr_kontr.id_cond = 30))
    and id_kontr in 
            (
                select id_kontr_db from tov_doc (nolock) where id_doc in ( select idDoc from @tDoc )
                union 
                select id_kontr_kr from tov_doc (nolock) where id_doc in  ( select idDoc from @tDoc )
                union 
                select id_kontr_db from fin_doc (nolock) where id_doc in  ( select idDoc from @tDoc )
                union 
                select id_kontr_kr from fin_doc (nolock) where id_doc in  ( select idDoc from @tDoc )
            )
   order by id_kontr
";

            SqlParameter par = new SqlParameter("tDoc", SqlDbType.Structured);
            par.Value = tDoc;
            par.TypeName = "listIdDoc";

            return DBConnector.DBExecutor.SelectTable(str, par);
        }

        public static DataTable GetKontrBank( DataTable tDoc )
        {
            string str = @"
 SELECT 
         kontr_bank.id_kontr,  
         kontr_bank.designed_score,   
         spr_kontr.okonh,  
         643 as id_cur,     
         'расчетный' as   account_type,
         case kontr_bank.use_def when 1 then 'Основной расчетный счет' else 'Расчетный счет' end predstavl,
         kontr_bank.id_addition    			
    FROM kontr_bank (nolock),   
         spr_kontr (nolock) 
   WHERE spr_kontr.id_kontr = kontr_bank.id_bank
		and spr_kontr.id_kontr in
(
                select id_kontr_db from tov_doc (nolock) where id_doc in ( select idDoc from @tDoc )
                union 
                select id_kontr_kr from tov_doc (nolock) where id_doc in  ( select idDoc from @tDoc )
                union 
                select id_kontr_db from fin_doc (nolock) where id_doc in  ( select idDoc from @tDoc )
                union 
                select id_kontr_kr from fin_doc (nolock) where id_doc in  ( select idDoc from @tDoc )
)
            ";

            SqlParameter par = new SqlParameter("tDoc", SqlDbType.Structured);
            par.Value = tDoc;
            par.TypeName = "listIdDoc";

            return DBConnector.DBExecutor.SelectTable(str, par);
        }

        public static DataTable GetTov( DataTable tDoc )
        {
            string str = @"
  SELECT  spr_tov.id_tov as id_tov,   
         case spr_tov.id_level when 3 then 0 else 1 end as is_group,   
         spr_tov.id_top_level as top_level,   
         case len(IsNull(spr_tov.n_tov,'')) when 0 then 'Не заполнено' else spr_tov.n_tov end as n_tov,           
         case when spr_tov.per_nds = 0.2 then 'Общая'  when spr_tov.per_nds = 0.1 then 'Пониженная' else 'Нулевая' end as per_nds,
         isnull(spr_country.n_country, '') as country,     
         'шт'  as measure,
			case spr_tov.pExcise when 1 then 'Товар акцизный' when 0 then 'Товар безакцизный' else '' end pexcise,
			case spr_tov.pExcise when 1 then '90.01.1' when 0 then '90.01.2' else '' end acc_db,
  			case spr_tov.pExcise when 1 then '90.02.1' when 0 then '90.02.2' else '' end acc_kr,
			case spr_tov.id_level when 3 then 'Материальные расходы' else '' end         st_zatr,		
		isnull(spr_tov.id_tov_oem_short, '') as artikul_oem
    FROM spr_tov (nolock) left outer join  spr_country   (nolock)
            on spr_tov.made_in = spr_country.id_country 
   WHERE spr_tov.id_tov > 0 
    and spr_tov.price_incl < 60
	 and spr_tov.id_tov in 
                (
                    select id_tov from tov (nolock) where id_doc in (select idDoc from @tDoc)
                    union
                    select id_tov from tov_in (nolock) where id_doc in (select idDoc from @tDoc)
                )
";
            SqlParameter par = new SqlParameter("tDoc", SqlDbType.Structured);
            par.Value = tDoc;
            par.TypeName = "listIdDoc";

            return DBConnector.DBExecutor.SelectTable(str, par);
        }

        public static string strResult;

        public static void SendEmailToDeveloper(string email, string HtmlMessage, string datefrom)
        {
            try
            {
                MailAddress from = new MailAddress("Developers@arkona36.ru");
                MailMessage m = new MailMessage();

                m.To.Add(email);

                m.Bcc.Add("muhinan@arkona36.ru");

                m.From = from;
                m.Subject = "Ошибка при автоматической перевыгрузке в 1С " + datefrom + ".";
                m.Body = HtmlMessage;

                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(ProjectProperty.MailServer, 587);
                smtp.Credentials = new NetworkCredential(ProjectProperty.MailServer, ProjectProperty.MailUserPriceParserPassword);
                smtp.EnableSsl = false;
                smtp.Send(m);
            }
            catch {
                UniLogger.WriteLog("", 1, "письмо на developers отправить не удалось");
            }
        }

        public static void FullLoad()
        {
            string CurrentStage = "";
            int errCnt = 0;
            try
            {
                int val = int.Parse(ALogic.DBConnector.DBAppParam.GetAppParamValue(435).ToString());

                strResult = "";
                

                if (val != 0)
                {
                    AddMessage("Выгрузка в 1с уже ведется!");
                    UniLogger.WriteLog("", 0, "Выгрузка в 1с уже ведется!");
                    return;
                }
                AddMessage("Выставляем параметр в 1, что выгрузка в 1С уже ведется");
                UniLogger.WriteLog("", 0, "Выставляем параметр в 1, что выгрузка в 1С уже ведется");
                ALogic.DBConnector.DBAppParam.SetAppParamValue(435, 1);
                CurrentStage = "Установка параметра выгрузки в 1";


                AddMessage("Начало выгрузки в 1С");
                UniLogger.WriteLog("", 0, "Начало выгрузки в 1С");
                CurrentStage = "Начало выгрузки - выполнение up_RepurchaseGroup";

                string sqlRepurchase = "exec up_RepurchaseGroup";
                DBConnector.DBExecutor.ExecuteQuery(sqlRepurchase);

                AddMessage("Перезакупки создались, up_RepurchaseGroup выполнена");
                UniLogger.WriteLog("", 0, "Перезакупки создались, up_RepurchaseGroup выполнена");

                CurrentStage = "Проведение закупки 1й этап";
                Process prZak1 = Process.Start(@"D:\Base1C\ex\4Test\bay.exe", "DESIGNDOC");

                AddMessage(@"Запущен процесс D:\Base1C\ex\4Test\bay.exe DESIGNDOC");
                UniLogger.WriteLog("", 0, @"Запущен процесс D:\Base1C\ex\4Test\bay.exe DESIGNDOC");
                prZak1.WaitForExit();

                if (!prZak1.HasExited)
                {
                    AddMessage("Проведение перезакупок №1 не удалось");
                    UniLogger.WriteLog("", 0, "Проведение перезакупок №1 не удалось");
                    return;
                }
                CurrentStage = "Проведение продажи 1й этап";
                Process prZak2 = Process.Start(@"D:\Base1C\ex\4Test\sale.exe", "DESIGNDOC");

                AddMessage(@"Запущен процесс D:\Base1C\ex\4Test\sale.exe DESIGNDOC");
                UniLogger.WriteLog("", 0, @"Запущен процесс D:\Base1C\ex\4Test\sale.exe DESIGNDOC");
                prZak2.WaitForExit();

                if (!prZak2.HasExited)
                {
                    AddMessage("Проведение перезакупок №2 не удалось");
                    UniLogger.WriteLog("", 0, "Проведение перезакупок №2 не удалось");
                    return;
                }

                CurrentStage = "Проведение закупки 2й этап";
                Process prZak3 = Process.Start(@"D:\Base1C\ex\4Test\bay.exe", "WORKDOC");

                AddMessage(@"Запущен процесс D:\Base1C\ex\4Test\bay.exe WORKDOC");
                UniLogger.WriteLog("", 0, @"Запущен процесс D:\Base1C\ex\4Test\bay.exe WORKDOC");
                prZak3.WaitForExit();

                if (!prZak3.HasExited)
                {
                    AddMessage("Проведение перезакупок №3 не удалось");
                    UniLogger.WriteLog("", 0, "Проведение перезакупок №3 не удалось");
                    return;
                }

                CurrentStage = "Проведение продажи 2й этап";
                Process prZak4 = Process.Start(@"D:\Base1C\ex\4Test\sale.exe", "WORKDOC");

                AddMessage(@"Запущен процесс D:\Base1C\ex\4Test\sale.exe WORKDOC");
                UniLogger.WriteLog("", 0, @"Запущен процесс D:\Base1C\ex\4Test\sale.exe WORKDOC");
                prZak4.WaitForExit();

                if (!prZak4.HasExited)
                {
                    AddMessage("Проведение перезакупок №4 не удалось");
                    UniLogger.WriteLog("", 0, "Проведение перезакупок №4 не удалось");
                    return;
                }

                CurrentStage = "Сохранение документов в XML";
                AddMessage("Начинаем сохранять документы в XML");
                UniLogger.WriteLog("", 0, "Начинаем сохранять документы в XML");
                UniLogger.WriteLog("", 0, LoadAll());
                ALogic.DBConnector.DBAppParam.SetAppParamValue(435, 0);

                //string path = @"D:\Base1C\ex\FirmAll\Log\log" + DateTime.Now.Ticks + ".txt";
                //File.WriteAllText(path, strResult);
            }
            catch (Exception ex)
            {
                errCnt++;
                //string path = @"D:\Base1C\ex\FirmAll\Log\log" + DateTime.Now.Ticks + ".txt";
                UniLogger.WriteLog("", 1, ex.Message);
                SendEmailToDeveloper("developers@arkona36.ru", "Этап: " + CurrentStage  + "\r\n Ошибка при автоматической перевыгрузке в 1С: \r\n" + ex.Message, DateTime.Today.ToString("d"));
                //WriteLogMessage(strResult);
                //File.WriteAllText(path, strResult);
            }
            finally
            {
                CurrentStage = "Завершение работы, сброс параметра в 0";
                //неважно - все закончили или нет - ФЛАГ НАДО ОСВОБОЖДАТЬ!
                ALogic.DBConnector.DBAppParam.SetAppParamValue(435, 0);
                UniLogger.WriteLog("", 0, "Сбрасываем параметр в 0, закончили");
                UniLogger.Flush();
                //if (errCnt == 0) { };
            }
        }

        public static void WriteLogMessage(string s)
        {
            var wayToFile = Directory.GetCurrentDirectory() + @"\log\log" + DateTime.Now.Ticks + ".txt";
            using (StreamWriter sw = new StreamWriter(wayToFile))
            {
                sw.WriteLine(s + "\r\n");
            }
        }

        private static void AddMessage(string p)
        {
            strResult += DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " : " + p + "\r\n";
        }
    }

    public static class UniLogger
    {
        private static BlockingCollection<string> _blockingCollection;
        private static string _filename = Directory.GetCurrentDirectory() + @"\log\log" + DateTime.Now.ToString("dd MM yy HH mm ss") + ".txt";
        private static Task _task;

        static UniLogger()
        {
            _blockingCollection = new BlockingCollection<string>();

            _task = Task.Factory.StartNew(() =>
            {
                using (var streamWriter = new StreamWriter(_filename, true, Encoding.UTF8))
                {
                    streamWriter.AutoFlush = true;

                    foreach (var s in _blockingCollection.GetConsumingEnumerable())
                        streamWriter.WriteLine(s);
                }
            },
            TaskCreationOptions.LongRunning);
        }

        public static void WriteLog(string action, int errorCode, string errorDescription)
        {
            //_blockingCollection.Add($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")} действие: {action}, код: {errorCode.ToString()}, описание: { errorDiscription} ");
            _blockingCollection.Add(@"[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + "] тип: " + errorCode.ToString() + " сообщение: " + errorDescription);
        }

        public static void Flush()
        {
            _blockingCollection.CompleteAdding();
            _task.Wait();
        }
    }
}
