using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ALogic.Model.EntityFrame
{
   public static class Context
    {    
        public static Entities Get()
        {            
            string encConnection = ConfigurationManager.ConnectionStrings[1].ConnectionString;
            var context = new Entities(encConnection);
            return context;
        }
    }
}
