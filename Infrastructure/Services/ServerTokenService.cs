using Application.Common.Interfaces;
using Application.Common.Settings;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services;

public class ServerTokenService : IServerTokenService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly JwtSettings _jwtSettings;

    public ServerTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings) =>
        (_tokenGenerator, _jwtSettings) = (tokenGenerator, jwtSettings);

    public string Generate(string serverId)
    {
        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.NameId, serverId),
        };
        return _tokenGenerator.Generate(new GenerateTokenRequest(_jwtSettings.AccessTokenSecret, _jwtSettings.Issuer,
            _jwtSettings.Audience,
            _jwtSettings.ServerTokenExprirationMinutes, claims)).Token;
    }
}
