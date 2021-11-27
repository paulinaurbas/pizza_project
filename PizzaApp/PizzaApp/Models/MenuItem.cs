using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public string Description { get; set; }

        [Required, Range(0, 10000)]
        public double Price { get; set; }

        [Required, Range(0, 100)]
        public float PercentDiscount { get; set; }

        [Required]
        public string Image { get; set; }

        public ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }
        public ICollection<MenuItemDish> MenuItemDishes { get; set; }
    }
}
