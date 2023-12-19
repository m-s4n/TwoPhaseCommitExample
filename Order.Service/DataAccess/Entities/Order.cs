using Order.Service.Enums;

namespace Order.Service.DataAccess.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Item { get; set; }
        public OrderPreparationStatus PreparationStatus { get; set; }
        public Guid TransactionId { get; set; }
    }
}
