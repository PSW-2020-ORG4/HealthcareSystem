Param(
    [Parameter(Mandatory=$false)]
    [Switch]$rmi
)
Write-Output "------------------------------------------------------"
Write-Output "Removing services"
Write-Output "------------------------------------------------------"
if ($rmi) {
docker-compose -f ./HealthcareSystem/docker-compose.pwa.yaml down --rmi local  -v
}
else {
docker-compose -f ./HealthcareSystem/docker-compose.pwa.yaml down -v
}