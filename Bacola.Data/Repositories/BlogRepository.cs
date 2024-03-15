using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

