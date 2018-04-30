﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.Reflection;
using System.Web.Routing;
using System.Data.Entity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
namespace NewLetter.Models
{
    public class baseClass : oriondbEntities
    {
        #region AutoGenerated Password
        public static string GetRandomPasswordString(int pwdLength)
        {
            int asciiZero;
            int asciiNine;
            int asciiA;
            int asciiZ;
            int count = 0;
            int randNum;
            string randomString;

            System.Random rRandom = new System.Random(System.DateTime.Now.AddMinutes(0).Millisecond);

            asciiZero = 48;
            asciiNine = 57;
            asciiA = 64;
            asciiZ = 90;

            randomString = "";
            while ((count < pwdLength))
            {
                if (count % 2 == 0)
                {
                    randNum = rRandom.Next(asciiZero, asciiNine);
                }
                else
                {
                    randNum = rRandom.Next(asciiA, asciiZ);
                }
                if (((randNum >= asciiZero) && (randNum <= asciiNine)) || ((randNum >= asciiA) && (randNum <= asciiZ)))
                {
                    randomString = (randomString + ((char)(randNum)));
                    count = (count + 1);
                }
            }
            return randomString;
        }
        #endregion
        #region Configuration
        public static string GetWebConfigValue(string Name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Name].ToString(); ;
        }

        #endregion
        #region Send Mail
        //public static void SendMail(String EmailIDTo, String SubjectText, String Body, String[] attachments = null)
        //{
        //    try
        //    {

        //        string EMAIL_SENT = GetWebConfigValue("EMAIL_SENT");
        //        string COMPANY_EMAIL = GetWebConfigValue("COMPANY_EMAIL");
        //        string COMPANY_EMAIL_PWD = GetWebConfigValue("COMPANY_EMAIL_PWD");
        //        string Host = GetWebConfigValue("Host");

        //        string CC = GetWebConfigValue("CC");
        //        //string BCC = GetWebConfigValue("BCC");

        //        SmtpClient smtpClient = new SmtpClient();
        //        MailMessage message = new MailMessage();
        //        MailAddress fromAddress = new MailAddress(COMPANY_EMAIL, SubjectText);
        //        smtpClient.Host = Host;
        //        smtpClient.Port = 587;
        //        smtpClient.EnableSsl = true;
        //        smtpClient.UseDefaultCredentials = false;
        //        smtpClient.Credentials = new System.Net.NetworkCredential(COMPANY_EMAIL, COMPANY_EMAIL_PWD);

        //        message.From = fromAddress;
        //        message.To.Add(Convert.ToString(EmailIDTo.Trim()));
        //        //message.CC.Add(Convert.ToString(CC));
        //        // message.Bcc.Add(Convert.ToString(BCC));

        //        StringBuilder sb = new StringBuilder();
        //        if (attachments != null)
        //        {
        //            foreach (var item in attachments)
        //            {
        //                if (item != null)
        //                    message.Attachments.Add(new Attachment(item));
        //            }
        //        }
        //        message.Subject = SubjectText;
        //        message.IsBodyHtml = true;
        //        message.Body = Body;// +sb.ToString();
        //        if (EMAIL_SENT == "Y")
        //        {
        //            smtpClient.Send(message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        #endregion



    }
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
    }

    public enum AdminInfo
    {
        UserID, FullName, UserPhoto, EmailID, Mobile, role_id, LoginID, IP, Last_Login_Date, latitude, longitude, IPTrackerID,

        SuperAdmin, DataEntryoperator, tktcategory, RoleBit,companyID,employerID,companyname,logo, password,
        Session_Person,socialCheck
    }


    public class HtmlHelperAutoComplete
    {
        public HtmlHelperAutoComplete()
        {
            this.ControllerName = "Json";
            this.ParmId = "";
            this.Placeholder = "";
        }

        public string Id { get; set; }
        public string hiddenId { get; set; }
        public string Value { get; set; }
        public string ClassName { get; set; }
        public string DbId { get; set; }
        public string dbValue { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ParmId { get; set; }
        public string Placeholder { get; set; }
        public BaseValidation BaseValidation { get; set; }
    }
    public enum FileType
    {
        PDF,
        DOC,
        TXT,
        CSV,
        XLS,
        XLSX,
        JPG,
        PNG,
        BMP,
    }



    public static class BaseUtil
    {
        private static baseClass db = new baseClass();
        // private static CommonUtil commonUtil = new CommonUtil();
        public static string JobpostOption { get; set; }
        #region Date & Time
        public static String WeekNumber(DateTime dt)
        {

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dt);

            return "Week-" + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        }

        public static String WeekNumberStartDate(DateTime dt)
        {

            DayOfWeek day = DayOfWeek.Sunday;
            int diff = dt.DayOfWeek - day;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date.ToString("dd-MMM-yyyy");

        }

        public static DateTime GetCurrentDateTime()
        {
            Int32 diffMinutes = Convert.ToInt32(GetWebConfigValue("MIN_DIFF"));
            return System.DateTime.Now.AddMinutes(diffMinutes);
        }
        public static DateTime GetCalculatedDateTime(int day)
        {
            Int32 diffMinutes = Convert.ToInt32(GetWebConfigValue("MIN_DIFF"));
            return System.DateTime.Now.AddMinutes(diffMinutes).AddDays(day);
        }
        public static DateTime GetTodayDate()
        {
            Int32 diffMinutes = Convert.ToInt32(GetWebConfigValue("MIN_DIFF"));
            return System.DateTime.Today.AddMinutes(diffMinutes).Date;
        }
        
        public static DateTime convertToDate(string str) {
            DateTime dt;

            string dd =  str.Substring(0, 2);
            string mm = str.Substring(3, 2);
            string yy = str.Substring(6, 4);
            string dat = mm + "/" + dd + "/" + yy;
            dt = Convert.ToDateTime(dat);
            return dt;
        }
        #endregion

        #region Configuration
        public static string GetWebConfigValue(string Name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Name].ToString(); ;
        }

        #endregion

        #region Set Property
        public static void SetProperty(object p, string propName, object value)
        {
            Type t = p.GetType();
            PropertyInfo info = t.GetProperty(propName);
            if (info == null)
                return;
            if (!info.CanWrite)
                return;
            info.SetValue(p, value, null);
        }
        #endregion

        #region Capture Error
        public static int CaptureErrorValues(Exception exception)
        {
            int errorLogID = 0;
            #region Capture the Values
            StringBuilder errrorMessage = new StringBuilder();

            string FormValues = String.Empty;

            string SessionValues = String.Empty;
            if (HttpContext.Current.Session != null)
            {
                foreach (var item in HttpContext.Current.Session.Keys)
                {
                    SessionValues = SessionValues + item.ToString() + " : " + HttpContext.Current.Session[item.ToString()] + "<br>";
                }
            }

            string RawUrl = HttpContext.Current.Request.RawUrl;
            string RequestType = HttpContext.Current.Request.RequestType;
            string UserAgent = HttpContext.Current.Request.UserAgent;
            string BrowserName = ((System.Web.Configuration.HttpCapabilitiesBase)(HttpContext.Current.Request.Browser)).Browser;


            errrorMessage.Append("<b>Simplysafe Tech Support Error</b><br><br>");

            errrorMessage.Append("<b>Error Date  :</b>" + System.DateTime.Now + "<br>");
            errrorMessage.Append("<b>Server Name  :</b>" + System.Environment.MachineName + "<br>");
            errrorMessage.Append("<b>Application Url :</b>" + RawUrl + "<br>");
            errrorMessage.Append("<b>Request Type :</b>" + RequestType + "<br>");
            errrorMessage.Append("<b>User Agent :</b>" + UserAgent + "<br><br>");
            errrorMessage.Append("<b>BrowserName :</b>" + BrowserName + "<br><br>");

            errrorMessage.Append("<b>Exception InnerException :</b>" + exception.InnerException + "<br><br>");
            errrorMessage.Append("<b>Exception Message :</b>" + exception.Message + "<br><br>");
            errrorMessage.Append("<b>Exception Source :</b>" + exception.Source + "<br><br>");

            errrorMessage.Append("<u><b>Posted Form Values</b></u><br>");
            errrorMessage.Append(FormValues);

            errrorMessage.Append("<br><br><u><b>Session Values Values</b></u><br>");
            errrorMessage.Append(SessionValues);

            errrorMessage.Append("<br><br><u><b>Exception Stack Trace:</b></u><br>" + exception.StackTrace);

            #endregion

            try
            {
                Int32 userID = 0;
                if (HttpContext.Current.Session != null)
                {
                    userID = HttpContext.Current.Session[AdminInfo.UserID.ToString()] != null ? Convert.ToInt32(HttpContext.Current.Session[AdminInfo.UserID.ToString()].ToString()) : 0;
                }
                DateTime currDateTime = GetCurrentDateTime();

                #region Insert Into APP_ERROR_LOG

                app_error_log errorlog = new app_error_log();
                errorlog.error_message = errrorMessage.ToString();
                if (userID > 0)
                {
                    errorlog.user_id = userID;
                }
                else
                {
                    errorlog.user_id = null;

                }
                errorlog.created_date = currDateTime;

                db.app_error_log.Add(errorlog);
                db.SaveChanges();

                errorLogID = errorlog.error_log_id;
                #endregion

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {

            }
            return errorLogID;

        }
        public static string GetValidationMessage(ModelStateDictionary modelState)
        {
            string msgResult = "";
            for (int i = 0; i < modelState.ToList().Count; i++)
            {
                if (modelState.ToList()[i].Value.Errors.Count > 0)
                {
                    msgResult += string.Format("{0}~{1};", modelState.ToList()[i].Key.ToString(), modelState.ToList()[i].Value.Errors[0].ErrorMessage);
                }
            }
            return msgResult;
        }
        #endregion

        #region Session
        public static void SetSessionValue(String Key, String Value)
        {
            HttpContext.Current.Session[Key] = Value;
        }
        public static String GetSessionValue(String Key)
        {
            return HttpContext.Current.Session[Key] != null ? HttpContext.Current.Session[Key].ToString() : string.Empty;
        }

        #endregion

        #region Accesible Pages

        public static List<string> AccesiblePages(int UserRole)
        {

            List<string> listAccessilePages = new List<string>();
            try
            {

                var au = (from R in db.roles
                          join RA in db.role_action on R.roleID equals RA.role_id
                          where R.roleID == UserRole
                          select new
                          {
                              ACTION = (RA.controller_name + RA.action_name).ToUpper()
                          }
                          ).ToList();

                foreach (var item1 in au)
                {
                    listAccessilePages.Add(item1.ACTION);
                }

            }


            catch (Exception ex)
            {
                throw ex;
            }
            return listAccessilePages;
        }
        public static List<string> ListControllerExcluded()
        {
            List<string> list = new List<string>() { "BASE", "JSON", "HOME", "ACCOUNT", "JOBDETAILS","EMPLOYER","PROFILE" };
            return list;
        }
       public static List<string> accesiblePages = null;
        public static bool CheckAuthentication(ActionExecutingContext filterContext)
        {
            bool result = false;

            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            String Action = string.Format("{0}{1}", controllerName, actionName).ToUpper();

            int UserRole = 0;
            if (IsAuthenticated())
            {
                UserRole = Convert.ToInt32(GetSessionValue(AdminInfo.role_id.ToString()));
            }
            if (accesiblePages == null)
            {
                accesiblePages = AccesiblePages(UserRole);
            }
            foreach (var item in accesiblePages)
            {
                if (Action == item)
                {
                    result = true;
                    break;
                }

            }

            return result;
        }

        #endregion


        public static String CheckUserFrofile(String LoginID, String PWD, string ActionName)
        {
            BaseClass bcs = new BaseClass();

            db = new baseClass();
            String result = "Invalid User Name / Password ";
            if (ActionName == "login")
            {
                var userInfo = db.EmployerDetails.AsEnumerable().Where(U => U.Email == LoginID && U.password == PWD && U.isDelete == false).ToList();

                foreach (var user in userInfo)
                {
                    if (!user.isActive)
                    {
                        result = !user.isActive ? "Your are not authorized to login! Please contact to Admin" : String.Empty;
                    }
                    else if (user.isActive)
                    {
                        string IP = "?";
                        IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (IP == "" || IP == null)
                        {
                            IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        }
                        Int32 IPTrackerID = 150;// AddUpdateUserTracker(user.UserID, IP, latitude, longitude); 
                        
                        SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(user.EmployerID));
                        SetSessionValue(AdminInfo.companyID.ToString(), Convert.ToString(user.companyID));
                        SetSessionValue(AdminInfo.employerID.ToString(), Convert.ToString(user.EmployerID));
                        SetSessionValue(AdminInfo.LoginID.ToString(), Convert.ToString(user.Email));
                        SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.Mobile));
                        SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(user.roleID));
                        SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(user.Name));
                        string encr = BaseUtil.encrypt(user.password);
                        SetSessionValue(AdminInfo.password.ToString(), encr);
                        SetSessionValue(AdminInfo.IP.ToString(), IP);
                        SetSessionValue(AdminInfo.IPTrackerID.ToString(), Convert.ToString(IPTrackerID));
                        var companyinfo = db.companyDetails.Where(c => c.companyID == user.companyID).FirstOrDefault();
                        var companyName = companyinfo.companyName;
                        var companylogo = companyinfo.logo;
                        SetSessionValue(AdminInfo.companyname.ToString(), companyinfo.companyName);
                        SetSessionValue(AdminInfo.logo.ToString(), companyinfo.logo);
                        result = "PASS";
                    }
                    else
                    {
                        result = !user.isActive ? "Your are Inactive! Please contact to Admin" : String.Empty;
                    }
                }
            }
            else
            {

                var userInfo = db.qendidateLists.AsEnumerable().Where(U => U.qenEmail == LoginID && U.password == PWD).ToList();
                foreach (var user in userInfo)
                {
                    if (!user.isActive)
                    {
                        result = !user.isActive ? "Your are not authorized to login! Please contact to Admin" : String.Empty;
                    }
                    else if (user.isActive)
                    {
                        //user.lastLoginTime = BaseUtil.GetCurrentDateTime();
                        //db.Entry(user).State = EntityState.Modified;
                        //db.SaveChanges();
                        string IP = "?";
                        IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (IP == "" || IP == null)
                        {
                            IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        }
                        Int32 IPTrackerID = 150;// AddUpdateUserTracker(user.UserID, IP, latitude, longitude); 
                        SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(user.qenID));
                        SetSessionValue(AdminInfo.LoginID.ToString(), Convert.ToString(user.qenEmail));
                        SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.qenPhone));
                        SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(user.roleID));
                       // SetSessionValue(AdminInfo.socialCheck.ToString(), Convert.ToString(user.socialCheck));
                        SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(user.qenName));
                        SetSessionValue(AdminInfo.IP.ToString(), IP);
                        SetSessionValue(AdminInfo.IPTrackerID.ToString(), Convert.ToString(IPTrackerID));                        
                        result = "PASS";
                    }
                    else
                    {
                        result = !user.isActive ? "Your are Inactive! Please contact to Admin" : String.Empty;
                    }
                }
            }

            return result;
        }

        // Check matching variables 

        public static int matching_skills(long jobID, long qenID)
        {
            var jobskills = db.jobSkills.Where(e => e.jobID == jobID).Select(e => new { e.skillsID }).ToArray();
            var qenSkills = db.qenSkills.Where(e => e.qenID == qenID).Select(e => new { e.skillsID }).ToArray();
            int j = 0;
            var matchedSkills = jobskills.Intersect(qenSkills).ToArray();
            if (jobskills != null && qenSkills != null && matchedSkills.Count() > 0)
            {
                j = Convert.ToInt16(((Int32)matchedSkills.Count() * 100) / (Int32)jobskills.Count());
            }
            return j;
        }

        public static int CheckUserExists(String EmailID)
        {
            return (from M in db.EmployerDetails.AsEnumerable()
                    where M.Email == EmailID
                    select M).Count();
        }

        public static bool IsAuthenticated()
        {
            return string.IsNullOrEmpty(GetSessionValue(AdminInfo.UserID.ToString())) ? false : true;
        }


        public static string RoleID()
        {
            return Convert.ToString(GetSessionValue(AdminInfo.role_id.ToString()));
        }
        #region Application Path

        static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        static HttpRequest Request
        {
            get { return Context.Request; }
        }


        public static UrlHelper GetUrlHelper()
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public static string GetActionPath(string controllerActionName) //Ex: "Category/Edit"
        {
            return GetApplicationPath(Request.RequestContext) + string.Format("/{0}", controllerActionName);
        }

        public static string GetActionPath(string controllerName, string actionName)
        {
            return GetApplicationPath(Request.RequestContext) + string.Format("/{0}/{1}", controllerName, actionName);
        }

        public static string GetApplicationPath(RequestContext requestContext)
        {
            string retPath;
            string httpOrigin = Request.ServerVariables["HTTP_ORIGIN"];
            string applicationPath = Request.ApplicationPath;
            //Approach #1: OK:Post
            //retPath = (applicationPath == "/" ? httpOrigin : httpOrigin + applicationPath);
            //Approach #2 OK:All
            retPath = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority) + (applicationPath == "/" ? "" : applicationPath);
            return retPath;
        }

        public static string GetApplicationPath()
        {
            string retPath;
            string applicationPath = Request.ApplicationPath;
            retPath = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority) + (applicationPath == "/" ? "" : applicationPath);
            return retPath;
        }
        public static string GetLoginID()
        {
            return SessionUtil.GetLoginID();
        }

        public static string GetCurrentController()
        {
            return Convert.ToString(Request.RequestContext.RouteData.Values["controller"]);
        }
        public static string GetCurrentAction()
        {
            return Convert.ToString(Request.RequestContext.RouteData.Values["action"]);
        }

        public static List<string> GetControllerNames()
        {
            List<string> controllerNames = new List<string>();
            GetSubClasses<Controller>().ForEach(
                type => controllerNames.Add(type.Name));
            return controllerNames;
        }

        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        public static List<string> GetControllerActionNames(Type t)
        {
            return t.GetMethods().Where(m => m.ReturnType == typeof(ActionResult))
                .Select(m => m.Name).Distinct().ToList();
        }
        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        #endregion
        public static string JOb_skillSet(long jobID)
        {
            string selectedskills = string.Empty;
            var ojobSkill = db.jobSkills.Where(e => e.jobID == jobID);
            if (ojobSkill != null)
            {

                foreach (var s in ojobSkill)
                {
                    var skillName_ = db.skills.Where(e => e.skillsID == s.skillsID).Select(x => new { x.skillName }).SingleOrDefault();
                    selectedskills = selectedskills + skillName_.skillName + ",";
                }
            }
            return selectedskills;
        }
        public static string Qen_skillSet(long QenID)
        {
            string selectedskills = string.Empty;
            var ojobSkill = db.qenSkills.Where(e => e.qenID == QenID);
            db.Database.Log = msg => Debug.Write(msg);
            if (ojobSkill != null)
            {

                foreach (var s in ojobSkill)
                {
                    var skillName_ = db.skills.Where(e => e.skillsID == s.skillsID).Select(x => new { x.skillName }).SingleOrDefault();
                    selectedskills = selectedskills + skillName_.skillName + ",";
                }
            }
            return selectedskills;
        }

        public static string SearchSkillStes(string skills)
        {

            string skillsReq = string.Empty;
            List<int> list = new List<int>();
            if (skills != "")
            {

                string[] values = skills.Split(',').Select(sValue => sValue.Trim()).ToArray();
                db.Database.Log = msg => Debug.Write(msg);
                foreach (string s in values)
                {
                    if (s != "")
                    {
                        list.Add(checkValuExist(s));
                    }
                }

                if (list.Count > 0)
                {
                    list.Sort();
                    for (int u = 0; u <= list.Count - 1; u++)
                    {
                        if (u != list.Count - 1)
                        {
                            skillsReq = skillsReq + list[u].ToString() + ",";
                        }
                        else
                        {
                            skillsReq = skillsReq + list[u].ToString();
                        }
                    }

                }
            }
            return skillsReq;
        }

        public static string checkSocialProfile(string email)
        {
            var result = "Profile Exists / Not Exists";
            var user = db.qendidateLists.Where(u => u.qenEmail == email).FirstOrDefault();
            if (user == null)
            {
                result = "NotExists";
            }
            else
            {
                result = "Exists";
            }

            return result;
        }

        public static void Skills(string orequiredSkills, long jobID)
        {
            jobSkill ojobSkill = null;
            if (jobID > 0)
            {
                db.jobSkills.RemoveRange(db.jobSkills.Where(x => x.jobID == jobID));
                string skills = orequiredSkills;
                string[] values = skills.Split(',').Select(sValue => sValue.Trim()).ToArray();
                foreach (string s in values)
                {
                    if (s != "")
                    {
                        ojobSkill = new jobSkill();
                        ojobSkill.jobID = jobID;
                        ojobSkill.skillsID = checkValuExist(s);
                        db.jobSkills.Add(ojobSkill);
                        db.SaveChanges();
                    }
                }
            }
        }

        public static int checkValuExist(string skill_)
        {

            var result = db.skills.Where(e => e.skillName == skill_).Select(x => new { x.skillsID }).SingleOrDefault();
            if (result == null)
            {
                skill oskill = new skill();
                oskill.skillName = skill_;
                db.skills.Add(oskill);
                db.SaveChanges();
                return oskill.skillsID;
            }
            else
            {
                return result.skillsID;
            }
        }

        // Central Method for sending emails using send grid 

        public static string sendEmailer(string ToEmail, string Mail_Subject, string HTML_Body, string attachment)
        {
            string result = "no";
            var apiKey = Environment.GetEnvironmentVariable("Auxo_SendGrid", EnvironmentVariableTarget.User);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("info@qendidate.com", "Qendidate");
            var to = new EmailAddress(ToEmail);
            var plainTextContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, Mail_Subject, plainTextContent, HTML_Body);
            try { var response = client.SendEmailAsync(msg);
                result= "ok";
            }
            catch(Exception e)
            {
                result = "no";
            }
            HTML_Body = null;
            return result;
        }

        // Central Method for string encryption
        public static string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (System.Security.Cryptography.Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        // Central Method for string decryption
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }

    public class HtmlHelperMultiSelect
    {
        public HtmlHelperMultiSelect()
        {
        }
        public HtmlHelperMultiSelect(string Id)
        {
            this.Id = Id;
        }
        public HtmlHelperMultiSelect(string Id, IList<SelectListItem> list, BaseValidation baseValidation)
        {
            this.Id = Id;
            this.List = list;
            this.BaseValidation = baseValidation;
        }
        public string Id { get; set; }
        public IList<SelectListItem> List { get; set; }
        public BaseValidation BaseValidation { get; set; }
        public string SelectedValues { get; set; }
        public string Caption { get; set; }
    }
    public class BaseValidation
    {
        public BaseValidation()
        {

        }
        public BaseValidation(bool isRequired, string requiredText, string validationId = null)
        {
            this.IsRequired = isRequired;
            this.RequiredText = requiredText;
            this.ValidationId = validationId;
        }
        public bool IsRequired { get; set; }
        public string ValidationId { get; set; }
        public string RequiredText { get; set; }
        public HtmlString GetValidationString()
        {
            var vs = "";
            if (this.ValidationId != null)
            {
                string ret = " " + BaseConst.VALIDATION_ISREQUIRED + "='{0}' " + BaseConst.VALIDATION_REQ_MSG + "='{1}' " + BaseConst.VALIDATION_ID + "='{2}' ";
                vs = string.Format(ret, Convert.ToString(this.IsRequired).ToLower(), this.RequiredText, this.ValidationId);
            }
            else
            {
                string ret = " " + BaseConst.VALIDATION_ISREQUIRED + "='{0}' " + BaseConst.VALIDATION_REQ_MSG + "='{1}' ";
                vs = string.Format(ret, Convert.ToString(this.IsRequired).ToLower(), this.RequiredText);
            }
            return new HtmlString(vs);
        }
    }
    public static class BaseConst
    {
        public const string VALIDATION_ISREQUIRED = "data-valc-isrequired";
        public const string VALIDATION_REQ_MSG = "data-valc-required-msg";
        public const string VALIDATION_ID = "data-valc-validation-id";

    }

    public static class SessionUtil
    {
        public static int GetUserID()
        {
            return Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
        }
        public static string GetLoginID()
        {
            return BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
        }

    }



    #region WebGrid
    //public static class WebGridBase
    //{
    //    public static WebGrid Init(ParmInGridInit parmIn)
    //    {
    //        WebGrid grid = new WebGrid(null, canPage: parmIn.CanPage, rowsPerPage: parmIn.RowPerPage,
    //        selectionFieldName: "selectedRow", ajaxUpdateContainerId: parmIn.AjaxContainerID);
    //        grid.Pager(WebGridPagerModes.FirstLast);
    //        grid.Bind(parmIn.Source);
    //        return grid;
    //    }
    //    public static IHtmlString GetWebGridHtml(WebGrid grid, WebGridColumn[] columns = null)
    //    {
    //        return grid.GetHtml(tableStyle: "table table-bordered",
    //        headerStyle: "DataGridHeader",
    //        alternatingRowStyle: "DataGridrowb",
    //        rowStyle: "DataGridrowa",
    //        selectedRowStyle: "DataGridHeader",
    //        mode: WebGridPagerModes.FirstLast | WebGridPagerModes.NextPrevious | WebGridPagerModes.Numeric,
    //        firstText: "First", lastText: "Last",
    //        previousText: "Prev", nextText: "Next",
    //        numericLinksCount: 10,
    //        columns: columns);
    //    }
    //}

    //public class ParmInGridInit
    //{
    //    public string AjaxContainerID { get; set; }
    //    public IEnumerable<dynamic> Source { get; set; }
    //    public int RowPerPage { get; set; }
    //    public bool CanPage { get; set; }
    //    public ParmInGridInit()
    //    {
    //        RowPerPage = 50;
    //        CanPage = true;
    //    }
    //}
    #endregion
}
