using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var products = await _productService.GetAllAsync(search);

            ViewData["Search"] = search;

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetDetailsAsync(id);

            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductInputDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInputDto input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var result = await _productService.CreateAsync(input);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);

                return View(input);
            }

            TempData["SuccessMessage"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var details = await _productService.GetDetailsAsync(id);

            if (details is null)
                return NotFound();

            var product = details.Product;

            var input = new ProductInputDto
            {
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                ReorderLevel = product.ReorderLevel
            };

            ViewData["ProductId"] = id;

            return View(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductInputDto input)
        {
            ViewData["ProductId"] = id;

            if (!ModelState.IsValid)
                return View(input);

            var result = await _productService.UpdateAsync(id, input);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);

                return View(input);
            }

            TempData["SuccessMessage"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _productService.DeactivateAsync(id);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            else
            {
                TempData["SuccessMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
