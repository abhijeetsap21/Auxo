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
    public  class SlotsController : BaseClass
    {
        private IUnitOfWork uow = null;
        private Slots repo = null;
        public SlotsController()
        {
            uow = new UnitOfWork();
            repo = new Slots(uow);
        }

        // GET: Admin/Slots
        public ActionResult Index()
        {            
            var status = repo.SQLQuery<sp_slotlist_Result>("sp_slotlist").ToList();
            return View(status);                       
        }

        // GET: Admin/Slots/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/Slots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "slotTime,isActive")] slot Slot)
        {
            if (ModelState.IsValid)
            {
                Slot.createdBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()))); 
                Slot.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                Slot.dataIsCreated = BaseUtil.GetCurrentDateTime();
                Slot.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                repo.Insert(Slot);
                return RedirectToAction("Index");
            }

            return View(Slot);
        }

        // GET: Admin/Slot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slot slots = repo.Single(id);
            if (slots == null)
            {
                return HttpNotFound();
            }
            return View(slots);
        }


        // POST: Admin/Slot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotID,slotTime,isActive")] slot slots)
        {
            var s = repo.Single(slots.SlotID);
            if (s == null)
            {
                return HttpNotFound();
            }
            s.slotTime = slots.slotTime;
            s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            s.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
            s.isActive = slots.isActive;
            if (ModelState.IsValid)
            {
                slots.modifiedBy = Convert.ToInt64((BaseUtil.GetSessionValue(AdminInfo.employerID.ToString())));
                slots.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                repo.Update(s);
                return RedirectToAction("Index");
            }
            return View(slots);
        }
    }
}
