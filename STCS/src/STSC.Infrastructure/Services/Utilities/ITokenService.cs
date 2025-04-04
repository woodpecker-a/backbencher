using System.Security.Claims;

namespace STCS.Infrastructure.Services.Utilities;

public interface ITokenService
{
    Task<string> GetJwtToken(IList<Claim> claims);
}