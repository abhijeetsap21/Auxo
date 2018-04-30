using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;

using NewLetter.Areas.Admin.Models;

namespace NewLetter.Areas.Admin.Controllers
{
    public partial class sp_dbStatics : BaseRepository<sp_dbStatics_Result>
    {

        public sp_dbStatics(IUnitOfWork unit) : base(unit)
        {
        }

    }
    public class dbStaticsController : BaseClass
    {
        private IUnitOfWork uow = null;
        private sp_dbStatics repo = null;
        public dbStaticsController()
        {

            uow = new UnitOfWork();
            repo = new sp_dbStatics(uow);
        }

        public JsonResult dbStatics_()
        {           
           var a= repo.SQLQuery<sp_dbStatics_Result>("sp_dbStatics").FirstOrDefault();
            return Json(a,JsonRequestBehavior.AllowGet);
        }

       
    }
}
