using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALogic.Model.EntityFrame;

namespace ALogic.Model.HP
{
    public class DecreeEntity
    {
        public Decree decree { get; set; }

        public List<rKontrDecree> l_rKontrDecree { get; set; }
    }
}
