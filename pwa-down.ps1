Param(
    [Parameter(Mandatory=$false)]
    [Switch]$rmi
)
Write-Output "------------------------------------------------------"
Write-Output "Removing test env"
Write-Output "------------------------------------------------------"
if ($rmi) {
docker-compose -f ./compose/pwa/docker-compose.pwa.yaml -f ./compose/pwa/docker-compose.pwa.test.yaml down --rmi local  -v
}
else {
docker-compose -f ./compose/pwa/docker-compose.pwa.yaml -f ./compose/pwa/docker-compose.pwa.test.yaml down -v
}
Write-Output "------------------------------------------------------"
Write-Output "Removing dev env"
Write-Output "------------------------------------------------------"
if ($rmi) {
docker-compose -f ./compose/pwa/docker-compose.pwa.yaml -f ./compose/pwa/docker-compose.pwa.dev.yaml down --rmi local  -v
}
else {
docker-compose -f ./compose/pwa/docker-compose.pwa.yaml -f ./compose/pwa/docker-compose.pwa.dev.yaml down -v
}