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
    public class qenSkillsController : BaseClass
    {
        private oriondbEntities db = new oriondbEntities();

        // GET: qenSkills
        public async Task<ActionResult> Index()
        {
            var qenSkills = db.qenSkills.Include(q => q.qendidateList).Include(q => q.skill);
            return View(await qenSkills.ToListAsync());
        }

        // GET: qenSkills/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qenSkill qenSkill = await db.qenSkills.FindAsync(id);
            if (qenSkill == null)
            {
                return HttpNotFound();
            }
            return View(qenSkill);
        }

        // GET: qenSkills/Create
        public ActionResult Create()
        {
            ViewBag.qenID = new SelectList(db.qendidateLists, "qenID", "qenName");
            ViewBag.skillsID = new SelectList(db.skills, "skillsID", "skillName");
            return View();
        }

        // POST: qenSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "qenSkillsID,qenID,skillsID,yearOfExp")] qenSkill qenSkill)
        {
            if (ModelState.IsValid)
            {
                db.qenSkills.Add(qenSkill);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.qenID = new SelectList(db.qendidateLists, "qenID", "qenName", qenSkill.qenID);
            ViewBag.skillsID = new SelectList(db.skills, "skillsID", "skillName", qenSkill.skillsID);
            return View(qenSkill);
        }

        // GET: qenSkills/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qenSkill qenSkill = await db.qenSkills.FindAsync(id);
            if (qenSkill == null)
            {
                return HttpNotFound();
            }
            ViewBag.qenID = new SelectList(db.qendidateLists, "qenID", "qenName", qenSkill.qenID);
            ViewBag.skillsID = new SelectList(db.skills, "skillsID", "skillName", qenSkill.skillsID);
            return View(qenSkill);
        }

        // POST: qenSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "qenSkillsID,qenID,skillsID,yearOfExp")] qenSkill qenSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qenSkill).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.qenID = new SelectList(db.qendidateLists, "qenID", "qenName", qenSkill.qenID);
            ViewBag.skillsID = new SelectList(db.skills, "skillsID", "skillName", qenSkill.skillsID);
            return View(qenSkill);
        }

        // GET: qenSkills/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qenSkill qenSkill = await db.qenSkills.FindAsync(id);
            if (qenSkill == null)
            {
                return HttpNotFound();
            }
            return View(qenSkill);
        }

        // POST: qenSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            qenSkill qenSkill = await db.qenSkills.FindAsync(id);
            db.qenSkills.Remove(qenSkill);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
