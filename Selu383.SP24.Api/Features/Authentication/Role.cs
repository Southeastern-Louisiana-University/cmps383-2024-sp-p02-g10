using Microsoft.AspNetCore.Identity;
using Selu383.SP24.Api.Features.Authentication;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> Users { get; set; }
}