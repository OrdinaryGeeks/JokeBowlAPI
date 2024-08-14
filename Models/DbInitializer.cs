namespace JokeAIAPI.Models
{
    using Microsoft.AspNetCore.Identity;
    public class DbInitializer
    {

        public static async Task Initialize(DBContext context, UserManager<User> userManager)
        {


            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "user1",
                    Email = "user1@user1.com"
                };

               // await userManager.CreateAsync(user, "BaseUser1Pass");
               // await userManager.AddToRoleAsync(user, "Member");


                var admin = new User
                {
                    UserName = "admin1",
                    Email = "admin1@admin1.com"
                };
                //await userManager.CreateAsync(admin, "BaseAdmin1Pass");
                //await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });

            }
        }
    }
}
