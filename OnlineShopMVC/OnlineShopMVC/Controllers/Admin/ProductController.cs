using Microsoft.AspNetCore.Mvc;
using OnlineShopMVC.Models;
using OnlineShopMVC.Services.Interfaces;

namespace OnlineShopMVC.Controllers.Admin
{
    [Route("admin/products")]
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
            return View("Admin/Product/Index", products);
        }

        // Create Product - Form View
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View("Admin/Product/Create");
        }

        // Create Product - Post Form
        [HttpPost("create")]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View("Admin/Product/Create", product);

            await _productService.AddProductAsync(product);
            return RedirectToAction("Index");
        }

        // Edit Product - Form View
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View("Admin/Product/Edit", product);
        }

        // Edit Product - Post Form
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (!ModelState.IsValid)
                return View("Admin/Product/Edit", product);

            await _productService.UpdateProductAsync(product);
            return RedirectToAction("Index");
        }

        // Delete Product - Form View
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return View("Admin/Product/Delete", product);
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
