using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR
{
    public static class DeliveryAndRoad
    {
        public static DataTable GetRoad()
        {
            string sql = @"
select
	 id_route as idRoute
	,n_route as nRoute
	,id_road as idRoad
from spr_route r1
where r1.id_route in (select idRoute from sDirectDelivery)
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static DataTable GetRoadAll()
        {
            string sql = @"
select
	 id_route as Id
	,n_route as Name
    ,0 as fCheck
	,id_road as idRoad
from spr_route r1
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }

        public static DataTable GetPosition(int idRoute, DataTable dtNewData)
        {
            string sql = @"
select
	 r1.id_position as idPosition
	,r1.n_position as nPosition
	,r2.n_position as nGroup
	,case when exists (select * from rRoutePosition where idRoute = @idRoute and idPosition = r1.id_position) then 1 else 0 end as fCheck
from spr_position r1
		inner join spr_position r2 on r1.id_top_level = r2.id_position
where r1.id_level = 3
";

            SqlParameter paridRoute = new SqlParameter("idRoute", idRoute);
            var tbl = DBConnector.DBExecutor.SelectTable(sql, paridRoute);

            foreach(var row in tbl.AsEnumerable())
            {
                var newData = dtNewData.AsEnumerable().Where(p => int.Parse(p["idPosition"].ToString()) == int.Parse(row["idPosition"].ToString()) && (int)p["idRoute"] == idRoute).FirstOrDefault();

                if (newData != null)
                    row["fCheck"] = newData["fCheck"];
            }

            return tbl;
        }

        public static bool Save(DataTable dtNewData)
        {
            foreach (var row in dtNewData.AsEnumerable().Where(p => (int.Parse(p["fCheck"].ToString()) == 1)))
            {
                if (dtNewData.AsEnumerable().Where(p => int.Parse(p["fCheck"].ToString()) == 1 && int.Parse(p["idPosition"].ToString()) == int.Parse(row["idPosition"].ToString())).Count() > 1)
                    return false;
            }

            foreach(var row in dtNewData.AsEnumerable())
            {
                SqlParameter parIdRoute = new SqlParameter("idRoute", row["idRoute"]);
                SqlParameter parIdPosition = new SqlParameter("idPosition", row["idPosition"]);

                string sql = "";
                if (int.Parse(row["fCheck"].ToString()) == 1)
                {
                    sql = @"
if not exists (select * from rRoutePosition where idRoute = @idRoute and idPosition = @idPosition )
	insert into rRoutePosition(idRoute,idPosition)
	values(@idRoute, @idPosition)
";
                }
                else
                {
                    sql = @"
delete from rRoutePosition
where idRoute = @idRoute and idPosition = @idPosition
";
                }

                DBConnector.DBExecutor.ExecuteQuery(sql, parIdRoute, parIdPosition);
            }

            return true;
        }

        public static DataTable GetCheck()
        {
            string sql = @"
select
	 idRoute as idRoute
	,idPosition as idPosition
    ,1 as fCheck
from rRoutePosition
";
            return DBConnector.DBExecutor.SelectTable(sql);
        }
    }
}
