using ApiWrapper.Provider;
using DataLayer.IRepository;
using DataLayer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Adapter.IApiWrapper;
using ServiceLayer.Services;
using System;
using System.IO;

namespace AssessmentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            // calls the Run method in App, which is replacing Main
            serviceProvider.GetService<App>().Run();
        }
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);
            services.AddTransient<ICoordinatesProvider, CoordinatesProvider>();
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddTransient<AddressService, AddressService>();
            // required to run the application
            services.AddTransient<App>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
