using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.ExternalServices.Implementations;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Bacola.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Bacola.Core.Enums;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Bacola.Data.Contexts;

namespace Bacola.Service.Services.Implementations
{
    public class AccountService : IAccountService
    {
        readonly IMapper _mapper;
        readonly BacolaDbContext _context;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IHttpContextAccessor _http;
        readonly IWebHostEnvironment _webHostEnviroment;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IWebHostEnvironment webHostEnviroment, IHttpContextAccessor http, IMapper mapper, BacolaDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

            _webHostEnviroment = webHostEnviroment;
            _http = http;
            _mapper = mapper;
            _context = context;
        }
        public async Task<CustomResponse<AppUser>> Login(LoginDto dto,bool isForAdminPanel)
        {
            AppUser appUser = await _userManager.FindByNameAsync(dto.UserNameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
            }
            if (appUser == null)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "User not found" };
            }
            var res = await _signInManager.PasswordSignInAsync(appUser, dto.Password, dto.RememberMe, true);
            if (!res.Succeeded)
            {
                if (res.IsLockedOut)
                {
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = "Your Account was blocked for 1 minute" };

                }
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "Username or password is not valid" };
            }
            if (!await _userManager.IsEmailConfirmedAsync(appUser))
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "Please verify your email" };

            }
            if (isForAdminPanel)
            {
                var isAdmin = await _userManager.IsInRoleAsync(appUser, "Admin");
                var isSuperAdmin = await _userManager.IsInRoleAsync(appUser, "SuperAdmin");

                if (!isAdmin && !isSuperAdmin)
                {
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = "Access denied. Only admins and superadmins can access this panel." };
                }
            }
            else
            {
                var user = await _userManager.IsInRoleAsync(appUser, "User");
                if (!user)
                {
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = "Access denied. Only Users  can access website." };
                }
            }

            return new CustomResponse<AppUser> { IsSuccess = true, Message = "Login is succesfully" };
        }

        public async Task<CustomResponse<AppUser>> LogOut()
        {
            await _signInManager.SignOutAsync();
            return new CustomResponse<AppUser> { IsSuccess = true, Message = "User is LogOut succesfully" };


        }
        public async Task<CustomResponse<AppUser>> VerifyEmail(string email, string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "User not found" };
            }
            var res = await _userManager.ConfirmEmailAsync(appUser, token);
            if (!res.Succeeded)
            {
                var errorDescriptions = string.Join("; ", res.Errors.Select(e => e.Description));
                return new CustomResponse<AppUser> { IsSuccess = false, Message = errorDescriptions };
            }
            appUser.EmailConfirmed = true;
            return new CustomResponse<AppUser> { IsSuccess = true, Message = "Email verified succesfully" };
        }

        public async Task<CustomResponse<AppUser>> Register(RegisterDto dto)
        {
            bool exists = dto.FullName==null|| dto.Username == null || dto.Password == null || dto.Email == null || dto.ConfirmPassword == null;
            if(exists)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "All fields are required" };
            }
            var usedEmail = await _userManager.FindByEmailAsync(dto.Email);

            if(usedEmail!=null)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "Email is already used!" };
            }
            AppUser appUser = new AppUser()
            {
                UserName = dto.Username,
                Email = dto.Email,
                FullName=dto.FullName
            };
            var res = await _userManager.CreateAsync(appUser, dto.Password);
            if(!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    var errorDescriptions = string.Join("; ", res.Errors.Select(e => e.Description));
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = errorDescriptions };
                }
            }
            var hasUserRole = await _userManager.IsInRoleAsync(appUser, "User");
            if (!hasUserRole)
            {
                
                var defaultRole = "User";
                await _userManager.AddToRoleAsync(appUser, defaultRole);
            }
           
            return new CustomResponse<AppUser> { IsSuccess = true, Message = "User is registerd succesfully",Data=appUser };

        }

        public async Task<CustomResponse<ResetPasswordDto>> ResetPasswordGet(string email, string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return new CustomResponse<ResetPasswordDto> { IsSuccess = false, Message = "User not found" };
            }
            ResetPasswordDto resetPasswordDto = new ResetPasswordDto()
            {
                Email = email,
                Token =token
            };
            return new CustomResponse<ResetPasswordDto> { IsSuccess = true, Message = $"", Data = resetPasswordDto };
        }

        public async Task<CustomResponse<AppUser>> ResetPasswordPost(ResetPasswordDto dto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser == null)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "User not found" };
            }
            var res = await _userManager.ResetPasswordAsync(appUser, dto.Token, dto.Password);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    var errorDescriptions = string.Join("; ", res.Errors.Select(e => e.Description));
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = errorDescriptions };
                }
            }
            return new CustomResponse<AppUser> { IsSuccess = true, Message = "Pasword was changed" };

        }

        public async Task<CustomResponse<AppUser>> Update(UpdateDto dto)
        {
            AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            if (appUser == null)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "User not found" };
            }
            appUser.FullName = dto.FullName;
            appUser.UserName = dto.Username;
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    var errorDescriptions = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = errorDescriptions };
                }
            }
            if (!string.IsNullOrEmpty(dto.OldPassword) && !string.IsNullOrEmpty(dto.NewPassword))
            {
                var changepassword = await _userManager.ChangePasswordAsync(appUser, dto.OldPassword, dto.NewPassword);
                if (!changepassword.Succeeded)
                {
                    var errorDescriptions = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = errorDescriptions };
                }
            }
            await _signInManager.SignInAsync(appUser, true);
            return new CustomResponse<AppUser> { IsSuccess = true, Message = "User Updated succesfully" };

        }
        public async Task<CustomResponse<AppUser>> ForgetPassword(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = "User not found" };
            }
            return new CustomResponse<AppUser> { IsSuccess = true, Message = "User found",Data=appUser };
        }

        public async Task<CustomResponse<IEnumerable<AppUser>>> GetAllUsers()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var usersInRole = allUsers.Where(user =>
                _userManager.IsInRoleAsync(user, "User").Result
       
            );

            return new CustomResponse<IEnumerable<AppUser>> { IsSuccess = true, Message = "All users are obtained", Data = usersInRole };
        }

        public async Task<CustomResponse<IEnumerable<AppUser>>> GetAllAdmins()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var usersInRole = allUsers.Where(user =>
                _userManager.IsInRoleAsync(user, "Admin").Result

            );

            return new CustomResponse<IEnumerable<AppUser>> { IsSuccess = true, Message = "All users are obtained", Data = usersInRole };
        }

        public async Task<CustomResponse<AppUser>> CreateAdmin(CreateAdminDto dto)
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var usersInRole = allUsers.Where(user =>
                _userManager.IsInRoleAsync(user, "Admin").Result

            );
            AppUser adminUser = _mapper.Map<AppUser>(dto);
            var userExists = usersInRole.Any(user =>
            user.UserName == dto.Username || user.Email == dto.Email);
            if (userExists)
            {
                return new CustomResponse<AppUser> { IsSuccess = false, Message = $"User '{dto.Username}' already exists in the database", Data = null };
            }
            var res = await _userManager.CreateAsync(adminUser, dto.Password);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    var errorDescriptions = string.Join("; ", res.Errors.Select(e => e.Description));
                    return new CustomResponse<AppUser> { IsSuccess = false, Message = errorDescriptions };
                }
            }
            var defaultRole = "Admin";
            await _userManager.AddToRoleAsync(adminUser, defaultRole);
            adminUser.EmailConfirmed = true;
            return new CustomResponse<AppUser> { IsSuccess = true, Message = "Admin is created successfully", Data = adminUser };
        }

       
        

    }
}

