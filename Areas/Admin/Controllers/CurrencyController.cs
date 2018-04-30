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
    public class CurrencyController : BaseClass
    {
        private IUnitOfWork uow = null;
        private Currency repo = null;
        public CurrencyController()
        {
            uow = new UnitOfWork();
            repo = new Currency(uow);
        }

        // GET: Admin/Currency
        public async Task<ActionResult> Index()
        {
            var currency = repo.SQLQuery<sp_currencyTypeList_Result>("sp_currencyTypeList").ToList();
            return View(currency);
        }
      
        // GET: Admin/Currency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Currency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "currencyID,currency1,isActive")] currency currency)
        {
            currency.dataIsCreated = BaseUtil.GetCurrentDateTime();
            currency.dataIsUpdated= BaseUtil.GetCurrentDateTime();
           
            if (ModelState.IsValid)
            {

                 repo.Insert(currency);
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        // GET: Admin/Currency/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            currency currency = repo.Single(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Admin/Currency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "currencyID,currency1,isActive")] currency currency)
        {
            var s = repo.Single(currency.currencyID);

            if (s == null)
            {
                return HttpNotFound();
            }
            s.currency1 = currency.currency1;
            s.dataIsUpdated = BaseUtil.GetCurrentDateTime();
            s.isActive = currency.isActive;
            s.isSelected = currency.isSelected;
            s.modifiedBy = Convert.ToInt64(BaseUtil.GetSessionValue(AdminInfo.employerID.ToString()));

            if (ModelState.IsValid)
            {
                repo.Update(s);
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        
    }
}
