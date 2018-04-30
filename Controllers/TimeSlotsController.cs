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

namespace NewLetter.Controllers
{
    public class TimeSlotsController : BaseClass
    {
        private oriondbEntities db = new oriondbEntities();

        // GET: TimeSlots
        public async Task<ActionResult> Index()
        {
            return View(await db.slots.ToListAsync());
        }
       

        // GET: TimeSlots/Create
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SlotID,slotTime")] slot slot)
        {
            if (ModelState.IsValid)
            {
                db.slots.Add(slot);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(slot);
        }

        // GET: TimeSlots/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slot slot = await db.slots.FindAsync(id);
            if (slot == null)
            {
                return HttpNotFound();
            }
            return View(slot);
        }

        // POST: TimeSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SlotID,slotTime")] slot slot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slot);
        }


        public ActionResult BlockSlots()
        {
            long CompanyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            var blockedSlots = db.slotsBlockeds.Include(e=>e.EmployerDetail).Include(e=>e.slot).Where(e => e.companyID == CompanyID && e.isDeleted==false ).OrderBy(e=>e.SlotID).ToList();

          List<Blockslots> oBlockslotsList= new List<Blockslots>();
            Blockslots oBlockslots;
            foreach (var slot in blockedSlots)
            {
                oBlockslots = new Blockslots();
                oBlockslots.blocked = true;
                oBlockslots.slotID = slot.SlotID;
                oBlockslots.slotTime = slot.slot.slotTime;
                oBlockslots.updated_BY = slot.EmployerDetail.Name;
                oBlockslots.updated_date = slot.dataIsUpdated;
                oBlockslotsList.Add(oBlockslots);

            }

            var FreshSlots = db.slots.ToList();
            foreach (var slot in FreshSlots)
            {
                var result = oBlockslotsList.Find(e => e.slotID == slot.SlotID);
                if (result==null)
                {
                    oBlockslots = new Blockslots();
                    oBlockslots.blocked = false;
                    oBlockslots.slotID = slot.SlotID;
                    oBlockslots.slotTime = slot.slotTime;                 
                   
                    oBlockslotsList.Add(oBlockslots);
                }
               
            }

            return View(oBlockslotsList);
        }
        [HttpGet]
        public string BlockUnblockSlot(int slotID, bool chk)
        {
            string result = "no";
            long CompanyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));

            var slotExists = db.slotsBlockeds.Where(e => e.SlotID == slotID && e.companyID == CompanyID).FirstOrDefault();
            if (slotExists == null)
            {

                slotsBlocked oslotsBlockeds = new slotsBlocked();
                oslotsBlockeds.companyID = CompanyID;
                oslotsBlockeds.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                oslotsBlockeds.EmployerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                oslotsBlockeds.isDeleted = false;
                oslotsBlockeds.modifyBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                oslotsBlockeds.SlotID = slotID;
                oslotsBlockeds.dataIsCreated = BaseUtil.GetCurrentDateTime();                
                db.slotsBlockeds.Add(oslotsBlockeds);
                try
                {
                    db.SaveChanges();
                    result = "OK";
                    return result;
                }
                catch (Exception e)
                {
                    return result;
                }
            }
            else {
                slotExists.EmployerID= Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                if (chk)
                {
                    slotExists.isDeleted = false;
                }
                else {
                    slotExists.isDeleted = true;
                }
                slotExists.modifyBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                slotExists.dataIsCreated = BaseUtil.GetCurrentDateTime();
                db.Entry(slotExists).State = EntityState.Modified;
                db.SaveChanges();
                result = "OK";
                return result;
            }
        }

        [HttpGet]
        public ActionResult tempBlockSlots()
        {
           ViewBag.mySkills = new SelectList(db.slots.OrderBy(e => e.SlotID), "SlotID", "slotTime");
            return View();
        }
        [HttpPost]
        public ActionResult tempBlockSlots(FormCollection frm)
        {
            string items = frm["mySkillsValues"].ToString();
            slotTempBlocked oslotTempBlocked;
            string[] i1;          
            if (items != null)
            {
                i1 = items.Split(new[] {   ","  }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < i1.Length; i++)
                {
                    string slotID = i1[i]; /*Inside string type s variable should contain items values */
                    oslotTempBlocked = new slotTempBlocked();
                    oslotTempBlocked.companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
                    oslotTempBlocked.EmployerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                    oslotTempBlocked.SlotID =Convert.ToInt32( slotID);
                    oslotTempBlocked.modifyBy= Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
                    oslotTempBlocked.isDeleted = false;
                    oslotTempBlocked.forDate = Convert.ToDateTime(frm["sdate"]);
                    oslotTempBlocked.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    oslotTempBlocked.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    db.slotTempBlockeds.Add(oslotTempBlocked);
                    db.SaveChanges();
                }
            }
           
            
            return View();
        }

        //[HttpGet]
        //public ActionResult SlotsInformation()
        //{
        //    DateTime dt = BaseUtil.GetCurrentDateTime();
        //    ViewBag.dAt = dt.ToString().Substring(0, 10);
        //    long companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
        //    string query = "select slotID, slotTime, 'Available' as blocked ,'' as 'qenID' from slots where " +
        //                 " slotID not in (select slotID from slotsBlocked where companyID = '" + companyID + "' AND isDeleted = 0 ) AND " +
        //                 " slotID not in (SELECT slotID FROM qenInterviewSchedule where companyID = '" + companyID + "'  AND slotID is not null AND convert(date, meetScheduledDateTime)= convert(date, '" + dt + "') ) AND " +
        //                  " slotID not in (SELECT slotID FROM slotTempBlocked where companyID = '" + companyID + "'  AND forDate = convert(date, '" + dt + "') AND isDeleted = 0 )" +
        //                  " union " +
        //                  " select slotTempBlocked.slotID , slotTime,'booked by HR' as blocked, '' as 'qenID' from slotTempBlocked left outer join slots on slotTempBlocked.slotID = slots.slotID where companyID = '" + companyID + "'  AND isDeleted = 0 AND forDate = convert(date, '" + dt + "') " +
        //                  " union " +
        //                  " select slotsBlocked.slotID , slotTime,'blocked by HR' as blocked, '' as 'qenID' from slotsBlocked left outer join slots on slotsBlocked.slotID = slots.slotID where companyID = '" + companyID + "'  AND isDeleted = 0 " +
        //                  "  union " +
        //                  " SELECT qenInterviewSchedule.slotID, slotTime,qenName as blocked,  qendidateList.qenID as 'qenID' FROM qenInterviewSchedule left outer join qendidateList " +
        //                  " on qenInterviewSchedule.qenID = qendidateList.qenID left outer join slots on qenInterviewSchedule.slotID = slots.slotID where qenInterviewSchedule.companyID = '" + companyID + "'  AND qenInterviewSchedule.slotID is not null AND convert(date, meetScheduledDateTime)= convert(date, '" + dt + "')";
        //    var SlotList = db.Database.SqlQuery<slotsInfo>(query).ToList();
        //    ViewBag.dAt = "";
        //    return View(SlotList);
        //}
       // [HttpPost]

        public ActionResult SlotsInformation(FormCollection frm, string dat)
        {
            DateTime dt = BaseUtil.GetCurrentDateTime();
            if (frm.Count > 0)
            {
                if (frm["txt_date"].ToString() != "")
                {
                    dt = Convert.ToDateTime(frm["txt_date"].ToString());
                    ViewBag.dAt = dt.ToString().Substring(0, 10);
                }
                
            }
            if (dat != null)
                if (dat != "")
                {
                    dt = Convert.ToDateTime(dat);
                    ViewBag.dAt = dt.ToString().Substring(0, 10);
                }
            long companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            string query = "select slotID, slotTime, 'Available' as blocked ,'' as 'qenID' ,''as jobID from slots where " +
                         " slotID not in (select slotID from slotsBlocked where companyID = '" + companyID + "' AND isDeleted = 0 ) AND " +
                         " slotID not in (SELECT slotID FROM qenInterviewSchedule where companyID = '" + companyID + "'  AND slotID is not null AND convert(date, meetScheduledDateTime)= convert(date, '" + dt + "') ) AND " +
                          " slotID not in (SELECT slotID FROM slotTempBlocked where companyID = '" + companyID + "'  AND forDate = convert(date, '" + dt + "') AND isDeleted = 0 )" +
                          " union " +
                          " select slotTempBlocked.slotID , slotTime,'booked by HR' as blocked, '' as 'qenID' ,''as jobID from slotTempBlocked left outer join slots on slotTempBlocked.slotID = slots.slotID where companyID = '" + companyID + "'  AND isDeleted = 0 AND forDate = convert(date, '" + dt + "') " +
                          " union " +
                          " select slotsBlocked.slotID , slotTime,'blocked by HR' as blocked, '' as 'qenID' ,''as jobID from slotsBlocked left outer join slots on slotsBlocked.slotID = slots.slotID where companyID = '" + companyID + "'  AND isDeleted = 0 " +
                          "  union " +
                          " SELECT qenInterviewSchedule.slotID, slotTime,qenName as blocked,  qendidateList.qenID as 'qenID' ,qenInterviewSchedule.jobID FROM qenInterviewSchedule left outer join qendidateList " +
                          " on qenInterviewSchedule.qenID = qendidateList.qenID left outer join slots on qenInterviewSchedule.slotID = slots.slotID where qenInterviewSchedule.companyID = '" + companyID + "'  AND qenInterviewSchedule.slotID is not null AND convert(date, meetScheduledDateTime)= convert(date, '" + dt + "')";
            var SlotList = db.Database.SqlQuery<slotsInfo>(query).ToList();
            return View(SlotList);
        }
        public string Get_Available(int slotid , string dat)
        {
            DateTime dt1 = BaseUtil.GetCurrentDateTime();
           
            if (dat.ToString() != "")
            {
                dt1 = Convert.ToDateTime(dat.ToString()).Date; 
            }
            var dt = dt1.Date;
            string result = "no";
            slotTempBlocked oslotTempBlocked = new slotTempBlocked();
            long companyID= Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            long employerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            oslotTempBlocked = db.slotTempBlockeds.Where(e => e.SlotID == slotid && e.companyID==companyID && e.forDate== dt).FirstOrDefault();

            oslotTempBlocked.isDeleted = true;
            oslotTempBlocked.modifyBy = employerID;
            oslotTempBlocked.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            try
            {
                db.Entry(oslotTempBlocked).State = EntityState.Modified;
                db.SaveChanges();
                result = "OK";
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public string slot_tempBlock(int slotid, string dat)
        {
            DateTime dt1 = BaseUtil.GetCurrentDateTime();
           
            if (dat.ToString() != "")
            {
                dt1 = Convert.ToDateTime(dat.ToString());
            }
            var dt = dt1.Date;
            string result = "no";
            slotTempBlocked oslotTempBlocked;
            long companyID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.companyID.ToString()));
            long employerID = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));
            oslotTempBlocked = db.slotTempBlockeds.Where(e => e.SlotID == slotid && e.companyID == companyID && e.forDate == dt).FirstOrDefault();
            if (oslotTempBlocked != null)
            {
                oslotTempBlocked.isDeleted = false;
                oslotTempBlocked.modifyBy = employerID;
                oslotTempBlocked.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                db.Entry(oslotTempBlocked).State = EntityState.Modified;
            }
            else {
                oslotTempBlocked = new slotTempBlocked();
                oslotTempBlocked.isDeleted = false;
                oslotTempBlocked.modifyBy = employerID;
                oslotTempBlocked.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                oslotTempBlocked.SlotID = slotid;
                oslotTempBlocked.forDate = dt;
                oslotTempBlocked.dataIsCreated= BaseUtil.GetCurrentDateTime();
                oslotTempBlocked.EmployerID = employerID;
                oslotTempBlocked.companyID = companyID;
                db.slotTempBlockeds.Add(oslotTempBlocked);
            }
            try
            {
               
                db.SaveChanges();
                result = "OK";
                return result;
            }
            catch (Exception e)
            {
                return result;
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
    }
}
