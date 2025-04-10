using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class InMemoryResgionRepository : IRegionRepository
    {
        public async Task<List<Regions>> GetAllAsync()
        {
            return new List<Regions>
            {
                new Regions()
                {
                    id = Guid.NewGuid(),
                    Code = "Ritik",
                    Name = "Ritik's World"

                }
            };
        }

        public Task<Regions> GetByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
