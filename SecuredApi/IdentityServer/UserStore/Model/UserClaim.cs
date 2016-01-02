namespace IdentityServer.UserStore.Model
{
    public class UserClaim
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}