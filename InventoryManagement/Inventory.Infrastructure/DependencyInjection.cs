using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using Inventory.Infrastructure.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository,
                ProductRepository>();

            services.AddScoped<IStockMovementRepository,
                StockMovementRepository>();

            services.AddScoped<IStockTransaction,
                StockTransaction>();

            return services;
        }
    }
}
