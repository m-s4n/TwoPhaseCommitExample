namespace Shared
{
    public class TransactionData
    {
        public int OrderNumber { get; set; }
        public string Item { get; set; }
        public float Price { get; set; }
        public Guid TransactionId { get; set; }
    }
}