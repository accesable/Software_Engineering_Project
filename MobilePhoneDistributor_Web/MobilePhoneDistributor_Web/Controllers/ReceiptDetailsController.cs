﻿using System;
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
            var receiptsDetail = db.ReceiptsDetail.Include(r => r.Receipt);
            return View(await receiptsDetail.ToListAsync());
        }

        // GET: ReceiptDetails/Details/5
        public async Task<ActionResult> Details(string id)
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
            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId");
            return View();
        }

        // POST: ReceiptDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReceiptDetailId,ReceiptId,Quantity,PhonneModelId,UnitAmmount")] ReceiptDetail receiptDetail)
        {
            if (ModelState.IsValid)
            {
                db.ReceiptsDetail.Add(receiptDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Edit/5
        public async Task<ActionResult> Edit(string id)
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
            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // POST: ReceiptDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReceiptDetailId,ReceiptId,Quantity,PhonneModelId,UnitAmmount")] ReceiptDetail receiptDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receiptDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ReceiptId = new SelectList(db.Receipts, "ReceiptId", "StaffId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Delete/5
        public async Task<ActionResult> Delete(string id)
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
        public async Task<ActionResult> DeleteConfirmed(string id)
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
