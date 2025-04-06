using System.ComponentModel.DataAnnotations;

namespace MobileWalletAdmin.Enum
{
    public enum BankStatus
    {
        [Display(Name = "New User")]
        NEW_USER,

        [Display(Name = "Closed")]
        CLOSED,

        [Display(Name = "Active")]
        ACTIVE,

        [Display(Name = "Pending")]
        PENDING,

        [Display(Name = "Inactive")]
        INACTIVE,

        [Display(Name = "Suspended")]
        SUSPENDED,

        [Display(Name = "Approved")]
        APPROVE,

        [Display(Name = "Rejected")]
        REJECT,

        [Display(Name = "Blocked")]
        BLOCK,

        [Display(Name = "Locked")]
        LOCKED,

        [Display(Name = "Frozen")]
        FROZEN,
    }
}
