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
    public class ExerciciosController : Controller
    {
        private ExercicioDb db = new ExercicioDb();

        // GET: Exercicios
        public ActionResult Index()
        {
            var exercicios = db.Exercicios.Include(e => e.Categoria);
            return View(exercicios.ToList());
        }

        // GET: Exercicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicios exercicios = db.Exercicios.Find(id);
            if (exercicios == null)
            {
                return HttpNotFound();
            }
            return View(exercicios);
        }

        // GET: Exercicios/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "ID", "Nome");
            return View();
        }

        // POST: Exercicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Objetivo,Passos,CategoriaFK")] Exercicios exercicios)
        {
            if (ModelState.IsValid)
            {
                db.Exercicios.Add(exercicios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaFK = new SelectList(db.Categorias, "ID", "Nome", exercicios.CategoriaFK);
            return View(exercicios);
        }

        // GET: Exercicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicios exercicios = db.Exercicios.Find(id);
            if (exercicios == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "ID", "Nome", exercicios.CategoriaFK);
            return View(exercicios);
        }

        // POST: Exercicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Objetivo,Passos,CategoriaFK")] Exercicios exercicios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercicios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "ID", "Nome", exercicios.CategoriaFK);
            return View(exercicios);
        }

        // GET: Exercicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicios exercicios = db.Exercicios.Find(id);
            if (exercicios == null)
            {
                return HttpNotFound();
            }
            return View(exercicios);
        }

        // POST: Exercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercicios exercicios = db.Exercicios.Find(id);
            db.Exercicios.Remove(exercicios);
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
