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
    public class ReceiptDetailsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: ReceiptDetails
        public async Task<ActionResult> Index()
        {
            var receiptsDetail = db.ReceiptsDetail.Include(r => r.PhoneModel).Include(r => r.Receipt);
            return View(await receiptsDetail.ToListAsync());
        }

        // GET: ReceiptDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiptDetail receiptDetail = await db.ReceiptsDetail.FindAsync(id);
            if (receiptDetail == null)
            {
                return HttpNotFound();
            }
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Create
        public ActionResult Create()
        {
            ViewBag.PhoneModelId = new SelectList(db.PhoneModels, "PhoneId", "PhoneName");
            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId");
            return View();
        }

        // POST: ReceiptDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReceiptDetailId,ReceiptId,Quantity,PhoneModelId,UnitAmmount")] ReceiptDetail receiptDetail)
        {

            if (ModelState.IsValid)
            {
                db.ReceiptsDetail.Add(receiptDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PhoneModelId = new SelectList(db.PhoneModels, "PhoneId", "PhoneName", receiptDetail.PhoneModelId);
            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }
        // GET: ReceiptDetails/Create
        public ActionResult AppendDetail(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var receipt = db.Receipts.Find(id);
            ViewBag.PhoneModelId = new SelectList(db.PhoneModels, "PhoneId", "PhoneName");
            return View(receipt);
        }

        // POST: ReceiptDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AppendDetail(string id,[Bind(Include = "Quantity,PhoneModelId,UnitAmmount")] ReceiptDetail receiptDetail)
        {

            if (ModelState.IsValid)
            {
                receiptDetail.ReceiptId = id;
                db.ReceiptsDetail.Add(receiptDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PhoneModelId = new SelectList(db.PhoneModels, "PhoneId", "PhoneName", receiptDetail.PhoneModelId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiptDetail receiptDetail = await db.ReceiptsDetail.FindAsync(id);
            if (receiptDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhoneModelId = new SelectList(db.PhoneModels, "PhoneId", "PhoneName", receiptDetail.PhoneModelId);
            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // POST: ReceiptDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReceiptDetailId,ReceiptId,Quantity,PhoneModelId,UnitAmmount")] ReceiptDetail receiptDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receiptDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PhoneModelId = new SelectList(db.PhoneModels, "PhoneId", "PhoneName", receiptDetail.PhoneModelId);
            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiptDetail receiptDetail = await db.ReceiptsDetail.FindAsync(id);
            if (receiptDetail == null)
            {
                return HttpNotFound();
            }
            return View(receiptDetail);
        }

        // POST: ReceiptDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReceiptDetail receiptDetail = await db.ReceiptsDetail.FindAsync(id);
            db.ReceiptsDetail.Remove(receiptDetail);
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
