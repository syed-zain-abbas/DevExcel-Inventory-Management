using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsApiController(
            ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id:int}/stock")]
        [ProducesResponseType(typeof(ProductStockDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductStockDto>> GetStock(int id)
        {
            var stock = await _productService.GetStockAsync(id);

            if (stock is null)
            {
                return NotFound(new
                {
                    message = "Product not found."
                });
            }

            return Ok(stock);
        }

    }
}
