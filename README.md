# azure
This repository contains a collection of azure related bits and pieces

## Contents
* Powershell
	* Module AzureUnattended.psm1 contains functions Start-AzureUnattendedVM and Stop-AzureUnattendedVM and is meant to start and stop Azure virtual machines without any input from the user. E.g., it could be used inside of a desktop shortcut or a scheduled task.
	
## Get Started

### AzureUnattended.psm1

#### Import the module

First add credentials for a valid administrator at the beginning of AzureUnattended.psm1 file
```powershell
Import-Module "<PSM1_PATH>\AzureUnattended.psm1"
```
where ```<PSM1_PATH>``` is the location of the .psm1 file, e.g. ```%HOMEPATH%\Dropbox\git\azure\powershell\AzureUnattended.psm1```

#### Start-AzureUnattendedVM

```powershell
# Start-AzureUnattendedVM -Name <VM_NAME>  -ServiceName <SERVICE_NAME>
```
where ```<VM_NAME>``` is the name of the VM, and ```<SERVICE_NAME>``` is the name of the cloud service containing the VM, 

#### Stop-AzureUnattendedVM

```powershell
# Stop-AzureUnattendedVM -Name <VM_NAME>  -ServiceName <SERVICE_NAME>
```

#### Desktop shortcut

##### Start VM

Create a new shortcut and write as the target:
```C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe -NonInteractive -ExecutionPolicy Bypass -Command "Import-Module <PSM1_PATH>\AzureUnattended.psm1; Start-AzureUnattendedVM -Name <VM_NAME> -ServiceName <SERVICE_NAME>;"```

##### Stop VM
Create a new shortcut and write as the target:
```C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe -NonInteractive -ExecutionPolicy Bypass -Command "Import-Module <PSM1_PATH>\AzureUnattended.psm1; Stop-AzureUnattendedVM -Name <VM_NAME> -ServiceName <SERVICE_NAME>;"```