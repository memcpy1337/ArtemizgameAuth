using Application.Common.SwaggerSchemaFilters.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Common.DTOs.Auth;

[SwaggerSchemaFilter(typeof(RegisterUserDtoSchemaFilter))]
public class RegisterUserRequest
{
    [SwaggerSchema(Required = new[] { "The User device id" })]
    public required string DeviceId { get; set; }
    [SwaggerSchema(Required = new[] { "The User Name id" })]
    public required string Username { get; set; }
    [SwaggerSchema(Required = new[] { "The GameName" })]
    public required string GameName { get; set; }

    //[SwaggerSchema(Required = new[] { "The User Email" })]
    //public string Email { get; set; }
    //[SwaggerSchema(Required = new[] { "The User Password" })]
    //public string Password { get; set; }
    //[SwaggerSchema(Required = new[] { "The User Confirmed Password" })]
    //public string ConfirmPassword { get; set; }
}