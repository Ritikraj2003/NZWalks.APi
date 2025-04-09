using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var regions= dbContext.Regions.ToList();

                 return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult getById([FromRoute]Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = dbContext.Regions.FirstOrDefault(x => x.id == id);
            if (region== null)
            {
                return NotFound();
            }
              return Ok(region);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Regions
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();


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
        public IActionResult UpdateById([FromRoute] Guid id, [FromBody] updateRegionDto updateRegionDto)
        {
            var updateDomainModel = dbContext.Regions.FirstOrDefault(x => x.id == id);
            if (updateDomainModel == null)
            {
                return NotFound();
            }
            updateDomainModel.Code = updateRegionDto.Code;
            updateDomainModel.Name = updateRegionDto.Name;
            updateDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;
            dbContext.SaveChanges();

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
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.id == id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }

            //delete  now
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

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
