using APIEscolaMinimal2.Models;

namespace APIEscolaMinimal2.Services;

public interface ITokenService
{
    string GetToken(string key, string issuer, string audience, User user);
}
