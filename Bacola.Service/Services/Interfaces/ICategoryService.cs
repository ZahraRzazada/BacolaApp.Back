using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface ICategoryService
	{
        public Task<IEnumerable<CategoryGetDto>> GetAllAsync();

        public Task<CustomResponse<Category>> CreateAsync(CategoryPostDto dto);

        public Task<CustomResponse<Category>> RemoveAsync(int id);

        public Task<CustomResponse<Category>> UpdateAsync(int id, CategoryPostDto dto);
        public Task<CustomResponse<CategoryGetDto>> GetAsync(int id);
    }
}

