using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.ADiadoc
{
    public static class DBDiadoc
    {
        public static DataRow GetDocHeader(object idDoc)
        {
            string query =
@"
            SELECT  tov_doc.nom_sf as NomSF,
                    tov_doc.nom_sf2 as NomSF2,
                    tov_doc.date_sf as DateSF,
            	     seller.inn as SellerInn,
            	     seller.kpp as SellerKpp,
            	     consignee.inn as ConsigneeInn,
            	     consignee.kpp as ConsigneeKpp,
            	     buyer.inn as  BuyerInn,
            	     buyer.kpp as   BuyerKpp,
            	     tov_doc.nom_doc as NomDoc,
            	     tov_doc.date_doc as DateDoc,
            	     tov_doc.id_cur as IdCur, 
            	     tov_doc.cur_rec as RateCur, 
            	     tov_doc.id_kontr_for as IdKontrFor, 
                     isnull(Shipper.inn, '0') as ShipperInn,
                     isnull(Shipper.kpp, '0') as ShipperKpp,
              	 case when contract.comentforupd IS null then ' Договор № ' + contract.nom_contract + ' от ' + convert(varchar(10),contract.date_contract,104) + case when tov_doc.OrderFromCustomer is null or tov_doc.OrderFromCustomer = '' then '' else '; Заказ №' + tov_doc.OrderFromCustomer end  else contract.comentforupd end as TransferBase 
            FROM tov_doc with (nolock)
              inner join  spr_kontr seller with (nolock)
            	    on tov_doc.id_firm = seller.id_kontr
              left join spr_kontr consignee with (nolock)
            	    on (tov_doc.id_kontr_for = 0 and consignee.id_kontr = tov_doc.id_kontr_db )
            	    or (tov_doc.id_kontr_for > 0	and consignee.id_kontr = tov_doc.id_kontr_for) 
              inner join spr_kontr buyer with (nolock)
            	    on tov_doc.id_kontr_db = buyer.id_kontr 
           	inner join contract with (nolock) 
           	    on tov_doc.idconvention = contract.idconvention 
            left join spr_kontr Shipper (nolock) on Shipper.id_kontr = tov_doc.idShipper

            WHERE tov_doc.id_doc = @idDoc 
";

            SqlParameter idDocPar = new SqlParameter("idDoc", idDoc);
            return DBExecutor.SelectRow(query, idDocPar);
        }

        public static DataRow GetDocKorInfo(object idDoc)
        {
            string query = @"
select
    top 1 nom_sf  as NomSF
,   nom_sf2 as NomSF2
,   date_sf as DateSF
from
    tov_doc
        inner join tov on tov_doc.id_doc = tov.idDocSale
where tov.id_doc = @id_doc
";
            SqlParameter idDocPar = new SqlParameter("id_doc", idDoc);
            return DBExecutor.SelectRow(query, idDocPar);
        }
                
        public static DataTable GetDocItems(object idDoc)
        {
            StringBuilder query = new StringBuilder();
            string sql = @"
SELECT
	 case when exists(select * from DetailOurFirm where idFirm = tov_doc.id_firm and fUseNtov2 = 1) then spr_tov.n_tov2 else spr_tov.n_tov end as nTov
	,isnull(sUnit.nUnitShort,'шт.') as nUnitShort
    ,case when tov_doc.in_tax = 11 and isnull(fMarketAgreement,0) = 1 then tov.PriceRec else tov.price_f end as Price 
    ,sum(tov.kol) as  Kol
    ,isnull(tov.MadeInCheck, 0) as  MadeIn
    ,isnull(tov.GTDcheck, '') as Declaration
    ,tov.per_nds as Nds
    ,case when isnull(os.idtovoemshort,'0') = '0' then  spr_tov.id_tov_oem_short else os.idtovoemshort end Oem 
  FROM tov_doc with (nolock)
      inner join tov with (nolock) on tov_doc.id_doc = tov.id_doc
      inner join contract with (nolock) on tov_doc.idconvention = contract.idconvention
      inner join spr_tov with (nolock) on spr_tov.id_tov = tov.id_tov
      left join sUnit with (nolock) on sUnit.idunit = spr_tov.idunit     
      left outer join ( select idrowsale, idtovoemshort from OrderSaleTov (nolock) ) os on tov.idrowsale = os.idrowsale 
WHERE tov_doc.id_doc = @idDoc 
GROUP BY tov_doc.id_firm
		,spr_tov.n_tov
		,sUnit.nUnitShort
		,tov_doc.in_tax
		,fMarketAgreement
		,tov.PriceRec
		,tov.price_f
		,spr_tov.made_in
		,tov.id_tov
		,tov_doc.id_direct
		,tov.per_nds
		,tov_doc.date_doc
		,os.idtovoemShort 
		,spr_tov.id_tov_oem_short
		,spr_tov.n_tov2
		,tov.MadeInCheck
		,tov.GTDcheck
";

            SqlParameter idDocPar = new SqlParameter("idDoc", idDoc);
            return DBExecutor.SelectTable(sql, idDocPar);
        }

        public static DataTable GetDocCorrItems(object idDoc)
        {
            string sql = @"
select 
	  spr_tov.n_tov as NTov
	 ,tb.kol as oldKol
	 ,tv.kol + tb.kol as newKol	
     ,tb.per_nds as Nds
     ,case when tov_doc.in_tax = 11 and isnull(fMarketAgreement,0) = 1 then tb.PriceRec else tb.price_f end as Price
from	
	tov tv
		inner join spr_tov on tv.id_tov = spr_tov.id_tov
		inner join tov tb on tv.idDocSale = tb.id_doc and tv.id_tov = tb.id_tov and tv.id_batch = tb.id_batch and tv.id_doc_in = tb.id_doc_in and tv.IdRowSale = tb.IdRowSale
        inner join tov_doc on tov_doc.id_doc = tb.id_doc
        inner join contract c on c.idConvention = tov_doc.idConvention
where tv.id_doc = @idDoc
";
            SqlParameter idDocPar = new SqlParameter("idDoc", idDoc);
            return DBExecutor.SelectTable(sql.ToString(), idDocPar);
        }

        public static DataTable GetSertificates()
        {
            StringBuilder query = new StringBuilder();
            query.Append("select ");
            query.Append("    DetailOurFirm.WayToCertificate as Way, ");
            query.Append("    DetailOurFirm.FIOUserCertificate as FIO,  ");
            query.Append("    spr_kontr.inn as Inn, ");
            query.Append("    spr_kontr.kpp as Kpp  ");
            query.Append("from DetailOurFirm inner join spr_kontr on DetailOurFirm.IdFirm = spr_kontr.id_kontr ");
            query.Append("where DetailOurFirm.fEDI = 1 ");
            return DBExecutor.SelectTable(query.ToString());
        }

        public static DataRow GetConsigneeAddress(int idConsignee)
        {
            StringBuilder query = new StringBuilder();

            query.Append("select ");
            query.Append("     CodeRegion as Region  ");
            query.Append("    ,CodeDistrict as Territory   ");
            query.Append("    ,PostIndex as ZipCode  ");
            query.Append("    ,CodeCity as City   ");
            query.Append("    ,CodeTown as Locality    ");
            query.Append("    ,CodeStreet as Street   ");
            query.Append("    ,CodeDoma as Building   ");
            query.Append("    ,House as Block   ");
            query.Append("    ,Quart as Apartment    ");
            query.Append("from Address   ");
            query.Append("where IdKontr = @idKonrt   ");

            SqlParameter idConsigneePar = new SqlParameter("idKonrt", idConsignee);
            return DBExecutor.SelectRow(query.ToString(), idConsigneePar);
        }

        public static DataRow GetUpdTransferParams(object idDoc)
        {
            StringBuilder query = new StringBuilder();
            query.Append("select ");
            query.Append("   contract.ComentForUPD as nameContract  ");
            query.Append("  ,contract.date_contract as DateContract  ");
            query.Append("  ,contract.nom_contract as NomContract   ");
            query.Append("  ,cast(tov_doc.date_doc as date) as DateDoc   ");
            query.Append("  ,tov_doc.nom_doc as NomDoc   ");
            query.Append("  ,cast((select sum(tov.kol * spr_tov.weight) from tov inner join spr_tov on tov.id_tov = spr_tov.id_tov where tov.id_doc = @idDoc) as decimal(18,2)) as Weight   ");
            query.Append("from    ");
            query.Append("  tov_doc ");
            query.Append("     inner join contract on tov_doc.idConvention = contract.idConvention ");
            query.Append("where tov_doc.id_doc = @idDoc");

            SqlParameter idDocPar = new SqlParameter("idDoc", idDoc);
            return DBExecutor.SelectRow(query.ToString(), idDocPar);
        }

        public static DataTable GetAllFreeDoc()
        {
            return DBExecutor.SelectTable("select id_doc from tov_doc (nolock) where id_type_doc in (8, 19) and idEDIstatus = 1 and nom_sf is not null and date_sf is not null");
            ////return DBExecutor.SelectTable("select id_doc from tov_doc where id_doc = '60/41 11.06.20 60397 11 554699");
            ////return DBExecutor.SelectTable("select id_doc from tov_doc where id_doc = '60/41 14.05.20 47683 11 554699'"); 
        }

        public static DataTable GetAllFreeDocCorrect()
        {
            return DBExecutor.SelectTable("select id_doc from tov_doc (nolock) where id_type_doc = 22 and idEDIstatus = 1 and nom_sf is not null and date_sf is not null");
        }

        public static DataTable GetAllSendedDoc()
        {
            return DBExecutor.SelectTable("select id_doc from tov_doc (nolock) where idEDIstatus = 3");
        }

        public static void UpdateDocStatus(object idDoc, object idEDIstatus)
        {
            SqlParameter parIdDoc = new SqlParameter("id_doc", idDoc);
            SqlParameter paridEDIstatus = new SqlParameter("idEDIstatus", idEDIstatus);
            DBExecutor.ExecuteQuery("update tov_doc set idEDIstatus = @idEDIstatus where id_doc = @id_doc", parIdDoc, paridEDIstatus);
        }

        public static void UpdateDiadocMessage(string idMessage, int idStatus)
        {
            SqlParameter paridMessage = new SqlParameter("idMessage", idMessage);
            SqlParameter paridStatus = new SqlParameter("idStatus", idStatus);
            DBExecutor.ExecuteQuery("update DiadocMessage set idDiadocMessageStatus = @idStatus where idMessage = @idMessage", paridMessage, paridStatus);
        }

        public static void SaveMessageBuyer(object idDoc, string messageId, string Inn)
        {
            SqlParameter parIdDoc = new SqlParameter("idDoc", idDoc);
            SqlParameter parIdMessage = new SqlParameter("messageId", messageId);
            SqlParameter parInn = new SqlParameter("Inn", Inn);
            StringBuilder query = new StringBuilder();

            query.Append("insert into DiadocMessage(idMessage,idDoc,idInvoice,idTypeMessage,idDiadocMessageStatus,fProcessed,idUser,dateCreate, inn) ");
            query.Append("values(@messageId,@idDoc,0,1,110, 1,552632,Getdate(), @Inn)");
            DBExecutor.ExecuteQuery(query.ToString(), parIdDoc, parIdMessage, parInn);

        }

        public static void SaveMessageSupplier(string idDoc, string messageId, string Inn, int idInvoice)
        {
            SqlParameter parIdDoc = new SqlParameter("idDoc", idDoc);
            SqlParameter parIdMessage = new SqlParameter("messageId", messageId);
            SqlParameter parInn = new SqlParameter("Inn", Inn);
            SqlParameter paridInvoice = new SqlParameter("idInvoice", idInvoice);
            StringBuilder query = new StringBuilder();

            query.Append("insert into DiadocMessage(idMessage,idDoc,idInvoice,idTypeMessage,idDiadocMessageStatus,fProcessed,idUser,dateCreate, inn) ");
            query.Append("values(@messageId,@idDoc,@idInvoice,2,120, 1,552632, Getdate(), @Inn)");
            DBExecutor.ExecuteQuery(query.ToString(), parIdDoc, parIdMessage, parInn, paridInvoice);

        }

        public static DataTable GetMessageByStatus(string Inn, int idStatus, int fProcessed)
        {
            SqlParameter parInn = new SqlParameter("Inn", Inn);
            SqlParameter paridStatus = new SqlParameter("idStatus", idStatus);
            SqlParameter parfProcessed = new SqlParameter("fProcessed", fProcessed);
            return DBExecutor.SelectTable("select * from DiadocMessage where idDiadocMessageStatus = @idStatus and fProcessed = @fProcessed and Inn = @Inn", parInn, paridStatus, parfProcessed);
        }

        public static void SaveMessageNoLoad(string Inn, string idMessage)
        {
            SqlParameter parInn = new SqlParameter("Inn", Inn);
            SqlParameter parmessageId = new SqlParameter("idMessage", idMessage);
            string sql = @"  if not exists(select * from DiadocMessage where idMessage = @idMessage) 
	                            insert into DiadocMessage(idMessage,idDoc,idInvoice,idTypeMessage,idDiadocMessageStatus,fProcessed,idUser,dateCreate,inn)
	                            values(@idMessage, '', -1, 2, -1, 1, 552632, GETDATE(), @inn)";
            DBExecutor.ExecuteQuery(sql, parInn, parmessageId);
        }

        public static DataRow GetSprKontrByInnKpp(object Inn, object Kpp)
        {
            SqlParameter inn = new SqlParameter("Inn", Inn);
            SqlParameter kpp = new SqlParameter("Kpp", Kpp);
            return DBExecutor.SelectRow("select * from spr_kontr where inn = @Inn and kpp = @Kpp", inn, kpp);
        }


        public static DataRow GetSprKontrById(object idKontr)
        {
            SqlParameter ParidKontr = new SqlParameter("idKontr", idKontr);
            return DBExecutor.SelectRow("select * from spr_kontr where id_kontr = @idKontr", ParidKontr);
        }

        public static DataRow GetSupplierByInnKpp(string Inn, string Kpp)
        {
            SqlParameter inn = new SqlParameter("Inn", Inn);
            SqlParameter kpp = new SqlParameter("Kpp", Kpp);
            return DBExecutor.SelectRow("select * from spr_kontr where inn = @Inn and kpp = @Kpp and supplier = 1", inn, kpp);
        }

        public static DataRow GetFirmByInnKpp(string Inn, string Kpp)
        {
            SqlParameter inn = new SqlParameter("Inn", Inn);
            SqlParameter kpp = new SqlParameter("Kpp", Kpp);
            return DBExecutor.SelectRow("select * from spr_kontr where inn = @Inn and kpp = @Kpp and firma >= 1", inn, kpp);
        }

        public static DataTable GetTovsForSupplier(int idKontr)
        {
            string sql = @"
select distinct id_tov, id_tov_oem_short, orderN 
from spr_tov s
	inner join OrderBuyTov o on s.id_tov = o.idTov
where 
	o.idOrderBuy in (select idOrderBuy from OrderBuy where idSupplier = @idKontr) 
";

            SqlParameter idKontrpar = new SqlParameter("idKontr", idKontr);
            return DBExecutor.SelectTable(sql, idKontrpar);
        }


        public static DataRow getTovInfo(string article, int idtm)
        {
            if (article != "")
                return DBExecutor.SelectRow(@"
                

select spr.id_tov, spr.n_tov, spr.id_tov_oem_short, price.sebest,price.MinSebesteMag, MinSebestOpt, MinSebestRetail, price.price2, price.price3, price.price4, price.MinPriceOpt, price.min_price
from spr_tov spr
join spr_price price on price.id_tov = spr.id_tov
where spr.id_tov_oem_short = dbo.f_replace_for_cross('" + article + @"') 
and spr.id_tm = " + idtm + @"
and spr.id_direct = 60
and price.id_direct = 60                


                ");

            return null;

        }


        public static string GetCountRowInTov(object idDoc)
        {
            string sql = @"select count (tt.id_doc)
                    from (
                        SELECT tov.id_tov,
                                tov.PriceRec,
		                        tov.price_f,
		                        sum(tov.kol) kol,
 		                         tov.gtdcheck ,
 		                         tov.id_doc
		                        from tov with (nolock)
	                        where id_doc = 	@iddoc
	                        group by tov.id_tov, tov.PriceRec,
			                        tov.price_f,
		                        tov.gtdcheck,
		                        tov.id_doc) tt";
            SqlParameter paridDoc = new SqlParameter("idDoc", idDoc);
            var ret = DBExecutor.SelectSchalar(sql, paridDoc);

            return ret.ToString();
        }

    }
}
