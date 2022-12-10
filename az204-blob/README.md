## Azure Blob

#### Criar account
az storage account create -n storage_account_name -g nome_resource_group -l nome_location --sku Standard_LRS --public-network-access Enabled --tags Environment=lab-azcopy Location=eastus --min-tls-version TLS1_2

#### Criar container
az storage container create --account-name storage_account_name --name nome_container --auth-mode Login (key/login) --public-access off (off/on)

#### Criar policy storage para um container
az storage container policy create --name nome_politica --container-name nome_container --start 2022-12-06T16:51Z --expiry 2022-12-07T16:51Z --permissions nome_permissao --account-key chave_conta --account-name storage_account_name  

#### Listar policy do container
az storage container policy list --account-name storage_account_name --container-name nome_container -o table

#### Deletar policy do container
az storage container policy delete --name nome_politica --container-name nome_container --account-key chave_conta --account-name storage_account_name 

#### Obter identificador da storage account
az storage account show --name storage_account_name --resource-group nome_resource_group --query id -o tsv

#### Adicionar permissão de Blob Data Contributor
az role assignment create --assignee user_name --role "Storage Blob Data Contributor" --scope identificador_storage_account_obtido_acima

#### Obter key da storage account
az storage account keys list --account-name storage_account_name --resource-group nome_resource_group --query "[0].value" -o tsv

#### Obter connectionstrings da storage account
az storage account show-connection-string --name storage_account_name --resource-group nome_resource_group 

#### Obter generate_sas
az storage container generate-sas --name nome_container --account-name storage_account_name --account-key key_obtida_comando_acima --expiry '2022-12-07T23:43:16Z' --https-only --permissions acdefilmrtwxy -o tsv
--permissions (a)dd (c)reate (d)elete (e)xecute (f)ilter_by_tags (i)set_immutability_policy (l)ist (m)ove (r)ead (t)ag (w)rite (x)delete_previous_version (y)permanent_delete