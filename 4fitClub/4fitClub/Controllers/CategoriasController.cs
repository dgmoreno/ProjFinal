﻿using System;
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
            return View(db.Categorias.ToList());
        }

        // GET: Categorias/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Create(CreateCategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);

            }

            var categoria = new Categorias
            {
                ID = db.GetIDCategoria(),
                Nome = model.Nome,
                Descricao = model.Descricao
            };
            ///Verificação se o ficheiro fornecido é uma imagem é feito no ViewModel

            ///cria o nome da imagem
            string nomeImagem = "Categoria_" + categoria.ID + Path.GetExtension(model.Imagem.FileName);
            /// indica onde a imagem será guardada
            string caminhoParaImagem = Path.Combine(Server.MapPath("~/imagens/"), nomeImagem);
            ///guarda o nome da imagem na BD
            categoria.Imagem = nomeImagem;

            try
            {
                /// guardar a imagem no disco rígido
                model.Imagem.SaveAs(caminhoParaImagem);
                /// adiciona na estrutura de dados, na memória do servidor,
                /// o objeto Categorias
                db.Categorias.Add(categoria);
                /// faz 'commit' na BD
                db.SaveChanges();

                ///redireciona o utilizador para a página de início
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                /// gerar uma mensagem de erro para o utilizador
                ModelState.AddModelError("", "Ocorreu um erro não determinado na criação da nova Categoria...");
            }
            ///devolve os dados do Agente à View
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

            var model = new EditCategoriaViewModel
            {
                ID = categorias.ID,
                Descricao = categorias.Descricao,
                ImagemAtual = categorias.Imagem
            };

            return View(model);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            ///Obtém a categoria a editar, caso não encontre gera uma mensagem de erro
            var categoria = db.Categorias.Find(model.ID);

            if(categoria == null)
            {
                ModelState.AddModelError("", "A categoria não existe");
                return View(model);
            }

            string novoNome = "";
            string nomeAntigo = "";

            if (ModelState.IsValid)
            {
                try
                {
                    if(model.Imagem != null)
                    {
                        ///guarda-se os dados da imagem antiga
                        ///para eliminar mais tarde do disco 
                        nomeAntigo = categoria.Imagem;
                        ///o novo nome vai conter a data e hora da lateração
                        novoNome = "Categoria_" + categoria.ID + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + Path.GetExtension(model.Imagem.FileName).ToLower();
                        ///atualiza os dados da categoria
                        categoria.Imagem = novoNome;

                        ///guarda a nova imagem no disco
                        model.Imagem.SaveAs(Path.Combine(Server.MapPath("~/imagens/"), novoNome));
                    }
                    ///Passar os dados do modelo para a categoria
                    categoria.Descricao = model.Descricao;
                    ///guarda os dados da categoria
                    db.Entry(categoria).State = EntityState.Modified;
                    ///commite na base de dados
                    db.SaveChanges();

                    ///remoção da imagem antiga no caso de ter sido fornecida uma imagem 
                    if (model.Imagem != null)
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/imagens/"), nomeAntigo));
                    }
                    ///enviar os dados para a página inicial das Categorias
                    return RedirectToAction("Index");
                }
                catch
                {
                    ///Se houver um erro, mostra-se uma mensagem de erro
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição dos dados da categoria {0}", categoria.Nome));
                }
            }

            ///preenche-se novamente os campos que se possam ter perdido
            model.ImagemAtual = categoria.Imagem;

            return View(model);
        }

        // GET: Categorias/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorias categorias = db.Categorias.Find(id);
            db.Categorias.Remove(categorias);
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
