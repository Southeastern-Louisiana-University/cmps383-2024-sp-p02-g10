using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Selu383.SP24.Api.Controllers
{
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
            var userToLogin = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
            };
            return Ok(userToLogin);
        }
    }
}
