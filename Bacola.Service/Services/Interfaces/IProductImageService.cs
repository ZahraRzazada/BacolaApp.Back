using System;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IProductImageService
	{
        public Task<CustomResponse<ProductImage>> RemoveAsync(int id);
    }
}

