using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;
using System.IO;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Data.Entity.Validation;
using SelectPdf;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.Web.Configuration;

namespace NewLetter.Controllers
{
    [CustomErrorHandling]
    public class jobDetailsController : BaseClass
    {

        private oriondbEntities db = new oriondbEntities();

        // GET: jobDetails
        [HttpGet]
        public ActionResult Jobs()
        {
            ViewBag.employmentType = new SelectList(db.EmploymentTypes.Where(e => e.isActive == true), "EmploymentTypeID", "EmploymentType1");
            try
            {
                long id = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                int rol = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));
                if (rol == 5)
                {
                    var result = db.qendidateLists.Where(e => e.qenID == id).Select(e => e.socialCheck).FirstOrDefault();

                    db.Database.Log = msg => Debug.Write(msg);
                    if (result.Equals(false))
                    {
                        ViewBag.socialvalue = "No";
                    }
                    else
                    {
                        ViewBag.socialvalue = "Yes";

                    }
                }
                else
                {
                    ViewBag.socialvalue = "Yes";
                }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e); ViewBag.socialvalue = "No";
            }
            return View();
        }

        // load partial view first time 
        public ActionResult _partialJobResultList(int jobid)
        {
            oriondbEntities db = new oriondbEntities();
            var result = (dynamic)null;
            DateTime updatbefore = BaseUtil.GetCalculatedDateTime(-365);
            try
            {
                string spExecute = "sp_searchjob @defaultSearch =1 , @modifiedDate='" + updatbefore + "',@PageNumber = 1, @jobID ='" + jobid + "'";
                result = db.Database.SqlQuery<sp_candidateSearch_result>(spExecute).ToList();
                sp_candidateSearch_result sp = new sp_candidateSearch_result();
                sp.PageCount = result[0].PageCount;
                sp.CurrentPageIndex = 1;
                ViewBag.totalpages = sp.PageCount;
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return PartialView("_partialCandidateResult", result);
        }

        public ActionResult Jobsearch(FormCollection frm)
        {
            //IQueryable<jobDetail> jobDetails = db.jobDetails;

            string city = ""; int employmentType = 0; string JobTitle = ""; string modifiedDate = "1"; DateTime updatbefore; long companyID = 0;
            var jobDetails = (dynamic)null;
            try
            {
                string query = "exec sp_searchjob @PageNumber=1,";
                int roleID = 0;

                if (frm.Keys.Count > 0)
                {

                    if (frm["ddl_update"].ToString() != "")
                    {
                        modifiedDate = frm["ddl_update"].ToString();
                    }
                    if (modifiedDate == "2")
                    {
                        updatbefore = BaseUtil.GetCalculatedDateTime(-30);
                    }
                    else if (modifiedDate == "5")
                    {
                        updatbefore = BaseUtil.GetCalculatedDateTime(-370);

                    }
                    else if (modifiedDate == "3")
                    {
                        updatbefore = BaseUtil.GetCalculatedDateTime(-90);

                    }
                    else if (modifiedDate == "4")
                    {
                        updatbefore = BaseUtil.GetCalculatedDateTime(-180);
                    }
                    else
                    {
                        updatbefore = BaseUtil.GetCalculatedDateTime(-30);
                    }
                    query = query + " @modifiedDate='" + updatbefore + "'";

                    JobTitle = frm["txt_JobTitle"].ToString();
                    if (JobTitle != "")
                    {
                        query = query + ", @title='" + JobTitle + "'";

                    }
                    if (frm["employmentType"].ToString() != "")
                    {
                        employmentType = Convert.ToInt16(frm["employmentType"]);
                        query = query + " , @employmentType='" + employmentType + "'";
                    }
                    if (frm["city"].ToString() != "")
                    {
                        city = frm["city"].ToString();
                        query = query + " , @city='" + city + "'";
                    }

                }
                else
                {
                    updatbefore = BaseUtil.GetCalculatedDateTime(-30);
                    query = query + " @modifiedDate='" + updatbefore + "'";
                }

                if (BaseUtil.IsAuthenticated())
                {
                    roleID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));

                    if (roleID == 2 || roleID == 3 || roleID == 1 || roleID == 4)
                    {
                        companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
                        query = query + ",@companyID='" + companyID + "'";
                    }
                }
                if (BaseUtil.IsAuthenticated())
                {
                    roleID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));

                    if (roleID == 5 && frm.Keys.Count == 0)
                    {
                        long loginID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                        query = query + " ,@qenID ='" + loginID + "'";
                    }
                }

                jobDetails = db.Database.SqlQuery<sp_searchjob_Result>(query).ToList();
                if (jobDetails != null && jobDetails.Count != 0)
                {
                    ViewBag.totalpages = jobDetails[0].PageCount;
                }

                ViewBag.employmentType = new SelectList(db.EmploymentTypes.Where(e => e.isActive == true), "EmploymentTypeID", "EmploymentType1", employmentType); //employmentType
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return PartialView("_partialResultView", jobDetails);

            //            select distinct *from(  select jobID, (   select Ltrim(RTRIM(skillsID)) + ',' from jobSkills C2 where c1.jobID = C2.jobID order by skillsID for XML path('')  ) skill from jobSkills C1) C where C.skill like '2,3,12' + '%'
        }

        public string LoadMoreJobs(string jobtitle, string city, int employmentType, int ddl_update, int currentPageIndex)
        {

            string htmlString = "";
            try { 
            DateTime updatbefore; long companyID = 0;
            string query = "exec sp_searchjob @PageNumber='" + currentPageIndex + "',";
            int roleID = 0;

            if (ddl_update == 2)
            {
                updatbefore = BaseUtil.GetCalculatedDateTime(-7);

            }

            else if (ddl_update == 3)
            {
                updatbefore = BaseUtil.GetCalculatedDateTime(-90);

            }
            else if (ddl_update == 4)
            {
                updatbefore = BaseUtil.GetCalculatedDateTime(-180);

            }
            else if (ddl_update == 5)
            {
                updatbefore = BaseUtil.GetCalculatedDateTime(-730);

            }
            else
            {
                updatbefore = BaseUtil.GetCalculatedDateTime(-30);

            }

            query = query + " @modifiedDate='" + updatbefore + "'";


            if (jobtitle != "")
            {
                query = query + ", @title='" + jobtitle + "'";

            }
            if (employmentType != 0)
            {

                query = query + " , @employmentType='" + employmentType + "'";
            }
            if (city != "")
            {
                query = query + " , @city='" + city + "'";
            }




            if (BaseUtil.IsAuthenticated())
            {
                roleID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));
                if (roleID == 2 || roleID == 3)
                {
                    companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));

                    query = query + ",@companyID='" + companyID + "'";
                }
            }
            if (BaseUtil.IsAuthenticated())
            {
                roleID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));

                if (roleID == 5)
                {
                    long loginID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                    query = query + " ,@qenID ='" + loginID + "'";
                }
            }

            var jobDetails = db.Database.SqlQuery<sp_searchjob_Result>(query);


            ViewBag.employmentType = new SelectList(db.EmploymentTypes.Where(e => e.isActive == true), "EmploymentTypeID", "EmploymentType1", employmentType); //employmentType
                                                                                                                                                               // return PartialView("_partialResultView", jobDetails.ToList());

            
            int index = (currentPageIndex) * 1;
            foreach (var item in jobDetails)
            {
                index = index + 1;

                htmlString = htmlString + " <tr> <td><div class='bs-example'> <div class='media'><div class='media-left  media-top'><a href = '#' ><img src='" + item.CompanyLogo + "' class='media-object' style='width:60px' alt='" + item.CompanyName + "'></a></div>";
                htmlString = htmlString + "<div class='media-body'>";
                htmlString = htmlString + " <h4 class='media-heading'>" + item.CompanyName + "<small><i>Posted on " + @item.dataIsCreated + "</i></small></h4>";
                htmlString = htmlString + " <p>" + item.jobTitle + "</p>";
                htmlString = htmlString + "<div class='col -sm-12'>";
                htmlString = htmlString + "<div class='col -sm-1' style='font-size:13px'> Skills : </div>     ";
                htmlString = htmlString + "<div class='col -sm-11' style='font-size:13px'>  " + item.skillset + "</div> </div>";

                htmlString = htmlString + " <div class='col -sm-12'>";

                htmlString = htmlString + " <table style = 'width:100%' >";
                htmlString = htmlString + "<tr><td style='font-size:13px'>Openings : </td>";
                htmlString = htmlString + "<td>" + item.NoOfOpenings + "</td>";

                htmlString = htmlString + " <td style = 'font-size:13px'> Work Experience : </td>";
                string exp = "";
                if (item.workExpMin == 0)
                {
                    exp = "Fresher";
                }
                else { exp = item.workExpMin + "year"; }

                htmlString = htmlString + "<td>" + exp + "</td>";
                htmlString = htmlString + " <td style = 'font-size:13px' > Location : </td>";
                htmlString = htmlString + " <td>" + item.city + "</td>";

                htmlString = htmlString + " <td style = 'font-size:13px' > Education : </td>";
                htmlString = htmlString + "<td>" + item.EducationReq + "</td>";

                htmlString = htmlString + "<td style = 'font-size:13px' > Industry : </td>";
                htmlString = htmlString + " <td>" + item.industryName + "</td></tr> </table>  </div>";
                htmlString = htmlString + "<div class='col-sm-12'>";

                if (roleID == 1 || roleID == 2 || roleID == 3 || roleID == 4)
                {
                    htmlString = htmlString + "<a href='/jobDetails/newjob?jobID==" + BaseUtil.encrypt(item.jobID.ToString()) + "'>Edit</a>";
                    htmlString = htmlString + "<a href='/companyDetails/cvShortlisted?jobID==" + BaseUtil.encrypt(item.jobID.ToString()) + "'>Candidates</a>";
                    var pass = BaseUtil.encrypt(BaseUtil.GetSessionValue(AdminInfo.password.ToString()));
                    var email = BaseUtil.encrypt(BaseUtil.GetSessionValue(AdminInfo.EmailID.ToString()));

                    htmlString = htmlString + " <input type = 'button' name='Auxopass' class='previous action-button' value='Auxo Pass' onclick='location.href = 'https://spotaneedle.com/Login.aspx?uid='" + email + "'&key='" + pass + "/>";
                }

                htmlString = htmlString + "<a href='/jobDetails/JobView?jobID=" + BaseUtil.encrypt(item.jobID.ToString()) + "'>View</a>";

                htmlString = htmlString + "  </div> </div></div> </div>  </td></tr>";

            }
            var htmlString1 = HttpUtility.HtmlEncode(htmlString);
            if (htmlString == "")
            {
                htmlString = "<tr> <td> No more records found.</td></tr>";
            }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e); 
            }
            return htmlString;
        }
        public ActionResult JobView(string jobID)

        {
            long jobid_ = 0; var jobDetails = (dynamic)null;
            try { 
            if (jobID != "")
            {
                jobid_ = Convert.ToInt64(BaseUtil.Decrypt(jobID));
            }
            if (jobid_ != 0)
            {
                    jobDetails = db.jobDetails.Include(e => e.industry).Include(e => e.EmploymentType).Include(e => e.companyDetail).Include(e => e.Education).Include(e => e.currency1).Where(e => e.jobID == jobid_).FirstOrDefault();

                    return View(jobDetails);
            }
            else
            {              
                return View(jobDetails);
            }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e); 
            }
            return View(jobDetails);
        }

        [HttpGet]
        public ActionResult review()
        {
            Int32 jobID = Convert.ToInt32(Session["JobId"]);
            if (jobID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobDetail ojobDetail = new jobDetail();
            ojobDetail = db.jobDetails.Where(m => m.jobID == jobID).FirstOrDefault();
            if (ojobDetail == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(ojobDetail);
        }
        [HttpPost]
        public ActionResult review(jobDetail ojobDetail)
        {

            ojobDetail = db.jobDetails.Where(m => m.jobID == ojobDetail.jobID).FirstOrDefault();
            if (ojobDetail == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ojobDetail.isActive = true;
            ojobDetail.compeletPosted = true;
            ojobDetail.jobStatusID = 1;
            db.Entry(ojobDetail).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Confirm");
        }
        [HttpGet]
        public ActionResult Confirm()
        {
            return View();
        }


        public ActionResult companyDetails()
        {
            job_posting_Title ojob_posting_Title = new job_posting_Title();
            try
            {
                ViewBag.EmploymentTypeID = new SelectList(db.EmploymentTypes, "EmploymentTypeID", "EmploymentType1");
                ViewBag.industryID = new SelectList(db.industries, "industryID", "industryName");
                ViewBag.currencyID = new SelectList(db.currencies, "currencyID", "currency");
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e); 
            }
            return View(ojob_posting_Title);
        }
        public JsonResult select_fillSkills(string term)
        {
            //Note : you can bind same list from database  
            var skillName = (dynamic)null;
            try { 
             skillName = db.skills.Where(m => m.skillName.Contains(term)).Select(x => new { value = x.skillName, userId = x.skillsID }).Distinct().Take(20);
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e); 
            }
            return Json(skillName, JsonRequestBehavior.AllowGet);

        }

        public ActionResult JobHold(int? jobID)
        {
            try { 
            if (jobID != null)
            {
                jobDetail ojobDetail = new jobDetail();
                ojobDetail = db.jobDetails.Find(jobID);
                ojobDetail.jobStatusID = 2;
                ojobDetail.modifiedBy = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                ojobDetail.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(ojobDetail).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e); 
            }
            return View();
        }
        public ActionResult JobDelete(int? jobID)
        {try { 
            if (jobID != null)
            {
                jobDetail ojobDetail = new jobDetail();
                ojobDetail = db.jobDetails.Find(jobID);
                ojobDetail.jobStatusID = 3;
                ojobDetail.modifiedBy = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                ojobDetail.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(ojobDetail).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e); 
            }
            return View();
        }

        public async Task<string> Apply(Int32? jobID)
        {
            try
            {
                var candidateID = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());
                if (candidateID == "" || candidateID == null)
                {
                    return "login";
                }
                int roleID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));
                long qenID = Convert.ToInt32(candidateID);
                var email = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
                if (roleID == 5)
                {

                    qenAppliedJob oqenAppliedJob = new qenAppliedJob();
                    if (!db.qenAppliedJobs.Any(e => e.jobID == jobID && e.qenID == qenID))
                    {
                        oqenAppliedJob.jobID = (long)jobID;
                        oqenAppliedJob.qenID = qenID;
                        oqenAppliedJob.appliedDate = BaseUtil.GetCurrentDateTime();
                        oqenAppliedJob.dataIsCreated = BaseUtil.GetCurrentDateTime();
                        oqenAppliedJob.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        oqenAppliedJob.isActive = true;
                        oqenAppliedJob.isDelete = false;
                        oqenAppliedJob.App_status_id = 1;
                        db.qenAppliedJobs.Add(oqenAppliedJob);

                        db.SaveChanges();

                        if (!db.qendidateListInJobs.Any(e => e.jobID == jobID && e.qenID == qenID))
                        {
                            qendidateListInJob oqendidateListInJobs = new qendidateListInJob();
                            oqendidateListInJobs.category = 0;
                            oqendidateListInJobs.jobID = (long)jobID;
                            oqendidateListInJobs.qenID = qenID;
                            oqendidateListInJobs.SelfApplied = true;
                            db.qendidateListInJobs.Add(oqendidateListInJobs);
                            db.SaveChanges();
                        }
                        else
                        {

                            qendidateListInJob oqendidateListInJobs = new qendidateListInJob();
                            oqendidateListInJobs = db.qendidateListInJobs.Where(e => e.jobID == jobID && e.qenID == qenID).FirstOrDefault();

                            oqendidateListInJobs.SelfApplied = true;
                            db.Entry(oqendidateListInJobs).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                        //----------------------------use below code to send emailer------------------------------------------------------------
                        string jobUrl = "https://spotaneedle.com/jobDetails/JobView?jobID=" + BaseUtil.encrypt(jobID.ToString());
                        StreamReader sr = new StreamReader(Server.MapPath("/Emailer/jobAppliedSuccessfully.html"));
                        string HTML_Body = sr.ReadToEnd();
                        string final_Html_Body = HTML_Body.Replace("#name", BaseUtil.GetSessionValue(AdminInfo.FullName.ToString())).Replace("#url", jobUrl);
                        sr.Close();
                        string To = email.ToString();
                        string mail_Subject = "Thank you for appling job";


                        string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                        //----------------------------end to send emailer------------------------------------------------------------

                        return "mail sent and applied";

                    }
                    else
                    {
                        return "alreadyApplied";
                    }
                }
                else
                {
                    return "access"; //"Don't have this access.";
                }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return "login";

        }
        public string ShareWF(string jobUrl, string email)
        {
            try
            {
                //----------------------------use below code to send emailer------------------------------------------------------------
                string name = BaseUtil.GetSessionValue(AdminInfo.FullName.ToString());
                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/shareJobWithFriend.html"));
                string HTML_Body = sr.ReadToEnd();
                string final_Html_Body = HTML_Body.Replace("#name", name).Replace("#url", jobUrl).Replace("#emailID", email);
                sr.Close();
                string To = email.ToString();
                string mail_Subject = name + " Recommended a jobs for you";


                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                //----------------------------end to send emailer------------------------------------------------------------
                return "Mail Sent";
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return "not sent";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // employer job dashboard
        public ActionResult jobDashboard()
        {
            return View();
        }

        // Newspaper Advertisement View
        [HttpGet]
        public ActionResult createAdvertisement(long? jobID)
        {
            return View();
        }
        [HttpPost]
        public ActionResult createAdvertisement(advertisementList adList, HttpPostedFileBase files, FormCollection frm)
        {
            try
            {
                if (frm["hdnjobID"] == null)
                {
                    advertisementList newadlist = new advertisementList();
                    newadlist.empID = Convert.ToUInt32(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                    newadlist.postStatus = 0;
                    newadlist.advertisementURL = "Some URL";

                    //======================================================================
                    //postStatus    0 -> for leave after upload advt image
                    //              1 -> post advt job by self
                    //              2 -> post advt job by operator
                    //=========================================================================

                    var path = string.Empty;
                    if (files != null)
                    {
                        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(files.FileName);
                        path = Path.Combine(Server.MapPath("~/newspaper"), fileName);
                        files.SaveAs(path);
                        newadlist.advertisementimageURL = "https://spotaneedle.com/newspaper/" + fileName;
                        newadlist.dataIsCreated = BaseUtil.GetCurrentDateTime();
                        newadlist.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.advertisementLists.Add(newadlist);
                        db.SaveChanges();
                        string advtID = BaseUtil.encrypt((newadlist.advertisementRefID).ToString());
                        newadlist.advertisementURL = "https://spotaneedle.com/jobDetails/advertisementApplyHome/?refid=" + advtID;
                        newadlist.isComplePosted = true;
                        db.SaveChanges();
                        return RedirectToAction("adPostSuccess", new { ID = newadlist.advertisementRefID });
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    long id = Convert.ToInt32(frm["hdnjobID"]);
                    advertisementList oadvertisementList = new advertisementList();
                    oadvertisementList = db.advertisementLists.Where(e => e.advertisementRefID == id).FirstOrDefault();
                    var path = string.Empty;
                    if (files != null)
                    {
                        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(files.FileName);
                        path = Path.Combine(Server.MapPath("~/newspaper"), fileName);
                        files.SaveAs(path);
                        oadvertisementList.advertisementimageURL = "https://spotaneedle.com/newspaper/" + fileName;
                        oadvertisementList.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        oadvertisementList.advertisementURL = "https://spotaneedle.com/jobDetails/advertisementApplyHome/?refid=" + id;
                        oadvertisementList.isComplePosted = true;
                        db.Entry(oadvertisementList).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("jobDashboard", "jobDetails");
                }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View();
        }
        // Posted Advertisement List

        public ActionResult jobPluginList()
        {
           
            List<jobPluginList> jl = new List<Models.jobPluginList>();
            try { 
            int empID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            var pluginjobs = db.advertisementLists.Where(e => e.empID == empID).Select(e => new { e.advertisementRefID, e.advertisementimageURL, e.dataIsCreated, e.advertisementPDFUrk }).OrderByDescending(e => e.dataIsCreated).ToList();
            long count = pluginjobs.Count();
            foreach (var item in pluginjobs)
            {
                jobPluginList jd = new Models.jobPluginList();
                jd.advertisementimageURL = item.advertisementimageURL;
                jd.advertisementPDFUrk = item.advertisementPDFUrk;
                jd.advertisementRefID = item.advertisementRefID;
                jd.dataIsCreated = Convert.ToDateTime(item.dataIsCreated);
                string id = BaseUtil.encrypt(jd.advertisementRefID.ToString());
                jd.advertisementJobsUrl = "https://spotaneedle.com/jobDetails/advertisementApplyHome/?refid=" + id + "&&val=1";

                jl.Add(jd);
            }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(jl);
        }



        // Create job advertisement
        [HttpGet]
        public ActionResult NewJob(Int64? jobID, Int64? refID, int? islink)
        {

            jobDetail jobDetailsModel = new jobDetail();
            try {
            if (jobID == 0 || jobID == null)
            {
                long employerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                if (refID != null)
                {
                    jobDetailsModel.advertisementRefID = refID;

                    //======================================================================
                    //postStatus    0 -> for leave after upload advt image
                    //              1 -> post advt job by self
                    //              2 -> post advt job by operator
                    //=========================================================================
                    advertisementList oadvertisementList = new advertisementList();

                    oadvertisementList = db.advertisementLists.Where(e => e.advertisementRefID == refID).FirstOrDefault();
                    if (oadvertisementList.empID == employerID)
                    {
                        oadvertisementList.postStatus = 1;
                        db.Entry(oadvertisementList).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                long companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));

                var comany = db.companyDetails.Find(companyID);
                var employer = db.EmployerDetails.Find(employerID);
                jobDetailsModel.CompanyName = comany.companyName;
                jobDetailsModel.CompanyDescription = comany.companyDescription;
                jobDetailsModel.CompanyWebsite = comany.website;
                jobDetailsModel.jobContactPersonName = employer.Name;
                jobDetailsModel.responsesEmailID = employer.Email;
                jobDetailsModel.jobContactPersonEmail = employer.Email;
                jobDetailsModel.jobContactPersonMobile = employer.Mobile;
                jobDetailsModel.CompanyLogo = comany.logo;
            }
            else
            {
                jobDetailsModel = db.jobDetails.Where(e => e.jobID == jobID).FirstOrDefault();
                jobDetailsModel.requiredSkills = BaseUtil.JOb_skillSet(jobDetailsModel.jobID);
            }
           
            ViewBag.gender = db.genderLists;
            ViewBag.EmploymentTypeID = db.EmploymentTypes.Where(e => e.isActive == true);
            ViewBag.industryID = db.industries.Where(e => e.isActive == true);
            ViewBag.currency = db.currencies.Where(e => e.isActive == true);
            ViewBag.EducationReq = db.Educations;
            ViewBag.ComapnyDetails = db.companyDetails.Where(e => e.isActive == true).Select(e => new { e.companyID, e.companyName });
            ViewBag.isLink = "";
            if (islink == 1)
            {
                ViewBag.isLink = "3";
            }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(jobDetailsModel);
        }


        public ActionResult NewJob(jobDetail jobDetailsModel, HttpPostedFileBase files)
        {
            try { 
            jobDetailsModel.city = jobDetailsModel.city;
            //newjobDetailModel.EducationReq = "Not Available";           
            jobDetailsModel.dataIsCreated = DateTime.Now;
            jobDetailsModel.dataIsUpdated = DateTime.Now;
            jobDetailsModel.unit = "Monthly";
            jobDetailsModel.isActive = true;
            jobDetailsModel.isDelete = false;
            jobDetailsModel.createdBy = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            jobDetailsModel.jobStatusID = 1;
            jobDetailsModel.responsesEmailID = jobDetailsModel.jobContactPersonEmail;

            var path = string.Empty;
            if (files != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(files.FileName);
                path = Path.Combine(Server.MapPath("~/Logo"), fileName);
                files.SaveAs(path);
                jobDetailsModel.CompanyLogo = "https://spotaneedle.com/Logo/" + fileName;
            }
            var roleID = BaseUtil.RoleID();
            string emailID = string.Empty; string conName = string.Empty;
            conName = jobDetailsModel.CompanyName;
            if (roleID == "2" || roleID == "3" || roleID == "6")
            {
                jobDetailsModel.companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
                emailID = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
            }
            else
            {
                jobDetailsModel.companyID = Convert.ToInt64(jobDetailsModel.CompanyName);
                var comapnyName = db.companyDetails.Where(e => e.companyID == jobDetailsModel.companyID).Select(e => new { e.companyName }).FirstOrDefault();
                jobDetailsModel.CompanyName = comapnyName.companyName.ToString();
                emailID = jobDetailsModel.jobContactPersonEmail;
                conName = jobDetailsModel.jobContactPersonName;
            }

            

                if (jobDetailsModel.jobID == 0)
                {
                    db.jobDetails.Add(jobDetailsModel);
                    db.SaveChanges();

                    jobDetailsModel.jobURL = "https://spotaneedle.com/jobDetails/Apply?jobID=" + jobDetailsModel.jobID;
                    var url = jobDetailsModel.jobURL;
                    db.Entry(jobDetailsModel).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(jobDetailsModel).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.jobid = jobDetailsModel.jobID;

                BaseUtil.Skills(jobDetailsModel.requiredSkills, jobDetailsModel.jobID);
                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toEmployerAfterjobPost.html"));
                string HTML_Body = sr.ReadToEnd();
                string final_Html_Body_Link = "";
                string final_Html_Body = HTML_Body.Replace("#name", conName);
                if (jobDetailsModel.jobURL != null)
                {
                    final_Html_Body_Link = final_Html_Body.Replace("#Link", jobDetailsModel.jobURL);
                }
                sr.Close();
                string To = emailID;
                string mail_Subject = "Job Posted Successfully ";
                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body_Link, "");
           
            TempData["refID"] = jobDetailsModel.advertisementRefID;
            var i = Request.Form["jobPOstType"].ToString();
            if (i == "3")
            {
                TempData["jobURL"] = jobDetailsModel.jobURL;
            }
            return RedirectToAction("ThankYou");
            }
            catch (DbEntityValidationException ex)
            {
                BaseUtil.CaptureErrorValues(ex);
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);                
               
            }
            ViewBag.gender = db.genderLists;
            ViewBag.EmploymentTypeID = db.EmploymentTypes.Where(e => e.isActive == true);
            ViewBag.industryID = db.industries.Where(e => e.isActive == true);
            ViewBag.currency = db.currencies.Where(e => e.isActive == true);
            ViewBag.EducationReq = db.Educations;
            ViewBag.ComapnyDetails = db.companyDetails.Where(e => e.isActive == true).Select(e => new { e.companyID, e.companyName });
            return View(jobDetailsModel);
        }



        //Post job Via Admin

        public ActionResult sendMailToOperator(long advID)
        {
          try { 
            advertisementList adlist = new advertisementList();
            adlist = db.advertisementLists.Include(e => e.jobDetails).Where(ex => ex.advertisementRefID == advID).FirstOrDefault();
            adlist.sentToDataOperatorForPost = true;
            db.Entry(adlist).State = EntityState.Modified;
            db.SaveChanges();
            long empid = adlist.empID;
            var empdet = db.EmployerDetails.Where(ex => ex.EmployerID == empid).FirstOrDefault();
            long cid = empdet.companyID;
            var compdet = db.companyDetails.Where(ex => ex.companyID == cid).FirstOrDefault();
            StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toAdminCreateAD.html"));
            string HTML_Body = sr.ReadToEnd();
            string final_Html_Body = HTML_Body.Replace("#name", "Admin").Replace("#refID", advID.ToString()).Replace("#companyname", compdet.companyName).Replace("#empname", empdet.Name).Replace("#pdfurl", adlist.advertisementPDFUrk).Replace("#imgurl", adlist.advertisementimageURL);
            sr.Close();
            string To = WebConfigurationManager.AppSettings["AdminEmail"].ToString();
            string mail_Subject = "Request Received For Newspaper Advertisement Creation";
            string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
            return RedirectToAction("ThankYou");
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View();
        }

        public ActionResult companyDetails_(string CompanyID)
        {
            companyD ocompanyD = new companyD();
            try
            {
                long cid = Convert.ToInt64(CompanyID);
                var companyDetails = db.companyDetails.Where(e => e.companyID == cid).Select(e => new { e.companyDescription, e.website, e.logo }).FirstOrDefault();
                var empDetails = db.EmployerDetails.Where(e => e.companyID == cid).Select(e => new { e.Name, e.Mobile, e.Email }).FirstOrDefault();
               
                ocompanyD.logo = companyDetails.logo;
                ocompanyD.CompanyWebsite = companyDetails.website;
                ocompanyD.CompanyDescription = companyDetails.companyDescription;
                ocompanyD.jobContactPersonMobile = empDetails.Mobile;
                ocompanyD.jobContactPersonName = empDetails.Name;
                ocompanyD.jobContactEmail = empDetails.Email;               
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return Json(ocompanyD, JsonRequestBehavior.AllowGet);
        }

        // Thank you page
        [HttpGet]
        public ActionResult ThankYou()
        {
            return View();
        }

        // Advertisement Posted Success View

        [HttpGet]
        public ActionResult adPostSuccess(long ID)
        {
            var adlist = (dynamic)null;
            try
            {
                adlist = db.advertisementLists.Where(e => e.advertisementRefID == ID).FirstOrDefault();
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            ViewBag.refID = ID;
            return View(adlist);
        }

        [HttpGet]
        public ActionResult advertisementApplyHome(string refid, int? val)
        {
            try
            {
                if (refid != null)
                {
                    var decryptvalue = BaseUtil.Decrypt(refid);
                    var refID = Convert.ToInt32(decryptvalue);
                    var adjoblist = db.jobDetails.Where(e => e.advertisementRefID == refID).ToList();
                    var advtImg = db.advertisementLists.Where(a => a.advertisementRefID == refID).FirstOrDefault();
                    ViewBag.advtImg1 = advtImg.advertisementimageURL;
                    long AdvtcreatedBy = advtImg.empID;
                    var companyName = db.EmployerDetails.Include(e => e.companyDetail).Where(e => e.EmployerID == AdvtcreatedBy).Select(e => new { e.companyDetail.companyName, e.companyDetail.logo }).FirstOrDefault();
                    ViewBag.companyLogo = companyName.logo;
                    ViewBag.company_name = companyName.companyName;
                    return View(adjoblist);
                }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View();
        }

        // render advertisement page 

        [HttpGet]
        public ActionResult generatenewspaperjob(int ID)
        {
            var advert = (dynamic)null;
            try
            {
                advert = db.advertisementLists.Where(a => a.advertisementRefID == ID).FirstOrDefault();
                ViewBag.ID = ID;
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(advert);
        }

        // Generate PDF and send mail

        [HttpPost]
        [ValidateInput(false)]
        public async Task<FileResult> Exportnews(string GridHtml, string advID)
        {

            string result = GridHtml;
            var path = string.Empty;
            int empID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            var empdetails = db.EmployerDetails.Where(e => e.EmployerID == empID).FirstOrDefault();
            long ID = Convert.ToInt64(advID);

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                HtmlToPdf converter = new HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(GridHtml);
                converter.Options.WebPageHeight = 400;
                converter.Options.WebPageWidth = 400;
                advertisementList adlist = new advertisementList();
                adlist = db.advertisementLists.Where(e => e.advertisementRefID == ID).FirstOrDefault();
                byte[] pdf = doc.Save();
                doc.Close();
                FileResult fileResult = new FileContentResult(pdf, "application/pdf");
                fileResult.FileDownloadName = empdetails.Name.ToString() + Guid.NewGuid().ToString() + "news.pdf";
                adlist.advertisementPDFUrk = "https://spotaneedle.com/newspaper_created/" + fileResult.FileDownloadName;
                var filename = fileResult.FileDownloadName;
                db.SaveChanges();
                uploadPDF(GridHtml, filename);
                var emailresult = db.advertisementLists.Where(ex => ex.empID == empID).FirstOrDefault();
                var encryptedID = BaseUtil.encrypt(ID.ToString());
                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toEmployerPostedAdWithLink.html"));
                string HTML_Body = sr.ReadToEnd();
                string newString = HTML_Body.Replace("#name", empdetails.Name).Replace("#refid", encryptedID);
                sr.Close();
                string To = empdetails.Email.ToString();
                string mail_Subject = "NewsPaper Link Creation Successs ";
                profileController objprofileController = new profileController();
                BaseUtil.sendEmailer(To, mail_Subject, newString, "");
                return fileResult;
            }
        }

        //upload PDF

        public void uploadPDF(string GridHtml, string file)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                HtmlToPdf converter = new HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(GridHtml);
                converter.Options.WebPageHeight = 400;
                converter.Options.WebPageWidth = 400;
                doc.Save(Server.MapPath("~/newspaper_created/" + file));
                doc.Close();
            }
        }

        // Job List    

        public ActionResult MyJobs()
        {
            var jobs = (dynamic)null;
            try { 
            ViewBag.jobStatusID = new SelectList(db.jobStatus, "jobStatusID", "statuts");
            int companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
             jobs = db.jobDetails.Where(e => e.companyID == companyID && e.compeletPosted == true).OrderByDescending(e => e.dataIsCreated).ToList();
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(jobs);
        }

        //Pending Job List

        public ActionResult pendingJobs()
        {
            var jobs = (dynamic)null;
            try
            {
                int companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
             jobs = db.jobDetails.Where(e => e.companyID == companyID && e.compeletPosted == false).OrderByDescending(e => e.dataIsCreated).ToList();
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(jobs);
        }



        public string jobStaus(int selectedVal, long jobID)
        {
            jobDetail oInterViewerComment = new jobDetail();
            oInterViewerComment = db.jobDetails.Where(e => e.jobID == jobID).FirstOrDefault();
            oInterViewerComment.jobStatusID = selectedVal;
            oInterViewerComment.requiredSkills = "closed";
            try
            {
                db.Entry(oInterViewerComment).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                BaseUtil.CaptureErrorValues(ex);


            }
            return "OK";
        }
    }
}
