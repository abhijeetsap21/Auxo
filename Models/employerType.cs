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
    
    public partial class employerType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employerType()
        {
            this.companyDetails = new HashSet<companyDetail>();
        }
    
        public int employerTypeID { get; set; }
        public string employerType1 { get; set; }
        public System.DateTime dataIsCreated { get; set; }
        public System.DateTime dataIsUpdated { get; set; }
        public bool isActive { get; set; }
        public bool isDelete { get; set; }
        public Nullable<bool> isSelected { get; set; }
        public Nullable<long> createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<companyDetail> companyDetails { get; set; }
        public virtual EmployerDetail EmployerDetail { get; set; }
        public virtual EmployerDetail EmployerDetail1 { get; set; }
    }
}
