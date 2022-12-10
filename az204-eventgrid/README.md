#### Azure Event Grid

#### Criar event grid
az eventgrid topic create --name topic_name --resource-group nomeresource-group --location nome_location --public-network-access enabled --input-schema eventgridschema  

#### Criar webapp de imagem do docker hub
Aba Docker -> Docker Hub -> Public -> Image microsoftlearning/azure-event-grid-viewer:latest

#### Criar subscription apontando para webapp criado acima
#### Copiar a url e key do Event Grid Topic e executar essa aplicação
