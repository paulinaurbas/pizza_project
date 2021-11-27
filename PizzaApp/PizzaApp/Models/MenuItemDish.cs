using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class MenuItemDish
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        [Required]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        [Required, Range(0, 10000)]
        public double Quantity { get; set; }

        [Required]
        public bool IsOptional { get; set; }

        [Required, Range(0, 10000)]
        public double AdditionalPrice { get; set; }
    }
}
