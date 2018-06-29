using _4fitClub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_4fitClub.Startup))]
namespace _4fitClub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //invoca o método para iniciar a configuração do Roles
            iniciaAplicacao();
        }
        
        private void iniciaAplicacao()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //criar a Role do Gestor da aplicação
            if (!roleManager.RoleExists("Manager"))
            {
                //não existe a 'role'
                //então cria a role
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            //criar a Role Utilizador
            if (!roleManager.RoleExists("Utilizador"))
            {
                //não existe a 'role'
                //então cria a role
                var role = new IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);
            }


            // criar um utilizador Manager
            var user = new ApplicationUser();
            user.UserName = "admin@mail.pt";
            user.Email = "admin@mail.pt";
            string userPWD = "123_Asd";
            var chkuser = userManager.Create(user, userPWD);

            //Adiciona o utilizador à respetiva Role Manager
            if (chkuser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Manager");
            }



            // criar um utilizador Manager
            user = new ApplicationUser();
            user.UserName = "david@mail.pt";
            user.Email = "david@mail.pt";
            userPWD = "123_Asd";
            chkuser = userManager.Create(user, userPWD);

            //Adiciona o utilizador à respetiva Role Manager
            if (chkuser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Utilizador");
            }


        }

    }
}
