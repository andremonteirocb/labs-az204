## Azure Resource Manager

#### Criar o grupo de recurso
az group create -n rg-arm -l eastus

#### Subir todos os recursos com arquivo de parametros
az deployment group create --resource-group rg-arm --template-file azuredeploy.json --parameters azuredeploy.parameters.json

#### Subir todos os recursos sem arquivo de parametros
az deployment group create --resource-group rg-arm --template-file storagedeploy.json

#### Validar template
az deployment group validate -g rg-arm --template-file storagedeploy.json