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
        public double Price { get; set; }

        // --- RELATIONS (The New Part) ---

        // This is the Foreign Key (links to Category table)
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // This is the Navigation Property (allows us to grab the Category name easily)
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
