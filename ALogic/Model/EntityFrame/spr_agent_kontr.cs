//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ALogic.Model.EntityFrame
{
    using System;
    using System.Collections.Generic;
    
    public partial class spr_agent_kontr
    {
        public decimal id_direct { get; set; }
        public decimal id_kontr { get; set; }
        public decimal id_agent { get; set; }
        public Nullable<System.DateTime> last_compare { get; set; }
        public Nullable<byte> compare { get; set; }
        public Nullable<byte> gr_compare { get; set; }
        public Nullable<int> category { get; set; }
        public Nullable<byte> id_day { get; set; }
        public Nullable<byte> id_week { get; set; }
        public Nullable<decimal> discount_real { get; set; }
        public Nullable<decimal> PerDeviation { get; set; }
    }
}
