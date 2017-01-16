[CmdletBinding()]
Param(
	[validateSet("Release", "Debug")]
	[string]$Configuration = "Release"
)

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
    nuget pack $projFile -properties "Configuration=$Configuration" -outputdirectory $outputDir -verbosity detailed
}

if( !( Test-Path -Path "$outputDir/*" ) )
{
    throw "No NuGet Files found following build and pack"
}

if( ![string]::IsNullOrWhiteSpace( $(NuGetApiKey) ) )
{
    nuget.exe push "$outputDir/*.nupkg" -Source https://www.nuget.org/api/v2/package -ApiKey $(NuGetApiKey)
}
else 
{
    Write-Information "No ApiKey provided for NuGet Push"    
}