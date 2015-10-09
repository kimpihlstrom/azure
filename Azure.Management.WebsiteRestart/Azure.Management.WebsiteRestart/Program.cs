using System;
using System.Linq;
using Microsoft.WindowsAzure.Management.WebSites;
using Microsoft.WindowsAzure;
using System.Security.Cryptography.X509Certificates;
using Microsoft.WindowsAzure.Management.WebSites.Models;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;

namespace Gilgamesh.Azure.Management.iisreset
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var subscriptionId = "[SUBSCRIPTION ID]";

            var cred = new CertificateCloudCredentials(subscriptionId, GetCertificate());

            RestartAllWebApps(cred);

            if(System.Diagnostics.Debugger.IsAttached)
            { 
                Console.WriteLine("Press anykey to exit");
                Console.Read();
            }
        }

        private static void RestartAllWebApps(CertificateCloudCredentials cred)
        {
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
        }

        private static X509Certificate2 GetCertificate()
        {
            string certPath = Environment.CurrentDirectory + "\\" + "[PFX CERTIFICATE FILE NAME]";

            var x509Cert = new X509Certificate2(certPath,"[PFX CERTIFICATE PASSWORD");

            return x509Cert;
        }
    }
}
