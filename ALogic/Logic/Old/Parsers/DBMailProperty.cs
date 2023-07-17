using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.Parsers
{
    public static class DBMailProperty
    {
        public static DataTable SelectSeMailKontrForAnswer()
        {
            return DBExecutor.SelectTable("Select * from SeMailKontr (nolock) where idTypeContact = 27 ");
        }

        public static DataTable SelectsALLParseFile()
        {
            return DBExecutor.SelectTable("Select * from sParseFile (nolock)");
        }

        public static DataRow SelectParseFile(object idKontr, object idTypeEdi)
        {
            SqlParameter par = new SqlParameter("idKontr", idKontr);
            SqlParameter parIdTypeEDI = new SqlParameter("idTypeEDI", idTypeEdi);
            return DBExecutor.SelectRow("Select * from sParseFile (nolock) where idTypeEDI = @idTypeEDI and idKOntr = @idKontr", par, parIdTypeEDI);
        }

        public static DataTable SelectsAllParseFileParm()
        {
            return DBExecutor.SelectTable("Select * from sParseFileParm (nolock)");
        }

        public static DataTable SelectsParseFileParm(object idParseFile)
        {
            SqlParameter par = new SqlParameter("idParseFile", idParseFile);
            return DBExecutor.SelectTable("select * from sParseFileParm (nolock) where idParseFile = @idParseFile", par);
        }

        public static void UpdatePriceOnlineTemp(int idUser, int idParserSetting, int kolInFile, int fCompetitorType)
        {
            SqlParameter paridUser = new SqlParameter("idUser", idUser);
            SqlParameter paridSetting = new SqlParameter("idParserSetting", idParserSetting);
            SqlParameter parkolInFile = new SqlParameter("kolInFile", kolInFile);
            if (fCompetitorType == 1)
            {
                DBExecutor.ExeciteProcedure("up_UpdatePriceOnlineTemp", paridUser, paridSetting, parkolInFile);
            }
            if (fCompetitorType == 2)
            {
                DBExecutor.ExeciteProcedure("up_UpdatePriceOnlineTempA1", paridUser, paridSetting, parkolInFile);
            }
            
        }
    }
}
