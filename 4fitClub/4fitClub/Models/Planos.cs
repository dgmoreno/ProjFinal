using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DateTime DataCriacao { get; set; }

        //incluir data de inicio de criação
        //public DateTime DataCriacao { get; set; }
        //*************************************************************
        // nome do login (UserName) utilizado para identificar os Planos do utilizador
        // na prática, é uma FK para a tabela dos utilizadores AspNetUsers
        [ForeignKey("Cliente")]
        public int ClienteFK { get; set; }
        public virtual Cliente  Cliente { get; set; }


        // relacionamento muito para muitos com os exercicios
        public virtual ICollection<Exercicios> ListaDeExercicios { get; set; }
        //associar a uma lista de utilizadores. Cada utilizador vai ter acesso
        //aos seus planos


    }
}