using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllAsync(string? search = null);

        Task<Product?> GetByIdAsync(int id);

        Task<bool> SkuExistsAsync(string sku, int? excludeProductId = null);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);
    }
}
