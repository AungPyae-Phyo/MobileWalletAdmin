namespace MobileWalletAdmin.Models.TransactionHistory
{
    public class TransactionHistoryModel
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string? Description { get; set; }
    }
}
