using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;
using Microsoft.AspNetCore.Http;

namespace Bacola.Service.Services.Interfaces;

	public interface ISettingService
	{
     Task<List<Setting>> GetAllAsync();
    Task<Dictionary<string, string>> GetAllSettings();

     Task<CustomResponse<Setting>> CreateAsync(SettingPostDto dto);

   Task<CustomResponse<Setting>> RemoveAsync(int id);

     Task<CustomResponse<Setting>> UpdateAsync(int id, SettingPostDto dto);
    Task<CustomResponse<SettingGetDto>> GetAsync(int id);
}

