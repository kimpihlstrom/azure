using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace Azure.AAD.Tester.Helpers
{
    public class AuthenticationHelper
    {
        public static string TokenForUser;

        /// <summary>
        /// Async task to acquire token for Application.
        /// </summary>
        /// <returns>Async Token for application.</returns>
        public static async Task<string> AcquireTokenAsyncForApplication(TokenHelper token)
        {
            return await Task.Run(() => GetTokenForApplication(token));
        }

        /// <summary>
        /// Get Token for Application.
        /// </summary>
        /// <returns>Token for application.</returns>
        public static string GetTokenForApplication(TokenHelper token)
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(
                string.Format(Constants.AAD.AuthString, token.TenantId),
                false);
            // Config for OAuth client credentials
            ClientCredential clientCred = new ClientCredential(token.ClientId, token.ClientSecret);
            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(Constants.AAD.ResourceUrl,
                clientCred);

            return authenticationResult.AccessToken;
        }

        /// <summary>
        /// Get Active Directory Client for Application.
        /// </summary>
        /// <returns>ActiveDirectoryClient for Application.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClientAsApplication(TokenHelper token)
        {
            Uri servicePointUri = new Uri(Constants.AAD.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, token.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => await AcquireTokenAsyncForApplication(token));
            return activeDirectoryClient;
        }

        /// <summary>
        /// Async task to acquire token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static async Task<string> AcquireTokenAsyncForUser(TokenHelper token)
        {
            return await Task.Run(() => GetTokenForUser(token));
        }

        /// <summary>
        /// Get Token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static string GetTokenForUser(TokenHelper token)
        {
            if (TokenForUser == null)
            {
                var redirectUri = new Uri("https://localhost");
                AuthenticationContext authenticationContext = new AuthenticationContext(
                    string.Format(Constants.AAD.AuthString, token.TenantId),
                    false);
                AuthenticationResult userAuthnResult = authenticationContext.AcquireToken(Constants.AAD.ResourceUrl,
                    token.ClientIdForUserAuthn, redirectUri, PromptBehavior.Always);
                TokenForUser = userAuthnResult.AccessToken;
                Console.WriteLine("\n Welcome " + userAuthnResult.UserInfo.GivenName + " " +
                                  userAuthnResult.UserInfo.FamilyName);
            }
            return TokenForUser;
        }

        /// <summary>
        /// Get Active Directory Client for User.
        /// </summary>
        /// <returns>ActiveDirectoryClient for User.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClientAsUser(TokenHelper token)
        {
            Uri servicePointUri = new Uri(Constants.AAD.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, token.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => await AcquireTokenAsyncForUser(token));
            return activeDirectoryClient;
        }
    }
}
