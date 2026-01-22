using System.ComponentModel.DataAnnotations;

namespace FoodieDash.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
