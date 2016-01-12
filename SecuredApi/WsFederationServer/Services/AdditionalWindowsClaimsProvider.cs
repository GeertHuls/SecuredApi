using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.WindowsAuthentication.Services;

namespace WsFederationServer.Services
{
    public class AdditionalWindowsClaimsProvider: ICustomClaimsProvider
    {
        public async Task TransformAsync(CustomClaimsProviderContext context)
        {
            var email = await GetEmailFromActiveDirectoryAsync();
            context.OutgoingSubject.AddClaim(new Claim("email", email));
        }

        /// <summary>
        /// In a real implemention you look for an email in a mapping table or most likely in active directory.
        /// </summary>
        /// <returns></returns>
        private Task<string> GetEmailFromActiveDirectoryAsync()
        {
            return Task.FromResult("geert.huls82@gmail.com");
        }
    }
}