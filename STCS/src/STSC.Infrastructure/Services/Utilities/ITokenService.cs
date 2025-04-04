using System.Security.Claims;

namespace STSC.Infrastructure.Services.Utilities;

public interface ITokenService
{
    Task<string> GetJwtToken(IList<Claim> claims);
}