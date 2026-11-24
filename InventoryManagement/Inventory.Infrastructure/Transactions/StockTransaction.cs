using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inventory.Infrastructure.Transactions
{
    public class StockTransaction : IStockTransaction
    {
        private readonly ApplicationDbContext _context;

        private IDbContextTransaction? _transaction;

        public StockTransaction(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginAsync()
        {
            if (_transaction is not null)
                throw new InvalidOperationException("A transaction is already active.");

            _transaction = await _context.Database
                .BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task LockProductAsync(int productId)
        {
            if (_transaction is null)
                throw new InvalidOperationException("No active transaction.");

            await _context.Products
                .FromSqlInterpolated($@"
                SELECT *
                FROM [Products] WITH (UPDLOCK, HOLDLOCK)
                WHERE [Id] = {productId}")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction is null)
                throw new InvalidOperationException("No active transaction.");

            await _transaction.CommitAsync();

            await _transaction.DisposeAsync();

            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction is null)
                return;

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();

                _transaction = null;

                _context.ChangeTracker.Clear();
            }
        }
    }
}
