using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Wishlist:BaseEntity
	{
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public List<WishlistItem> WishlistItems { get; set; } = null!;
    }
}

