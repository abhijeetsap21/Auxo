using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;
using NewLetter.Areas.Admin.Models;
using System.Diagnostics;
using System.Data.SqlClient;

namespace NewLetter.Areas.Admin.Controllers
{
    [CustomErrorHandling]
    public class EducationController : BaseClass
    {
        private IUnitOfWork uow = null;
        private Education_ repo = null;

        public EducationController()
        {          
             uow = new UnitOfWork();
            repo = new Education_(uow);           
        }
        
        // GET: Admin/Education
        public ActionResult Index()
        {           
            var educations= repo.SQLQuery<sp_selectEducationTypeList_Result>("sp_selectEducationTypeList").ToList();
            //db.Educations.Include(e => e.EmployerDetail).Include(e => e.EmployerDetail1);
            return View(educations);
        }

      

        // GET: Admin/Education/Create
        public ActionResult Create()
        {          
            return View();
        }

        // POST: Admin/Education/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewLetter.Models.Education education)
        {
            education.createdBy =Convert.ToInt64( BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            education.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            education.dataIsCreated = BaseUtil.GetCurrentDateTime();
            education.dataIsUpdated= BaseUtil.GetCurrentDateTime();
           
            if (ModelState.IsValid)
            {
                repo.Insert(education);
                return RedirectToAction("Index");
            }
            
            return View(education);
        }

        // GET: Admin/Education/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = repo.Single(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            
            return View(education);
        }

        // POST: Admin/Education/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewLetter.Models.Education education)
        {
            var s = repo.Single(education.EducationID);
            if (s == null)
            {
                return HttpNotFound();
            }
            s.educationName = education.educationName;
            s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            s.isActive = education.isActive;
            s.isSelected = education.isSelected;
            s.modifiedBy= Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            
            if (ModelState.IsValid)
            {
                repo.Update(s);
                return RedirectToAction("Index");
            }
            
            return View(education);
        }

      
       
    }
}
