using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class WishlistItemRepository : Repository<WishlistItem>, IWishlistItemRepository
    {
        public WishlistItemRepository(BacolaDbContext context) : base(context)
        {

        }
    }
}

