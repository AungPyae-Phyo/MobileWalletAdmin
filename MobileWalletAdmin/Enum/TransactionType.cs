using System.ComponentModel.DataAnnotations;

namespace MobileWalletAdmin.Enum
{
    public enum TransactionType
    {
        [Display(Name = "Deposit")]
        DEPOSIT,

        [Display(Name = "Withdrawal")]
        WITHDRAWAL,

        [Display(Name = "Transfer")]
        TRANSFER,

        [Display(Name = "Loan")]
        LOAN,

        [Display(Name = "Payment")]
        PAYMENT,

        [Display(Name = "Refund")]
        REFUND,

        [Display(Name = "Exchange")]
        EXCHANGE,

        [Display(Name = "Investment")]
        INVESTMENT,
        DIVIDEND,
        INTEREST,
        FEE,
        CHARGE,
        REWARD,
        BONUS,
        COMMISSION,

        [Display(Name = "Discount")]
        DISCOUNT,

        [Display(Name = "Gift Card")]
        GIFT_CARD
    }
}
