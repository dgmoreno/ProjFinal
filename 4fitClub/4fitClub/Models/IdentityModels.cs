using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _4fitClub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// classe para efetuar a gestão de utilizadores
    /// </summary>

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /// <summary>
    /// classe responsável pela criação da base de dados
    ///     -da autenticação
    ///     -do 'negócio'
    /// </summary>

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ExrecicioDBConnectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        // vamos colocar, aqui, as instruções relativas às tabelas do 'negócio'
        // descrever os nomes das tabelas na Base de Dados
        public virtual DbSet<Exercicios> Exercicios { get; set; } //cria tabela Exercicio

        public virtual DbSet<Categorias> Categorias { get; set; } //cria tabela Categoria

        public virtual DbSet<Planos> Planos { get; set; } //cria tabela Plano

        public virtual DbSet<Imagens> Imagens { get; set; } //cria tabela Imagens

        public int GetIDCategoria()
        {
            return this.Database
                .SqlQuery<int>("Select Next Value For [dbo].[SeqIdCategoria]")
                .Single();
        }

        public int GetIDMultimedia()
        {
            return this.Database
                .SqlQuery<int>("Select Next Value For [dbo].[SeqIdImagem]")
                .Single();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
