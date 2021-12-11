using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.Authorization
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<ApplicationRoleViewModel> ApplicationRoleViewModels { get; set; }

        public ApplicationUserViewModel()
        {
            ApplicationRoleViewModels = new List<ApplicationRoleViewModel>();
        }
    }
}
