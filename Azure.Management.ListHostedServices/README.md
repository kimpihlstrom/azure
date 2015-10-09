# Azure.Management.ListHostedServices
This is a command line helper program for iterating through all cloud services, and the virtual machines they contain, using the Azure Management Library

## Prerequirements
* Certificate based login. Follow the instructions outlined here https://blogs.endjin.com/2015/02/generating-and-using-a-certificate-to-authorise-azure-automation/ or simply just run the following command in a prompt:
```
makecert.exe -sky exchange -r -n "CN=change-this-value" -pe -a sha256 -len 2048 -ss My "certificate.cer"
```
followed by the following PowerShell cmdlets:
```powershell
$MyPwd = ConvertTo-SecureString -String "Change_the_password!" -Force -AsPlainText
$AzureCert = Get-ChildItem -Path Cert:\CurrentUser\My | where {$_.Subject -match "change-this-value"}
Export-PfxCertificate -FilePath C:\certificate.pfx -Password $MyPwd -Cert $AzureCert

```
* Next upload the .cer file to Azure and edit the command line program so it can find the .pfx file. See https://azure.microsoft.com/en-us/blog/getting-started-with-the-azure-java-management-libraries/ for more details.
* Obtain the the subscription id from your Azure account. See e.g. http://blogs.msdn.com/b/mschray/archive/2015/05/13/getting-your-azure-guid-subscription-id.aspx for details.

## Sample output
```
Processing hosted service <Cloud Service 1>
 - Name: <Name>
 - Deployment slot: Production
 - Created: 7.10.2015 11.24.00
 - Status: Running
 - URI: http://<domain>.cloudapp.net/
 - External IP: <ip>
 - Listing roles
 - - Name: <name>
 - - Size: ExtraSmall
 - Listing role instaces
 - - Name: <name>
 - - Hostname: <name>
 - - Status: ReadyRole
 - - IP: <ip>
 - - Status: ReadyRole
 - - Getting RDP file for instance
 - - - Error: BadRequest: An external endpoint to the Remote Desktop port (3389) must first be added to the role.
Processing hosted service <Cloud Service 2>
 - Name: 6f13d065e8704dbb80913e59480f36e0
 - Deployment slot: Production
 - Created: 3.10.2015 21.02.05
 - Status: Running
 - URI: http://<domain>.cloudapp.net/
 - External IP: <ip>
 - Listing roles
 - - Name: Orchard.Azure.Web
 - - Size:
 - Listing role instaces
 - - Name: Orchard.Azure.Web_IN_0
 - - Hostname:
 - - Status: ReadyRole
 - - IP: <ip>
 - - Status: ReadyRole
 - - Getting RDP file for instance
 - - - Status: OK
 - - - File size: 137
Press anykey to exit
```

## References
This code is based around work presented in this blog posting http://www.bradygaster.com/post/managing-web-sites-from-web-sites-using-the-windows-azure-management-libraries-for-net