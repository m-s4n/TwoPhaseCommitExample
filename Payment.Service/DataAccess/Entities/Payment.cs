using Payment.Service.Enums;

namespace Payment.Service.DataAccess.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public  PaymentStatus PaymentMode { get; set; }
        public Guid TransactionId { get; set; }
        public int OrderNumber { get; set; }
    }
}
