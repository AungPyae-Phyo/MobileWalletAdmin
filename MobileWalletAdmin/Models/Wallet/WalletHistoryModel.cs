namespace MobileWalletAdmin.Models.Wallet
{
    public class WalletHistoryModel
    {
        public string Id { get; set; }
        public string WalletId { get; set; }
        public decimal ChangeAmount { get; set; }
        public decimal NewBalance { get; set; }
        public decimal PreviousBalance { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
