using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;

namespace IdentityServer.UserStore
{
    public class CustomUserService : UserServiceBase
    {
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = await UserRepositoryFactory.Create()
                .GetUserAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.AuthenticateResult = new AuthenticateResult("Invalid credentials");
            }
            else
            {
                var givenNameClaim = user.UserClaims.First(c => c.ClaimType == Constants.ClaimTypes.GivenName).ClaimValue;
                context.AuthenticateResult = new AuthenticateResult(user.Subject, givenNameClaim);
            }
        }
    }
}