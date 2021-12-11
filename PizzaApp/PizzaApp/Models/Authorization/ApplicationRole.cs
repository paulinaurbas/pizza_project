using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PizzaApp.Models.Authorization
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
