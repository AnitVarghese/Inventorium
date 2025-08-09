using System.ComponentModel.DataAnnotations;

namespace Inventorium.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public required string ItemName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public required string Unit { get; set; }  // Kg, Litre, etc.

        [Required]
        public decimal Price { get; set; }

        [Required]
        public required string Currency { get; set; }  // INR, USD, JPY

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }
    }
}
