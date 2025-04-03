using System.ComponentModel.DataAnnotations;

namespace MobileWalletAdmin.Enum
{
    public enum DocumentType
    {
       
        NRC,

        [Display(Name = "Passport")]
        PASSPORT,

        [Display(Name = "Driver License")]
        DRIVER_LICENSE,
        UTILITY_BILL,
        BANK_STATEMENT,
        PAYSLIP,
        TAX_DOCUMENT,
        BIRTH_CERTIFICATE,
        MARRIAGE_CERTIFICATE,
        RESIDENCE_PERMIT,
        WORK_PERMIT,
        OTHER

    }
}
