using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;
using Microsoft.AspNetCore.Http;

namespace Bacola.Service.Services.Interfaces
{
	public interface ISettingService
	{
        public Task<IEnumerable<SettingGetDto>> GetAllAsync();

        public Task<CustomResponse<Setting>> CreateAsync(SettingPostDto dto);

        public Task<CustomResponse<Setting>> RemoveAsync(int id);

        public Task<CustomResponse<Setting>> UpdateAsync(int id, SettingPostDto dto);
        public Task<CustomResponse<SettingGetDto>> GetAsync(int id);
    }
}

