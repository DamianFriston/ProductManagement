namespace ProductManagement.Server.Model
{
    public class Product
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public DateTime DateAdded { get; set; }       
        public int CategoryId { get; set; }        
    }
}


