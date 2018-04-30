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
    
    public partial class jobDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public jobDetail()
        {
            this.HiredCandidates = new HashSet<HiredCandidate>();
            this.InterViewerComments = new HashSet<InterViewerComment>();
            this.jobSavedQendidates = new HashSet<jobSavedQendidate>();
            this.jobSkills = new HashSet<jobSkill>();
            this.qenAppliedJobs = new HashSet<qenAppliedJob>();
            this.qendidateListInJobs = new HashSet<qendidateListInJob>();
            this.qendidateSavedForReferences = new HashSet<qendidateSavedForReference>();
            this.qendidateTestSchedules = new HashSet<qendidateTestSchedule>();
            this.qenInterviewSchedules = new HashSet<qenInterviewSchedule>();
            this.qenMialSendInteresteds = new HashSet<qenMialSendInterested>();
        }
    
        public long jobID { get; set; }
        public string jobTitle { get; set; }
        public string jobDescription { get; set; }
        public int workExpMin { get; set; }
        public int salary { get; set; }
        public string currency { get; set; }
        public string unit { get; set; }
        public int industryID { get; set; }
        public string city { get; set; }
        public int EmploymentTypeID { get; set; }
        public string EducationReq { get; set; }
        public int NoOfOpenings { get; set; }
        public long createdBy { get; set; }
        public System.DateTime dataIsCreated { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public Nullable<System.DateTime> dataIsUpdated { get; set; }
        public int jobStatusID { get; set; }
        public string jobContactPersonName { get; set; }
        public string jobContactPersonMobile { get; set; }
        public string jobContactPersonEmail { get; set; }
        public bool isActive { get; set; }
        public bool isDelete { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyWebsite { get; set; }
        public string responsesEmailID { get; set; }
        public string CompanyLogo { get; set; }
        public bool compeletPosted { get; set; }
        public bool salaryVisibleToEmployee { get; set; }
        public string otherinformation { get; set; }
        public long companyID { get; set; }
        public string gender { get; set; }
        public Nullable<long> advertisementRefID { get; set; }
        public string jobURL { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string streetNo { get; set; }
        public string Address { get; set; }
        public string zipCode { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<long> assessmentID { get; set; }
    
        public virtual advertisementList advertisementList { get; set; }
        public virtual companyDetail companyDetail { get; set; }
        public virtual EmployerDetail EmployerDetail { get; set; }
        public virtual EmployerDetail EmployerDetail1 { get; set; }
        public virtual EmploymentType EmploymentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HiredCandidate> HiredCandidates { get; set; }
        public virtual industry industry { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterViewerComment> InterViewerComments { get; set; }
        public virtual jobStatu jobStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jobSavedQendidate> jobSavedQendidates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jobSkill> jobSkills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenAppliedJob> qenAppliedJobs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidateListInJob> qendidateListInJobs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidateSavedForReference> qendidateSavedForReferences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidateTestSchedule> qendidateTestSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenInterviewSchedule> qenInterviewSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenMialSendInterested> qenMialSendInteresteds { get; set; }
    }
}
