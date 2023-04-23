using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobilePhoneDistributor_Web.Models;

namespace MobilePhoneDistributor_Web.Controllers
{
    public class PhoneModelsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: PhoneModels
        public async Task<ActionResult> Index()
        {
            return View(await db.PhoneModels.ToListAsync());
        }

        // GET: PhoneModels/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneModel phoneModel = await db.PhoneModels.FindAsync(id);
            if (phoneModel == null)
            {
                return HttpNotFound();
            }
            return View(phoneModel);
        }

        // GET: PhoneModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PhoneId,PhoneName,PhoneBrand")] PhoneModel phoneModel)
        {
            if (ModelState.IsValid)
            {
                db.PhoneModels.Add(phoneModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(phoneModel);
        }

        // GET: PhoneModels/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneModel phoneModel = await db.PhoneModels.FindAsync(id);
            if (phoneModel == null)
            {
                return HttpNotFound();
            }
            return View(phoneModel);
        }

        // POST: PhoneModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PhoneId,PhoneName,PhoneBrand")] PhoneModel phoneModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phoneModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phoneModel);
        }

        // GET: PhoneModels/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneModel phoneModel = await db.PhoneModels.FindAsync(id);
            if (phoneModel == null)
            {
                return HttpNotFound();
            }
            return View(phoneModel);
        }

        // POST: PhoneModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PhoneModel phoneModel = await db.PhoneModels.FindAsync(id);
            db.PhoneModels.Remove(phoneModel);
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
