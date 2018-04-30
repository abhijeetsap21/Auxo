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

namespace NewLetter.Areas.Admin.Controllers
{
    public class EmploymentTypesController : BaseClass
    {
        private IUnitOfWork uow = null;
        private employmentType_ repo = null;
        public EmploymentTypesController()
        {
            uow = new UnitOfWork();
            repo = new employmentType_(uow);
        }

        // GET: Admin/EmploymentTypes
        public ActionResult Index()
        {
            var employmentTypes = repo.GetAll().ToList();
            return View(employmentTypes);
        }

        // GET: Admin/EmploymentTypes/Details/5
     

        // GET: Admin/EmploymentTypes/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: Admin/EmploymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploymentTypeID,EmploymentType1,dataIsCreated,dataIsUpdated,isActive,isSelected,createdBy,modifiedBy")] NewLetter.Models.EmploymentType employmentType)
        {
            employmentType.createdBy= Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
            employmentType.modifiedBy= Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
            employmentType.dataIsCreated= BaseUtil.GetCurrentDateTime();
            employmentType.dataIsUpdated= BaseUtil.GetCurrentDateTime();
            if (ModelState.IsValid)
            {
                repo.Insert(employmentType);
                return RedirectToAction("Index");
            }
           
            return View(employmentType);
        }

        // GET: Admin/EmploymentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          EmploymentType employmentType = repo.Single(id);
            if (employmentType == null)
            {
                return HttpNotFound();
            }           
            return View(employmentType);
        }

        // POST: Admin/EmploymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmploymentTypeID,EmploymentType1,dataIsCreated,dataIsUpdated,isActive,isSelected,createdBy,modifiedBy")] NewLetter.Models.EmploymentType employmentType)
        {
            var s = repo.Single(employmentType.EmploymentTypeID);
            if (s == null)
            {
                return HttpNotFound();
            }
            s.EmploymentType1 = employmentType.EmploymentType1;
            s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            s.isActive = employmentType.isActive;
            s.isSelected = employmentType.isSelected;
            s.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            if (ModelState.IsValid)
            {
                repo.Update(s);
                return RedirectToAction("Index");
            }
           
            return View(employmentType);
        }
        
       
    }
}
