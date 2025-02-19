﻿using APIEscolaMinimal2.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIEscolaMinimal2.Services;

public class TokenService : ITokenService
{
    public string GetToken(string key, string issuer, string audience, User user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, user.Name!),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: issuer,
                                    audience: audience,
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(10),
                                    signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}
