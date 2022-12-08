## Azure Key Vault

#### Criar o grupo de recurso
az group create --resource-group azkeyvault-rg --location eastus

#### Criar storage account
az storage account create --name storageaccountalsam --resource-group azkeyvault-rg --location eastus --sku Standard_LRS --min-tls-version TLS1_2

#### Criar container na storage chamado drop (privado)

#### Realizar o upload do arquivo records.json

#### Obter key da storage account
az storage account keys list --account-name storageaccountalsam --resource-group azkeyvault-rg --query "[0].value" -o tsv

#### Obter connectionstrings da storage account
az storage account show-connection-string --name storageaccountalsam --resource-group azkeyvault-rg

#### Criar um keyvault
az keyvault create --resource-group azkeyvault-rg --name azkeyvault-alsam --sku standard --public-network-access Enabled --retention-days 90

#### Criar uma secret no cofre
az keyvault secret set --vault-name azkeyvault-alsam --name StorageConnectionString --value DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=storageaccountalsam;AccountKey=aWfQ8rRx5F6TmYmWJBc10LOLFCVHHjl81xyuWzeXnBPbT98DTcesdmQ2qZJ1sROK64nIdn+K+5Co+AStSaFIAw==;BlobEndpoint=https://storageaccountalsam.blob.core.windows.net/;FileEndpoint=https://storageaccountalsam.file.core.windows.net/;QueueEndpoint=https://storageaccountalsam.queue.core.windows.net/;TableEndpoint=https://storageaccountalsam.table.core.windows.net/

#### Visualizar uma secret no cofre
az keyvault secret show --name StorageConnectionString --vault-name azkeyvault-alsam

#### Criar uma app service function
az functionapp create --name GetBlobKeyVault --resource-group azkeyvault-rg --storage-account storageaccountalsam --consumption-plan-location eastus --runtime dotnet --os-type Windows --functions-version 4

#### Ativar identidade na function
Function App -> Settings -> Identity -> Status (On)

#### Associar policy a identidade da function
Key Vault -> Access Policies -> Create -> Secret permissions -> Get -> Next -> Function App -> Create

#### Adicionar configuração referente ao keyvault na function app
Function App -> Configuration -> Application settings -> New application setting
Name: StorageConnectionString
Value: @Microsoft.KeyVault(SecretUri=https://securevaultstudent.vault.azure.net/secrets/storagecredentials/17b41386df3e4191b92f089f5efb4cbf)