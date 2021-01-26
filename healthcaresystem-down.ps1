$compose = "./compose/local-deployment/docker-compose.localdep.yaml"
$isaCompose = "./compose/local-deployment/docker-compose.localdep.isa.yaml"
$pswCompose = "./compose/local-deployment/docker-compose.localdep.psw.yaml"

docker-compose -f $compose -f $isaCompose -f $pswCompose down -v --rmi all