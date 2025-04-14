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
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficulty difficultyRepo;
        private readonly IMapper mapper;

        public DifficultyController(IDifficulty difficultyRepo,IMapper mapper)
        {
            this.difficultyRepo = difficultyRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
         var DifficultyDomainModel=  await difficultyRepo.GetAll();

            //map  Domain model  to Dto
            return Ok(mapper.Map<List<DificultyDto>>(DifficultyDomainModel));
        }


        [HttpPost]
        public async Task<IActionResult>Create(AddDifficulty addDifficultyDto)
        {
            //Map Dto To Domain Model
            var addDifficulty = mapper.Map<Difficulty>(addDifficultyDto);
            await difficultyRepo.Create(addDifficulty);

            // Map to Domain Model to Dto
            var CreatedDto = mapper.Map<DificultyDto>(addDifficulty);

            return Ok(CreatedDto);



        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var  deletedModel= await difficultyRepo.DeleteById(id);

            return Ok(mapper.Map<DificultyDto>(deletedModel));

        }
    }
}
