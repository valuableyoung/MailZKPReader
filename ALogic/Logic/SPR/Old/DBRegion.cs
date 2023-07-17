using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{
    public static class DBRegion
    {
        public static DataTable GetRegionsForPlan()
        {
            var result = DBExecutor.SelectTable("select id_position as idRegion, n_position as nRegion  from spr_position where fInPlan = 1 ");
            result.Rows.Add(0, "Прочее");

            return result;
        }

        static DataTable list = GetRegionsForExcel();
        public static DataTable GetRegionsForExcel()
        {
            var result = DBExecutor.SelectTable("select id_position as idRegion, n_position as nRegion  from spr_position where id_level = 2 ");
            return result;
        }


        public static int getRegionIDByName(string name)
        {
            foreach (DataRow item in list.Rows)
            {
                if (name != "" && item[1].ToString().ToLower().Contains(name.ToLower())) return int.Parse(item[0].ToString());
            }

            return -1;
        }

    }
}
