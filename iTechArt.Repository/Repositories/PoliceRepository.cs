﻿using iTechArt.Domain.RepositoryInterfaces;
using iTechArt.Database.DbContexts;
using iTechArt.Domain.ModelInterfaces;

namespace iTechArt.Repository.Repositories
{
    public sealed class PoliceRepository : Repository<IPolice>, IPoliceRepository
    {
        public PoliceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
