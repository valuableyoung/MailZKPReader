using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALogic.DBConnector;
using ALogic.Logic.SPR;

namespace ALogic.Logic.Base
{
    public static class ConrtolPositionLogic
    {
        public static int GetControlPosition(string nControl)
        {
            var obj = DBControlPosition.GetControlPosition(nControl, User.CurrentUserId);
            if (obj != null)
                return int.Parse(obj.ToString());

            return -1;
        }

        public static void SaveControlPosition(string nControl, int pos)
        {
            DBControlPosition.SaveControlPosition(nControl, User.CurrentUserId, pos);
        }
    }
}
