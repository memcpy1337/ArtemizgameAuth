using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Domain.Entities;

namespace Infrastructure.Services;

internal sealed class AccessTokenService : IAccessTokenService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly JwtSettings _jwtSettings;

    public AccessTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings) =>
        (_tokenGenerator, _jwtSettings) = (tokenGenerator, jwtSettings);

    public string Generate(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Jti, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName)
        };
        return _tokenGenerator.Generate(new GenerateTokenRequest(_jwtSettings.AccessTokenSecret, _jwtSettings.Issuer,
            _jwtSettings.Audience,
            _jwtSettings.AccessTokenExpirationMinutes, claims)).Token;
    }
}