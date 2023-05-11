
using BookShoppingCartMvcUI.Constans;
using Microsoft.AspNetCore.Identity;

namespace BookShoppingCartMvcUI.Data
{
    public class DbSeeder
    {
        //The IServiceProvider is responsible for resolving instances of types at runtime,
        //as required by the application.
        //These instances can be injected
        //into other services resolved from the same
        //dependency injection container.
        //The ServiceProvider ensures that resolved services live for the expected lifetime
        public static async Task SeedDefaultData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            //Adding Roles To DataBase
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            //Create Admin Recored

            var Admin = new IdentityUser
            {
                UserName = "Admin@gmail.com",
                Email= "Admin@gmail.com",
                EmailConfirmed=true,
               



            };
            var userisExistsinDB=await userManager.FindByEmailAsync(Admin.Email);  
            if (userisExistsinDB==null) {
                await userManager.CreateAsync(Admin, "Admin@123");
                await userManager.AddToRoleAsync(Admin, Roles.Admin.ToString());
            }


        }
    }
}
