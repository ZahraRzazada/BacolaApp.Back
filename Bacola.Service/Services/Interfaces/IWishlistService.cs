using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IWishlistService
    {
        public Task<CustomResponse<WishlistItemDto>> GetWishlistItemAsync(int id);
        public Task<CustomResponse<Wishlist>> AddWishlist(int id);
        public Task<CustomResponse<WishlistGetDto>> GetWishlist();
        public Task<CustomResponse<Wishlist>> RemoveAllWishlist();
        public Task<CustomResponse<bool>> RemoveWishlistItemAsync(int id);
    }
}

