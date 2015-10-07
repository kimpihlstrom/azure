using System;
using System.Linq;
using Microsoft.WindowsAzure.Management.WebSites;
using Microsoft.WindowsAzure;
using System.Security.Cryptography.X509Certificates;
using Microsoft.WindowsAzure.Management.WebSites.Models;

namespace Gilgamesh.Azure.Management.iisreset
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionId = "[INSERT_SUBSCRIPTION_ID]";

            var cred = new CertificateCloudCredentials(subscriptionId, GetCertificate());

            var client = new WebSiteManagementClient(cred);

            WebSpacesListResponse webspaces = client.WebSpaces.List();

            webspaces.Select(p =>
            {
                Console.WriteLine("Processing webspace {0}", p.Name);

                WebSpacesListWebSitesResponse websitesInWebspace = client.WebSpaces.ListWebSites(p.Name,
                                new WebSiteListParameters()
                                {
                                });

                websitesInWebspace.Select(o =>
                {
                    Console.Write(" - Restarting {0} ... ", o.Name);

                    OperationResponse operation = client.WebSites.Restart(p.Name, o.Name);

                    Console.WriteLine(operation.StatusCode.ToString());

                    return o;
                }).ToArray();

                return p;
            }).ToArray();

            if(System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press anykey to exit");
                Console.Read();
            }
        }

        private static X509Certificate2 GetCertificate()
        {
            string certPath = Environment.CurrentDirectory + "\\" + "[PFX_CERTIFICATE_FILENAME]";

            var x509Cert = new X509Certificate2(certPath,"[PFX_CERTIFICATE_PASSWORD]");

            return x509Cert;
        }
    }
}
