using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }


        public int ClientId { get; set; }
        public Client Client { get; set; }


        [Required, MaxLength(250)]
        public string Description { get; set; }

        public ICollection<OrderMenuItem> OrderMenuItems { get; set; }
    }
}
