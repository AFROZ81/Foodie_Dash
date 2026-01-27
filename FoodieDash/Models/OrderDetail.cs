using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieDash.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public OrderHeader? OrderHeader { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        public MenuItem? MenuItem { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
    }
}