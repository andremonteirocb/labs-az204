## Azure Resource Manager

#### Criar o grupo de recurso
az group create -n rg-arm -l eastus

#### Subir todos os recursos
az deployment group create --resource-group rg-arm --template-file azuredeploy.json --parameters azuredeploy.parameters.json