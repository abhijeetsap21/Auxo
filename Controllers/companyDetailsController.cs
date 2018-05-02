using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;
using System.Data.Entity.Validation;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace NewLetter.Controllers

{
    [CustomErrorHandling]
    public class companyDetailsController : BaseClass
    {


        private oriondbEntities db = new oriondbEntities();
        private string r;



        // GET: companyDetails
        public ActionResult Index()
        {
            var companyDetails = db.companyDetails.Include(c => c.city).Include(c => c.EmployerDetails).Include(c => c.employerType);
            return View(companyDetails.ToList());

        }

        // GET: companyDetails/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            companyDetail companyDetail = db.companyDetails.Find(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }




        // View Candidates 

        public async Task<ActionResult> candidates(string qenName, string qenPhone, string qenEmail, string skills)
        {


            var jobDetails = db.qendidateLists.Include(e => e.qenSkills);
            string queryParameter = string.Empty;
            return View(await jobDetails.ToListAsync());
        }
        //View Candidate Resume 

        public ActionResult CandidateView(int qenid)
        {
            ResumeModel model = new ResumeModel();
            qendidateList personal = db.qendidateLists.Where(ex => ex.qenID == qenid).FirstOrDefault();
            qenSecondary s = db.qenSecondaries.Where(ex => ex.qenID == qenid).FirstOrDefault();
            qenHigherSecondary hs = db.qenHigherSecondaries.Where(ex => ex.qenID == qenid).FirstOrDefault();
            qendidateGraduation g = db.qendidateGraduations.Where(ex => ex.qenID == qenid).FirstOrDefault();
            qendidatePGraduation pg = db.qendidatePGraduations.Where(ex => ex.qenID == qenid).FirstOrDefault();
            List<qenEmpDetail> emp = db.qenEmpDetails.Where(ex => ex.qenID == qenid).ToList();
            List<qendidatePHD> phd = db.qendidatePHDs.Where(ex => ex.qenID == qenid).ToList();
            List<qenReference> refrences = db.qenReferences.Where(ex => ex.qenID == qenid).ToList();
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
            return View(model);
        }

        // GET: companyDetails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            companyDetail companyDetail = db.companyDetails.Find(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.modifiedBy = new SelectList(db.EmployerDetails, "EmployerID", "Name", companyDetail.modifiedBy);
            ViewBag.employerTypeID = new SelectList(db.employerTypes, "employerTypeID", "employerType1", companyDetail.employerTypeID);
            return View(companyDetail);
        }

        // POST: companyDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "companyID,companyName,cityID,address,website,companyIndustry,companyDescription,gstNo,tinNo,ctsNo,logo,createdDate,isActive,isDeleted,modifiedBy,modifiedDate,employerTypeID")] companyDetail companyDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.cityID = new SelectList(db.cities, "cityID", "city1", companyDetail.cityID);
            ViewBag.modifiedBy = new SelectList(db.EmployerDetails, "EmployerID", "Name", companyDetail.modifiedBy);
            ViewBag.employerTypeID = new SelectList(db.employerTypes, "employerTypeID", "employerType1", companyDetail.employerTypeID);
            return View(companyDetail);
        }

        // GET: companyDetails/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            companyDetail companyDetail = db.companyDetails.Find(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }

        // POST: companyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            companyDetail companyDetail = db.companyDetails.Find(id);
            db.companyDetails.Remove(companyDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult companyMain()
        {
            return View();

        }
        //Edit Employer full main page 

        public ActionResult empedit()
        {
            return View();
        }
        // Render Edit company page 

        [HttpGet]
        public ActionResult _partialEditCompany()
        {
            int companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            companyDetail companyDetail = new companyDetail();
            companyDetail = db.companyDetails.Where(ex => ex.companyID == companyID).FirstOrDefault();
            ViewBag.employerTypeID = new SelectList(db.employerTypes, "employerTypeID", "employerType1", companyDetail.employerTypeID);
            ViewBag.companyIndustry = new SelectList(db.industries, "industryID", "industryName");
            return PartialView("_partialEditCompany", companyDetail);
        }


        // Edit Company
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult savecompanyDetails(companyDetail companyDetail, HttpPostedFileBase files)
        {

            int companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            companyDetail cmpdetail = new companyDetail();
            cmpdetail = db.companyDetails.Where(ex => ex.companyID == companyDetail.companyID).FirstOrDefault();
            cmpdetail.companyName = companyDetail.companyName;
            String fileName = "";
            if (files != null)
            {
                fileName = Guid.NewGuid() + "_" + Path.GetFileName(files.FileName);
                var path = Path.Combine(Server.MapPath("~/Logo/"), fileName);
                files.SaveAs(path);
                cmpdetail.logo = "http://newsletter.qendidate.com/Logo/" + fileName;
            }

            cmpdetail.city = companyDetail.city;
            cmpdetail.address = companyDetail.address;
            cmpdetail.website = companyDetail.website;
            cmpdetail.companyDescription = companyDetail.companyDescription;
            cmpdetail.gstNo = companyDetail.gstNo;
            cmpdetail.tinNo = companyDetail.tinNo;
            cmpdetail.ctsNo = companyDetail.ctsNo;
            cmpdetail.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            cmpdetail.employerTypeID = companyDetail.employerTypeID;
            cmpdetail.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            cmpdetail.streetNo = companyDetail.streetNo;
            cmpdetail.state = companyDetail.state;
            cmpdetail.country = companyDetail.country;
            cmpdetail.zipCode = companyDetail.zipCode;
            cmpdetail.latitude = companyDetail.latitude;
            cmpdetail.companyIndustry = companyDetail.companyIndustry;
            cmpdetail.longitude = companyDetail.longitude;

            if (ModelState.IsValid)
            {
                db.Entry(cmpdetail).State = EntityState.Modified;
                db.SaveChanges();
            }

            ViewBag.employerTypeID = new SelectList(db.employerTypes, "employerTypeID", "employerType1", companyDetail.employerTypeID);
            ViewBag.companyIndustry = new SelectList(db.industries, "industryID", "industryName");

            TempData["saveResult"] = "Success";
            return RedirectToAction("empedit");
        }

        [HttpGet]
        public ActionResult _partialEditEmployer()
        {
            int employerID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            EmployerDetail employerDetail = new EmployerDetail();
            employerDetail = db.EmployerDetails.Where(ex => ex.EmployerID == employerID).FirstOrDefault();
            return PartialView("_partialEditEmployer", employerDetail);
        }


        //Edit Employer
        [HttpPost]
        public ActionResult editEmployer(EmployerDetail employerDetail)
        {
            int companyID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            int employerID = Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            EmployerDetail empdetail = new EmployerDetail();
            empdetail = db.EmployerDetails.Where(ex => ex.EmployerID == employerDetail.EmployerID).FirstOrDefault();
            empdetail.Name = employerDetail.Name;
            empdetail.Mobile = employerDetail.Mobile;
            empdetail.Email = employerDetail.Email;
            empdetail.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            db.Entry(empdetail).State = EntityState.Modified;
            db.SaveChanges();

            TempData["saveResult"] = "empSuccess";
            return RedirectToAction("empedit");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: View Candidates

        [HttpGet]
        public ActionResult cvShortlisted(long? jobID)

        {
            ViewBag.jobID = jobID;
            return View();
        }

        //Load Partial Result 1st time
        public ActionResult _partialCandidateResultList(int jobid)
        {
            oriondbEntities db = new oriondbEntities();
            DateTime updatbefore = BaseUtil.GetCalculatedDateTime(-365);
            string spExecute = "sp_candidateSearch @defaultSearch =1 , @modifiedDate='" + updatbefore + "',@PageNumber = 1, @jobID ='" + jobid + "'";
            var result = db.Database.SqlQuery<sp_candidateSearch_result>(spExecute).ToList();
            sp_candidateSearch_result sp = new sp_candidateSearch_result();
            if (result != null)
            {
                sp.PageCount = result[0].PageCount;
            }
            else {
                sp.PageCount = 1;
            }
            ViewBag.defaultSearch = 1;
            sp.CurrentPageIndex = 1;
            ViewBag.totalpages = sp.PageCount;
            return PartialView("_partialCandidateResult", result);
        }

        public string loadMore(string jobtitle, string city, string txtskill, int ddl_update, Int64? jobid, int currentPageIndex ,int defaultsearch)
        {
           DateTime updatbefore;
            string query = "sp_candidateSearch @PageNumber = '" + currentPageIndex + "',";
          
            
            
                query = query + " @defaultSearch ="+ defaultsearch + ", ";
              
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
                    query = query + "  @modifiedDate='" + updatbefore + "'";
                    if (txtskill != "")
                {
                    query = query + " ,@skillsSet ='" + txtskill + "' ";
                }
                
                if (city != "")
                {
                    query = query + ",@city ='" + city + "' ";
                }

                if (jobtitle != "")
                {
                    query = query + ",@title ='" + jobtitle + "' ";
                }
            
           
         

            long jid = 0;
            if (jobid != null)
            {
                jid = (long)jobid;
            }
           
            query = query + " ,@jobID ='" + jid + "' ";

            var qendidatesearchlist = db.Database.SqlQuery<sp_candidateSearch_result>(query);

            ViewBag.jobID = jobid;
            string htmlString = "";
            int index = (currentPageIndex-1) * 1;
            foreach (var item in qendidatesearchlist)
            {
                index = index + 1;
                 htmlString = htmlString + "<tr> <td><p >"+index+ "</p> <input type = 'hidden' id = 'qen_"+index+"' value = '"+item.qenID+"'></td>";


                if (item.SelfApplied == true)
                {
                    if (item.qenLinkdInUrl != null)
                    {
                        htmlString = htmlString + "<td> <a href='" + item.qenLinkdInUrl + "'>" + item.qenName + " <span id='ast'>&#42;</span><div id='tooltip'>These candidates applied through Auxo </div></a></td>";

                    }

                    else
                    {
                        htmlString = htmlString + "<td>" + item.qenName + "<span id='ast'>&#42;</span><div id='tooltip'>These candidates applied through Auxo </div></td>";

                    }
                }
                else
                {
                    htmlString = htmlString + "<td>" + item.qenName + "</td>";

                }


                htmlString = htmlString + "<td> " + @item.skillset + "</ td >";
                htmlString = htmlString + " <td><input id='item_qencategory_" + index + "' name='"+item.category+"' type='hidden' value='"+item.category+"'>  <input type = 'button' class='btn btn-info btn-lg' data-toggle='modal' data-target='#myModal' value='View' onclick='return dispalyResume(" + @item.qenID + ");'> </td>";
                if (item.category == 0)
                {

                    htmlString = htmlString + " <td><input type = 'radio' name = 'Category_" + item.qenID + "' onclick = 'return categoriesCandidate1(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "' onclick = 'return categoriesCandidate2(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "' onclick = 'return categoriesCandidate3(" + index + ",item_qencategory_" + index + ");'></td>";
                }
                else if (item.category == 1)
                {
                    htmlString = htmlString + " <td><input type = 'radio' name = 'Category_" + item.qenID + "'checked onclick = 'return categoriesCandidate1(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "' onclick = 'return categoriesCandidate2(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "' onclick = 'return categoriesCandidate3(" + index + ",item_qencategory_" + index + ");'></td>";

                }
                else if (item.category == 2)
                {
                    htmlString = htmlString + " <td><input type = 'radio' name = 'Category_" + item.qenID + "'checked onclick = 'return categoriesCandidate1(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "'checked onclick = 'return categoriesCandidate2(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "' onclick = 'return categoriesCandidate3(" + index + ",item_qencategory_" + index + ");'></td>";


                }
                  else
                    {
                    htmlString = htmlString + " <td><input type = 'radio' name = 'Category_" + item.qenID + "'checked onclick = 'return categoriesCandidate1(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "'checked onclick = 'return categoriesCandidate2(" + index + ",item_qencategory_" + index + ");'></td>";

                    htmlString = htmlString + "<td><input type = 'radio' name = 'Category_" + item.qenID + "'checked onclick = 'return categoriesCandidate3(" + index + ",item_qencategory_" + index + ");'></td>";


                }


                        htmlString = htmlString + "</tr>";

                    }
                    var htmlString1 = HttpUtility.HtmlEncode(htmlString);
            if (htmlString == "")
            {
                htmlString = "<tr> <td colspan='9'> No more records found.</td></tr>";
            }
            return htmlString;
                }



        public ActionResult candidateSearch(FormCollection frm, Int64? jobid)
        {
            string city = string.Empty; string JobTitle = ""; string modifiedDate = "1"; DateTime updatbefore;
            string query = "sp_candidateSearch";

            if (frm.Keys.Count > 0)
            {
                query = query + " @defaultSearch =0 ,@PageNumber = 1";
                ViewBag.defaultSearch = 0;
                modifiedDate = frm["frm[ddl_update]"].ToString();
                if (modifiedDate == "2")
                {
                    updatbefore = BaseUtil.GetCalculatedDateTime(-7);

                }

                else if (modifiedDate == "3")
                {
                    updatbefore = BaseUtil.GetCalculatedDateTime(-90);

                }
                else if (modifiedDate == "4")
                {
                    updatbefore = BaseUtil.GetCalculatedDateTime(-180);

                }
                else if (modifiedDate == "5")
                {
                    updatbefore = BaseUtil.GetCalculatedDateTime(-730);

                }
                else
                {
                    updatbefore = BaseUtil.GetCalculatedDateTime(-30);

                }
                string skills = frm["frm[txtskill]"].ToString();
                if (skills != "")
                {
                    query = query + " ,@skillsSet ='" + skills + "' ";
                }
                city = frm["frm[city]"].ToString();
                if (city != "")
                {
                    query = query + ",@city ='" + city + "' ";
                }

                JobTitle = frm["frm[txt_JobTitle]"].ToString();

                if (JobTitle != "")
                {
                    query = query + ",@title ='" + JobTitle + "' ";
                }
            }
            else
            {
                ViewBag.defaultSearch = 1;
                query = query + ", @defaultSearch =1 ";
                updatbefore = BaseUtil.GetCalculatedDateTime(-365);
            }
            query = query + " , @modifiedDate='" + updatbefore + "'";

            long jid = 0;
            if (jobid != null)
            {
                jid = (long)jobid;
            }
            else
            {
                if (frm.Keys.Count > 0)
                {
                    jid = Convert.ToInt64((frm["jobid"].ToString()));
                }
            }
            query = query + " ,@jobID ='" + jid + "' ";

            var qendidatesearchlist = db.Database.SqlQuery<sp_candidateSearch_result>(query);

            ViewBag.jobID = jobid;
            return PartialView("_partialCandidateResult", qendidatesearchlist);
        }




        public int checkValuExist(string skill_)
        {

            var result = db.skills.Where(e => e.skillName == skill_).Select(x => new { x.skillsID }).SingleOrDefault();
            if (result == null)
            {
                skill oskill = new skill();
                oskill.skillName = skill_;
                db.skills.Add(oskill);
                db.SaveChanges();
                return oskill.skillsID;
            }
            else
            {
                return result.skillsID;
            }
        }

        //Saving TO orion Comm

        public string saveToOrionComm(int[][] searchmodel, string jobid)
        {
            //long jobid = Convert.ToInt64((searchmodel["jobid"].ToString()));
            string result = "no";
            long jid = Convert.ToInt32(jobid);
            int rowcount = 0;

            if (searchmodel != null)
            { rowcount = searchmodel.Count(); }
            qendidateListInJob newjoblist = new qendidateListInJob();
            for (long i = 0; i < rowcount; i++)
            {
                long qenid =Convert.ToInt16( searchmodel[i][0]);
                int catid = Convert.ToInt16(searchmodel[i][1]);
                if (catid != 0)
                {
                    var qenExists = db.qendidateListInJobs.Where(e => e.qenID == qenid && e.jobID == jid).FirstOrDefault();
               
                    if (qenExists == null)
                    {
                        newjoblist.qenID = qenid;
                        newjoblist.category = catid;
                        newjoblist.jobID = jid;
                        db.qendidateListInJobs.Add(newjoblist);
                        db.SaveChanges();
                    }
                    else
                    {
                        qenExists.category = catid;
                        db.Entry(qenExists).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                result = jid.ToString();
            }

           
            return result;
        }
    }
}
