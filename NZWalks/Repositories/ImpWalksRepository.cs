using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Walks> DeleteById(Guid id)
        {
           var existingWalkDomain =  await dbContext.Walks.FirstOrDefaultAsync(x=>x.Id==id);
            if(existingWalkDomain == null)
            {
                return null;
            }
            dbContext.Remove(existingWalkDomain);
            return existingWalkDomain;
        }

        public async Task<List<Walks>> GetAllAsync()
        {
            var result= await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return result;
        }

        public async Task<Walks> GetByIdAsync(Guid id)
        {
           return  await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Walks> UpdateAsync(Guid id, Walks walks)
        {
            var existingWalk =  await dbContext. Walks.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingWalk == null)
            {
                return null;        
            }
            existingWalk.Name=walks.Name;
            existingWalk.Description=walks.Description; 
            existingWalk.lengthInKm=walks.lengthInKm;
            existingWalk.WalkImageUrl=walks.WalkImageUrl;   
            existingWalk.DifficultyId=walks.DifficultyId;   
            existingWalk.RegionId=walks.RegionId;

            await dbContext.SaveChangesAsync();

               return existingWalk;

        }
    }
}
