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
    public class AdvertisementListController : BaseClass
    {
        private IUnitOfWork uow = null;
        private AdvertisementList repo = null;

        public AdvertisementListController()
        {
            uow = new UnitOfWork();
            repo = new AdvertisementList(uow);
        }
        // GET: Admin/AdvertisementList
        public ActionResult Index()
        {
            return View();
        }

        //GET :Admin : Partial View Candidate Result 
        [Route("", Name = "Admin")]
        public ActionResult _partialAdvertisementList()
        {
            oriondbEntities db = new oriondbEntities();
            string spExecute = "sp_AdvertisementList";
            var result = db.Database.SqlQuery<sp_AdvertisementList_Result>(spExecute).ToList();
            sp_AdvertisementList_Result sp = new sp_AdvertisementList_Result();
            ViewBag.data = "Yes";
            return PartialView(result);
        }

        // Search result
        [Route("", Name = "Admin")]
        public ActionResult advertisementResult(FormCollection frm)
        {
            oriondbEntities db = new oriondbEntities();
            string spExecute = "";
            
            if (frm["frm[fromdate]"] != "")
            {
                DateTime fd = Convert.ToDateTime(frm["frm[fromdate]"]);
                spExecute += "sp_AdvertisementList @fromDate = '" + fd.Date + "',";
            }
            else
            {
                DateTime fd = Convert.ToDateTime(BaseUtil.GetCalculatedDateTime(-30));
                spExecute += "sp_AdvertisementList @fromDate = '" + fd.Date + "',";
            }

            if (frm["frm[todate]"] != "")
            {


                DateTime td = Convert.ToDateTime(frm["frm[todate]"]);
                spExecute += "@tillDate = '" + td.Date + "'";

            }
            else
            {
                spExecute += "@tillDate = null ";
            }           
                       

            var result = db.Database.SqlQuery<sp_AdvertisementList_Result>(spExecute).ToList();
            sp_AdvertisementList_Result sp = new sp_AdvertisementList_Result();           
            if(result.Count == 0)
            {
                ViewBag.data = "Nodata";
            }
            return PartialView("_partialAdvertisementList", result);
        }
    }
}