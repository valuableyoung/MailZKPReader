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
    
    public partial class StaffMove
    {
        public int IdStaffMove { get; set; }
        public int idKontr { get; set; }
        public int idPost { get; set; }
        public System.DateTime DateS { get; set; }
        public Nullable<System.DateTime> DateE { get; set; }
        public int idUnit { get; set; }
        public string nDecree { get; set; }
        public int idFirm { get; set; }
    }
}
