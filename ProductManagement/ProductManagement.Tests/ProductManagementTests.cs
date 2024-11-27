using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProductManagement.Server.Data;
using ProductManagement.Server.DTO;
using ProductManagement.Server.Model;
using ProductManagement.Server.Service;
using System.Linq;
using System.Net;

namespace ProductManagementTests.Tests
{
    public class ProductDbContextTests
    {
        private DbContextOptions<ProductContext> CreateInMemoryDatabase()
        {       
            return new DbContextOptionsBuilder<ProductContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;
        }

        private ProductDTO testProductDto;
        private CategoryDTO testCategoryDto;

        private ProductDTO CreateTestProductDto()
        {
            return new ProductDTO
            {
                ProductName = "Test Product",
                ProductCode = "TEST0000",
                Price = 9.99m,
                StockQty = 15,
                DateAdded = DateTime.Now,
                CategoryId = 1
            };
        }

        private CategoryDTO CreateTestCategoryDto()
        {
            return new CategoryDTO
            {
                CategoryName = "Test Category"
            };
        }

        // Ensure that the test DTO changes do not persist across tests when run together
        [SetUp]
        public void Setup()
        {            
            testProductDto = CreateTestProductDto();
            testCategoryDto = CreateTestCategoryDto();
        }
    
        // PRODUCT TESTS
        [Test]
        public async Task CanAddAndRetrieveProduct()
        {
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
               
                var productService = new ProductService(context);
                var addResponse = await productService.AddProductAsync(testProductDto);
                Assert.IsTrue(addResponse.IsSuccess);

                var getResponse = await productService.GetProductByIdAsync(addResponse.Data.Id);
                Assert.That(getResponse.Data.ProductName, Is.EqualTo("Test Product"));             
            }
        }               

        [Test]
        public async Task CannotAddProductToInvalidCategory()
        {
            var invalidCategoryId = -1;
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                testProductDto.CategoryId = invalidCategoryId;
                var addResponse = await productService.AddProductAsync(testProductDto);
                Assert.IsFalse(addResponse.IsSuccess);
                Assert.IsTrue(addResponse.Message == "The Product Category does not exist");              
            }
        }

        [Test]
        public async Task ProductCodeMustBeUnique()
        {            
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                testProductDto.ProductCode = "DUPLICATE";
                var addResponse = await productService.AddProductAsync(testProductDto);

                var duplicateCodeProduct = new ProductDTO
                {
                    ProductName = "Duplicate Product",
                    ProductCode = "DUPLICATE",
                    Price = 9.99m,
                    StockQty = 15,
                    DateAdded = DateTime.Now,
                    CategoryId = 1
                };

                var addDuplicateResponse = await productService.AddProductAsync(duplicateCodeProduct);
                Assert.IsFalse(addDuplicateResponse.IsSuccess);
                Assert.IsTrue(addDuplicateResponse.Message == "The Product Code must be unique");
            }
        }

        [Test]
        public async Task ProductAddDateCannotBeInFuture()
        {
            var invalidProductDate = DateAndTime.Now.AddDays(1);

            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                testProductDto.DateAdded = invalidProductDate;
                var addResponse = await productService.AddProductAsync(testProductDto);
                Assert.IsFalse(addResponse.IsSuccess);
                Assert.IsTrue(addResponse.Message == "The DateAdded cannot be in the future");
            }
        }

        [Test]
        public async Task ProductPriceCannotBeNegative()
        {
            var invalidPrice = -1m;           
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                testProductDto.Price = invalidPrice;
                var addResponse = await productService.AddProductAsync(testProductDto);
                Assert.IsFalse(addResponse.IsSuccess);
                Assert.IsTrue(addResponse.Message == "The Price cannot be negative");
            }
        }

        [Test]
        public async Task CanDeleteProduct()
        {
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                var addResponse = await productService.AddProductAsync(testProductDto);              

                var deleteResponse = await productService.DeleteProductAsync(addResponse.Data.Id);
                Assert.IsTrue(deleteResponse.IsSuccess);
                var getResponse = await productService.GetProductByIdAsync(addResponse.Data.Id);
                Assert.IsFalse(getResponse.IsSuccess);
            }
        }

        // CATEGORIES TESTS
        [Test]
        public async Task CanAddAndRetrieveCategory()
        {
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                var addResponse = await productService.AddCategoryAsync(testCategoryDto);              
                Assert.IsTrue(addResponse.IsSuccess);

                var getResponse = await productService.GetCategoryByIdAsync(addResponse.Data.Id);
                Assert.That(getResponse.Data.CategoryName, Is.EqualTo("Test Category"));
            }
        }

        [Test]
        public async Task CannotAddDuplicateCategory()
        {
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                var addResponse = await productService.AddCategoryAsync(testCategoryDto);             
                var addDuplicateResponse = await productService.AddCategoryAsync(testCategoryDto);
                Assert.IsFalse(addDuplicateResponse.IsSuccess);
                Assert.IsTrue(addDuplicateResponse.Message == "Category already exists");
            }
        }

        [Test]
        public async Task CanDeleteCategory()
        {
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);            
                var addResponse = await productService.AddCategoryAsync(testCategoryDto);

                var deleteResponse = await productService.DeleteCategoryAsync(addResponse.Data.Id);
                Assert.IsTrue(deleteResponse.IsSuccess);

                var getResponse = await productService.GetCategoryByIdAsync(addResponse.Data.Id);
                Assert.IsFalse(getResponse.IsSuccess);
            }
        }

        [Test]
        public async Task CannotDeleteCategoryWithAssociatedProduct()
        {
            var options = CreateInMemoryDatabase();
            using (var context = new ProductContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var productService = new ProductService(context);
                var addResponse = await productService.AddCategoryAsync(testCategoryDto);

                // Create a reference to the Category from the Product
                testProductDto.CategoryId = addResponse.Data.Id;
                var addProductResponse = await productService.AddProductAsync(testProductDto);

                var deleteResponse = await productService.DeleteCategoryAsync(addResponse.Data.Id);
                Assert.IsFalse(deleteResponse.IsSuccess);
                Assert.IsTrue(deleteResponse.Message == "Cannot remove Category as there are Products assigned to it");
            }
        }
    }
}
