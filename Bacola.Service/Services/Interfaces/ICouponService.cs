using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface ICouponService
	{
        public Task<IEnumerable<CouponGetDto>> GetAllAsync();
        public Task<CustomResponse<Coupon>> CreateAsync(CouponPostDto dto);
        public Task<CustomResponse<Coupon>> RemoveAsync(int id);
        public Task<CustomResponse<Coupon>> UpdateAsync(int id, CouponPostDto dto);
        public Task<CustomResponse<CouponGetDto>> GetAsync(int id);
        public Task UseLess(int id);
        public Task Active(int id);
        public Task<int> CheckCouponValidity(string code);
        
    }
}

