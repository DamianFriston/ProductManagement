using Microsoft.EntityFrameworkCore;
using ProductManagement.Server.Data;
using ProductManagement.Server.DTO;
using ProductManagement.Server.Model;

namespace ProductManagement.Server.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context;
        }
           
        
        public async Task<ServiceResponse<Product>> AddProductAsync(ProductDTO productDto)
        {
            try
            {
                if (productDto.Price < 0)
                {
                    return ServiceResponse<Product>.Failure("The Price cannot be negative");
                }
                if (productDto.DateAdded > DateTime.Now)
                {
                    return ServiceResponse<Product>.Failure("The DateAdded cannot be in the future");
                }
                if (await _context.Products.AnyAsync(p => p.ProductCode == productDto.ProductCode))
                {
                    return ServiceResponse<Product>.Failure("The Product Code must be unique");
                }

                Category? category = await _context.Categories.FindAsync(productDto.CategoryId);

                if (category == null)
                {
                    return ServiceResponse<Product>.Failure("The Product Category does not exist");
                }
                                
                Product product = new Product()
                {
                    CategoryId = productDto.CategoryId,
                    ProductCode = productDto.ProductCode,
                    ProductName = productDto.ProductName,
                    StockQty = productDto.StockQty,
                    DateAdded = productDto.DateAdded,
                    Price = decimal.Parse(productDto.Price.ToString("F2"))
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return ServiceResponse<Product>.Success(product);                                  
            }

            catch (Exception ex)
            {
                return ServiceResponse<Product>.Failure($"Error adding new product: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            try
            {                
                var product = await _context.Products.FindAsync(id);
             
                if (product == null)
                {
                    return ServiceResponse<bool>.Failure($"Could not find Product with ID: {id}");
                }
                
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
  
                return ServiceResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Failure($"Error deleting Product: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetProductsAsync()
        {
            try
            {
                var products = await _context.Products
                                 .Select(p => new ProductDTO
                                 {
                                     Id = p.Id,
                                     ProductName = p.ProductName,
                                     ProductCode = p.ProductCode,
                                     StockQty = p.StockQty,
                                     Price = p.Price,
                                     CategoryId = p.CategoryId,
                                     CategoryName = p.Category.CategoryName,
                                     DateAdded = p.DateAdded
                                 })
                                 .ToListAsync();

                if (products == null)
                {
                    return ServiceResponse<IEnumerable<ProductDTO>>.Failure($"No Products Found");
                }

                return ServiceResponse<IEnumerable<ProductDTO>>.Success(products);
            }

            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<ProductDTO>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResponse<ProductDTO>> GetProductByIdAsync(int id)
        {
            try
            {               
                var product = await _context.Products
                    .Where(p => p.Id == id)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        ProductCode = p.ProductCode,
                        StockQty = p.StockQty,
                        Price = p.Price,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.CategoryName,
                        DateAdded = p.DateAdded
                    })
                    .FirstOrDefaultAsync(); 
              
                if (product == null)
                {
                    return ServiceResponse<ProductDTO>.Failure($"Could not find Product with ID: {id}");
                }

                return ServiceResponse<ProductDTO>.Success(product);
            }

            catch (Exception ex)
            {             
                return ServiceResponse<ProductDTO>.Failure($"Error retrieving Product: {ex.Message}");
            }
        }



        public async Task<ServiceResponse<Category>> AddCategoryAsync(CategoryDTO categoryDto)
        {
            try
            {                              
                if (await _context.Categories.AnyAsync(c => c.CategoryName == categoryDto.CategoryName))
                {
                    return ServiceResponse<Category>.Failure($"Category already exists");
                }

                Category category = new Category()
                {
                   CategoryName = categoryDto.CategoryName,
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return ServiceResponse<Category>.Success(category);
            }

            catch (Exception ex)
            {
                return ServiceResponse<Category>.Failure($"Error adding new category: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                {
                    return ServiceResponse<bool>.Failure($"Could not find Category with ID: {id}");
                }

                if (await _context.Products.AnyAsync(p => p.CategoryId == category.Id))
                {
                    return ServiceResponse<bool>.Failure($"Cannot remove Category as there are Products assigned to it");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return ServiceResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Failure($"Error deleting Category: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<CategoryDTO>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories
                                 .Select(c => new CategoryDTO
                                 {
                                     Id = c.Id,
                                     CategoryName = c.CategoryName                         
                                 })
                                 .ToListAsync();

                if (categories?.Any() != true)
                {
                    return ServiceResponse<IEnumerable<CategoryDTO>>.Failure($"No Categories Found");
                }

                return ServiceResponse<IEnumerable<CategoryDTO>>.Success(categories);
            }

            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<CategoryDTO>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResponse<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.Categories
                    .Where(c => c.Id == id)
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        CategoryName = c.CategoryName
                    })
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    return ServiceResponse<CategoryDTO>.Failure($"Could not find Product with ID: {id}");
                }

                return ServiceResponse<CategoryDTO>.Success(category);
            }

            catch (Exception ex)
            {
                return ServiceResponse<CategoryDTO>.Failure($"Error retrieving Product: {ex.Message}");
            }
        }
    }
}
