using Core.Emuns;

namespace Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public ProductTypes Type { get; set; }
        public ProductStatus Status { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public decimal InitialValuePaid { get; set; } = 0;
        public string Size { get; set; }
        public List<string> AdditionalInformation { get; set; } = new List<string>();
        public List<string> PaymentInformation { get; set; } = new List<string>();
    }
}
