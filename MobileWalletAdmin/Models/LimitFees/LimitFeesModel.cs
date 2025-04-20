using MobileWalletAdmin.Enum;
using System.Text.Json.Serialization;

namespace MobileWalletAdmin.Models.LimitFees
{
    public class LimitFeesModel
    {
        public string? Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType TransactionType { get; set; }
        public string? description { get; set; }
        public decimal min_amount { get; set; }
        public decimal max_amount { get; set; }
        public decimal percent_fees { get; set; }
        public bool is_active { get; set; } = true;
    }
}
