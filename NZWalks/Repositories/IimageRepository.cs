using System.Runtime.InteropServices;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IimageRepository
    {
     Task<Image>CreateImage(Image image);
    }
}
