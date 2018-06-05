using System;
using System.Linq;
using System.Web.Mvc;
using NewLetter.Models;

namespace NewLetter.Areas.Admin.Controllers
{
    [CustomErrorHandling]
    public class AdvertisementListController : BaseClass
    {
        oriondbEntities db = new oriondbEntities();
        // GET: Admin/AdvertisementList
        public ActionResult Index()
        {
            return View();
        }

        //GET :Admin : Partial View Candidate Result 
        [Route("", Name = "Admin")]
        public ActionResult _partialAdvertisementList()
        {           
            string spExecute = "sp_AdvertisementList";
            var result = (dynamic)null;
            try
            {
                result = db.Database.SqlQuery<sp_AdvertisementList_Result>(spExecute).ToList();
                ViewBag.data = "Yes";
            }
            catch (Exception e)
            {
                ViewBag.data = "";
                BaseUtil.CaptureErrorValues(e);
            }
           
            return PartialView(result);
        }

        // Search result
        [Route("", Name = "Admin")]
        public ActionResult advertisementResult(FormCollection frm)
        {
           
            string spExecute = ""; var result = (dynamic)null;
            try { 
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
                       

             result = db.Database.SqlQuery<sp_AdvertisementList_Result>(spExecute).ToList();           
            if(result.Count == 0)
            {
                ViewBag.data = "Nodata";
            }
           
            }
            catch (Exception e)
            {
                ViewBag.data = "";
                BaseUtil.CaptureErrorValues(e);
            }
            return PartialView("_partialAdvertisementList", result);
        }
    }
}