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
using _4fitClub.ViewModels;

namespace _4fitClub.Controllers
{
    [Authorize(Roles = "Manager,Utilizador")]
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorias
        [AllowAnonymous]
        public ActionResult Index()
        {
            var listaDeCategorias = db.Categorias.ToList().OrderBy(c => c.Nome);
            return View(listaDeCategorias);
        }

        // GET: Categorias/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return RedirectToAction("Index");
            }
            //var a = db.Exercicios.Where(e => e.CategoriaFK.Equals(categorias)).Include(e => e.Categoria);


            return View(categorias);

            

        }

        // GET: Categorias/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Descricao")] Categorias categoria, HttpPostedFileBase uploadImagem)
        {
            
            
            int idNovaCategoria = 0;
            try
            {
                //id da categoria será +1 que o último id
                idNovaCategoria = db.Categorias.Max(c => c.ID) + 1;
            }
            catch
            {
                idNovaCategoria = 1;
            }
            

            ///cria o nome da imagem
            string nomeImagem = "Categoria_" + idNovaCategoria + ".jpg";
            string path = "";
       
            
            if(uploadImagem != null)
            {
                if (uploadImagem.FileName.EndsWith("jpg") || uploadImagem.FileName.EndsWith("png"))
                {
                    //caminho para guardar a imagem
                    path = Path.Combine(Server.MapPath("~/imagens/"), nomeImagem);
                }
                //novo nome da imagem
                categoria.Imagem = nomeImagem;
            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem");
                return View(categoria);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    //adicionar os dados à tabela Categorias
                    db.Categorias.Add(categoria);
                    //commit na base de dados
                    db.SaveChanges();
                    //guardar a imagem no caminho criado anteriormente
                    uploadImagem.SaveAs(path);
                    //redireciona para a view inical das categorias
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Houve um erro com a criação da nova Categoria...");
                }
            }
            ///devolve os dados da Categoria à View
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ///return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ///Se Id==nulll retornar à página das categorias
                return RedirectToAction("Index");
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                ///return HttpNotFound();
                return RedirectToAction("Index");
               
            }

            return View(categorias);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Descricao,Imagem")] Categorias categoria, HttpPostedFileBase uploadImagemEdit)
        {
            string novaImagem = "Categoria_" + categoria.ID + ".jpg";

            string path = "";

            if(uploadImagemEdit!=null)
            {
                if (uploadImagemEdit.FileName.EndsWith("jpg") || uploadImagemEdit.FileName.EndsWith("png"))
                {
                    path = Path.Combine(Server.MapPath("~/imagens/"), novaImagem);
                   
                }
                else
                {
                    ModelState.AddModelError("", "Ficheiro não é uma imagem");
                }
                categoria.Imagem = novaImagem;
            }


            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();

                if (uploadImagemEdit != null)
                {
                    uploadImagemEdit.SaveAs(path);
                }

                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return RedirectToAction("Index");
            }
            return View(categorias);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorias categoria = db.Categorias.Find(id);
            try
            {
                db.Categorias.Remove(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar a Categoria {0}, existem exercícios associados à categoria",
                                           categoria.Nome)
               );
            }

            return View(categoria);
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
