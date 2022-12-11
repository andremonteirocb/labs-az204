#### Azure Queue Storage

#### Criar o grupo de recurso
az group create --resource-group nome_resource_group  --location nome_location

#### Criar a storage account
az storage account create --name nome_storage_account --location nome_location --sku Standard_LRS --kind StorageV2 --resource-group nome_resource_group 

#### Criar a fila na storage account
az storage queue create --account-name nome_storage_account -n nome_fila --metadata key1=value1 key2=value2

#### Visualizar a connection string da storage account
az storage account show-connection-string --name nome_storage_account --resource-group nome_resource_group 