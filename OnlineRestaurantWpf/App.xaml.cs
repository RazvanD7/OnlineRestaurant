using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.ViewModels;
using OnlineRestaurantWpf.Views;
using System;
using System.IO;
using System.Windows;
using OnlineRestaurantWpf.BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurantWpf.Helpers;
using Microsoft.Extensions.Logging;
using OnlineRestaurantWpf.Converters;

namespace OnlineRestaurantWpf
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;
        public IConfiguration Configuration { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Set global menu discount from config
            var discountStr = Configuration["AppSettings:MenuDiscountPercentageX"];
            if (decimal.TryParse(discountStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var discount))
            {
                MenuPriceConverter.GlobalMenuDiscountPercentage = discount;
            }

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddTransient<ConfigHelper>();

            services.AddDbContextFactory<RestaurantDbContext>(options =>
            {
                string? connectionString = Configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
                }
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<Func<RestaurantDbContext>>(sp =>
                () => sp.GetRequiredService<IDbContextFactory<RestaurantDbContext>>().CreateDbContext());

            services.AddTransient<UserBLL>();
            services.AddTransient<CategoryBLL>();
            services.AddTransient<DishBLL>();
            services.AddTransient<MenuBLL>();
            services.AddTransient<AllergenBLL>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<ClientMenuViewModel>();

            services.AddSingleton<MainViewModel>();

            services.AddTransient<MainWindow>();
        }
    }
}