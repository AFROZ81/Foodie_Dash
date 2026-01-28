using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieDash.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageUrl { get; set; } // We will store the link to the image

        [Range(1, 2000)]
        public double Price { get; set; }

        [Display(Name = "Veg / Non-Veg")]
        public string? Type { get; set; }

        [Display(Name = "Menu Course")]
        public string? Course { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
