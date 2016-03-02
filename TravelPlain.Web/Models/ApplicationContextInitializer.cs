using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TravelPlain.Web.Models
{
    public class ApplicationContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Manager"));

            var adminTony = new ApplicationUser
            {
                UserName = "tony.swiftlook@gmail.com",
                Email = "tony.swiftlook@gmail.com",
                PhoneNumber = "+380507779447"
            };
            var result = userManager.Create(adminTony, "P@ssw0rd!");
            userManager.AddToRoles(adminTony.Id, "Admin", "Manager");

            var managerJohn = new ApplicationUser
            {
                UserName = "john.snow@gmail.com",
                Email = "john.snow@gmail.com",
                PhoneNumber = "+380507779448"
            };
            userManager.Create(managerJohn, "P@ssw0rd!");
            userManager.AddToRole(managerJohn.Id, "Manager");

            var managerOmlet = new ApplicationUser
            {
                UserName = "omlet.egg@yahoo.com",
                Email = "omlet.egg@yahoo.com",
                PhoneNumber = "+380507779449"
            };
            userManager.Create(managerOmlet, "P@ssw0rd!");
            userManager.AddToRole(managerOmlet.Id, "Manager");

            context.SaveChanges();

            base.Seed(context);
        }
    }
}