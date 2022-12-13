#### Azure Content Delivery Network (CDN)

#### Limpar cache dos arquivos
az cdn endpoint purge --content-paths '/css/*' '/js/app.js' --name ContosoEndpoint --profile-name DemoProfile --resource-group ExampleGroup

#### Carregar arquivos no cache
az cdn endpoint load --content-paths '/img/*' '/js/module.js' --name ContosoEndpoint --profile-name DemoProfile --resource-group ExampleGroup