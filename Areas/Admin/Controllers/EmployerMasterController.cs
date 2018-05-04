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
    [CustomErrorHandling]
    public class EmployerMasterController : BaseClass
    {
        private IUnitOfWork uow = null;
        private EmployerMaster repo = null;
        oriondbEntities db = new oriondbEntities();
        public EmployerMasterController()
        {
            uow = new UnitOfWork();
            repo = new EmployerMaster(uow); 
        }
        //private oriondbEntities db = new oriondbEntities();

        // GET: Admin/EmployerMaster
        public ActionResult Index()
        {
            return View();
        }

        //
        [Route("", Name = "Admin")]
        public ActionResult _partialEmployerListResult()
        {
            var result = (dynamic)null;
            try
            {
                string spExecute = "sp_EmployerList @PageNumber = 1 ,@filterbyCreatedDateORmodifiedDate = true,";

                DateTime fd = Convert.ToDateTime(BaseUtil.GetCalculatedDateTime(-30));
                spExecute += "@fromDate = '" + fd.Date + "',";

                DateTime fd1 = Convert.ToDateTime(BaseUtil.GetCurrentDateTime());
                spExecute += "@tillDate = '" + fd1.Date + "',";

                spExecute += "@isActive = 1 ";
                 result = db.Database.SqlQuery<sp_EmployerList_Result>(spExecute).ToList();
                //var emp = repo.SQLQuery<sp_EmployerList_Result>("sp_EmployerList @PageNumber, @filterbyCreatedDateORmodifiedDate", new SqlParameter("PageNumber", SqlDbType.Int) { Value = 1},new SqlParameter("filterbyCreatedDateORmodifiedDate", SqlDbType.Bit) { Value = 0}).ToList();
                sp_EmployerList_Result sp = new sp_EmployerList_Result();
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
            }
            catch (Exception e)
            {
                
                BaseUtil.CaptureErrorValues(e);
                ViewBag.data = "Nodata";
            }
                return PartialView(result);

            }
        // Search result
        [Route("", Name = "Admin")]
        public ActionResult EmployerListResult(int currentPageIndex, FormCollection frm)
        {
            var result = (dynamic)null;
            try { 
            string spExecute = "sp_EmployerList @PageNumber ='" + currentPageIndex + "',";
          
                spExecute += "@filterbyCreatedDateORmodifiedDate ='1', ";
           

            if (frm["frm[fromdate]"] != "")
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
            if (frm["frm[companyname]"] != "")
            {
                spExecute += "@ComapnyName = '" + frm["frm[companyname]"] + "',";
            }
            else
            {
                spExecute += "@ComapnyName = null , ";
            }
            if (frm["frm[employername]"] != "")
            {
                spExecute += "@EmployerName = '" + frm["frm[employername]"] + "',";
            }
            else
            {
                spExecute += "@EmployerName = null , ";
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

             result = db.Database.SqlQuery<sp_EmployerList_Result>(spExecute).ToList();
            sp_EmployerList_Result sp = new sp_EmployerList_Result();
         
            decimal pageCount = 0;
            if (result.Count > 0)
            {
                pageCount = (decimal)(result[1].total / 4);
                int a = (int)(result[1].total % 4);
                if (a > 0) { pageCount = pageCount + 1; } ViewBag.data = "Yes";
            }
            else
            {
                ViewBag.data = "Nodata";
            }
               sp.CurrentPageIndex = currentPageIndex;
                ViewBag.count = pageCount;
               
                ViewBag.currindex = currentPageIndex;

            }
            catch (Exception e)
            {               
                BaseUtil.CaptureErrorValues(e);
                ViewBag.data = "Nodata";
            }
            return PartialView("_partialEmployerListResult",result);
        }


     
    

        // GET: Admin/EmployerMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            var emp = (dynamic)null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                emp = repo.Single(id);
            }
            catch (Exception e)
            {              
                BaseUtil.CaptureErrorValues(e);
            }

            return View(emp);
        }

            // POST: Admin/EmployerMaster/Edit/5
            //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isActive")] EmployerDetail employerDetail)
        {
            try
            {
                var s = repo.Single(employerDetail.EmployerID);
                if (s == null)
                {
                    return HttpNotFound();
                }

                s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                s.isActive = employerDetail.isActive;
                if (ModelState.IsValid)
                {
                    repo.Update(s);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {              
                BaseUtil.CaptureErrorValues(e);
            }
            return View(employerDetail);
        }

       

        public string updateDB(bool check, long qenID)
        {

            string result = "no";
            try
            {
                var s = repo.Single(qenID);
                if (s != null)
                {
                    s.isActive = check;
                    repo.Update(s);
                    result = "ok";
                }
            }
            catch (Exception e)
            {               
                BaseUtil.CaptureErrorValues(e);
            }
            return result;
        }

    }
}
