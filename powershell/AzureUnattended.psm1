#Location of the Azure Powershell SDK
$azureModulePath = "C:\Program Files (x86)\Microsoft SDKs\Azure\PowerShell\ServiceManagement\Azure\Azure.psd1"

#Credentials for the administrator account used for the unattended operations. Note: Must be a federated account, e.g. user hosted in Azure AD.
$userName = "<username>"
$password = "<password>"

function global:Start-AzureUnattendedVM {

	<#
		.SYNOPSIS
		This Cmdlet Starts an Azure virtual machine using predefined credentials

		.DESCRIPTION
		Using credentials specified in Variables.ps1 this Cmdlet will start a virtual machine

		.EXAMPLE
		Start-AzureUnattendedVM ExampleVM ExampleCloudService
		Starts the virtual machine named "ExampleVM" hosted in the Azure cloud service named "ExampleCloudService"

		.NOTES
		Credentials stored in Variables.ps1 can not be from a live account. They must be either business or school credentials.

		.LINK
		http://www.powershell.ca
	#>

	[CmdletBinding(SupportsShouldProcess=$True,ConfirmImpact='Medium')]

	PARAM(
		[parameter(Mandatory=$True,Position=0)]
		[STRING]$Name,

		[parameter(Mandatory=$True,Position=1)]
		[STRING]$ServiceName
	)

	Begin {
	    #. .\Variables.ps1
		if( !(Test-Path $azureModulePath)) {
			Write-Error "Azure Powershell SDK is missing! Exiting."
			exit
		}

		Import-Module $azureModulePath
	}

	Process {
		$securePassword = ConvertTo-SecureString -String $password -AsPlainText -Force
		$cred = New-Object System.Management.Automation.PSCredential($userName, $securePassword)

		Write-Debug "`$userName: $userName"
		Write-Debug "`$Name: $Name"
		Write-Debug "`$ServiceName: $ServiceName"

		Write-Verbose "Authenticating user"
		If ($PSCmdlet.ShouldProcess("Authenticating using credentials from Common.ps1")) {
			Add-AzureAccount -Credential $cred
		}

		Write-Verbose "Starting virtual machine"
		If ($PSCmdlet.ShouldProcess("Starting VM")) {
			Start-AzureVM $Name -ServiceName $ServiceName
		}
	}

	End {}
}


function global:Stop-AzureUnattendedVM {

	<#
		.SYNOPSIS
		This Cmdlet Stops an Azure virtual machine using predefined credentials

		.DESCRIPTION
		Using credentials specified in Variables.ps1 this Cmdlet will start a virtual machine

		.EXAMPLE
		Stop-AzureUnattendedVM ExampleVM ExampleCloudService
		Stops the virtual machine named "ExampleVM" hosted in the Azure cloud service named "ExampleCloudService"

		.NOTES
		Credentials stored in Variables.ps1 can not be from a live account. They must be either business or school credentials.

		.LINK
		http://www.powershell.ca
	#>

	[CmdletBinding(SupportsShouldProcess=$True,ConfirmImpact='Medium')]

	PARAM(
		[parameter(Mandatory=$True,Position=0)]
		[STRING]$Name,

		[parameter(Mandatory=$True,Position=1)]
		[STRING]$ServiceName
	)

	Begin {
	    #. .\Variables.ps1
		if( !(Test-Path $azureModulePath)) {
			Write-Error "Azure Powershell SDK is missing! Exiting."
			exit
		}

		Import-Module $azureModulePath
	}

	Process {
		$securePassword = ConvertTo-SecureString -String $password -AsPlainText -Force
		$cred = New-Object System.Management.Automation.PSCredential($userName, $securePassword)

		Write-Debug "`$userName: $userName"
		Write-Debug "`$Name: $Name"
		Write-Debug "`$ServiceName: $ServiceName"
		Write-Debug "Get-ScriptDirectory"

		Write-Verbose "Authenticating user"
		If ($PSCmdlet.ShouldProcess("Authenticating using credentials from Common.ps1")) {
			Add-AzureAccount -Credential $cred
		}

		Write-Verbose "Stopping virtual machine"
		If ($PSCmdlet.ShouldProcess("Stopping VM")) {
			Stop-AzureVM -Name $Name -ServiceName $ServiceName -Force
		}
	}

	End {}
}

function Get-ScriptDirectory
{
  $Invocation = (Get-Variable MyInvocation -Scope 1).Value
  write-host $Invocation.Value
  Split-Path $Invocation.MyCommand.Path
}