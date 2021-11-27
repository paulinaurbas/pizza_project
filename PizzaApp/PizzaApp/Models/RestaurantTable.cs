using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class RestaurantTable
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, Range(0,100)]
        public int AvailableSeats { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<ReservationRestaurantTable> ReservationRestaurantTables { get; set; }
    }
}
