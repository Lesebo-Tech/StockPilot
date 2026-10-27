
using Microsoft.AspNetCore.Mvc;
using StockPilot.Api.DTOs;
using StockPilot.Api.Services;

namespace StockPilot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(
            InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("stock-in")]
        public async Task<IActionResult> StockIn(
            [FromBody] StockInDto request)
        {
            try
            {
                var movement = await _inventoryService.StockInAsync(
                    request.ProductId,
                    request.Quantity,
                    request.Reason
                );

                return Ok(new
                {
                    Message = "Stock received successfully.",
                    MovementId = movement.Id,
                    ProductId = movement.ProductId,
                    MovementType = movement.MovementType,
                    QuantityAdded = movement.Quantity,
                    PreviousQuantity = movement.PreviousQuantity,
                    NewQuantity = movement.NewQuantity,
                    Reason = movement.Reason,
                    CreatedAt = movement.CreatedAt
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    Message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
            catch (OverflowException)
            {
                return BadRequest(new
                {
                    Message = "Stock quantity exceeds the allowed limit."
                });
            }
        }
    }
}
