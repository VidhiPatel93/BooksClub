using BookClub.API.Models.DTO;
using BookClub.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.API.Controllers
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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegistereRequestDTO registereRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registereRequestDTO.Username,
                Email = registereRequestDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registereRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                if (registereRequestDTO.Roles != null && registereRequestDTO.Roles.Any()) 
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registereRequestDTO.Roles);

                    if (identityResult.Succeeded) 
                    {
                        return Ok("User is registered! Please Login.");
                    }
                }
            }

            return BadRequest("Something went Wrong");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null) 
                    {
                        var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response); 
                    }
                }
            }

            return BadRequest("Username or Password incorrect.");


        }
    }
}
