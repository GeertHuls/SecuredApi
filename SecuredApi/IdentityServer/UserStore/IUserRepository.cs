using System.Threading.Tasks;
using IdentityServer.Config;
using IdentityServer.UserStore.Model;

namespace IdentityServer.UserStore
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userName, string password);
    }
}