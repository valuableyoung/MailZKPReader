using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using ALogic.DBConnector;

namespace ALogic.Logic.SPR
{
    public class WindowAction
    {
        public int IdWE { get; set; }
        public string ButtonText { get; set; }
        public string ButtonImage { get; set; }
        public string KeyFlag { get; set; }
        public string Skey { get; set; }

        public static WindowAction GetAction(int idWE)
        {
            var dataRow = DBWindowAction.GetWindowAction(idWE);
            var result = new WindowAction();
            result.IdWE = idWE;
            result.ButtonText = dataRow["ButtonText"].ToString();
            result.ButtonImage = dataRow["ButtonImage"].ToString();
            result.KeyFlag = dataRow["KeyFlag"].ToString();
            result.Skey = dataRow["Skey"].ToString();

            return result;
        }

        public static DataTable GetAllActions()
        {
            DataTable dt = DBWindowAction.GetAllActions();
            dt.Columns.Add("Image", typeof(Image) );

            foreach(var row in dt.AsEnumerable())
            {
                Image img = Image.FromFile(row["ButtonImage"].ToString());
                row["Image"] = img;
            }
            return dt;
        }

        public static void Save(DataTable dataTable)
        {
            foreach (var row in dataTable.AsEnumerable().Where(p => p.RowState == DataRowState.Added ) )
            {
                DBWindowAction.Save(row["nWE"], row["ButtonText"], row["ButtonImage"], row["Number"], row["keyFlag"], row["Skey"]);
            }

            foreach (var row in dataTable.AsEnumerable().Where(p => p.RowState == DataRowState.Modified))
            {
                DBWindowAction.Update(row["idWE"], row["nWE"], row["ButtonText"], row["ButtonImage"], row["Number"], row["keyFlag"], row["Skey"]);
            }
        }

        public static void Delete(DataTable dataTable)
        {
            foreach (var row in dataTable.AsEnumerable())
            {
                DBWindowAction.Delete(row["idWE"]);
            }
        }
    }
}
