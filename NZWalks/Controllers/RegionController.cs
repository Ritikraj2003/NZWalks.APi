using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository ,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var regions=  await regionRepository.GetAllAsync();

            //Map Domain Model To DTO

            //var regiondto = new List<ResgionDto>();
            //foreach (var region in regions)
            //{


            //        regiondto.Add(new ResgionDto()
            //        {    id=region.id,
            //            Code = region.Code,
            //            Name = region.Name,
            //            RegionImageUrl = region.RegionImageUrl
            //        });
            //}

            
            return Ok(mapper.Map<List<ResgionDto>>(regions));
            
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
               //var regiondto = new ResgionDto
               //{
               //    id = region.id,
               //    Code = region.Code,
               //    Name = region.Name,
               //    RegionImageUrl = region.RegionImageUrl

               //};

            return Ok(mapper.Map<ResgionDto>(region));
        }

        [HttpPost]
        [validateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //if(ModelState.IsValid)
            //{
                var regionDomainModel = mapper.Map<Regions>(addRegionRequestDto);

                await regionRepository.CreateAsync(regionDomainModel);

                var regiondto = mapper.Map<ResgionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regiondto.id }, regiondto);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            }
               
        

        //update region
        [HttpPut]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] updateRegionDto updateRegionDto)
        {
            if(ModelState.IsValid)
            {
                //Map DTO TO main Model
                var regionDomainModel = mapper.Map<Regions>(updateRegionDto);

                var updateDomainModel = await regionRepository.UpdateAsunc(id, regionDomainModel);


                var resgiondto = mapper.Map<ResgionDto>(updateDomainModel);

                return Ok(resgiondto);
            }
            else
            {
                return BadRequest(ModelState);
            }
                
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            var resgiondto = mapper.Map<ResgionDto>(regionDomainModel);


            return Ok(resgiondto);

        }

    }
}
