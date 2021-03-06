﻿params (
	[Int32]$buildNumber=0
)

cls

# '[p]sake' is the same as 'psake' but $Error is not polluted
Remove-Module [p]sake

# find psake's path
$psakeModule = (Get-ChildItem (".\packages\psake*\tools\psake.psm1")).FullName | Sort-Object $_ | select -Last 1

Import-Module $psakeModule

Invoke-psake -buildFile .\Setup\default.ps1 -taskList Package `
	-framework 4.6.1 `
	-properties @{ 
		"buildServer" = "jenkins" #or teamcity
		"testCategory" = "UnitTest"
		"packageType" = "nuget" #or zip
		"buildConfiguration" = "Release" 
		"buildPlatform" = "Any CPU"} `
	-parameters @{ 
		"solutionFile" = "..\EmployeeManagement.sln"
		"buildNumber" = $buildNumber}

Write-Host "Build exit code:" $LASTEXITCODE

# Propagating the exit code so that builds actually fail when there is a problem
exit $LASTEXITCODE