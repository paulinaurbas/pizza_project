using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class DishIngredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        [Required]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
