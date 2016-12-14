using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PhotoGallery.Migrations;
using PhotoGallery.Models;

[assembly: OwinStartupAttribute(typeof(PhotoGallery.Startup))]
namespace PhotoGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ApplicationDbContext,Configuration>());
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Creating Admin Role and a default Admin User at Startup
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Creating an Admin user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "ivmiron";
                user.Email = "ivomironov@msn.com";

                string userPWD = "Qwerty@12";

                var createdUser = userManager.Create(user, userPWD);

                //Adding the default User to Role Admin   
                if (createdUser.Succeeded)
                {
                    var admin = userManager.AddToRole(user.Id, "Admin");
                }
            }

            // Creating User role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
