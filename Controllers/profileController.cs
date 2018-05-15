using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;
using System.Threading.Tasks;
using System.IO;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Data.Entity;
using System.Data.Entity.Validation;
using SelectPdf;
using System.Globalization;
using System.Data.SqlClient;
using static NewLetter.Models.storedProcedureModels;

namespace NewLetter.Controllers
{
    [CustomErrorHandling]
    public class profileController : BaseClass
    {
        oriondbEntities db = null;
        public profileController()
        {
            db = new oriondbEntities();
        }
  

        public ActionResult _partialPInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                qendidateList model = null;
                model = db.qendidateLists.Where(ex => ex.qenID == qenid).FirstOrDefault();
                if (model != null)
                {
                    ViewBag.genderID = new SelectList(db.genderLists, "genderID", "genderName", model.genderID);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }
        // Candidate Main Render 

        //[HttpGet]
        //public ActionResult candidateHome()
        //{
        //    return View();
        //}

        // Verify Details 
        public ActionResult VerifyDetails()
        {
            var phone = BaseUtil.GetSessionValue(AdminInfo.Mobile.ToString());
            if(phone == "9999999999")
            {
                TempData["result"] = "addphone";
                return RedirectToAction("editCandidate");
            }
            if(BaseUtil.GetSessionValue(AdminInfo.mobileVerified.ToString()) == "False")
                {
                    int OTP = BaseUtil.GenerateRandomNo();
                    var email = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
                    var result = db.qendidateLists.Where(e => e.qenEmail == email).FirstOrDefault();
                    result.OTP = OTP;
                    result.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();
                    string message = "Your mobile verification code is " + OTP + "." + "Thanks Team Qendidate";
                    string smsresult = BaseUtil.sendSMS(message, result.qenPhone);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePersonalInfo(qendidateList model, HttpPostedFileBase files)
        {
            try
            {
                String fileName = "";
                if (files != null)
                {
                    fileName = Guid.NewGuid() + "_" + Path.GetFileName(files.FileName);
                    var path = Path.Combine(Server.MapPath("~/Logo/"), fileName);
                    files.SaveAs(path);
                    fileName = "https://spotaneedle.com/Logo/" + fileName;
                }

                qendidateList data = null;
                //if (ModelState.IsValid)
                {
                    data = db.qendidateLists.Where(ex => ex.qenID == model.qenID).FirstOrDefault();
                    if (data != null)
                    {
                        if (data.qenPhone != model.qenPhone)
                        {
                            data.isMobileVerified = false;
                            BaseUtil.SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(model.qenPhone));
                            BaseUtil.SetSessionValue(AdminInfo.mobileVerified.ToString(), Convert.ToString(data.isMobileVerified));
                        }
                        data.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        data.qenEmail = model.qenEmail;
                        data.City = model.City;
                        data.country = model.country;
                        data.state = model.state;
                        data.zipCode = model.zipCode;
                        data.latitude = model.latitude;
                        data.longitude = model.longitude;
                        data.dob = model.dob;
                        data.streetNo = model.streetNo;
                        data.qenAddress = model.qenAddress;
                        data.qenName = model.qenName;
                        data.qenNationality = model.qenNationality;
                        data.qenPassport = model.qenPassport;
                        data.qenLinkdInUrl = model.qenLinkdInUrl;
                        data.qenPhone = model.qenPhone;
                        data.genderID = model.genderID;   
                                        

                        if (fileName != "")
                        {
                            data.qenImage = fileName;
                        }
                        db.SaveChanges();
                        TempData["message"] = "Personal Information Updated Successfully";
                    }
                    else
                    {
                        model.qenImage = fileName;
                        db.qendidateLists.Add(model);
                        db.SaveChanges();
                        TempData["message"] = "Personal Information Updated Successfully";
                    }
                }
                return RedirectToAction("editCandidate");

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult _partialacadInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                ViewBag.qenid = qenid;
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult highSchool(long qenid)
        {
            try
            {
                qenSecondary oqenSecondary = null;
                oqenSecondary = db.qenSecondaries.Where(e => e.qenID == qenid).FirstOrDefault();
                if (oqenSecondary != null)
                {
                    return PartialView("_partialHighSchool", oqenSecondary);
                }
                else
                {
                    oqenSecondary = new qenSecondary();
                    oqenSecondary.qenID = qenid;
                    return PartialView("_partialHighSchool");
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
        [ValidateAntiForgeryToken]
        public ActionResult highSchool(qenSecondary oqenSecondary)
        {
            try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qenSecondaries.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qenSecondaries.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qenSecondaries.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.schoolName = oqenSecondary.schoolName;
                        oqenSecondary1.secondaryBoard = oqenSecondary.secondaryBoard;
                        oqenSecondary1.secondaryPassYear = oqenSecondary.secondaryPassYear;
                        oqenSecondary1.secondaryPercentage = oqenSecondary.secondaryPercentage;
                        oqenSecondary1.secondarySubjects = oqenSecondary.secondarySubjects;
                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                }
                else
                {
                    TempData["msg"] = "Error Updating Details";
                }
                return RedirectToAction("editCandidate");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult higherSchool(long qenid)
        {
            try
            {
                qenHigherSecondary oqenSecondary = null;
                oqenSecondary = db.qenHigherSecondaries.Where(e => e.qenID == qenid).FirstOrDefault();
                if (oqenSecondary != null)
                {
                    return PartialView("_partialHigherSchool", oqenSecondary);
                }
                else
                {
                    oqenSecondary = new qenHigherSecondary();
                    oqenSecondary.qenID = qenid;
                    return PartialView("_partialHigherSchool");
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
        [ValidateAntiForgeryToken]
        public ActionResult higherSchool(qenHigherSecondary oqenSecondary)
        {
            try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qenHigherSecondaries.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qenHigherSecondaries.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qenHigherSecondaries.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.hSecondaryBoard = oqenSecondary.hSecondaryBoard;
                        oqenSecondary1.hSecondaryPassYear = oqenSecondary.hSecondaryPassYear;
                        oqenSecondary1.hSecondaryPercentage = oqenSecondary.hSecondaryPercentage;
                        oqenSecondary1.hSecondarySubjects = oqenSecondary.hSecondarySubjects;
                        oqenSecondary1.schoolName = oqenSecondary.schoolName;
                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                }
                else
                {
                    TempData["msg"] = "fail";
                }
                return RedirectToAction("editCandidate");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Graduation(long qenid)
        {
            try
            {
                qendidateGraduation oqenSecondary = null;
                oqenSecondary = db.qendidateGraduations.Where(e => e.qenID == qenid).FirstOrDefault();
                if (oqenSecondary != null)
                {
                    return PartialView("_partialGraduation", oqenSecondary);
                }
                else
                {
                    oqenSecondary = new qendidateGraduation();
                    oqenSecondary.qenID = qenid;
                    return PartialView("_partialGraduation");
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
        [ValidateAntiForgeryToken]
        public ActionResult Graduation(qendidateGraduation oqenSecondary)
        {
            try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qendidateGraduations.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qendidateGraduations.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qendidateGraduations.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.gradPercentage = oqenSecondary.gradPercentage;
                        oqenSecondary1.subjects = oqenSecondary.subjects;
                        oqenSecondary1.YearPassing = oqenSecondary.YearPassing;
                        oqenSecondary1.collegeName = oqenSecondary.collegeName;
                        oqenSecondary1.collegeUniversity = oqenSecondary.collegeUniversity;
                        oqenSecondary1.courseField = oqenSecondary.courseField;
                        oqenSecondary1.courseName = oqenSecondary.courseName;
                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                }
                else
                {
                    TempData["msg"] = "fail";
                }
                return RedirectToAction("editCandidate");
            }

            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult PostGraduation(long qenid)
        {
            try
            {
                qendidatePGraduation oqenSecondary = null;
                oqenSecondary = db.qendidatePGraduations.Where(e => e.qenID == qenid).FirstOrDefault();
                if (oqenSecondary != null)
                {
                    return PartialView("_partialPostGraduation", oqenSecondary);
                }
                else
                {
                    oqenSecondary = new qendidatePGraduation();
                    oqenSecondary.qenID = qenid;
                    return PartialView("_partialPostGraduation");
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
        [ValidateAntiForgeryToken]
        public ActionResult PostGraduation(qendidatePGraduation oqenSecondary)
        {
            try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qendidatePGraduations.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qendidatePGraduations.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qendidatePGraduations.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.pGradPercentage = oqenSecondary.pGradPercentage;
                        oqenSecondary1.subjects = oqenSecondary.subjects;
                        oqenSecondary1.YearPassing = oqenSecondary.YearPassing;
                        oqenSecondary1.collegeName = oqenSecondary.collegeName;
                        oqenSecondary1.collegeUniversity = oqenSecondary.collegeUniversity;
                        oqenSecondary1.courseField = oqenSecondary.courseField;
                        oqenSecondary1.courseName = oqenSecondary.courseName;
                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["message"] = "Academic Information Updated Successfully";
                    }
                }
                else
                {
                    TempData["msg"] = "fail";
                }
                return RedirectToAction("editCandidate");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult PhdInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                qendidatePHD model = null;
                model = db.qendidatePHDs.Where(ex => ex.qenID == qenid).FirstOrDefault();
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult _partialEmpInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                EmployerModel model = null;
                List<qenEmpDetail> emp = null;
                if (qenid != 0)
                {
                    emp = db.qenEmpDetails.Where(ex => ex.qenID == qenid).ToList();
                    if (emp.Count > 0 && emp != null)
                    {
                        model = new EmployerModel();
                        model.employers = emp;
                        model.qenid = qenid;
                    }
                    else
                    {
                        model = new EmployerModel();
                        model.qenid = qenid;
                    }
                }
                else
                {
                    model = new EmployerModel();
                    model.qenid = qenid;
                }
                ViewBag.qenID = qenid;
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult _partialRefInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                qenrefvalidation model = null;
                List<qenReference> reflist = db.qenReferences.Where(ex => ex.qenID == qenid && ex.isDelete==false).OrderBy(ex => ex.qenRefID).ToList();
                if (reflist != null && reflist.Count() > 1)
                {
                    model = new qenrefvalidation();
                    model.qenRefName1 = reflist[0].qenRefName;
                    model.qenRefPhone1 = reflist[0].qenRefPhone;
                    model.qenRefPosition1 = reflist[0].qenRefPosition;
                    model.qenRefEmail1 = reflist[0].qenRefEmail;
                    model.qenRefID1 = reflist[0].qenRefID;
                    model.qenRefCompany1 = reflist[0].qenRefCompany;
                    model.qenRefName2 = reflist[1].qenRefName;
                    model.qenRefPhone2 = reflist[1].qenRefPhone;
                    model.qenRefPosition2 = reflist[1].qenRefPosition;
                    model.qenRefEmail2 = reflist[1].qenRefEmail;
                    model.qenRefCompany2 = reflist[1].qenRefCompany;
                    model.qenRefID2 = reflist[1].qenRefID;
                }
                else
                {
                    model = new qenrefvalidation();
                    model.qenid = (Int32)qenid;
                }
                ViewBag.qenID_ = qenid;
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRefrenceInfo(qenrefvalidation refinfo)
        {
            try
            {
                List<qenReference> refrence = db.qenReferences.Where(ex => ex.qenID == refinfo.qenid && ex.isDelete == false).ToList();
                qenReference ref1 = null;
                qenReference ref2 = null;
                if (refrence.Count > 0 && refrence != null)
                {
                    ref1 = refrence[0];
                    ref2 = refrence[1];
                    if (ref1 != null)
                    {
                        ref1.qenRefEmail = refinfo.qenRefEmail1;
                        ref1.qenRefCompany = refinfo.qenRefCompany1;
                        ref1.qenRefName = refinfo.qenRefName1;
                        ref1.qenRefPhone = refinfo.qenRefPhone1;
                        ref1.qenRefPosition = refinfo.qenRefPosition1;
                        ref1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        ref1.qenRefID = refinfo.qenRefID1;
                        ref2.qenID = Convert.ToInt32(refinfo.qenid);
                        db.SaveChanges();
                        sendReferenceEmailer(ref1.qenRefEmail, ref1.qenRefName, ref2.qenID.ToString(), ref1.qenRefID.ToString());
                       
                    }
                    if (ref2 != null)
                    {
                        ref2.qenRefEmail = refinfo.qenRefEmail2;
                        ref2.qenRefCompany = refinfo.qenRefCompany2;
                        ref2.qenRefName = refinfo.qenRefName2;
                        ref2.qenRefPhone = refinfo.qenRefPhone2;
                        ref2.qenRefPosition = refinfo.qenRefPosition2;
                        ref2.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        ref2.qenID = Convert.ToInt32(refinfo.qenid);
                        ref2.qenRefID = refinfo.qenRefID2;
                        db.SaveChanges();
                        sendReferenceEmailer(ref2.qenRefEmail, ref2.qenRefName, ref2.qenID.ToString(), ref2.qenRefID.ToString());
                    }
                }
                else
                {
                    ref1 = new qenReference();
                    ref2 = new qenReference();
                    ref1.qenRefEmail = refinfo.qenRefEmail1;
                    ref1.qenRefCompany = refinfo.qenRefCompany1;
                    ref1.qenRefName = refinfo.qenRefName1;
                    ref1.qenRefPhone = refinfo.qenRefPhone1;
                    ref1.qenRefPosition = refinfo.qenRefPosition1;
                    ref1.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    ref1.qenID = Convert.ToInt32(refinfo.qenid);
                    ref2.qenRefEmail = refinfo.qenRefEmail2;
                    ref2.qenRefCompany = refinfo.qenRefCompany2;
                    ref2.qenRefName = refinfo.qenRefName2;
                    ref2.qenRefPhone = refinfo.qenRefPhone2;
                    ref2.qenRefPosition = refinfo.qenRefPosition2;
                    ref2.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    ref2.qenID = Convert.ToInt32(refinfo.qenid);
                    db.qenReferences.Add(ref1);
                    db.qenReferences.Add(ref2);
                    db.SaveChanges();
                    sendReferenceEmailer(ref1.qenRefEmail, ref1.qenRefName, ref2.qenID.ToString(), ref1.qenRefID.ToString());
                    sendReferenceEmailer(ref2.qenRefEmail, ref2.qenRefName, ref2.qenID.ToString(), ref2.qenRefID.ToString());
                }
                return RedirectToAction("editCandidate");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult thankyou()
        {
            return View();
        }

        public PartialViewResult EmploymentAddPop(int? empno, int? qenid)
        {
            qenEmpDetail emp = null;
            try
            {
                if (empno != null && empno > 0)
                {
                    emp = db.qenEmpDetails.Where(ex => ex.qenEmploymentNum == empno).FirstOrDefault();
                }
                else
                {
                    emp = new qenEmpDetail();
                    emp.qenID = Convert.ToInt32(qenid);
                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);                
            }
            return PartialView("_PartialPopUpEmployment", emp);
        }

        [HttpPost]
         [ValidateAntiForgeryToken]
        public ActionResult SaveEmploymentDetails(qenEmpDetail model)
        {
            try
            {               
                qenEmpDetail emp = null;
                if (model.qenEmploymentNum != 0)
                {
                    emp = db.qenEmpDetails.Where(ex => ex.qenEmploymentNum == model.qenEmploymentNum).FirstOrDefault();
                    if (emp != null)
                    {
                        
                        emp.qenPosition = model.qenPosition;
                        emp.qenEmpFrom = model.qenEmpFrom;
                        emp.qenEmpTo = model.qenEmpTo;
                        emp.qenSalary = model.qenSalary;
                        emp.CompanyName = model.CompanyName;
                        emp.jobDescription = model.jobDescription;
                        emp.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.SaveChanges();
                        TempData["message"] = "Employment information updated";
                    }
                }
                else
                {
                    model.qenEmpFrom = model.qenEmpFrom; 
                    model.qenEmpTo = model.qenEmpTo;
                    model.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    db.qenEmpDetails.Add(model);
                    db.SaveChanges();
                    // --------------------------in case new employment histry added update old references -----------------------------------------
                    var refernceCheck = db.qenReferences.Where(e => e.qenID == model.qenID && e.isDelete==false).ToList();
                    foreach (var s in refernceCheck)
                    {
                        s.isDelete = true;
                        db.Entry(s).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    var qenList = db.qendidateLists.Find(model.qenID);
                    qenList.IsReferenceCleared = false;
                    db.Entry(qenList).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Employment information updated";
                }
                return RedirectToAction("editCandidate");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult Deletejob(Int32 empno)
        {
            qenEmpDetail emp = null;
            try
            {
                emp = db.qenEmpDetails.Where(ex => ex.qenEmploymentNum == empno).FirstOrDefault();
                if (emp != null)
                {
                    db.qenEmpDetails.Remove(emp);
                    db.SaveChanges();
                    TempData["message"] = "Employment information deleted";
                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);                
            }
            return RedirectToAction("editCandidate");
        }


        [HttpGet]
        public ActionResult _partialCertifications(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                List<qendidatePHD> qenCertificationsList = null;
                qenCertificationsList = db.qendidatePHDs.Where(ex => ex.qenID == qenid).ToList();

                ViewBag.qenID = qenid;
                return View(qenCertificationsList);
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
                TempData["msg"] = "Error : " + ex.Message.ToString();
                return RedirectToAction("Error");
            }
        }

        public ActionResult DeleteCertifications(long phdid)
        {
            qendidatePHD emp = null;
            try
            {
                emp = db.qendidatePHDs.Where(ex => ex.phdid == phdid).FirstOrDefault();
                if (emp != null)
                {
                    db.qendidatePHDs.Remove(emp);
                    db.SaveChanges();
                }
                TempData["message"] = "Academic Information Updated Successfully";
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);                
            }

            return RedirectToAction("editCandidate");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePhdInfo(qendidatePHD model)
        {
            try
            {
                if (model.courseField != null && model.courseField.Trim() != "")
                {
                    qendidatePHD phd = null;
                    if (model.phdid != 0)
                    {
                        phd = db.qendidatePHDs.Where(ex => ex.phdid == model.phdid).FirstOrDefault();
                        if (phd != null)
                        {
                            phd.courseField = model.courseField;
                            phd.phdRemarks = model.phdRemarks;
                            phd.phdTitle = model.phdTitle;
                            phd.phdStart = model.phdStart;
                            phd.phdEnd = model.phdEnd;
                            phd.collegeName = model.collegeName;
                            phd.collegeUniversity = model.collegeUniversity;
                            phd.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                            db.SaveChanges();
                            TempData["message"] = "Certificate Information Updated Successfully";
                        }
                    }
                    else
                    {
                        model.dataIsCreated = BaseUtil.GetCurrentDateTime();
                        db.qendidatePHDs.Add(model);
                        db.SaveChanges();
                        TempData["message"] = "Certificate Information Added Successfully";
                    }
                }
                return RedirectToAction("editCandidate");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        public PartialViewResult certificationsAddPop(int? phdid, int? qenid)
        {
            qendidatePHD emp = null;
            try
            {
                if (phdid != null && phdid > 0)
                {
                    emp = db.qendidatePHDs.Where(ex => ex.phdid == phdid).FirstOrDefault();
                }
                else
                {
                    emp = new qendidatePHD();
                    emp.qenID = Convert.ToInt32(qenid);
                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);               
            }
            return PartialView("_PartialAddCertifications", emp);
        }

        public ActionResult _partialSkills(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                ViewBag.qenID = qenid;
                var qenSkills = db.qenSkills.Include(q => q.skill).Where(q => q.qenID == qenid);

                return View(qenSkills.ToList());
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
                TempData["msg"] = "Error : " + ex.Message.ToString();
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSkills(qenSkillName model)
        {
            try
            {
                int skill_id = checkValuExist(model.skillName.ToString());
                qenSkill oqenSkill = null;

                if (skill_id == 0)
                {
                    TempData["message"] = "Please Select Skill from the list";
                }

                else
                {

                    oqenSkill = db.qenSkills.Where(ex => ex.skillsID == skill_id && ex.qenID == model.qenID).FirstOrDefault();
                    if (oqenSkill != null)
                    {
                        oqenSkill.skillsID = skill_id;
                        oqenSkill.yearOfExp = model.yearOfExp;
                        oqenSkill.qenID = model.qenID;

                        db.Entry(oqenSkill).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["message"] = "Skills Information Updated Successfully";
                    }
                    else
                    {
                        oqenSkill = new qenSkill();
                        oqenSkill.skillsID = skill_id;
                        oqenSkill.yearOfExp = model.yearOfExp;
                        oqenSkill.qenID = model.qenID;
                        db.qenSkills.Add(oqenSkill);
                        db.SaveChanges();
                        TempData["message"] = "Skill Information Added Successfully";
                    }
                }

                return RedirectToAction("editCandidate");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public PartialViewResult skillsAddPop(int? qenSkillsID, int? qenid)
        {
            
            if (qenSkillsID != null && qenSkillsID > 0)
            {
                try
                {

                    var emp = db.qenSkills.Include(e => e.skill).Where(ex => ex.qenSkillsID == qenSkillsID).FirstOrDefault();
                    qenSkillName oqenSkillName = new qenSkillName();
                    oqenSkillName.skillID = emp.skillsID;
                    oqenSkillName.qenID = Convert.ToInt32(emp.qenID);
                    oqenSkillName.qenSkillsID = Convert.ToInt32(emp.skillsID);
                    oqenSkillName.skillName = emp.skill.skillName;
                    oqenSkillName.yearOfExp = emp.yearOfExp;
                    return PartialView("_partialAddSkills", oqenSkillName);
                }
                catch (Exception ex)
                {                   
                    BaseUtil.CaptureErrorValues(ex);
                    qenSkillName oqenSkillName = new qenSkillName();
                    return PartialView("_partialAddSkills", oqenSkillName);
                }
            }
            else
            {
                qenSkillName oqenSkillName = new qenSkillName();
                return PartialView("_partialAddSkills", oqenSkillName);
            }
        }


      
        public int checkValuExist(string skill_)
        {
            try
            {

                var result = db.skills.Where(e => e.skillName == skill_).Select(x => new { x.skillsID }).SingleOrDefault();
                if (result == null)
                {
                    
                    return 0;
                }
                else
                {
                    return result.skillsID;
                }
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
                return 0;                
            }
        }

        public ActionResult DeleteqenSkills(long qenSkillsID)
        {
            try
            {
                qenSkill oqenSkill = null;
                oqenSkill = db.qenSkills.Where(ex => ex.qenSkillsID == qenSkillsID).FirstOrDefault();
                if (oqenSkill != null)
                {
                    db.qenSkills.Remove(oqenSkill);
                    db.SaveChanges();
                    TempData["message"] = "Skill Information Updated Successfully";
                }
            }
            catch (Exception ex)
            {               
                BaseUtil.CaptureErrorValues(ex);               
            }
            return RedirectToAction("editCandidate");
        }


        public ActionResult Error()
        {
            ViewBag.error = TempData["msg"].ToString();
            return View();
        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        [HttpGet]
        public ActionResult CareerObjective(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                var careerObjective = db.qendidateLists.Where(e => e.qenID == qenid).Select(e => new { e.CareerObjective }).FirstOrDefault();
                if (careerObjective.CareerObjective != null)
                {
                    ViewBag.careerObjective = careerObjective.CareerObjective.ToString();
                }
                else
                {
                    ViewBag.careerObjective = "";
                }
                ViewBag.qenID = qenid;
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public ActionResult CareerObjective(FormCollection frm)
        {
            try
            {
                long qenid = 0;
                string txt = string.Empty;
                qenid = Convert.ToInt64(frm["hdn_qenID"]);
                txt = frm["CareerObjective"].ToString();
                qendidateList oqendidateList = new qendidateList();
                oqendidateList = db.qendidateLists.Find(qenid);
                oqendidateList.CareerObjective = txt;
                db.SaveChanges();
                return RedirectToAction("thankyou");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        //Edit Candidate
        [HttpGet]
        public ActionResult editCandidate()
        {
            TempData["message"] = TempData["message"];
            return View();
        }

        // preview CV

        public ActionResult _partialPreviewCV(Int64 qenID)
        {
            ResumeModel model = new ResumeModel();
            try
            {
                qendidateList personal = db.qendidateLists.Where(ex => ex.qenID == qenID).FirstOrDefault();
                qenSecondary s = db.qenSecondaries.Where(ex => ex.qenID == qenID).FirstOrDefault();
                qenHigherSecondary hs = db.qenHigherSecondaries.Where(ex => ex.qenID == qenID).FirstOrDefault();
                qendidateGraduation g = db.qendidateGraduations.Where(ex => ex.qenID == qenID).FirstOrDefault();
                qendidatePGraduation pg = db.qendidatePGraduations.Where(ex => ex.qenID == qenID).FirstOrDefault();
                List<qenEmpDetail> emp = db.qenEmpDetails.Where(ex => ex.qenID == qenID).ToList();
                List<qendidatePHD> phd = db.qendidatePHDs.Where(ex => ex.qenID == qenID).ToList();
                List<qenReference> refrences = db.qenReferences.Where(ex => ex.qenID == qenID).ToList();
                List<qenSkill> skills = db.qenSkills.Include(ex => ex.skill).Where(ex => ex.qenID == qenID).ToList();
                model.personainfo = personal;
                AcademicModel academic = new AcademicModel();
                academic.graduation = g != null ? g : new qendidateGraduation();
                academic.hsecondary = hs != null ? hs : new qenHigherSecondary();
                academic.secondary = s != null ? s : new qenSecondary();
                academic.pgraduation = pg != null ? pg : new qendidatePGraduation();
                model.educationinfo = academic;
                model.employmentinfo = emp != null ? emp : new List<qenEmpDetail>();
                model.refrences = refrences != null ? refrences : new List<qenReference>();
                model.phdinfo = phd != null ? phd : new List<qendidatePHD>();
                model.skills = skills != null ? skills : new List<qenSkill>();
                //eventName : 1 for view, 2 for contact, 3 for download
                int roleID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));
                if (roleID != 5)
                {
                    UpdateProfilePerformance(qenID, 1);
                }
            }
            catch (Exception ex)
            {               
                BaseUtil.CaptureErrorValues(ex);                
            }
            return View(model);
        }

        // Generate PDF 

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml, string qenID_)
        {
            long qenID = Convert.ToInt64(qenID_);
            //eventName : 1 for view, 2 for contact, 3 for download
            if (Convert.ToInt16(BaseUtil.GetSessionValue(AdminInfo.role_id.ToString())) == 2)
            {
                UpdateProfilePerformance(qenID, 3);
            }
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(GridHtml);
                converter.Options.WebPageHeight = 842;
                converter.Options.WebPageWidth = 595;
                byte[] pdf = doc.Save();
                doc.Close();
                FileResult fileResult = new FileContentResult(pdf, "application/pdf");
                fileResult.FileDownloadName = "Document.pdf";
                return fileResult;
            }
        }

        [HttpGet]
        public ActionResult _careerHighLights()
        {
            long qenid = 0;
            careerObjective ocareerObjective = new careerObjective();
            try
            {
                qenid = (long)Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                if (qenid != 0)
                {
                    var result = db.qendidateLists.Where(e => e.qenID == qenid).Select(e => new { e.CareerObjective, e.CareerHighlight }).FirstOrDefault();
                    ocareerObjective.careerObj = result.CareerObjective;
                    ocareerObjective.careerHighLights = result.CareerHighlight;
                    ocareerObjective.qenID = qenid;
                }
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
            }
            return View("_careerHighLights", ocareerObjective);
        }

        [HttpPost]
        public ActionResult _careerHighLights(careerObjective ocareerObjective)
        {
            long qenid = ocareerObjective.qenID;
            try
            {
                qendidateList oqendidateList = new qendidateList();
                oqendidateList = db.qendidateLists.Where(e => e.qenID == qenid).FirstOrDefault();
                oqendidateList.CareerObjective = ocareerObjective.careerObj;
                oqendidateList.CareerHighlight = ocareerObjective.careerHighLights;
                oqendidateList.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(oqendidateList).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Career highligts Updated Successfully";
            }
            catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
                TempData["msg"] = ex.Message.ToString();
                
            }
            return RedirectToAction("editCandidate");
        }

        [HttpGet]
        public ActionResult AppliedJobs()
        {
            long userID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            DateTime updatbefore = BaseUtil.GetCalculatedDateTime(-30);
            var AppliedJob = db.qenAppliedJobs.Include(e => e.jobDetail).Include(e => e.App_status).Where(e => e.dataIsCreated >= updatbefore && e.qenID == userID).OrderByDescending(e => e.dataIsCreated);
            return View(AppliedJob.ToList());
        }

        [HttpGet]
        public ActionResult ProfilePerformance()
        {
            try
            {
                ProfilePerformance oProfilePerformance = new ProfilePerformance();
                long userID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                string updatbefore1 = BaseUtil.GetCalculatedDateTime(-180).ToString("MM/dd/yyyy");
                DateTime updatbefore = Convert.ToDateTime(updatbefore1);
                var AppliedJob = db.ProfilePerformances.Where(e => e.dataIsCreated >= updatbefore && e.qenID == userID).OrderByDescending(e => e.dataIsCreated);
                ProfilePerformanceCount oProfilePerformanceCount = new ProfilePerformanceCount();
                oProfilePerformanceCount.viewCount = AppliedJob.Where(e => e.ViewedDate != null).Count();
                oProfilePerformanceCount.DownloadCount = AppliedJob.Where(e => e.Downloaded != null).Count();
                oProfilePerformanceCount.contactCount = AppliedJob.Where(e => e.Contacted != null).Count();
                ViewData["oProfilePerformanceCount"] = oProfilePerformanceCount;
                var lastDate = BaseUtil.GetCurrentDateTime();


                //-------------calculation for monthly records--------------------------
                monthDate omonthDate;
                List<monthDate> listomonthDate = new List<monthDate>();
                for (int i = 0; i < 6; i++)
                {
                    omonthDate = new monthDate();
                    omonthDate.firstDayOfMonth = new DateTime(lastDate.Year, lastDate.Month, 1);
                    omonthDate.lastDayOfMonth = new DateTime(lastDate.Year, lastDate.Month, DateTime.DaysInMonth(lastDate.Year, lastDate.Month));
                    listomonthDate.Add(omonthDate);
                    lastDate = lastDate.AddMonths(-1);

                }
                ProfilePerformanceCount objProfilePerformanceCount;
                List<ProfilePerformanceCount> LSTProfilePerformanceCount = new List<ProfilePerformanceCount>();
                foreach (var lst in listomonthDate)
                {
                    DateTime startDate, endDate;
                    startDate = lst.firstDayOfMonth.Date;
                    endDate = lst.lastDayOfMonth.Date;
                    objProfilePerformanceCount = new ProfilePerformanceCount();
                    objProfilePerformanceCount.MonthName = startDate.ToString("MMMM");
                    objProfilePerformanceCount.viewCount = AppliedJob.Where(e => e.ViewedDate != null && e.dataIsCreated >= startDate && e.dataIsCreated <= endDate).Count();
                    objProfilePerformanceCount.DownloadCount = AppliedJob.Where(e => e.Downloaded != null && e.dataIsCreated >= startDate && e.dataIsCreated <= endDate).Count();
                    objProfilePerformanceCount.contactCount = AppliedJob.Where(e => e.Contacted != null && e.dataIsCreated >= startDate && e.dataIsCreated <= endDate).Count();
                    LSTProfilePerformanceCount.Add(objProfilePerformanceCount);
                }
                ViewData["LSTProfilePerformanceCount"] = LSTProfilePerformanceCount;
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);               
            }
            return View();
        }

        // Render Select meet page 

        public ActionResult selectmeet(FormCollection frm, string dat, string qenID, string jobID)
        {
            long qenid = 0, jid = 0;
            if (frm.Count > 0)
            {
                qenid = Convert.ToInt64(frm["qenID"].ToString());
                jid = Convert.ToInt64(frm["jobID"].ToString());
            }
            else {
                if (qenID != null)
                {
                    qenid = Convert.ToInt64(BaseUtil.Decrypt(qenID));
                }
                if (jobID != null)
                {
                    jid = Convert.ToInt64(BaseUtil.Decrypt(jobID));
                }
            }        
           

            DateTime dt = BaseUtil.GetCurrentDateTime();
            if (frm.Count > 0)
            {
                if (frm["txt_date"].ToString() != "")
                {
                    dt = Convert.ToDateTime(frm["txt_date"].ToString());
                    ViewBag.dAt = dt.ToString().Substring(0, 10);
                }
            }
            if (dat != null)
                if (dat != "")
                {
                    dt = Convert.ToDateTime(dat);
                    ViewBag.dAt = dt.ToString().Substring(0, 10);
                }
            try
            {
                var companyID_ = db.jobDetails.Where(e => e.jobID == jid).Select(e => new { e.companyID }).FirstOrDefault();
                long companyID = Convert.ToInt64(companyID_.companyID.ToString());
                string query = "select slotID, slotTime, 'Available' as blocked ,'' as 'qenID' ,''as jobID from slots where " +
                             " slotID not in (select slotID from slotsBlocked where companyID = '" + companyID + "' AND isDeleted = 0 ) AND " +
                             " slotID not in (SELECT slotID FROM qenInterviewSchedule where companyID = '" + companyID + "'  AND slotID is not null AND convert(date, meetScheduledDateTime)= convert(date, '" + dt + "') ) AND " +
                              " slotID not in (SELECT slotID FROM slotTempBlocked where companyID = '" + companyID + "'  AND forDate = convert(date, '" + dt + "') AND isDeleted = 0 )" +
                              " union " +
                              " select slotTempBlocked.slotID , slotTime,'booked by HR' as blocked, '' as 'qenID' ,''as jobID from slotTempBlocked left outer join slots on slotTempBlocked.slotID = slots.slotID where companyID = '" + companyID + "'  AND isDeleted = 0 AND forDate = convert(date, '" + dt + "') " +
                              " union " +
                              " select slotsBlocked.slotID , slotTime,'blocked by HR' as blocked, '' as 'qenID' ,''as jobID from slotsBlocked left outer join slots on slotsBlocked.slotID = slots.slotID where companyID = '" + companyID + "'  AND isDeleted = 0 " +
                              "  union " +
                              " SELECT qenInterviewSchedule.slotID, slotTime,qenName as blocked,  qendidateList.qenID as 'qenID' ,qenInterviewSchedule.jobID FROM qenInterviewSchedule left outer join qendidateList " +
                              " on qenInterviewSchedule.qenID = qendidateList.qenID left outer join slots on qenInterviewSchedule.slotID = slots.slotID where qenInterviewSchedule.companyID = '" + companyID + "'  AND qenInterviewSchedule.slotID is not null AND convert(date, meetScheduledDateTime)= convert(date, '" + dt + "')";
                var SlotList = db.Database.SqlQuery<slotsInfo>(query).ToList();
                foreach (var item in SlotList)
                {
                    item.qenID = qenid;
                    item.jobID = jid;
                }
                return View(SlotList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        // Book Meeting 
        public string bookMeet(int slotID, long qenID, string dat, long jobID)
        {
            Int64 meetID_ = 0;
            try
            {
                string returndate = "select distinct t.meetID from qenInterviewSchedule t inner join (select qenID, max(dataIsUpdated) as MaxDate from[dbo].[qenInterviewSchedule] group by qenID) tm on t.qenID ='" + qenID + "' and t.dataIsUpdated = tm.MaxDate";
                var meetid = db.Database.SqlQuery<Int64>(returndate).ToList();
                meetID_ = Convert.ToInt64(meetid[0].ToString());
                var latest = db.qenInterviewSchedules.Where(e => e.qenID == qenID && e.meetID == meetID_).FirstOrDefault();
                if (latest != null)
                {
                    latest.JobID = jobID;
                    latest.qenID = qenID;
                    latest.slotID = slotID;
                    latest.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    latest.meetMailRepliedDateTime = BaseUtil.GetCurrentDateTime();
                    latest.meetScheduledDateTime = Convert.ToDateTime(dat);
                    latest.meetPreferredMedium = "Skyp";
                    latest.meetScheduledMailRecieved = true;
                }
                var qenDet = db.qendidateLists.Where(e => e.qenID == qenID).Select(x => new { x.qenName, x.qenEmail }).FirstOrDefault();
                var jobDet = db.jobDetails.Where(j => j.jobID == jobID).Select(j => new { j.CompanyName, j.jobTitle }).FirstOrDefault();
                var slotdet = db.slots.Where(s => s.SlotID == slotID).Select(s => s.slotTime).FirstOrDefault();
                string qenName = qenDet.qenName;
                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateMeetLink.html"));
                string HTML_Body = sr.ReadToEnd();
                //----------------------------use below code to send emailer------------------------------------------------------------           
                string final_Html_Body = HTML_Body.Replace("#name", qenName).Replace("#jobTitle", jobDet.jobTitle).Replace("#companyName", jobDet.CompanyName).Replace("#date", dat).Replace("#time", slotdet).Replace("#url", latest.meetURL);
                sr.Close();
                string To = qenDet.qenEmail;
                string mail_Subject = "Online Meeting Schduled for your job application" + jobDet.CompanyName + jobDet.jobTitle + dat;

                string resultEmailSend = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                db.Entry(latest).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {              
                BaseUtil.CaptureErrorValues(ex);                
            }
            return "OK";
        }


        public void UpdateProfilePerformance(long qenid, int action_)
        {
            ProfilePerformance oProfilePerformance = new Models.ProfilePerformance();
            try
            {
                long EMPID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                DateTime dt = BaseUtil.GetCurrentDateTime().Date;
                oProfilePerformance = db.ProfilePerformances.Where(e => e.qenID == qenid && e.EmployerID == EMPID && e.dataIsCreated == dt).FirstOrDefault();
                if (oProfilePerformance != null)
                //eventName : 1 for view, 2 for contact, 3 for download
                {
                    if (action_ == 1)
                    {
                        oProfilePerformance.ViewedDate = BaseUtil.GetCurrentDateTime().Date;
                    }
                    if (action_ == 2)
                    {
                        oProfilePerformance.Contacted = BaseUtil.GetCurrentDateTime().Date;
                    }
                    if (action_ == 3)
                    {
                        oProfilePerformance.Downloaded = BaseUtil.GetCurrentDateTime().Date;
                    }
                    oProfilePerformance.dataIsCreated = BaseUtil.GetCurrentDateTime().Date;
                    db.Entry(oProfilePerformance).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    oProfilePerformance = new Models.ProfilePerformance();
                    if (action_ == 1)
                    {
                        oProfilePerformance.ViewedDate = BaseUtil.GetCurrentDateTime().Date;
                    }
                    if (action_ == 2)
                    {
                        oProfilePerformance.Contacted = BaseUtil.GetCurrentDateTime().Date;
                    }
                    if (action_ == 3)
                    {
                        oProfilePerformance.Downloaded = BaseUtil.GetCurrentDateTime().Date;
                    }

                    oProfilePerformance.dataIsCreated = BaseUtil.GetCurrentDateTime().Date;
                    oProfilePerformance.EmployerID = EMPID;
                    oProfilePerformance.qenID = qenid;
                    db.ProfilePerformances.Add(oProfilePerformance);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {                
                BaseUtil.CaptureErrorValues(ex);               
            }
        }

     
        // Select Test schedule 

        [HttpGet]
        public ActionResult selectTest(string qenid, string jobID)
        {
            
            var a = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());
            if (a != null)
            {
                ViewBag.conversionYN = "Yes";
            }
            return View();
        }

        [HttpPost]
        public ActionResult submitselectTest(FormCollection frm)
        {
            Int64 mailSendID_ = 0;
            long qenid = Convert.ToInt64(frm["qid"]);
            long jobId = Convert.ToInt64(frm["jid"]);
            var date = Convert.ToDateTime(frm["dt"]);
            try
            {
                string returndate = "select distinct TOP 1 t.mailSendID from qendidateTestSchedule t inner join (select qenID, max(dataIsUpdated) as MaxDate from[dbo].[qendidateTestSchedule] group by qenID, jobID) tm on t.qenID = '" + qenid + "' and t.jobID = '" + jobId + "'  and t.dataIsUpdated = tm.MaxDate";
                var mailSendID = db.Database.SqlQuery<Int64>(returndate).ToList();
                mailSendID_ = Convert.ToInt64(mailSendID[0].ToString());
                var latest = db.qendidateTestSchedules.Where(e => e.qenID == qenid && e.mailSendID == mailSendID_).FirstOrDefault();
                if (latest != null)
                {
                    latest.testScheduledDateTime = date;
                    latest.mailSentTestScheduled = true;
                    latest.mailReceivedscheduled = true;
                    latest.dataIsUpdated = DateTime.Now;
                }
                db.Entry(latest).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();         


            // Condition Check for assessment creation for employer
            jobDetail forassessment = new jobDetail();
            var jDetails = db.jobDetails.Where(e => e.jobID == jobId).Include(e => e.EmployerDetail).FirstOrDefault();
            if (jDetails.assessmentID == null)
            {
                StreamReader sremp = new StreamReader(Server.MapPath("/Emailer/toEmployerAssignTest.html"));
                string HTML_Body_Emp = sremp.ReadToEnd();
                string final_Html_Body_Emp = HTML_Body_Emp.Replace("#name", jDetails.jobContactPersonName).Replace("#position", jDetails.jobTitle).Replace("#date", jDetails.dataIsCreated.ToString());
                sremp.Close();
                string ToEmp = jDetails.jobContactPersonEmail;
                string mail_Subject_Emp = "Create Test for for " + jDetails.jobTitle + jDetails.dataIsCreated;
                string resultEmailSend_Emp = BaseUtil.sendEmailer(ToEmp, mail_Subject_Emp, final_Html_Body_Emp, "");
            }

            // Mailer sent to candidate with test link
            var qenDetails = db.qendidateLists.Where(e => e.qenID == qenid).FirstOrDefault();
            StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateWithTestLink.html"));
            string HTML_Body = sr.ReadToEnd();
            string final_Html_Body = HTML_Body.Replace("#name", qenDetails.qenName).Replace("#post", jDetails.jobTitle).Replace("#url", "http://onlinetest.qendidate.com/OnlineAssessment.aspx?cid=" + qenid.ToString() + "&jid=" + jobId.ToString());
            sr.Close();
            string To = qenDetails.qenEmail;
            string mail_Subject = "Test Created for you  " + jDetails.jobTitle;
            string resultEmailSend = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
            ViewBag.conversionYN = "No";
        }
         catch (Exception ex)
            {
                BaseUtil.CaptureErrorValues(ex);
            }

            return View("selectTest", new { qenid, jobId });
        }

        public string sendReferenceEmailer(string email, string name, string  qenID, string qenRefID1) {
           
            string qenName = BaseUtil.GetSessionValue(AdminInfo.FullName.ToString()).ToString();
            StreamReader sr = new StreamReader(Server.MapPath("/Emailer/referenceCheck.html"));
            string HTML_Body = sr.ReadToEnd(); 
            //----------------------------use below code to send emailer------------------------------------------------------------           
            string final_Html_Body = HTML_Body.Replace("#name", name).Replace("#refID",BaseUtil.encrypt( qenRefID1));
            sr.Close();
            string To = email;
            string mail_Subject = "Reference for Mr/s" + qenName;

            string resultEmailSend = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
            //----------------------------end to send emailer------------------------------------------------------------

           
            return resultEmailSend;
        }
        [HttpGet]
        public ActionResult ReferenceCheck()
        {
            referenceCheck oreferenceCheck = new referenceCheck();
             long refid = 0; string name = "";
            if (!string.IsNullOrEmpty(Request.QueryString["refID"]))
            {
                refid = Convert.ToInt64(BaseUtil.Decrypt(Request.QueryString["refID"].ToString()));
                oreferenceCheck.qenRefID = refid;
            }
           
            if (!string.IsNullOrEmpty(Request.QueryString["name"]))
            {
                name =Request.QueryString["name"];
                oreferenceCheck.name = name;
            }
            oreferenceCheck.isCheckComplete = false;
            return View(oreferenceCheck);
        }
        [HttpPost]
        public ActionResult ReferenceCheck(referenceCheck oreferenceChec)
        {
            try
            {
                var a = db.qenReferences.Where(e => e.qenRefID == oreferenceChec.qenRefID).FirstOrDefault();
                a.isCheckComplete = oreferenceChec.isCheckComplete;
                a.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.result = "ok";
                return View(oreferenceChec);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                BaseUtil.CaptureErrorValues(ex);
                return RedirectToAction("Error");
            }
        }

        // Resend Activation Email
        public string resendActivationEmail()
        {
            var email = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
            var result = db.qendidateLists.Where(e => e.qenEmail == email ).Select(e => e.password).FirstOrDefault();
            StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateRegistrationSuccess_withActivationLink.html"));
            string HTML_Body = sr.ReadToEnd();
            string newString = HTML_Body.Replace("#name", AdminInfo.FullName.ToString()).Replace("#qenid", AdminInfo.UserID.ToString()).Replace("#password", result);
            sr.Close();
            string To = email;
            string mail_Subject = "Candidate Registration Confirmation ";
            profileController objprofileController = new profileController();
            BaseUtil.sendEmailer(To, mail_Subject, newString, "");
            return "Sent";
        }

        // Verify OTP
        public string verifyOTP(string OTP)
        {
            string email = BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
            var result = db.qendidateLists.Where(e => e.qenEmail == email).FirstOrDefault();
            long otp_ = Convert.ToInt64(OTP);
            if (result.OTP == otp_)
            {
                result.isActive = true;
                result.isMobileVerified = true;
                result.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                BaseUtil.SetSessionValue(AdminInfo.mobileVerified.ToString(),Convert.ToString(result.isMobileVerified));
                return "Verified";
            }
            else
            {
                return "NotVerified";
            }
                
        }

        public int getProfileCompletePercentage(string qenID)
        {
            var qenID_ = new SqlParameter("@qenID", qenID);
            var result = db.Database.SqlQuery<usp_GetProfileCompletenessPerc_Result>("usp_GetProfileCompletenessPerc @qenID", qenID_).FirstOrDefault();
            return result.perc;
        }      
    }
}		  