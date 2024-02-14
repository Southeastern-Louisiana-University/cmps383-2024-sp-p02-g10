using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Selu383.SP24.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {
            if (createUserDto.UserName == null) 
            {
                return BadRequest();
            }

            if (createUserDto.Password == null)
            {
                return BadRequest();
            }

            if (createUserDto.UserName == "")
            {
                return BadRequest();
            }


            var user = await userManager.FindByNameAsync(createUserDto.UserName);
            if(user != null)
            {
                return BadRequest();
            }

            if(createUserDto.Password == null)
            {
                return BadRequest();
            }

            if (!createUserDto.Roles.Any())
            {
                return BadRequest();
            }

            var roles = await roleManager.Roles.Select(x => x.Name).ToListAsync();
            foreach (var x in createUserDto.Roles)
            {
                if (!roles.Contains(x))
                {
                    return BadRequest();
                }
            }

            var userToCreate = new User
            {
                UserName = createUserDto.UserName
            };

            var result = await userManager.CreateAsync(userToCreate, createUserDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            await userManager.AddToRolesAsync(userToCreate, createUserDto.Roles);

            var userToReturn = new UserDto
            {
                Id = userToCreate.Id,
                UserName = createUserDto.UserName,
                Roles = createUserDto.Roles,

            };

            return Ok(userToReturn);


        }
    }
}
