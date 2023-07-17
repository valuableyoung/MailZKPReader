using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Reload1C
{
    public class Reload1CParams
    {
        public bool FinDoc { get; set; }
        public bool TovDoc { get; set; }

        public int idTypeDoc { get; set; }
        public int idFirm { get; set; }

        public DateTime DateS { get; set; }
        public DateTime DateE { get; set; }


        public Reload1CParams()
        {
            FinDoc = true;
            TovDoc = true;
            idTypeDoc = 0;
            idFirm = 0;
            DateS = DateTime.Now.Date;
            DateE = DateTime.Now.Date;
        }
    }
}
