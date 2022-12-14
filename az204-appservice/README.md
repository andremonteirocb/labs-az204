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
az webapp create --name nome_app_serve --plan nome_service_plan --resource-group rg-arm --deployment-container-image-name nome_container_registry.azurecr.io/nome_image:tag_image

#### Adicionar configuração no azure container registry (ACR)
az webapp config container set --resource-group rg-arm --name nome_app_serve --docker-registry-server-url http://nome_container_registry.azurecr.io --docker-registry-server-user nome_usuario_acr
--docker-registry-server-password senha_usuario_acr

#### Ativar logs
az webapp log config --name nome_app_serve --resource-group rg-arm --docker-container-logging filesystem

#### Visualizar logs
az webapp log tail --name nome_app_serve --resource-group rg-arm

#### Anotações
- <strong>Always On:</strong> Evita que seu aplicativo fique ocioso devido à inatividade; <br />
- <strong>ARR affinity:</strong> Melhore o desempenho do seu aplicativo sem estado desativando o Affinity Cookie. 
Os aplicativos com estado devem manter essa configuração ativada para compatibilidade; <br />
- <strong>WEBSITE_RUN_FROM_PACKAGE:</strong> WEBSITE_RUN_FROM_PACKAGE="1" permite que você execute seu aplicativo de um pacote local para seu aplicativo
