using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly ApplicationDbContext _context;

        public StockMovementRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCurrentStockAsync(int productId)
        {
            return await _context.StockMovements
                .Where(x => x.ProductId == productId)
                .SumAsync(x => x.Type == MovementType.In
                    ? x.Quantity
                    : -x.Quantity);
        }

        public async Task<List<StockMovementDto>> GetHistoryAsync(
            int productId)
        {
            return await _context.StockMovements
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedUtc)
                .ThenByDescending(x => x.Id)
                .Select(x => new StockMovementDto
                {
                    Id = x.Id,
                    Type = x.Type,
                    Quantity = x.Quantity,
                    Note = x.Note,
                    CreatedUtc = x.CreatedUtc
                })
                .ToListAsync();
        }

        public async Task AddAsync(StockMovement movement)
        {
            _context.StockMovements.Add(movement);

            await _context.SaveChangesAsync();
        }
    }
}
