using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Core.DTOS;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeSliderController : Controller
    {
        readonly IHomeSliderService _sliderService;

        public HomeSliderController(IHomeSliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _sliderService.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _sliderService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var res = await _sliderService.GetAsync(id);

            return View(res.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, SliderPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _sliderService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _sliderService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

