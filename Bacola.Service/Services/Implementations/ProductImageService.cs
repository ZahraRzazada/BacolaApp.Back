using System;
using System.Reflection.Metadata;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;

namespace Bacola.Service.Services.Implementations
{
	public class ProductImageService:IProductImageService
	{
        readonly IProductImageRepository productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            this.productImageRepository = productImageRepository;
        }

        public async Task<CustomResponse<ProductImage>> RemoveAsync(int id)
        {
            var image = await productImageRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);

            if (image == null)
            {
                return new CustomResponse<ProductImage> { IsSuccess = false, Message = "This Image doesnt exist" };
            }

            image.IsDeleted = true;
            await productImageRepository.UpdateAsync(image);
            await productImageRepository.SaveChangesAsync();
            return new CustomResponse<ProductImage> { IsSuccess = true, Message = "Image is removed successfully", Data = image };
        }
    }
}

