using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _4fitClub.Models;

namespace _4fitClub.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ImagensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Imagens
        public ActionResult Index()
        {
            var imagens = db.Imagens.Include(i => i.Exercicio);
            return View(imagens.ToList());
        }

        // GET: Imagens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagens imagens = db.Imagens.Find(id);
            if (imagens == null)
            {
                return HttpNotFound();
            }
            return View(imagens);
        }

        // GET: Imagens/Create
        public ActionResult Create()
        {
            ViewBag.ExercicioFK = new SelectList(db.Exercicios, "ID", "Nome");
            return View();
        }

        // POST: Imagens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ordem,Tipo,ExercicioFK")] Imagens imagens, HttpPostedFileBase uploadFotografia)
        {
            int id = 0;

            string nomeImagem = "Img_" + id + ".jpg";

            string path = "";

            if(uploadFotografia != null)
            {

                    path = Path.Combine(Server.MapPath("~/multimedia/"), nomeImagem);
             

                imagens.Nome = nomeImagem;


            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem");
                return View(imagens);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Imagens.Add(imagens);
                    db.SaveChanges();
                    uploadFotografia.SaveAs(path);
                    return RedirectToAction("Index/Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Houve um erro na adição da imagem...");
                }
            }

            ViewBag.ExercicioFK = new SelectList(db.Exercicios, "ID", "Nome", imagens.ExercicioFK);
            return View(imagens);
        }

        // GET: Imagens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagens imagens = db.Imagens.Find(id);
            if (imagens == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExercicioFK = new SelectList(db.Exercicios, "ID", "Nome", imagens.ExercicioFK);
            return View(imagens);
        }

        // POST: Imagens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Ordem,Tipo,ExercicioFK")] Imagens imagens, HttpPostedFileBase uploadFotografia)
        {
            string nomeImagem = "Img_" + imagens.ID + ".jpg";

            string path = "";

            if (uploadFotografia != null)
            {
                if (uploadFotografia.FileName.EndsWith("jpg") || uploadFotografia.FileName.EndsWith("png"))
                {
                    path = Path.Combine(Server.MapPath("~/multimedia/"), nomeImagem);
                }
                else
                {
                    ModelState.AddModelError("", "Ficheiro não é uma imagem");
                }
                imagens.Nome = nomeImagem;
            }
            if (ModelState.IsValid)
            {
                db.Entry(imagens).State = EntityState.Modified;
                db.SaveChanges();

                if(uploadFotografia != null)
                {
                    uploadFotografia.SaveAs(path);

                }
                return RedirectToAction("Index/Index");
            }
            ViewBag.ExercicioFK = new SelectList(db.Exercicios, "ID", "Nome", imagens.ExercicioFK);
            return View(imagens);
        }

        // GET: Imagens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagens imagens = db.Imagens.Find(id);
            if (imagens == null)
            {
                return HttpNotFound();
            }
            return View(imagens);
        }

        // POST: Imagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imagens imagens = db.Imagens.Find(id);
            db.Imagens.Remove(imagens);
            db.SaveChanges();
            return RedirectToAction("Index/Index");
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
