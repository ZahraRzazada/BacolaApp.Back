using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IBrandService
	{
        public Task<IEnumerable<BrandGetDto>> GetAllAsync();

        public Task<CustomResponse<Brand>> CreateAsync(BrandPostDto dto);

        public Task<CustomResponse<Brand>> RemoveAsync(int id);

        public Task<CustomResponse<Brand>> UpdateAsync(int id, BrandPostDto dto);
        public Task<CustomResponse<BrandGetDto>> GetAsync(int id);
    }
}

