using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Core.Repositories.Generic;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

