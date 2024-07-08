using APIEscolaMinimal2.Models;
using APIEscolaMinimal2.Services;
using Microsoft.AspNetCore.Authorization;

namespace APIEscolaMinimal2.ApiEndpoints;

public static class AutenticacaoEndpoints
{
    public static void MapAutenticacaoEndpoints(this WebApplication app)
    {
        app.MapPost("/login", [AllowAnonymous] (User user, ITokenService tokenService) =>
        {
            if (user is null) return Results.BadRequest("Login Inválido");

            if(user.Name == "leandro" && user.Password == "123456")
            {
                var tokenString = tokenService.GetToken(
                    app.Configuration["Jwt:Key"]!,
                    app.Configuration["Jwt:Issuer"]!,
                    app.Configuration["Jwt:Audience"]!,
                    user);
                return Results.Ok( new { token = tokenString } );
            }
            else
            {
                return Results.BadRequest("Login Inválido");
            }
        }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .WithName("Login")
            .WithTags("Autenticação");
    }
}
