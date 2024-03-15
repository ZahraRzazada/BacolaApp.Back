using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ContactController : Controller
    {
        readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contactService.GetAllAsync());
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _contactService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

