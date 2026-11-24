using Inventory.Application.Common;
using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class StockMovementsController : Controller
    {
        private readonly StockMovementService _stockService;

        public StockMovementsController(
            StockMovementService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockMovementInputDto input)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = string.Join(" ",
                    ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));

                return RedirectToAction(
                    "Details", "Products",
                    new { id = input.ProductId });
            }

            ServiceResult? result = null;

            if (input.Type == MovementType.In)
            {
                result = await _stockService.StockInAsync(input);
            }
            else if (input.Type == MovementType.Out)
            {
                result = await _stockService.StockOutAsync(input);
            }

            if (result is null)
            {
                TempData["ErrorMessage"] = "Invalid movement type.";
            }
            else if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            else
            {
                TempData["SuccessMessage"] = result.Message;
            }

            return RedirectToAction("Details", "Products", new { id = input.ProductId });
        }
    }
}
