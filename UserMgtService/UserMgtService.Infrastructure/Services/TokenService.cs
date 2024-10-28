﻿using Ardalis.GuardClauses;
using UserMgtService.Application.Abstraction.Services;
using UserMgtService.Application.DTOs;
using UserMgtService.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserMgtService.Infrastructure.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IOptions<TokenOptions> _tokenOptions;
        public TokenService(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = Guard.Against.Null(tokenOptions);
            Guard.Against.NullOrEmpty(_tokenOptions?.Value?.Key);
        }
        public string GenerateToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_tokenOptions.Value.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim(ClaimTypes.Name, user.Username),
                 new Claim(ClaimTypes.Role, user.Role),
             }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenOptions.Value.ExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _tokenOptions.Value.Audience,
                Issuer = _tokenOptions.Value.Issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
