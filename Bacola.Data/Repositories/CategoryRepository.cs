using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

