using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.WindowsAuthentication.Services;

namespace WsFederationServer.Services
{
    public class AdditionalWindowsClaimsProvider: ICustomClaimsProvider
    {
        public async Task TransformAsync(CustomClaimsProviderContext context)
        {
            var email = await GetEmailFromActiveDirectoryAsync(context.OutgoingSubject);
            context.OutgoingSubject.AddClaim(new Claim("email", email));
        }

        /// <summary>
        /// In a real implemention you look for an email in a mapping table or most likely in active directory.
        /// 
        /// This hard-coded email however will trigger the idenity server to ask for additional information since
        /// the user is not known yet in the user store and he used windows authentication to authenticate.
        /// </summary>
        /// <param name="outgoingSubject"></param>
        /// <returns></returns>
        private Task<string> GetEmailFromActiveDirectoryAsync(ClaimsIdentity outgoingSubject)
        {
            return Task.FromResult("foo@bar.com");
        }
    }
}