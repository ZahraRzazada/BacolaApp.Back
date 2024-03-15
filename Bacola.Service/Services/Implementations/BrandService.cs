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
    public class BrandService : IBrandService
    {
        readonly IBrandRepository _brandRepository;
        readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponse<Brand>> CreateAsync(BrandPostDto dto)
        {
            bool brandExists = await _brandRepository.GetQuery(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && !x.IsDeleted).AnyAsync();
            if (brandExists)
            {
                return new CustomResponse<Brand> { IsSuccess = false, Message = $"Brand '{dto.Name}' already exists in the database", Data = null };
            }
            Brand brand = _mapper.Map<Brand>(dto);
            await _brandRepository.AddAsync(brand);
            await _brandRepository.SaveChangesAsync();
            return new CustomResponse<Brand> { IsSuccess = true, Message = $"{brand.Name} is created successfully", Data = brand };
        }

        public async Task<IEnumerable<BrandGetDto>> GetAllAsync()
        {
            IEnumerable<BrandGetDto> brands = await _brandRepository.GetQuery(x => !x.IsDeleted).AsNoTrackingWithIdentityResolution().Select(x => new BrandGetDto { Name = x.Name, Id = x.Id }).ToListAsync();
            return brands;
        }

        public async Task<CustomResponse<BrandGetDto>> GetAsync(int id)
        {
            Brand? brand = await _brandRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (brand == null)
            {
                return new CustomResponse<BrandGetDto> { IsSuccess = false, Message = "This brand doesnt exist" };
            }
            BrandGetDto brandGetDto = new BrandGetDto()
            {
                Name = brand.Name,
                Id = brand.Id
            };
            return new CustomResponse<BrandGetDto> { IsSuccess = true, Data = brandGetDto };
        }

        public async Task<CustomResponse<Brand>> RemoveAsync(int id)
        {
            Brand? brand = await _brandRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if(brand==null)
            {
                return new CustomResponse<Brand> { IsSuccess = false, Message = "This brand doesnt exist" };
            }
            brand.IsDeleted = true;
            await _brandRepository.UpdateAsync(brand);
            await _brandRepository.SaveChangesAsync();
            return new CustomResponse<Brand> { IsSuccess = true, Message = $"{brand.Name} is removed successfully", Data = brand };
        }

        public async Task<CustomResponse<Brand>> UpdateAsync(int id, BrandPostDto dto)
        {
            Brand? brand = await _brandRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (brand == null)
            {
                return new CustomResponse<Brand> { IsSuccess = false, Message = "This brand doesnt exist" };
            }
            brand.Name = dto.Name;
            await _brandRepository.UpdateAsync(brand);
            await _brandRepository.SaveChangesAsync();
            return new CustomResponse<Brand> { IsSuccess = true, Message = $"{brand.Name} is updated successfully", Data = brand };

        }
    }
}

