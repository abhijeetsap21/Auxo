﻿using System;
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
    public class CandidateMasterController : BaseClass
    {
        private IUnitOfWork uow = null;
        private CandidateMaster repo = null;

        public CandidateMasterController()
        {
            uow = new UnitOfWork();
            repo = new CandidateMaster(uow);
        }        
        // GET: Admin/EmployerMaster
        public ActionResult Index()
        {
           
            return View();
        }

        //GET :Admin : Partial View Candidate Result 
        [Route("",Name ="Admin")]
        public ActionResult _partialCandidateResult ()
        {
            oriondbEntities db = new oriondbEntities();
            string spExecute = "sp_QendidateList @PageNumber = 1 ,@filterbyCreatedDateORmodifiedDate = true,";
            DateTime fd = Convert.ToDateTime(BaseUtil.GetCalculatedDateTime(-30));
            spExecute += "@fromDate = '" + fd.Date + "',";


            DateTime fd1 = Convert.ToDateTime(BaseUtil.GetCurrentDateTime());
            spExecute += "@tillDate = '" + fd1.Date + "',";

            spExecute += "@isActive = 1 ";

            var result = db.Database.SqlQuery<sp_QendidateList_Result>(spExecute).ToList();
            sp_QendidateList_Result sp = new sp_QendidateList_Result();
            decimal pageCount = 0;
            if (result.Count > 0)
            {
                pageCount = (decimal)(result[1].total / 4);
                int a = (int)(result[1].total % 4);
                if (a > 0) { pageCount = pageCount + 1; }
                ViewBag.data = "Yes";

            }
            else
            {
                ViewBag.data = "Nodata";
            }
            sp.PageCount = (int)pageCount;
            sp.CurrentPageIndex = 1;
            ViewBag.count = pageCount;
           
            ViewBag.currindex = sp.CurrentPageIndex;
            return PartialView(result);
        }

        // Search result
        [Route("", Name = "Admin")]
        public ActionResult candidateResult(int currentPageIndex, FormCollection frm)
        {
            oriondbEntities db = new oriondbEntities();
            string spExecute = "sp_QendidateList @PageNumber =" + currentPageIndex + ",";
            
                spExecute += "@filterbyCreatedDateORmodifiedDate = 1 ,";
            
            if (frm["frm[fromdate]"] != "" )
            {                
                DateTime fd = Convert.ToDateTime(frm["frm[fromdate]"]);
                spExecute += "@fromDate = '" + fd.Date + "',";
            }
            else
            {
                DateTime fd = Convert.ToDateTime(BaseUtil.GetCalculatedDateTime(-30));
                spExecute += "@fromDate = '" + fd.Date + "',";
            }

            if (frm["frm[todate]"] != "")
            {
                
                
                DateTime td = Convert.ToDateTime(frm["frm[todate]"]);
                spExecute += "@tillDate = '" + td.Date + "',";

            }
            else
            {
                DateTime fd = Convert.ToDateTime(BaseUtil.GetCurrentDateTime());
                spExecute += "@tillDate = '" + fd.Date + "',";
            }
          
            if (frm["frm[name]"] != "")
            {                            
                
                
                spExecute += "@name = '" + frm["frm[name]"] + "',";
            }
            else
            {
                spExecute += "@name = null , ";
            }
            if (frm["frm[mobile]"] != "")
            {
                spExecute += "@mobile = '" + frm["frm[mobile]"] + "',";

            }
            else
            {
                spExecute += "@mobile = null ,";
            }
            
            if (frm["frm[email]"] != "")
            {                
                spExecute += "@email = '" + frm["frm[email]"] + "',";
            }
            else
            {
                spExecute += "@email = null ,";

            }
            if (frm["frm[isActive]"] != null)
            { 
                spExecute += "@isActive = '" + frm["frm[isActive]"] + "'";
            }
            else
            {
                spExecute += "@isActive = 1 ";
            }

            var result = db.Database.SqlQuery<sp_QendidateList_Result>(spExecute).ToList();
            sp_QendidateList_Result sp = new sp_QendidateList_Result();
            decimal pageCount = 0;
            if (result.Count > 0)
            {
                pageCount = (decimal)(result[1].total / 4);
                int a = (int)(result[1].total % 4);
                if (a > 0) { pageCount = pageCount + 1; }
               
 ViewBag.data = "Yes";
            }
            else
            {
                ViewBag.data = "Nodata";
            } sp.PageCount = (int)pageCount;
            sp.CurrentPageIndex = currentPageIndex;
                ViewBag.count = pageCount;
               
                ViewBag.currindex = currentPageIndex;
           
            return PartialView("_partialCandidateResult",result);
        }

   


   

        public ActionResult _partialEditCandidateMaster(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qendidateList qenList = repo.Single(id);
            if (qenList == null)
            {
                return HttpNotFound();
            }

            return View(qenList);
        }

        // POST: Admin/EmployerMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        public ActionResult saveCandidateMaster( qendidateList qendidateList)
        {
            var s = repo.Single(qendidateList.qenID);
            if (s == null)
            {
                return HttpNotFound();
            }

            s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            s.isActive = qendidateList.isActive;
            if (ModelState.IsValid)
            {
                repo.Update(s);
                
            }
            return PartialView("_partialEditCandidateMaster");
        }

       

        public string updateDB(bool check, long qenID)
        {

            string result = "no";
            var s = repo.Single(qenID);
            if (s != null)
            {
                s.isActive = check;
                repo.Update(s);
                result = "ok";
            }
            
            return result;
        }
    }
}
