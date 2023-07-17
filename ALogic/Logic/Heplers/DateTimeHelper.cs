using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Heplers
{
    public static class DateTimeHelper
    {
        public static string ToWorkTime(DateTime? s)
        {
            if (s != null)
            {
                DateTime workdate = s.Value;
                int year = DateTime.Now.Year - workdate.Year;
                int month = Math.Abs(DateTime.Now.Month - workdate.Month);
                if (DateTime.Now.Month < workdate.Month)
                {
                    month = 12 - workdate.Month + DateTime.Now.Month;
                    year--;
                }
                return year.ToString() + " год. " + month.ToString() + " мес.";
            }
            return "";
        }
    }
}
