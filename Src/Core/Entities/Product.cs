using Core.Emuns;

namespace Core.Entities
{
    public class Product
    {
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public ProductTypes Type { get; set; }
        public ProductStatus Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
