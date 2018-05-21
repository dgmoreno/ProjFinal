using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4fitClub.Models
{
    public class Exercicios
    {

        public Exercicios()
        {
            ListaDePlanos = new HashSet<Planos>();

            ListaDeImagens = new HashSet<Imagens>();
        }

        [Key]
        public int ID { get; set; } //Primary key

        public string Nome { get; set; }

        public string Objetivo { get; set; }

        public string Passos { get; set; }


        //FK para a tabela das Categorias
        [ForeignKey("Categoria")]
        public int CategoriaFK { get; set; }
        public virtual Categorias Categoria { get; set; }

        //relação muitos para muitos com os diferentes Planos de treino
        public virtual ICollection<Planos> ListaDePlanos { get; set; }

        public virtual ICollection<Imagens> ListaDeImagens { get; set; }


    }
}