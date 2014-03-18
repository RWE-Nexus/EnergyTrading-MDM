Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop";

$environment = $env:NEXUS_DEPLOYMENT_ENV
Write-Host "Environment: $environment"

Import-Module .\Invoke-RemoteCommand.psm1
Import-Module .\Get-Artifacts.psm1
Import-Module .\Rename-Config.psm1
Import-Module .\Update-Config.psm1
Import-Module .\Copy-Files.psm1
Import-Module .\Install-Service.psm1
Import-Module .\Install-WebApp.psm1
Import-Module .\Create-Bus-Performance-Counters.psm1
Import-Module .\Exit-WithCode.psm1

$project = "$environment.Nexus.MDM.Service"
$artifact = "MDMService.zip"
$webSite = "Default Web Site"
$webApp = ($environment + "_" + "MDMService")
$appPool = "$environment.Nexus.MDMService"

$version = $env:BUILD_NUMBER
Write-Host "Version: $version"

$destination = "D:\webapps\$project"
$stagingLocation = "$destination\staging"
$appLocation = "$destination\releases\$version"

$serviceUserName = $env:NEXUS_SERVICE_USERNAME
$servicePassword = $env:NEXUS_SERVICE_PWD

#
# Customisations for MDM separate at the moment - until we have a full picture of which customisations are required and
# can introduct them to the base scripts all at once.
#
function Install-MdmWebApp($session, $website, $webapp, $appPool, $appLocation, $username, $password, $keepAppPoolAlive)
{
  Write-Host "Configuring IIS"
  
  Write-Host "Web site: $webSite"
  Write-Host "Web app: $webApp"
  Write-Host "App pool: $appPool"
  Write-Host "App location: $appLocation"
  
  run "Import-Module WebAdministration" $session

  Uninstall-WebApp $website $webapp $appPool $session

  $runtimeVersion = "v4.0"
  $enable32BitAppOnWin64 = $True
  Create-AppPool $appPool $session $runtimeVersion $enable32BitAppOnWin64 $username $password $keepAppPoolAlive
  
  # MDM Custom
  Write-Host "Setting Application Pool to run as Network Service identity"
  run "Set-ItemProperty -Path IIS:\AppPools\$appPool -Name processmodel.identityType -Value 2" $session
  # End MDM Custom
  
  Write-Host "Creating web application"
  run "New-WebApplication -Name $webApp -Site `"$webSite`" -PhysicalPath $appLocation -ApplicationPool $appPool | Out-Null" $session
  
  Write-Host "Starting app pool"
  run "Start-WebAppPool $appPool" $session
}

foreach ($computerName in $env:NEXUS_DEPLOYMENT_SERVERS.split(",")) {
	try {
		Write-Host "Deploying to:" $computerName
		$session = New-PSSession -ComputerName $computerName

        Get-Artifacts2 $stagingLocation $artifact $computerName $session
		Rename-WebConfig $stagingLocation $environment $session
		Copy-Files $stagingLocation $appLocation $session
		Create-Bus-Performance-Counters $env:NEXUS_BUS_PERF_CNTR_CATEGORY $session
		Install-MdmWebApp $session $website $webapp $appPool $appLocation $serviceUserName $servicePassword $True
		Remove-OldVersions $appLocation 3 $session
	}
	catch {
	  Write-Host "##teamcity[message text='POWERSHELL_SCRIPT_FAILED' errorDetails='Deployment to $computerName failed' status='ERROR']"
	  $_.Exception
	  ExitWithCode 1
	}
	finally {
	  Remove-PSSession -Session $session
	}
}
