using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ALogic.Logic.SPR
{
    public static class SprLogic
    {
        public static DataTable GetFirms()
        {
            string sql = @"
select 
	cast(k.id_kontr as int) as idFirm, k.n_kontr as nFirm, d.Abbreviation as Abbr
from 
	DetailOurFirm d (nolock)
		inner join spr_kontr k (nolock) on k.id_kontr = d.IdFirm
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }



        public static DataTable GetSprUnit()
        {
            string sql = @"
select 
    id_unit as idUnit
    ,n_unit as nUnit
from spr_unit (nolock)
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static object GetSprDirect()
        {
            string sql = @"
select   id_direct as idDirect
	, n_direct as nDirect
		
from spr_direct (nolock) where id_cond = 10
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static DataTable GetInvestorShareholder()
        {
            DataTable dtISH = new DataTable();
            dtISH.Columns.Add("Id", typeof(int));
            dtISH.Columns.Add("Value", typeof(string));

            dtISH.Rows.Add(5, "ЗС Р");
            dtISH.Rows.Add(6, "ЗС И");
            dtISH.Rows.Add(7, "ЗС Б");
            dtISH.Rows.Add(20, "АК");

            return dtISH;
        }

        public static object GetSegment()
        {
            DataTable dtISH = new DataTable();
            dtISH.Columns.Add("Id", typeof(int));
            dtISH.Columns.Add("Value", typeof(string));

            dtISH.Rows.Add(0, "Не задан");
            dtISH.Rows.Add(1, "СТО");
            dtISH.Rows.Add(2, "Магазин");
            dtISH.Rows.Add(3, "Автопарк");
            dtISH.Rows.Add(4, "Опт");

            return dtISH;
        }

        public static DataTable GetUrFiz()
        {
            DataTable dtYN = new DataTable();
            dtYN.Columns.Add("", typeof(int));
            dtYN.Columns.Add("Value", typeof(string));

            dtYN.Rows.Add(0, "Не выбрано");
            dtYN.Rows.Add(1, "Юр. лицо");
            dtYN.Rows.Add(2, "ИП");          

            return dtYN;
        }

        public static DataTable GetPost()
        {
            string sql = @"
select cast(id_post as int) as idPost, n_post as nPost 
from spr_post (nolock)
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static DataTable GetYesNo()
        {
            DataTable dtYN = new DataTable();
            dtYN.Columns.Add("Id", typeof(int));
            dtYN.Columns.Add("Value", typeof(string));

            dtYN.Rows.Add(1, "Да");
            dtYN.Rows.Add(0, "Нет");          

            return dtYN;
        }

        public static DataTable GetSalaryType()
        {
            DataTable dtST = new DataTable();
            dtST.Columns.Add("Id", typeof(int));
            dtST.Columns.Add("Value", typeof(string));

            dtST.Rows.Add(0, "Касса");
            dtST.Rows.Add(1, "На ЗС");
            dtST.Rows.Add(2, "На АК");          

            return dtST;
        }

        public static DataTable GetCondEmpl()
        {
            DataTable dtCE = new DataTable();
            dtCE.Columns.Add("Id", typeof(int));
            dtCE.Columns.Add("Value", typeof(string));

            dtCE.Rows.Add(10, "Активен");
            dtCE.Rows.Add(20, "Уволен");          

            return dtCE;
        }

        public static DataTable GetCondKontr()
        {
            string sql = @"
select cast(id_cond as int) as idCond, n_cond as nCond 
from s_CondKontr  (nolock)
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static DataTable GetTabelRowType()
        {
            string sql = @"
select 
  cast(id_type_row as int) as idTypeRow
 ,n_type_row as nTypeRow
from spr_tabel_row_type (nolock)
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static DataTable GetEmployers()
        {
            string sql = @"
select 
  cast(id_kontr as int) as idKontr
 ,n_kontr as nKontr
from spr_kontr (nolock)
where employer = 1
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static DataTable GetDecree()
        {
            string sql = @"
select 
  idDecree
 ,nDecree
from Decree (nolock)
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static object GetTypeSing()
        {
            DataTable dtCE = new DataTable();
            dtCE.Columns.Add("Id", typeof(string));
            dtCE.Columns.Add("Value", typeof(string));

            dtCE.Rows.Add("Генеральный директор", "Генеральный директор");
            dtCE.Rows.Add("ИО директора", "ИО директора");

            return dtCE;
        }

        public static object GetTypeDocTo1c()
        {
            string sql = @"
select 
	cast(id_type_doc as int) as idTypeDoc
   ,n_type_doc as nTypeDoc 
from spr_type_doc (nolock)
	where id_type_doc in (1, 11, 5, 117, 6, 23, 24, 130, 150, 49, 52, 124, 34, 37, 25, 8, 19, 22, 3, 18, 27, 193)
";
            var tbl = DBConnector.DBExecutor.SelectTable(sql);
            tbl.Rows.Add(0, "ВСЕ");

            return tbl;
        }

        public static DataTable GetBrands()
        {
            string sql = @"
select 
   tm_id as idBrand
  ,tm_name as nBrand
from spr_tm (nolock)
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }



    }
}
