using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Server.Data;
using ProductManagement.Server.DTO;
using ProductManagement.Server.Service;

namespace ProductManagement.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IProductService _service;

        public CategoriesController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categoriesRes = await _service.GetCategoriesAsync();

            if (!categoriesRes.IsSuccess)
            {
                return StatusCode(500, categoriesRes.Message);
            }

            return Ok(categoriesRes.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var categoryRes = await _service.GetCategoryByIdAsync(id);

            if (!categoryRes.IsSuccess)
            {
                return StatusCode(404, categoryRes.Message);
            }

            return Ok(categoryRes.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDto)
        {
            var categoryRes = await _service.AddCategoryAsync(categoryDto);

            if (!categoryRes.IsSuccess)
            {
                return StatusCode(500, categoryRes.Message);
            }

            return StatusCode(201, categoryRes.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryRes = await _service.DeleteCategoryAsync(id);

            if (!categoryRes.IsSuccess)
            {
                return StatusCode(500, categoryRes.Message);
            }
            return StatusCode(204, "Category deleted");
        }
    }
}
