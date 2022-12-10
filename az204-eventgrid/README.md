#### Azure Event Grid

#### Criar event grid
az eventgrid topic create --name topic_name --resource-group nomeresourcegroup --location nome_location --public-network-access enabled --input-schema eventgridschema  

#### Setar quantidade de tentativas para falha
az eventgrid event-subscription create -g nomeresourcegroup --topic-name topic_name --name subscription_name --endpoint subscription_name_endpoint_url --max-delivery-attempts numero_tentativas

#### Criar webapp de imagem do docker hub (webhook subscription)
Aba Docker -> Docker Hub -> Public -> Image microsoftlearning/azure-event-grid-viewer:latest

#### Criar subscription apontando para webapp criado acima
#### Copiar a url e key do Event Grid Topic e executar
