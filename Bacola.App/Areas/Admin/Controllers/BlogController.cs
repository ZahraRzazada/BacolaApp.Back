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
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;
        readonly ICategoryService _categoryService;
        readonly ITagService _tagService;

        public BlogController(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View();
            }
            var response = await _blogService.CreateAsync(dto);
            if (!response.IsSuccess)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            var blogs = await _blogService.GetAsync(id);
            return View(blogs.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View(await _blogService.GetAsync(id));
            }
            var response = await _blogService.UpdateAsync(id, dto);

            if (!response.IsSuccess)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View(await _blogService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Remove(int id)
        {
            await _blogService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}

