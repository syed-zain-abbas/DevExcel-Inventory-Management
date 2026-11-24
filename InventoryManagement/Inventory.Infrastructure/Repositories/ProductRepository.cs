using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Common;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllAsync(string? search = null)
        {
            var query = _context.Products.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Sku.Contains(search) || x.Name.Contains(search));
            }

            return await query
                .OrderBy(x => x.Name)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Sku = x.Sku,
                    Name = x.Name,
                    Description = x.Description,
                    ReorderLevel = x.ReorderLevel,
                    IsActive = x.IsActive,
                    CreatedUtc = x.CreatedUtc,

                    CurrentStock = x.StockMovements.Sum(m =>
                        m.Type == MovementType.In
                            ? m.Quantity
                            : -m.Quantity)
                })
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<bool> SkuExistsAsync(string sku, int? excludeProductId = null)
        {
            return await _context.Products.AnyAsync(x => x.Sku == sku && (!excludeProductId.HasValue || x.Id != excludeProductId.Value));
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);

            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlException
                      && sqlException.Number is 2601 or 2627)
            {
                throw new DuplicateSkuException(ex);
            }
        }
    }
}
