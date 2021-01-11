Write-Output "---------------------------------------------------------------------------"
Write-Output "Building solution"
Write-Output "---------------------------------------------------------------------------"
dotnet publish ./HealthcareSystem/HealthcareSystem.sln -c Release

Write-Output "---------------------------------------------------------------------------"
Write-Output "Removing existing services"
Write-Output "---------------------------------------------------------------------------"
docker-compose -f ./compose/docker-compose.localdep.yaml down -v


Write-Output "---------------------------------------------------------------------------"
Write-Output "Starting services"
Write-Output "---------------------------------------------------------------------------"
docker-compose -f ./compose/docker-compose.localdep.yaml up --build

