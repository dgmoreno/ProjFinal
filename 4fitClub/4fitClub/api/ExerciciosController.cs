using _4fitClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _4fitClub.api
{
    [RoutePrefix("api/exercicios")]
    public class ExerciciosController : ApiController
    {
        //referência a base de dados 
        private ApplicationDbContext db = new ApplicationDbContext();

        //Obter uma lista de execícios
        public IHttpActionResult GetExercicios(int categoria)
        {

            IQueryable < Exercicios > query = db.Exercicios;


                query = query
                    .Where(exercicio => exercicio.CategoriaFK.Equals(categoria));
         

            var resultado = query
                        .Select(exercicio => new
                        {
                            exercicio.ID,
                            exercicio.Nome,
                            exercicio.Objetivo,
                            exercicio.Passos,
                            exercicio.CategoriaFK,
                        });

            return Ok(resultado);
        }

    }
}
