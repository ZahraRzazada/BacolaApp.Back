using System;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Service.Services.Implementations
{
    public class SettingService : ISettingService
    {
        readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        readonly IMapper _mapper;
        public async Task<CustomResponse<Setting>> CreateAsync(SettingPostDto dto)
        {
            Setting setting = _mapper.Map<Setting>(dto);
            await _settingRepository.AddAsync(setting);
            await _settingRepository.SaveChangesAsync();
            return new CustomResponse<Setting> { IsSuccess = true, Message = $"Setting is created successfully", Data = setting };
        }
    

        public async Task<List<Setting>> GetAllAsync()
        {
            var settings = await _settingRepository.GetQuery(x => !x.IsDeleted).ToListAsync();
       
            return settings;
        }
        public async Task<Dictionary<string, string>> GetAllSettings()
        {
            var settings = await _settingRepository.GetQuery(x => !x.IsDeleted).ToDictionaryAsync(x=>x.Key,x=>x.Value);

            return settings;
        }

        public async Task<CustomResponse<SettingGetDto>> GetAsync(int id)
        {

            Setting setting = await _settingRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (setting == null)
            {
                return new CustomResponse<SettingGetDto> { IsSuccess = false, Message = "This setting doesnt exist" };
            }
            SettingGetDto settingGetDto = new SettingGetDto
            {
                Key=setting.Key,
                Value=setting.Value,
                Id = setting.Id
            };
            return new CustomResponse<SettingGetDto> { IsSuccess = true, Data = settingGetDto };
        }

        public async Task<CustomResponse<Setting>> RemoveAsync(int id)
    {
        Setting? setting = await _settingRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

        if (setting == null)
        {
            return new CustomResponse<Setting> { IsSuccess = false, Message = "This Setting doesnt exist" };
        }
        setting.IsDeleted = true;
        await _settingRepository.UpdateAsync(setting);
        return new CustomResponse<Setting> { IsSuccess = true, Message = $"Setting is removed successfully", Data = setting };
        }
        public async Task<CustomResponse<Setting>> UpdateAsync(int id, SettingPostDto dto)
        {
            Setting? setting = await _settingRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (setting == null)
            {
                return new CustomResponse<Setting> { IsSuccess = false, Message = "This Setting doesnt exist" };
            }
            setting.Key = dto.Key;
            setting.Value = dto.Value;
            await _settingRepository.UpdateAsync(setting);
            return new CustomResponse<Setting> { IsSuccess = true, Message = $"Setting is updated successfully", Data = setting };
        }
    }
}

