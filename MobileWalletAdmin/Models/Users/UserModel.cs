﻿using MobileWalletAdmin.Enum;
using System.Diagnostics.SymbolStore;
using System.Text.Json.Serialization;

namespace MobileWalletAdmin.Models.Users
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentType? DocumentType { get; set; }

        public Guid WalletID { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BankStatus Status { get; set; }
        public decimal Balance { get; set; }
        public DateTime createdOn { get; set; }
    }
}
