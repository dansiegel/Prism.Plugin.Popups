$location = Get-Location
$currentDirectory = $location.Path

Write-Host "Currect working directory: $currentDirectory"

$nupkg = Get-ChildItem -Path $currentDirectory -Filter *.nupkg -Recurse | Select-Object -First 1

if($nupkg -eq $null)
{
    Throw "No NuGet Package could be found in the current directory"
}

Write-Host "Package Name: $($nupkg.Name)"
$nupkg.Name -match '^(.*?)\.((?:\.?[0-9]+){3,}(?:[-a-z]+)?)\.nupkg$'

$VersionName = $Matches[2]
$IsPreview = $VersionName -match '-pre$'
$DeployToNuGet = !($VersionName -match '-ci$')
$ReleaseDisplayName = $VersionName

if($IsPreview -eq $true)
{
    $ReleaseDisplayName = "$VersionName - Preview"
}

Write-Host "Version Name" $VersionName
Write-Host "IsPreview $IsPreview"
Write-Host "Deploy to NuGet: $DeployToNuGet"
Write-Host "Release Display Name: $ReleaseDisplayName"

Write-Output ("##vso[task.setvariable variable=DeployToNuGet;]$DeployToNuGet")
Write-Output ("##vso[task.setvariable variable=VersionName;]$VersionName")
Write-Output ("##vso[task.setvariable variable=IsPreview;]$IsPreview")
Write-Output ("##vso[task.setvariable variable=ReleaseDisplayName;]$ReleaseDisplayName")