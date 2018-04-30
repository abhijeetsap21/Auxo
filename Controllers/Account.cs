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
using SendGrid;
using SendGrid.Helpers.Mail;
using RestSharp;
using System.Web.Script.Serialization;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Collections.Specialized;
using Facebook;
using System.Web.Security;
using System.IO;

namespace NewLetter.Controllers
{

    public class AccountController : BaseClass
    {
        private oriondbEntities db = new oriondbEntities();

        // GET: Account


        public ActionResult login(int? val)
        {
            if (val == 1)
            {
                ViewBag.valMessage = "ok";
            }
            return View();            
        }
        

        public ActionResult Employerlogin(int? val)
        {
            if (val == 1)
            {
                ViewBag.valMessage = "ok";
            }
            return View();
        }
        //Login Employer
        [HttpGet]
        public ActionResult _Employerlogin(string qenid)
        {
           
            
            if (qenid != null)
            {
                var decryptValue = BaseUtil.Decrypt(qenid);
                int ID = Convert.ToInt32(decryptValue);
                ViewBag.qenid = ID != null ? ID : 0;
                //login ologin = new login();
                var result = db.EmployerDetails.Where(e => e.EmployerID == ID).FirstOrDefault();
                result.isActive = true;
                result.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["result"] = "Activated";
                // return View("_partialEmploerLogin");
                return RedirectToAction("Employerlogin");
            }
            else
            {
                int ID = Convert.ToInt32(qenid);
                ViewBag.qenid = ID != null ? ID : 0;               
                return View("_partialEmploerLogin");
            }
        }

        [HttpPost]       
        public ActionResult _Employerlogin(login ologin)
        {
            try
            {
                String LoginResult = BaseUtil.CheckUserFrofile(ologin.qenEmail, ologin.password, "login");
                if (LoginResult == "PASS")
                {
                    return RedirectToAction("jobDashboard", "jobDetails");
                }
                TempData["result"] = LoginResult;
            }
            catch (Exception c)
            {
                TempData["result"] = "Login failed";
            }
            return RedirectToAction("Employerlogin");
        }

        [HttpGet]
        public ActionResult partialEmployerReg()
        {
            empRegistration oempRegistration = new empRegistration();
            ViewBag.employerTypeID = new SelectList(db.employerTypes, "employerTypeID", "employerType1");
            return View("_partialEmploerReg", oempRegistration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> partialEmployerReg(empRegistration empRegistartion)
         {
            
            companyDetail cmpdetails = new companyDetail();
            EmployerDetail empdetails = new EmployerDetail();
            cmpdetails.companyName = empRegistartion.companyName;
            cmpdetails.city = " Not specified ";
            cmpdetails.state= " Not specified ";
            cmpdetails.country= " Not specified ";
            empdetails.Email = empRegistartion.Email;
            cmpdetails.website = empRegistartion.website;            
            cmpdetails.employerTypeID = empRegistartion.employerTypeID;
            cmpdetails.address = " Not specified ";
            cmpdetails.companyDescription = " Not specified ";
            cmpdetails.dataIsCreated = BaseUtil.GetCurrentDateTime(); 
            cmpdetails.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            cmpdetails.zipCode = "------";
            cmpdetails.companyIndustry = 0;      
            db.companyDetails.Add(cmpdetails);
            try
            {
                await db.SaveChangesAsync();
                empdetails.companyID = cmpdetails.companyID;
                empdetails.dataIsCreated = BaseUtil.GetCurrentDateTime();
                empdetails.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                empRegistartion.password = baseClass.GetRandomPasswordString(10);
                empdetails.Name = empRegistartion.companyName;
                empdetails.Mobile = empRegistartion.mobile;                
                empdetails.isActive = false;
                empdetails.isDelete = false;
                empdetails.roleID = 2;
                empdetails.password = empRegistartion.password;
                db.EmployerDetails.Add(empdetails);
                await db.SaveChangesAsync();
                empRegistartion.employerID = empdetails.EmployerID;
                var emailresult = db.EmployerDetails.Where(ex => ex.EmployerID == empRegistartion.employerID).FirstOrDefault();
                var encryptedID = BaseUtil.encrypt(emailresult.EmployerID.ToString());             

                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toEmployerRegistrationSuccess.html"));
                
                string HTML_Body = sr.ReadToEnd();
                string newString = HTML_Body.Replace("#name", emailresult.Name).Replace("#EMPID",encryptedID).Replace("#password", emailresult.EmployerDetail1.password);
                sr.Close();
                string To = emailresult.Email.ToString();
                string mail_Subject = "Employer Registration Confirmation ";
                profileController objprofileController = new profileController();
                BaseUtil.sendEmailer(To, mail_Subject, newString, "");                
                                
                TempData["result"] = "Registred";                
            }
            catch (DbEntityValidationException ex)
            {
                TempData["result"] = "Registration failed.";
            }
            ViewBag.employerTypeID = new SelectList(db.employerTypes, "employerTypeID", "employerType1");
            ViewBag.companyIndustry = new SelectList(db.industries, "industryID", "industryName");
            return RedirectToAction("Employerlogin");           
        }


        //Candidate Login
        [HttpGet]
        public ActionResult logincandidate(string qenid)
        {
            if (qenid != null)
            {
                var decryptValue = BaseUtil.Decrypt(qenid);
                int ID = Convert.ToInt32(decryptValue);
                ViewBag.qenid = ID != null ? ID : 0;
                //login ologin = new login();
                var result = db.qendidateLists.Where(e => e.qenID == ID).FirstOrDefault();
                result.isActive = true;
                result.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["result"] = "Activated";
                return RedirectToAction("login");
            }
            else
            {
                int ID = Convert.ToInt32(qenid);
                ViewBag.qenid = ID != null ? ID : 0;
                return View("_PartialLoginCandidate");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logincandidate(login ologin)
        {
            try
            {                
                String LoginResult = BaseUtil.CheckUserFrofile(ologin.qenEmail, ologin.password, "logincandidate");

                if (LoginResult == "PASS")
                {                                     
                    return RedirectToAction("jobs", "jobDetails");
                }
                TempData["result"] = LoginResult;
            }
            catch (Exception c)
            {
                TempData["result"] = "Login failed";
            }
            return RedirectToAction("login");
        }

        public ActionResult Logout()
        {
            BaseUtil.accesiblePages = null; 
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult _partialQenForgotPassword()
        {
            qenforgotPassword qenforgotPassword = new qenforgotPassword();
            return PartialView("_partialQenForgotPassword");
        }

        public string qforgotPassword(string email)
        {
            qendidateList qenList = new qendidateList();
            //await db.SaveChangesAsync();
            qenList = db.qendidateLists.Where(e => e.qenEmail == email).FirstOrDefault();
            if (qenList != null)
            {
                //----------------------------use below code to send emailer------------------------------------------------------------

                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/ForgetPassword.html"));
                string HTML_Body = sr.ReadToEnd();
                string final_Html_Body = HTML_Body.Replace("#name", qenList.qenName).Replace("#REFID",BaseUtil.encrypt( qenList.qenID.ToString())).Replace("#role", BaseUtil.encrypt("5"));
                sr.Close();
                string To = email.ToString();
                string mail_Subject = "Reset password request recieved";               
                string  result= BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                //----------------------------end to send emailer------------------------------------------------------------
                if (result == "ok")
                {
                    //ViewBag.success = "ok";
                }
                return "yes";
            }
            else
            {
                //ViewBag.message = "User not Found ";
                return "no";
            }
        }

       
        // For got Password Employer

        [HttpGet]
        public ActionResult _partailEforgotPassword()
        {
            eforgotPassword eforgotPassword = new eforgotPassword();
            return PartialView("_partailEforgotPassword",eforgotPassword);
        }

        public  string EforgotPassword(string email)
        {
            EmployerDetail empdetail = new EmployerDetail();
            //await db.SaveChangesAsync();
            empdetail = db.EmployerDetails.Where(e => e.Email == email).FirstOrDefault();

            if (empdetail != null)
            {
                //----------------------------use below code to send emailer------------------------------------------------------------

                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/ForgetPassword.html"));
                string HTML_Body = sr.ReadToEnd();
                string final_Html_Body = HTML_Body.Replace("#name", empdetail.Name).Replace("#REFID", BaseUtil.encrypt( empdetail.EmployerID.ToString())).Replace("#role", BaseUtil.encrypt("2"));
                sr.Close();
                string To = email.ToString();
                string mail_Subject = "Reset password request recieved";


                string result = BaseUtil.sendEmailer(To, mail_Subject, final_Html_Body, "");
                //----------------------------end to send emailer------------------------------------------------------------
                if (result == "ok")
                {
                    //ViewBag.success = "ok";
                }
                //return View("Employerlogin");
                return "Yes";
            }
            else
            {
                //ViewBag.message = "User not Found ";
                return "No";
                //return View("_partailEforgotPassword");
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Linked in Plugin

        public ActionResult linkdInReg(string code, string state)
        {
            var ID = "";
            try
            {
                //Get Accedd Token
                var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken");
                var request = new RestRequest(Method.POST);
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", code);
                request.AddParameter("redirect_uri", "http://localhost:51126/Account/linkdInReg");
                request.AddParameter("client_id", "8183ea15suyzy4");
                request.AddParameter("client_secret", "Bm4AevVeyg5E0ZxI");
                //request.AddParameter("scope", "r_emailaddress");

                IRestResponse response = client.Execute(request);
                var content = response.Content;
                //Fetch AccessToken
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                LinkedINVM linkedINVM = jsonSerializer.Deserialize<LinkedINVM>(content);

                //Get Profile Details
                client = new RestClient("https://api.linkedin.com/v1/people/~:(id,first-name,last-name,headline,summary,picture-url,positions,location,public-profile-url,email-address)?oauth2_access_token=" + linkedINVM.access_token + "&format=json");
                request = new RestRequest(Method.GET);
                response = client.Execute(request);
                content = response.Content;


                jsonSerializer = new JavaScriptSerializer();
                LinkedINResVM linkedINResVM = jsonSerializer.Deserialize<LinkedINResVM>(content);
                //linkedINResVM.emailaddress= content.
                //return RedirectToAction("login");
                var result = BaseUtil.checkSocialProfile(linkedINResVM.emailaddress);
                if (result == "NotExists")
                {
                    qendidateList list = new qendidateList();
                    list.qenName = linkedINResVM.firstName + " " + linkedINResVM.lastName;
                    list.qenEmail = linkedINResVM.emailaddress;
                    list.qenLinkdInUrl = linkedINResVM.publicprofileurl;
                    list.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    list.dataIsUpdated = BaseUtil.GetCurrentDateTime();

                    list.roleID = 5;
                    list.isDelete = false;
                    list.isActive = true;
                    list.password = baseClass.GetRandomPasswordString(10);
                    list.qenImage = linkedINResVM.pictureurl;
                    list.qenPhone = "0000000000";
                    list.qenAddress = "some address";
                    list.qenAddress = null;
                    list.socialCheck = true;
                    list.CareerObjective = linkedINResVM.summary;
                    db.qendidateLists.Add(list);
                    db.SaveChanges();

                    StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateRegistrationSuccess.html"));
                    string HTML_Body = sr.ReadToEnd();
                    string newString = HTML_Body.Replace("#name", list.qenName).Replace("#password", list.password);
                    sr.Close();
                    string To = list.qenEmail.ToString();
                    string mail_Subject = "Candidate Registration Confirmation ";
                    profileController objprofileController = new profileController();
                    BaseUtil.sendEmailer(To, mail_Subject, newString, "");


                    BaseUtil.SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(list.qenID));
                    //BaseUtil.SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.qenPhone));
                    BaseUtil.SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(list.roleID));
                    BaseUtil.SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(list.qenName));
                    ID = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());
                }
                else if (result == "Exists")
                {
                    var user = db.qendidateLists.Where(u => u.qenEmail == linkedINResVM.emailaddress).FirstOrDefault();
                    user.socialCheck = true;
                    user.CareerObjective = linkedINResVM.summary;
                    user.qenLinkdInUrl = linkedINResVM.publicprofileurl;
                    user.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    user.qenName = linkedINResVM.firstName + " " + linkedINResVM.lastName;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    BaseUtil.SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(user.qenID));
                    //BaseUtil.SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.qenPhone));
                    BaseUtil.SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(user.roleID));
                    BaseUtil.SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(user.qenName));                     
                    ID = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());
                    TempData["Success"] = "Linked";
                }

                return RedirectToAction("jobs", "jobDetails", new { ID = ID });
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        //Google+ login

        public ActionResult googleReg()
        {
            string provider = "google";
            string returnUrl = "http://localhost:51126/Account/ExternalLoginCallback";
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));

        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var ID = "";
            string ProviderName = OpenAuth.GetProviderNameFromCurrentRequest();

            if (ProviderName == null || ProviderName == "")
            {
                System.Collections.Specialized.NameValueCollection nvs = Request.QueryString;
                if (nvs.Count > 0)
                {
                    if (nvs["state"] != null)
                    {
                        NameValueCollection provideritem = HttpUtility.ParseQueryString(nvs["state"]);
                        if (provideritem["__provider__"] != null)
                        {
                            ProviderName = provideritem["__provider__"];
                        }
                    }
                }
            }

            DotNetOpenAuth.GoogleOAuth2.GoogleOAuth2Client.RewriteRequest();
            var redirectUrl = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl });
            var retUrl = returnUrl;
            var authResult = OpenAuth.VerifyAuthentication(redirectUrl);


            //string ProviderDisplayName = OpenAuth.GetProviderDisplayName(ProviderName);

            if (!authResult.IsSuccessful)
            {
                return Redirect(Url.Action("login", "Account"));
            }

            else
            {

                qendidateList gmailUser = new qendidateList();
                string ProviderUser = authResult.ProviderUserId;
                string ProviderUName = authResult.UserName;
                string UEmail = null;
                if (UEmail == null && authResult.ExtraData.ContainsKey("email"))
                {
                    UEmail = authResult.ExtraData["email"];
                }
                var result = BaseUtil.checkSocialProfile(UEmail);
                if (result == "NotExists")
                {

                    gmailUser.qenName = ProviderUName;
                    gmailUser.qenEmail = UEmail;
                    gmailUser.qenLinkdInUrl = "www.someurl.com";
                    gmailUser.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    gmailUser.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    gmailUser.roleID = 5;
                    gmailUser.isDelete = false;
                    gmailUser.isActive = true;
                    gmailUser.password = baseClass.GetRandomPasswordString(10);
                    //gmailUser.qenImage = linkedINResVM.pictureurl;
                    gmailUser.qenPhone = "0000000000";
                    gmailUser.qenAddress = "some address";
                    gmailUser.qenAddress = null;
                    db.qendidateLists.Add(gmailUser);
                    db.SaveChanges();

                    //----------------------------use below code to send emailer------------------------------------------------------------

                    StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateRegistrationSuccess.html"));
                    string HTML_Body = sr.ReadToEnd();
                    string newString = HTML_Body.Replace("#name", gmailUser.qenName).Replace("#password", gmailUser.password);
                    sr.Close();
                    string To = gmailUser.qenEmail.ToString();
                    string mail_Subject = "Candidate Registration Confirmation ";
                    profileController objprofileController = new profileController();
                    BaseUtil.sendEmailer(To, mail_Subject, newString, "");
                    //----------------------------end to send emailer------------------------------------------------------------

                    BaseUtil.SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(gmailUser.qenID));
                    //BaseUtil.SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.qenPhone));
                    BaseUtil.SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(gmailUser.roleID));
                    BaseUtil.SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(gmailUser.qenName));
                    ID = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());
                }
                else if (result == "Exists")
                {
                    var user = db.qendidateLists.Where(u => u.qenEmail == UEmail).FirstOrDefault();

                    BaseUtil.SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(user.qenID));
                    //BaseUtil.SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.qenPhone));
                    BaseUtil.SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(user.roleID));
                    BaseUtil.SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(user.qenName));
                    ID = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());
                }
            }
            return RedirectToAction("jobs", "jobDetails", new { ID = ID });
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OpenAuth.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        // Facebook Login

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1484503554920573",
                client_secret = "dbe3b963bd866da2f3cdb22848774411",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            var ID = "";
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "1484503554920573",
                client_secret = "dbe3b963bd866da2f3cdb22848774411",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information, like email, first name, middle name etc
            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string firstname = me.first_name;
            string middlename = me.middle_name;
            string lastname = me.last_name;

            var sresult = BaseUtil.checkSocialProfile(email);
            if (sresult == "NotExists")
            {
                qendidateList list = new qendidateList();
                list.qenName = firstname + " " + lastname;
                list.qenEmail = email;
                //list.qenLinkdInUrl = linkedINResVM.publicprofileurl;
                list.dataIsCreated = BaseUtil.GetCurrentDateTime();
                list.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                list.roleID = 5;
                list.isDelete = false;
                list.isActive = false;
                list.password = baseClass.GetRandomPasswordString(10);
                //list.qenImage = linkedINResVM.pictureurl;
                list.qenPhone = "0000000000";
                list.qenAddress = "some address";
                list.qenAddress = null;
                db.qendidateLists.Add(list);
                db.SaveChanges();

                BaseUtil.SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(list.qenID));
                //BaseUtil.SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.qenPhone));
                BaseUtil.SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(list.roleID));
                BaseUtil.SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(list.qenName));
                ID = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());

                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateRegistrationSuccess.html"));
                string HTML_Body = sr.ReadToEnd();
                string newString = HTML_Body.Replace("#name", list.qenName).Replace("#password", list.password);
                sr.Close();
                string To = list.qenEmail.ToString();
                string mail_Subject = "Candidate Registration Confirmation ";
                profileController objprofileController = new profileController();
                BaseUtil.sendEmailer(To, mail_Subject, newString, "");
            }
            else if (sresult == "Exists")
            {
                var user = db.qendidateLists.Where(u => u.qenEmail == email).FirstOrDefault();

                BaseUtil.SetSessionValue(AdminInfo.UserID.ToString(), Convert.ToString(user.qenID));
                //BaseUtil.SetSessionValue(AdminInfo.Mobile.ToString(), Convert.ToString(user.qenPhone));
                BaseUtil.SetSessionValue(AdminInfo.role_id.ToString(), Convert.ToString(user.roleID));
                BaseUtil.SetSessionValue(AdminInfo.FullName.ToString(), Convert.ToString(user.qenName));
                ID = BaseUtil.GetSessionValue(AdminInfo.UserID.ToString());
            }
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("jobs", "jobDetails", new { ID = ID });

            // Set the auth cookie


        }
        [HttpGet]
        public ActionResult candidateReg()
        {
            candidateRegistration canreg = new candidateRegistration();

            return View("_partialCandidateReg", canreg);

        }

        [HttpPost]
        public  ActionResult candidateReg(candidateRegistration candidateReg)
        {
            //candidateRegistration data = new candidateRegistration();
            qendidateList qenlist = new qendidateList();

            qenlist.qenName = candidateReg.candidateName;
            qenlist.qenEmail = candidateReg.Email;
            qenlist.qenPhone = candidateReg.candidatePhone;
            qenlist.qenAddress = "some address";            
            qenlist.roleID = 5;
            qenlist.isActive = false;
            qenlist.isDelete = false;
            qenlist.password = baseClass.GetRandomPasswordString(10);
            candidateReg.password = qenlist.password;
            qenlist.dataIsCreated = BaseUtil.GetCurrentDateTime();
            qenlist.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            db.qendidateLists.Add(qenlist);
            try
            {
                db.SaveChanges();
                candidateReg.candidateID = qenlist.qenID;
            }
            catch (DbEntityValidationException ex)
            {
                TempData["result"] = "Registration failed.";
            }
            var emailresult = db.qendidateLists.Where(ex => ex.qenID == candidateReg.candidateID).FirstOrDefault();
            var encryptedID = BaseUtil.encrypt(emailresult.qenID.ToString());

            //----------------------------use below code to send emailer------------------------------------------------------------

            StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toCandidateRegistrationSuccess.html"));
            string HTML_Body = sr.ReadToEnd();
            string newString = HTML_Body.Replace("#name", emailresult.qenName).Replace("#qenid", encryptedID).Replace("#password", emailresult.password);
            sr.Close();
            string To = emailresult.qenEmail.ToString();
            string mail_Subject = "Candidate Registration Confirmation ";
            profileController objprofileController = new profileController();
            BaseUtil.sendEmailer(To, mail_Subject, newString, "");
            //----------------------------end to send emailer------------------------------------------------------------

            TempData["result"] = "Registred";
            return RedirectToAction("login");

        }
       
        
        [HttpGet]
        public ActionResult NewPassword()
        {
            int role = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["role"]))
            {
                role = Convert.ToInt16(BaseUtil.Decrypt( Request.QueryString["role"].ToString()));
            }
            long UID = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["UID"]))
            {
                UID = Convert.ToInt64(BaseUtil.Decrypt(Request.QueryString["UID"].ToString()));
            }
            CreateNewPassword oCreateNewPassword = new CreateNewPassword();
            oCreateNewPassword.userID = UID;
            oCreateNewPassword.roleID = role;
            return View(oCreateNewPassword);
        }
        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult NewPassword(CreateNewPassword oCreateNewPassword)
        {
            long userID = oCreateNewPassword.userID;
            if (userID != 0) { 
            //---------check for Employer-----------
            if (oCreateNewPassword.roleID == 2)
            {
                    var emplyoer = db.EmployerDetails.Where(e => e.EmployerID == userID).FirstOrDefault();
                    if (emplyoer != null)
                    {
                        emplyoer.password = oCreateNewPassword.password;
                        db.Entry(emplyoer).State= EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.success = 2;
                    }

            }

                //---------check for candidate-----------
                if (oCreateNewPassword.roleID == 5)
                {
                    var emplyoer = db.qendidateLists.Where(e => e.qenID == userID).FirstOrDefault();
                    if (emplyoer != null)
                    {
                        emplyoer.password = oCreateNewPassword.password;
                        db.Entry(emplyoer).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.success = 5;
                    }
                }
            }
            return View();
        }

        public JsonResult GetNotificationContacts()
        {
            long QenID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            var dt_ = db.qendidateLists.Where(e => e.qenID == QenID).Select(e => new { e.lastLoginTime }).FirstOrDefault();
            DateTime dt = Convert.ToDateTime(dt_.lastLoginTime);
            var list = getNotifyForInterested(dt);
            //update session here for get only new added contacts (notification)
            Session["LastUpdate"] = DateTime.Now;
            var s= Json(list, JsonRequestBehavior.AllowGet);
            return s;
           
        }
        public List<qenMialSendInterested_> getNotifyForInterested(DateTime afterDate)
        {
            long QenID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            List<qenMialSendInterested_> oqenMialSendInterested_ = new List<qenMialSendInterested_>();
            qenMialSendInterested_ oqenMialSendInterested = new qenMialSendInterested_();
            var a = db.qenMialSendInteresteds.Where(e => e.dataIsCreated > afterDate && e.qenID== QenID).Select(e => new { e.qenID, e.jobID }).ToList();
            foreach (var e in a)
            {
                oqenMialSendInterested.qenID = e.qenID;
                oqenMialSendInterested.jobID = e.jobID;
                var CompanyID_ = db.jobDetails.Where(e1 => e1.jobID == e1.jobID).Select(e1=>new { e1.companyID}).FirstOrDefault();
                var companyName=db.companyDetails.Where(e1 => e1.companyID == CompanyID_.companyID).Select(e1 => new { e1.companyName }).FirstOrDefault();
                oqenMialSendInterested.companyName = companyName.companyName;
                oqenMialSendInterested_.Add(oqenMialSendInterested);
            }
            return oqenMialSendInterested_;
        }


        public JsonResult getNotificationDownload() {
            long QenID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            var d = db.sp_getNotificationDownload(QenID).ToList();
            return Json(d, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult getNotificationView()
        {
            long QenID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            var d = db.sp_getNotificationDownload(QenID).ToList();
            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult isUserNameAvialable(string Email)
        {
            return Json(!db.EmployerDetails.Any(UserInfo => UserInfo.Email == Email), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult isCandidateAvialable(string Email)
        {
            return Json(!db.qendidateLists.Any(UserInfo => UserInfo.qenEmail == Email), JsonRequestBehavior.AllowGet);
        }
        public JsonResult NotificationCount()
        {
            long QenID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            var result = Json(db.sp_getNotificationCount(QenID), JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}