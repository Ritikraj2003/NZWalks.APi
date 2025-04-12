using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImpWalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext dbContext;

        public ImpWalksRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walks> CreateAsync(Walks walks)
        {
            await dbContext.Walks.AddAsync(walks);
            await dbContext.SaveChangesAsync();
            return walks;
        }

        public async Task<List<Walks>> GetAllAsync()
        {
            var result= await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return result;
        }
    }
}
