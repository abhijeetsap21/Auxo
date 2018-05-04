using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;
using System.Net;
using System.Data.Entity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.IO;
using Supremes;

namespace NewLetter.Controllers
{
    [CustomErrorHandling]
    public class EmployerController : BaseClass
    {
        //
        // GET: /Employer/
        oriondbEntities db = null;
        profileController p = new profileController();
        public EmployerController()
        {
            db = new oriondbEntities();
        }

        public ActionResult Dashbord()
        {
            int roleID = Convert.ToInt16(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));
            long CompanyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            try
            {
                var jobDashborad = db.Database.SqlQuery<sp_EmployerDashborad_Result>("sp_EmployerDashborad @companyID='" + CompanyID + "'").ToList();
                return View(jobDashborad);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult Comm(long jobID) {
            try
            {
                var qenDetailss = db.qenMialSendInteresteds.Where(e => e.jobID == jobID && e.mailReceivedjobChancgeInterested == true).ToList();
                return View(qenDetailss);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }
        public ActionResult category(int? jobID)
        {
            ViewBag.jobID = jobID;
            return View();
        }

        public ActionResult categoryC1(int? jobID)
        {
            string q = "select '0',qenName,qenEmail,qendidateList.qenID,case when qenMialSendInterested.mailReceivedjobChancgeInterested is null then '0'  else mailReceivedjobChancgeInterested end as mailReceivedjobChancgeInterested  from qendidateListInJob left outer join qendidateList on qendidateListInJob.qenID = qendidateList.qenID left outer join qenMialSendInterested on qendidateListInJob.qenID =qenMialSendInterested.qenID  and qendidateListInJob.jobID =qenMialSendInterested.jobID where qendidateListInJob.jobID = '" + jobID + "' AND category = 1";
            try
            {
                var qenList = db.Database.SqlQuery<mailReceivedInterested>(q).ToList();
                return PartialView("_partialCategory1", qenList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult categoryC2(int? jobID)
        {
            string q = "select '0',qenName,qenEmail,qendidateList.qenID,case when qenMialSendInterested.mailReceivedjobChancgeInterested is null then '0'  else mailReceivedjobChancgeInterested end as mailReceivedjobChancgeInterested  from qendidateListInJob left outer join qendidateList on qendidateListInJob.qenID = qendidateList.qenID left outer join qenMialSendInterested on qendidateListInJob.qenID =qenMialSendInterested.qenID   and qendidateListInJob.jobID =qenMialSendInterested.jobID where qendidateListInJob.jobID = '" + jobID + "' AND category = 2";
            try
            {
                var qenList = db.Database.SqlQuery<mailReceivedInterested>(q).ToList();
                return PartialView("_partialCategory2", qenList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
            
        }

        public void notify1(int[] arrnew1, long jobID)
        {
            var companyemail = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
            var companyName = BaseUtil.GetSessionValue(AdminInfo.FullName.ToString());
            qenMialSendInterested qenInterested = new qenMialSendInterested();
            int rowcount = arrnew1.Count();
            try
            {
                var jobDetails = db.jobDetails.FirstOrDefault(e => e.jobID == jobID);

                if (jobDetails != null)
                {
                    StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("/Emailer/areYouInterestedforJob.html"));
                    string HTML_Body = sr.ReadToEnd();
                    for (long i = 0; i < rowcount; i++)
                    {
                        long row = arrnew1[i];
                        var emailresult = db.qendidateLists.Where(ex => ex.qenID == row).Select(e => new { e.qenEmail, e.qenName }).FirstOrDefault();
                        if (emailresult != null)
                        {
                            qenInterested = db.qenMialSendInteresteds.Where(e => e.jobID == jobID && e.qenID == row).FirstOrDefault();
                            if (qenInterested != null)
                            {
                                qenInterested.mailSentDateTime = DateTime.Now;
                                qenInterested.dataIsUpdated = DateTime.Now;
                                db.Entry(qenInterested).State = EntityState.Modified;
                                db.SaveChanges();

                                //----------------------------use below code to send emailer------------------------------------------------------------
                                string jobUrl = "http://localhost:51126/Employer/submitInterest?qenid=" + BaseUtil.encrypt(arrnew1[i].ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                                string final_Html_Body = HTML_Body.Replace("#name", emailresult.qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                                sr.Close();
                                string To = emailresult.qenEmail.ToString();
                                string mail_Subject = "Your profile short listed by " + companyName;
                                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                                //----------------------------end to send emailer------------------------------------------------------------

                            }
                            else
                            {
                                qenInterested = new qenMialSendInterested();
                                qenInterested.qenID = row;
                                qenInterested.jobID = jobID;
                                qenInterested.mailReceivedjobChancgeInterested = false;
                                qenInterested.mailSentDateTime = DateTime.Now;
                                qenInterested.mailSentjobChancgeInterested = true;
                                qenInterested.mailReceivedDatetime = DateTime.Now;
                                qenInterested.dataIsCreated = DateTime.Now;
                                qenInterested.dataIsUpdated = DateTime.Now;
                                db.qenMialSendInteresteds.Add(qenInterested);
                                db.SaveChanges();

                                //----------------------------use below code to send emailer------------------------------------------------------------
                                string jobUrl = "http://localhost:51126/Employer/submitInterest?qenid=" + BaseUtil.encrypt(arrnew1[i].ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                                string final_Html_Body = HTML_Body.Replace("#name", emailresult.qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                                sr.Close();
                                string To = emailresult.qenEmail.ToString();
                                string mail_Subject = "Your profile short listed by " + companyName;

                                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                                //----------------------------end to send emailer------------------------------------------------------------

                            }
                            //eventName : 1 for view, 2 for contact, 3 for download
                            p.UpdateProfilePerformance(qenInterested.qenID, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
            }       

        }

        public void notify2(int[] arrnew2, long jobID)
        {
            var companyemail = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
            var companyName = BaseUtil.GetSessionValue(AdminInfo.FullName.ToString());
            qenMialSendInterested qenInterested = new qenMialSendInterested();
            int rowcount = arrnew2.Count();
            try
            {
                var jobDetails = db.jobDetails.FirstOrDefault(e => e.jobID == jobID);
                if (jobDetails != null)
                {

                    StreamReader sr = new StreamReader(Server.MapPath("/Emailer/areYouInterestedforJob.html"));
                    string HTML_Body = sr.ReadToEnd();

                    for (long i = 0; i < rowcount; i++)
                    {

                        long row = arrnew2[i];
                        var emailresult = db.qendidateLists.Where(ex => ex.qenID == row).Select(e => new { e.qenEmail, e.qenName }).FirstOrDefault();
                        if (emailresult != null)
                        {
                            qenInterested = db.qenMialSendInteresteds.Where(e => e.jobID == jobID && e.qenID == row).FirstOrDefault();
                            if (qenInterested != null)
                            {
                                qenInterested.mailSentDateTime = DateTime.Now;
                                qenInterested.dataIsUpdated = DateTime.Now;
                                db.Entry(qenInterested).State = EntityState.Modified;
                                db.SaveChanges();
                                //----------------------------use below code to send emailer------------------------------------------------------------
                                string jobUrl = "http://localhost:51126/Employer/submitInterest?qenid=" + BaseUtil.encrypt(arrnew2[i].ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                                string final_Html_Body = HTML_Body.Replace("#name", emailresult.qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                                sr.Close();
                                string To = emailresult.qenEmail.ToString();
                                string mail_Subject = "Your profile short listed by " + companyName;

                                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                                //----------------------------end to send emailer------------------------------------------------------------
                            }
                            else
                            {
                                qenInterested = new qenMialSendInterested();
                                qenInterested.qenID = row;
                                qenInterested.jobID = jobID;
                                qenInterested.mailReceivedjobChancgeInterested = false;
                                qenInterested.mailSentDateTime = DateTime.Now;
                                qenInterested.mailSentjobChancgeInterested = true;
                                qenInterested.mailReceivedDatetime = DateTime.Now;
                                qenInterested.dataIsCreated = DateTime.Now;
                                qenInterested.dataIsUpdated = DateTime.Now;
                                db.qenMialSendInteresteds.Add(qenInterested);
                                //----------------------------use below code to sendemailer------------------------------------------------------------
                                db.SaveChanges();
                                string jobUrl = "http://localhost:51126/Employer/submitInterest?qenid=" + BaseUtil.encrypt(arrnew2[i].ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                                string final_Html_Body = HTML_Body.Replace("#name", emailresult.qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                                sr.Close();
                                string To = emailresult.qenEmail.ToString();
                                string mail_Subject = "Job matching for your profile";

                                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                                //----------------------------end to send emailer------------------------------------------------------------
                            }
                            //eventName : 1 for view, 2 for contact, 3 for download
                            p.UpdateProfilePerformance(qenInterested.qenID, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {               
                BaseUtil.CaptureErrorValues(ex);                
            }
        }
        public ActionResult categoryC3(int? jobID)
        {
            try
            {
                string q = "select '0',qenName,qenEmail,qendidateList.qenID,case when qenMialSendInterested.mailReceivedjobChancgeInterested is null then '0'  else mailReceivedjobChancgeInterested end as mailReceivedjobChancgeInterested  from qendidateListInJob left outer join qendidateList on qendidateListInJob.qenID = qendidateList.qenID left outer join qenMialSendInterested on qendidateListInJob.qenID =qenMialSendInterested.qenID  and qendidateListInJob.jobID =qenMialSendInterested.jobID where qendidateListInJob.jobID = '" + jobID + "' AND category = 3";
                var qenList = db.Database.SqlQuery<mailReceivedInterested>(q).ToList();
                return PartialView("_partialCategory3", qenList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public void notify3(int[] arrnew3, long jobID)
        {
            var companyemail = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
            var companyName = BaseUtil.GetSessionValue(AdminInfo.FullName.ToString());
            qenMialSendInterested qenInterested = new qenMialSendInterested();
            int rowcount = arrnew3.Count();
            try
            {
                var jobDetails = db.jobDetails.FirstOrDefault(e => e.jobID == jobID);
                if (jobDetails != null)
                {

                    StreamReader sr = new StreamReader(Server.MapPath("/Emailer/areYouInterestedforJob.html"));
                    string HTML_Body = sr.ReadToEnd();


                    for (long i = 0; i < rowcount; i++)
                    {

                        long row = arrnew3[i];
                        var emailresult = db.qendidateLists.Where(ex => ex.qenID == row).Select(e => new { e.qenEmail, e.qenName }).FirstOrDefault();
                        if (emailresult != null)
                        {
                            qenInterested = db.qenMialSendInteresteds.Where(e => e.jobID == jobID && e.qenID == row).FirstOrDefault();
                            if (qenInterested != null)
                            {
                                qenInterested.mailSentDateTime = DateTime.Now;
                                qenInterested.dataIsUpdated = DateTime.Now;
                                db.Entry(qenInterested).State = EntityState.Modified;
                                db.SaveChanges();
                                //----------------------------use below code to send emailer------------------------------------------------------------
                                string jobUrl = "http://localhost:51126/Employer/submitInterest?qenid=" + BaseUtil.encrypt(arrnew3[i].ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                                string final_Html_Body = HTML_Body.Replace("#name", emailresult.qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                                sr.Close();
                                string To = emailresult.qenEmail.ToString();
                                string mail_Subject = "Your profile short listed by " + companyName;

                                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                                //----------------------------end to send emailer------------------------------------------------------------
                            }
                            else
                            {
                                qenInterested = new qenMialSendInterested();
                                qenInterested.qenID = row;
                                qenInterested.jobID = jobID;
                                qenInterested.mailReceivedjobChancgeInterested = false;
                                qenInterested.mailSentDateTime = DateTime.Now;
                                qenInterested.mailSentjobChancgeInterested = true;
                                qenInterested.mailReceivedDatetime = DateTime.Now;
                                qenInterested.dataIsCreated = DateTime.Now;
                                qenInterested.dataIsUpdated = DateTime.Now;
                                db.qenMialSendInteresteds.Add(qenInterested);
                                db.SaveChanges();
                                //----------------------------use below code to send emailer------------------------------------------------------------
                                string jobUrl = "http://localhost:51126/Employer/submitInterest?qenid=" + BaseUtil.encrypt(arrnew3[i].ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                                string final_Html_Body = HTML_Body.Replace("#name", emailresult.qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                                sr.Close();
                                string To = emailresult.qenEmail.ToString();
                                string mail_Subject = "Your profile short listed by " + companyName;
                                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                                //----------------------------end to send emailer------------------------------------------------------------
                            }
                            //eventName : 1 for view, 2 for contact, 3 for download
                            p.UpdateProfilePerformance(qenInterested.qenID, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);               
            }
        }

        public ActionResult submitInterest()
        {
            long jobID = 0; long qenID = 0; 
            if (!string.IsNullOrEmpty(Request.QueryString["jobID"]))
            {
                jobID = Convert.ToInt64(BaseUtil.Decrypt(Request.QueryString["jobID"].ToString()));
            }
            if (!string.IsNullOrEmpty(Request.QueryString["qenID"]))
            {
                qenID = Convert.ToInt64(BaseUtil.Decrypt(Request.QueryString["qenID"].ToString()));
            }
            var emailresult = db.qendidateLists.Where(ex => ex.qenID == qenID).Select(e => new { e.qenEmail, e.qenName }).FirstOrDefault();
            
            qenMialSendInterested qenInterested = new qenMialSendInterested();
            qendidateTestSchedule testSchedule = new qendidateTestSchedule();

            qenInterested = db.qenMialSendInteresteds.Where(ex => ex.qenID == qenID && ex.jobID == jobID).FirstOrDefault();
            try
            {
                if (qenInterested != null)
                {
                    qenInterested.mailReceivedjobChancgeInterested = true;
                    qenInterested.mailReceivedDatetime = DateTime.Now;
                    qenInterested.dataIsUpdated = DateTime.Now;
                    db.Entry(qenInterested).State = EntityState.Modified;
                    db.SaveChanges();
                }

                testSchedule.jobID = jobID;
                testSchedule.qenID = qenID;
                testSchedule.mailSentTestScheduled = true;
                testSchedule.mailSentTestScheduled = false;
                testSchedule.dataIsCreated = DateTime.Now;
                testSchedule.dataIsUpdated = DateTime.Now;
                testSchedule.ReasonReschedule = null;
                testSchedule.testScheduleCountInt = 0;
                testSchedule.examMarks = 0;
                testSchedule.isExamAttempted = false;
                testSchedule.isExamSubmitted = false;
                testSchedule.IsMeetScheduled = false;
                testSchedule.slotID = 1;
                db.qendidateTestSchedules.Add(testSchedule);
                db.SaveChanges();



                var jobDetails = db.jobDetails.Include(e => e.companyDetail).FirstOrDefault(e => e.jobID == jobID);
                if (jobDetails != null)
                {
                    //----------------------------use below code to send emailer------------------------------------------------------------

                    string jobUrl = "http://localhost:51126/profile/selectTest?qenid=" + BaseUtil.encrypt(qenID.ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                    StreamReader sr = new StreamReader(Server.MapPath("/Emailer/testTimeSchedular.html"));
                    string HTML_Body = sr.ReadToEnd();
                    string final_Html_Body = HTML_Body.Replace("#name", emailresult.qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", jobDetails.companyDetail.companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                    sr.Close();
                    string To = emailresult.qenEmail.ToString();
                    string mail_Subject = "Schedule your online exam " + jobDetails.companyDetail.companyName.ToString();

                    string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                    //----------------------------end to send emailer------------------------------------------------------------

                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);                
            }
            return View();
        }

        
        public ActionResult scheduleTestDashboard(long jobID)
        {
            if (jobID != 0)
            {
                var qenSchedultestList = " WITH TopPerDay AS" +
                                       "( SELECT max(dataIsCreated) as 'a'  FROM qendidateTestSchedule where jobID='" + jobID + "' group by(qendidateTestSchedule.qenID)   )" +
                                        "SELECT distinct qendidateTestSchedule.qenID ,jobID,qendidateList.qenName,mailReceivedscheduled,qendidateList.qenEmail,testScheduleCountInt,testScheduledDateTime, 0 as 'matchSkills' " +
                                        "FROM qendidateTestSchedule right outer join TopPerDay on  TopPerDay.a = qendidateTestSchedule.dataIsCreated left outer join qendidateList on qendidateTestSchedule.qenID =qendidateList.qenID;  ";
                try
                {
                    var qenList = db.Database.SqlQuery<mailReceivedtestSchedule>(qenSchedultestList).ToList();

                    foreach (var k in qenList)
                    {
                        k.matchSkills = BaseUtil.matching_skills((long)jobID, k.qenID);
                    }

                    return View(qenList);
                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message.ToString();
                    BaseUtil.CaptureErrorValues(ex);
                    return RedirectToAction("Error");
                }
            }
            return View();
        }


        public string rescheduleTest(long qenID, string qenName, string qenEmail, long jobID)
        {
            //mail send code if yes then proceed further
            qendidateTestSchedule obqendidateTestSchedule = new qendidateTestSchedule();
            try
            {
                var oqendidateTestSchedule = db.qendidateTestSchedules.Where(e => e.jobID == jobID && e.qenID == qenID).FirstOrDefault();
                if (oqendidateTestSchedule != null)
                {
                    oqendidateTestSchedule.testScheduleCountInt += 1;

                    obqendidateTestSchedule.jobID = jobID;
                    obqendidateTestSchedule.qenID = qenID;
                    obqendidateTestSchedule.mailSentTestScheduled = true;
                    obqendidateTestSchedule.mailReceivedscheduled = false;
                    obqendidateTestSchedule.testScheduleCountInt = oqendidateTestSchedule.testScheduleCountInt;
                    obqendidateTestSchedule.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    obqendidateTestSchedule.testScheduledDateTime = Convert.ToDateTime("1900-01-01");
                    obqendidateTestSchedule.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    db.qendidateTestSchedules.Add(obqendidateTestSchedule);
                    db.SaveChanges();
                    var jobDetails = db.jobDetails.Include(e => e.companyDetail).FirstOrDefault(e => e.jobID == jobID);
                    if (jobDetails != null)
                    {
                        //----------------------------use below code to send emailer------------------------------------------------------------

                        string jobUrl = "http://localhost:51126/profile/selectTest?qenid=" + BaseUtil.encrypt(qenID.ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                        StreamReader sr = new StreamReader(Server.MapPath("/Emailer/testTimeSchedular.html"));
                        string HTML_Body = sr.ReadToEnd();
                        string final_Html_Body = HTML_Body.Replace("#name", qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", jobDetails.companyDetail.companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID));
                        sr.Close();
                        string To = qenEmail.ToString();
                        string mail_Subject = "Reschedule your online exam " + jobDetails.companyDetail.companyName.ToString();

                        string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                        //----------------------------end to send emailer------------------------------------------------------------
                    }
                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);               
            }
            return "ok";
        }

        public ActionResult rescheduleTestHistory(long qenID, long jobID)
        {
            var OqendidateTestSchedule = db.qendidateTestSchedules.Include(e => e.qendidateList).Where(e => e.qenID == qenID && e.jobID == jobID).OrderByDescending(e => e.dataIsCreated);
            return View(OqendidateTestSchedule.ToList());
        }       

        public ActionResult ProceedToMeetSchedule(long jobID )
        {
            string ExamHistory1 = string.Empty;
                       
                ExamHistory1 = "WITH TopPerDay AS(SELECT max(mailSendID) as 'a'  FROM qendidateTestSchedule where jobID = '"+jobID+ "'and isExamAttempted = 1 group by(qendidateTestSchedule.qenID) ) " +
                                      " select distinct qendidateTestSchedule.qenID ,jobID,qendidateList.qenName, isExamAttempted,isExamSubmitted,examMarks,qendidateList.qenEmail, 0 as 'matchSkills', a as mailSendID, " +
                                     " IsMeetScheduled FROM qendidateTestSchedule right outer join TopPerDay on TopPerDay.a = qendidateTestSchedule.mailSendID left outer join qendidateList on  " +
                                      " qendidateTestSchedule.qenID = qendidateList.qenID";
            try
            {

                var ExamHistory_ = db.Database.SqlQuery<ExamHistory>(ExamHistory1).ToList();

                foreach (var k in ExamHistory_)
                {
                    k.matchSkills = BaseUtil.matching_skills((long)jobID, k.qenID);
                }
                return View(ExamHistory_);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public string ProceedToMeet(long qenID, string qenName, string qenEmail, long jobID, string IsMeetScheduled, long mailSendID, string examMarks)
        {
            string result = "";

            if (examMarks == "-")
            {
                var exmMarks = db.qendidateTestSchedules.Where(e => e.qenID == qenID && e.jobID == jobID).Select(e=>new  { e.examMarks}).FirstOrDefault();
                examMarks = exmMarks.examMarks.ToString();
            }
            var jobDetails = db.jobDetails.Include(e => e.companyDetail).FirstOrDefault(e => e.jobID == jobID);
            try
            {
                if (jobDetails != null)
                {
                    //----------------------------use below code to send emailer------------------------------------------------------------

                    string jobUrl = "https://spotaneedle.com/profile/selectmeet?qenid=" + BaseUtil.encrypt(qenID.ToString()) + "&jobID=" + BaseUtil.encrypt(jobID.ToString());
                    StreamReader sr = new StreamReader(Server.MapPath("/Emailer/proceedToMeet.html"));
                    string HTML_Body = sr.ReadToEnd();
                    string final_Html_Body = HTML_Body.Replace("#name", qenName.ToString()).Replace("#jobTitle", jobDetails.jobTitle).Replace("#companyName", jobDetails.companyDetail.companyName).Replace("#url", jobUrl).Replace("#salary", jobDetails.salary.ToString() + " Monthly").Replace("#location", jobDetails.city).Replace("#skills", BaseUtil.JOb_skillSet(jobID)).Replace("#marks", examMarks);
                    sr.Close();
                    string To = qenEmail.ToString();
                    string mail_Subject = "Schedule your online meet with " + jobDetails.companyDetail.companyName.ToString();

                    result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                    //----------------------------end to send emailer------------------------------------------------------------
                }

                //mail send code if yes then proceed further
                if (result == "ok")
                {
                    if (IsMeetScheduled == "False")
                    {

                        var oqendidateTestSchedule = db.qendidateTestSchedules.Where(e => e.mailSendID == mailSendID).FirstOrDefault();
                        oqendidateTestSchedule.IsMeetScheduled = true;
                        db.Entry(oqendidateTestSchedule).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    var obqenInterviewSchedule1 = db.qenInterviewSchedules.Where(e => e.JobID == jobID && e.qenID == qenID).FirstOrDefault();
                    if (obqenInterviewSchedule1 == null)
                    {
                        var doc = Dcsoup.Parse(new Uri("https://interviews.skype.com/en/Quick/"), 5000);
                        var meetURL = doc.Select("div.link").Html;
                        qenInterviewSchedule obqenInterviewSchedule = new qenInterviewSchedule();
                        obqenInterviewSchedule.reScheduled = 0;
                        obqenInterviewSchedule.JobID = jobID;
                        obqenInterviewSchedule.qenID = qenID;
                        obqenInterviewSchedule.meetScheduledMailSent = true;
                        obqenInterviewSchedule.meetScheduledMailRecieved = false;
                        obqenInterviewSchedule.reScheduled = 0;
                        obqenInterviewSchedule.meetMailSentDateTime = BaseUtil.GetCurrentDateTime();
                        obqenInterviewSchedule.meetMailRepliedDateTime = Convert.ToDateTime("1900-01-01");
                        obqenInterviewSchedule.dataIsCreated = BaseUtil.GetCurrentDateTime();
                        obqenInterviewSchedule.dataIsUpdated = Convert.ToDateTime("1900-01-01");
                        obqenInterviewSchedule.meetScheduledDateTime = Convert.ToDateTime("1900-01-01");
                        obqenInterviewSchedule.meetURL = meetURL;
                        obqenInterviewSchedule.companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
                        db.qenInterviewSchedules.Add(obqenInterviewSchedule);

                        db.SaveChanges();
                    }
                    else
                    {
                        var doc = Dcsoup.Parse(new Uri("https://interviews.skype.com/en/Quick/"), 5000);
                        var meetURL = doc.Select("div.link").Html;
                        qenInterviewSchedule obqenInterviewSchedule = new qenInterviewSchedule();
                        long meetID = db.qenInterviewSchedules.Where(e => e.JobID == jobID && e.qenID == qenID).Max(e => e.meetID);
                        var reschedule = db.qenInterviewSchedules.Where(e => e.meetID == meetID).Select(e => new { e.reScheduled }).FirstOrDefault();
                        obqenInterviewSchedule = obqenInterviewSchedule1;
                        //update old record to set slot time null to free slot 
                        obqenInterviewSchedule.slotID = null;
                        obqenInterviewSchedule.meetURL = meetURL;
                        db.Entry(obqenInterviewSchedule).State = EntityState.Modified;
                        db.SaveChanges();

                        obqenInterviewSchedule.reScheduled = Convert.ToInt16(reschedule.reScheduled) + 1;
                        db.qenInterviewSchedules.Add(obqenInterviewSchedule);
                        db.SaveChanges();
                    }
                    return "ok";
                }
                else
                {
                    return "no";
                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);
                return "Error";
            }

        }

        public ActionResult CandidateComm(long jobID, Int64 ? qenID)
        {
            var query = string.Empty;
            if (qenID != null)
            {
                query = " WITH TopPerDay AS(SELECT max(meetID) as 'a'  FROM qenInterviewSchedule where jobID = '" + jobID + "' AND qenInterviewSchedule.qenID='"+ qenID + "' group by(qenInterviewSchedule.qenID) )" +
                      " select qenInterviewSchedule.qenID ,meetURL,jobID,qendidateList.qenName,  meetScheduledMailRecieved,meetPreferredMedium, reScheduled,qendidateList.qenEmail, a as meetID,meetScheduledDateTime " +
                     " ,slots.slotTime FROM qenInterviewSchedule right outer join TopPerDay on TopPerDay.a = qenInterviewSchedule.meetID left outer join qendidateList on " +
                      " qenInterviewSchedule.qenID = qendidateList.qenID  left outer join slots on qenInterviewSchedule.slotID=slots.slotID ";
            }
            else {
                query = " WITH TopPerDay AS(SELECT max(meetID) as 'a'  FROM qenInterviewSchedule where jobID = '"+ jobID + "' group by(qenInterviewSchedule.qenID) )" +
                       " select qenInterviewSchedule.qenID ,meetURL,jobID,qendidateList.qenName,  meetScheduledMailRecieved,meetPreferredMedium, reScheduled,qendidateList.qenEmail, a as meetID,meetScheduledDateTime " +
                       " ,slots.slotTime FROM qenInterviewSchedule right outer join TopPerDay on TopPerDay.a = qenInterviewSchedule.meetID left outer join qendidateList on " +
                       " qenInterviewSchedule.qenID = qendidateList.qenID  left outer join slots on qenInterviewSchedule.slotID=slots.slotID ";
            }

            try
            {
                var CandidateComm_ = db.Database.SqlQuery<CandidateCom>(query).ToList();
                return View(CandidateComm_);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult rescheduleMeetHistory(long qenID, long jobID)
        {
            try
            {
                var OqendidateMeetSchedule = db.qenInterviewSchedules.Include(e => e.qendidateList).Include(e => e.slot).Where(e => e.qenID == qenID && e.JobID == jobID).OrderByDescending(e => e.dataIsCreated);
                return View(OqendidateMeetSchedule.ToList());
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }

        }

        [HttpGet]
        public ActionResult Newcomments(long jobID, long qenID)
        {
            try
            {
                ViewBag.Candidate_status = new SelectList(db.Candidate_status.Where(e => e.isActive == true), "Candidate_status1", "status");
                InterViewerComment obInterViewerComment = new InterViewerComment();
                var salary = db.InterViewerComments.Where(e => e.qenID == qenID && e.jobID == jobID).Select(e => new { e.ExpectedSalaryMonthly, e.ExpectedSalaryGross, e.Department }).FirstOrDefault();
                if (salary != null)
                {
                    obInterViewerComment.ExpectedSalaryGross = salary.ExpectedSalaryGross;
                    obInterViewerComment.ExpectedSalaryMonthly = salary.ExpectedSalaryMonthly;
                    obInterViewerComment.Department = salary.Department;
                }
                else
                {
                    obInterViewerComment.ExpectedSalaryGross = 0;
                    obInterViewerComment.ExpectedSalaryMonthly = 0;
                    obInterViewerComment.Department = "";
                }
                obInterViewerComment.jobID = jobID;
                obInterViewerComment.qenID = qenID;
                obInterViewerComment.EmployerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));

                obInterViewerComment.Comment = "";
                ViewBag.result = "no";
                return PartialView("_partialNewComment", obInterViewerComment);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }

        }
        [HttpPost]
        public string Newcomments(FormCollection frm)
        {
            InterViewerComment obInterViewerComment = new InterViewerComment();
            try
            {
                obInterViewerComment.qenID = Convert.ToInt64(frm["qenID"]);
                obInterViewerComment.jobID = Convert.ToInt64(frm["jobID"].ToString());
                obInterViewerComment.Comment = frm["Comment"].ToString();
                obInterViewerComment.Department = frm["Department"].ToString();
                obInterViewerComment.Candidate_status = Convert.ToInt16(frm["Candidate_status"].ToString());
                obInterViewerComment.EmployerID = Convert.ToInt64(frm["EmployerID"].ToString());
                obInterViewerComment.ExpectedSalaryGross = Convert.ToInt16(frm["ExpectedSalaryGross"]);
                obInterViewerComment.ExpectedSalaryMonthly = Convert.ToInt16(frm["ExpectedSalaryMonthly"]);
                if (ModelState.IsValid)
                {
                    db.InterViewerComments.Add(obInterViewerComment);
                    db.SaveChanges();
                    Int64 jobID = Convert.ToInt16(frm["jobID"]); Int64 qenID = Convert.ToInt16(frm["QenID"]);
                    //---------------------------


                    HiredCandidate oHiredCandidate = null;
                    oHiredCandidate = db.HiredCandidates.Where(e => e.jobID == jobID && e.qenID == qenID).FirstOrDefault();
                    if (oHiredCandidate == null)
                    {
                        oHiredCandidate = new HiredCandidate();
                        oHiredCandidate.ExpectedSalaryGross = Convert.ToInt16(frm["ExpectedSalaryGross"]);
                        oHiredCandidate.ExpectedSalaryMonthly = Convert.ToInt16(frm["ExpectedSalaryMonthly"]);
                        oHiredCandidate.qenID = qenID;
                        oHiredCandidate.jobID = jobID;
                        oHiredCandidate.EmployerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                        oHiredCandidate.Designtion = null;
                        oHiredCandidate.joinningDate = Convert.ToDateTime("1900-01-01");
                        oHiredCandidate.SpecialComment = null;
                        db.HiredCandidates.Add(oHiredCandidate);
                        db.SaveChanges();
                        obInterViewerComment = null;

                        ViewBag.Candidate_status = new SelectList(db.Candidate_status.Where(e => e.isActive == true), "Candidate_status1", "status");
                        return "ok";
                    }
                    else
                    {

                        oHiredCandidate.ExpectedSalaryGross = Convert.ToInt16(frm["ExpectedSalaryGross"]);
                        oHiredCandidate.ExpectedSalaryMonthly = Convert.ToInt16(frm["ExpectedSalaryMonthly"]);
                        oHiredCandidate.qenID = qenID;
                        oHiredCandidate.jobID = jobID;
                        oHiredCandidate.EmployerID = 4;
                        db.HiredCandidates.Add(oHiredCandidate);
                        db.Entry(oHiredCandidate).State = EntityState.Modified;
                        obInterViewerComment = null;
                        ViewBag.saved = "ok";
                        ViewBag.Candidate_status = new SelectList(db.Candidate_status.Where(e => e.isActive == true), "Candidate_status1", "status");
                        return "ok";
                    }
                }
                ViewBag.saved = "no";
                ViewBag.Candidate_status = new SelectList(db.Candidate_status, "Candidate_status1", "status");
                return "no";
            }
            catch (Exception ex)
            {

                BaseUtil.CaptureErrorValues(ex);
                return ex.Message;
            }
        }

        public ActionResult Oldcomments(long jobID, long qenID)
        {
            try
            { 
            var obInterViewerComments = db.InterViewerComments.Include(e => e.EmployerDetail).Include(e => e.Candidate_status1).Where(e => e.jobID == jobID && e.qenID == qenID).ToList();
            return PartialView("_partialOLDComment", obInterViewerComments);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }


        public ActionResult ShortlistedCadidates(long jobID)
        {

            var query = " select qendidateList.qenName,InterViewerComment.*, candidate_status.status, 0 as ExamMarks, 0 as Skills   from [InterViewerComment] " +
                           " left outer join qendidateList on InterViewerComment.qenID = qendidateList.qenID  " +
                           " left outer join candidate_status on InterViewerComment.candidate_status = candidate_status.Candidate_status  " +
                           "  where commentID in( select max(commentID) as commentID from[InterViewerComment] where jobID = '" + jobID + "' group by qenID)";
            try
            {
                var CandidateComm_ = db.Database.SqlQuery<interViewerComment>(query).ToList();
                foreach (var item in CandidateComm_)
                {
                    var ExamMarks = db.qendidateTestSchedules.Where(e => e.jobID == jobID && e.qenID == item.qenID && e.isExamSubmitted == true).Select(e => new { e.examMarks }).FirstOrDefault();
                    if (ExamMarks != null)
                    {
                        item.ExamMark = Convert.ToInt16(ExamMarks.examMarks);
                        item.Skills = BaseUtil.matching_skills(jobID, item.qenID);
                    }
                }
                ViewBag.Candidate_status = new SelectList(db.Candidate_status, "Candidate_status1", "status");
                return View(CandidateComm_);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }


        public string candidateStaus(int selectedVal, long jobID, long qenID)
        {
            InterViewerComment oInterViewerComment = new InterViewerComment();
            try
            {
                var commentID = db.InterViewerComments.Where(e => e.jobID == jobID && e.qenID == qenID).Max(e => e.commentID);
                oInterViewerComment = db.InterViewerComments.Find(commentID);
                oInterViewerComment.Candidate_status = selectedVal;
                db.Entry(oInterViewerComment).State = EntityState.Modified;
                db.SaveChanges();
                if (selectedVal != 4)
                {
                    HiredCandidate oHiredCandidate = null;
                    oHiredCandidate = db.HiredCandidates.Where(e => e.jobID == jobID && e.qenID == qenID).FirstOrDefault();
                    if (oHiredCandidate != null)
                    {
                        db.HiredCandidates.Remove(oHiredCandidate);
                        db.SaveChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);                
            }
            return "OK";

        }
        [HttpGet]
        public ActionResult HiredCandidatesComment(long jobID, long qenID)
        {
            HiredCandidate oHiredCandidate = null;
            try
            {
                oHiredCandidate = db.HiredCandidates.Where(e => e.jobID == jobID && e.qenID == qenID).FirstOrDefault();
                if (oHiredCandidate != null)
                {
                    return PartialView("_partialHiredCandidate", oHiredCandidate);
                }
                else
                {
                    oHiredCandidate = new HiredCandidate();
                    var commentID = db.InterViewerComments.Where(e => e.jobID == jobID && e.qenID == qenID).Max(e => e.commentID);
                    if (commentID != null)
                    {
                        var InterViewerComment = db.InterViewerComments.Where(e => e.commentID == commentID).Select(e => new { e.ExpectedSalaryGross, e.ExpectedSalaryMonthly, e.Department }).FirstOrDefault();
                        oHiredCandidate.ExpectedSalaryGross = Convert.ToInt16(InterViewerComment.ExpectedSalaryMonthly);
                        oHiredCandidate.ExpectedSalaryMonthly = Convert.ToInt16(InterViewerComment.ExpectedSalaryGross);
                        oHiredCandidate.Designtion = InterViewerComment.Department;
                        oHiredCandidate.Designtion = InterViewerComment.Department;
                    }
                    else
                    {
                        oHiredCandidate.ExpectedSalaryGross = 0;
                        oHiredCandidate.ExpectedSalaryMonthly = 0;
                        oHiredCandidate.Designtion = "";
                    }

                    oHiredCandidate.qenID = qenID;
                    oHiredCandidate.jobID = jobID;

                    oHiredCandidate.qenID = qenID;
                    oHiredCandidate.jobID = jobID;
                    oHiredCandidate.EmployerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                    return PartialView("_partialHiredCandidate", oHiredCandidate);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public string HiredCandidatesComment(FormCollection frm)
        {
            HiredCandidate oHiredCandidate = null;
            try
            {
                Int64 jobID = Convert.ToInt16(frm["jobID"]); Int64 qenID = Convert.ToInt16(frm["QenID"]);
                oHiredCandidate = db.HiredCandidates.Where(e => e.jobID == jobID && e.qenID == qenID).FirstOrDefault();
                if (oHiredCandidate == null)
                {
                    oHiredCandidate = new HiredCandidate();
                    oHiredCandidate.ExpectedSalaryGross = Convert.ToInt16(frm["ExpectedSalaryGross"]);
                    oHiredCandidate.ExpectedSalaryMonthly = Convert.ToInt16(frm["ExpectedSalaryMonthly"]);
                    oHiredCandidate.qenID = qenID;
                    oHiredCandidate.jobID = jobID;
                    oHiredCandidate.EmployerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                    oHiredCandidate.Designtion = frm["Designtion"].ToString();
                    oHiredCandidate.joinningDate = Convert.ToDateTime(frm["joinningDate"]);
                    oHiredCandidate.SpecialComment = frm["SpecialComment"].ToString();
                    db.HiredCandidates.Add(oHiredCandidate);
                    db.SaveChanges();

                    var companyname = BaseUtil.GetSessionValue(AdminInfo.companyname.ToString());
                    var qendetails = db.qendidateLists.Where(e => e.qenID == qenID).FirstOrDefault();
                    var jobdetails = db.HiredCandidates.Include(e => e.jobDetail).FirstOrDefault(e => e.jobID == jobID);
                    StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateHired.html"));
                    string HTML_Body = sr.ReadToEnd();
                    string final_Html_Body = HTML_Body.Replace("#name", qendetails.qenName.ToString()).Replace("#companyname", companyname).Replace("#position", jobdetails.jobDetail.jobTitle).Replace("#joindate", jobdetails.joinningDate.ToShortDateString()).Replace("##sal", jobdetails.ExpectedSalaryMonthly + " Monthly").Replace("#desg", jobdetails.Designtion);
                    sr.Close();
                    string To = qendetails.qenEmail.ToString();
                    string mail_Subject = "Congratulations , you have been hired by " + companyname;
                    BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                    return "ok";
                }
                else
                {

                    oHiredCandidate.ExpectedSalaryGross = Convert.ToInt16(frm["ExpectedSalaryGross"]);
                    oHiredCandidate.ExpectedSalaryMonthly = Convert.ToInt16(frm["ExpectedSalaryMonthly"]);
                    oHiredCandidate.qenID = qenID;
                    oHiredCandidate.jobID = jobID;
                    oHiredCandidate.EmployerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                    oHiredCandidate.Designtion = frm["Designtion"].ToString();
                    oHiredCandidate.joinningDate = Convert.ToDateTime(frm["joinningDate"]);
                    oHiredCandidate.SpecialComment = frm["SpecialComment"].ToString();
                    db.HiredCandidates.Add(oHiredCandidate);
                    db.Entry(oHiredCandidate).State = EntityState.Modified;
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
                string msg = "";
                return msg = ex.Message;
            }


        }


        public ActionResult HiredCandidateList(long jobID)
        {
            try
            {
                var hiredCandidates = db.HiredCandidates.Include(e => e.qendidateList).Where(e => e.jobID == jobID).ToList();
                return View(hiredCandidates);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }
        //Shortlisted Candidate Mailer

        public string candidateShortlisted(string jobID, string qenID)
        {
            try
            {
                long qid = Convert.ToInt64(qenID);
                long jid = Convert.ToInt64(jobID);
                var companyname = BaseUtil.GetSessionValue(AdminInfo.companyname.ToString());
                var qendetails = db.qendidateLists.Where(e => e.qenID == qid).FirstOrDefault();
                var jobdetails = db.jobDetails.FirstOrDefault(e => e.jobID == jid);
                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateShortlisted.html"));
                string HTML_Body = sr.ReadToEnd();
                string final_Html_Body = HTML_Body.Replace("#name", qendetails.qenName.ToString()).Replace("#companyname", companyname).Replace("#position", jobdetails.jobTitle);
                sr.Close();
                string To = qendetails.qenEmail.ToString();
                string mail_Subject = "Congratulations , you have been Shortlisted by " + companyname;
                BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                return "ok";
            }
            catch (Exception ex)
            {
                
                BaseUtil.CaptureErrorValues(ex);
                string msg = "";
                return msg = ex.Message;
            }
        }

        [HttpGet]
        public ActionResult Comments(long jobID, long qenID)
        {
            ViewBag.jobID_ = jobID;
            ViewBag.qenID_ = qenID;
            return PartialView("_partialComment");
        }


        [HttpGet]
        public ActionResult addColleauge()
        {
            EmployerDetail em = new EmployerDetail();
            var roleidIn = new int[] { 3,2};
            try
            {
                ViewBag.role = db.roles.Where(e => roleidIn.Contains(e.roleID));
                return View(em);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }

        }

        [HttpPost]
        public ActionResult addColleauge(EmployerDetail em)
        {
            try
            {
                em.password = baseClass.GetRandomPasswordString(10);
                em.dataIsCreated = BaseUtil.GetCurrentDateTime();
                em.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                em.isActive = false;
                em.isDelete = false;
                em.companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                db.EmployerDetails.Add(em);
                db.SaveChanges();


                var roleidIn = new int[] { 3, 2 };
                ViewBag.role = db.roles.Where(e => roleidIn.Contains(e.roleID));

                //-------------------------------------------------------------------
                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toEmployerRegistrationSuccess.html"));

                string HTML_Body = sr.ReadToEnd();
                string newString = HTML_Body.Replace("#name", em.Name).Replace("#EMPID", em.Email).Replace("#password", em.password);
                sr.Close();
                string To = em.Email;
                string mail_Subject = "Employer Registration Confirmation ";
                BaseUtil.sendEmailer(To, mail_Subject, newString, "");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }

            return View(em);

        }
    }
   
}







