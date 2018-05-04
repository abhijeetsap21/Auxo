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
    public class CompanyMasterController : BaseClass
    {
        private IUnitOfWork uow = null;
        private CompanyMaster repo = null;
        oriondbEntities db = new oriondbEntities();
        public CompanyMasterController()
        {
            uow = new UnitOfWork();
            repo = new CompanyMaster(uow);
        }

        // GET: Admin/CompanyMaster
        public ActionResult Index()
        {
          
            return View();
        }

        [Route("", Name = "Admin")]
        public ActionResult _partialCompanyListResult()
        {
            var result = (dynamic)null;
            try
            {
                string spExecute = "sp_CompanyList @PageNumber = 1 ,@filterbyCreatedDateORmodifiedDate = true, ";

                DateTime fd = Convert.ToDateTime(BaseUtil.GetCalculatedDateTime(-30));
                spExecute += "@fromDate = '" + fd.Date + "',";

                DateTime fd1 = Convert.ToDateTime(BaseUtil.GetCurrentDateTime());
                spExecute += "@tillDate = '" + fd1.Date + "',";

                spExecute += "@isActive = 1 ";

                result = db.Database.SqlQuery<sp_CompanyList_Result>(spExecute).ToList();
                sp_EmployerList_Result sp = new sp_EmployerList_Result(); decimal pageCount = 0;
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
                sp.PageCount = (int)pageCount; sp.CurrentPageIndex = 1;
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
        public ActionResult companyListResult(int currentPageIndex, FormCollection frm)
        {
            var result = (dynamic)null;
            try
            {
                string spExecute = "sp_CompanyList @PageNumber =" + currentPageIndex + ",";


                spExecute += "@filterbyCreatedDateORmodifiedDate = 1 ,";


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
                    spExecute += "@ComapnyName ='" + frm["frm[companyname]"] + "',";
                }
                else
                {
                    spExecute += "@ComapnyName = null , ";
                }

                if (frm["frm[isActive]"] != null)
                {
                    spExecute += "@isActive = '" + frm["frm[isActive]"] + "'";
                }
                else
                {
                    spExecute += "@isActive = 1 ";
                }

                result = db.Database.SqlQuery<sp_CompanyList_Result>(spExecute).ToList();
                sp_CompanyList_Result sp = new sp_CompanyList_Result();
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

                sp.CurrentPageIndex = currentPageIndex;
                ViewBag.count = pageCount;
                ViewBag.data = "Yes";
                ViewBag.currindex = currentPageIndex;
            }
            catch (Exception e)
            {
                BaseUtil.CaptureErrorValues(e);
                ViewBag.data = "Nodata";
            }
            return PartialView("_partialCompanyListResult",result);
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
                ViewBag.data = "Nodata";
            }
            return result;
        }
    }

     
    }
