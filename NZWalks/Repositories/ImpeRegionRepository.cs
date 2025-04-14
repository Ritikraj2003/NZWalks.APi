using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
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

     

        public async Task<Regions> CreateAsync(Regions region)
        {
             await dbContext.Regions.AddAsync(region);
             await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Regions> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
             if(existingRegion== null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;

        }

        public async Task<List<Regions>> GetAllAsync()
        {
           return await dbContext.Regions.ToListAsync();
        }

        public async Task<Regions?> GetByIdAsync(Guid id)
        {
           return await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Regions?> UpdateAsunc(Guid id, Regions regions)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);

            if(existingRegion== null)
            {
                return null;
            }
            existingRegion.Code = regions.Code;
            existingRegion.Name = regions.Name;
            existingRegion.RegionImageUrl = regions.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
