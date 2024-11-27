using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagement.Server.DTO
{
    public class ProductDTO
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        
        [SwaggerSchema(ReadOnly = true)]
        public string? CategoryName { get; set; }
        
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public DateTime DateAdded { get; set; }
        public int CategoryId { get; set; }
    }
}
