//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewLetter.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class slotTempBlocked
    {
        public long blockedSlotTempID { get; set; }
        public long companyID { get; set; }
        public int SlotID { get; set; }
        public System.DateTime forDate { get; set; }
        public long EmployerID { get; set; }
        public System.DateTime dataIsCreated { get; set; }
        public System.DateTime dataIsUpdated { get; set; }
        public long modifyBy { get; set; }
        public bool isDeleted { get; set; }
    
        public virtual companyDetail companyDetail { get; set; }
        public virtual EmployerDetail EmployerDetail { get; set; }
        public virtual EmployerDetail EmployerDetail1 { get; set; }
        public virtual slot slot { get; set; }
    }
}
