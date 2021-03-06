﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4fitClub.Models
{
    public class Cliente
    {
        public Cliente()
        {
            ListaDePlanos = new HashSet<Planos>();
        }


        public int ID { get; set; }

        public string Nome { get; set; }
    
        public string Imagem { get; set; }

        public string UserName { get; set; }

        // Lista de Planos do cliente
        public virtual ICollection<Planos> ListaDePlanos { get; set; }

    }
}