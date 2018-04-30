using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;

namespace NewLetter.Controllers
{
    public class HomeController : BaseClass
    {
        private oriondbEntities db = new oriondbEntities();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult General(int id)
        {
            ViewBag.Title = "Error";
            ViewBag.Icon = "glyphicon glyphicon-remove-circle";
            return View(db.app_error_log.Find(id));
        }
        public ActionResult AccessDenied()
        {
            ViewBag.Message = "AccessDenied";
            ViewBag.Title = "Access Denied";
            ViewBag.Icon = "glyphicon glyphicon-warning-sign";
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        

	}
}