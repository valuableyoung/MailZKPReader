using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{
    public static class DBSprSegment
    {
        static DataTable list = GetSegmentForPlan();
        public static DataTable GetSegmentForPlan()
        {
            return DBExecutor.SelectTable("select idGroup, nGroup from v_rNameGroupForSegm where idGroup < 5");
        }

        public static int getIDSegmentByName(string name)
        {
            foreach (DataRow item in list.Rows)
            {
                if (name != "" && item[1].ToString().ToLower().Contains(name.ToLower())) return (int)item[0];
            }

            return -1;
        }


    }
}
