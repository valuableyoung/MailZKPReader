using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using ALogic.DBConnector;
using ALogic.Logic.ADiadoc;

namespace ALogic.Model.Entites.Invoice
{
    public class InvoiceBuy
    {
        public string NomSF { get; set; }
        public DateTime DateSF { get; set; }
        public int IdKontr { get; set; }
        public int Proform { get; set; }
        public string NFile { get; set; }
        public string MessageId { get; set; }
        public int idConvention { get; set; }
        public int idTypeSource { get; set; }
        public int idFirm { get; set; }

        public List<InvoiceBuyTov> listTov { get; set; }

        public InvoiceBuy()
        {
            listTov = new List<InvoiceBuyTov>();
            NomSF = "";
            DateSF = DateTime.Now;
            NFile = "";
            IdKontr = 0;
            Proform = 0;
            MessageId = "";
            idConvention = 0;
            idTypeSource = 0;
        }

        public void Save()
        {
            try
            {
                string paramSql = @"
                                    select ISNULL(value_param, 0)
                                    from application_param with (nolock)
                                    where id_application = 50
                                    and id_param = 463 ";

                int paramValue = 1420;
                var result = int.Parse(DBExecutor.SelectSchalar(paramSql).ToString());

                string typeContractSql = @"select id_contract from contract with (nolock) where idConvention = @idConvention";
                SqlParameter idConventionp = new SqlParameter("idConvention", idConvention);
                var TypeContractResult = DBConnector.DBExecutor.SelectSchalar(typeContractSql, idConventionp);

                if (TypeContractResult != null && TypeContractResult.ToString() == "11")
                {
                    if (result == 0) paramValue = 1420;
                    else paramValue = 1410;
                }





                StringBuilder query = new StringBuilder();
                query.Append(" insert into InvoiceBuy(idOrderBuy,DateInvoice,NomInvoice,idSupplier,idStatusInvoice,idDocBuy,idcur,IdTerritory,IdDirect,Idfirm,Curs,fProformInvoice,fNotProcessed, NameLoadedFile, idConvention, idTypeSource) ");
                query.Append(" values( 0, @DateSF, @nomSF, @idKontr, @valuParam, NULL, 0, 1, 60, @idFirm, 1, @fProformInvoice, 0, @nFile, @idConvention, @idTypeSource )");
                query.Append(" select @@Identity ");

                SqlParameter nomSFPar = new SqlParameter("nomSF", NomSF);
                SqlParameter nFilePar = new SqlParameter("nfile", NFile);
                SqlParameter DateSFPar = new SqlParameter("DateSF", DateSF);
                SqlParameter idKontrPar = new SqlParameter("idKontr", IdKontr);
                SqlParameter fProformPar = new SqlParameter("fProformInvoice", Proform);
                SqlParameter idConventionPar = new SqlParameter("idConvention", idConvention);
                SqlParameter idTypeSourcePar = new SqlParameter("idTypeSource", idTypeSource);
                SqlParameter idFirmPar = new SqlParameter("idFirm", idFirm);
                SqlParameter valuParam = new SqlParameter("valuParam", paramValue);
                object idInvoice = DBConnector.DBExecutor.SelectSchalar(query.ToString(), nomSFPar, DateSFPar, idKontrPar, fProformPar, nFilePar, idConventionPar, idTypeSourcePar, idFirmPar, valuParam);

                foreach (var ans in listTov)
                {
                    StringBuilder queryDetail = new StringBuilder();
                    queryDetail.Append(" insert into InvoiceBuyTov(idInvoice,idRowBuy,idTov,Kol,Price,nCountryDomain,idStatusTov,idbrand,idtovoem,declaration,nSubSupplier,idRowSale,idDocBuy,netto,nettoWithCan,brutto) ");
                    queryDetail.Append(" values(@idInvoice, 0, 0, @Kol, @Price, @MadeIn, 220, @nTm, @idTovOem, @declaration, '', 0, null, 0, 0, 0) ");
                    SqlParameter idInvoicePar = new SqlParameter("idInvoice", idInvoice);
                    SqlParameter KolPar = new SqlParameter("Kol", ans.Kol);
                    SqlParameter PriceCurPar = new SqlParameter("Price", ans.Price);
                    SqlParameter MadeInPar = new SqlParameter("MadeIn", ans.MadeIn);
                    SqlParameter NTmPar = new SqlParameter("nTm", ans.Brend);
                    SqlParameter IdTovOemPar = new SqlParameter("idTovOem", ans.IdArt);
                    SqlParameter DeclarationPar = new SqlParameter("declaration", ans.Declaration == null ? "" : ans.Declaration.Replace("-", ""));

                    DBConnector.DBExecutor.ExecuteQuery(queryDetail.ToString(), idInvoicePar, KolPar, PriceCurPar, MadeInPar, NTmPar, IdTovOemPar, DeclarationPar);
                }
                SqlParameter idInvoiceP = new SqlParameter("idInvoice", idInvoice);

                if (IdKontr == 549441)
                    DBConnector.DBExecutor.ExecuteQuery("exec up_SetIdTovInInvoiceTov @idInvoice,'0',1", idInvoiceP);
                else
                    DBConnector.DBExecutor.ExecuteQuery("exec up_SetIdTovInInvoiceTov @idInvoice,'0',0", idInvoiceP);

                if (MessageId != "")
                    DBDiadoc.SaveMessageSupplier("", MessageId, "", int.Parse(idInvoice.ToString()));
            }
            catch (Exception ex)
            {
                ALogic.Logic.Old.Logger.WriteErrorMessage("Ошибка при сохранении в InvoiceBuyTov: " + ex.Message);
            }
        }


        public static DateTime GetLastInvoiceDate()
        {
            var data = DBConnector.DBExecutor.SelectTable("select Max(DateInvoice) from InvoiceBuy where MessageId is not null and MessageId <> ''");
            return (data.Rows.Count == 0 || data.Rows[0][0] == DBNull.Value) ? DateTime.Now.Date : DateTime.Parse(data.Rows[0][0].ToString()).AddHours(-1);
        }

        public static List<string> GetListMessageID(DateTime startDate)
        {
            SqlParameter parDate = new SqlParameter("date", startDate);
            var data = DBConnector.DBExecutor.SelectTable("select idMessage from DiadocMessage where dateCreate >= @date", parDate);
            return data.AsEnumerable().Select(p => p[0].ToString()).ToList();
        }

        public static DataRow GetConventionWithSeller(int idKontr, string nConv, DateTime dateConv)
        {
            SqlParameter paridKontr = new SqlParameter("idKontr", idKontr);
            SqlParameter parnConv = new SqlParameter("dateContr", dateConv);
            SqlParameter pardateConv = new SqlParameter("nomContr", nConv);
            return DBExecutor.SelectRow("select * from contract where id_kontr = @idKontr and date_contract =@dateContr and nom_contract = @nomContr and id_contract in (3, 4, 10, 11, 9)", paridKontr, parnConv, pardateConv);
        }

        public static DataTable GetConventionWithSeller(int idKontr)
        {
            SqlParameter paridKontr = new SqlParameter("idKontr", idKontr);
            return DBExecutor.SelectTable("select * from contract where id_kontr = @idKontr and id_contract in (3, 4, 10, 11, 9)", paridKontr);
        }

        public static DataTable GetConventionWithSeller(int idKontr, int idFirm)
        {
            string sql = @"select idConvention
                        from contract with (nolock)
                        where id_kontr = @idKontr
                        and id_firm = @idFirm
                        and id_contract in ( 3, 4, 10, 11, 9)
                        and idStatusContract in (10,20)
                        and fEDI = 1 ";
            SqlParameter paridKontr = new SqlParameter("idKontr", idKontr);
            SqlParameter paridFirm = new SqlParameter("idFirm", idFirm );
            return DBExecutor.SelectTable(sql, paridKontr,paridFirm);
        }

      
    }
}
