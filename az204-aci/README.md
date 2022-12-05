## Azure Container Instance

#### Criar o grupo de recurso
az group create -n rg-aci -l eastus

#### Criar container instance
az container create -g rg-aci -n nome_container --image nome_image:tag_image --ports 80 --dns-name-label nome_dns_unico --location nome_location 

#### Criar um container instance baseado no yaml
az container create --resource-group rg-aci --file nome_arquivo.yaml

#### Criar um container instance com volume compartilhado
az container create --resource-group rg-aci --name nome_container --image nome_image:tag_image --dns-name-label nome_dns --ports 80 --azure-file-volume-account-name nome_storage_account --azure-file-volume-account-key access_key --azure-file-volume-share-name nome_file_shared --azure-file-volume-mount-path /aci/logs/

#### Visualizar container instance
az container show --resource-group rg-aci --name nome_container --query "{FQDN:ipAddress.fqdn,ProvisioningState:provisioningState}" --out table

#### Informar política de reinicio no container instance
az container create --resource-group rg-aci --name nome_container --image nome_image:tag_image --restart-policy (Always, Never ou OnFailure)