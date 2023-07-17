using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{
    public static class DBOurFirm
    {
        public static DataTable GetOurFirm()
        {
            return DBExecutor.SelectTable("select DetailOurFirm.IdFirm, spr_kontr.n_kontr from DetailOurFirm inner join spr_kontr on spr_kontr.id_kontr = DetailOurFirm.IdFirm where idFirmTransport > 0 order by spr_kontr.n_kontr");
        }

        public static DataTable GetOurFirmRems(DateTime date)
        {
            SqlParameter parDate = new SqlParameter("date", date);

            string query = "select " + "\n";
            query += "    DetailOurFirm.IdFirm, " + "\n";
            query += "    spr_kontr.n_kontr," + "\n";
            query += "    count(stockbase.kol) 	" + "\n";
            query += "from DetailOurFirm " + "\n";
            query += "    inner join spr_kontr on spr_kontr.id_kontr = DetailOurFirm.IdFirm" + "\n";
            query += "    inner join stockbase on stockbase.id_kontr = DetailOurFirm.IdFirm" + "\n";
            query += "where stockbase.date_rest = @date" + "\n";
            query += "group by DetailOurFirm.IdFirm, spr_kontr.n_kontr" + "\n";
            query += "order by spr_kontr.n_kontr" + "\n";

            return DBExecutor.SelectTable(query, parDate);
        }
    }
}
