using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class ParentCommentRepository : Repository<ParentComment>, IParentCommentRepository
    {
        public ParentCommentRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

