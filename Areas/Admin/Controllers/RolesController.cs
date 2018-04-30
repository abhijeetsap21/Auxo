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
using NewLetter.Areas.Admin.Models;

namespace NewLetter.Areas.Admin.Controllers
{
    public class RolesController : BaseClass
    {        

        private IUnitOfWork uow = null;
        private Roles repo = null;

        public RolesController()
        {
            uow = new UnitOfWork();
            repo = new Roles(uow);
        }

        // GET: Admin/Roles
        public ActionResult Index()

        {
            //var roles = repo.GetAll(null, null, "EmployerDetail,EmployerDetail1");
            var roles = repo.SQLQuery<sp_roleList_Result>("sp_roleList").ToList();
            return View(roles.ToList());
        }    
      

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "role1")] role role)
        {
            if (ModelState.IsValid)
            {
                role.dataIsCreated = BaseUtil.GetCurrentDateTime();
                role.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                role.createdBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                role.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                repo.Insert(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var role = repo.Single(id);
            if (role == null)
            {
                return HttpNotFound();
            }
                        
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleID,role1")] role role)
        {
            var r = repo.Single(role.roleID);
            if (r == null)
            {
                return HttpNotFound();
            }
            r.role1 = role.role1;
            r.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            r.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));

            if (ModelState.IsValid)
            {
                repo.Update(r);
            }
                        
            return View(role);
        }   
               
    }
}
