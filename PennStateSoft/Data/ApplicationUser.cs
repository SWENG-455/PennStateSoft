using Microsoft.AspNetCore.Identity;
using PennStateSoft.Components.Account.Pages.Manage;

namespace PennStateSoft.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        private static List<ApplicationUser>? Admins;
        private static ApplicationUser Admin;
        private IdentityRole? Role;
        private ApplicationUser CreateAdminUser()
        {
            if (Admins == null)
            {
                try
                {
                    Admins = new List<ApplicationUser>();

                    Admins?.Add(Activator.CreateInstance<ApplicationUser>());
                    Admin = Admins.LastOrDefault();
                    Admin.Role = new IdentityRole("Administrator");
                }
                catch
                {
                    throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                        $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
                } 
            }

            return Admin;
        }

        //Passes an instance of ApplicationUser as an argument
        public string GetRole(ApplicationUser user)
        {
            return user.Role?.Name ?? "None";
        }

        //Uses the current ApplicationUser instance to the call
        public string GetRole()
        {
            return Role?.Name ?? "None";
        }

        public static string GetAdminUser(ApplicationUser user)
        {
            string? UserName = "null";
            if (Admins != null)
            {
                foreach (ApplicationUser admin in Admins)
                {
                    if (user == admin)
                    {
                        Admin = admin;
                        UserName = Admin.UserName;
                        break;
                    }
                }
            }
            return UserName ?? "null";
        }

    }

}
