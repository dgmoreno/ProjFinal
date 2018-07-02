using _4fitClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;

namespace _4fitClub.api
{
    [RoutePrefix("api/categoria")]
    public class CategoriaController : ApiController
    {
        //Referência para base de dados
        private ApplicationDbContext db = new ApplicationDbContext();


        //Obter uma lista de categorias
        public IHttpActionResult GetCategorias()
        {
            var resultado = db.Categorias
                        .Select(categoria => new
                        {
                            categoria.ID,
                            categoria.Nome,
                            categoria.Descricao,
                            categoria.Imagem
                        }).ToList();


            return Ok(resultado);
        }





    }
}
