using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImpeRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public ImpeRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public NZWalksDbContext DbContext { get; }

        public async Task<List<Regions>> GetAllAsync()
        {
           return await dbContext.Regions.ToListAsync();
        }

        public Task<Regions> GetByIdAsync()
        {
           return
        }
    }
}
