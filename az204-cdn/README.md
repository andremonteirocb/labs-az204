#### Azure Content Delivery Network (CDN)

#### Limpar cache dos arquivos
az cdn endpoint purge --content-paths '/css/*' '/js/app.js' --name nome_endpoint --profile-name nome_profile --resource-group nome_resource_group

#### Carregar arquivos no cache
az cdn endpoint load --content-paths '/img/*' '/js/module.js' --name nome_endpoint --profile-name nome_profile --resource-group nome_resource_group