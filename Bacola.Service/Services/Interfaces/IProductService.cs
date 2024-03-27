
using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IProductService
	{
		public Task<CustomResponse<Product>> RemoveAsync(int id);
		public Task<CustomResponse<Product>> CreateAsync(ProductPostDto dto);
		public Task<CustomResponse<Product>> UpdateAsync(int id, ProductPostDto dto);
        public Task<CustomResponse<Product>> UpdateAsync(int id, ProductPutDto dto);

        public Task<PagginatedResponse<ProductGetDto>> GetAllAsync(int page = 1);
        public Task<CustomResponse<ProductGetDto>> GetAsync(int id);
        public Task<CustomResponse<ProductPutDto>> GetPutAsync(int id);
        public Task<CustomResponse<List<ProductGetDto>>> Search(string search);
		//public Task<PagginatedResponse<ProductGetDto>> FilterByCategory(List<int> categoryIds);
		Task<PagginatedResponse<ProductGetDto>> GetFilteredProducts(ProductFilterDto filter);
    }
}

