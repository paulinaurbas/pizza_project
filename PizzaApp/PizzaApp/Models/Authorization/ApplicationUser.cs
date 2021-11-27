using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PizzaApp.Models.Authorization
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
