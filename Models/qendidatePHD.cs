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
    
    public partial class qendidatePHD
    {
        public long phdid { get; set; }
        public long qenID { get; set; }
        public string collegeName { get; set; }
        public string collegeUniversity { get; set; }
        public string phdTitle { get; set; }
        public string courseField { get; set; }
        public string phdRemarks { get; set; }
        public Nullable<System.DateTime> phdStart { get; set; }
        public Nullable<System.DateTime> phdEnd { get; set; }
        public Nullable<System.DateTime> dataIsCreated { get; set; }
        public Nullable<System.DateTime> dataIsUpdated { get; set; }
    
        public virtual qendidateList qendidateList { get; set; }
    }
}
