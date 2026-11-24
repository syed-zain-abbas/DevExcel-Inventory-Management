using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IStockTransaction
    {
        Task BeginAsync();

        Task LockProductAsync(int productId);

        Task CommitAsync();

        Task RollbackAsync();
    }
}
