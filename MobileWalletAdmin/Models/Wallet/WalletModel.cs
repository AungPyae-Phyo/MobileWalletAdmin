namespace MobileWalletAdmin.Models.Wallet
{
    public class WalletModel
    {

        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
