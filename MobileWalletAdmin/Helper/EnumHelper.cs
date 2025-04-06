using MobileWalletAdmin.Enum;
using System.ComponentModel.DataAnnotations;
using MudBlazor; // Add this for MudBlazor's Color type
using System.Reflection;

namespace MobileWalletAdmin.Helper
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(System.Enum? value)
        {
            if (value == null)
            {
                return "Unknown";
            }

            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();

            return attribute?.Name ?? value.ToString();
        }

        public static MudBlazor.Color GetStatusColor(BankStatus status)
        {
            return status switch
            {
                BankStatus.NEW_USER => MudBlazor.Color.Primary,
                BankStatus.ACTIVE => MudBlazor.Color.Success,
                BankStatus.INACTIVE => MudBlazor.Color.Default,
                BankStatus.PENDING => MudBlazor.Color.Warning,
                BankStatus.SUSPENDED => MudBlazor.Color.Error,
                BankStatus.APPROVE => MudBlazor.Color.Info,
                BankStatus.REJECT => MudBlazor.Color.Error,
                _ => MudBlazor.Color.Secondary
            };
        }
    }
}