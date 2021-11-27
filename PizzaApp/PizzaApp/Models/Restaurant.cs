using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required, MaxLength(200)]
        public string Address { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Telephone { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }
        public ICollection<RestaurantIngredient> RestaurantIngredients { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<RestaurantTable> RestaurantTables { get; set; }
    }
}
