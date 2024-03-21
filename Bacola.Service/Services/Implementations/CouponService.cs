    using System;
using System.Net.NetworkInformation;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Enums;
using Bacola.Core.Repositories;
using Bacola.Data.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Service.Services.Implementations
{
    public class CouponService : ICouponService
    {

        readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        readonly IMapper _mapper;
        public async Task<CustomResponse<Coupon>> CreateAsync(CouponPostDto dto)
        {
            Coupon coupon = _mapper.Map<Coupon>(dto);
            await _couponRepository.AddAsync(coupon);
            await _couponRepository.SaveChangesAsync();
            return new CustomResponse<Coupon> { IsSuccess = true, Message = $"{coupon.Title} is created successfully", Data = coupon };
        }

        public async Task<IEnumerable<CouponGetDto>> GetAllAsync()
        {
            IEnumerable<CouponGetDto> coupons = await _couponRepository.GetQuery(x => !x.IsDeleted)
           .AsNoTrackingWithIdentityResolution().Select(x => new CouponGetDto { Title = x.Title, Id = x.Id, Code=x.Code, DiscountAmount=x.DiscountAmount,Status=x.Status }).ToListAsync();
            return coupons;
        }

        public async Task<CustomResponse<CouponGetDto>> GetAsync(int id)
        {
            Coupon coupon = await _couponRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (coupon == null)
            {
                return new CustomResponse<CouponGetDto> { IsSuccess = false, Message = "This Coupon doesnt exist" };
            }
            CouponGetDto couponGetDto = new CouponGetDto
            {
                DiscountAmount=coupon.DiscountAmount,
                Status=coupon.Status,
                Code=coupon.Code,
                Title = coupon.Title,
                Id = coupon.Id,
            };
            return new CustomResponse<CouponGetDto> { IsSuccess = true, Data = couponGetDto };
        }

        public async Task<CustomResponse<Coupon>> RemoveAsync(int id)
        {
            Coupon coupon = await _couponRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (coupon == null)
            {
                return new CustomResponse<Coupon> { IsSuccess = false, Message = "This Coupon doesnt exist" };
            }
            coupon.IsDeleted = true;
            await _couponRepository.UpdateAsync(coupon);
            await _couponRepository.SaveChangesAsync();
            return new CustomResponse<Coupon> { IsSuccess = true, Message = $"{coupon.Title} is removed successfully", Data = coupon };
        }

        public async Task<CustomResponse<Coupon>> UpdateAsync(int id, CouponPostDto dto)
        {
            Coupon coupon = await _couponRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (coupon == null)
            {
                return new CustomResponse<Coupon> { IsSuccess = false, Message = "This Coupon doesnt exist" };
            }
            coupon.Title = dto.Title;
            coupon.Status = dto.Status;
            coupon.Code = dto.Code;
            coupon.DiscountAmount = dto.DiscountAmount;
            await _couponRepository.UpdateAsync(coupon);
            await _couponRepository.SaveChangesAsync();
            return new CustomResponse<Coupon> { IsSuccess = true, Message = $"{coupon.Title} is updated successfully", Data = coupon };
        }

        public async Task UseLess(int id)
        {
            Coupon coupon = await _couponRepository.GetAsync(x => x.Id == id);
            coupon.Status = (int)CouponStatus.Useless;
            await _couponRepository.UpdateAsync(coupon);
            await _couponRepository.SaveChangesAsync();
        }
        public async Task Active(int id)
        {
            Coupon coupon = await _couponRepository.GetAsync(x => x.Id == id);
            coupon.Status = (int)CouponStatus.Active;
            await _couponRepository.UpdateAsync(coupon);
            await _couponRepository.SaveChangesAsync();
        }
        public async Task<int> CheckCouponValidity(string code)
        {
           
            var coupon = await _couponRepository.GetAsync(x => x.Code == code);
            if (coupon != null && (Bacola.Core.Enums.CouponStatus)coupon.Status == Bacola.Core.Enums.CouponStatus.Active)
            {
                return coupon.Id;
            }
            return 0;
        }
    }
}

