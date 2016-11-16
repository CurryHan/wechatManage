using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using Sensing.Data;
using Sensing.Entities.Users;

namespace SensingCloud
{
    public partial class Startup
    {
        private const string RoleName = "Administrator";

        public void ConfigureApp(IAppBuilder app)
        {
            using (var context = new SensingSiteDbContext())
            {
                context.Database.Delete();
                context.Database.Create();
                new SensingSiteSampleData().Seed(context,false);
            }

            CreateAdminUser().Wait();
        }

        private async Task CreateAdminUser()
        {
            var username = ConfigurationManager.AppSettings["DefaultAdminUsername"];
            var password = ConfigurationManager.AppSettings["DefaultAdminPassword"];

            using (var context = new SensingSiteDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                var role = new IdentityRole(RoleName);
                
                var result = await roleManager.RoleExistsAsync(RoleName);
                if (!result)
                {
                    await roleManager.CreateAsync(role);
                }

                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = username };
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user.Id, RoleName);
                }
            }
        }
    }
}