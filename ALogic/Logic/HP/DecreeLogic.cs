using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ALogic.Model.EntityFrame;

namespace ALogic.Logic.HP
{
    public class DecreeLogic
    {
        public static DataTable GetAll()
        {
            return DBConnector.DBExecutor.SelectTable("select IdDecree, nDecree, NomDecree, TitleDecree, DateDecree from Decree");
        }

        public static Decree Create()
        {
            Decree d = new Decree();
            d.listrKontrDecree = new List<rKontrDecree>();
            d.DateDecree = DateTime.Now;
            d.Content = "";
            d.nDecree = "Новый приказ";
            d.NomDecree = "";
            d.nSign = "Селютин В. Д.";
            d.nTypeSign = "Генеральный директор";

            return d;
        }

        public static Decree Get(int idDecree = 0)
        {
            using (var c = Context.Get())
            {
                var e = (from i in c.Decree where i.IdDecree == idDecree select i).FirstOrDefault();
                if (e == null)
                    return Create();

                e.listrKontrDecree = (from i in c.rKontrDecree where i.IdDecree == idDecree select i).ToList();
                return  e;      
            }          
        }

        public static int Save(Decree d)
        {
            if (IsValid(d))
            {
                using (var c = Context.Get())
                {
                    c.Decree.Attach(d);
                    c.ObjectStateManager.ChangeObjectState(d, d.IdDecree == 0 ? EntityState.Added : EntityState.Modified);                  
                    c.SaveChanges();

                    d.listrKontrDecree.ForEach(p => p.IdDecree = d.IdDecree);
                    d.listrKontrDecree.ForEach(p => c.CleverAttach(p));
                    c.SaveChanges();

                    return d.IdDecree;
                }
            }
            return d.IdDecree;
        }

        private static bool IsValid(Decree d)
        {
            d.ErrorList = new Dictionary<string, string>();
            if (d.Content == null)
                  d.ErrorList.Add("Content", "Не зполнено поле содержание");
            if (d.DateDecree == DateTime.MinValue)
                d.ErrorList.Add("DateDecree", "Не заполнено поле содержание");
            if (d.nDecree == null || d.nDecree.Length == 0)
                d.ErrorList.Add("nDecree", "Не заполнено поле наименование");
            if (d.NomDecree == null || d.NomDecree.Length == 0)
                d.ErrorList.Add("NomDecree", "Не заполнено поле номер");
            if (d.TitleDecree == null || d.TitleDecree.Length == 0)
                d.ErrorList.Add("TitleDecree", "Не заполнено поле заголовок");

            return d.ErrorList.Count == 0;
        }     

        public static void Delete(int idDecree)
        {
            using (var c = Context.Get())
            {
                var e = (from i in c.Decree where i.IdDecree == idDecree select i).FirstOrDefault();
                if (e != null)
                    c.DeleteObject(e);
                var ldata = (from i in c.rKontrDecree where i.IdDecree == idDecree select i).ToList();
                ldata.ForEach(p => c.DeleteObject(p));
                c.SaveChanges();
            }
        }
    }
}
