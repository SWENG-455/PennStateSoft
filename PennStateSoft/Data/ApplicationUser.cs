using Microsoft.AspNetCore.Identity;
using PennStateSoft.Components.Account.Pages.Manage;

namespace PennStateSoft.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        private static List<ApplicationUser>? Admins;
        private static ApplicationUser? Admin;
        private IdentityRole? Role;
        private ApplicationUser CreateAdminUser()
        {
            if (Admins == null)
            {
                Admins = []; 
            }
            try
            {
                Admins?.Add(Activator.CreateInstance<ApplicationUser>());
                Admin = Admins?.LastOrDefault();
                Admin!.Role = new IdentityRole("Admin");
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
            }

            return Admin;
        }
        public ApplicationUser CreateAdminAccount(ApplicationUser user)
        {
            if (GetRole(user).Equals("None"))
            {
                throw new InvalidOperationException($"You do not have access to this resource");
            }
            else
            {
                return CreateAdminUser();
            } 
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
