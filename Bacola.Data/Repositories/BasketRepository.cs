using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Core.Repositories.Generic;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        public BasketRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

