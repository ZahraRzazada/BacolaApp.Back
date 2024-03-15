using System;

using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
	public class WishlistGetDto
	{
        public List<WishlistItemDto> wishlistItems { get; set; }
        public WishlistGetDto()
        {
            wishlistItems = new List<WishlistItemDto>();
        }
    }
    public class WishlistItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public bool InStock { get; set; }
    }
}

