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
    
    public partial class finalQendidateList
    {
        public long finalCandidateID { get; set; }
        public long jobID { get; set; }
        public long qenID { get; set; }
        public string candidateSalary { get; set; }
        public string candidatePosition { get; set; }
        public Nullable<System.DateTime> expectedJoiningDate { get; set; }
        public Nullable<System.DateTime> dataIsCreated { get; set; }
        public Nullable<System.DateTime> dataIsUpdated { get; set; }
    
        public virtual qendidateList qendidateList { get; set; }
    }
}
