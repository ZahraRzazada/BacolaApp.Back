using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class WishlistItem:BaseEntity
	{
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; } = null!;
        public DateTime DateAdded { get; set; }

    }
}

