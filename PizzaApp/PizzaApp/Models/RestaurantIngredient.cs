using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class RestaurantIngredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [Required, Range(0, 10000)]
        public double Quantity { get; set; }
    }
}
