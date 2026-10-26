
using System.ComponentModel.DataAnnotations;

namespace StockPilot.Api.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;

        [Range(
            typeof(decimal),
            "0.01",
            "999999999",
            ParseLimitsInInvariantCulture = true)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; } = 5;
    }
}
