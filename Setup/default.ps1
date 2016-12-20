Include ".\helpers.ps1"

properties {
	$testMessage = 'Executed Clean!'
	$cleanMessage = 'Executed Clean!'

	$buildServer = 'teamcity'
	$testCategory = 'Unit'
	$packageType = 'zip'

	$solutionDirectory = (Get-Item $solutionFile).DirectoryName
	$outputDirectory = "$solutionDirectory\.build"
	$temporaryOutputDirectory = "$outputDirectory\temp"
	$newDirectoryList = @($outputDirectory, $temporaryOutputDirectory)

	$publishedNUnitTestsDirectory = "$temporaryOutputDirectory\_PublishedNUnitTests"
	$publishedxUnitTestsDirectory = "$temporaryOutputDirectory\_PublishedxUnitTests"
	$publishedMSTestTestsDirectory = "$temporaryOutputDirectory\_PublishedMSTestTests"
	$publishedApplicationsDirectory = "$temporaryOutputDirectory\_PublishedApplications"
	$publishedWebApplicationsDirectory = "$temporaryOutputDirectory\_PublishedWebsites"

	$testResultsDirectory = "$outputDirectory\TestResults"
	$NUnitTestResultsDirectory = "$testResultsDirectory\NUnit"
	$xUnitTestResultsDirectory = "$testResultsDirectory\xUnit"
	$MSTestTestResultsDirectory = "$testResultsDirectory\MSTest"
	

	$testCoverageDirectory = "$outputDirectory\TestCoverage"
	$testCoverageReportPath = "$testCoverageDirectory\OpenCover.xml"
	$testCoverageFilter = "+[*]* -[xunit.*]* -[*.NUnitTests]* -[*.Tests]* -[*.xUnitTests]*"
	$testCoverageExcludeByAttribute = "*.ExcludeFromCodeCoverage*"
	$testCoverageExcludeByFile = "*\*Designer.cs; *\*.g.cs; *\*.g.i.cs"
	$packagesOutputDirectory = "$outputDirectory\Packages"
	$applicationsOutputDirectory = "$packagesOutputDirectory\Applications"

	$buildConfiguration = "Release"
	$buildPlatform = "Any CPU"

	$packagesPath = "$solutionDirectory\packages"
	$NUnitExe = (Find-PackagePath $packagesPath "NUnit.ConsoleRunner" ) + "\Tools\nunit3-console.exe"
	$xUnitExe = (Find-PackagePath $packagesPath "xUnit.Runner.Console" ) + "\Tools\xunit.console.exe"
	$vsTestExe = (Get-ChildItem ("C:\Program Files (x86)\Microsoft Visual Studio*\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe")).FullName | Sort-Object $_ | select -last 1
	$openCoverExe = (Find-PackagePath $packagesPath "OpenCover") + "\Tools\OpenCover.Console.exe"
	$reportGeneratorExe = (Find-PackagePath $packagesPath "ReportGenerator") + "\Tools\ReportGenerator.exe"
	$7ZipExe = (Find-PackagePath $packagesPath "7-Zip.CommandLine") + "\Tools\7za.exe"
	$nugetExe = (Find-PackagePath $packagesPath "NuGet.CommandLine") + "\Tools\NuGet.exe"
}

FormatTaskName "`r`n`r`n--------Executing {0} Task ---------"

task default -depends Test

task Init -description "Initialises the build by removing previous artifacts and creating output directories" -requiredVariables $newDirectoryList {
	Assert -conditionToCheck ("Debug", "Release" -contains $buildConfiguration) -failureMessage "Invalid build configuration '$buildConfiguration'. Valid values are 'Debug' or 'Release'"
	Assert -conditionToCheck ("x64", "x86", "Any CPU" -contains $buildPlatform) -failureMessage "Invalid build platform '$buildPlatform'. Valid values are 'x64' , 'x86' or 'Any CPU'"

	# Check that all tools are available
	Write-Host "Checking that all required tools are available"
 
	Assert (Test-Path $NUnitExe) "NUnit Console could not be found"
	Assert (Test-Path $xUnitExe) "xUnit Console could not be found"
	Assert (Test-Path $vsTestExe) "VSTest Console could not be found"
	Assert (Test-Path $openCoverExe) "OpenCover Console could not be found"
	Assert (Test-Path $reportGeneratorExe) "ReportGenerator Console could not be found"
	Assert (Test-Path $7ZipExe) "7-Zip Command Line could not be found"
	Assert (Test-Path $nugetExe) "NuGet Command Line could not be found"

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

task TestNUnit `
	-depends Compile `
	-description "Run NUnit tests" `
	-precondition { return Test-Path $publishedNUnitTestsDirectory } `
	-requiredVariable publishedNUnitTestsDirectory, NUnitTestResultsDirectory ` {
	$testAssemblies = Prepare-Tests -testRunnerName "NUnit" `
									-publishedTestsDirectory $publishedNUnitTestsDirectory `
									-testResultsDirectory $NUnitTestResultsDirectory `
									-testCoverageDirectory $testCoverageDirectory

	$targetArgs = "$testAssemblies /xml:`"`"$NUnitTestResultsDirectory\NUnit.xml`"`" /nologo /noshadow"

	# Run OpenCover, which in turn will run NUnit	
	Run-Tests -openCoverExe $openCoverExe `
			  -targetExe $nunitExe `
			  -targetArgs $targetArgs `
			  -coveragePath $testCoverageReportPath `
			  -filter $testCoverageFilter `
			  -excludebyattribute:$testCoverageExcludeByAttribute `
			  -excludebyfile: $testCoverageExcludeByFile
}

task TestXUnit `
	-depends Compile `
	-description "Run xUnit tests" `
	-precondition { return Test-Path $publishedxUnitTestsDirectory } `
	-requiredVariable publishedxUnitTestsDirectory, xUnitTestResultsDirectory ` {
	$testAssemblies = Prepare-Tests -testRunnerName "xUnit" `
									-publishedTestsDirectory $publishedxUnitTestsDirectory `
									-testResultsDirectory $xUnitTestResultsDirectory `
									-testCoverageDirectory $testCoverageDirectory

	$targetArgs = "$testAssemblies -xml `"`"$xUnitTestResultsDirectory\xUnit.xml`"`" -nologo -noshadow"

	# Run OpenCover, which in turn will run xUnit	
	Run-Tests -openCoverExe $openCoverExe `
			  -targetExe $xunitExe `
			  -targetArgs $targetArgs `
			  -coveragePath $testCoverageReportPath `
			  -filter $testCoverageFilter `
			  -excludebyattribute:$testCoverageExcludeByAttribute `
			  -excludebyfile: $testCoverageExcludeByFile
}

task TestMSTest `
	-depends Compile `
	-description "Run MSTest tests" `
	-precondition { return Test-Path $publishedMSTestTestsDirectory } `
	-requiredVariable publishedMSTestTestsDirectory, MSTestTestResultsDirectory `
{
	$testAssemblies = Prepare-Tests -testRunnerName "MSTest" `
									-publishedTestsDirectory $publishedMSTestTestsDirectory `
									-testResultsDirectory $MSTestTestResultsDirectory `
									-testCoverageDirectory $testCoverageDirectory


	$targetArgs = "$testAssemblies /Logger:trx"
	
	if ($testCategory) {
		$targetArgs += " /TestCaseFilter:TestCategory=UnitTest"
	}

	# vstest console doesn't have any option to change the output directory
	# so we need to change the working directory
	Push-Location $MSTestTestResultsDirectory
	
	# Run OpenCover, which in turn will run VSTest	
	Run-Tests -openCoverExe $openCoverExe `
			  -targetExe $vsTestExe `
			  -targetArgs $targetArgs `
			  -coveragePath $testCoverageReportPath `
			  -filter $testCoverageFilter `
			  -excludebyattribute:$testCoverageExcludeByAttribute `
			  -excludeByFile: $testCoverageExcludeByFile

	Pop-Location

	# move the .trx file back to $MSTestTestResultsDirectory
	Move-Item -path $MSTestTestResultsDirectory\TestResults\*.trx -destination $MSTestTestResultsDirectory\MSTest.trx

	Remove-Item $MSTestTestResultsDirectory\TestResults
}

task Package `
	-description "Packages all the applications" `
	-depends Compile, Test `
	-requiredVariables publishedApplicationsDirectory, publishedWebApplicationsDirectory, applicationsOutputDirectory {
	$applications = @()

	if(Test-Path $publishedApplicationsDirectory) {
		$applications += @(Get-ChildItem $publishedApplicationsDirectory)
	}

	if(Test-Path $publishedWebApplicationsDirectory) {
		$applications += @(Get-ChildItem $publishedWebApplicationsDirectory)
	}

	if ($applications.Length -gt 0 -and !(Test-Path $applicationsOutputDirectory)) {
		New-Item $applicationsOutputDirectory -ItemType Directory | Out-Null
	}

	foreach ($application in $applications) {
		if ($packageType -eq "zip") {
			Write-Host "Packaging $($application.Name) as zip file"

			$archivePath = "$applicationsOutputDirectory\$($application.Name).zip"
			$inputDirectory = "$($application.FullName)\*"
			Exec { &$7ZipExe a -r -mx3 $archivePath $inputDirectory }
		}

		if ($packageType -eq "nuget") {
			$projectFile = (Get-ChildItem($solutionDirectory + "\*\" + $applications.Name + ".csproj")).FullName | select -First 1
			Write-Host "File Here: $projectFile"

			if($projectFile) {
				Write-Host "Packaging $($application.Name) as NuGet package"

				Exec { &$nugetExe pack "$projectFile" -Build -Properties Configuration=$buildConfiguration -IncludeReferencedProjects -OutputDirectory $applicationsOutputDirectory }
			}
		}
	}
}


task Test `
	-depends Compile, TestNUnit, TestXUnit, TestMSTest `
	-description "Run unit tests and test coverage" `
	-requiredVariables testCoverageDirectory, testCoverageReportPath ` {
	# parse OpenCover results to extract summary
	if (Test-Path $testCoverageReportPath) {
		#publish coverage data to teamcity
		if ($buildServer -eq "teamcity") {
			Write-Host "Parsing OpenCover results"

			# Load the coverage report as XML
			$coverage = [xml](Get-Content -Path $testCoverageReportPath)

			$coverageSummary = $coverage.CoverageSession.Summary

			# Write class coverage
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsCCovered' value='$($coverageSummary.visitedClasses)']"
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsCTotal' value='$($coverageSummary.numClasses)']"
			Write-Host ("##teamcity[buildStatisticValue key='CodeCoverageC' value='{0:N2}']" -f (($coverageSummary.visitedClasses / $coverageSummary.numClasses)*100))

			# Report method coverage
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsMCovered' value='$($coverageSummary.visitedMethods)']"
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsMTotal' value='$($coverageSummary.numMethods)']"
			Write-Host ("##teamcity[buildStatisticValue key='CodeCoverageM' value='{0:N2}']" -f (($coverageSummary.visitedMethods / $coverageSummary.numMethods)*100))
		
			# Report branch coverage
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsBCovered' value='$($coverageSummary.visitedBranchPoints)']"
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsBTotal' value='$($coverageSummary.numBranchPoints)']"
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageB' value='$($coverageSummary.branchCoverage)']"

			# Report statement coverage
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsSCovered' value='$($coverageSummary.visitedSequencePoints)']"
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageAbsSTotal' value='$($coverageSummary.numSequencePoints)']"
			Write-Host "##teamcity[buildStatisticValue key='CodeCoverageS' value='$($coverageSummary.sequenceCoverage)']"
		}

		# Generate HTML test coverage report
		Write-Host "`r`nGenerating HTML test coverage report"
		Exec { &$reportGeneratorExe $testCoverageReportPath $testCoverageDirectory }
	}
	else {
		Write-Host "No coverage file found at: $testCoverageReportPath"
	}
}