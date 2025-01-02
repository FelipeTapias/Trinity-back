using Core.Enums;

namespace Core.Entities
{
    public class CustomerUser: User
    {
        public string CustomerId { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
