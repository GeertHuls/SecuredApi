namespace IdentityServer.UserStore.Model
{
    public class UserLogin
    {
        public string Subject { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}