using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ALogic.Model.EntityFrame
{
    public interface IDBEntity
    {
        int DbState { get; set; }
    }

    public partial class Entities
    {
        public void CleverAttach(IDBEntity entity)
        {
            string st = entity.ToString().Split('.').Last();
            this.AddObject(st, entity);
            this.ObjectStateManager.ChangeObjectState(entity, (EntityState)entity.DbState);
            
        }
    }

    public partial class CompanyEntity : IDBEntity
    {
        public int DbState { get; set; }
    }

    public partial class Company:IDBEntity
    {
        public int DbState { get; set; }
    }

    public partial class spr_kontr : IDBEntity
    {
        public int DbState { get; set; }

        public string JobTime { get; set; }
    }

    public partial class Decree : IDBEntity
    {
        public int DbState { get; set; }

        public List<rKontrDecree> listrKontrDecree { get; set; }

        public Dictionary<string, string> ErrorList { get; set; }
    }

    public partial class sAutoDelivery : IDBEntity
    {
        public int DbState { get; set; }
    }

    public partial class tabel : IDBEntity
    {
        public int DbState { get; set; }
    }

    public partial class sChildren : IDBEntity
    {
        public int DbState { get; set; }
    }
    public partial class rKontrDecree : IDBEntity
    {
        public int DbState { get; set; }
    }

    public partial class StaffMove : IDBEntity
    {
        public int DbState { get; set; }
    }

}
