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
using System.Security.Cryptography;

namespace MobilePhoneDistributor_Web.Controllers
{
    public class AgentsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Agents
        public async Task<ActionResult> Index()
        {
            return View(await db.Agents.ToListAsync());
        }

        // GET: Agents/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agents.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
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
                Agent agent = db.Agents.Where(x => x.Username == loginViewModel.Username).FirstOrDefault();
                if (agent == null) return RedirectToAction("Index", "Home");
                if (PasswordHasher.ValidatePassword(loginViewModel.Password, agent.Password, agent.PasswordSalt))
                {
                    Session["user"] = agent.AgentId as string;
                    Session["user_fullname"] = agent.FirstName + " " + agent.LastName as string;
                    Session["role"] = "Agent" as string;
                    return RedirectToAction("Message","Staffs",null);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        // GET: Agents/Create
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
                string LastAgent = db.Agents.OrderByDescending(s => s.AgentId).FirstOrDefault()?.AgentId;
                Agent AddedAgent = new Agent()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    AgentId = General.GenerateAgentID(LastAgent),
                    Password = StoredPassword[0],
                    PasswordSalt = StoredPassword[1],
                    Email = model.Email,
                    Username = model.Username,
                    PhoneNumber = model.PhoneNumber,
                };

                db.Agents.Add(AddedAgent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Agents/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agents.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AgentId,FirstName,LastName,Username,Password,PasswordSalt,Email,PhoneNumber")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(agent);
        }

        // GET: Agents/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agents.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Agent agent = await db.Agents.FindAsync(id);
            db.Agents.Remove(agent);
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
