﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _4fitClub.Models
{
    public class Imagens
    {

        public int ID { get; set; }

        public string Nome { get; set; }

        public int Ordem { get; set; } //ordem de apresentação

        public string Tipo { get; set; } // se é imagem ou vídeo


        //FK para a tabela dos Exercicios
        [ForeignKey("Exercicio")]
        public int ExercicioFK { get; set; }
        public virtual Exercicios Exercicio { get; set; }

    }
}