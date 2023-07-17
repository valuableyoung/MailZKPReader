using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{
    public static class DBConvention
    {
        public static DataRow GetConventionZakTovByIdKontr(int idKontr)
        {
            SqlParameter idKontrPar = new SqlParameter("idKontr", idKontr);
            string str = "select top 1 * from contract where id_contract = 11 and idStatusContract in (0, 10, 20) and id_kontr = @idKontr";
            return DBExecutor.SelectRow(str, idKontrPar);
        }
    }
}
