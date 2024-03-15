using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.ExternalServices.Interfaces;
using Bacola.Service.Services.Implementations;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Areas.Admin.Controllers
{
    [Area("admin")]

    public class AccountController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly IAccountService _accountService;
        readonly IEmailService _emailService;
        readonly IWebHostEnvironment _webHostEnvironment;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAccountService accountService, IEmailService emailService, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _emailService = emailService;
            _webHostEnvironment = webHostEnvironment;
        }

        readonly SignInManager<AppUser> _signInManager;

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto, bool isForAdminPanel=true)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _accountService.Login(dto, isForAdminPanel=true);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return RedirectToAction("index", "home");
        }

        //[Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> LogOut()
        {
            var result = await _accountService.LogOut();
            return RedirectToAction("login", "account");
        }
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            var res = await _accountService.VerifyEmail(email, token);
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View();
            }
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var res = await _accountService.ForgetPassword(email);
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(res.Data);
            var url = $"{Request.Scheme}://{Request.Host}{Url.Action("ResetPassword", "Account", new { email = res.Data.Email, token = token })}";

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "Verify.html");

            string body = string.Empty;

            body = System.IO.File.ReadAllText(path);

            body = body.Replace("{{url}}", url);


            await _emailService.SendEmail(res.Data.Email, "Reset Passsword", body);
            TempData["resetPassword"] = "reset";
            return RedirectToAction("login", "account");
        }
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            if (!ModelState.IsValid)
            {
                return View(email);
            }
            var res = await _accountService.ResetPasswordGet(email, token);
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View(email);
            }
            return View(res.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var res = await _accountService.ResetPasswordPost(dto);
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View(dto);
            }
            return RedirectToAction("index", "home");
        }
        [Authorize]

        public async Task<IActionResult> Update()
        {
            var query = _userManager.Users.Where(x => x.UserName == User.Identity.Name);
            UpdateDto? updateDto = await query.Select(x => new UpdateDto { Username = x.UserName, FullName = x.FullName })
                .FirstOrDefaultAsync();

            return View(updateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Update(UpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var res = await _accountService.Update(dto);
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View(dto);
            }
            TempData["update"] = "update";
            return RedirectToAction("index", "home");

        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var res = await _accountService.GetAllUsers();
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View();

            }
            return View(res);
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllAdmins()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var res = await _accountService.GetAllAdmins();
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View();

            }
            return View(res);
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAdmin(CreateAdminDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _accountService.CreateAdmin(dto);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }


            return RedirectToAction("GetAllAdmins", "Account");
        }

    }
}

