using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;

namespace NewLetter.jobSchedulers  
{
    public class completeYourProfile : Controller,IJob  
    {
        private oriondbEntities db = new oriondbEntities();
        DateTime updatbefore = BaseUtil.GetCalculatedDateTime(-180);


        public void Execute(IJobExecutionContext context)
        {
            var qendetails = db.qendidateLists.Where(l => l.IsReferenceCleared != true || l.dataIsUpdated >= updatbefore).ToList();
            foreach (var item in qendetails)
            {
               // StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateCompleteYourProfile.html"));
                StreamReader sr = new StreamReader(@"D:\\git\\Auxo\\Emailer\\toCandidateCompleteYourProfile.html");
                string HTML_Body = sr.ReadToEnd();
                string final_Html_Body = HTML_Body.Replace("#name", item.qenName).Replace("qenid",BaseUtil.encrypt(item.qenID.ToString()));
                sr.Close();
                string To = item.qenEmail.ToString();
                string mail_Subject = "Action Required on Profile ";
                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
            }
        }
    }
}