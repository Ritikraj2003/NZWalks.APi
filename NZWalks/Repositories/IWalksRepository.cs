using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {
        Task<Walks> CreateAsync(Walks walks);

        Task<List<Walks>> GetAllAsync(string? FilterOn = null, string? fillterQuary= null);
        Task<Walks> GetByIdAsync(Guid id);

        Task<Walks> UpdateAsync( Guid id ,Walks walks);

        Task<Walks> DeleteById( Guid id);
    }
}
