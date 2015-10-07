# Azure.Management.WebsiteRestart
This is a command line helper program for iterating through all web sites in an Azure subscription and restarting all web sites available.

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

## References
This code is based around work presented in this blog posting http://www.bradygaster.com/post/managing-web-sites-from-web-sites-using-the-windows-azure-management-libraries-for-net