
using Microsoft.EntityFrameworkCore;
using StockPilot.Api.Data;
using StockPilot.Api.Models;

namespace StockPilot.Api.Services
{
    public class InventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StockMovement> StockInAsync(
            int productId,
            int quantity,
            string reason)
        {
            // Validate the quantity.
            if (quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }

            // Validate the reason.
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException(
                    "A reason is required.");
            }

            reason = reason.Trim();

            if (reason.Length > 250)
            {
                throw new ArgumentException(
                    "Reason cannot exceed 250 characters.");
            }

            // Start a transaction to protect stock updates.
            await using var transaction =
                await _context.Database.BeginTransactionAsync(
                    System.Data.IsolationLevel.Serializable);

            // Find the product.
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new KeyNotFoundException(
                    "Product not found.");
            }

            // Remember the quantity before the update.
            int previousQuantity = product.Quantity;

            // Add the new stock safely.
            product.Quantity = checked(
                product.Quantity + quantity);

            // Record the stock movement.
            var movement = new StockMovement
            {
                ProductId = productId,
                MovementType = "IN",
                Quantity = quantity,
                PreviousQuantity = previousQuantity,
                NewQuantity = product.Quantity,
                Reason = reason,
                CreatedAt = DateTime.UtcNow
            };

            _context.StockMovements.Add(movement);

            // Save the quantity and movement together.
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return movement;
        }

        public async Task<StockMovement> StockOutAsync (
            int productId,
            int quantity,
            string reason)
        {
            // Validate the quantity.
            if (quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }
            // Validate the reason.
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException(
                    "A reason is required.");
            }
            reason = reason.Trim();
            if (reason.Length > 250)
            {
                throw new ArgumentException(
                    "Reason cannot exceed 250 characters.");
            }
            // Start a transaction to protect stock updates.
            await using var transaction =
                await _context.Database.BeginTransactionAsync(
                    System.Data.IsolationLevel.Serializable);
            // Find the product.
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                throw new KeyNotFoundException(
                    "Product not found.");
            }
            // Check for sufficient stock.
            if (product.Quantity < quantity)
            {
                throw new InvalidOperationException(
                    "Insufficient stock available.");
            }
            // Remember the quantity before the update.
            int previousQuantity = product.Quantity;
            // Remove the stock safely.
            product.Quantity -= quantity;
            // Record the stock movement.
            var movement = new StockMovement
            {
                ProductId = productId,
                MovementType = "OUT",
                Quantity = quantity,
                PreviousQuantity = previousQuantity,
                NewQuantity = product.Quantity,
                Reason = reason,
                CreatedAt = DateTime.UtcNow
            };
            _context.StockMovements.Add(movement);
            // Save the quantity and movement together.
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return movement;
        }
    }
}
