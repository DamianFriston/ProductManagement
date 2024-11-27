using ProductManagement.Server.DTO;
using ProductManagement.Server.Model;

namespace ProductManagement.Server.Service
{
    public interface IProductService
    {
        public Task<ServiceResponse<IEnumerable<ProductDTO>>> GetProductsAsync();
        public Task<ServiceResponse<ProductDTO>> GetProductByIdAsync(int id);
        public Task<ServiceResponse<Product>> AddProductAsync(ProductDTO productDto);
        public Task<ServiceResponse<bool>> DeleteProductAsync(int id);

        public Task<ServiceResponse<IEnumerable<CategoryDTO>>> GetCategoriesAsync();
        public Task<ServiceResponse<CategoryDTO>> GetCategoryByIdAsync(int id);
        public Task<ServiceResponse<Category>> AddCategoryAsync(CategoryDTO productDto);
        public Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
    }
}
