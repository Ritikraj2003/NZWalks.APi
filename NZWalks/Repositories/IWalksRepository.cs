using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {
        Task<Walks> CreateAsync(Walks walks);

        Task<List<Walks>> GetAllAsync();
    }
}
