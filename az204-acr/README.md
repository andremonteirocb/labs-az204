## Azure Container Registry

#### Criar azure container registry (ACR)
az acr create --name nome_container_registry --resource-group nome_resource_group --sku basic

#### Gerar imagem através do dockerfile
az acr build --image nome_image:tag_image --registry nome_container_registry .
az acr build --image nome_image:tag_image --registry nome_container_registry --file Dockerfile .

#### Visualizar as imagens existente no ACR
az acr repository list --name nome_container_registry --output table

#### Visualizar as tags existentes para uma imagem no ACR
az acr repository show-tags --name nome_container_registry --repository nome_image --output table

#### Executar uma determinada imagem do ACR
az acr run --registry nome_container_registry--cmd '$Registry/nome_image:tag_image' /dev/null

#### Exibir usuário e senha para autenticação
az acr credential show --name nome_container_registry --resource-group nome_resource_group --query [username,passwords[?name=='password'].value] --output tsv