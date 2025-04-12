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

        public WalksController(IMapper mapper,IWalksRepository walksRepository )
        {
            this.mapper = mapper;
            this.walksRepository = walksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // map  this dto into domail model
             var walkDomainModel= mapper.Map<Walks>(addWalkRequestDto);

            await walksRepository.CreateAsync(walkDomainModel);
            // Map domain Modal to  Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }
    }
}
