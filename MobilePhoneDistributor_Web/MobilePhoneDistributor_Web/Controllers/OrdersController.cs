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
using Microsoft.Extensions.Options;

namespace MobilePhoneDistributor_Web.Controllers
{
    public class OrdersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            if (Session["user"]==null || Session["role"] as string != "Agent")
            {
                return RedirectToAction("Login", "Agents", null);
            }
            string id = Session["user"] as string;
            return View(db.Orders.ToList().Where(i=>i.AgentId==id));
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            var options = new List<SelectListItem>
            {
                new SelectListItem { Value = "COD", Text = "Cash On Delivery" },
                new SelectListItem { Value = "VNPAY", Text = "VnPay" },
           
            };
            ViewBag.Options = new SelectList(options,"Value","Text","COD");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PaymentMethod")] Order order)
        {
            
            Order IdLastest;
            if ((from i in db.Orders orderby i.OrderId descending select i)?.FirstOrDefault() == null)
            {
                IdLastest = null;
            }
            else
            {
                IdLastest = (from i in db.Orders orderby i.OrderId descending select i)?.FirstOrDefault();
            }
            string id = General.GenerateOrdertId(IdLastest);
            order.OrderId = id;
            order.OrderDate = DateTime.Now;
            order.AgentId = Session["user"] as string;
            order.OrderStatus = "On Processing";
            order.PaymentStatus = "Not Payed";
            var cart = Session["cart"] as Cart;
            if (order.PaymentMethod != null)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
            }
            else
            {
                return View();
            }
            foreach ( var item in cart.GetItems())
            {
                var detail = new OrderDetail
                {
                    OrderId = id,
                    PhoneModelId = item.PhoneModelId,
                    Quantity = item.Quantity,
                };
                db.OrdersDetail.Add(detail);
                db.SaveChanges();
            }
            Session["cart"] = null;
            return RedirectToAction("Index");


            var options = new List<SelectListItem>
            {
                new SelectListItem { Value = "COD", Text = "Cash On Delivery" },
                new SelectListItem { Value = "VNPAY", Text = "VnPay" },

            };
            ViewBag.Options = new SelectList(options, "Value", "Text", "COD");
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "FirstName", order.AgentId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,OrderDate,OrderStatus,PaymentMethod,PaymentStatus,AgentId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "FirstName", order.AgentId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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
