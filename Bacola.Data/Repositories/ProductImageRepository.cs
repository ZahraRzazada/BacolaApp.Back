using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

