using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR
{
    public static class DBSprCur
    {
        public static DataTable GetAllSprCur()
        {
            return DBExecutor.SelectTable("select * from spr_cur");
        }

        public static DataRow GetSprCurByCurId(object cur_id)
        {
            SqlParameter curIdpar = new SqlParameter("cur_id", cur_id);
            return DBExecutor.SelectRow("select * from spr_cur where id_cur = @cur_id", curIdpar);
        }

        public static DataRow GetSprCurById1c(object id1c)
        {
            SqlParameter id1cpar = new SqlParameter("cur_id", id1c);
            return DBExecutor.SelectRow("select * from spr_cur where cur_id = @cur_id", id1cpar);
        }
    }
}
