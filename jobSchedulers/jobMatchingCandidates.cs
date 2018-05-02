using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;
using System.IO;
using System.Data;
using Quartz;
using Quartz.Impl;

namespace NewLetter.jobSchedulers
{
    public class jobMatchingCandidates : Controller, IJob
    {
        private oriondbEntities db = new oriondbEntities();
        DateTime updatbefore = BaseUtil.GetCalculatedDateTime(-180);

        public void Execute(IJobExecutionContext context)
        {
            DateTime updatbefore = BaseUtil.GetCalculatedDateTime(-180);
            var qenIDList = db.Database.SqlQuery<sp_schedular_result>("sp_schedular");
            Int64 qenid = 0;

            int cout = 0;
            string rows;
            string rownew = "";
            StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateRecomendedJob.html"));
            foreach (var qen in qenIDList)
            {
                if (qenid == 0 || qenid != qen.qenid)
                {
                    rows = "";
                    rownew = "";
                    cout = qenIDList.Where(e => e.qenid == qen.qenid).Count();
                    for (int i = 1; i <= cout; i++)
                    {
                        var jobID_ = qenIDList.Where(e => e.qenid == qen.qenid && e.top10 == i).Select(e => new { e.jobID }).FirstOrDefault();
                        var jobTitle_ = qenIDList.Where(e => e.qenid == qen.qenid && e.top10 == i).Select(e => new { e.jobTitle }).FirstOrDefault();
                        rows = MvcHtmlString.Create("<tr><td><a href='http://spotaneedle.com/jobDetails/JobView?jobID='" + jobID_.jobID + "'>'" + i + "'.)'" + jobTitle_.jobTitle + "'</a></td></tr><br />").ToString();
                        rownew = rows + rownew;
                        i++;
                    }
                    qenid = qen.qenid;
                    var demo = MvcHtmlString.Create("<table>'" + rownew + "'</table>").ToString();
                    string HTML_Body = sr.ReadToEnd();
                    string final_Html_Body = HTML_Body.Replace("#content", demo);

                    string To = qen.qenEmail.ToString();
                    string mail_Subject = "Jobs matching your profile";
                    string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                }

            }
            sr.Close();
        }
    }
}