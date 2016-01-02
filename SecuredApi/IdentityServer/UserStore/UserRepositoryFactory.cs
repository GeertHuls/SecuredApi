namespace IdentityServer.UserStore
{
    public static class UserRepositoryFactory
    {
        public static IUserRepository Create()
        {
            return new UserRepository(@"app_data/userstore.json");
        }
    }
}