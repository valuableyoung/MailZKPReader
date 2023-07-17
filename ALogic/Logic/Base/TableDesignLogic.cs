using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ALogic.DBConnector;
using ALogic.Logic.SPR;

namespace ALogic.Logic.Base
{
    public static class TableDesignLogic
    {

      /*  public int idTableDesign { get; set; }
        public string nTable { get; set; }
        public int idUser { get; set; }
        public string strDesign { get; set; }*/

        public static MemoryStream GetDesign(string tablename)
        {
            object val = DBTableDesign.getProperties(tablename, User.CurrentUserId);
            if (val == null)
                return null;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(val.ToString() ?? ""));

            return stream;
        }
        public static MemoryStream GetDesignDefault(string tablename)
        {
            object val = DBTableDesign.getResetProperties(tablename);
            if (val == null)
                return null;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(val.ToString() ?? ""));

            return stream;
        }




        public static void SaveDesign(string tablename, MemoryStream stream)
        {           
            DBTableDesign.saveProperties(tablename,  Encoding.UTF8.GetString(stream.ToArray()), User.getInstance().IdUser);
        }



        
    }
}
