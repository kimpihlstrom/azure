namespace Azure.AAD.Tester.Helpers
{
    public class TokenHelper
    {
        public string TenantId { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ClientIdForUserAuthn { get; set; }

        public TokenHelper(string TenantId, string ClientId, string ClientSecret, string ClientIdForUserAuthn)
        {
            this.TenantId = TenantId;
            this.ClientId = ClientId;
            this.ClientSecret = ClientSecret;
            this.ClientIdForUserAuthn = ClientIdForUserAuthn;
        }
    }
}
