#### Azure Event Hub

#### Criar event hub
az eventhubs eventhub create --resource-group nome_resource_group --namespace-name nome_namespace --name nome_event_hub --message-retention 4 --partition-count 15