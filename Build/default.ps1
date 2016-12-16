properties {
	$testMessage = 'Executed Clean!'
	$cleanMessage = 'Executed Clean!'

	$solutionDirectory = (Get-Item $solutionFile).DirectoryName
	$outputDirectory = "$solutionDirectory\.build"
	$temporaryOutputDirectory = "$outputDirectory\temp"
}

task default -depends Test

task Init -description "Initialises the build by removing previous artifacts and creating output directories" {
	Write-Host "Creating output directory located at $outputDirectory"
	New-Item $outputDirectory -ItemType Directory | Out-Null

	Write-Host "Creating temporary directory located at $temporaryDirectory"
	New-Item $temporaryDirectory -ItemType Directory | Out-Null
}

task Clean -description "Remove temporary files" {
	Write-Host $cleanMessage
}

task Compile -depends Init -description "Compile the code" {
	Write-Host 'Executed Compile!'
}

task Test -depends Compile, Clean {
	Write-Host $testMessage
}