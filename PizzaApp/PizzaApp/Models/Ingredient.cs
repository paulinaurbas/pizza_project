using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<DishIngredient> DishIngredients { get; set; }
        public ICollection<RestaurantIngredient> RestaurantIngredients { get; set; }
    }
}
