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
    
    public partial class contract
    {
        public int idConvention { get; set; }
        public decimal id_firm { get; set; }
        public decimal id_kontr { get; set; }
        public int id_contract { get; set; }
        public Nullable<int> year { get; set; }
        public System.DateTime date_contract { get; set; }
        public string nom_contract { get; set; }
        public byte prolong { get; set; }
        public System.DateTime DateEndContract { get; set; }
        public Nullable<short> DaysKred { get; set; }
        public Nullable<short> idTypeDaysKred { get; set; }
        public string fModul { get; set; }
        public string commentary { get; set; }
        public string commentaryShort { get; set; }
        public Nullable<byte> fDefault { get; set; }
        public short idStatusContract { get; set; }
        public Nullable<byte> f1c { get; set; }
        public Nullable<int> SumCred { get; set; }
        public Nullable<int> RealDayCred { get; set; }
        public Nullable<decimal> MaxSumCred { get; set; }
        public Nullable<decimal> SummContract { get; set; }
        public string idDoc { get; set; }
        public short fBackOrder { get; set; }
        public short fMarketAgreement { get; set; }
        public short fSimpleFinPlan { get; set; }
        public Nullable<int> MethodCalcPlan { get; set; }
        public Nullable<int> idFormContract { get; set; }
        public Nullable<int> idTypeDelivery { get; set; }
        public Nullable<int> idTypePay { get; set; }
        public Nullable<int> idFormPay { get; set; }
        public int fEDI { get; set; }
        public string ComentForUPD { get; set; }
        public int idTerritory { get; set; }
        public int idDirect { get; set; }
        public int fTranzit { get; set; }
        public int idTranzitSklad { get; set; }
        public int fBackOrderInner { get; set; }
        public int fArkonaBonus { get; set; }
        public int idTypeArkonaBonus { get; set; }
        public Nullable<byte> fRoaming { get; set; }
        public string idFnsParticipant { get; set; }
        public string userName { get; set; }
    }
}
