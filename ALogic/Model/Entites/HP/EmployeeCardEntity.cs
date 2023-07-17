using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ALogic.Model.EntityFrame;
using System.Data.SqlClient;
using System.Data.EntityClient;
using ALogic.Logic.Heplers;


namespace ALogic.Model.Entites.HP
{
    public class EmployeeCardEntity
    {
        public spr_kontr spr_kontr { get; set; }   

        public List<sChildren> listChildren { get; set; }
        public List<sAutoDelivery> listAvto { get; set; }   
        public List<tabel> listTabel { get; set; }
        public List<StaffMove> listStaffMove { get; set; }
        public List<rKontrDecree> listKontrDecree { get; set; }

        public Dictionary<string, string> ErrorList { get; set; }

        public EmployeeCardEntity(int id_kontr)
        {
            using (var content = Context.Get())
            {
                try
                {
                    var spr_kontr = (from i in content.spr_kontr
                                     where i.id_kontr == id_kontr
                                     select i).FirstOrDefault();

                    if (spr_kontr != null)
                        spr_kontr.JobTime = DateTimeHelper.ToWorkTime(spr_kontr.last_compare);

                    var sAutoDelivery = from i in content.sAutoDelivery where i.idDriverDefault == id_kontr select i;
                    var Children = from i in content.sChildren where i.idKontr == id_kontr select i;
                    var Tabel = from i in content.tabel where i.id_kontr == id_kontr && i.type_row != 10 select i;
                    var StaffMoves = from i in content.StaffMove where i.idKontr == id_kontr select i;
                    var KontrDecree = from i in content.rKontrDecree where i.IdKontr == id_kontr && i.fDossier == true select i;

                    this.spr_kontr = id_kontr == 0 ? new EntityFrame.spr_kontr() : spr_kontr;
                    this.listChildren = Children.ToList();
                    this.listAvto = sAutoDelivery.ToList();
                    this.listStaffMove = StaffMoves.ToList();
                    this.listTabel = Tabel.ToList();
                    this.listKontrDecree = KontrDecree.ToList();
                }
                catch (Exception ex)
                {
                    var mes = ex.ToString();
                }
            };
        }      

        public void Save()
        {
            using (var content = Context.Get())
            {
                if (spr_kontr.id_kontr == 0)
                {
                    var id = (from i in content.spr_kontr select i.id_kontr).Max();
                    spr_kontr.id_kontr = id + 1;

                    content.spr_kontr.Attach(spr_kontr);
                    content.ObjectStateManager.ChangeObjectState(spr_kontr, EntityState.Added);
                    content.SaveChanges();
                }
                else
                {
                    content.spr_kontr.Attach(spr_kontr);
                    content.ObjectStateManager.ChangeObjectState(spr_kontr, EntityState.Modified);
                }

                listChildren.ForEach(p => p.idKontr = spr_kontr.id_kontr);
                listAvto.ForEach(p => p.idDriverDefault = spr_kontr.id_kontr);
                listKontrDecree.ForEach(p => p.IdKontr = int.Parse(spr_kontr.id_kontr.ToString()));
                listStaffMove.ForEach(p => p.idKontr = int.Parse(spr_kontr.id_kontr.ToString()));

                listAvto.ForEach(p => content.CleverAttach(p));
                listChildren.ForEach(p => content.CleverAttach(p));
                listKontrDecree.ForEach(p => content.CleverAttach(p));
                listStaffMove.ForEach(p => content.CleverAttach(p));

                try
                {
                    content.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    ErrorList.Add("Errror", ex.InnerException.Message);
                }
            }
        }  
    }
}
