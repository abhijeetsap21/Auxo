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
    [CustomErrorHandling]
    public class CandidateStatusController : BaseClass
    {

        private IUnitOfWork uow = null;
        private CandidateStatus repo = null;
        public CandidateStatusController()
        {

            uow = new UnitOfWork();
            repo = new CandidateStatus(uow);
        }

        // GET: Admin/CandidateStatus
        public ActionResult Index()
        {
            var status = (dynamic)null;
            try
            {
                 status = repo.SQLQuery<sp_candidateStatusTypeList_Result>("sp_candidateStatusTypeList").ToList();                
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(status);
        }   

        // GET: Admin/CandidateStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CandidateStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Candidate_status1,status")] Candidate_status candidate_status)
        {
            try
            {
                candidate_status.createdBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                candidate_status.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                candidate_status.dataIsCreated = BaseUtil.GetCurrentDateTime();
                candidate_status.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                candidate_status.isActive = true;
                if (ModelState.IsValid)
                {
                    repo.Insert(candidate_status);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(candidate_status);
        }

        // GET: Admin/CandidateStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate_status = repo.Single(id);

            if (candidate_status == null)
            {
                return HttpNotFound();
            }
            return View(candidate_status);
        }

        // POST: Admin/CandidateStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Candidate_status1,status")] Candidate_status candidate_status)
        {
           

            var s = repo.Single(candidate_status.Candidate_status1);
            if (s == null)
            {
                return HttpNotFound();
            }
            s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            s.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            
            if (ModelState.IsValid)
            {
                repo.Update(s);
                return RedirectToAction("Index");
            }
            return View(candidate_status);
        }

        
    }
}
