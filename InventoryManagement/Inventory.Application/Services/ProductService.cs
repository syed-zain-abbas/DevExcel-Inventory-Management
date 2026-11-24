using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Inventory.Application.Common;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;

namespace Inventory.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IStockMovementRepository _stockRepository;

        public ProductService(IProductRepository productRepository, IStockMovementRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        public async Task<List<ProductDto>> GetAllAsync(string? search = null)
        {
            return await _productRepository.GetAllAsync(search?.Trim());
        }

        public async Task<ProductDetailsDto?> GetDetailsAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return null;

            var currentStock = await _stockRepository.GetCurrentStockAsync(id);

            var movements = await _stockRepository.GetHistoryAsync(id);

            return new ProductDetailsDto
            {
                Product = new ProductDto
                {
                    Id = product.Id,
                    Sku = product.Sku,
                    Name = product.Name,
                    Description = product.Description,
                    ReorderLevel = product.ReorderLevel,
                    IsActive = product.IsActive,
                    CreatedUtc = product.CreatedUtc,
                    CurrentStock = currentStock
                },
                Movements = movements
            };
        }

        public async Task<ServiceResult> CreateAsync(ProductInputDto input)
        {
            try {

                var validationError = ValidateProduct(input);

                if (validationError is not null)
                    return ServiceResult.Failure(validationError);

                var sku = input.Sku.Trim().ToUpperInvariant();

                if (await _productRepository.SkuExistsAsync(sku))
                {
                    return ServiceResult.Failure("This SKU already exists.");
                }

                var product = new Product
                {
                    Sku = sku,
                    Name = input.Name.Trim(),
                    Description = input.Description?.Trim(),
                    ReorderLevel = input.ReorderLevel,
                    IsActive = true,
                    CreatedUtc = DateTime.UtcNow
                };

                await _productRepository.AddAsync(product);

                return ServiceResult.Success("Product created successfully.");
            }
            catch(DuplicateSkuException ex)
            {
                return ServiceResult.Failure(ex.Message);
            }
            
        }

        public async Task<ServiceResult> UpdateAsync(int id, ProductInputDto input)
        {
            try {
                var validationError = ValidateProduct(input);

                if (validationError is not null)
                    return ServiceResult.Failure(validationError);

                var product = await _productRepository.GetByIdAsync(id);

                if (product is null)
                    return ServiceResult.Failure("Product not found.");

                var sku = input.Sku.Trim().ToUpperInvariant();

                if (await _productRepository.SkuExistsAsync(sku, id))
                {
                    return ServiceResult.Failure("This SKU already exists.");
                }

                product.Sku = sku;
                product.Name = input.Name.Trim();
                product.Description = input.Description?.Trim();
                product.ReorderLevel = input.ReorderLevel;

                await _productRepository.UpdateAsync(product);

                return ServiceResult.Success("Product updated successfully.");

            } catch (DuplicateSkuException ex) { 
                return ServiceResult.Failure(ex.Message); 
            } 

            
        }

        public async Task<ServiceResult> DeactivateAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return ServiceResult.Failure("Product not found.");

            if (!product.IsActive)
            {
                return ServiceResult.Failure("Product is already inactive.");
            }

            product.IsActive = false;

            await _productRepository.UpdateAsync(product);

            return ServiceResult.Success("Product deactivated successfully.");
        }

        private static string? ValidateProduct(ProductInputDto input)
        {
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(input, new ValidationContext(input), results, validateAllProperties: true);

            return isValid
                ? null
                : results.First().ErrorMessage;
        }

    }
}
