using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Core.DTOS;
using Bacola.Service.Services.Implementations;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _categoryService.CreateAsync( dto);
            if (!response.IsSuccess)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }
            await _categoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _categoryService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            var categories = await _categoryService.GetAsync(id);
            return View(categories.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _categoryService.UpdateAsync(id, dto);
            if (!response.IsSuccess)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }
            await _categoryService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
    }
}

