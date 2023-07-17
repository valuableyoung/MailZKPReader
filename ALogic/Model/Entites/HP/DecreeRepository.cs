using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ALogic.Model.EntityFrame;

namespace ALogic.Model.HP
{
    public static class DecreeRepository
    {
        public static DataTable GetAll()
        {
            return DBConnector.DBExecutor.SelectTable("select * from Decree");
        }

        public static DecreeEntity Get(int idDecree)
        {
            var content = Context.Get();

            DecreeEntity entity = new DecreeEntity();
           /* {
                decree = (from i in content.Decrees where i.IdDecree == idDecree select i).FirstOrDefault()
                ,
                l_rKontrDecree = (from i in content.rKontrDecrees where i.IdDecree == idDecree select i).ToList()
            };
            */
            return entity;
        }

        public static DecreeEntity Save(DecreeEntity entity)
        {
            var content = Context.Get();
            /*
            if (entity.decree.IdDecree == 0)
            {
                content.Decrees.InsertOnSubmit(entity.decree);
                content.SubmitChanges();

                entity.l_rKontrDecree.ForEach(p => p.IdDecree = entity.decree.IdDecree);
            }
            /*
               foreach(var el in entity.l_rKontrDecree.Where(p=>p.))
             * */
            
            content.SaveChanges();
            return entity;

        }
    }
}
