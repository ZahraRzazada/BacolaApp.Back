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
    public class CouponController : Controller
    {
        readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _couponService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _couponService.CreateAsync(dto);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _couponService.RemoveAsync(id);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var res = await _couponService.GetAsync(id);
            return View(res.Data);
        }
        public async Task<IActionResult> UseLess(int id)
        {
            await _couponService.UseLess(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Active(int id)
        {
            await _couponService.Active(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CouponPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _couponService.UpdateAsync(id, dto);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

