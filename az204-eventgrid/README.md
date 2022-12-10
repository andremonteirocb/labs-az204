#### Azure Event Grid

#### Criar fila (queue)
az eventgrid topic create --name topic_name --resource-group nomeresourcegroup --location nome_location --public-network-access enabled --input-schema eventgridschema  

#### Criar tópico (topic)
az servicebus topic create --resource-group nome_resource_group --namespace-name nome_namespace --name nome_topic --max-message-size-in-kilobytes 102400

#### Setar quantidade de tentativas para falha
az eventgrid event-subscription create -g nomeresourcegroup --topic-name topic_name --name subscription_name --endpoint subscription_name_endpoint_url --max-delivery-attempts numero_tentativas

#### Criar webapp de imagem do docker hub (webhook subscription)
Aba Docker -> Docker Hub -> Public -> Image microsoftlearning/azure-event-grid-viewer:latest

#### Criar subscription apontando para webapp criado acima
#### Copiar a url e key do Event Grid Topic e executar
