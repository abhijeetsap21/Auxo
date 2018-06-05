using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace NewLetter.Models
{
    [MetadataType(typeof(qendidateListValidation))]
    public partial class qendidateList
    {
        public int qenCategory { get; set; }
        public long jobid { get; set; }


    }
    //[MetadataType(typeof(qenrefvalidation))]
    //public partial class qenReference
    //{

    //}
    [MetadataType(typeof(jobDetailsvalidation))]
    public partial class jobDetail
    {

        public string requiredSkills { get; set; }
    }
    [MetadataType(typeof(qengradutionvalidtion))]
    public partial class qendidateGraduation
    {


    }

    [MetadataType(typeof(qenpgradutionvalidtion))]
    public partial class qendidatePGraduation
    {


    }

    [MetadataType(typeof(EmpDetails))]
    public partial class qenEmpDetail
    {


    }

    [MetadataType(typeof(ValidateSecondary))]
    public partial class qenSecondary
    {

    }

    [MetadataType(typeof(HCValidateSecondary))]
    public partial class qenHigherSecondary
    {

    }

    [MetadataType(typeof(HCValidateCertificate))]
    public partial class qendidatePHD
    {

    }

    [MetadataType(typeof(educationTypevalidation))]
    public partial class educationType
    {

    }

    [MetadataType(typeof(courseTypeSPValidation))]
    public partial class usp_GetcourseTypesByeducationTypeIDandPageIndex_Result
    {

    }

    [MetadataType(typeof(courseTypeValidation))]
    public partial class courseType
    {

    }
    public partial class courseTypeValidation
    {
        [Required(ErrorMessage = "Course name is required")]
        [Display(Name = "Course Name")]
        public string courseName { get; set; }

        [Required(ErrorMessage = "Education type is required")]
        [Display(Name = "Education Type")]
        public int educationTypeID { get; set; }
    }



    public partial class courseTypeSPValidation
    {
        [Display(Name = "Course Name")]
        public string courseName { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime dataIscCreated { get; set; }
    }

    public class educationTypevalidation
    {
        [Required(ErrorMessage = "Name is requied")]
        [Display(Name = "Education Type")]
        public string educationTypeName { get; set; }
                
        [Display(Name = "Active / In-Active")]
        public bool isActive { get; set; }
    }

    public class HCValidateCertificate
        {
        [Required(ErrorMessage = "Certificate name is required")]
        public string courseField { get; set; }

        [Required(ErrorMessage = "Authority name is required")]
        public string collegeUniversity { get; set; }

        [Required(ErrorMessage = "Licence Number: is required")]
        public string phdTitle { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime phdStart { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime phdEnd { get; set; }

        

    }



    public class jobDetailsvalidation
    {
        public long jobID { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "200 charectar max")]
        [Required(ErrorMessage = "please enter job title")]
        [Display(Name = "Job Title")]
        public string jobTitle { get; set; }

        //[AllowHtml]
        [Display(Name = "Job Description")]       
        [Required(ErrorMessage = "job description required")]
        public string jobDescription { get; set; }

        [Display(Name = "Openings")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "no of openings required")]
        public int NoOfOpenings { get; set; }

        [Required(ErrorMessage = "please enterrequired skills ")]
        [Display(Name = "Required Skills")]
        public string requiredSkills { get; set; }

        [Display(Name = "Experience(year)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "year of experience required")]
        public int workExpMin { get; set; }

        [Display(Name = "Salary(Monthly)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "salary (gross salary) required")]
        public int salary { get; set; }

        [Display(Name = "Industry")]
        [Required(ErrorMessage = "please select industry")]
        public int industryID { get; set; }

        [Required(ErrorMessage = "please select Education")]
        [Display(Name = "Education")]
        public string EducationReq { get; set; }

        [Display(Name = "Currency")]
        [Required(ErrorMessage = "please select currency")]
        public string currency { get; set; }

       

        [Required(ErrorMessage = "please enter your city")]
        public string city { get; set; }

        [Required(ErrorMessage = "please enter your state")]
        public string state { get; set; }

        [Required(ErrorMessage = "please enter your country")]
        public string country { get; set; }

        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "please enter your zipCode")]
        public string zipCode { get; set; }

        [Display(Name = "Street No")]
        public string streetNo { get; set; }

        [Display(Name = "Salry visible to employee")]
        public bool salaryVisibleToEmployee { get; set; }


        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name required")]
        public string CompanyName { get; set; }



        [StringLength(200, MinimumLength = 3, ErrorMessage = "200 charectar max")]
        [Display(Name = "Company Website")]       
        public string CompanyWebsite { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = "30 charectar max")]
        [Display(Name = "Contact person")]
        [Required(ErrorMessage = "Contact person required")]
        public string jobContactPersonName { get; set; }

        [Required(ErrorMessage = "please enter contact person number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Contact Number")]
        public string jobContactPersonMobile { get; set; }
        [AllowHtml]
        [Display(Name = "Additional Information")]
        public string otherinformation { get; set; }

        [Display(Name = "Resume received on mail")]
        [EmailAddress]
        [Required(ErrorMessage = "Email ID required ")]
        public string responsesEmailID { get; set; }

        [AllowHtml]
        [Display(Name = "Company Description")]
        public string CompanyDescription { get; set; }

      
        [Display(Name = "Contact Email")]
        public string jobContactPersonEmail { get; set; }

        [Display(Name = "Employment Type")]
        [Required(ErrorMessage = "Employment Type required ")]
        public string EmploymentTypeID { get; set; }
        

    }
    public class ValidateSecondary
    {
        [Display(Name ="School Name")]
        [Required(ErrorMessage = "please enter school name")]
        public string schoolName {get;set;}

        [Display(Name = "Year of passing")]        
        [Required(ErrorMessage = "please select passing year")]
        public int? secondaryPassYear { get; set; }

        [Display(Name = "Grade Or Percentage")]
        [Required(ErrorMessage = "please select percentage/grade")]
        public string secondaryGradeOrPercentage { get; set; }

        [Display(Name = "Board")]
        [Required(ErrorMessage = "please select board")]
        public string secondaryBoard { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "please select city")]
        public string city { get; set; }

    }
    public class HCValidateSecondary
    {
        [Display(Name = "School Name")]
        [Required(ErrorMessage = "please enter school name")]
        public string schoolName { get; set; }

        [Display(Name = "Year of passing")]
        [Required(ErrorMessage = "please select passing year")]
        public int? hSecondaryPassYear { get; set; }

        [Display(Name = "Grade Or Percentage")]
        [Required(ErrorMessage = "please select percentage/grade")]        
        public string hsecondaryGradeOrPercentage { get; set; }

        [Display(Name = "Board")]
        [Required(ErrorMessage = "please select board")]
        public string hSecondaryBoard { get; set; }

        [Display(Name = "Stream")]
        [Required(ErrorMessage = "please select stream")]
        public string hSecondaryStream { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "please select city")]
        public string city { get; set; }
    }
    public class EmpDetails
    {
        [Required(ErrorMessage = "please enter company name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "please enter job location")]
        public string jobLocation { get; set; }

        [Required(ErrorMessage = "please enter your postition")]
        public string qenPosition { get; set; }

        [Required(ErrorMessage = "please enter salary")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid salary detail")]
        public System.Nullable<double> qenSalary { get; set; }

        [AllowHtml]
        [Display(Name = "Responsibilities")]
        public string jobDescription { get; set; }
        
        [Required(ErrorMessage ="please select month")]
        public int empStartMonth { get; set; }

        [Required(ErrorMessage = "please select year")]
        public int empStartYear { get; set; }

        [Required(ErrorMessage = "please select month")]
        public int empEndMonth { get; set; }

        [Required(ErrorMessage = "please select year")]
        public int empEndYear { get; set; }


    }
    public class EmployerModel
    {
        public EmployerModel()
        {
            employers = new List<qenEmpDetail>();
        }
        public long qenid { get; set; }
        
        public List<qenEmpDetail> employers { get; set; }
    }
    public class qengradutionvalidtion
    {
        [Display(Name = "Graduation Year")]
        [Required(ErrorMessage = "please select passing year")]
        public int? YearPassing { get; set; }

        [Display(Name = "Grade/Percentage")]
        [Required(ErrorMessage = "please select grade/percentage")]       
        public string gradPercentageorGrade { get; set; }

        [Display(Name = "Specialization")]
        [Required(ErrorMessage ="Course field is required")]
        public long courseField { get; set; }

        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Course name is required")]
        public long courseName { get; set; }

        [Display(Name = "University")]
        [Required(ErrorMessage = "University name is required")]
        public string collegeUniversity { get; set; }

        [Display(Name = "College")]
        [Required(ErrorMessage = "College name is required")]
        public string collegeName { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "city name is required")]
        public string city { get; set; }



    }

    public class qenpgradutionvalidtion
    {
        [Display(Name = "Post Graduation Year")]
        [Required(ErrorMessage = "please select passing year")]
        public int? YearPassing { get; set; }

        [Display(Name = "Grade/Percentage")]
        [Required(ErrorMessage = "please select grade/percentage")]
        public string pgradPercentageorGrade { get; set; }

        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Course field is required")]
        public long courseField { get; set; }

        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Course name is required")]
        public long courseName { get; set; }

        [Display(Name = "University")]
        [Required(ErrorMessage = "University name is required")]
        public string collegeUniversity { get; set; }

        [Display(Name = "College")]
        [Required(ErrorMessage = "College name is required")]
        public string collegeName { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "city name is required")]
        public string city { get; set; }



    }
    public class qenrefvalidation
    {
        public long qenRefID1 { get; set; }
        public long qenRefID2 { get; set; }
        [Required(ErrorMessage = "please enter refrence name")]
        public string qenRefName1 { get; set; }
        [Required(ErrorMessage = "please enter company name")]
        public string qenRefCompany1 { get; set; }
        [Required(ErrorMessage = "please enter postion")]
        public string qenRefPosition1 { get; set; }
        [Required(ErrorMessage = "please enter phone number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        public System.Nullable<double> qenRefPhone1 { get; set; }
        [Required(ErrorMessage = "please enter email ")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+‌​)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string qenRefEmail1 { get; set; }
        [Required(ErrorMessage = "please enter refrence name")]
        public string qenRefName2 { get; set; }
        [Required(ErrorMessage = "please enter company name")]
        public string qenRefCompany2 { get; set; }
        [Required(ErrorMessage = "please enter phone number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        public System.Nullable<double> qenRefPhone2 { get; set; }
        [Required(ErrorMessage = "please enter email ")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+‌​)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string qenRefEmail2 { get; set; }
        [Required(ErrorMessage = "please enter postion")]
        public string qenRefPosition2 { get; set; }
        public int? qenid { get; set; }
        [Required(ErrorMessage = "Please accept terms and conditions")]
        public string acceptterms { get; set; }
        public bool isCheckComplete1 { get; set; }
        public bool isCheckComplete2 { get; set; }
    }
    public class qendidateListValidation
    {
        public long qenID { get; set; }
        [Required(ErrorMessage = "please enter your name")]
        public string qenName { get; set; }
        // [Required(ErrorMessage = "please enter your address")]
        public string qenAddress { get; set; }

        [Required(ErrorMessage = "please enter your email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+‌​)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string qenEmail { get; set; }
        public string qenLinkdInUrl { get; set; }
        [Required(ErrorMessage = "please enter your phone number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]        
        public System.Nullable<long> qenPhone { get; set; }
        public string qenNationality { get; set; }
        public string qenPassport { get; set; }

    }

    public class AcademicModel
    {

        public qendidateGraduation graduation { get; set; }
        public qendidatePGraduation pgraduation { get; set; }
        public qenHigherSecondary hsecondary { get; set; }
        public qenSecondary secondary { get; set; }
        public int? qenid { get; set; }
        public AcademicModel()
        {
            graduation = new qendidateGraduation();
            pgraduation = new qendidatePGraduation();
            hsecondary = new qenHigherSecondary();
            secondary = new qenSecondary();
        }

    }
    public class UrlAttribute : ValidationAttribute
    {
        public UrlAttribute() { }

        public override bool IsValid(object value)
        {
            //may want more here for https, etc
            Regex regex = new Regex(@"(http://)?(www\.)?\w+\.(com|net|edu|org)");

            if (value == null) return false;

            if (!regex.IsMatch(value.ToString())) return false;

            return true;
        }
    }

   

    public class qenSkillName
    {
        [Required(ErrorMessage = "please enter your skill name")]
        public string skillName { get; set; }
        public int skillID { get; set; }
        [Required(ErrorMessage = "please enter year of exp")]
        [RegularExpression("^(100([.][0]{1,})?$|[0-9]{1,2}([.][0-9]{1,})?)$", ErrorMessage = "invalid number")]
        public float yearOfExp { get; set; }
        public int qenID { get; set; }
        public int qenSkillsID { get; set; }
    }

    public class login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+‌​)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [EmailAddress]
        public string qenEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "please enter your password")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid")]
        public string password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class job_posting_Title
    {
        public long jobID { get; set; }

        [StringLength(200, MinimumLength = 10, ErrorMessage = "200 charectar max")]
        [Required(ErrorMessage = "please enter job title")]
        [Display(Name = "Job Title")]
        public string jobTitle { get; set; }

        [AllowHtml]
        [Display(Name = "Job Description")]      
        [Required(ErrorMessage = "job description required")]
        public string jobDescription { get; set; }

        [Display(Name = "Openings")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "no of openings required")]
        public int openings { get; set; }


        [Display(Name = "Experience(year)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "year of experience required")]
        public int experience { get; set; }

        [Display(Name = "Salary")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "salary (gross salary) required")]
        public int salary { get; set; }

        [Display(Name = "Industry")]
        [Required(ErrorMessage = "please select industry")]
        public int industry { get; set; }


        [Display(Name = "Currency")]
        [Required(ErrorMessage = "please select currency")]
        public string currency { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "location required")]
        public string city { get; set; }

        [Display(Name = "Job Type")]
        [Required(ErrorMessage = "job type required")]
        public int employmentType { get; set; }


        [Display(Name = "Unit")]
        [Required(ErrorMessage = "salary unit required")]
        public string Unit { get; set; }

        [Display(Name = "Salry visible to employee")]
        public bool salaryVisibleToEmployee { get; set; }


    }


    public class requiredSkills
    {
        [Display(Name = "Technical skills")]
        public string skills { get; set; }

        public long jobID { get; set; }
    }
    public class company_
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "100 charectar max")]
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name required")]
        public string companyName { get; set; }

        [AllowHtml]        
        [Display(Name = "About Company")]
        [Required(ErrorMessage = "About company is required")]
        public string aboutCompany { get; set; }


        [StringLength(200, MinimumLength = 3, ErrorMessage = "200 charectar max")]
        [Display(Name = "Company Website")]
        [Url]
        public string companyWebsite { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = "30 charectar max")]
        [Display(Name = "Contact person")]
        [Required(ErrorMessage = "Contact person required")]
        public string contactPerson { get; set; }

        [Required(ErrorMessage = "please enter contact person number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Contact Number")]
        public string contactNumber { get; set; }
        [AllowHtml]
        [Display(Name = "Additional Information")]
        public string otherinformation { get; set; }

        [Display(Name = "Resume received on mail")]
        [EmailAddress]
        [Required(ErrorMessage = "Email ID required ")]
        public string receiveMailAt { get; set; }

        public long jobID { get; set; }
        public string companyLogo { get; set; }
    }


    public class empRegistration
    {
        [Required(ErrorMessage = "please enter company name")]
        public string companyName { get; set; }

        [Remote("isUserNameAvialable", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }
        public string website { get; set; }
        public int employerTypeID { get; set; }
        public string password { get; set; }
        public long employerID { get; set; }
        [Required(ErrorMessage = "please enter mobile number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]

        public string mobile { get; set; }
    }

    public class candidateRegistration
    {
        [Required(ErrorMessage = "please enter your full name")]
        public string candidateName { get; set; }

        [Remote("isCandidateAvialable", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter mobile number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]       
        [Display(Name = "Contact Number")]
        public string candidatePhone { get; set; }


        public string password { get; set; }
        public long candidateID { get; set; }

    }
    public class eforgotPassword
    {
        //[Remote("isUserNameAvialable", "companyDetails", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        public string password { get; set; }
    }

    public class linkedInShare
    {
        [StringLength(700, MinimumLength = 20 , ErrorMessage = "700 charectar max")]
        [Required(ErrorMessage = "You must specify your share text")]
        [Display(Name = "Share Text")]
        public string caption { get; set; }
    }
    public class qenforgotPassword
    {
        //[Remote("isUserNameAvialable", "companyDetails", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        public string password { get; set; }
    }

    public class AuxoDashBoard
    {
        public string JobID { get; set; }
        public string jobTitle { get; set; }
        public int orionCV { get; set; }
        public int orionCVC1 { get; set; }
        public int orionCVC2 { get; set; }
        public int orionCVC3 { get; set; }

        public int orionCOMM { get; set; }
        public int orionPASS { get; set; }
        public int orionMEET { get; set; }
        public int shortList { get; set; }
        public int orionExamScheduled { get; set; }
        public int hired { get; set; }

    }
    public class mailReceivedInterested
    {
        public string qenName { get; set; }
        public bool mailReceivedjobChancgeInterested { get; set; }
        public long qenID { get; set; }
        public string qenEmail { get; set; }
        public bool selectedcandidate { get; set; }
    }

    public class mailReceivedtestSchedule
    {
        public string qenName { get; set; }
        public long qenID { get; set; }
        public bool mailReceivedscheduled { get; set; }
        public string qenEmail { get; set; }
        public int testScheduleCountInt { get; set; }
        public long jobID { get; set; }
        public Nullable<System.DateTime> testScheduledDateTime { get; set; }
        public int matchSkills { get; set; }
    }

    public class ExamHistory
    {
        public long qenID { get; set; }
        public long jobID { get; set; }
        public string qenName { get; set; }

        public bool isExamAttempted { get; set; }
        public bool isExamSubmitted { get; set; }
        public int examMarks { get; set; }
        public string qenEmail { get; set; }
        public int matchSkills { get; set; }

        public bool IsMeetScheduled { get; set; }
        public long mailSendID { get; set; }
    }


    public class CandidateCom
    {
        public long qenID { get; set; }
        public long jobID { get; set; }
        public string qenName { get; set; }
        public bool meetScheduledMailRecieved { get; set; }
        public string meetPreferredMedium { get; set; }
        public int reScheduled { get; set; }
        public string qenEmail { get; set; }
        public long meetID { get; set; }
        public DateTime meetScheduledDateTime { get; set; }
        public string slotTime { get; set; }
        public string meetURL { get; set; }

    }
    public class interViewerComment
    {
        public long commentID { get; set; }
        public long jobID { get; set; }
        public long qenID { get; set; }
        public long EmployerID { get; set; }
        public string Department { get; set; }
        public string Comment { get; set; }
        public int Candidate_status { get; set; }
        public int ExpectedSalaryMonthly { get; set; }
        public int ExpectedSalaryGross { get; set; }
        public int ExamMark { get; set; }
        public string QenName { get; set; }
        public int Skills { get; set; }
        public string status { get; set; }

    }
    [MetadataType(typeof(EmployerDetailValidation))]
    public partial class EmployerDetail
    {

    }
    public partial class EmployerDetailValidation
    {
        [Required(ErrorMessage = "please enter your full name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please enter contact number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Contact Number")]
        public string Mobile { get; set; }

    }

    public partial class EmployeeDetailValidation
    {
        [Required(ErrorMessage = "please enter your full name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please enter contact number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Contact Number")]
        public string Mobile { get; set; }

        [Remote("isUserNameAvialable", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    [MetadataType(typeof(companyDetailValidation))]
    public partial class companyDetail
    {

    }
    public partial class companyDetailValidation
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "100 charectar max")]
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name required")]
        public string companyName { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "200 charectar max")]
        [Display(Name = "Company URL")]
        [Url]
        [Required(ErrorMessage = "Company url required")]
        public string website { get; set; }

        [Display(Name = "Industry Type")]
        [Required(ErrorMessage = "please select industry")]
        public int companyIndustry { get; set; }

        [Display(Name = "Employer Type")]
        [Required(ErrorMessage = "please select Employer Type")]
        public int employerTypeID { get; set; }

        [AllowHtml]
        [Display(Name = "About Company")]
        [Required(ErrorMessage = "please enter company description")]
        public string companyDescription { get; set; }

        [Required(ErrorMessage = "please enter your city")]
        public string city { get; set; }

        [Required(ErrorMessage = "please enter your state")]
        public string state { get; set; }

        [Required(ErrorMessage = "please enter your country")]
        public string country { get; set; }

        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "please enter your zipCode")]
        public string zipCode { get; set; }
    }



    public class ProfilePerformanceCount
    {
        public int viewCount { get; set; }
        public int contactCount { get; set; }
        public int DownloadCount { get; set; }
        public string MonthName { get; set; }
    }


    public class monthDate
    {
        public DateTime firstDayOfMonth { get; set; }
        public DateTime lastDayOfMonth { get; set; }

    }

    public class CreateNewPassword
    {
        public long userID { get; set; }
        public int roleID { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "please enter Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("password")]
        public string Repassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "please enter your password")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid")]
        public string password { get; set; }



    }
    //===================================== Time Slots ==========================================================



    [MetadataType(typeof(slotCretae))]
    public partial class slot
    {

    }

    public partial class slotCretae
    {
        [StringLength(11, MinimumLength = 9, ErrorMessage = "11 charectar max")]
        [Display(Name = "Time Slot")]
        [Required(ErrorMessage = "Required field ")]
        public string slotTime { get; set; }
    }


    public partial class Blockslots
    {
        public int slotID { get; set; }
        public string slotTime { get; set; }
        public bool blocked { get; set; }
        public string updated_BY { get; set; }
        public DateTime updated_date { get; set; }
    }
    public class slotsInfo
    {
        public int slotID { get; set; }
        public string slotTime { get; set; }
        public string blocked { get; set; }
        public long qenID { get; set; }
        public long jobID { get; set; }

    }

    public class careerObjective
    {
        public long qenID { get; set; }
        //[AllowHtml]
        public string careerObj { get; set; }
        //[AllowHtml]
        public string careerHighLights { get; set; }
    }

    public class referenceCheck
    {
        public long qenRefID { get; set; }
        public bool isCheckComplete { get; set; }
        public string name { get; set; }
    }

    public class jobPluginList
    {
        [Display(Name = "Advertisement ID")]
        public long advertisementRefID { get; set; }

        [Display(Name = "Created Date")]
        public DateTime dataIsCreated { get; set; }

        [Display(Name = "Image URL")]
        public string advertisementimageURL { get; set; }

        [Display(Name = "Image URL")]
        public string advertisementPDFUrk { get; set; }

        [Display(Name = "Jobs in Advertisement URL")]
        public string advertisementJobsUrl { get; set; }
    }


    public class companyD
    {

        public string jobContactEmail { get; set; }
        public string logo { get; set; }
        public string CompanyWebsite { get; set; }
        public string jobContactPersonName { get; set; }
        public string jobContactPersonMobile { get; set; }
        [AllowHtml]
        public string otherinformation { get; set; }

        public string responsesEmailID { get; set; }
        [AllowHtml]
        public string CompanyDescription { get; set; }
    }

    public class employerMaster
    {
        public List<EmployerDetail> emp { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public class qenMialSendInterested_
    {       
        public long jobID { get; set; }
        public long qenID { get; set; }
        public string companyName { get; set; }
    }



public class sp_candidateSearch_result
{
    public int srno { get; set; }
    public long qenID { get; set; }
    public string qenName { get; set; }
    public string qenEmail { get; set; }
    public string qenLinkdInUrl { get; set; }
    public string qenPhone { get; set; }
    public string qenNationality { get; set; }
    public string qenPassport { get; set; }
    public Nullable<System.DateTime> dataIsCreated { get; set; }
    public Nullable<System.DateTime> dataIsUpdated { get; set; }
        public Nullable<int> roleID { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public Nullable<bool> isActive { get; set; }
    public string password { get; set; }
    public string qenResume { get; set; }
    public string qenCoverLetter { get; set; }
    public Nullable<bool> IsReferenceCleared { get; set; }
    public string qenImage { get; set; }
        public Nullable<int> genderID { get; set; }
    public string CareerObjective { get; set; }
    public Nullable<System.DateTime> dob { get; set; }
    public Nullable<bool> socialCheck { get; set; }
    public Nullable<double> latitude { get; set; }
    public Nullable<double> longitude { get; set; }
    public string City { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string streetNo { get; set; }
    public string qenAddress { get; set; }
    public string zipCode { get; set; }
    public string careerHighlight { get; set; }
    public Nullable<System.DateTime> lastLoginTime { get; set; }
    public string genderName { get; set; }
    public Nullable<bool> SelfApplied { get; set; }
        public Nullable<int> category { get; set; }
    public string skillset { get; set; }
        public Nullable<int> PageCount { get; set; }
        public Nullable<int> CurrentPageIndex { get; set; }
        public Nullable<int> total { get; set; }
    }

    public class sp_schedular_result
    {
        public long qenid { get; set; }
        public string qenName { get; set; }
        public string qenEmail { get; set; }
        public long jobID { get; set; }
        public string jobTitle { get; set; }
         public int top10 { get; set; }
    }
    }