using FlowardApp.Services.CatalogService.Dtos;
using FlowardApp.Services.CatalogService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowardApp.Services.CatalogService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;   
                var productService = serviceProvider.GetRequiredService<IProductRepository>();

                if(!productService.GetAllAsync().Result.Data.Any())
                {
                    productService.CreateAsync(new ProductCreateDto { Name = "Flower 01", Cost = 30, Price = 70 }).Wait();
                    productService.CreateAsync(new ProductCreateDto { Name = "Flower 02", Cost = 50, Price = 150 }).Wait();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
