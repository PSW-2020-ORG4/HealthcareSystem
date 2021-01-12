$compose = "./compose/local-deployment/docker-compose.localdep.yaml"
$isaCompose = "./compose/local-deployment/docker-compose.localdep.isa.yaml"
$pswCompose = "./compose/local-deployment/docker-compose.localdep.psw.yaml"

Write-Output "---------------------------------------------------------------------------"
Write-Output "Building solution"
Write-Output "---------------------------------------------------------------------------"
dotnet publish ./HealthcareSystem/HealthcareSystem.sln -c Release

Write-Output "---------------------------------------------------------------------------"
Write-Output "Removing existing services"
Write-Output "---------------------------------------------------------------------------"
docker-compose -f $compose -f $isaCompose -f $pswCompose down -v


Write-Output "---------------------------------------------------------------------------"
Write-Output "Starting services"
Write-Output "---------------------------------------------------------------------------"
docker-compose -f $compose -f $isaCompose -f $pswCompose up --build

