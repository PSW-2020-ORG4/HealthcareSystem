Param(
    [Parameter(Mandatory=$false)]
    [Switch]$noDbBuild,
    [Parameter(Mandatory=$false)]
    [Switch]$noServiceBuild
)
Write-Output "---------------------------------------------------------------------------"
Write-Output "WARNING: SLN SHOULD NOT BE OPEN IN VISUAL STUDIO WHILE THIS EXECUTES"

if($noServiceBuild -eq $false) {
Write-Output "---------------------------------------------------------------------------"
Write-Output "Building solution"
Write-Output "---------------------------------------------------------------------------"
dotnet publish ./HealthcareSystem/HealthcareSystem.sln -c Release
}

Write-Output "---------------------------------------------------------------------------"
Write-Output "Removing existing services"
Write-Output "---------------------------------------------------------------------------"
docker-compose -f ./HealthcareSystem/docker-compose.pwa.yaml down -v

if($noDbBuild -eq $false) {
Write-Output "---------------------------------------------------------------------------"
Write-Output "Building database image"
Write-Output "---------------------------------------------------------------------------"
docker build ./HealthcareSystem -f ./HealthcareSystem/Dockerfile.postgre -t seeded-postgre
}

Write-Output "---------------------------------------------------------------------------"
Write-Output "Starting services (PatientWebApp will run at localhost:8181)"
Write-Output "---------------------------------------------------------------------------"
if($noServiceBuild) {
docker-compose -f ./HealthcareSystem/docker-compose.pwa.yaml up
}
else {
docker-compose -f ./HealthcareSystem/docker-compose.pwa.yaml up --build
}
