using Microsoft.AspNetCore.Identity;

namespace ProjectTaskManagement.Infrastructure.Identity;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
