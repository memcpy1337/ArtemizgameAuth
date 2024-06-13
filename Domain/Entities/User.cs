using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public required string GameName { get; set; }
    public required string DeviceId { get; set; }
}