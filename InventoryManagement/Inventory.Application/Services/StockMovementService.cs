using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Common;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Application.Services
{
    public class StockMovementService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockMovementRepository _stockRepository;
        private readonly IStockTransaction _transaction;

        public StockMovementService(IProductRepository productRepository, IStockMovementRepository stockRepository, IStockTransaction transaction)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _transaction = transaction;
        }
        public async Task<ServiceResult> StockInAsync(StockMovementInputDto input)
        {
            if (input.Quantity <= 0)
                return ServiceResult.Failure("Quantity must be greater than zero.");

            if (input.Type != MovementType.In)
                return ServiceResult.Failure("Invalid movement type.");

            await _transaction.BeginAsync();

            try
            {
                await _transaction.LockProductAsync(input.ProductId);

                var product = await _productRepository.GetByIdAsync(input.ProductId);

                if (product is null || !product.IsActive)
                {
                    await _transaction.RollbackAsync();

                    return ServiceResult.Failure("Product not found or inactive.");
                }

                var movement = new StockMovement
                {
                    ProductId = input.ProductId,
                    Type = MovementType.In,
                    Quantity = input.Quantity,
                    Note = input.Note?.Trim(),
                    CreatedUtc = DateTime.UtcNow
                };

                await _stockRepository.AddAsync(movement);

                await _transaction.CommitAsync();

                return ServiceResult.Success("Stock added successfully.");
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ServiceResult> StockOutAsync(StockMovementInputDto input)
        {
            if (input.Quantity <= 0)
                return ServiceResult.Failure("Quantity must be greater than zero.");

            if (input.Type != MovementType.Out)
                return ServiceResult.Failure("Invalid movement type.");

            await _transaction.BeginAsync();

            try
            {
                await _transaction.LockProductAsync(input.ProductId);

                var product = await _productRepository.GetByIdAsync(input.ProductId);

                if (product is null || !product.IsActive)
                {
                    await _transaction.RollbackAsync();

                    return ServiceResult.Failure("Product not found or inactive.");
                }

                var currentStock = await _stockRepository.GetCurrentStockAsync(input.ProductId);

                if (input.Quantity > currentStock)
                {
                    await _transaction.RollbackAsync();

                    return ServiceResult.Failure($"Insufficient stock. Available stock: {currentStock}.");
                }

                var movement = new StockMovement
                {
                    ProductId = input.ProductId,
                    Type = MovementType.Out,
                    Quantity = input.Quantity,
                    Note = input.Note?.Trim(),
                    CreatedUtc = DateTime.UtcNow
                };

                await _stockRepository.AddAsync(movement);

                await _transaction.CommitAsync();

                return ServiceResult.Success("Stock issued successfully.");
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }
    }
}
