using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
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