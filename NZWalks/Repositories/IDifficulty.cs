using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IDifficulty
    {
        Task<List<Difficulty>>GetAll();
        Task<Difficulty> Create(Difficulty difficulty);
        Task<Difficulty> DeleteById(Guid id);
    }
}
