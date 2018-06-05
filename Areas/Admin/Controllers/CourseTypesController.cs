using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NewLetter.Models;
using NewLetter.Areas.Admin.Models;
using System;

namespace NewLetter.Areas.Admin.Controllers
{
    public class CourseTypesController : BaseClass
    {
        private IUnitOfWork uow = null;
        private CourseTypes repo = null;
        oriondbEntities db = new oriondbEntities();
        public CourseTypesController()
        {
            uow = new UnitOfWork();
            repo = new CourseTypes(uow);
        }

        // GET: Admin/CourseTypes
        public ActionResult Index()
        {
            ViewBag.educationTypes = repo.SQLQuery<usp_GetAllEducationTypes_Admin_Result>("usp_GetAllEducationTypes_Admin").ToList();            
            return View();
        }

        [Route("", Name = "Admin")]
        public ActionResult getCourseTypesByEducationType(int currentPageIndex,int educationTypeID)
        {
            int maxRows = 10;
            string usp_execute = "usp_GetcourseTypesByeducationTypeIDandPageIndex @educationTypeID =" + educationTypeID;
            var result = repo.SQLQuery<usp_GetcourseTypesByeducationTypeIDandPageIndex_Result>(usp_execute).OrderBy(e=>e.courseName).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            double pageCount = (double)((decimal)db.courseTypes.Count() / Convert.ToDecimal(maxRows));
            ViewBag.PageCount = (int)Math.Ceiling(pageCount);
            ViewBag.CurrentPageIndex = currentPageIndex;
            return PartialView("_partialCourseTypeList", result);
        }        

        // GET: Admin/CourseTypes/Create
        public ActionResult Create()
        {
            ViewBag.educationTypes = repo.SQLQuery<usp_GetAllEducationTypes_Admin_Result>("usp_GetAllEducationTypes_Admin").Select(e=>new { e.educationTypeID,e.educationTypeName}).ToList();            
            return View();
        }

        // POST: Admin/CourseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(courseType courseType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    courseType.createdBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                    courseType.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                    courseType.dataIscCreated = BaseUtil.GetCurrentDateTime();
                    courseType.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    repo.Insert(courseType);
                    return RedirectToAction("Index");
                }                
            }

            catch(Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/CourseTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            courseType courseType = repo.Single(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            ViewBag.educationTypes = repo.SQLQuery<usp_GetAllEducationTypes_Admin_Result>("usp_GetAllEducationTypes_Admin").Select(e => new { e.educationTypeID, e.educationTypeName }).ToList();            
            return View(courseType);
        }

        // POST: Admin/CourseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(courseType courseType)
        {
            try
            {
                var courseDetails = repo.Single(courseType.courseTypeID);
                if (courseDetails == null)
                {
                    return HttpNotFound();
                }

                
                courseDetails.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                courseDetails.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
                courseDetails.educationTypeID = courseType.educationTypeID;
                courseDetails.courseName = courseType.courseName;
                courseDetails.isActive = true;
                if (ModelState.IsValid)
                {
                    repo.Update(courseDetails);
                    return RedirectToAction("Index");
                }            
                
            }
            catch(Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
            }
            return View(courseType);
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
