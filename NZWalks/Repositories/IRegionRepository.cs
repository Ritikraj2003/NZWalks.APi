using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
       Task<List<Regions>>GetAllAsync();

       Task<Regions> GetByIdAsync(Guid id);

       Task<Regions> CreateAsync(Regions regions);

        Task<Regions?> UpdateAsunc(Guid id, Regions regions);

        Task<Regions?> DeleteAsync(Guid id);
    }
}
