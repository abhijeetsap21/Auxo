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
    public class IndustryController : BaseClass
    {
        private oriondbEntities db = new oriondbEntities();

        private IUnitOfWork uow = null;
        private Industry repo = null;
        public IndustryController()
        {
            uow = new UnitOfWork();
            repo = new Industry(uow);
        }

        // GET: Admin/Industry
        public ActionResult Index()
        {
            var industries = repo.SQLQuery<sp_IndustryList_Result>("sp_IndustryList").ToList();
            return View(industries.ToList());
        }

        
        // GET: Admin/Industry/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Admin/Industry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "industryName,isActive")] industry industry)
        {
            if (ModelState.IsValid)
            {
                industry.createdBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                industry.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                industry.isActive = industry.isActive;
                industry.dataIsCreated = BaseUtil.GetCurrentDateTime();
                industry.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                repo.Insert(industry);
                return RedirectToAction("Index");
            }            
            return View(industry);
        }

        // GET: Admin/Industry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var industry = repo.Single(id);
            if (industry == null)
            {
                return HttpNotFound();
            }
            
            return View(industry);
        }

        // POST: Admin/Industry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "industryID,industryName,isActive")] industry industry)
        {
            var i = repo.Single(industry.industryID);
            if (i == null)
            {
                return HttpNotFound();
            }
            i.industryName = industry.industryName;
            i.isActive = industry.isActive;
            i.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            i.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));

            if (ModelState.IsValid)
            {
                repo.Update(i);
                return RedirectToAction("Index");
            }
            
            return View(industry);
        }

        
        
    }
}
