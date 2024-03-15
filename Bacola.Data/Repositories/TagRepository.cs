    using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

