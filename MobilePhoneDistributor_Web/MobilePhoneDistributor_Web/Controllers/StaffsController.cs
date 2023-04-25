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
                    Session["user"] = staff.StaffId as string;
                    Session["user_fullname"] = staff.FirstName + " " + staff.LastName as string;
                    Session["role"] = "Staff" as string;
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
                string LastStaff = db.Staffs.OrderByDescending(s => s.StaffId).FirstOrDefault()?.StaffId;
                Console.WriteLine(LastStaff);
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

        
        
        public ActionResult DeniedAccess()
        {
            return View();
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
