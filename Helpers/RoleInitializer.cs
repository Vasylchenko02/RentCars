using RentCarsApp.Models;  // пространство имен модели User
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace RentCarsApp.Helpers
{

    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var password = "qwerty12";
            var adminRole = "admin";
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            //if (await roleManager.FindByNameAsync("employee") == null)
            //{
            //    await roleManager.CreateAsync(new IdentityRole("employee"));
            //}

            var adminIdentity = await userManager.FindByNameAsync(adminEmail);

            if (adminIdentity == null)
            {
                adminIdentity = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                var result = await userManager.CreateAsync(adminIdentity, password);
                if (!result.Succeeded)
                    return;
            }
            if (!await userManager.IsInRoleAsync(adminIdentity, adminRole))
                await userManager.AddToRoleAsync(adminIdentity, adminRole);
        }
    }
}