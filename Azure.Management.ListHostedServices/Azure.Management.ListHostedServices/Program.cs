﻿using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Azure.Management.ListHostedServices
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionId = "[SUBSCRIPTION ID]";

            var cred = new CertificateCloudCredentials(subscriptionId, GetCertificate());

            ListAllHostedServicesDetails(cred);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press anykey to exit");
                Console.Read();
            }
        }
        private static void ListAllHostedServicesDetails(CertificateCloudCredentials cred)
        {
            var client = new ComputeManagementClient(cred);

            HostedServiceListResponse hostedServices = client.HostedServices.List();

            hostedServices.Select(p =>
            {
                Console.WriteLine("Processing hosted service {0}", p.ServiceName);

                HostedServiceGetDetailedResponse details = client.HostedServices.GetDetailed(p.ServiceName);

                details.Deployments.Select(o =>
                {
                    #region List deployment slot infromation
                    Console.WriteLine(" - Name: {0}", o.Name);
                    Console.WriteLine(" - Deployment slot: {0}", o.DeploymentSlot);
                    Console.WriteLine(" - Created: {0}", o.CreatedTime);
                    Console.WriteLine(" - Status: {0}", o.Status);
                    Console.WriteLine(" - URI: {0}", o.Uri);
                    Console.WriteLine(" - External IP: {0}",
                        String.Join(", ", o.VirtualIPAddresses
                        .ToList()
                        .Select(ip => ip.Address)
                        .ToArray()));
                    #endregion

                    #region List role sepecific information
                    Console.WriteLine(" - Listing roles");
                    o.Roles.Select(r =>
                    {
                        Console.WriteLine(" - - Name: {0}", r.RoleName);
                        Console.WriteLine(" - - Size: {0}", r.RoleSize);
                        return r;
                    }).ToArray();
                    #endregion

                    #region List role instance details
                    Console.WriteLine(" - Listing role instaces");
                    o.RoleInstances.Select(r =>
                    {
                        Console.WriteLine(" - - Name: {0}", r.InstanceName);
                        Console.WriteLine(" - - Hostname: {0}", r.HostName);
                        Console.WriteLine(" - - Status: {0}", r.InstanceStatus);
                        Console.WriteLine(" - - IP: {0}", r.IPAddress);
                        Console.WriteLine(" - - Status: {0}", r.InstanceStatus);

                        #region Get the rdp file for the instance, if possible
                        Console.WriteLine(" - - Getting RDP file for instance");
                        try
                        {
                            VirtualMachineGetRemoteDesktopFileResponse rdpFile = client.VirtualMachines.GetRemoteDesktopFile(p.ServiceName, o.Name, r.InstanceName);

                            Console.WriteLine(" - - - Status: {0}", rdpFile.StatusCode.ToString());
                            Console.WriteLine(" - - - File size: {0}", rdpFile.RemoteDesktopFile.Length);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(" - - - Error: {0}{1}", e.Message, e.InnerException == null ? "" : " - " + e.InnerException.Message);
                        }
                        #endregion

                        return r;
                    }).ToArray();
                    #endregion

                    return o;
                }).ToArray();

                return p;
            }).ToArray();
        }

        private static X509Certificate2 GetCertificate()
        {
            string certPath = Environment.CurrentDirectory + "\\" + "[PFX CERTIFICATE FILENAME]";

            var x509Cert = new X509Certificate2(certPath, "[PFX CERTIFICATE PASSWORD]");

            return x509Cert;
        }
    }
}