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
    
    public partial class courseSpecialization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public courseSpecialization()
        {
            this.qendidateGraduations = new HashSet<qendidateGraduation>();
            this.qendidatePGraduations = new HashSet<qendidatePGraduation>();
        }
    
        public long courseSpecialization1 { get; set; }
        public long courseTypeID { get; set; }
        public string specializationName { get; set; }
        public System.DateTime dataIsCreated { get; set; }
        public System.DateTime dataIsUpdated { get; set; }
        public long createdBy { get; set; }
        public long modifiedBy { get; set; }
        public bool isActive { get; set; }
    
        public virtual courseType courseType { get; set; }
        public virtual EmployerDetail EmployerDetail { get; set; }
        public virtual EmployerDetail EmployerDetail1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidateGraduation> qendidateGraduations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidatePGraduation> qendidatePGraduations { get; set; }
    }
}