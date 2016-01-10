using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;

namespace IdentityServer.UserStore
{
    public class CustomUserService : UserServiceBase
    {
        private readonly IUserRepository _userRepository;

        public CustomUserService()
        {
            _userRepository = UserRepositoryFactory.Create();
        }

        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            var externalIdentity = context.ExternalIdentity;
            context.AuthenticateResult = await GetAppropriateAuthenticationResult(externalIdentity);
        }

        private async Task<AuthenticateResult> GetAppropriateAuthenticationResult(ExternalIdentity externalIdentity)
        {
            var account = await _userRepository.GetUserForExternalProviderAsync(externalIdentity.Provider,
                externalIdentity.ProviderId);

            if (account != null)
            {
                return new AuthenticateResult(
                    account.Subject,
                    account.UserClaims.First(c => c.ClaimType == Constants.ClaimTypes.GivenName).ClaimValue,
                    account.UserClaims.Select(uc => new Claim(uc.ClaimType, uc.ClaimValue)),
                    authenticationMethod: Constants.AuthenticationMethods.External,
                    identityProvider: externalIdentity.Provider);
            }

            var emailClaim = externalIdentity.Claims.FirstOrDefault(c => c.Type == "email");
            if (emailClaim == null)
            {
                return new AuthenticateResult("No email claim available.");
            }

            var userWithMatchingEmailClaim = await _userRepository.GetUserByEmailAsync(emailClaim.Value);
            if (userWithMatchingEmailClaim == null)
            {
                return new AuthenticateResult("No existing account found");
            }

            await _userRepository.AddUserLoginAsync(
                userWithMatchingEmailClaim.Subject,
                externalIdentity.Provider,
                externalIdentity.ProviderId);

            return new AuthenticateResult(
                   userWithMatchingEmailClaim.Subject,
                   userWithMatchingEmailClaim.UserClaims.First(c => c.ClaimType == Constants.ClaimTypes.GivenName).ClaimValue,
                   userWithMatchingEmailClaim.UserClaims.Select(uc => new Claim(uc.ClaimType, uc.ClaimValue)),
                   authenticationMethod: Constants.AuthenticationMethods.External,
                   identityProvider: externalIdentity.Provider);
        }

        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = await _userRepository
                .GetUserAsync(context.UserName, context.Password);

            context.AuthenticateResult = user == null
                ? new AuthenticateResult("Invalid credentials")
                : new AuthenticateResult(user.Subject, user.UserName);
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userRepository
                .GetUserAsync(context.Subject.GetSubjectId());

            var claims = new List<Claim>
                {
                    new Claim(Constants.ClaimTypes.Subject, user.Subject),
                }

                .Union(
                    user.UserClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)))
                    
                .Where(c => ClaimIsRequestedOnly(context, c));

            context.IssuedClaims = claims;
        }

        private static bool ClaimIsRequestedOnly(ProfileDataRequestContext context, Claim claim)
        {
            return !context.AllClaimsRequested && context.RequestedClaimTypes.Contains(claim.Type);
        }

        public override async Task IsActiveAsync(IsActiveContext context)
        {
            var user =  await _userRepository.GetUserAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null && user.IsActive;
        }
    }
}