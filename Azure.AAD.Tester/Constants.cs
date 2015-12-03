namespace Azure.AAD.Tester
{
    public class Constants
    {

        public class AAD
        {
            //Edit the following lines and insert valid values
            public const string UPN = "<user>@<tenant>.onmicrosoft.com";
            public const string TenantId = "<tenant id>";
            public const string ClientId = "<client id>";
            public const string ClientSecret = "<client secret>";

            public const string ClientIdForUserAuthn = ""; //not used
            public const string AuthString = "https://login.microsoftonline.com/{0}";
            public const string ResourceUrl = "https://graph.windows.net";
        }
    }
}
