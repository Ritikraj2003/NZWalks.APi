using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalksRepository walksRepository;

        public WalksController(IMapper mapper, IWalksRepository walksRepository)
        {
            this.mapper = mapper;
            this.walksRepository = walksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // map  this dto into domail model
            var walkDomainModel = mapper.Map<Walks>(addWalkRequestDto);

            await walksRepository.CreateAsync(walkDomainModel);
            // Map domain Modal to  Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getWalk = await walksRepository.GetAllAsync();

            return Ok(mapper.Map<List<WalkDto>>(getWalk));
            //return Ok(getWalk);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
           var walkDomainModel =   await walksRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //Mapping  Domain moddel to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>UpdateWalk([FromRoute] Guid id, UpdateWalkDto updateWalkDto)
        {
            //Map DTO to Domain model
            var WalkDomainModel = mapper.Map<Walks>(updateWalkDto);
               WalkDomainModel= await walksRepository.UpdateAsync(id, WalkDomainModel);
            if (WalkDomainModel == null)
            {
                return NotFound();  
            }
             //Map Domain model to DTO

            return Ok(mapper.Map<WalkDto>(WalkDomainModel));
               


        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
           var deletedDomainModel =  await walksRepository.DeleteById(id);

            //Map to domain model to dto
            return Ok(mapper.Map<WalkDto>(deletedDomainModel));
        }
    }
}
