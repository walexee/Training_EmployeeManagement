properties {
	$testMessage = 'Executed Clean!'
	$cleanMessage = 'Executed Clean!'

	$solutionDirectory = (Get-Item $solutionFile).DirectoryName
	$outputDirectory = "$solutionDirectory\.build"
	$temporaryOutputDirectory = "$outputDirectory\temp"
	$newDirectoryList = @($outputDirectory, $temporaryOutputDirectory)

	$buildConfiguration = "Release"
	$buildPlatform = "Any CPU"
}

FormatTaskName "`r`n`r`n--------Executing {0} Task ---------"

task default -depends Test

task Init -description "Initialises the build by removing previous artifacts and creating output directories" -requiredVariables $newDirectoryList {
	Assert -conditionToCheck ("Debug", "Release" -contains $buildConfiguration) -failureMessage "Invalid build configuration '$buildConfiguration'. Valid values are 'Debug' or 'Release'"
	Assert -conditionToCheck ("x64", "x86", "Any CPU" -contains $buildPlatform) -failureMessage "Invalid build platform '$buildPlatform'. Valid values are 'x64' , 'x86' or 'Any CPU'"

	foreach ($directory in $newDirectoryList) {
		if (Test-Path $directory) {
			Write-Host "Removing directory located at $directory"
			Remove-Item $directory -Force -Recurse
		}

		Write-Host "Creating directory located at $directory"
		New-Item $directory -ItemType Directory | Out-Null
	}
}

task Clean -description "Remove temporary files" {
	Write-Host $cleanMessage
}

task Compile -depends Init `
	-description "Compile the code" `
	-requiredVariables solutionFile, buildConfiguration, buildPlatform, temporaryOutputDirectory {
	Write-Host "Building solution $solutionFile"

	Exec {
		msbuild $solutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform;OutDir=$temporaryOutputDirectory"
	}
}

task Test -depends Compile, Clean {
	Write-Host $testMessage
}