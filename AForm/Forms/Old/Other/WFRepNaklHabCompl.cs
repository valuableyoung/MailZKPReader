using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AForm.Base;
using ALogic.DBConnector;

namespace AForm.Forms.Old.Other
{
    public partial class WFRepNaklHabCompl : Form
    {
        public WFRepNaklHabCompl()
        {
            InitializeComponent();
            this.Tag = "gsergbeasrbsehnsjrt6m";
            gcMain.GV.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            string sql = @"

if object_id('tempdb..#tmp') is not null drop table #tmp
create table #tmp
(
     idDoc varchar(100)
    ,datedoc datetime
    ,dateBron datetime
    ,dateNab datetime
    ,datenNab datetime
    ,dateCompl datetime
    ,sku int
    ,mass decimal(18,2)
    ,napravl varchar(100)
    ,dayK varchar(100)
)

--insert into @tmp(idDoc, datedoc, dateBron, dateNab,datenNab, dateCompl, sku, mass, napravl, dayK)
insert into #tmp(idDoc, datedoc, dateBron, dateNab,datenNab, dateCompl, sku, mass, napravl, dayK)
select
     tov_doc.id_doc
    ,date_doc
    ,case when lg_per.dtBron is null then lg_bron.dtBron else lg_per.dtBron end
    ,lg_nab.dtNab
    ,lg_nnab.dtNab
    ,lg_compl.dtCompl
    ,lg_kol.kTov
    ,lg_w.wTov
    ,r.n_route
    ,case DATEPART(weekday, lg_compl.dtCompl)
     when 1 then 'ВС'
      when 2 then 'ПН'
       when 3 then 'ВТ'
        when 4 then 'СР'
         when 5 then 'ЧТ'
          when 6 then 'ПТ'
           when 7 then 'СБ' end
from
    tov_doc     (nolock)
        left join (select min(LogeOrder.dateAction) dtBron,LogeOrder.iddoc
                    from LogeOrder  with (nolock) where idTypeAction = 24
                    group by LogeOrder.iddoc ) lg_per on lg_per.iddoc = tov_doc.id_doc

        left join (select min(LogeOrder.dateAction) dtBron,LogeOrder.iddoc
                    from LogeOrder  with (nolock) where idTypeAction = 18
                    group by LogeOrder.iddoc ) lg_bron on lg_bron.iddoc = tov_doc.id_doc
        left join (select MIN(logSkladDoc.datetimeChange) dtNab, logSkladDoc.idDoc
                    from logSkladDoc with (nolock) where idSkladStatus = 110
                    group by logSkladDoc.idDoc) lg_nab on lg_nab.idDoc = tov_doc.id_doc
        left join (select MIN(logSkladDoc.datetimeChange) dtNab, logSkladDoc.idDoc
                    from logSkladDoc with (nolock) where idSkladStatus = 130
                    group by logSkladDoc.idDoc) lg_nnab on lg_nnab.idDoc = tov_doc.id_doc
        left join (select min(logSkladDoc.datetimeChange) dtCompl, logSkladDoc.idDoc
                    from logSkladDoc with (nolock) where idSkladStatus = 230
                    group by logSkladDoc.idDoc) lg_compl on lg_compl.idDoc = tov_doc.id_doc
        left join (select COUNT(distinct id_tov) as kTov, d.id_doc
                    from tov with (nolock)  inner join tov_doc d with (nolock) on d.id_doc = tov.id_doc and d.date_doc between @dates and @datee
                    group by d.id_doc ) lg_kol on lg_kol.id_doc = tov_doc.id_doc
        left join (select sum(s.weight) as wTov, d.id_doc
                    from tov with (nolock)
                        inner join tov_doc d with (nolock) on d.id_doc = tov.id_doc and d.date_doc between @dates and @datee
                        inner join spr_tov s with (nolock) on s.id_tov = tov.id_tov
                    group by d.id_doc ) lg_w on lg_w.id_doc = tov_doc.id_doc
        left join spr_trans t on tov_doc.id_trans = t.id_trans
        left join spr_route r on r.id_route = t.id_route

where
        date_doc between @dates and @datee
    and (id_type_doc in (8, 19) or (id_type_doc = 27 and id_addition_db = 21) )
    and in_tax in (0, 11)
    and id_status_doc < 60


    update #tmp
        set dateBron = DATEADD( mi, (1+DATEPART(mi, dateBron) / 30) * 30,  DATEADD( hh, datepart(hh, dateBron), cast(cast(dateBron as date) as datetime)) )
           ,dateCompl = DATEADD( mi, (1+DATEPART(mi, dateCompl) / 30) * 30,  DATEADD( hh, datepart(hh, dateCompl), cast(cast(dateCompl as date) as datetime)) )
           ,dateNab = DATEADD( mi, (1+DATEPART(mi, dateNab) / 30) * 30,  DATEADD( hh, datepart(hh, dateNab), cast(cast(dateNab as date) as datetime)) )
           ,datenNab = DATEADD( mi, (1+DATEPART(mi, dateNab) / 30) * 30,  DATEADD( hh, datepart(hh, dateNab), cast(cast(dateNab as date) as datetime)) )

    select
          convert(varchar(10),datedoc, 104) as [Дата док]
        , convert(varchar(10),dateBron, 104)as [Дата на бронь]
        , convert(varchar(5),dateBron, 108)as [Время на бронь]
        , convert(varchar(10),dateNab, 104)as [Дата в наборку]
        , convert(varchar(5),dateNab, 108)as [Время в наборку]
        , convert(varchar(10),datenNab, 104)as [Дата начала наборки]
        , convert(varchar(5),datenNab, 108)as [Время начала наборки]
        , convert(varchar(10),dateCompl, 104)as [Дата в комплектацию]
        , convert(varchar(5),dateCompl, 108)as [Время в комплектацию]
        , sku as [Кол-во скю]
        , mass as [Масса]
        , napravl as [Направление]
        , dayK  as [День недели]
    from #tmp 

";
            SqlParameter p1 = new SqlParameter("dates", dts.Value.Date);
            SqlParameter p2 = new SqlParameter("datee", dtpo.Value.Date);

            DataTable dt = DBExecutor.SelectTable(sql, p1, p2);
            
            gcMain.DataSource = dt;
        }
    }
}
