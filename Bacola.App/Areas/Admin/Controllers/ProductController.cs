using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Core.DTOS;
using Bacola.Core.Repositories;
using Bacola.Service.Services.Implementations;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : Controller
    {
        readonly IProductService _productService;
        readonly ITagService _tagService;
        public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService, IProductImageService productImageService, ITagService tagService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _tagService = tagService;
        }

        readonly IBrandService _brandService;
        readonly ICategoryService _categoryService;
        readonly IProductImageService _productImageService;
        public async Task<IActionResult> Index()
        {
            return View( await _productService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Brands = await _brandService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Brands = await _brandService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(dto);
            };
            var res = await _productService.CreateAsync(dto);
            if (!res.IsSuccess)
            {
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Brands = await _brandService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ModelState.AddModelError("", res.Message);
                return View(dto);
            };
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Brands = await _brandService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();
            var product = await _productService.GetAsync(id);
            return View(product.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Brands = await _brandService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();
                var product = await _productService.GetAsync(id);
                return View(product.Data);
            };
            var res = await _productService.UpdateAsync(id, dto);
            if (!res.IsSuccess)
            {
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Brands = await _brandService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();
                var product = await _productService.GetAsync(id);
                ModelState.AddModelError("", res.Message);
                return View(product.Data);
            };
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _productService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveImage(int id)
        {
            await _productImageService.RemoveAsync(id);
            return Json(new { status = 200 });
        }

    }
}

