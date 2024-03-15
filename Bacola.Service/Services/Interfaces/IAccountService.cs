using System;
using Bacola.Core.DTOS;

using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IAccountService
	{
        public Task<CustomResponse<AppUser>> Register(RegisterDto dto);
        public Task<CustomResponse<AppUser>> Login(LoginDto dto, bool isForAdminPanel);
        public Task<CustomResponse<AppUser>> LogOut();
        public Task<CustomResponse<IEnumerable<AppUser>>> GetAllUsers();
        public Task<CustomResponse<IEnumerable<AppUser>>> GetAllAdmins();
        public Task<CustomResponse<AppUser>> Update(UpdateDto dto);
        public Task<CustomResponse<AppUser>> VerifyEmail(string email, string token);
        public Task<CustomResponse<AppUser>> ForgetPassword(string email);
        public Task<CustomResponse<ResetPasswordDto>> ResetPasswordGet(string email, string token);
        public Task<CustomResponse<AppUser>> ResetPasswordPost(ResetPasswordDto dto);
        public Task<CustomResponse<AppUser>> CreateAdmin(CreateAdminDto dto);
    }
}

    