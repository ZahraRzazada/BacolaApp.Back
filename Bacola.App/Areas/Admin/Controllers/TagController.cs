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
    public class TagController : Controller
    {
        readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _tagService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _tagService.CreateAsync(dto);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            var response=await _tagService.RemoveAsync(id);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var res = await _tagService.GetAsync(id);
            return View(res.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, TagPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response=await _tagService.UpdateAsync(id, dto);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }
       
            return RedirectToAction(nameof(Index));
        }
    }
}

