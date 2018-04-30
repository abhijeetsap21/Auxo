using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;

namespace NewLetter.Models
{
    public class BaseClass :Controller
    {
        public oriondbEntities db = null;
        public BaseClass()
        {
            db = new oriondbEntities();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            String ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper();
            String ActionName = filterContext.ActionDescriptor.ActionName.ToUpper();
            String Action = string.Format("{0}Controller{1}", ControllerName, ActionName).ToUpper();
            //-----Check Authorization ---------

            if (BaseUtil.ListControllerExcluded().Contains(ControllerName))
            {
                if ((ControllerName == "PROFILE" && (ActionName == "SELECTTEST" || ActionName == "SUBMITSELECTTEST" || ActionName== "REFERENCECHECK" || ActionName == "SELECTMEET" || ActionName == "BOOKMEET")) || (ControllerName  == "EMPLOYER" && ActionName == "SUBMITINTEREST") ||(ControllerName == "JOBDETAILS" && (ActionName== "LOADMOREJOBS" || ActionName == "JOBVIEW" || ActionName == "SHAREWF" || ActionName == "SELECT_FILLSKILLS") || ControllerName=="ACCOUNT" || ControllerName == "HOME"))
                {
                    return;
                }
                if (BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()) == "")
                {
                    filterContext.Result = null;
                    filterContext.Result = new RedirectResult("/Account/login");
                    return;
                }
                if (!BaseUtil.CheckAuthentication(filterContext))
                {
                    filterContext.Result = null;
                    filterContext.Result = new RedirectResult("/Home/AccessDenied");
                    return;
                }

                return;
            }
            else
            {
                if (BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()) == "")
                {
                    filterContext.Result = null;
                    filterContext.Result = new RedirectResult("/Account/login");
                    return;
                }
                if (!BaseUtil.CheckAuthentication(filterContext))
                {
                    filterContext.Result = null;
                    filterContext.Result = new RedirectResult("/Home/AccessDenied");
                    return;
                }

                return;
            }

        }

    }
}