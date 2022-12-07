## Azure App Service

#### Criar o grupo de recurso
az group create -n rg-arm -l eastus

#### Criar service plan
az appservice plan create -n nome_service_plan -l nome_location -g rg-arm --sku F1 --is-linux

#### Criar app service
az webapp create -n nome_app_serve -g rg-arm -p nome_service_plan --runtime "DOTNETCORE:6.0"

#### Realizar deploy da aplicação com arquivo zip
az webapp deployment source config-zip --src ./nome_pacote.zip -g rg-arm -n nome_app_serve

#### Criar app service com azure container registry (ACR)
az webapp create --name nome_app_serve --plan nome_service_plan --resource-group rg-arm --deployment-container-image-name nome_container_registry/nome_image:tag_image

#### Adicionar configuração no azure container registry (ACR)
az webapp config container set --resource-group rg-arm --name nome_app_serve --docker-registry-server-url http://nome_container_registry.azurecr.io --docker-registry-server-user nome_usuario_acr