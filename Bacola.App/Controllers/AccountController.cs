using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.ExternalServices.Interfaces;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Controllers
{
    public class AccountController : Controller
    {
        readonly IAccountService _accountService;
        readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment webHostEnvironment, IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
        }

        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IWebHostEnvironment _webHostEnvironment;

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto, bool isForAdminPanel = false)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var res = await _accountService.Login(dto,isForAdminPanel = false);
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View();

            }
            
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> LogOut()
        {
            var res = await _accountService.LogOut();
            return RedirectToAction("index", "home");
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
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var res = await _accountService.Register(dto);

            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message);
                return View();
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(res.Data);
            var url = $"{Request.Scheme}://{Request.Host}{Url.Action("VerifyEmail", "Account", new { email = res.Data.Email, token = token })}";

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "Verify.html");

            string body = string.Empty;

            body = System.IO.File.ReadAllText(path);

            body = body.Replace("{{url}}", url);


            await _emailService.SendEmail(dto.Email, "Verify Email", body);

            TempData["emailverify"] = "verify";
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
            return RedirectToAction("index", "home");
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
            return RedirectToAction("login", "Account");
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
    }
}

