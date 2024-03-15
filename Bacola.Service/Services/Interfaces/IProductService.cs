
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
		public Task<IEnumerable<ProductGetDto>> GetAllAsync();
		public Task<CustomResponse<ProductGetDto>> GetAsync(int id);
	}
}

