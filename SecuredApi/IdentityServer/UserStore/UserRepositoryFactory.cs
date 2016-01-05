using System;

namespace IdentityServer.UserStore
{
    public static class UserRepositoryFactory
    {
        private static readonly Lazy<IUserRepository> UserRepository =
            new Lazy<IUserRepository>(() => new UserRepository(@"app_data/userstore.json"));

        public static IUserRepository Create()
        {
            return UserRepository.Value;
        }
    }
}