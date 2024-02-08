using Microsoft.AspNetCore.Identity;
using Selu383.SP24.Api.Features.Authentication;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> Roles { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string[] Roles { get; set; }
}

public class LoginDto
{
    public string UserName { get; set; } 

    public string Password { get; set; }
}