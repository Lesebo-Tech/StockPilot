
using System.ComponentModel.DataAnnotations;

namespace StockPilot.Api.DTOs
{
    public class StockInDto
    {
        [Range(1, int.MaxValue,
            ErrorMessage = "Product ID must be greater than zero.")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "A reason is required.")]
        [StringLength(250,
            ErrorMessage = "Reason cannot exceed 250 characters.")]
        public string Reason { get; set; } = string.Empty;
    }
}
