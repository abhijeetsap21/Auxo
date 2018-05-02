﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class oriondbEntities : DbContext
    {
        public oriondbEntities()
            : base("name=oriondbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<advertisementList> advertisementLists { get; set; }
        public virtual DbSet<app_error_log> app_error_log { get; set; }
        public virtual DbSet<App_status> App_status { get; set; }
        public virtual DbSet<Candidate_status> Candidate_status { get; set; }
        public virtual DbSet<companyDetail> companyDetails { get; set; }
        public virtual DbSet<currency> currencies { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EmployerDetail> EmployerDetails { get; set; }
        public virtual DbSet<employerType> employerTypes { get; set; }
        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }
        public virtual DbSet<genderList> genderLists { get; set; }
        public virtual DbSet<HiredCandidate> HiredCandidates { get; set; }
        public virtual DbSet<industry> industries { get; set; }
        public virtual DbSet<InterViewerComment> InterViewerComments { get; set; }
        public virtual DbSet<jobDetail> jobDetails { get; set; }
        public virtual DbSet<jobSavedQendidate> jobSavedQendidates { get; set; }
        public virtual DbSet<jobSkill> jobSkills { get; set; }
        public virtual DbSet<jobStatu> jobStatus { get; set; }
        public virtual DbSet<ProfilePerformance> ProfilePerformances { get; set; }
        public virtual DbSet<qenAppliedJob> qenAppliedJobs { get; set; }
        public virtual DbSet<qendidateGraduation> qendidateGraduations { get; set; }
        public virtual DbSet<qendidateList> qendidateLists { get; set; }
        public virtual DbSet<qendidateListInJob> qendidateListInJobs { get; set; }
        public virtual DbSet<qendidatePGraduation> qendidatePGraduations { get; set; }
        public virtual DbSet<qendidatePHD> qendidatePHDs { get; set; }
        public virtual DbSet<qendidateProfileSharedWith> qendidateProfileSharedWiths { get; set; }
        public virtual DbSet<qendidateSavedForReference> qendidateSavedForReferences { get; set; }
        public virtual DbSet<qendidateTestSchedule> qendidateTestSchedules { get; set; }
        public virtual DbSet<qenEmpDetail> qenEmpDetails { get; set; }
        public virtual DbSet<qenHigherSecondary> qenHigherSecondaries { get; set; }
        public virtual DbSet<qenInterviewSchedule> qenInterviewSchedules { get; set; }
        public virtual DbSet<qenMialSendInterested> qenMialSendInteresteds { get; set; }
        public virtual DbSet<qenReference> qenReferences { get; set; }
        public virtual DbSet<qenSecondary> qenSecondaries { get; set; }
        public virtual DbSet<qenSkill> qenSkills { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<role_action> role_action { get; set; }
        public virtual DbSet<salaryUnit> salaryUnits { get; set; }
        public virtual DbSet<skill> skills { get; set; }
        public virtual DbSet<slot> slots { get; set; }
        public virtual DbSet<slotsBlocked> slotsBlockeds { get; set; }
        public virtual DbSet<slotTempBlocked> slotTempBlockeds { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<view_qenlist> view_qenlist { get; set; }
        public virtual DbSet<sendGridDetail> sendGridDetails { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<sp_selectEducationTypeList_Result> sp_selectEducationTypeList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_selectEducationTypeList_Result>("sp_selectEducationTypeList");
        }
    
        public virtual ObjectResult<sp_candidateStatusTypeList_Result> sp_candidateStatusTypeList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_candidateStatusTypeList_Result>("sp_candidateStatusTypeList");
        }
    
        public virtual ObjectResult<sp_currencyTypeList_Result> sp_currencyTypeList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_currencyTypeList_Result>("sp_currencyTypeList");
        }
    
        public virtual ObjectResult<sp_slotlist_Result> sp_slotlist()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_slotlist_Result>("sp_slotlist");
        }
    
        public virtual ObjectResult<sp_IndustryList_Result> sp_IndustryList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_IndustryList_Result>("sp_IndustryList");
        }
    
        public virtual ObjectResult<sp_employementTypeList_Result> sp_employementTypeList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_employementTypeList_Result>("sp_employementTypeList");
        }
    
        public virtual ObjectResult<sp_dbStatics_Result> sp_dbStatics()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_dbStatics_Result>("sp_dbStatics");
        }
    
        public virtual ObjectResult<Nullable<System.Guid>> SqlQueryNotificationStoredProcedure_398797f1_1694_4a0f_a224_2c60bb3a150d()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.Guid>>("SqlQueryNotificationStoredProcedure_398797f1_1694_4a0f_a224_2c60bb3a150d");
        }
    
        public virtual ObjectResult<sp_EmployerList_Result> sp_EmployerList(Nullable<int> pageNumber, Nullable<bool> filterbyCreatedDateORmodifiedDate, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> tillDate, string comapnyName, string employerName, string mobile, string email, Nullable<bool> isActive)
        {
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var filterbyCreatedDateORmodifiedDateParameter = filterbyCreatedDateORmodifiedDate.HasValue ?
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", filterbyCreatedDateORmodifiedDate) :
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", typeof(bool));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(System.DateTime));
    
            var tillDateParameter = tillDate.HasValue ?
                new ObjectParameter("tillDate", tillDate) :
                new ObjectParameter("tillDate", typeof(System.DateTime));
    
            var comapnyNameParameter = comapnyName != null ?
                new ObjectParameter("ComapnyName", comapnyName) :
                new ObjectParameter("ComapnyName", typeof(string));
    
            var employerNameParameter = employerName != null ?
                new ObjectParameter("EmployerName", employerName) :
                new ObjectParameter("EmployerName", typeof(string));
    
            var mobileParameter = mobile != null ?
                new ObjectParameter("mobile", mobile) :
                new ObjectParameter("mobile", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("isActive", isActive) :
                new ObjectParameter("isActive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_EmployerList_Result>("sp_EmployerList", pageNumberParameter, filterbyCreatedDateORmodifiedDateParameter, fromDateParameter, tillDateParameter, comapnyNameParameter, employerNameParameter, mobileParameter, emailParameter, isActiveParameter);
        }
    
        public virtual ObjectResult<sp_QendidateList_Result> sp_QendidateList(Nullable<int> pageNumber, Nullable<bool> filterbyCreatedDateORmodifiedDate, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> tillDate, string name, string mobile, string email, Nullable<bool> isActive)
        {
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var filterbyCreatedDateORmodifiedDateParameter = filterbyCreatedDateORmodifiedDate.HasValue ?
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", filterbyCreatedDateORmodifiedDate) :
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", typeof(bool));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(System.DateTime));
    
            var tillDateParameter = tillDate.HasValue ?
                new ObjectParameter("tillDate", tillDate) :
                new ObjectParameter("tillDate", typeof(System.DateTime));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var mobileParameter = mobile != null ?
                new ObjectParameter("mobile", mobile) :
                new ObjectParameter("mobile", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("isActive", isActive) :
                new ObjectParameter("isActive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_QendidateList_Result>("sp_QendidateList", pageNumberParameter, filterbyCreatedDateORmodifiedDateParameter, fromDateParameter, tillDateParameter, nameParameter, mobileParameter, emailParameter, isActiveParameter);
        }
    
        public virtual ObjectResult<sp_CompanyList_Result> sp_CompanyList(Nullable<int> pageNumber, Nullable<bool> filterbyCreatedDateORmodifiedDate, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> tillDate, string comapnyName, Nullable<bool> isActive)
        {
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var filterbyCreatedDateORmodifiedDateParameter = filterbyCreatedDateORmodifiedDate.HasValue ?
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", filterbyCreatedDateORmodifiedDate) :
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", typeof(bool));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(System.DateTime));
    
            var tillDateParameter = tillDate.HasValue ?
                new ObjectParameter("tillDate", tillDate) :
                new ObjectParameter("tillDate", typeof(System.DateTime));
    
            var comapnyNameParameter = comapnyName != null ?
                new ObjectParameter("ComapnyName", comapnyName) :
                new ObjectParameter("ComapnyName", typeof(string));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("isActive", isActive) :
                new ObjectParameter("isActive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_CompanyList_Result>("sp_CompanyList", pageNumberParameter, filterbyCreatedDateORmodifiedDateParameter, fromDateParameter, tillDateParameter, comapnyNameParameter, isActiveParameter);
        }
    
        public virtual ObjectResult<sp_skillsList_Result> sp_skillsList(Nullable<int> pageNumber, Nullable<bool> filterbyCreatedDateORmodifiedDate, string skillName, Nullable<bool> isActive)
        {
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var filterbyCreatedDateORmodifiedDateParameter = filterbyCreatedDateORmodifiedDate.HasValue ?
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", filterbyCreatedDateORmodifiedDate) :
                new ObjectParameter("filterbyCreatedDateORmodifiedDate", typeof(bool));
    
            var skillNameParameter = skillName != null ?
                new ObjectParameter("skillName", skillName) :
                new ObjectParameter("skillName", typeof(string));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("isActive", isActive) :
                new ObjectParameter("isActive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_skillsList_Result>("sp_skillsList", pageNumberParameter, filterbyCreatedDateORmodifiedDateParameter, skillNameParameter, isActiveParameter);
        }
    
        public virtual ObjectResult<sp_roleList_Result> sp_roleList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_roleList_Result>("sp_roleList");
        }
    
        public virtual ObjectResult<sp_getNotificationCount_Result> sp_getNotificationCount(Nullable<long> qenID)
        {
            var qenIDParameter = qenID.HasValue ?
                new ObjectParameter("qenID", qenID) :
                new ObjectParameter("qenID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getNotificationCount_Result>("sp_getNotificationCount", qenIDParameter);
        }
    
        public virtual ObjectResult<string> sp_getNotificationDownload(Nullable<long> qenID)
        {
            var qenIDParameter = qenID.HasValue ?
                new ObjectParameter("qenID", qenID) :
                new ObjectParameter("qenID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_getNotificationDownload", qenIDParameter);
        }
    
        public virtual ObjectResult<string> sp_getNotificationView(Nullable<long> qenID)
        {
            var qenIDParameter = qenID.HasValue ?
                new ObjectParameter("qenID", qenID) :
                new ObjectParameter("qenID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_getNotificationView", qenIDParameter);
        }
    
        public virtual int sp_JobDetails_Update_AssignAssessment(Nullable<long> assessmentId, Nullable<long> jobId, string userName, ObjectParameter isSuccess)
        {
            var assessmentIdParameter = assessmentId.HasValue ?
                new ObjectParameter("AssessmentId", assessmentId) :
                new ObjectParameter("AssessmentId", typeof(long));
    
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("JobId", jobId) :
                new ObjectParameter("JobId", typeof(long));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_JobDetails_Update_AssignAssessment", assessmentIdParameter, jobIdParameter, userNameParameter, isSuccess);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_QendidateTestSchedule_GetAll(Nullable<long> jobId, Nullable<long> qendidateId, ObjectParameter isSuccess)
        {
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("JobId", jobId) :
                new ObjectParameter("JobId", typeof(long));
    
            var qendidateIdParameter = qendidateId.HasValue ?
                new ObjectParameter("QendidateId", qendidateId) :
                new ObjectParameter("QendidateId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_QendidateTestSchedule_GetAll", jobIdParameter, qendidateIdParameter, isSuccess);
        }
    
        public virtual ObjectResult<sp_AdvertisementList_Result> sp_AdvertisementList(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> tillDate)
        {
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(System.DateTime));
    
            var tillDateParameter = tillDate.HasValue ?
                new ObjectParameter("tillDate", tillDate) :
                new ObjectParameter("tillDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AdvertisementList_Result>("sp_AdvertisementList", fromDateParameter, tillDateParameter);
        }
    
        public virtual int sp_returnMatchingSkillOFqendidateToJob(Nullable<long> qenID, Nullable<long> jobID, ObjectParameter skillMatchPercentage)
        {
            var qenIDParameter = qenID.HasValue ?
                new ObjectParameter("qenID", qenID) :
                new ObjectParameter("qenID", typeof(long));
    
            var jobIDParameter = jobID.HasValue ?
                new ObjectParameter("jobID", jobID) :
                new ObjectParameter("jobID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_returnMatchingSkillOFqendidateToJob", qenIDParameter, jobIDParameter, skillMatchPercentage);
        }
    
        public virtual int sp_schedular()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_schedular");
        }
    
        public virtual ObjectResult<sp_JobDetails_Get_ByJobId_Result> sp_JobDetails_Get_ByJobId(Nullable<long> jobId)
        {
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("JobId", jobId) :
                new ObjectParameter("JobId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_JobDetails_Get_ByJobId_Result>("sp_JobDetails_Get_ByJobId", jobIdParameter);
        }
    
        [DbFunction("oriondbEntities", "skillsetFromWordArrayTointgerArray")]
        public virtual IQueryable<string> skillsetFromWordArrayTointgerArray(string skillSet)
        {
            var skillSetParameter = skillSet != null ?
                new ObjectParameter("skillSet", skillSet) :
                new ObjectParameter("skillSet", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<string>("[oriondbEntities].[skillsetFromWordArrayTointgerArray](@skillSet)", skillSetParameter);
        }
    
        public virtual int sp_candidateSearch(Nullable<long> jobID, Nullable<System.DateTime> modifiedDate, string title, string city, string skillsSet, Nullable<bool> defaultSearch, Nullable<int> pageNumber)
        {
            var jobIDParameter = jobID.HasValue ?
                new ObjectParameter("jobID", jobID) :
                new ObjectParameter("jobID", typeof(long));
    
            var modifiedDateParameter = modifiedDate.HasValue ?
                new ObjectParameter("modifiedDate", modifiedDate) :
                new ObjectParameter("modifiedDate", typeof(System.DateTime));
    
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("city", city) :
                new ObjectParameter("city", typeof(string));
    
            var skillsSetParameter = skillsSet != null ?
                new ObjectParameter("skillsSet", skillsSet) :
                new ObjectParameter("skillsSet", typeof(string));
    
            var defaultSearchParameter = defaultSearch.HasValue ?
                new ObjectParameter("defaultSearch", defaultSearch) :
                new ObjectParameter("defaultSearch", typeof(bool));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_candidateSearch", jobIDParameter, modifiedDateParameter, titleParameter, cityParameter, skillsSetParameter, defaultSearchParameter, pageNumberParameter);
        }
    
        public virtual ObjectResult<sp_searchjob_Result> sp_searchjob(string title, Nullable<int> employmentType, string city, Nullable<System.DateTime> modifiedDate, Nullable<long> qenID, Nullable<int> pageNumber, Nullable<int> companyID)
        {
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            var employmentTypeParameter = employmentType.HasValue ?
                new ObjectParameter("employmentType", employmentType) :
                new ObjectParameter("employmentType", typeof(int));
    
            var cityParameter = city != null ?
                new ObjectParameter("city", city) :
                new ObjectParameter("city", typeof(string));
    
            var modifiedDateParameter = modifiedDate.HasValue ?
                new ObjectParameter("modifiedDate", modifiedDate) :
                new ObjectParameter("modifiedDate", typeof(System.DateTime));
    
            var qenIDParameter = qenID.HasValue ?
                new ObjectParameter("qenID", qenID) :
                new ObjectParameter("qenID", typeof(long));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_searchjob_Result>("sp_searchjob", titleParameter, employmentTypeParameter, cityParameter, modifiedDateParameter, qenIDParameter, pageNumberParameter, companyIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_skillsetFromWordArrayTointgerArray(string skillSet)
        {
            var skillSetParameter = skillSet != null ?
                new ObjectParameter("skillSet", skillSet) :
                new ObjectParameter("skillSet", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_skillsetFromWordArrayTointgerArray", skillSetParameter);
        }
    
        public virtual ObjectResult<sp_EmployerDashborad_Result> sp_EmployerDashborad(Nullable<long> companyID)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_EmployerDashborad_Result>("sp_EmployerDashborad", companyIDParameter);
        }
    
        public virtual ObjectResult<sp_AvailableSlotsForCandidate_Result> sp_AvailableSlotsForCandidate(Nullable<System.DateTime> dt, Nullable<long> jobID)
        {
            var dtParameter = dt.HasValue ?
                new ObjectParameter("dt", dt) :
                new ObjectParameter("dt", typeof(System.DateTime));
    
            var jobIDParameter = jobID.HasValue ?
                new ObjectParameter("jobID", jobID) :
                new ObjectParameter("jobID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AvailableSlotsForCandidate_Result>("sp_AvailableSlotsForCandidate", dtParameter, jobIDParameter);
        }
    
        public virtual ObjectResult<sp_sendNewsletter_Result> sp_sendNewsletter()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_sendNewsletter_Result>("sp_sendNewsletter");
        }
    }
}
