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
using NewLetter.Areas.Admin.Models;

namespace NewLetter.Areas.Admin.Controllers
{
    public class JobStatusController : BaseClass
    {
        private oriondbEntities db = new oriondbEntities();

        private IUnitOfWork uow = null;
        private JobStatu repo = null;

        public JobStatusController()
        {
            uow = new UnitOfWork();
            repo = new JobStatu(uow);
        }

        // GET: Admin/JobStatus
        public ActionResult Index()
        {
            var jobs = repo.SQLQuery<sp_jobStatus_List_Result>("sp_jobStatus_List").ToList();
            return View(jobs.ToList());
        }


        // GET: Admin/JobStatus/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: Admin/JobStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "statuts,isActive")] jobStatu jobStatu)
        {
            if (ModelState.IsValid)
            {
                jobStatu.dataIsCreated = BaseUtil.GetCurrentDateTime();
                jobStatu.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                jobStatu.isActive = jobStatu.isActive;
                jobStatu.createdBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                jobStatu.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));

                repo.Insert(jobStatu);
                return RedirectToAction("Index");
            }
                        
            return View(jobStatu);
        }

        // GET: Admin/JobStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           

            var jobStatu = repo.Single(id);
            if (jobStatu == null)
            {
                return HttpNotFound();
            }            
            return View(jobStatu);
        }

        // POST: Admin/JobStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "statuts,isActive")] jobStatu jobStatu)
        {
            var j = repo.Single(jobStatu.jobStatusID);
            if (j == null)
            {
                return HttpNotFound();
            }
            j.statuts = jobStatu.statuts;
            j.isActive = jobStatu.isActive;
            j.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            j.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            if (ModelState.IsValid)
            {
                repo.Update(j);
                return RedirectToAction("Index");
            }
            
            return View(jobStatu);
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
