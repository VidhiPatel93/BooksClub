using Microsoft.AspNetCore.Identity;

namespace BookClub.API.Repository
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
