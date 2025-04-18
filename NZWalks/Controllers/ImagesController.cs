using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IimageRepository iimageRepository;

        public ImagesController(IimageRepository iimageRepository)
        {
            this.iimageRepository = iimageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> CreateImage(ImageRequestingDto imageRequestingDto)
        {
            ValidateFileUpload(imageRequestingDto);
            if (ModelState.IsValid)
            {
                //Convert Dto to  DomainModel
                var ImageDomainModel = new Image
                {
                    File = imageRequestingDto.File,
                    FileExtension = Path.GetExtension(imageRequestingDto.File.FileName),
                    FileSizeInbyte = imageRequestingDto.File.Length,
                    FileName = imageRequestingDto.FileName,
                    Description = imageRequestingDto.Description,
                };
                //User Repository to upload image

                 await iimageRepository.CreateImage(ImageDomainModel);
                return Ok(ImageDomainModel);


            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageRequestingDto imageRequestingDto)
        {
            if (imageRequestingDto != null)
            {
                var allowedExtension = new String[] { ".jpg", "jpeg", "png" };
                if (!allowedExtension.Contains(Path.GetExtension(imageRequestingDto.File.FileName)))
                {
                    ModelState.AddModelError("File", "Unsupported File Extension");
                }
                if (imageRequestingDto.File.Length > 10485760)
                {
                    ModelState.AddModelError("File", "File Size is more than 10MB.");
                }
            }

        }
    }
}
