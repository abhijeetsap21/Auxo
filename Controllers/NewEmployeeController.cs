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
using System.IO;

namespace NewLetter.Controllers
{
    public class NewEmployeeController : BaseClass
    {
        private oriondbEntities db = new oriondbEntities();

        // GET: NewEmployee
        public async Task<ActionResult> Index()
        {
            Int64 companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            var employerDetails = db.EmployerDetails.Where(e=>e.companyID== companyID);
            return View(await employerDetails.ToListAsync());
        }

        // GET: NewEmployee/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployerDetail employerDetail = await db.EmployerDetails.FindAsync(id);
            if (employerDetail == null)
            {
                return HttpNotFound();
            }
            var role_= db.roles.Where(e => e.roleID == employerDetail.roleID).Select(e => new { e.role1 }).FirstOrDefault();
            ViewBag.role = role_.role1.ToString();
            return View(employerDetail);
        }

        // GET: NewEmployee/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: NewEmployee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeDetailValidation oEmployeeDetailValidation)
        {
            EmployerDetail employerDetail = new EmployerDetail();
            employerDetail.Name = oEmployeeDetailValidation.Name;
                employerDetail.Email = oEmployeeDetailValidation.Email;
            employerDetail.Mobile = oEmployeeDetailValidation.Mobile;
            employerDetail.isActive = false;
            employerDetail.isDelete = false;
            employerDetail.dataIsCreated = BaseUtil.GetCurrentDateTime();
            employerDetail.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            Int64 roleid=Convert.ToInt64( BaseUtil.GetSessionValue(AdminInfo.role_id.ToString()));
            employerDetail.password= baseClass.GetRandomPasswordString(10);
            employerDetail.companyID= Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            if (roleid == 2)
            {
                employerDetail.roleID = 3;
            }
            if (roleid ==1)
            {
                employerDetail.roleID = 4;
            }

            if (ModelState.IsValid)
            {
                db.EmployerDetails.Add(employerDetail);
                await db.SaveChangesAsync();
                var encryptedID = BaseUtil.encrypt(employerDetail.EmployerID.ToString());

                StreamReader sr = new StreamReader(Server.MapPath("/Emailer/toEmployerRegistrationSuccess.html"));

                string HTML_Body = sr.ReadToEnd();
                string newString = HTML_Body.Replace("#name", employerDetail.Name).Replace("#EMPID", encryptedID).Replace("#password", employerDetail.password);
                sr.Close();
                string To = employerDetail.Email.ToString();
                string mail_Subject = "Employer Registration Confirmation ";
                profileController objprofileController = new profileController();
                BaseUtil.sendEmailer(To, mail_Subject, newString, "");
                return RedirectToAction("Index");
            }

          
            return View(employerDetail);
        }

        // GET: NewEmployee/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployerDetail employerDetail = await db.EmployerDetails.FindAsync(id);
            if (employerDetail == null)
            {
                return HttpNotFound();
            }
         
            return View(employerDetail);
        }

        // POST: NewEmployee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployerDetail employerDetail)
        {
            employerDetail.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            if (ModelState.IsValid)
            {
                db.Entry(employerDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           
            return View(employerDetail);
        }

       
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
