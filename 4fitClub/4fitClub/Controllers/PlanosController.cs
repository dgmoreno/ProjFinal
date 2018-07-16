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
        /// <summary>
        /// Retorna os planos de um utilizador ordenados pela data de criação 
        /// </summary>
        /// <returns></returns>
        // GET: Planos
        public ActionResult Index()
        {
            ///retorna os planos do user que efetuou login
            var planos = db.Planos
                            .Where(p => p.Cliente.UserName.Equals(User.Identity.Name))
                            .Include(p => p.Cliente);
            
               
                
            return View(planos.ToList().OrderByDescending(p => p.DataCriacao));
        }
        /// <summary>
        /// Detalhes de um plano, e na view será possível visualizar os exercícios do plano,
        /// com link para o exercício
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Planos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Planos planos = db.Planos.Find(id);
            if (planos == null)
            {
                return RedirectToAction("Index");
            }

            return View(planos);
        }

        // GET: Planos/Create
        public ActionResult Create()
        {
            ViewBag.ListaExercicios = db.Exercicios.OrderBy(e => e.Nome).ToList();
            return View();
        }
        /// <summary>
        /// Cria um plano, em que podem ser incluídos os exercícios
        /// </summary>
        /// <param name="planos"></param>
        /// <param name="opcoesEscolhidas"></param>
        /// <returns></returns>
        // POST: Planos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Descricao")] Planos planos, string[] opcoesEscolhidas)
        {
            //falta relaciona tablea de login para preencher os dados do cliente


            //retornar o id do cliente autenticado
            //var cliente = db.Cliente.Find(planos.ClienteFK).ID;
            var cliente = db.Cliente.Where(p => p.UserName.Equals(User.Identity.Name)).Single().ID;

            planos.ClienteFK = cliente;
            planos.DataCriacao = DateTime.Now;
            var exercicios = db.Exercicios.ToList();

            foreach (var ee in exercicios)
            {
                if (opcoesEscolhidas.Contains(ee.ID.ToString()))
                {
                    if (!planos.ListaDeExercicios.Contains(ee))
                    {
                    planos.ListaDeExercicios.Add(ee);
                    }
                }
                else
                {
                    planos.ListaDeExercicios.Remove(ee);
                }
            }

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
                return RedirectToAction("Index");
            }
            Planos planos = db.Planos.Find(id);
            if (planos == null)
            {
                return RedirectToAction("Index");
            }

            // gerar a lista de objetos de exercicios que podem ser associados aos planos
            ViewBag.ListaExercicios = db.Exercicios.OrderBy(e => e.Nome).ToList();
            return View(planos);
        }
        /// <summary>
        /// Edição do plano, pode-se adiciona ou retirar exercícios
        /// </summary>
        /// <param name="planos"></param>
        /// <param name="opcoesEscolhidas"></param>
        /// <returns></returns>
        // POST: Planos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Descricao")] Planos planos, string[] opcoesEscolhidas)
        {
            // ler da BD o objeto que se pretende editar
            var pp = db.Planos.Include(e => e.ListaDeExercicios).Where(e => e.ID == planos.ID).SingleOrDefault();

            if (ModelState.IsValid)
            {
                pp.Nome = planos.Nome;
                pp.Descricao = planos.Descricao;
            }
            else
            {
                // gerar a lista de objetos de exercicios que podem ser associados aos planos
                ViewBag.ListaExercicios = db.Exercicios.OrderBy(e => e.Nome).ToList();
                // devolver o controlo à View
                return View(planos);
            }

            // tentar fazer o Update
            if (TryUpdateModel(pp, "", new string[] { nameof(pp.Nome), nameof(pp.Descricao), nameof(pp.ListaDeExercicios) }))
            {
                //obter lista de exercicios
                var exercicios = db.Exercicios.ToList();

                if (opcoesEscolhidas != null)
                {
                    // se existirem opções escolhidas, vamos associá-las
                    foreach (var ee in exercicios)
                    {
                        if (opcoesEscolhidas.Contains(ee.ID.ToString()))
                        {
                            // se uma opção escolhida ainda não está associada, cria-se a associação
                            if (!pp.ListaDeExercicios.Contains(ee))
                            {
                                pp.ListaDeExercicios.Add(ee);
                            }
                        }
                        else
                        {
                            // caso exista associação para uma opção que não foi escolhida, 
                            // remove-se essa associação
                            pp.ListaDeExercicios.Remove(ee);
                        }
                    }
                }
                else
                {
                    // não existem opções escolhidas!
                    // vamos eliminar todas as associações
                    foreach (var ee in exercicios)
                    {
                        if (pp.ListaDeExercicios.Contains(ee))
                        {
                            pp.ListaDeExercicios.Remove(ee);
                        }
                    }
                }
                //guardar alterações
                db.SaveChanges();

                //devolver controlo à View
                return RedirectToAction("Index");
            }

            // se cheguei aqui, é pq alguma coisa correu mal
            ModelState.AddModelError("", "Alguma coisa correu mal...");

            // gerar a lista de objetos de exercicios que podem ser associados aos planos
            ViewBag.ListaExercicios = db.Exercicios.OrderBy(e => e.Nome).ToList();


            // visualizar View...
            return View(planos);
        }

        // GET: Planos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Planos planos = db.Planos.Find(id);
            if (planos == null)
            {
                return RedirectToAction("Index");
            }
            return View(planos);
        }
        /// <summary>
        /// O cliente pode apagar o plano, tirando a associação aos exercícios,
        /// podendo remover com segurança o plano
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Planos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planos planos = db.Planos.Find(id);
            // ler da BD o objeto que se pretende eliminar
            var pp = db.Planos.Include(e => e.ListaDeExercicios).Where(e => e.ID == planos.ID).SingleOrDefault();
            //obter lista de exercicios
            var exercicios = db.Exercicios.ToList();
            foreach (var ee in exercicios)
            {
                if (pp.ListaDeExercicios.Contains(ee))
                {
                    pp.ListaDeExercicios.Remove(ee);
                }
            }
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
