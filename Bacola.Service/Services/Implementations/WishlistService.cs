using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Migrations;
using Bacola.Data.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Wishlist = Bacola.Core.Entities.Wishlist;

namespace Bacola.Service.Services.Implementations
{
    public class WishlistService : IWishlistService
    {
        readonly IProductService _productService;

        public WishlistService(IProductService productService, IHttpContextAccessor http, UserManager<AppUser> userManager, IWishlistRepository wishlistRepository, IWishlistItemRepository wishlistItemRepository)
        {
            _productService = productService;
            _http = http;
            _userManager = userManager;
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
        }

        readonly IHttpContextAccessor _http;
        readonly IWishlistRepository _wishlistRepository;
        readonly IWishlistItemRepository _wishlistItemRepository;
        readonly UserManager<AppUser> _userManager;

        public async Task<CustomResponse<Wishlist>> AddWishlist(int id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null) return new CustomResponse<Wishlist> { IsSuccess = false, Message = "Product not found" };
            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

                var wishlist = await _wishlistRepository.GetAsync(x => !x.IsDeleted && x.AppUserId == appUser.Id, "WishlistItems");

                if (wishlist != null)
                {
                    var wishlistItem = wishlist.WishlistItems.FirstOrDefault(x => !x.IsDeleted && x.ProductId == id);

                    if (wishlistItem != null)
                    {
                        return new CustomResponse<Wishlist> { IsSuccess = false, Message = "his product is exists" };
                    }
                    else
                    {
                        wishlistItem = new WishlistItem
                        {
                            Wishlist = wishlist,
                            ProductId = id,
                            DateAdded = DateTime.UtcNow
                        };
                        await _wishlistItemRepository.AddAsync(wishlistItem);
                    }
                }
                else
                {
                    wishlist = new Wishlist { AppUserId = appUser.Id };
                    await _wishlistRepository.AddAsync(wishlist);

                    var wishlistItem = new WishlistItem
                    {
                        Wishlist = wishlist,
                        ProductId = id,
                        DateAdded = DateTime.UtcNow
                    };
                    await _wishlistItemRepository.AddAsync(wishlistItem);
                }

                await _wishlistRepository.SaveChangesAsync();
            }
            else
            {
                List<WishlistPostDto> wishlistDtos = new List<WishlistPostDto>();

                var wishlistJson = _http.HttpContext.Request.Cookies["wishlist"];

                if (wishlistJson == null)
                {
              
                    WishlistPostDto wishlistDto = new WishlistPostDto()
                    {
                        Id = id
                    };
                    wishlistDtos.Add(wishlistDto);
                }
                else
                {
                    wishlistDtos = JsonConvert.DeserializeObject<List<WishlistPostDto>>(wishlistJson);
                    WishlistPostDto wishlistDto = wishlistDtos.FirstOrDefault(x => x.Id == id);

                    if (wishlistDto == null)
                    {
                        wishlistDto = new WishlistPostDto()
                        {
                            Id = id
                        };
                        wishlistDtos.Add(wishlistDto);
                    }
                    else
                    {
                        var existingWishlistItem = new WishlistItemDto
                        {
                            Id = wishlistDto.Id,
                        };
                        return new CustomResponse<Wishlist> { IsSuccess = false, Message = "This Product is already in the wishlist" };
                    }
                }


                wishlistJson = JsonConvert.SerializeObject(wishlistDtos);
                _http.HttpContext.Response.Cookies.Append("wishlist", wishlistJson);
            }
            return new CustomResponse<Wishlist> { IsSuccess = true, Message = "Process is succesful" };
        }
        public async Task<CustomResponse<WishlistGetDto>> GetWishlist()
        {
            WishlistGetDto wishlistGetDto = new WishlistGetDto();
         
            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

                var wishlistQuery = await _wishlistRepository.GetQuery(x => !x.IsDeleted && x.AppUserId == appUser.Id)
                    .Include(x => x.WishlistItems)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                    .FirstOrDefaultAsync();

                if (wishlistQuery != null)
                {
                    wishlistGetDto.wishlistItems = wishlistQuery.WishlistItems
                        .Where(item => !item.IsDeleted)
                        .Select(item => new WishlistItemDto
                        {
                            Id = item.ProductId,
                            InStock=item.Product.InStock,
                            Name = item.Product.Title,
                            Image = item.Product.ProductImages.FirstOrDefault(img => !img.IsDeleted && img.IsMain)?.Image,
                            DiscountPrice = item.Product.DiscountPercent == 0 ? item.Product.Price : (item.Product.Price * item.Product.DiscountPercent) / 100,
                            Price =item.Product.Price
                        }).ToList();
                }
            }
            else
            {
                var wishlistJson = _http.HttpContext.Request.Cookies["wishlist"];

                if (wishlistJson != null)
                {
                    List<WishlistPostDto> wishlistDtos = JsonConvert.DeserializeObject<List<WishlistPostDto>>(wishlistJson);

                    foreach (var item in wishlistDtos)
                    {
                        var product = await _productService.GetAsync(item.Id);
                        if (product != null)
                        {
                            var wishlistItem = new WishlistItemDto
                            {
                                Id = product.Data.Id,
                                Name = product.Data.Title,
                                Image = product.Data.ProductImages.FirstOrDefault(x => !x.IsDeleted && x.IsMain).Image,
                                DiscountPrice = product.Data.DiscountPercent == 0 ? product.Data.Price : (product.Data.Price * product.Data.DiscountPercent) / 100,
                            };
                            wishlistGetDto.wishlistItems.Add(wishlistItem);
                        }
                    }
                }
            }

            return new CustomResponse<WishlistGetDto> { IsSuccess = true, Message = "Process is successful", Data = wishlistGetDto };
        }
        public async Task<CustomResponse<Wishlist>> RemoveAllWishlist()
        {
            try
            {
                var user = _http.HttpContext.User;

                if (user.Identity.IsAuthenticated)
                {
                    AppUser appUser = await _userManager.FindByNameAsync(user.Identity.Name);
                    var wishlist = await _wishlistRepository.GetQuery(x => !x.IsDeleted && x.AppUserId == appUser.Id)
                                                            .Include(x => x.WishlistItems)
                                                            .FirstOrDefaultAsync();
                    if (wishlist != null)
                    {
                        foreach (var item in wishlist.WishlistItems)
                        {
                            item.IsDeleted = true;
                        }
                        await _wishlistRepository.SaveChangesAsync();
                        return new CustomResponse<Wishlist> { IsSuccess = true, Message = "All products removed from the Wishlist", Data = null };
                    }
                    else
                    {
                        return new CustomResponse<Wishlist> { IsSuccess = false, Message = "Wishlist not found", Data = null };
                    }
                }
                else
                {
                    // If the user is not authenticated, remove the wishlist from the cookie
                    _http.HttpContext.Response.Cookies.Delete("wishlist");
                    return new CustomResponse<Wishlist> { IsSuccess = true, Message = "Wishlist removed from the cookie", Data = null };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while removing wishlist items: " + ex.Message);
                return new CustomResponse<Wishlist> { IsSuccess = false, Message = "An error occurred while removing Wishlist", Data = null };
            }
        }
        public async Task<CustomResponse<bool>> RemoveWishlistItemAsync(int id)
        {
            var wishlistItemResponse = await GetWishlistItemAsync(id);

            if (wishlistItemResponse.IsSuccess)
            {
                var wishlistItem = wishlistItemResponse.Data;

                if (_http.HttpContext.User.Identity.IsAuthenticated)
                {
                    // If the user is authenticated, mark the wishlist item as deleted
                    AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

                    var wishlistQuery = await _wishlistRepository.GetQuery(x => !x.IsDeleted && x.AppUserId == appUser.Id)
                        .Include(x => x.WishlistItems)
                        .FirstOrDefaultAsync();

                    if (wishlistQuery != null)
                    {
                        var itemToRemove = wishlistQuery.WishlistItems.FirstOrDefault(item => item.ProductId == id);

                        if (itemToRemove != null)
                        {
                            itemToRemove.IsDeleted = true;
                            await _wishlistRepository.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    // If the user is not authenticated, remove the wishlist item from the cookie
                    var wishlistJson = _http.HttpContext.Request.Cookies["wishlist"];

                    if (wishlistJson != null)
                    {
                        List<WishlistItemDto> wishlistItems = JsonConvert.DeserializeObject<List<WishlistItemDto>>(wishlistJson);
                        var wishlistItemToRemove = wishlistItems.FirstOrDefault(item => item.Id == id);

                        if (wishlistItemToRemove != null)
                        {
                            // Remove the wishlist item from the list
                            wishlistItems.Remove(wishlistItemToRemove);

                            // Update the cookie with the modified wishlist items
                            _http.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistItems));
                        }
                    }
                }

                return new CustomResponse<bool> { IsSuccess = true, Message = "Wishlist item removed successfully", Data = true };
            }
            else
            {
                return new CustomResponse<bool> { IsSuccess = false, Message = "Wishlist item not found", Data = false };
            }
        }
        public async Task<CustomResponse<WishlistItemDto>> GetWishlistItemAsync(int id)
        {
            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

                var wishlistQuery = await _wishlistRepository.GetQuery(x => !x.IsDeleted && x.AppUserId == appUser.Id)
                    .Include(x => x.WishlistItems)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                    .FirstOrDefaultAsync();

                if (wishlistQuery != null)
                {
                    var wishlistItem = wishlistQuery.WishlistItems
                        .FirstOrDefault(item => item.ProductId == id && !item.IsDeleted);

                    if (wishlistItem != null)
                    {
                        var wishlistItemDto = new WishlistItemDto
                        {
                            Id = wishlistItem.ProductId,
                            InStock = wishlistItem.Product.InStock,
                            Name = wishlistItem.Product.Title,
                            Image = wishlistItem.Product.ProductImages.FirstOrDefault(img => !img.IsDeleted && img.IsMain)?.Image,
                            DiscountPrice = wishlistItem.Product.DiscountPrice,
                            Price = wishlistItem.Product.Price
                        };

                        return new CustomResponse<WishlistItemDto> { IsSuccess = true, Message = "Wishlist item retrieved successfully", Data = wishlistItemDto };
                    }
                    else
                    {
                        return new CustomResponse<WishlistItemDto> { IsSuccess = false, Message = "Wishlist item not found", Data = null };
                    }
                }
                else
                {
                    return new CustomResponse<WishlistItemDto> { IsSuccess = false, Message = "Wishlist not found for the current user", Data = null };
                }
            }
            else
            {
                var wishlistJson = _http.HttpContext.Request.Cookies["wishlist"];

                if (wishlistJson != null)
                {
                    List<WishlistItemDto> wishlistItems = JsonConvert.DeserializeObject<List<WishlistItemDto>>(wishlistJson);
                    var wishlistItem = wishlistItems.FirstOrDefault(item => item.Id == id);

                    if (wishlistItem != null)
                    {
                        return new CustomResponse<WishlistItemDto> { IsSuccess = true, Message = "Wishlist item retrieved successfully", Data = wishlistItem };
                    }
                    else
                    {
                        return new CustomResponse<WishlistItemDto> { IsSuccess = false, Message = "Wishlist item not found in the cookie", Data = null };
                    }
                }
                else
                {
                    return new CustomResponse<WishlistItemDto> { IsSuccess = false, Message = "Wishlist not found in the cookie", Data = null };
                }
            }
        }




    }


}


