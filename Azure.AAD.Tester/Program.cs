using Azure.AAD.Tester.Helpers;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System.Linq;

namespace Azure.AAD.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            AADHelper aad = new AADHelper(
                Constants.AAD.TenantId,
                Constants.AAD.ClientId,
                Constants.AAD.ClientSecret,
                Constants.AAD.ClientIdForUserAuthn);

            #region Try to fetch the user. NULL is returned if the object doesn't exist
            IUser user = aad.Get(Constants.AAD.UPN).FirstOrDefault();
            #endregion

            if (user == null)
                #region Create a user with some dummy values
                aad.Create(new User()
                {
                    DisplayName = "test user",
                    GivenName = "test",
                    Surname = "user",
                    TelephoneNumber = "+3581234567",
                    UserPrincipalName = Constants.AAD.UPN,
                    AccountEnabled = true,
                    UsageLocation = "FI",
                    MailNickname = "test",
                    PasswordProfile = new PasswordProfile
                    {
                        Password = "PlaceHolder123!",
                        ForceChangePasswordNextLogin = true
                    }
                });
#endregion
            else
            {
                #region Give the dummy a new password
                user.PasswordProfile = new PasswordProfile
                {
                    Password = "Qwerty12345!",
                    ForceChangePasswordNextLogin = true
                };

                aad.Update(user);
                #endregion
            }
        }
    }
}
