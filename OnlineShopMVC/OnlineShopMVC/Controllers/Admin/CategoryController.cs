using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopMVC.Models;
using OnlineShopMVC.Services.Interfaces;
using System.Runtime.InteropServices;

namespace OnlineShopMVC.Controllers.Admin
{
    [Route("admin/categories")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // List Categories
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View("~/Views/Admin/Category/Index.cshtml", categories);
        }

        // Create Category - Form View
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Category/Create.cshtml");
        }

        // Create Category - Post Form
        [HttpPost("create")]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Admin/Category/Create.cshtml", category);

            await _categoryService.AddCategoryAsync(category);
            return RedirectToAction("Index");
        }

        // Edit Category - Form View
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return View("~/Views/Admin/Category/Edit.cshtml", category);
        }

        // Edit Category - Post Form
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Admin/Category/Edit.cshtml", category);

            await _categoryService.UpdateCategoryAsync(category);
            return RedirectToAction("Index");
        }

        // Delete Category - Form View
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return View("~/Views/Admin/Category/Delete.cshtml", category);
        }

        // Delete Category - Post Form
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
