using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class OrderMenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        [Required, Range(0,50)]
        public int Quantity { get; set; }

        public ICollection<OrderMenuItemDish> OrderMenuItemDishes { get; set; }
    }
}
