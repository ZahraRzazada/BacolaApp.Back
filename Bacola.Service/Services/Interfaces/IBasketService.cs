using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IBasketService
	{

        public Task<CustomResponse<Basket>> AddBasket(int id, int? count);
        public Task<CustomResponse<BasketGetDto>> GetBasket();
        public Task<CustomResponse<Basket>> DecreaseCount(int id);
        public Task<CustomResponse<Basket>> RemoveAllBasketItems();
        public Task<CustomResponse<double>> ApplyCoupon(string code, CustomResponse<BasketGetDto> dto);
    }
}

