## Durable Function

#### Add Pacakages
dotnet add package Microsoft.Azure.WebJobs.Extensions.DurableTask --version 2.8.1
dotnet add package Flurl.Http --version 3.2.4


#### Configure storage account (local.settings.json)
AzureWebJobsStorage: 

#### Run
http://localhost:7071/api/Searching

Checa resultado no endereço 
statusQueryGetUri: 