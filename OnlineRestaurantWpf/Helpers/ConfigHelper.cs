using Microsoft.Extensions.Configuration;
using System;

namespace OnlineRestaurantWpf.Helpers
{
    public class ConfigHelper
    {
        private readonly IConfiguration _configuration;

        // Constructor for Dependency Injection
        public ConfigHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string name = "DefaultConnection")
        {
            return _configuration.GetConnectionString(name)
                   ?? throw new InvalidOperationException($"Connection string '{name}' not found.");
        }

        public string GetAppSetting(string key)
        {
            return _configuration[$"AppSettings:{key}"]
                   ?? throw new InvalidOperationException($"App setting '{key}' not found.");
        }

        public T GetAppSetting<T>(string key) where T : IConvertible
        {
            string? value = _configuration[$"AppSettings:{key}"];
            if (value == null)
            {
                throw new InvalidOperationException($"App setting '{key}' not found.");
            }
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error converting app setting '{key}' with value '{value}' to type {typeof(T).Name}.", ex);
            }
        }

        // Specific configuration getters (examples)
        public decimal MenuDiscountPercentageX => GetAppSetting<decimal>("MenuDiscountPercentageX");
        public decimal OrderDiscountThresholdY => GetAppSetting<decimal>("OrderDiscountThresholdY");
        public int OrderCountForDiscountZ => GetAppSetting<int>("OrderCountForDiscountZ");
        public int OrderTimeIntervalT => GetAppSetting<int>("OrderTimeIntervalT");
        public decimal OrderDiscountPercentageW => GetAppSetting<decimal>("OrderDiscountPercentageW");
        public decimal MinOrderValueForFreeShippingA => GetAppSetting<decimal>("MinOrderValueForFreeShippingA");
        public decimal ShippingCostB => GetAppSetting<decimal>("ShippingCostB");
        public int LowStockThresholdC => GetAppSetting<int>("LowStockThresholdC");
    }
}