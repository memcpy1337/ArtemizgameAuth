using Application.Common.SwaggerSchemaFilters.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Common.DTOs.Auth;

[SwaggerSchemaFilter(typeof(LoginUserDtoSchemaFilter))]
[SwaggerSchema(Required = new[] { "User" })]
public class LoginUserRequest
{

    //[SwaggerSchema(Required = new[] { "The User Email" })]
    //public string Email { get; set; }
    //[SwaggerSchema(Required = new[] { "The User Password" })]
    //public string Password { get; set; }
    [SwaggerSchema(Required = new[] { "The Device Id" })]
    public string DeviceId { get; set; }

    [SwaggerSchema(Required = new[] { "The Game Name to log on" })]
    public string GameName { get; set; }
}