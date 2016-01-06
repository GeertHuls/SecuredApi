using System.Threading.Tasks;
using IdentityServer.Config;
using IdentityServer.UserStore.Model;

namespace IdentityServer.UserStore
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string subject);
        Task<User> GetUserAsync(string userName, string password);
        void SaveUser(User newUser);
        Task<User> GetUserForExternalProviderAsync(string loginProvider, string providerKey);
    }
}