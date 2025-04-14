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

        public async Task<List<Walks>> GetAllAsync(string? FilterOn = null, string? fillterQuary = null, string? sort = null, bool isascending = true)
        {
             var walks=dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filter

            if (string.IsNullOrWhiteSpace(FilterOn)== false && string.IsNullOrWhiteSpace(fillterQuary) == false)
            {
                if (FilterOn.Equals("name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(fillterQuary));
                }

               
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sort) == false)
            {
                if(sort.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isascending ? walks.OrderBy(x => x.Name):walks.OrderByDescending(x => x.Name);
                }
                else if(sort.Equals("Length",StringComparison.OrdinalIgnoreCase))
                {
                    walks=isascending? walks.OrderBy(x => x.lengthInKm) : walks.OrderByDescending(x=>x.lengthInKm);
                }
            }

            return await walks.ToListAsync();
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
