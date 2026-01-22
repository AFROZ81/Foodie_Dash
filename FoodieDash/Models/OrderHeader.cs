using System.ComponentModel.DataAnnotations;

namespace FoodieDash.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PickupName { get; set; } = "";

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
    }
}