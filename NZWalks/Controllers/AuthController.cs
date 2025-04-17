using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //Post:/api/auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RagisterRequestDto ragisterRequestDto)
        {
            var idnentityUser = new IdentityUser
            {
                UserName = ragisterRequestDto.username,
                Email = ragisterRequestDto.username
            };
             var identityResult = await userManager.CreateAsync(idnentityUser, ragisterRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // add roles to user
                if(ragisterRequestDto.Roles != null && ragisterRequestDto.Roles.Any())
                {
                    identityResult= await userManager.AddToRolesAsync(idnentityUser, ragisterRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Created");
                    }
                }

            }
            return BadRequest("User was not created");

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
              var cheackPasswordResult =    await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (cheackPasswordResult)
                {
                    //Get role for this user
                  var roles =  await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        //Create token
                     var JwtToken =   tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = JwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Ragister Before login");
        }
    }
}
