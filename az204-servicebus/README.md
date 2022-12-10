#### Azure Service Bus

#### Criar espaço de trabalho
az servicebus namespace create --resource-group nome_resource_group --name nome_namespace --location nome_location --tags tag1=value1 tag2=value2 --sku Basic

#### Criar fila
az servicebus queue create --resource-group nome_resource_group --namespace-name nome_namespace --name nome_fila

#### Alterar service-bus-connection-string pelo shared key do namespace
<serviceBus-connection-string>
