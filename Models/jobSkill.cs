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
    
    public partial class jobSkill
    {
        public long jobSkillsID { get; set; }
        public long jobID { get; set; }
        public int skillsID { get; set; }
    
        public virtual jobDetail jobDetail { get; set; }
        public virtual skill skill { get; set; }
    }
}
