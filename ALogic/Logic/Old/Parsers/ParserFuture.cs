using ALogic.DBConnector;
using ALogic.Logic.Reload1C;
using ALogic.Logic.SPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace ALogic.Logic.Old.Parsers
{
    public class ParserFuture
    {

        public int CreateFutureHeader(int idkontr, ParserSettings currentSetting, int iduser, DateTime datefrom, int loadtype)
        {

            SqlParameter _pricetype = new SqlParameter("pricetype", 5);
            SqlParameter _idkontr = new SqlParameter("idkontr", idkontr);
            SqlParameter _datefrom = new SqlParameter("datefrom", datefrom);
            SqlParameter _idpricesettings = new SqlParameter("idpricesettings", currentSetting == null ? 0: currentSetting.idParserSettings);
            SqlParameter _iduser = new SqlParameter("iduser", iduser);
            SqlParameter _loadtype = new SqlParameter("loadtype", loadtype);

            DBExecutor.ExecuteQuery(@"
                insert into PriceFutureH
                (
                        pricetype,
                        idkontr,
                        datefrom,
                        idpricesettings,
                        iduser,
                        loadtype
                )
                        values(
                        @pricetype,
                        @idkontr,
                        @datefrom,
                        @idpricesettings,
                        @iduser,
                        @loadtype
                )
            ", _pricetype, _idkontr, _datefrom, _idpricesettings, _iduser, _loadtype);

            var res = DBExecutor.SelectRow("select max(idpf) as res from PriceFutureH");

            return int.Parse(res[0].ToString());

        }

        public void LoadSKU(int idpf, int kolInFile)
        {
            SqlParameter _idpf = new SqlParameter("idpf", idpf);
            SqlParameter _kolInFile = new SqlParameter("kolInFile", kolInFile);
            try
            {
                DBExecutor.ExeciteProcedure("up_UpdatePriceOnlineTempFuture", _idpf, _kolInFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка загрузки прайса: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        internal static DataTable GetLoadType()
        {
            return DBExecutor.SelectTable("select loadtype, loadname from pricefutureLoadtype");
        }

        public int CheckFutureheader(int idSupplier, DateTime datefrom, int loadtype)
        {

            SqlParameter _idSupplier = new SqlParameter("idSupplier", idSupplier);
            SqlParameter _datefrom = new SqlParameter("datefrom", datefrom);
            SqlParameter _loadtype = new SqlParameter("loadtype", loadtype);


           var res =  DBExecutor.SelectRow(@"select idpf from PriceFutureH 
                                                    where 
                                                    idkontr = @idSupplier and
                                                    datefrom = @datefrom and
                                                    loadtype = @loadtype
                                 ", _idSupplier, _datefrom, _loadtype);
            if(res!=null) return int.Parse(res[0].ToString());

            return 0;
        }

        public DataTable GetNotConfirmedLoads(List<int> l)
        {
            string s = "";
            foreach(var v in l)
            {
                if (s.Length == 0)
                {
                    s = s + v.ToString();
                }
                else
                {
                    s = s + ", " + v.ToString();
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select pfh.idkontr, s.n_kontr, pfh.datefrom, pfh.dateprice, s2.n_kontr whoload, pfh.dateconfirm, s3.n_kontr n_whoconfirm, pfh.idpf ");
            sb.Append("from PriceFutureH pfh (nolock) ");
            sb.Append("inner join spr_kontr s (nolock) on s.id_kontr = pfh.idkontr ");
            sb.Append("inner join spr_kontr s2 (nolock) on s2.id_kontr = pfh.iduser ");
            sb.Append("left join spr_kontr s3 (nolock) on s3.id_kontr = pfh.whoconfirm ");
            sb.Append("inner join pricefutureLoadtype plt on plt.loadtype = pfh.loadtype ");
            //sb.Append("where pfh.status = 'N' and pfh.confirmed = 0 ");
            sb.Append("where pfh.idpf in (" + s + ")");

            string s2 = sb.ToString();
            var table = DBExecutor.SelectTable(s2);
                
            return table;
        }

        public DataTable CheckNotConfirmed(int idpf)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"declare @perc numeric(6, 2), @idpf int ");
            sb.Append("select @perc = isnull(value_param, 100) from application_param where id_param = 467 ");
            sb.Append("set @idpf = " + idpf.ToString() + " ");
            sb.Append("if object_id('tempdb..#ids') is not null drop table #ids ");
            sb.Append("create table #ids(idpf int) ");
            sb.Append("insert into #ids select @idpf ");
            sb.Append("if object_id('tempdb..#tovs') is not null drop table #tovs ");
            sb.Append("create table #tovs(idtov int, idtovoem varchar(250), idkontr int, kprice numeric(18, 4), kpricetm numeric(18, 4), nprice numeric(18, 4), npricetm numeric(18, 4), ");
            sb.Append("    price_ratio numeric(18, 2), pricetm_ratio numeric(18, 2), idpf int) ");
            sb.Append("insert into #tovs ");
            sb.Append("select d.idtov, d.idtovoem, h.idkontr, k.price, k.pricetm, d.price newprice, d.pricetm newpricetm, ");
            sb.Append("case when isnull(d.price, 0) <> 0 then  cast(100 - (k.price /  d.price * 100) as numeric(18, 2)) else 0 end price_ratio, ");
            sb.Append("case when isnull(d.pricetm, 0) <> 0 then  cast(100 - (k.pricetm /  d.pricetm * 100) as numeric(18, 2)) else 0 end pricetm_ratio, ");
            sb.Append("d.idpf ");
            sb.Append("from pricefutured d (nolock) ");
            sb.Append("inner join pricefutureh h (nolock) on h.idpf = d.idpf ");
            sb.Append("left join kontr_tov_price k (nolock) on k.id_tov = d.idtov and k.id_kontr = h.idkontr ");
            sb.Append("where d.idpf in (select idpf from #ids) ");
            sb.Append("select #tovs.idtov, #tovs.idtovoem, spr_tov.n_tov, #tovs.idkontr, isnull(#tovs.price_ratio, 0) price_ratio, isnull(#tovs.pricetm_ratio, 0) pricetm_ratio, #tovs.idpf ");
            sb.Append("from #tovs inner join spr_tov(nolock) on spr_tov.id_tov = #tovs.idtov ");
            sb.Append("where isnull(abs(#tovs.price_ratio), 0) > @perc or isnull(abs(#tovs.pricetm_ratio), 0) > @perc order by 5 desc ");
            var s = sb.ToString();
            var table = DBExecutor.SelectTable(s);
            return table;
        }

        public DataTable GetNotConfirmedLoadsDetail()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"declare @perc numeric(6, 2), @idpf int ");
            sb.Append("select @perc = isnull(value_param, 100) from application_param where id_param = 467 ");
            //sb.Append("set @idpf = " + idpf.ToString() + " ");
            sb.Append("if object_id('tempdb..#ids') is not null drop table #ids ");
            sb.Append("create table #ids(idpf int) ");
            //sb.Append("insert into #ids select @idpf ");
            sb.Append("insert into #ids select idpf from pricefutureh (nolock) where status <> 'L' and confirmed = 0");
            sb.Append("if object_id('tempdb..#tovs') is not null drop table #tovs ");
            sb.Append("create table #tovs(idtov int, idtovoem varchar(250), idkontr int, kprice numeric(18, 4), kpricetm numeric(18, 4), nprice numeric(18, 4), npricetm numeric(18, 4), ");
            sb.Append("    price_ratio numeric(18, 2), pricetm_ratio numeric(18, 2), idpf int, idtm int) ");
            sb.Append("insert into #tovs ");
            sb.Append("select d.idtov, d.idtovoem, h.idkontr, k.price, k.pricetm, d.price newprice, d.pricetm newpricetm, ");
            sb.Append("case when isnull(d.price, 0) <> 0 then  cast(100 - (k.price /  d.price * 100) as numeric(18, 2)) else 100 end price_ratio, ");
            sb.Append("case when isnull(d.pricetm, 0) <> 0 then  cast(100 - (k.pricetm /  d.pricetm * 100) as numeric(18, 2)) else 100 end pricetm_ratio, ");
            sb.Append("d.idpf, d.idtm ");
            sb.Append("from pricefutured d (nolock) ");
            sb.Append("inner join pricefutureh h (nolock) on h.idpf = d.idpf ");
            sb.Append("left join kontr_tov_price k (nolock) on k.id_tov = d.idtov and k.id_kontr = h.idkontr ");
            sb.Append("where d.idpf in (select idpf from #ids) ");
            sb.Append("select #tovs.idtov, #tovs.idtovoem, spr_tov.n_tov, #tovs.idkontr, isnull(#tovs.price_ratio, 0) price_ratio, isnull(#tovs.pricetm_ratio, 0) pricetm_ratio, #tovs.idpf, spr_tm.tm_name ");
            sb.Append("from #tovs inner join spr_tov(nolock) on spr_tov.id_tov = #tovs.idtov ");
            sb.Append("inner join spr_tm (nolock) on spr_tm.tm_id = #tovs.idtm ");
            sb.Append("where isnull(abs(#tovs.price_ratio), 0) > @perc or isnull(abs(#tovs.pricetm_ratio), 0) > @perc order by tm_name, pricetm_ratio desc ");
            var s = sb.ToString();
            var table = DBExecutor.SelectTable(s);
            return table;
        }

        public DataTable GeDataAnaliz(int idkontr)
        {
            /*
            string sql = @"
                            
                        select
                            h.datefrom,
                            d.idtov,
                            tm.tm_name,
                            t.id_tov_oem,
                            t.n_tov,
                            isnull(t.OrderN, '') ordern,
                            t.id_tov_oem,
                            k.price current_price,
                            k.pricetm current_rrp,
                            d.price new_price,
                            d.pricetm new_rrp,
                            case when d.price <> 0 then cast(round(100.0 - (k.price * 100.0 / d.price), 2) as numeric(18, 2)) else 0 end percprice,
                            case when d.pricetm <> 0 then cast(round(100.0 - (k.pricetm * 100.0 / d.pricetm), 2) as numeric(18, 2)) else 0 end percrrp,
                            d.price - k.price rubprice,
                            d.pricetm - k.pricetm rubrrp,
                            h.idpf
                            from
                            PriceFutureH h
                            inner join PriceFutureD d on d.idpf = h.idpf
                            inner join kontr_tov_price k on k.id_kontr = h.idkontr and k.id_tov = d.idtov 
                            inner join spr_tov t on t.id_tov = d.idtov
                            inner join spr_tm tm on tm.tm_id = t.id_tm
                        where h.idkontr = @idkontr
                            and h.datefrom > GETDATE()
                            and h.status = 'N'
                        order by h.datefrom, rubprice
                ";
                */

            SqlParameter _idkontr = new SqlParameter("idkontr", idkontr);
            return DBExecutor.ExecuteProcedureTable("up_PriceFutureAnalysis", _idkontr);
            /*DataTable dt = new DataTable();
            
            if (Connection.ConnectToDataBase())
            {
                SqlCommand cmd = new SqlCommand("up_PriceFutureAnalysis", Connection.SqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@idkontr", idkontr);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                cmd.ExecuteNonQuery();
               
                adapter.Fill(dt);
                Connection.CloseConnection();
            }
            return dt;*/
            //return DBExecutor.SelectTable(sql, _idkontr);
        }

        public void UpdateFutureHeaderUser(int _idPf, int _currentUserId)
        {
            SqlParameter idPf = new SqlParameter("idPf", _idPf);
            SqlParameter currentUserId = new SqlParameter("idKontr", _currentUserId);
            DBExecutor.ExecuteQuery("update PriceFutureH set iduser = @idKontr where idpf = @idPf", idPf, currentUserId);

        }

        public void deleteFromPriceOnlineTemp(int idSupplier)
        {
            SqlParameter idkontr = new SqlParameter("idKontr", idSupplier);
            DBExecutor.ExecuteQuery("delete from PriceOnlineTemp where idKontr = @idKontr", idkontr);
        }

        public void AddRowsToHtmlMessage(int idpf, int idsupplier, ref DataTable t, ref StringBuilder msg)
        {
            if (t.Rows.Count == 0) { return; }

            msg.Append("<br><br>");

            SqlParameter _idKontrSupp = new SqlParameter("idKontr", idsupplier);
            var NKontr = DBExecutor.SelectRow("select n_kontr from spr_kontr where id_kontr = @idKontr", _idKontrSupp)[0].ToString();

            msg.Append(@"ВНИМАНИЕ! В прайсе поставщика <b>" + NKontr + "</b>");
            msg.Append(" есть товарные позиции, разница в ценах у которых больше контрольного значения<br><br>");
            msg.Append("<b>Необходимо подтвердить корректность загруженных цен</b><br><br>");

            msg.Append("Артикулы:<br><br>");

            foreach (DataRow r in t.AsEnumerable())
            {
                msg.Append(r["idtov"].ToString() + " \t" + r["idtovoem"].ToString() + " \t" + r["n_tov"].ToString() + "<br>");
            }
        }

        public void SendEmailForConfirm(int idpf, int idSupplier, ref DataTable t)
        {
            try
            {
                if (t.Rows.Count == 0) { return; }

                SqlParameter _idpf = new SqlParameter("idpf", idpf);
                var table = DBExecutor.SelectTable(@"
                    select distinct sk.id_kontr, sk.n_kontr_full, el_mail from rKontrBrand rk
                    join spr_kontr sk on sk.id_kontr = rk.idKontr
                    where idTm in(
	                    select distinct idtm from PriceFutureD
	                    where idpf = @idpf
                    )
                ", _idpf);
                //SqlParameter _idKontrName = new SqlParameter("idKontr", idSupplier);

                SqlParameter _idKontrSupp = new SqlParameter("idKontr", idSupplier);
                var NKontr = DBExecutor.SelectRow("select n_kontr from spr_kontr where id_kontr = @idKontr", _idKontrSupp)[0].ToString();

                StringBuilder htmlMessage = new StringBuilder();
                string email = "";
                foreach (DataRow item in table.Rows)
                {
                    var RTKname = item["n_kontr_full"].ToString();
                    var RTKmail = item["el_mail"].ToString();
                    email = RTKmail;

                    htmlMessage.Append(@"ВНИМАНИЕ! В прайсе поставщика <b>" + NKontr + "</b>");
                    htmlMessage.Append(" есть товарные позиции, разница в ценах у которых больше контрольного значения<br><br>");
                    htmlMessage.Append("<b>Необходимо подтвердить корректность загруженных цен</b><br><br>");

                    htmlMessage.Append("Артикулы:<br><br>");

                    foreach (DataRow r in t.AsEnumerable())
                    {
                        htmlMessage.Append(r["idtov"].ToString() + " " + r["idtovoem"].ToString() + " " + r["n_tov"].ToString() + "<br>");
                    }

                    htmlMessage.Append("<br><br>Подробнее можно смотреть во вкладках 'Анализ' и 'Неподтвержденные прайсы'");
                }


                MailAddress from = new MailAddress(ProjectProperty.MailUserPriceParser);
                MailMessage m = new MailMessage();

                //TODO: Разкоментить email получателя, прикрепленного за брендом 

                m.To.Add(email);
                //m.CC.Add(ProjectProperty.MailFuturesBoss);
                //    if (email == "Fedchenkoas@arkona36.ru") { m.CC.Add("dubininap@arkona36.ru"); }
                //m.To.Add("muhinan@arkona36.ru");
                m.Bcc.Add("muhinan@arkona36.ru");

                m.From = from;
                m.Subject = "Требуется подтверждение цен по поставщику " + NKontr;
                m.Body = htmlMessage.ToString();

                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(ProjectProperty.MailServer, 587);
                smtp.Credentials = new NetworkCredential(ProjectProperty.MailServer, ProjectProperty.MailUserPriceParserPassword);
                smtp.EnableSsl = false;
                smtp.Send(m);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать и отправить письмо о контроле цен: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SendEmail(int idpf, int idSupplier, ref DataTable dtconf)
        {
            StringBuilder htmlMessage = new StringBuilder();
            try
            {
                SqlParameter _idpf = new SqlParameter("idpf", idpf);
                var table = DBExecutor.SelectTable(@"
                    select distinct sk.id_kontr, sk.n_kontr_full, el_mail from rKontrBrand rk
                    join spr_kontr sk on sk.id_kontr = rk.idKontr
                    where idTm in(
	                    select distinct idtm from PriceFutureD
	                    where idpf = @idpf
                    )
                ", _idpf);

                SqlParameter _idKontrName = new SqlParameter("idKontr", idSupplier);
                SqlParameter _idKontrSupp = new SqlParameter("idKontr", idSupplier);
                var tblStatusTemp = DBExecutor.ExecuteProcedureTable("up_PriceFutureAnalysis", _idKontrName);
                var NKontr = DBExecutor.SelectRow("select n_kontr from spr_kontr where id_kontr = @idKontr", _idKontrSupp)[0].ToString();


                foreach (DataRow item in table.Rows)
                {
                    htmlMessage.Clear();

                    SqlParameter _idKontr = new SqlParameter("idKontr", item["id_kontr"]);

                    //var KontrParent = int.Parse(item["id_kontr"].ToString());
                    //var tblStatushasRows = (from i in tblStatusTemp.AsEnumerable() where i.Field<int>("idpf") == idpf && i.Field<int>("idkontrparent") == KontrParent select i);
                    var tblStatushasRows = (from i in tblStatusTemp.AsEnumerable() where i.Field<int>("idpf") == idpf select i);
                    if (tblStatushasRows != null && tblStatushasRows.Count() > 0)
                    {
                        var tblStatus = tblStatushasRows.CopyToDataTable();

                        var UpSku = (from DataRow i in tblStatus.Rows orderby i["idtm"].ToString(), double.Parse(i["Perc_Price"].ToString()) descending where double.Parse(i["RubPrice"].ToString()) > 0 select i);
                        var DownSku = (from DataRow i in tblStatus.Rows orderby i["idtm"].ToString(), double.Parse(i["Perc_Price"].ToString()) descending where double.Parse(i["RubPrice"].ToString()) < 0 select i);
                        var NotChanged = (from DataRow i in tblStatus.Rows where double.Parse(i["Perc_Price"].ToString()) == 0 select i);

                        htmlMessage.Append("Уважаемый(ая) <b>" + item["n_kontr_full"].ToString() + "</b>,<br>");
                        htmlMessage.Append("в прайсе поставщика \"<b>");
                        htmlMessage.Append(NKontr);
                        htmlMessage.Append("\"</b> начиная c ");
                        htmlMessage.Append(DateTime.Parse(tblStatus.Rows[0]["datefrom"].ToString()).ToShortDateString());
                        htmlMessage.Append(" будут изменения.<br>");
                        htmlMessage.Append("<b>Необходимо проконтролировать корректность загруженных цен.</b><br>");
                        ////htmlMessage.Append("<b>Для того, чтобы эти цены стали действующими необходимо подтверждение!</b><br>");

                        if (UpSku.Count() > 0)
                        {
                            htmlMessage.Append(" - " + UpSku.Count());
                            htmlMessage.Append(" позиций подорожают <br>");
                        }

                        if (DownSku.Count() > 0)
                        {
                            htmlMessage.Append(" - " + DownSku.Count());
                            htmlMessage.Append(" позиций подешевеют <br>");
                        }

                        if (NotChanged.Count() > 0)
                        {
                            htmlMessage.Append(" - " + NotChanged.Count());
                            htmlMessage.Append(" позиций останутся без изменения <br>");
                        }

                        if (UpSku.Count() > 0)
                        {
                            htmlMessage.Append("<h2 align='center'>Позиции ПОДОРОЖАЮТ</h2>");

                            htmlMessage.Append(@"
                            <table border='1' width='100%' cellpadding='5' cellspacing='0'> 
                                <thead bgcolor='F2F2F2'>
                                    <tr>
                                    <th align='center'>Код</th>
                                    <th align='center'>Бренд</th>
                                    <th align='center'>Артикул</th>
                                    <th align='center'>Наименование</th>
                                    <th align='center'>Текущая цена пост., руб</th>
                                    <th align='center'>Новая цена пост., руб</th>
                                    <th align='center'>% Изм цены</th>
                                    <th align='center'>Цена разница, руб</th>                                    
                                    <th align='center'>Текущая РРЦ, руб.</th>
                                    <th align='center'>Новая РРЦ, руб.</th>
                                    </tr>
                                </thead>
                            ");

                            foreach (DataRow tblItem in UpSku)
                            {
                                htmlMessage.Append(@"

                                <tr>
                                    <td align='right'>" + tblItem["Idtov"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["Tm_Name"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["id_tov_oem"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["n_tov"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Current_Price"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["New_Price"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Perc_Price"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["RubPrice"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Current_RRP"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["New_RRP"].ToString() + @"</td>
                                 </tr>
                              ");
                            }
                            htmlMessage.Append("</table><br>");
                        }

                        if (DownSku.Count() > 0)
                        {
                            htmlMessage.Append("<h2 align='center'>Позиции ПОДЕШЕВЕЮТ</h2>");

                            htmlMessage.Append(@"
                            <table border='1' width='100%' cellpadding='5' cellspacing='0'> 
                                <thead bgcolor='F2F2F2'>
                                    <tr>
                                    <th align='center'>Код</th>
                                    <th align='center'>Бренд</th>
                                    <th align='center'>Артикул</th>
                                    <th align='center'>Наименование</th>
                                    <th align='center'>Текущая цена пост., руб</th>
                                    <th align='center'>Новая цена пост., руб</th>
                                    <th align='center'>% Изм цены</th>
                                    <th align='center'>Цена разница, руб</th>
                                    <th align='center'>Текущая РРЦ, руб.</th>
                                    <th align='center'>Новая РРЦ, руб.</th>
                                    </tr>
                                </thead>
                            ");

                            foreach (DataRow tblItem in DownSku)
                            {
                                htmlMessage.Append(@"

                                <tr>
                                    <td align='right'>" + tblItem["Idtov"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["Tm_Name"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["id_tov_oem"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["n_tov"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Current_Price"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["New_Price"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Perc_Price"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["RubPrice"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Current_RRP"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["New_RRP"].ToString() + @"</td>
                                 </tr>
                              ");
                            }
                            htmlMessage.Append("</table><br>");
                        }

                        if (dtconf.Rows.Count > 0)
                        {
                            //SendEmailForConfirm(idpf, idSupplier, ref dtconf);
                            AddRowsToHtmlMessage(idpf, idSupplier,  ref dtconf, ref htmlMessage);
                            SetUnsetConfirm(idpf, 0, 0);
                        }
                        else
                        {
                            SetUnsetConfirm(idpf, 1, User.CurrentUserId);
                        }

                        htmlMessage.Append(@"<br>Подробнее можно смотреть во вкладках 'Анализ' и 'Неподтвержденные прайсы' для прайсов со сроком действия (КИС - Модули - закупки - АРМ РТК - в меню Импорт / парсинг - Парсинг прайса со сроком действия)");

                        SendEmalToKontr(item["el_mail"].ToString(), htmlMessage.ToString(), DateTime.Today.ToString());
                        //SendEmalToKontr("muhinan@arkona36.ru", htmlMessage.ToString(), DateTime.Parse(tblStatus.Rows[0]["datefrom"].ToString()).ToShortDateString());
                    }
                }
            }
            catch (Exception ex) {
                UniLogger.WriteLog("", 1, "ошибка отправки письма об изменении цены: "+ ex.Message);
            }
        }

        public void SendRatioEmail(int idSupplier)
        {
            SqlParameter _idSupplier = new SqlParameter("idkontr", idSupplier);
            var table = DBExecutor.SelectTable(@"
                    select distinct sk.id_kontr, sk.n_kontr_full, el_mail 
                    from rKontrBrand rk (nolock)
                    inner join spr_kontr sk (nolock) on sk.id_kontr = rk.idKontr
                    where idTm in(
                    select distinct idtm from PriceFutureD (nolock)
                    inner join PriceFutureH (nolock) on PriceFutureH.idpf = PriceFutureD.idpf
                    where PriceFutureH.idkontr = @idkontr and PriceFutureH.status <> 'L')
                ", _idSupplier); //and PriceFutureH.status <> 'L'

            SqlParameter _idKontrName = new SqlParameter("idKontr", idSupplier);
            SqlParameter _idKontrSupp = new SqlParameter("idKontr", idSupplier);
            var tblStatusTemp = DBExecutor.ExecuteProcedureTable("up_PriceFutureAnalysis", _idKontrName);
            var NKontr = DBExecutor.SelectRow("select n_kontr from spr_kontr (nolock) where id_kontr = @idKontr", _idKontrSupp)[0].ToString();

            foreach (DataRow item in table.Rows)
            {
                SqlParameter _idKontr = new SqlParameter("idKontr", item["id_kontr"]);

                var KontrParent = int.Parse(item["id_kontr"].ToString());
                var tblStatushasRows = (from i in tblStatusTemp.AsEnumerable() where i.Field<int>("idkontrparent") == KontrParent select i);
                if (tblStatushasRows != null && tblStatushasRows.Count() > 0)
                {
                    var tblStatus = tblStatushasRows.CopyToDataTable();

                    StringBuilder htmlMessage = new StringBuilder();

                    var UpRatio = (from DataRow i in tblStatus.Rows orderby i["idtm"].ToString(), double.Parse(i["Perc_Price"].ToString()) descending
                                   where double.Parse(i["Future_Ratio"].ToString()) > double.Parse(i["Curr_Ratio"].ToString()) select i);

                    var DownRatio = (from DataRow i in tblStatus.Rows
                                   orderby i["idtm"].ToString(), double.Parse(i["Perc_Price"].ToString()) descending
                                   where double.Parse(i["Future_Ratio"].ToString()) < double.Parse(i["Curr_Ratio"].ToString()) select i);

                    var NotChangedRatio = (from DataRow i in tblStatus.Rows
                                   orderby i["idtm"].ToString(), double.Parse(i["Perc_Price"].ToString()) descending
                                   where double.Parse(i["Future_Ratio"].ToString()) == double.Parse(i["Curr_Ratio"].ToString()) select i);

                    htmlMessage.Append("Уважаемый(ая) <b>" + item["n_kontr_full"].ToString() + "</b>,<br>");
                    htmlMessage.Append("в прайсе поставщика \"<b>");
                    htmlMessage.Append(NKontr);
                    htmlMessage.Append("\"</b> начиная c ");
                    htmlMessage.Append(DateTime.Parse(tblStatus.Rows[0]["datefrom"].ToString()).ToShortDateString());
                    htmlMessage.Append(" будут изменения:<br><br>");

                    if (UpRatio.Count() > 0)
                    {
                        htmlMessage.Append(" - " + UpRatio.Count());
                        htmlMessage.Append(" увеличится соотношение Цены Поставщика и РРЦ <br>");
                    }

                    if (DownRatio.Count() > 0)
                    {
                        htmlMessage.Append(" - " + DownRatio.Count());
                        htmlMessage.Append(" уменьшится соотношение Цены Поставщика и РРЦ <br>");
                    }

                    if (NotChangedRatio.Count() > 0)
                    {
                        htmlMessage.Append(" - " + NotChangedRatio.Count());
                        htmlMessage.Append(" соотношение Цены Поставщика и РРЦ останется без изменений <br>");
                    }

                    if (UpRatio.Count() > 0)
                    {
                        htmlMessage.Append("<h2 align='center'>Соотношение Цены поставщика и РРЦ УВЕЛИЧИТСЯ</h2>");

                        htmlMessage.Append(@"
                            <table border='1' width='100%' cellpadding='5' cellspacing='0'> 
                                <thead bgcolor='F2F2F2'>
                                    <tr>
                                    <th align='center'>Код</th>
                                    <th align='center'>Бренд</th>
                                    <th align='center'>Артикул</th>
                                    <th align='center'>Наименование</th>
                                    <th align='center'>Текущее соотношение Цены Поставщика и РРЦ, %</th>
                                    <th align='center'>Новое соотношение Цены Поставщика и РРЦ, %</th>
                                    </tr>
                                </thead>
                            ");

                        foreach (DataRow tblItem in UpRatio)
                        {
                            htmlMessage.Append(@"
                                <tr>
                                    <td align='right'>" + tblItem["Idtov"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["Tm_Name"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["id_tov_oem"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["n_tov"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Curr_Ratio"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Future_Ratio"].ToString() + @"</td>
                                 </tr>
                              ");
                        }
                        htmlMessage.Append("</table><br>");
                    }

                    if (DownRatio.Count() > 0)
                    {
                        htmlMessage.Append("<h2 align='center'>Соотношение Цены поставщика и РРЦ УМЕНЬШИТСЯ</h2>");

                        htmlMessage.Append(@"
                            <table border='1' width='100%' cellpadding='5' cellspacing='0'> 
                                <thead bgcolor='F2F2F2'>
                                    <tr>
                                    <th align='center'>Код</th>
                                    <th align='center'>Бренд</th>
                                    <th align='center'>Артикул</th>
                                    <th align='center'>Наименование</th>
                                    <th align='center'>Текущее соотношение Цены Поставщика и РРЦ, %</th>
                                    <th align='center'>Новое соотношение Цены Поставщика и РРЦ, %</th>
                                    </tr>
                                </thead>
                            ");

                        foreach (DataRow tblItem in DownRatio)
                        {
                            htmlMessage.Append(@"

                                <tr>
                                    <td align='right'>" + tblItem["Idtov"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["Tm_Name"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["id_tov_oem"].ToString() + @"</td>
                                    <td align='left'>" + tblItem["n_tov"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Curr_Ratio"].ToString() + @"</td>
                                    <td align='right'>" + tblItem["Future_Ratio"].ToString() + @"</td>
                                 </tr>
                              ");
                        }
                        htmlMessage.Append("</table><br>");
                    }


                    htmlMessage.Append(@"<br>Подробнее можно смотреть во вкладке 'Анализ' для прайсов со сроком действия (КИС - Модули - закупки - АРМ РТК - в меню Импорт / парсинг - Парсинг прайса со сроком действия)");

                    if (item["el_mail"] != null)
                    {
                        SendEmalToKontr(item["el_mail"].ToString(), htmlMessage.ToString(), DateTime.Parse(tblStatus.Rows[0]["datefrom"].ToString()).ToShortDateString());
                        ////SendEmalToKontr("muhinan@arkona36.ru", htmlMessage.ToString(), DateTime.Parse(tblStatus.Rows[0]["datefrom"].ToString()).ToShortDateString());
                    }
                }
            }
        }

        public void SendEmalToKontr(string  email, string HtmlMessage, string datefrom)
        {
            try
            {
                MailAddress from = new MailAddress(ProjectProperty.MailUserPriceParser);
                MailMessage m = new MailMessage();

                //TODO: Разкоментить email получателя, прикрепленного за брендом 

                m.To.Add(email);
                //m.CC.Add(ProjectProperty.MailFuturesBoss);
                //if (email == "Fedchenkoas@arkona36.ru") { m.CC.Add("dubininap@arkona36.ru"); }
                
                m.Bcc.Add("muhinan@arkona36.ru");

                m.From = from;
                m.Subject = "Отчет об изменении цен на " + datefrom + ".";
                m.Body = HtmlMessage;

                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(ProjectProperty.MailServer, 587);
                smtp.Credentials = new NetworkCredential(ProjectProperty.MailServer, ProjectProperty.MailUserPriceParserPassword);
                smtp.EnableSsl = false;
                smtp.Send(m);
                UniLogger.WriteLog("", 0, "Отправлено письмо об изменении цен: \r\n" + email);
            }
            catch(Exception ex) { UniLogger.WriteLog("", 1, "Ошибка метода SendEmalToKontr: " + ex.Message); }
            finally {  }
        }

        public string GetLoadedLog(int idSupplier, int idpf)
        {
            return DBParserSettings.GetLogForKontr(idSupplier, idpf);
        }

        public void SetUnsetConfirm(int idpf, int confirmed, int whoconfirm)
        {
            SqlParameter _idpf = new SqlParameter("idpf", idpf);
            SqlParameter _confirmed = new SqlParameter("confirmed", confirmed);
            switch (confirmed)
            {
                case 0: //требует подтверждения, есть расхождение по ценам
                    DBExecutor.ExecuteQuery("update pricefutureh set confirmed = @confirmed, whoconfirm = null, dateconfirm = null where idpf = @idpf", _confirmed, _idpf);
                    break;
                case 1: //автоматическое подтверждение, нет расхождений по ценам
                    SqlParameter _whoconfirm = new SqlParameter("whoconfirm", whoconfirm);
                    DBExecutor.ExecuteQuery("update pricefutureh set confirmed = @confirmed, whoconfirm = @whoconfirm, dateconfirm = getdate() where idpf = @idpf", _confirmed, _whoconfirm, _idpf);
                    break;
                case 2: //ручное подтверждение РТК, НОЗ
                    SqlParameter _whoconfirm2 = new SqlParameter("whoconfirm", whoconfirm);
                    string s = @"update pricefutureh set confirmed = @confirmed, whoconfirm = @whoconfirm, dateconfirm = getdate(), 
                        datefrom = case when dateadd(day, 1, getdate()) > datefrom then cast(dateadd(day, 1, getdate()) as date) else datefrom end 
                        where idpf = @idpf";
                    DBExecutor.ExecuteQuery(s, _confirmed, _whoconfirm2, _idpf);
                    break;
            }
            
        }

        public DataTable PriceFutereAnalisesDetail(int idtov, DateTime datefrom)
        {
            SqlParameter _idtov = new SqlParameter("idtov", idtov);
            SqlParameter _datefrom = new SqlParameter("datefrom", datefrom);

            return DBExecutor.ExecuteProcedureTable("up_PriceFutureAnalysisDetail", _idtov, _datefrom);
        }
    }
}
