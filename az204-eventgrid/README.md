#### Azure Event Grid

#### Criar fila (queue)
az eventgrid topic create --name topic_name --resource-group nomeresourcegroup --location nome_location --public-network-access enabled --input-schema eventgridschema  

#### Criar tópico (topic)
az servicebus topic create --resource-group nome_resource_group --namespace-name nome_namespace --name nome_topic --max-message-size-in-kilobytes 102400

#### Setar quantidade de tentativas para falha
az eventgrid event-subscription create -g nomeresourcegroup --topic-name topic_name --name subscription_name --endpoint subscription_name_endpoint_url --max-delivery-attempts numero_tentativas

#### Registre o provedor de recursos do Event Grid
az provider register --namespace Microsoft.EventGrid

#### Verificar o status do provedor de recursos
az provider show --namespace Microsoft.EventGrid --query "registrationState"

#### Criar webapp via Azure Resource Manager (ARM)
az deployment group create --resource-group nomeresourcegroup --template-uri "https://raw.githubusercontent.com/Azure-Samples/azure-event-grid-viewer/main/azuredeploy.json" --parameters siteName=$mySiteName hostingPlanName=viewerhost

#### Criar webapp de imagem do docker hub
Aba Docker -> Docker Hub -> Public -> Image microsoftlearning/azure-event-grid-viewer:latest

#### Assinar um tópico personalizado
az eventgrid event-subscription create --source-resource-id "/subscriptions/41d086cb-db3c-4b2e-a10e-f56c9274002d/resourceGroups/az204-evgrid-rg/providers/Microsoft.EventGrid/topics/az204-egtopic-alsam" --name az204ViewerSub --endpoint https://testeeventgridcli.azurewebsites.net/api/updates

#### Recuperar url do tópico
az eventgrid topic show --name az204-egtopic-alsam -g az204-evgrid-rg --query "endpoint" --output tsv

#### Recuperar key do tópico
az eventgrid topic key list --name az204-egtopic-alsam -g az204-evgrid-rg --query "key1" --output tsv

#### Executar aplicação para enviar os eventos
