using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR.Old
{
    public static class DBTime
    {

        public static DataTable GetYears(int start, int end)
        {
            var result = new DataTable();
            result.Columns.Add("year", typeof(int));
            for (int i = start; i < end; i++)
                result.Rows.Add(DateTime.Now.Year + i);
            result.Rows.InsertAt(result.NewRow(), 0);
            return result;
        }



        public static List<Period> GetPeriod()
        {
            List<Period> period = new List<Period>();
            period.Add(new Period { nomKv = 0, name = "За ГОД" });
            period.Add(new Period { nomKv = 1, name = "За 1 Квартал" });
            period.Add(new Period { nomKv = 2, name = "За 2 Квартал" });
            period.Add(new Period { nomKv = 3, name = "За 3 Квартал" });
            period.Add(new Period { nomKv = 4, name = "За 4 Квартал" });

            return period;
        }


    }

    public class Period
    {
        public int nomKv { get; set; }
        public string name { get; set; }
    }
}
