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
    [Authorize(Roles = "Manager,Utilizador")]
    public class ClientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clientes
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        [Authorize(Roles = "Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,NIF,UserName")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = "Manager,Utilizador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                if (User.IsInRole("Utilizador"))
                {
                    return RedirectToAction("Index", "Manage");
                }
                return RedirectToAction("Index");
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                if (User.IsInRole("Utilizador"))
                {
                    return RedirectToAction("Index", "Manage");
                }
                return RedirectToAction("Index");
            }
            if (User.IsInRole("Manager") || User.Identity.Name.Equals(cliente.UserName))
            {
                return View(cliente);
            }
            if (User.IsInRole("Utilizador"))
            {
                return RedirectToAction("Index", "Manage");
            }
            return RedirectToAction("Index");
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,NIF,UserName")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Utilizador"))
                {
                    return RedirectToAction("Index", "Manage");
                }
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            try
            {
                db.Cliente.Remove(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar o Cliente {0}, existem planos de treino associados ao Cliente",
                                            cliente.Nome)
                );
            }
            return View(cliente);
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
