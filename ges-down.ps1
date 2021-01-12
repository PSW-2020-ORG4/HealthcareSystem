Param(
    [Parameter(Mandatory=$false)]
    [Switch]$rmi
)
Write-Output "------------------------------------------------------"
Write-Output "Removing test env"
Write-Output "------------------------------------------------------"
if ($rmi) {
docker-compose -f ./compose/ges/docker-compose.ges.yaml -f ./compose/ges/docker-compose.ges.test.yaml down --rmi local  -v
}
else {
docker-compose -f ./compose/ges/docker-compose.ges.yaml -f ./compose/ges/docker-compose.ges.test.yaml down -v
}
Write-Output "------------------------------------------------------"
Write-Output "Removing dev env"
Write-Output "------------------------------------------------------"
if ($rmi) {
docker-compose -f ./compose/ges/docker-compose.ges.yaml -f ./compose/ges/docker-compose.ges.dev.yaml down --rmi local  -v
}
else {
docker-compose -f ./compose/ges/docker-compose.ges.yaml -f ./compose/ges/docker-compose.ges.dev.yaml down -v
}