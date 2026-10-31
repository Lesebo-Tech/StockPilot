
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockPilot.Api.Data;
using StockPilot.Api.DTOs;
using StockPilot.Api.Services;

namespace StockPilot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;
        private readonly ApplicationDbContext _context;

        public InventoryController(
            InventoryService inventoryService,
            ApplicationDbContext context)
        {
            _inventoryService = inventoryService;
            _context = context;
        }

        // POST: api/inventory/stock-in
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

        // POST: api/inventory/stock-out
        [HttpPost("stock-out")]
        public async Task<IActionResult> StockOut(
            [FromBody] StockInDto request)
        {
            try
            {
                var movement = await _inventoryService.StockOutAsync(
                    request.ProductId,
                    request.Quantity,
                    request.Reason
                );

                return Ok(new
                {
                    Message = "Stock removed successfully.",
                    MovementId = movement.Id,
                    ProductId = movement.ProductId,
                    MovementType = movement.MovementType,
                    QuantityRemoved = movement.Quantity,
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
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    Message = ex.Message
                });
            }
        }

        // GET: api/inventory/movements
        [HttpGet("movements")]
        public async Task<IActionResult> GetMovements()
        {
            var movements = await _context.StockMovements
                .AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .ThenByDescending(m => m.Id)
                .ToListAsync();

            return Ok(movements);
        }
    }
}
