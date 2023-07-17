using ALogic.Logic.SPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.ArkonaBonus
{
    public class ArkonaBonusLogic
    {
        public static DataTable GetAll()
        {
            //            string sql = @"select idBrand
            //                                ,cast(retailBonus21 * 100  as decimal(18,2)) as retailBonus21
            //                                ,cast(retailBonus22 * 100  as decimal(18,2)) as retailBonus22
            //                                ,cast(retailBonus23 * 100  as decimal(18,2)) as retailBonus23
            //                                ,cast(optBonus * 100  as decimal(18,2)) as optBonus
            //                                ,cast(optASKUBonus*100 as decimal(18,2)) as optASKUBonus
            //                                ,cast(retailASKUBonus21*100 as decimal(18,2)) as retailASKUBonus21
            //                                ,cast(retailASKUBonus22*100 as decimal(18,2)) as retailASKUBonus22
            //                                ,cast(retailASKUBonus23*100 as decimal(18,2)) as retailASKUBonus23
            //                                ,dates
            //                                ,datee
            //                                ,cast(retailBonus20 * 100  as decimal(18,2)) as retailBonus20
            //                                ,cast(opt1Bonus * 100  as decimal(18,2)) as opt1Bonus
            //                                ,cast(opt2Bonus * 100  as decimal(18,2)) as opt2Bonus
            //                                ,cast(retailASKUBonus20 * 100  as decimal(18,2)) as retailASKUBonus20
            //                                ,cast(opt1ASKUBonus * 100  as decimal(18,2)) as opt1ASKUBonus
            //                                ,cast(opt2ASKUBonus * 100  as decimal(18,2)) as opt2ASKUBonus
            //                                from sArkonaBonus (nolock)
            //";
            
            //изменение расчета цен А+ и цен для эксклюзивного прайса Exist
            string sql = @"select idBrand 'Код бренда', spr_tm.tm_name 'Наименование бренда', tipname as 'Тип настроек',
                case when sArkonaBonusFraction.tovgr = -1 then '' else spr_tov_level4.tov_name end as 'Товарная группа',
                cast(minLevelRRP * 100 as numeric(18, 2)) as 'Мин. уровень РРЦ, %',
                cast(fractionPercent * 100 as numeric(18, 2)) as 'Доля, %',
                cast(BrandMarkup * 100 as numeric(18, 2)) as 'Наценка, %'
                from
                sArkonaBonusFraction (nolock)
                inner join spr_tm (nolock) on spr_tm.tm_id = sArkonaBonusFraction.idBrand
                left join spr_tov_level4 (nolock) on spr_tov_level4.tov_id = sArkonaBonusFraction.tovgr

                order by spr_tm.tm_name, sArkonaBonusFraction.tovgr, sArkonaBonusFraction.tip ";

            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static void Save(DataTable dt)
        {
            return;

//            if (dt == null)
//                return;

//            foreach (var row in dt.AsEnumerable())
//            {
//                if (decimal.Parse(row["opt"].ToString()) != 0 || decimal.Parse(row["rozn"].ToString()) != 0 || decimal.Parse(row["imag"].ToString()) != 0)
//                {
//                    string sql = @"
//if exists (select * from sArkonaBonus where idBrand = @idBrand)
//    update sArkonaBonus
//    set  opt = @opt / 100
//        ,rozn = @rozn / 100
//        ,imag = @imag / 100
//        ,dates = @dates
//        ,datee = @datee
//    where idBrand = @idBrand
//else
//    insert into sArkonaBonus(idBrand,opt,rozn,imag,dates,datee)
//    values(@idBrand, @opt / 100, @rozn / 100, @imag / 100, @dates, @datee)
//";
//                    SqlParameter paridBrand = new SqlParameter("idBrand", row["idBrand"]);
//                    SqlParameter paropt = new SqlParameter("opt", row["opt"]);
//                    SqlParameter parrozn = new SqlParameter("rozn", row["rozn"]);
//                    SqlParameter parimag = new SqlParameter("imag", row["imag"]);
//                    SqlParameter pardates = new SqlParameter("dates", row["dates"]);
//                    SqlParameter pardatee = new SqlParameter("datee", row["datee"]);

//                    DBConnector.DBExecutor.ExecuteQuery(sql, paridBrand, paropt, parrozn, parimag, pardates, pardatee);
//                }
//            }
        }

        /// <summary>
        /// Мегахреновина для показа всех начисленных бонусов, как А+ так и Ретробонусов, а также участие в программах лояльности. Исключаются только бонусы Эверикар
        /// </summary>
        /// <param name="date">дата, с которой начислялись бонусы</param>
        /// <returns>ничего не возвращает</returns>
        public static DataTable GetAllCustomerForBonus(DateTime date)
        {
            var idUser = User.CurrentUserId;

            if (!NeedFilter(idUser))
            {
                idUser = 0;
            }
            #region old for delete
            /*string sql = @"if object_id('tempdb..#tkontr') is not null drop table #tkontr
                create table #tkontr(idKontr int)

                create nonclustered index idx_idkontr on #tkontr(idkontr)

                if object_id('tempdb..#tdoc') is not null drop table #tdoc
                create table #tdoc(idDoc varchar(100), idKontr int, sumDoc decimal(18,2), sumPay decimal(18,2), sumBonus decimal(18,2), sumBonusNo decimal(18,2), sumBonusN decimal(18,2), sumBonusPay decimal(18,2))

                create nonclustered index idx_iddoc on #tdoc(iddoc)

                insert into #tkontr
                select spr_kontr.id_kontr from contract (nolock)
                inner join spr_kontr (nolock) on spr_kontr.id_kontr = contract.id_kontr and fArkonaBonus = 1
                and ISNULL(id_torg, 0) = 0 and spr_kontr.id_cond = 10
                inner join spr_agent_kontr (nolock) on spr_kontr.id_kontr = spr_agent_kontr.id_kontr
                and spr_agent_kontr.id_direct = 60
                and (spr_agent_kontr.id_agent = @idagent or @idagent = 0 
                 or (spr_agent_kontr.id_agent  in (select id_agent from spr_assistant (nolock) where id_assistant = @idagent ))
                 )

                insert into #tkontr
                select spr_kontr.id_kontr 
                from spr_kontr (nolock)
                inner join spr_agent_kontr (nolock) on PercentForBonuses > 0 and ISNULL(id_torg, 0) = 0  and spr_kontr.id_cond = 10
                and spr_kontr.id_kontr =  spr_agent_kontr.id_kontr
                and spr_agent_kontr.id_direct = 60
                and (spr_agent_kontr.id_agent = @idagent or @idagent = 0 
                 or (spr_agent_kontr.id_agent  in (select id_agent from spr_assistant (nolock) where id_assistant = @idagent ))
                  )

                insert into #tdoc
                select tov_doc.id_doc  as idDoc, tov_doc.id_kontr_db as idKontr, sum(tov.kol * tov.price) as sumDoc , 0, 

                case when isnull((select top 1 nTypeArkonaBonus from sTypeArkonaBonus (nolock) where idTypeArkonaBonus = 
                        (select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = tov_doc.id_kontr_db and fArkonaBonus = 1 and idStatusContract in (10, 20))), 'Ретро Бонус')
		                = 'Ретро Бонус' then sum(tov.kol * (tov.price - priceB)) 
		                else sum(floor(tov.kol * (tov.price - priceB))) * 1.00
                end
                /*sum(floor(tov.kol * (tov.price - priceB)))*/  //as sumBonus, 0, 0, 0
                                                                /*from tov_doc (nolock)
                                                                   inner join tov (nolock) on tov_doc.id_doc = tov.id_doc 
                                                                where  
                                                                     tov_doc.date_doc >= @date
                                                                 and tov_doc.id_kontr_db  in (select idkontr from  #tkontr)  
                                                                 and tov_doc.id_status_doc > 10
                                                                 and tov_doc.id_status_doc < 60
                                                                 and tov_doc.id_type_doc in (8, 19)
                                                                 and tov_doc.in_tax <> 10
                                                                group by tov_doc.id_doc,tov_doc.id_kontr_db

                                                                update #tdoc
                                                                set sumPay = isnull(sumlink, 0)
                                                                from 
                                                                #tdoc s
                                                                 left join 
                                                                 (select s.iddoc, sum(v_SumLinkDoc.sumlink) sumlink
                                                                    from #tdoc s
                                                                   inner  join v_SumLinkDoc (nolock) on v_SumLinkDoc.idDoc = s.iddoc 
                                                                   group by s.iddoc
                                                                   ) as linkdoc
                                                                on s.idDoc = linkdoc.iddoc   

                                                                update #tdoc
                                                                set  sumBonusN = sumBonus
                                                                where sumDoc <= sumPay + 1

                                                                update #tdoc
                                                                set  sumBonusNo = sumBonus
                                                                where sumDoc > sumPay + 1

                                                                update #tdoc
                                                                set sumBonusPay = isnull(sumlink, 0)
                                                                from 
                                                                 #tdoc s
                                                                left join 
                                                                (select s.iddoc , sum(link.sumlink) as sumlink
                                                                from #tdoc s
                                                                 left join link (nolock) on s.iddoc = link.idDoc 
                                                                and link.idTypeLink =  1
                                                                group by s.iddoc) as linkdoc
                                                                on s.iddoc = linkdoc.iddoc

                                                                insert into #tdoc
                                                                 select
                                                                  null,
                                                                  fin_doc.id_kontr_kr,
                                                                  0,
                                                                  0,
                                                                  0,--fin_doc.sum_f - isnull(t.SumLink, 0),
                                                                  0,
                                                                  0,
                                                                  fin_doc.sum_f - isnull(t.SumLink, 0)
                                                                 from fin_doc (nolock)
                                                                   left join (select idDoc, SUM( SumLink ) SumLink
                                                                        from link (nolock) where idTypeLink = 1
                                                                    group by idDoc) t on t.idDoc = fin_doc.id_doc
                                                                 where fin_doc.id_type_doc in (143, 188, 189) and fin_doc.sum_f > isnull(t.SumLink, 0 )
                                                                         and id_status_doc < 60

                                                                update #tdoc
                                                                set  sumBonusN = sumBonusN - isnull(sumBonusPay, 0)

                                                                select
                                                                  k.id_kontr as idKontr
                                                                 ,k.n_kontr as nKontr
                                                                 ,d.sumDoc
                                                                 ,d.sumBonus sumBonus
                                                                 ,d.sumBonusN
                                                                 ,d.sumBonusNo
                                                                 ,d.sumBonusPay
                                                                 ,isnull((select top 1 nTypeArkonaBonus from sTypeArkonaBonus (nolock) where idTypeArkonaBonus = 
                                                                        (select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = k.id_kontr and fArkonaBonus = 1 and idStatusContract in (10, 20))), 'Ретро Бонус') as nTypeArkonaBonus
                                                                 ,ag.n_kontr as Agent
                                                                from
                                                                 spr_kontr k (nolock)
                                                                 inner join spr_agent_kontr rag (nolock) on rag.id_direct = 60 and rag.id_kontr = k.id_kontr
                                                                 inner join spr_kontr ag (nolock) on ag.id_kontr = rag.id_agent
                                                                 left join
                                                                 ( 
                                                                  select idKontr, SUM(sumDoc) as sumDoc,  SUM(sumBonus) as sumBonus, SUM(sumBonusN) as sumBonusN, SUM(sumBonusNo) as sumBonusNo,  SUM(sumBonusPay) as sumBonusPay
                                                                  from #tdoc
                                                                  group by idKontr
                                                                 ) d on k.id_kontr = d.idKontr

                                                                where 
                                                                 k.id_kontr in (select idkontr from  #tKontr)
                                                                 and isnull((select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = k.id_kontr and fArkonaBonus = 1 and idStatusContract in (10, 20)), 2) <> 3";*/
                                                                /*upd MuhinAN 19.11.2020 - вынес все в процедуру*/
                                                                /*string sql = @"if object_id('tempdb..#tkontr') is not null drop table #tkontr
                                                                            create table #tkontr(idKontr int, idType int, npromo varchar(128))

                                                                            create nonclustered index idx_idkontr on #tkontr(idkontr)

                                                                            if object_id('tempdb..#tdoc') is not null drop table #tdoc
                                                                            create table #tdoc(idDoc varchar(100), idKontr int, sumDoc decimal(18,2), sumPay decimal(18,2), sumBonus decimal(18,2), 
                                                                                sumBonusNo decimal(18,2), sumBonusN decimal(18,2), sumBonusPay decimal(18,2))

                                                                            create nonclustered index idx_iddoc on #tdoc(iddoc)

                                                                            insert into #tkontr --Личные
                                                                            select spr_kontr.id_kontr, 1, (select dbo.f_GetPromoNameString(contract.id_kontr)) from contract (nolock)
                                                                            inner join spr_kontr (nolock) on spr_kontr.id_kontr = contract.id_kontr and fArkonaBonus = 1
                                                                            and ISNULL(id_torg, 0) = 0 and spr_kontr.id_cond = 10
                                                                            inner join spr_agent_kontr (nolock) on spr_kontr.id_kontr = spr_agent_kontr.id_kontr
                                                                            and spr_agent_kontr.id_direct = 60
                                                                            and (spr_agent_kontr.id_agent = @idagent or @idagent = 0 
                                                                             or (spr_agent_kontr.id_agent  in (select id_agent from spr_assistant (nolock) where id_assistant = @idagent ))
                                                                             )

                                                                            insert into #tkontr --Ретро
                                                                            select spr_kontr.id_kontr, 2, (select dbo.f_GetPromoNameString(spr_kontr.id_kontr)) 
                                                                            from spr_kontr (nolock)
                                                                            inner join spr_agent_kontr (nolock) on PercentForBonuses > 0 and ISNULL(id_torg, 0) = 0  and spr_kontr.id_cond = 10
                                                                            and spr_kontr.id_kontr =  spr_agent_kontr.id_kontr
                                                                            and spr_agent_kontr.id_direct = 60
                                                                            and (spr_agent_kontr.id_agent = @idagent or @idagent = 0 
                                                                             or (spr_agent_kontr.id_agent  in (select id_agent from spr_assistant (nolock) where id_assistant = @idagent ))
                                                                              )

                                                                            insert into #tkontr --промоакции и прочая
                                                                            select distinct r.idkontr, 3, (select dbo.f_GetPromoNameString(r.idkontr)) 
                                                                            from rBonusCastrolKontr r 
                                                                            inner join spr_kontr on spr_kontr.id_kontr = r.idKontr
                                                                            inner join spr_agent_kontr on spr_agent_kontr.id_kontr = r.idKontr
                                                                                and spr_agent_kontr.id_direct = 60
                                                                                and (spr_agent_kontr.id_agent = @idagent or @idagent = 0)
                                                                            where r.bonus > 0 and not exists(select 1 from #tkontr where idKontr = r.idKontr)

                                                                            insert into #tdoc
                                                                            select tov_doc.id_doc  as idDoc, tov_doc.id_kontr_db as idKontr, sum(tov.kol * tov.price) as sumDoc , 0, 

                                                                            --case when isnull((select top 1 nTypeArkonaBonus from sTypeArkonaBonus (nolock) where idTypeArkonaBonus = 
                                                                                    --(select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = tov_doc.id_kontr_db and fArkonaBonus = 1 and idStatusContract in (10, 20))), 'Ретро Бонус')
                                                                                    --= 'Ретро Бонус' then sum(tov.kol * (tov.price - priceB)) 
                                                                                    --else sum(floor(tov.kol * (tov.price - priceB))) * 1.00
                                                                            --end
                                                                            ----sum(floor(tov.kol * (tov.price - priceB)))  as sumBonus, 0, 0, 0,

                                                                            case when #tkontr.idType = 1 then sum(floor(tov.kol * (tov.price - priceB))) * 1.00
                                                                                 when #tkontr.idType in (2, 3) then sum(tov.kol * (tov.price - priceB))
                                                                            end as sumBonus, 0, 0, 0

                                                                            from tov_doc (nolock)
                                                                               inner join tov (nolock) on tov_doc.id_doc = tov.id_doc 
                                                                               inner join #tkontr on #tkontr.idKontr = tov_doc.id_kontr_db
                                                                            where  
                                                                                 tov_doc.date_doc >= @date
                                                                             --and tov_doc.id_kontr_db  in (select idkontr from  #tkontr)  
                                                                             and tov_doc.id_status_doc > 10
                                                                             and tov_doc.id_status_doc < 60
                                                                             and tov_doc.id_type_doc in (8, 19)
                                                                             and tov_doc.in_tax <> 10
                                                                            group by tov_doc.id_doc,tov_doc.id_kontr_db, #tkontr.idType

                                                                            --select *, sumBonus - sumBonus_NEW from #tdoc where sumBonus <> sumBonus_NEW

                                                                            update #tdoc
                                                                            set sumPay = isnull(sumlink, 0)
                                                                            from 
                                                                            #tdoc s
                                                                             left join 
                                                                             (select s.iddoc, sum(v_SumLinkDoc.sumlink) sumlink
                                                                                from #tdoc s
                                                                               inner  join v_SumLinkDoc (nolock) on v_SumLinkDoc.idDoc = s.iddoc 
                                                                               group by s.iddoc
                                                                               ) as linkdoc
                                                                            on s.idDoc = linkdoc.iddoc   

                                                                            update #tdoc
                                                                            set  sumBonusN = sumBonus
                                                                            where sumDoc <= sumPay + 1

                                                                            update #tdoc set  sumBonusNo = sumBonus where sumDoc > sumPay + 1

                                                                            update #tdoc
                                                                            set sumBonusPay = isnull(sumlink, 0)
                                                                            from 
                                                                             #tdoc s
                                                                            left join 
                                                                            (select s.iddoc , sum(link.sumlink) as sumlink
                                                                            from #tdoc s
                                                                             left join link (nolock) on s.iddoc = link.idDoc 
                                                                            and link.idTypeLink =  1
                                                                            group by s.iddoc) as linkdoc
                                                                            on s.iddoc = linkdoc.iddoc

                                                                            insert into #tdoc
                                                                             select
                                                                              null,
                                                                              fin_doc.id_kontr_kr,
                                                                              0,
                                                                              0,
                                                                              0,--fin_doc.sum_f - isnull(t.SumLink, 0),
                                                                              0,
                                                                              0,
                                                                              fin_doc.sum_f - isnull(t.SumLink, 0)
                                                                             from fin_doc (nolock)
                                                                               left join (select idDoc, SUM( SumLink ) SumLink
                                                                                    from link (nolock) where idTypeLink = 1
                                                                                group by idDoc) t on t.idDoc = fin_doc.id_doc
                                                                             where fin_doc.id_type_doc in (143, 188, 189) and fin_doc.sum_f > isnull(t.SumLink, 0 )
                                                                                     and id_status_doc < 60

                                                                            update #tdoc
                                                                            set  sumBonusN = sumBonusN - isnull(sumBonusPay, 0)

                                                                            select
                                                                              k.id_kontr as idKontr
                                                                             ,k.n_kontr as nKontr
                                                                             ,d.sumDoc
                                                                             ,d.sumBonus sumBonus
                                                                             ,d.sumBonusN
                                                                             ,d.sumBonusNo
                                                                             ,d.sumBonusPay
                                                                             --,isnull((select top 1 nTypeArkonaBonus from sTypeArkonaBonus (nolock) where idTypeArkonaBonus = 
                                                                            --        (select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = k.id_kontr and fArkonaBonus = 1 and idStatusContract in (10, 20))), 'Ретро Бонус') as nTypeArkonaBonus,
                                                                             ,case 
                                                                                when #tkontr.idType = 1 then case when len(isnull(#tkontr.npromo, '')) > 0 then 'Личные/' + isnull(#tkontr.npromo, '') else 'Личные' end
                                                                                when #tkontr.idType = 2 then case when len(isnull(#tkontr.npromo, '')) > 0 then 'Ретро Бонус/' + isnull(#tkontr.npromo, '') else 'Ретро Бонус' end
                                                                                else isnull(#tkontr.npromo, '')
                                                                             end as nTypeArkonaBonus
                                                                             ,ag.n_kontr as Agent
                                                                            from
                                                                             spr_kontr k (nolock)
                                                                             inner join #tkontr on #tkontr.idKontr = k.id_kontr
                                                                             inner join spr_agent_kontr rag (nolock) on rag.id_direct = 60 and rag.id_kontr = k.id_kontr
                                                                             inner join spr_kontr ag (nolock) on ag.id_kontr = rag.id_agent
                                                                             left join
                                                                             ( 
                                                                              select idKontr, SUM(sumDoc) as sumDoc,  SUM(sumBonus) as sumBonus, SUM(sumBonusN) as sumBonusN, SUM(sumBonusNo) as sumBonusNo,  SUM(sumBonusPay) as sumBonusPay
                                                                              from #tdoc
                                                                              group by idKontr
                                                                             ) d on k.id_kontr = d.idKontr
                                                                            where 
                                                                             k.id_kontr in (select idkontr from  #tKontr)
                                                                             and 
                                                                             isnull((select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = k.id_kontr and fArkonaBonus = 1 and idStatusContract in (10, 20)), 2) <> 3
                                                                              --and k.id_kontr in (select idKontr from rBonusCastrolKontr)
                                                                             order by nKontr";*/
                                                                //var idPost = ALogic.Logic.SPR.User.GetPostByUserId(idUser);
                                                                //SqlParameter paridPost = new SqlParameter("idPost", idPost);
            #endregion
            SqlParameter paridAgent = new SqlParameter("idAgent", idUser);
            SqlParameter pardate = new SqlParameter("date", date);
            //return DBConnector.DBExecutor.SelectTable(sql, paridAgent, pardate);
            return DBConnector.DBExecutor.ExecuteProcedureTable("up_ManageAPlusBonus", paridAgent, pardate);
            //return DBConnector.DBExecutor.ExecuteProcedureTable("up_ManageAPlusBonus_new", paridAgent, pardate);
        }

        private static bool NeedFilter(int idUser)
    {
        string sql = @"


     select  count(*)   
     from spr_kontr (nolock)
     where sql_id in (select memberuid 
          from sysusers (nolock) 
          inner join sysmembers 
          on sysusers.name = 'NotFilterAgent' and sysusers.uid = sysmembers.groupuid
          union
           select memberuid 
          from sysusers (nolock)
          inner join sysmembers 
          on (name = 'dbo' or name = 'developers')
          and sysusers.uid = sysmembers.groupuid)
            
     and id_kontr = @idAgent


";
        SqlParameter paridAgent = new SqlParameter("idAgent", idUser);

        var res = DBConnector.DBExecutor.SelectSchalar(sql, paridAgent);
        if (res.ToString() == "1") return false;
        return true;
    }

        //public static DataTable GetBonusForCustomer(object idKontr, DateTime date, object idPromo)
        public static DataTable GetBonusForCustomer(object idKontr, DateTime date)
        {
            string sql = @"
            declare @idType int = (select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = @idKontr and fArkonaBonus = 1 and idStatusContract in (10, 20))
            select *
            from uf_APlusBonus(@idKontr, @idType)
            where dateSf > @date
            order by dateSf desc, nomsf desc
            ";
            //string sql = @"
            //            select *
            //            from uf_APlusBonus_New(@idKontr, @idPromo)
            //            where dateSf > @date
            //            order by dateSf desc, nomsf desc 
            //            ";
            SqlParameter paridKontr = new SqlParameter("idKontr", idKontr);
            SqlParameter pariddate = new SqlParameter("date", date);
            //SqlParameter paridpromo = new SqlParameter("idPromo", idPromo);

            //return DBConnector.DBExecutor.SelectTable(sql, paridKontr, pariddate, paridpromo);
            return DBConnector.DBExecutor.SelectTable(sql, paridKontr, pariddate);
        }

        //public static DataTable GetBonusDetailForDoc(object idDoc, object idPromo)
        public static DataTable GetBonusDetailForDoc(object idDoc)
        {
            string sql = @"
            declare @idKontr int = (select id_kontr_db from tov_doc (nolock) where id_Doc = @idDoc)

            select
            *
            from uf_APlusBonusDtl(@idKontr)
            where id_Doc = @idDoc
            ";

            //string sql = @"
            //            declare @idKontr int = (select id_kontr_db from tov_doc where id_Doc = @idDoc)

            //            select
            //            *
            //            from uf_APlusBonusDtl_new(@idKontr, @idPromo)
            //            where id_Doc = @idDoc
            //            ";
            SqlParameter paridDoc = new SqlParameter("idDoc", idDoc);
            //SqlParameter paridpromo = new SqlParameter("idPromo", idPromo);

            //return DBConnector.DBExecutor.SelectTable(sql, paridDoc, paridpromo);
            return DBConnector.DBExecutor.SelectTable(sql, paridDoc);
        }

    public static string GenerateDocCorrBalanse(object idKontr, decimal sumCheck, string nTypeArkonaBonus, string AgentName)
    {
        SqlParameter paridKontr = new SqlParameter("idKontr", idKontr);
        var sum = sumCheck;   // int.Parse( DBConnector.DBExecutor.SelectSchalar("select dbo.uf_APlusBonusEorder(@idKontr)", paridKontr).ToString());

        if (sum != sumCheck)
            return "Данные устарели. Пожалуйста обновите окно!";
        int idTypeDoc = 0;
        int idTypeArkonaBonus = 0;
        object idConvention = null;
        if (nTypeArkonaBonus != "Ретро Бонус")
        {
            SqlParameter p3 = new SqlParameter("idKontr", idKontr);
            string sqlTypeArkonaBonus = "select top 1 idTypeArkonaBonus from contract (nolock) where id_kontr = @idKontr and idStatusContract in (10, 20) and fArkonaBonus = 1";
            idTypeArkonaBonus = int.Parse(DBConnector.DBExecutor.SelectSchalar(sqlTypeArkonaBonus, p3).ToString());
            idTypeDoc = idTypeArkonaBonus == 1 ? 189 : 188;
        }
        else
        {
                idTypeDoc = 143;
                //убрали для корпаративных бонусов которых нет

            }

            SqlParameter p1 = new SqlParameter("idKontr", idKontr);
        object idFirm = DBConnector.DBExecutor.SelectSchalar("select id_firm from spr_kontr (nolock) where id_kontr = @idKontr ", p1);

        SqlParameter p2 = new SqlParameter("idKontr", idKontr);
        object inTax = DBConnector.DBExecutor.SelectSchalar("select in_tax_def from spr_kontr (nolock) where id_kontr = @idKontr ", p2);
        inTax = idTypeArkonaBonus == 1 ? 0 : inTax;
        inTax = idTypeDoc == 143 ? 0 : inTax;

        if (nTypeArkonaBonus == "Ретро Бонус")
        {
                idConvention = SPR.DocLogic.GetidConventionBonusFor(idKontr, idFirm);
        }
        else
        {
                idConvention = SPR.DocLogic.GetidConventionBonus(idKontr);
        }


            string nomDoc = SPR.DocLogic.NextNom(idFirm, inTax, idTypeDoc);
        string idDoc = SPR.DocLogic.GetIdDoc(nomDoc, idFirm, inTax, idTypeDoc);

        object idKontrDb = (int)idTypeDoc == 188 ? idFirm : idKontr;
        object idKontrKr = (int)idTypeDoc == 188 ? idKontr : idFirm;

        SPR.DocLogic.GenerateFinDocBonus(idDoc, nomDoc, idFirm, inTax, idTypeDoc, idKontrDb, idKontrKr, idConvention, sum, AgentName);
        return "Документ создан. Пожалуйста проведите документ в модуле " + (idTypeArkonaBonus == 1 ? "Касса" : "Аналитика");
    }
}
}
