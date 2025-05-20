using Microsoft.Extensions.Configuration;
using System;

namespace OnlineRestaurantWpf.Helpers
{
    public class ConfigHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string name = "DefaultConnection")
        {
            return _configuration.GetConnectionString(name)
                   ?? throw new InvalidOperationException($"Connection string '{name}' not found.");
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

        public decimal MenuDiscountPercentageX => GetAppSetting<decimal>("MenuDiscountPercentageX");
    }
}