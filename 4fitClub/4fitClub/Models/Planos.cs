using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4fitClub.Models
{

    public class Planos
    {
        public Planos()
        {
            ListaDeExercicios = new HashSet<Exercicios>();
        }

        [Key]
        public int ID { get; set; } //Primary key

        public string Nome { get; set; }

        public string Descricao { get; set; }

        // relacionamento muito para muitos com os exercicios
        public virtual ICollection<Exercicios> ListaDeExercicios { get; set; }
        //associar a uma lista de utilizadores. Cada utilizador vai ter acesso
        //aos seus planos
    }
}