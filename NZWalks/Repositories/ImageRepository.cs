using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImageRepository : IimageRepository
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageRepository(NZWalksDbContext dbContext, IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> CreateImage(Image image)
        {
            var localfilepath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",$"{image.FileName}{image.FileExtension}");

            //Upload Image to Local Path

            using var stream = new FileStream(localfilepath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Image/{image.FileName}{image.FileExtension}";

            image.Path = urlFilePath;

            //Add image to the Image Table

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;   
        }
    }
}
