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
    public class ReceiptsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Receipts
        public async Task<ActionResult> Index()
        {
            var receipts = db.Receipts.Include(r => r.Staff);
            return View(await receipts.ToListAsync());
        }

        // GET: Receipts/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = await db.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // GET: Receipts/Create
        public ActionResult Create()
        {
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReceiptId,ReceiptDate,StaffId")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Receipts.Add(receipt);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", receipt.StaffId);
            return View(receipt);
        }

        // GET: Receipts/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = await db.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", receipt.StaffId);
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReceiptId,ReceiptDate,StaffId")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipt).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", receipt.StaffId);
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = await db.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Receipt receipt = await db.Receipts.FindAsync(id);
            db.Receipts.Remove(receipt);
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
