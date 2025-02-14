using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopMVC.Models;
using OnlineShopMVC.Services.Interfaces;
using OnlineShopMVC.ViewModels;

namespace OnlineShopMVC.Controllers.Admin
{
    [Route("admin/products")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // List Products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View("~/Views/Admin/Product/Index.cshtml", products);
        }

        // Create Product - Form View
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View("~/Views/Admin/Product/Create.cshtml", new ProductAdminViewModel());
        }

        // Create Product - Post Form
        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductAdminViewModel model)
        {
            // Ensure Categories are passed
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Product/Create.cshtml", model);
            }

            // Try to parse the PriceInput
            if (!decimal.TryParse(model.PriceInput, out decimal parsedPrice))
            {
                ModelState.AddModelError("PriceInput", "Invalid price format. Please enter a decimal value using a comma (e.g., 19,99).");
                return View("~/Views/Admin/Product/Create.cshtml", model);
            }

            // Create a new Product from the view model
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = parsedPrice,
                StockQuantity = model.StockQuantity,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
            };

            await _productService.AddProductAsync(product);
            return RedirectToAction("Index");
        }

        // Edit Product - Form View
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            // Ensure Categories are passed
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();

            // Retrieve the existing product from database
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            // Create a new ViewModel
            var model = new ProductAdminViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PriceInput = product.Price.ToString(),
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
            };
            
            return View("~/Views/Admin/Product/Edit.cshtml", model);
        }

        // Edit Product - Post Form
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ProductAdminViewModel model)
        {
            // Ensure Categories are passed
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();

            if (!ModelState.IsValid)
                return View("~/Views/Admin/Product/Edit.cshtml", model);

            // Try to parse the PriceInput
            if (!decimal.TryParse(model.PriceInput, out decimal parsedPrice))
            {
                ModelState.AddModelError("PriceInput", "Invalid price format. Please enter a decimal value using a comma (e.g., 19,99).");
                return View("~/Views/Admin/Product/Edit.cshtml", model);
            }

            // Retrieve the existing product from database
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            // Update the Product details
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = parsedPrice;
            product.StockQuantity = model.StockQuantity;
            product.ImageUrl = model.ImageUrl;
            product.CategoryId = model.CategoryId;

            await _productService.UpdateProductAsync(product);
            return RedirectToAction("Index");
        }

        // Delete Product - Form View
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Ensure Categories are passed
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return View("~/Views/Admin/Product/Delete.cshtml", product);
        }

        // Delete Product - Post Form
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
