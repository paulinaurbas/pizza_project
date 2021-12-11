﻿using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.Authorization
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
