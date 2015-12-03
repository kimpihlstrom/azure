using Microsoft.Azure.ActiveDirectory.GraphClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Azure.AAD.Tester.Helpers
{
    public class AADHelper
    {
        private ActiveDirectoryClient client;
        private Helpers.TokenHelper token;

        public AADHelper(string TenantId, string ClientId, string ClientSecret, string ClientIdForUserAuthn)
        {
            token = new Helpers.TokenHelper(TenantId, ClientId, ClientSecret, ClientIdForUserAuthn);

            //*********************************************************************
            // setup Active Directory Client
            //*********************************************************************
            try
            {
                client = Helpers.AuthenticationHelper.GetActiveDirectoryClientAsApplication(token);
            }
            catch (AuthenticationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (ex.InnerException != null)
                {
                    //You should implement retry and back-off logic per the guidance given here:http://msdn.microsoft.com/en-us/library/dn168916.aspx
                    //InnerException Message will contain the HTTP error status codes mentioned in the link above
                    Console.WriteLine("Error detail: {0}", ex.InnerException.Message);
                }
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        internal IUser Update(IUser user)
        {
            if (client == null)
            {
                return null;
            }

            user.UpdateAsync().Wait();

            return user;
        }

        internal IUser Create(IUser user)
        {
            if (client == null)
            {
                return null;
            }

            client.Users.AddUserAsync(user).Wait();

            return user;
        }

        internal List<IUser> Get(string UserPrincipalName)
        {
            if (client == null)
            {
                return null;
            }

            // search for a single user by UPN
            User retrievedUser = new User();
            List<IUser> retrievedUsers = null;
            try
            {
                retrievedUsers = client.Users
                    .Where(user => user.UserPrincipalName.Equals(UserPrincipalName))
                    .ExecuteAsync().Result.CurrentPage.ToList();

                return retrievedUsers;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
