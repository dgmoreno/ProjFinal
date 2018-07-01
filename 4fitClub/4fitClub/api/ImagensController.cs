using _4fitClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _4fitClub.api
{
    [RoutePrefix("api/imagens")]
    public class ImagensController : ApiController
    {
        //referência a base de dados 
        private ApplicationDbContext db = new ApplicationDbContext();

        //Obter uma lista de Imagens

        public IHttpActionResult GetImagens(int img)
        {
            IQueryable<Imagens> querry = db.Imagens;

            querry = querry
                    .Where(imagem => imagem.ExercicioFK.Equals(img));

            var resultado = querry
                            .Select(imagem => new
                            {
                                imagem.ID,
                                imagem.Nome,
                                imagem.Ordem,
                                imagem.Tipo,
                                imagem.ExercicioFK
                            });


            return Ok(resultado);
        }


    }
}
