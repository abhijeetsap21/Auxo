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
    public class EducationTypesController : BaseClass
    {
        private IUnitOfWork uow = null;
        private EducationTypes repo = null;

        public EducationTypesController()
        {
            uow = new UnitOfWork();
            repo = new EducationTypes(uow);
        }

        // GET: Admin/EducationTypes
        public ActionResult Index()
        {
            var educatoinTypes = (dynamic)null;
            try
            {
                educatoinTypes = repo.SQLQuery<usp_GetAllEducationTypes_Result>("usp_GetAllEducationTypes").ToList();
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(educatoinTypes);
        }      

        // GET: Admin/EducationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/EducationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "educationTypeID,educationTypeName")] educationType educationType)
        {
            try
            {
                educationType.createdBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                educationType.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                educationType.dataIsCreated = BaseUtil.GetCurrentDateTime();
                educationType.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                educationType.isActive = true;
                if (ModelState.IsValid)
                {
                    repo.Insert(educationType);
                    return RedirectToAction("Index");
                }
            }

            catch(Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(educationType);
        }

        // GET: Admin/EducationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var educationType = repo.Single(id);
            if (educationType == null)
            {
                return HttpNotFound();
            }
            return View(educationType);
        }

        // POST: Admin/EducationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "educationTypeID,educationTypeName")] educationType educationType)
        {
            try
            {
                var educationDetails = repo.Single(educationType.educationTypeID);
                if (educationDetails == null)
                {
                    return HttpNotFound();
                }
                
                educationDetails.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                educationDetails.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                educationDetails.educationTypeName = educationType.educationTypeName;
                educationDetails.isActive = educationType.isActive;
                if (ModelState.IsValid)
                {
                    repo.Update(educationDetails);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
                return View(educationType);
        }      
        
    }
}
