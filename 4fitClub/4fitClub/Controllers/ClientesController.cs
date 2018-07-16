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
    [Authorize(Roles = "Manager,Utilizador")]
    public class ClientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Ação para visualização dos clientes na aplicação
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Ação para criação de cliente, mas não átiva
        /// Isto deve-se ao facto dos clientes criarem as sua própias contas 
        /// e automáticamente é adicionado à base de dados por isso esta funcionalidade 
        /// do lado do "Manager" não está átiva
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Imagem,UserName")] Cliente cliente)
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
        /// <summary>
        /// Ação para Edição que recebe a imagem do upload e o Cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="uploadImagemEdit"></param>
        /// <returns></returns>
        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Imagem,UserName")] Cliente cliente, HttpPostedFileBase uploadImagemEdit)
        {
            string novaImagem = "User_" + cliente.ID + ".jpg";

            string path = "";

            if (uploadImagemEdit != null)
            {
                if (uploadImagemEdit.FileName.EndsWith("jpg") || uploadImagemEdit.FileName.EndsWith("png"))
                {
                    path = Path.Combine(Server.MapPath("~/UserImages"), novaImagem);

                }
                else
                {
                    ModelState.AddModelError("", "Ficheiro não é uma imagem");
                }
                cliente.Imagem = novaImagem;
            }

            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                if (uploadImagemEdit != null)
                {
                    uploadImagemEdit.SaveAs(path);
                }
                if (User.IsInRole("Utilizador"))
                {
                    return RedirectToAction("Index", "Manage");
                }
                else
                {
                    return RedirectToAction("Index");
                }
               
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
        /// <summary>
        /// Tal como o create, para apagar o cliente seria uma decisão interna
        /// Na minha opinião pode ser feita mas isto gera o problema de haver planos criados
        /// que não têm nenhum cliente associado o impossibilita a eliminação desses planos
        /// Por isso decidi não fazer a eliminação do cliente. Mas mais uma vez
        /// na minha opinão deve ser possível a sua eliminação
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
