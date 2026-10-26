
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockPilot.Api.Data;
using StockPilot.Api.Models;
using StockPilot.Api.DTOs;


namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(
            CreateProductDto request)
        {
            string name = request.Name?.Trim() ?? "";
            string sku = request.SKU?.Trim().ToUpperInvariant() ?? "";

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(sku))
            {
                return BadRequest("Product name and SKU are required.");
            }

            bool skuExists = await _context.Products
                .AnyAsync(p => p.SKU == sku);

            if (skuExists)
            {
                return Conflict("A product with this SKU already exists.");
            }

            var product = new Product
            {
                Name = name,
                SKU = sku,
                Price = request.Price,
                Quantity = request.Quantity,
                ReorderLevel = request.ReorderLevel
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = product.Id },
                product
            );
        }

    }
}
