## AzCopy

#### Criar o grupo de recurso
az group create -n rg-arm -l eastus

#### Subir todos os recursos sem arquivo de parametros (projeto az204-arm)
az deployment group create --resource-group rg-arm --template-file storagedeploy.json

#### Obter identificador da storage account
az storage account show --name azcopyalsam --resource-group rg-arm --query id -o tsv

#### Adicionar permissão de Blob Data Contributor
az role assignment create --assignee user_name --role "Storage Blob Data Contributor" --scope identificador_storage_account_obtido_acima

#### Obter key da storage account
az storage account keys list --account-name storage_account_name --resource-group rg-arm --query "[0].value" -o tsv

#### Obter generate_sas
az storage container generate-sas --name nome_container --account-name storage_account_name --account-key key_obtida_comando_acima --expiry '2022-12-07T23:43:16Z' --https-only --permissions acdefilmrtwxy -o tsv

--permissions (a)dd (c)reate (d)elete (e)xecute (f)ilter_by_tags (i)set_immutability_policy (l)ist (m)ove (r)ead (t)ag (w)rite (x)delete_previous_version (y)permanent_delete

#### Efetuar login
azcopy login
azcopy login --tenant-id seu_tenant_id

#### Listar arquivos
azcopy list https://storage_account_name.blob.core.windows.net/nome_container 

#### Copiar todos os arquivos locais para azure
azcopy copy 'caminho_fisico_local\*' 'https://storage_account_name.blob.core.windows.net/nome_container?generate_sas'

#### Copiar todos os arquivos azure para local
azcopy copy 'https://storage_account_name.blob.core.windows.net/*?generate_sas' 'caminho_fisico_local' --recursive

#### Copiar um arquivo azure para local
azcopy copy 'https://storage_account_name.blob.core.windows.net/nome_arquivo_blob?generate_sas' 'caminho_fisico_local\nome_arquivo_desejado'

#### Remover arquivo da storage
azcopy remove 'https://storage_account_name.blob.core.windows.net/nome_arquivo_blob?generate_sas'