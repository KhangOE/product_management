using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public ICollection<Order> Orders { get; }
        // [JsonExtensionData]
        public Dictionary<int, Address> Adress { get; set; }  = new Dictionary<int, Address>();
        public Dictionary<int, Product> ProductsSaved { get; set; } = new Dictionary<int, Product>();

        [NotMapped]
        private Dictionary<int, Order> peopleDictionary = null;

        [NotMapped]
        public Dictionary<int, Order> PeopleDictionary
        {
            get
            {
                if (this.peopleDictionary == null)
                {
                    this.peopleDictionary = this.Orders?.ToDictionary(x => x.Id);
                }

                return this.peopleDictionary;
            }
        }
    };
}
