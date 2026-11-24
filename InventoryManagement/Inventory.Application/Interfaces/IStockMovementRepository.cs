using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<int> GetCurrentStockAsync(int productId);
        Task<List<StockMovementDto>> GetHistoryAsync(int productId);
        Task AddAsync(StockMovement movement);
    }
}
