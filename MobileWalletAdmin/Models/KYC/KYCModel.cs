using MobileWalletAdmin.Enum;
using System.Text.Json.Serialization;

namespace MobileWalletAdmin.Models.KYC
{
    public class KYCModel
    {
        public string Id { get; set; }  
        public string UserID { get; set; }
        public string? Name { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentType? DocumentType { get; set; }
        public string? Identity { get; set; }
        public string? Address { get; set; }
        public string? FrontImage { get; set; }
        public string? BackImage { get; set; }
        public string? SelfieImage { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? FatherName { get; set; }
        public string AccountNumber { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BankStatus Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get;set; }
        public DateTime createdOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
