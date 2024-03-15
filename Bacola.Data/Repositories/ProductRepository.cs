using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

