using System.Linq;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace ResourceServer
{
    public class AuthorizationManager : ResourceAuthorizationManager
    {
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            var resourceName = context.Resource.First().Value;
            return Eval(context.Principal.HasClaim("role", resourceName));
        }
    }
}