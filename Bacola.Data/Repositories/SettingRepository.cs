using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;

namespace Bacola.Data.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        public SettingRepository(BacolaDbContext context) : base(context)
        {
        }
    }
}

