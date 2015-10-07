# azure
This repository contains a collection of azure related bits and pieces

## Contents
* Powershell
	* Module AzureUnattended.psm1 contains functions Start-AzureUnattendedVM and Stop-AzureUnattendedVM and is meant to start and stop Azure virtual machines without any input from the user. E.g., it could be used inside of a desktop shortcut or a scheduled task.
* Azure.Management.WebsiteRestart
	* Command line program that queries an Azure subscription for all web sites available and restarts them.
	