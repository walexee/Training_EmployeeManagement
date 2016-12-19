function Find-PackagePath {
    [CmdletBinding()]
    param ( 
        [Parameter(Position=0, Mandatory=1)]$packagesPath,
        [Parameter(Position=1, Mandatory=1)]$packageName
    )

    return (Get-ChildItem($packagesPath + "\" + $packageName + "*")).FullName | Sort-Object $_ | select -Last 1
}

function Prepare-Tests {
	[CmdletBinding()]	param(		[Parameter(Position=0,Mandatory=1)]$testRunnerName,		[Parameter(Position=1,Mandatory=1)]$publishedTestsDirectory,		[Parameter(Position=2,Mandatory=1)]$testResultsDirectory	)

	$projects = Get-ChildItem $publishedTestsDirectory

	if ($projects.Count -eq 1) {
		Write-Host "1 $testRunnerName project has been found:"
	}
	else {
		Write-Host $projects.Count " $testRunnerName projects have been found:"
	}
	
	Write-Host ($projects | Select $_.Name )

	# Create the test results directory if needed	if (!(Test-Path $testResultsDirectory)) {		Write-Host "Creating test results directory located at $testResultsDirectory"		mkdir $testResultsDirectory | Out-Null	}

	# Get the list of test DLLs	$testAssembliesPaths = $projects | ForEach-Object { $_.FullName + "\" + $_.Name + ".dll" }

	$testAssemblies = [string]::Join(" ", $testAssembliesPaths)

	return $testAssemblies
}