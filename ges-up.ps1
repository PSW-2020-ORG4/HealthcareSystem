Param(
    [Parameter(Mandatory=$false)]
    [Switch]$noDbBuild,
    [Parameter(Mandatory=$false)]
    [Switch]$noServiceBuild,
    [Parameter(Mandatory=$false)]
    [Switch]$dev
)
if ($dev) {
$efile = "./compose/ges/docker-compose.ges.dev.yaml"
}
else {
$efile = "./compose/ges/docker-compose.ges.test.yaml"
}
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
docker-compose -f ./compose/ges/docker-compose.ges.yaml -f $efile down -v

if($noDbBuild -eq $false) {
Write-Output "---------------------------------------------------------------------------"
Write-Output "Building database image"
Write-Output "---------------------------------------------------------------------------"
if ($dev -eq $false) {
docker build ./HealthcareSystem/Backend -f ./HealthcareSystem/Backend/Dockerfile.postgre -t seeded-postgre
}
else {
docker build ./HealthcareSystem/Backend -f ./HealthcareSystem/Backend/Dockerfile.mysql -t seeded-mysql
}
}

Write-Output "---------------------------------------------------------------------------"
Write-Output "Starting services (GraphicalEditorServer will run at localhost:5001)"
Write-Output "---------------------------------------------------------------------------"
if($noServiceBuild) {
docker-compose -f ./compose/ges/docker-compose.ges.yaml -f $efile up
}
else {
docker-compose -f ./compose/ges/docker-compose.ges.yaml -f $efile up --build
}
