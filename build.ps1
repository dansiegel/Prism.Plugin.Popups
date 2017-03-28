[CmdletBinding()]
Param(
	[validateSet("Release", "Debug")]
	[string]$Configuration = "Release"
)

Write-Host "Starting $Configuration Build"

$outputDir = "./build"
$solutionFile = "Prism.Plugin.Popups.sln"
$projects = "Prism.Plugin.Popups.Autofac","Prism.Plugin.Popups.DryIoc","Prism.Plugin.Popups.Ninject","Prism.Plugin.Popups.Unity"

if(!(Test-Path -Path $outputDir )){
    New-Item -ItemType directory -Path $outputDir
}

nuget restore $solutionFile

msbuild $solutionFile /t:Clean

msbuild $solutionFile /p:"Configuration=$Configuration"

foreach( $projectName in $projects )
{
    $projFile = "./src/$projectName/$projectName.csproj"
    nuget pack $projFile -properties "Configuration=$Configuration" -outputdirectory $outputDir
}