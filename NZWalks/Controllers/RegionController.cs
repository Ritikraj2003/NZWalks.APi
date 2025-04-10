using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var regions=  await regionRepository.GetAllAsync();

            //Map Domain Model To DTO

            var regiondto = new List<ResgionDto>();
            foreach (var region in regions)
            {
                //regiondto.Add(region);
                
                    regiondto.Add(new ResgionDto()
                    {
                        id = region.id,
                        Code = region.Code,
                        Name = region.Name,
                        RegionImageUrl = region.RegionImageUrl
                    });
            }
            return Ok(regiondto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> getById([FromRoute]Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
            if (region== null)
            {
                return NotFound();
            }
              return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Regions
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            await dbContext.Regions.AddAsync(regionDomainModel);
           await dbContext.SaveChangesAsync();


            var regiondto = new Regions
            {
                id = regionDomainModel.id,
                Code = addRegionRequestDto.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl


            };

            return CreatedAtAction(nameof(getById),new {id= regiondto.id},regiondto);
        }

        //update region

        [HttpPut]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] updateRegionDto updateRegionDto)
        {
            var updateDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
            if (updateDomainModel == null)
            {
                return NotFound();
            }
            updateDomainModel.Code = updateRegionDto.Code;
            updateDomainModel.Name = updateRegionDto.Name;
            updateDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;
           await dbContext.SaveChangesAsync();

            var resgiondto = new ResgionDto
            {
                id = updateDomainModel.id,
                Code = updateDomainModel.Code,
                Name = updateDomainModel.Name,
                RegionImageUrl = updateDomainModel.RegionImageUrl
            };

            return Ok(resgiondto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }

            //delete  now
            dbContext.Regions.Remove(regionDomainModel);
             await dbContext.SaveChangesAsync();

            var resgiondto = new ResgionDto
            {
                id = regionDomainModel.id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };


            return Ok(resgiondto);

        }

    }
}
