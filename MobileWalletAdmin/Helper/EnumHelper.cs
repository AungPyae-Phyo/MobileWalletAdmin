using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MobileWalletAdmin.Helper
{
    public static class EnumHelper  
    {
        public static string GetEnumDescription(System.Enum value)
        {
            // Get the field info for the enum value
            var field = value.GetType().GetField(value.ToString());

            // Look for the Display attribute on the enum field
            var attribute = field.GetCustomAttribute<DisplayAttribute>();

            // If found, return the description, otherwise fallback to enum's string name
            return attribute != null ? attribute.Name : value.ToString();
        }
    }
}
