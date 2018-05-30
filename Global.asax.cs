using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NewLetter.Models;
using NewLetter.App_Start;
using System.Configuration;
using System.Data.SqlClient;

namespace NewLetter
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        //string con = ConfigurationManager.ConnectionStrings["NotificationConnection"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            //SqlDependency.Start(con);
            // jobSchedulers.schedulerMain.Start();

        }

        // below code for automatic redirectoin to secure 
        //protected void Application_BeginRequest()
        //{
        //    if (!Context.Request.IsSecureConnection)
        //        Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));
        //}
        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    NotificationComponenet NC = new NotificationComponenet();
        //    var currentTime = DateTime.Now;
        //    HttpContext.Current.Session["LastUpdated"] = currentTime;
        //    NC.RegisterNotification(currentTime);
        //}

        //protected void Application_End()
        //{
        //    //here we will stop Sql Dependency
        //    SqlDependency.Stop(con);
        //}
    }
}