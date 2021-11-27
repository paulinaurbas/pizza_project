using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class ReservationRestaurantTable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int? RestaurantTableId { get; set; }
        public RestaurantTable RestaurantTable { get; set; }
    }
}
