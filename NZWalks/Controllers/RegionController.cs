﻿using System.Collections.Generic;
using System.Text.Json;
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
   
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger1;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository ,IMapper mapper, ILogger<RegionController> logger1)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger1 = logger1;
        }

        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult>GetAll()
        {
            //logger1.LogInformation("Get ALl Action Method was Invoke....");

            //logger1.LogWarning("THis is a warring message...");

            //logger1.LogError("This is an error Log");
            

            //try
           // {
               // throw new Exception("This is a custom exception");
                var regions = await regionRepository.GetAllAsync();

                //logger1.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regions)}");
                return Ok(mapper.Map<List<ResgionDto>>(regions));
            //}
            //catch (Exception ex) 
           // {
           //     logger1.LogError(ex,ex.Message);
            //    throw;
           // }

            
        }

        [HttpGet ]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public  async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            
               

            return Ok(mapper.Map<ResgionDto>(region));
        }

        [HttpPost ]
        [validateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

         
                var regionDomainModel = mapper.Map<Regions>(addRegionRequestDto);

                await regionRepository.CreateAsync(regionDomainModel);

                var regiondto = mapper.Map<ResgionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regiondto.id }, regiondto);
        
            }
               
        

        //update region
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
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

        [HttpDelete ]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            var resgiondto = mapper.Map<ResgionDto>(regionDomainModel);


            return Ok(resgiondto);

        }

    }
}
