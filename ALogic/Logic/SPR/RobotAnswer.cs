using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR
{
    public static class RobotAnswer
    {
        public static void SendAnswer(int idRobot)
        {
            SqlParameter idRobotPar = new SqlParameter("idRobot", idRobot);
            DBConnector.DBExecutor.ExecuteQuery("update logRobot set dtLastAction = GetDate() where idRobot = @idRobot", idRobotPar);
        }

        public static int GetPeriod(int idRobot)
        {
            SqlParameter idRobotPar = new SqlParameter("idRobot", idRobot);
            return int.Parse(DBConnector.DBExecutor.SelectSchalar("select Period from sRobot where idRobot = @idRobot", idRobotPar).ToString());
        }
    }
}
