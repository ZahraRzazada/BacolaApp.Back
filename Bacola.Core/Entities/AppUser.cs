﻿using System;
using Microsoft.AspNetCore.Identity;

namespace Bacola.Core.Entities
{
	public class AppUser:IdentityUser
	{
		public string FullName { get; set; } = null!;
        public List<Basket> BasketItems { get; set; } = null!;
        public List<Wishlist> WishlistItems { get; set; } = null!;
        public List<Order> Orders { get; set; } = null!;
        public List<Coment> Coments { get; set; }

    }
}

