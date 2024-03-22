using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class ComentRepository : Repository<Coment>, IComentRepository
    {
        public ComentRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

