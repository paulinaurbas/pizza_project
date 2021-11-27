using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class OrderMenuItemDish
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderMenuItemId { get; set; }
        public OrderMenuItem OrderMenuItem { get; set; }

        public int? MenuItemDishId { get; set; }
        public MenuItemDish MenuItemDish { get; set; }
    }
}
