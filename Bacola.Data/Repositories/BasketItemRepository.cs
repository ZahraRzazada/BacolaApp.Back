using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class BasketItemRepository : Repository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(BacolaDbContext context) : base(context)
        {

        }
    }
}

