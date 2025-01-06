using Core.Enums;

namespace Core.Entities
{
    public class Payment
    {
        public string PaymentId { get; set; }
        public string ProductId { get; set; }
        public PaymentTypes Type { get; set; }
        public decimal Value { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
