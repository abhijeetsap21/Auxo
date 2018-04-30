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
using System.Data.SqlClient;

namespace NewLetter.Areas.Admin.Controllers
{
    public class SkillsController : BaseClass
    {
        private IUnitOfWork uow = null;
        private Skills repo = null;
        public SkillsController()
        {
            uow = new UnitOfWork();
            repo = new Skills(uow);
        }

        // GET: Admin/Skills
        public ActionResult Index()
        {
            
            return View();
        }

        [Route("", Name = "Admin")]
        public ActionResult _partialSkillsResultList()
        {
            oriondbEntities db = new oriondbEntities();
            string spExecute = "sp_skillsList @PageNumber = 1 ,@filterbyCreatedDateORmodifiedDate = true";
            var result = db.Database.SqlQuery<sp_skillsList_Result>(spExecute).ToList();
            sp_skillsList_Result sp = new sp_skillsList_Result();
            double pageCount = Convert.ToDouble(result[1].total / 2);
            sp.PageCount = (int)Math.Ceiling(pageCount);
            sp.CurrentPageIndex = 1;
            ViewBag.count = pageCount;
            ViewBag.data = "Yes";
            ViewBag.currindex = sp.CurrentPageIndex;
            return PartialView(result);
        }

        // Search Result
        [Route("", Name = "Admin")]
        public ActionResult skillsResultList(int currentPageIndex, FormCollection frm)
        {
            oriondbEntities db = new oriondbEntities();
            string spExecute = "sp_skillsList @PageNumber =" + currentPageIndex + ",";
            if (frm["frm[radio0]"] != null)
            {
                spExecute += "@filterbyCreatedDateORmodifiedDate =" + frm["frm[radio0]"] + ",";
            }
            else
            {
                spExecute += "@filterbyCreatedDateORmodifiedDate = 1 ,";
            }
            if (frm["frm[name]"] != "")
            {
                spExecute += "@skillName = '" + frm["frm[name]"] + "',";
            }
            else
            {
                spExecute += "@skillName = null , ";
            }

            if (frm["frm[isActive]"] != null)
            {
                spExecute += "@isActive = " + frm["frm[isActive]"] + "";
            }
            else
            {
                spExecute += "@isActive = 1 ";
            }
            var result = db.Database.SqlQuery<sp_skillsList_Result>(spExecute).ToList();
            sp_skillsList_Result sp = new sp_skillsList_Result();
            double pageCount;
            if (result.Count == 1)
            {
                pageCount = Convert.ToDouble(result.Count);
                sp.PageCount = (int)Math.Ceiling(pageCount);
                sp.PageCount = Convert.ToInt32(pageCount);
                sp.CurrentPageIndex = currentPageIndex;
                ViewBag.count = pageCount;
                ViewBag.data = "Yes";
                ViewBag.currindex = currentPageIndex;
            }
            else if (result.Count > 1)
            {
                pageCount = Convert.ToDouble((result.Count) / 2);
                sp.PageCount = (int)Math.Ceiling(pageCount);
                sp.PageCount = Convert.ToInt32(pageCount);
                sp.CurrentPageIndex = currentPageIndex;
                ViewBag.count = pageCount;
                ViewBag.data = "Yes";
                ViewBag.currindex = currentPageIndex;
            }
            else
            {
                ViewBag.data = "Nodata";
            }


            return PartialView("_partialSkillsResultList",result);
        }

        //[HttpPost]
        //public ActionResult Index(int currentPageIndex, FormCollection frm)
        //{
        //    oriondbEntities db = new oriondbEntities();
        //    string spExecute = "sp_skillsList @PageNumber =" + currentPageIndex + ",";
        //    if (frm["radio0"] != null)
        //    {
        //        spExecute += "@filterbyCreatedDateORmodifiedDate =" + frm["radio0"] + ",";
        //    }
        //    else
        //    {
        //        spExecute += "@filterbyCreatedDateORmodifiedDate = 1 ,";
        //    }
        //    if (frm["name"] != "")
        //    {
        //        spExecute += "@skillName = '" + frm["name"] + "',";
        //    }
        //    else
        //    {
        //        spExecute += "@skillName = null , ";
        //    }

        //    if (frm["isActive"] != null)
        //    {
        //        spExecute += "@isActive = " + frm["isActive"] + "";
        //    }
        //    else
        //    {
        //        spExecute += "@isActive = 1 ";
        //    }
        //    var result = db.Database.SqlQuery<sp_skillsList_Result>(spExecute).ToList();
        //    sp_skillsList_Result sp = new sp_skillsList_Result();
        //    double pageCount;
        //    if (result.Count == 1)
        //    {
        //        pageCount = Convert.ToDouble(result.Count);
        //        sp.PageCount = (int)Math.Ceiling(pageCount);
        //        sp.PageCount = Convert.ToInt32(pageCount);
        //        sp.CurrentPageIndex = currentPageIndex;
        //        ViewBag.count = pageCount;
        //        ViewBag.data = "Yes";
        //        ViewBag.currindex = currentPageIndex;
        //    }
        //    else if (result.Count > 1)
        //    {
        //        pageCount = Convert.ToDouble((result.Count) / 2);
        //        sp.PageCount = (int)Math.Ceiling(pageCount);
        //        sp.PageCount = Convert.ToInt32(pageCount);
        //        sp.CurrentPageIndex = currentPageIndex;
        //        ViewBag.count = pageCount;
        //        ViewBag.data = "Yes";
        //        ViewBag.currindex = currentPageIndex;
        //    }
        //    else
        //    {
        //        ViewBag.data = "Nodata";
        //    }


        //    return View(result);
        //}

        // GET: Admin/Skills/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "skillName,isActive")] skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.createdBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                skill.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                skill.dataIsCreated = BaseUtil.GetCurrentDateTime();
                skill.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                repo.Insert(skill);
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        // GET: Admin/Skill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skill skill = repo.Single(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Admin/Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "skillName,isActive")] skill skill)
        {
            var s = repo.Single(skill.skillsID);
            if (s == null)
            {
                return HttpNotFound();
            }
            s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            s.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            s.isActive = skill.isActive;
            if (ModelState.IsValid)
            {
                skill.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                skill.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                repo.Update(s);
                return RedirectToAction("Index");
            }
            return View(skill);
        }
    }
}
