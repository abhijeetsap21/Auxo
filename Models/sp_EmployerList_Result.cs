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
    
    public partial class sp_EmployerList_Result
    {
        public Nullable<long> Numero { get; set; }
        public long EmployerID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string role { get; set; }
        public string companyName { get; set; }
        public long companyID { get; set; }
        public System.DateTime dataIsCreated { get; set; }
        public System.DateTime dataIsUpdated { get; set; }
        public bool isActive { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<int> PageCount { get; set; }
        public Nullable<int> CurrentPageIndex { get; set; }
    }
}
