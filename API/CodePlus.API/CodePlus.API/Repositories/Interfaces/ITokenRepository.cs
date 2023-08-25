using Microsoft.AspNetCore.Identity;

namespace CodePlus.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);

    }
}
