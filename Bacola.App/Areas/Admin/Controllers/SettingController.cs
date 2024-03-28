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
    public class SettingController : Controller
        {
            readonly ISettingService _SettingService;

            public SettingController(ISettingService SettingService)
            {
                _SettingService = SettingService;
            }

            public async Task<IActionResult> Index()
            {
            var data = await _SettingService.GetAllAsync();
            return View(data); ;
            }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(SettingPostDto dto)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var res = await _SettingService.CreateAsync(dto);
                if (!res.IsSuccess)
                {
                    ModelState.AddModelError("", res.Message);
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }

            

            public async Task<IActionResult> Update(int id)
            {
                var res = await _SettingService.GetAsync(id);
                return View(res.Data);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Update(int id, SettingPostDto dto)
            {
                if (!ModelState.IsValid)
                {

                    var result = await _SettingService.GetAsync(id);
                    return View(result.Data);
                }

                var res = await _SettingService.UpdateAsync(id, dto);

                if (!res.IsSuccess)
                {
                    ModelState.AddModelError("", res.Message);
                    var result1 = await _SettingService.GetAsync(id);
                    return View(result1.Data);
                }

                return RedirectToAction(nameof(Index));
            }
        }
    }


