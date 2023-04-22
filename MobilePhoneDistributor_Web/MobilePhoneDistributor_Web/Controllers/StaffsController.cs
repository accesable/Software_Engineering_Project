using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobilePhoneDistributor_Web.Models;

namespace MobilePhoneDistributor_Web.Controllers
{
    public class StaffsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Staffs
        public ActionResult Index()
        {
            if (Session["user"]==null) return RedirectToAction("Message");
            return View(db.Staffs.ToList());
        }

        // GET: Staffs
        public ActionResult Message()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Staff staff=db.Staffs.Where(x=>x.Username==loginViewModel.Username).FirstOrDefault();
                if(staff==null) return RedirectToAction("Index", "Home");
                if (PasswordHasher.ValidatePassword(loginViewModel.Password, staff.Password, staff.PasswordSalt))
                {
                    Session["user"] = staff.StaffId;
                    Session["user_fullname"] = staff.FirstName+" "+staff.LastName;
                    return RedirectToAction("Message");
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return View();
        }

        // GET: Staffs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string[] StoredPassword = PasswordHasher.CreatePassword(model.Password);
                string LastStaff = db.Staffs.ToList().LastOrDefault().StaffId;
                Staff AddedStaff= new Staff() {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StaffId = General.GenerateStaffID(LastStaff),
                    Password = StoredPassword[0],
                    PasswordSalt = StoredPassword[1],
                    Email=model.Email,
                    Username=model.Username,
                    PhoneNumber=model.PhoneNumber,
            };
                
                db.Staffs.Add(AddedStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,Username,Email,PhoneNumber")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
            db.SaveChanges();
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
