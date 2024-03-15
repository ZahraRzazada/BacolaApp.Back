    using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        public CouponRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

