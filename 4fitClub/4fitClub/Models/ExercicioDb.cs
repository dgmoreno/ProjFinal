using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace _4fitClub.Models
{
    public class ExercicioDb : DbContext
    {
        //construtor que indica a base de dados a ser utilizada
        public ExercicioDb():base("name=ExrecicioDBConnectionString")
        {

        }

        //tabelas na Base de Dados

        public virtual DbSet<Exercicios> Exercicios { get; set; } //cria tabela Exercicio

        public virtual DbSet<Categorias> Categorias { get; set; } //cria tabela Categoria

        public virtual DbSet<Planos> Planos { get; set; } //cria tabela Plano

        public virtual DbSet<Imagens> Imagens { get; set; } //cria tabela Imagens




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }


    }
}