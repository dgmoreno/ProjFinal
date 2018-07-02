using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _4fitClub.Models;

namespace _4fitClub.Controllers
{
    [Authorize(Roles = "Utilizador")]
    public class PlanosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Planos
        public ActionResult Index()
        {
            ///retorna os plnos do user que efetuou login
            var planos = db.Planos
                            .Where(p => p.UserName.Equals(User.Identity.Name))
                            .Include(p => p.ListaDeExercicios);
            
               
                
            return View(planos.ToList());
        }

        // GET: Planos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planos planos = db.Planos.Find(id);
            if (planos == null)
            {
                return HttpNotFound();
            }
            /// ideia seria colocar os exercícios relacionados com o plano



            return View(planos);
        }

        // GET: Planos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Descricao")] Planos planos)
        {
            planos.UserName = User.Identity.Name;

            if (ModelState.IsValid)
            {
                
                db.Planos.Add(planos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planos);
        }

        // GET: Planos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planos planos = db.Planos.Find(id);
            if (planos == null)
            {
                return HttpNotFound();
            }
            return View(planos);
        }

        // POST: Planos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Descricao")] Planos planos)
        {
            planos.UserName = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Entry(planos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planos);
        }

        // GET: Planos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planos planos = db.Planos.Find(id);
            if (planos == null)
            {
                return HttpNotFound();
            }
            return View(planos);
        }

        // POST: Planos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planos planos = db.Planos.Find(id);
            db.Planos.Remove(planos);
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
