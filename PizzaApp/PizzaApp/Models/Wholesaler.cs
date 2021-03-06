using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Wholesaler
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        public string Address { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Telephone{ get; set; }
    }
}
