using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImpDifficulty : IDifficulty
    {
        private readonly NZWalksDbContext dbContext;

        public ImpDifficulty(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Difficulty> Create(Difficulty difficulty)
        {
             await dbContext.Difficulties.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty> DeleteById(Guid id)
        {
           var exixting =  await dbContext.Difficulties.FirstOrDefaultAsync(x=>x.id==id);
            if (exixting==null) 
                {
                return null;
                }
               dbContext.Difficulties.Remove(exixting);
                await dbContext.SaveChangesAsync();
            return exixting;

        }

        public async Task<List<Difficulty>> GetAll()
        {
          return  await dbContext.Difficulties.ToListAsync();
        }
    }

}
