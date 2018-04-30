using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewLetter.Models
{
    public class ResumeModel
    {
        public qendidateList personainfo { get; set; }
        public AcademicModel educationinfo { get; set; }
        public List<qendidatePHD> phdinfo { get; set; }
        public List<qenEmpDetail> employmentinfo { get; set; }
        public List<qenReference> refrences { get; set; }
        public List<qenSkill> skills { get; set;  }
    }

    public class companyRegistrationModel
    {
        public companyDetail companyInfo { get; set; }
        public EmployerDetail employerInfo { get; set; }

    }

    public class companyRegistrationEmailModel
    {
        public string from { get; set; }
        public string to { get; set; }

        public string subject { get; set; }
        public string body { get; set; }

    }
    public class candidateRegistratioEmailModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
   public class qendidateSearchmodel
    {
        
        public long qenid { get; set; }
        public string qenname { get; set; }
        public string qenexperience { get; set; }
        public string qenskills { get; set; }
        public double qensalary { get; set; }
        public int qencategory { get; set; }        
        public int jobid { get; set; }
        public bool selfApplied { get; set; }
        public string linkedINURL { get; set; }
    }
    public class hiredCandidateList
    {
        public long qenID { get; set; }
        public string qenName { get; set; }
        public string jobtitle { get; set; }
        public string dateOfJoining { get; set; }
        public long salary { get; set; }
    }

    public class advertisementApplyHome
    {
        
        public string jobLogo { get; set; }
        public string companyName { get; set; }
        public string jobCity { get; set; }
        public string jobDescription { get; set; }
        public long refID { get; set; }
        public long jobID { get; set; }
        public long empdID { get; set; }
        public long companyID { get; set; }

    }
    public class LinkedINVM
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }


    public class LinkedINResVM
    {
        public string firstName { get; set; }
        public string headline { get; set; }
        public string id { get; set; }
        public string lastName { get; set; }
        public Sitestandardprofilerequest siteStandardProfileRequest { get; set; }
        public string emailaddress { get; set; }
        public string pictureurl { get; set; }
       // public string location { get; set; }
        public string publicprofileurl { get; set; }
        public string summary { get; set; }



    }

    public class Sitestandardprofilerequest
    {
        public string url { get; set; }
    }

}