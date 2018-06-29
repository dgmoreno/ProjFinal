using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4fitClub.Models
{
    public class Categorias
    {
        public Categorias()
        {
            ListaDeExercicios = new HashSet<Exercicios>();
        }


        [Key]
        public int ID { get; set; } //Primary key

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        
        public string Imagem { get; set; }


        //referência às categorias a que um exercício pertence
        public virtual ICollection<Exercicios> ListaDeExercicios { get; set; }

    }
}