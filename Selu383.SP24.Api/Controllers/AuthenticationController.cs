using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Selu383.SP24.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if(user == null)
            {
                return BadRequest();
            }
            var passwordCheck= await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (!passwordCheck.Succeeded) {
                return BadRequest();
            }
            await signInManager.SignInAsync(user, false);
            var userToLogin = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
            };
            return Ok(userToLogin);
        }
        
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> me()
        {
            var user = await userManager.FindByNameAsync(User.Identity?.Name);

            var userToReturn = await (userManager.Users).Select(x => new UserDto 
            { Id = x.Id, UserName = x.UserName, Roles = x.Roles.Select(role => role.Role!.Name).ToArray()! })
                .SingleAsync(x => x.UserName == user.UserName);

            return Ok(userToReturn);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}
