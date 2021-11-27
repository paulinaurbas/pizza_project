using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }


        [Required]
        public int ComplaintStatusId { get; set; }
        public ComplaintStatus ComplaintStatus { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
