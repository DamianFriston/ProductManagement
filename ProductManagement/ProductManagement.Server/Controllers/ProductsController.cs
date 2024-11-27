using Microsoft.AspNetCore.Mvc;
using ProductManagement.Server.DTO;
using ProductManagement.Server.Service;

namespace ProductManagement.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {            
            var productsRes = await _service.GetProductsAsync();

            if (!productsRes.IsSuccess)
            {
                return StatusCode(500, productsRes.Message);
            }

            return Ok(productsRes.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var productRes = await _service.GetProductByIdAsync(id);

            if (!productRes.IsSuccess)
            {
                return StatusCode(404, productRes.Message);
            }                     

            return Ok(productRes.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productDto)
        {
            var productRes = await _service.AddProductAsync(productDto);

            if (!productRes.IsSuccess)
            {
                return StatusCode(500, productRes.Message);
            }

            return StatusCode(201, productRes.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productRes = await _service.DeleteProductAsync(id);

            if (!productRes.IsSuccess)
            {
                return StatusCode(500, productRes.Message);
            }

            return StatusCode(204, "Product deleted");
        }
    }
}
