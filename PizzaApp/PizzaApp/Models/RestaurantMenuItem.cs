using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class RestaurantMenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
