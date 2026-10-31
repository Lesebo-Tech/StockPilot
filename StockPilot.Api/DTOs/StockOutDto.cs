using System.ComponentModel.DataAnnotations;

namespace StockPilot.Api.DTOs
{
    public class StockOutDto
    {
        [Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        public string? Reason { get; set; }
    }
}