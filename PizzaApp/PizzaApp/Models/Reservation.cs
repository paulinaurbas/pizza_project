using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Range(0, 100)]
        public int? NumberOfSeats { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }

        public ICollection<ReservationRestaurantTable> ReservationRestaurantTables { get; set; }
    }
}
