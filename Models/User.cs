using System.ComponentModel.DataAnnotations;

namespace Product_management.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(6)]
        public string Password { get; set; }
        public ICollection<Order> Orders { get; } = new List<Order>();

    };
}
