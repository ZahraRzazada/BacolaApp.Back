using System;
using System.Xml.Linq;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bacola.Service.Services.Implementations
{
    public class BasketService : IBasketService
    {

        readonly IProductService _productService;
        readonly ICouponService _couponService;
        readonly IHttpContextAccessor _http;

        public BasketService(IHttpContextAccessor http, IProductService productService, IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, UserManager<AppUser> userManager, ICouponService couponService)
        {
            _http = http;
            _productService = productService;
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _userManager = userManager;
            _couponService = couponService;
        }

        readonly IBasketRepository _basketRepository;
        readonly IBasketItemRepository _basketItemRepository;
        readonly UserManager<AppUser> _userManager;
        public async Task<CustomResponse<Basket>> AddBasket(int id, int? count)
        {
            var product = await _productService.GetAsync(id);
            if (product == null) return new CustomResponse<Basket> { IsSuccess = false, Message = "Product not found" };
            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

                var basket = await _basketRepository.GetAsync(x => !x.IsDeleted && x.AppUserId == appUser.Id, "BasketItems");

                if (basket != null)
                {

                    var basketItem = basket.BasketItems.Where(X => !X.IsDeleted).FirstOrDefault(x => x.ProductId == id);

                    if (basketItem != null)
                    {
                        basketItem.Count++;
                        await _basketItemRepository.UpdateAsync(basketItem);
                    }
                    else
                    {
                        basketItem = new Core.Entities.BasketItem
                        {
                            Count = count ?? 1,
                            Basket = basket,
                            ProductId = id,
                        };
                        await _basketItemRepository.AddAsync(basketItem);
                    }
                }
                else
                {
                    basket = new Basket { AppUserId = appUser.Id };
                    await _basketRepository.AddAsync(basket);
                    var basketItem = new Core.Entities.BasketItem
                    {
                        Count = 1,
                        Basket = basket,
                        ProductId = id,
                    };
                    await _basketItemRepository.AddAsync(basketItem);
                }
                await _basketRepository.SaveChangesAsync();
            }
            else
            {
                List<BasketPostDto>? basketDtos = new List<BasketPostDto>();

                var basketJson = _http.HttpContext.Request.Cookies["basket"];

                if (basketJson == null)
                {
                    BasketPostDto basketDto = new BasketPostDto()
                    {
                        Id = id,
                        Count = 1
                    };
                    basketDtos.Add(basketDto);
                }
                else
                {
                    basketDtos = JsonConvert.DeserializeObject<List<BasketPostDto>>(basketJson);

                    BasketPostDto? basketDto = basketDtos.FirstOrDefault(x => x.Id == id);

                    if (basketDto == null)
                    {
                        basketDto = new BasketPostDto()
                        {
                            Id = id,
                            Count = 1
                        };
                        basketDtos.Add(basketDto);
                    }
                    else
                    {
                        basketDto.Count++;
                    }

                }


                basketJson = JsonConvert.SerializeObject(basketDtos);
                _http.HttpContext.Response.Cookies.Append("basket", basketJson);
            }
            return new CustomResponse<Basket> { IsSuccess = true, Message = "Process is succesful" };
        }
        public async Task<CustomResponse<Basket>> DecreaseCount(int id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return new CustomResponse<Basket> { IsSuccess = false, Message = "Basket not found" };
            }

            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

                var basket = await _basketRepository.GetAsync(x => !x.IsDeleted && x.AppUserId == appUser.Id, "BasketItems");

                if (basket == null)
                {
                    if (basket == null) return new CustomResponse<Basket> { IsSuccess = false, Message = "Basket not found" };
                }

                var basketItem = basket.BasketItems.Where(X => !X.IsDeleted).FirstOrDefault(x => x.ProductId == id);

                if (basketItem == null)
                {
                    if (basketItem == null) return new CustomResponse<Basket> { IsSuccess = false, Message = "Basket not found" };
                }

                else if (basketItem.Count == 1)
                {
                    basketItem.IsDeleted = true;
                }
                else
                {
                    basketItem.Count--;
                }
                await _basketItemRepository.UpdateAsync(basketItem);
                await _basketItemRepository.SaveChangesAsync();
            }
            else
            {
                List<BasketPostDto>? basketDtos = new List<BasketPostDto>();

                var basketJson = _http.HttpContext.Request.Cookies["basket"];
                if (basketJson == null)
                {
                    if (product == null) return new CustomResponse<Basket> { IsSuccess = false, Message = "Product not found" };
                }
                else
                {
                    basketDtos = JsonConvert.DeserializeObject<List<BasketPostDto>>(basketJson);

                    BasketPostDto? basketDto = basketDtos.FirstOrDefault(x => x.Id == id);

                    if (basketDto == null)
                    {
                        return new CustomResponse<Basket> { IsSuccess = false, Message = "Product not found" };
                    }
                    else if (basketDto.Count == 1)
                    {
                        basketDtos.Remove(basketDto);
                    }
                    else
                    {
                        basketDto.Count--;
                    }

                }
                basketJson = JsonConvert.SerializeObject(basketDtos);
                _http.HttpContext.Response.Cookies.Append("basket", basketJson);
             
            }
            return new CustomResponse<Basket> { IsSuccess = true, Message = "Process is succesful"};
        }
        public async Task<CustomResponse<BasketGetDto>> GetBasket()
        {
            BasketGetDto basketGetDto = new BasketGetDto();

            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

                    var basketquery = await _basketRepository.GetQuery(x => !x.IsDeleted && x.AppUserId == appUser.Id)
                        .Include(x => x.BasketItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.ProductImages)
                        .FirstOrDefaultAsync();

                    if (basketquery != null)
                    {
                        basketGetDto.basketItems = basketquery.BasketItems
             .Where(item => !item.IsDeleted) 
             .Select(item => new BasketItemDto
             {
                 Count = item.Count,
                 Id = item.ProductId,
                 Image = item.Product.ProductImages.FirstOrDefault(img => !img.IsDeleted && img.IsMain)?.Image,
                 Name = item.Product.Title,
                 DiscountPrice = item.Product.DiscountPercent == 0 ? item.Product.Price : Math.Round((item.Product.Price * (100 - item.Product.DiscountPercent)) / 100, 1),
             }).ToList();

               
                    basketGetDto.TotalPrice = basketGetDto.basketItems.Sum(x => (x.DiscountPrice * x.Count));

                }
            }
            else
            {
                var basketJson = _http.HttpContext.Request.Cookies["basket"];

                if (basketJson != null)
                {
                    List<BasketPostDto> basketDtos = JsonConvert.DeserializeObject<List<BasketPostDto>>(basketJson);

                    foreach (var item in basketDtos)
                    {
                        var product = await _productService.GetAsync(item.Id);
                        if (product != null)
                        {
                            var basketItem = new BasketItemDto
                            {
                                Id = product.Data.Id,
                                Count = item.Count,
                                Image = product.Data.ProductImages.FirstOrDefault(x => !x.IsDeleted && x.IsMain).Image,
                                Name = product.Data.Title,
                                DiscountPrice = product.Data.DiscountPercent == 0 ? product.Data.Price : Math.Round((product.Data.Price * (100 - product.Data.DiscountPercent)) / 100, 1),
                            };
                            basketGetDto.basketItems.Add(basketItem);
                            basketGetDto.TotalPrice += basketItem.DiscountPrice * basketItem.Count;
                        }
                    }
                }
            }
          
            return new CustomResponse<BasketGetDto> { IsSuccess = true, Message = "Process is succesful", Data = basketGetDto };
        }
        public async Task<CustomResponse<Basket>> RemoveAllBasketItems()
        {
            try
            {
                var user = _http.HttpContext.User;

                if (user.Identity.IsAuthenticated)
                {
                    AppUser appUser = await _userManager.FindByNameAsync(user.Identity.Name);
                    var basket = await _basketRepository.GetQuery(x => !x.IsDeleted && x.AppUserId == appUser.Id)
                                                         .Include(x => x.BasketItems)
                                                         .FirstOrDefaultAsync();
                    if (basket != null)
                    {
                        foreach (var item in basket.BasketItems)
                        {
                            item.IsDeleted = true;
                        }
                        await _basketRepository.SaveChangesAsync();
                        return new CustomResponse<Basket> { IsSuccess = true, Message = "All products removed from the Basket", Data = null };
                    }
                    else
                    {
                        return new CustomResponse<Basket> { IsSuccess = false, Message = "Basket not found", Data = null };
                    }
                }
                else
                {
                
                    _http.HttpContext.Response.Cookies.Delete("basket");
                    return new CustomResponse<Basket> { IsSuccess = true, Message = "Basket removed from the cookie", Data = null };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while removing basket items: " + ex.Message);
                return new CustomResponse<Basket> { IsSuccess = false, Message = "An error occurred while removing Basket", Data = null };
            }
        }

        public async Task<CustomResponse<double>> ApplyCoupon(string code, CustomResponse<BasketGetDto> dto)
        {
            int couponId = await _couponService.CheckCouponValidity(code);
            dto.Data.TotalPrice = dto.Data.basketItems.Sum(x => (x.DiscountPrice * x.Count));
            if (couponId == 0)
            {
                dto.Data.TotalPrice = dto.Data.basketItems.Sum(x => (x.DiscountPrice * x.Count));
                return new CustomResponse<double>
                {
                    IsSuccess = false,
                    Data = dto.Data.TotalPrice,
                    Message = "Invalid coupon ID or coupon is not active."

                };

            }
            var coupon = await _couponService.GetAsync(couponId);
            double discountAmount = 0;
            if (coupon != null && coupon.Data != null && dto.Data.TotalPrice > 0)
            {
                discountAmount = (dto.Data.TotalPrice * coupon.Data.DiscountAmount) / 100;
            }
            double discountedTotalPrice = dto.Data.TotalPrice - discountAmount;
            dto.Data.TotalPrice = discountedTotalPrice;
            dto.Data.IsCouponApplied = true;
            dto.Data.CuponPrice = discountedTotalPrice;
            return new CustomResponse<double>
            {
                IsSuccess = true,
                Data = dto.Data.TotalPrice,
                Message = $"Total price after applying the coupon: {discountedTotalPrice}"
            };
        }
    }
}

