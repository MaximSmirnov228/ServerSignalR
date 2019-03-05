using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ServerNetCore
{
    public static class MyIdentityDataInitializer
    {
        private const string RoleUser = "User";

        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                (RoleUser).Result)
            {
                var role = new IdentityRole();
                role.Name = RoleUser;
                //role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            string login = "admin@gmail.com";
            string password = "12345";

            if (userManager.FindByNameAsync
                    (login).Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = login;
                user.Email = login;

                IdentityResult result = userManager.CreateAsync
                    (user, password).Result;

                if (result.Succeeded)
                {
                    var resWait = userManager.AddToRoleAsync(user,
                        RoleUser).Result;
                }
            }
        }
    }

}
