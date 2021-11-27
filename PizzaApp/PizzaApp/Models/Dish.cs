using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsVegan { get; set; }

        [Required]
        public bool IsAllergenic { get; set; }

        public ICollection<MenuItemDish> MenuItemDishes { get; set; }
        public ICollection<DishIngredient> DishIngredients { get; set; }
    }
}
