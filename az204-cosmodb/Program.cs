using az204_cosmodb.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Cosmo Db exercise\n");

string endpoint = "endpoint_key";
string key = "key";

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var cosmosClient = new CosmosClient(endpoint, key,
            new CosmosClientOptions()
            {
                ApplicationRegion = Regions.EastUS
            }
        );

        // var cosmosClientBuilder = new CosmosClientBuilder(endpoint, key)
        //     .WithApplicationRegion(Regions.EastUS);
        // cosmosClient = cosmosClientBuilder.Build();

        services.AddSingleton(cosmosClient);
        services.AddScoped<ICosmoDbService, CosmoDbService>();
    })
    .Build();

string databaseId = "az204Database";
string containerId = "az204Container";
string partitionKey = "/id";

using IServiceScope serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;
var cosmoDbService = provider.GetRequiredService<ICosmoDbService>();

var database = await cosmoDbService.CreateDataBase(databaseId);
//var database = cosmoDbService.GetDataBase(databaseId);
//await cosmoDbService.DeleteDataBase();

var container = await cosmoDbService.CreateContainer(containerId, partitionKey);
//var container = cosmoDbService.GetContainer(containerId);
//await cosmoDbService.DeleteContainer(containerId);

string id = new Random().Next(0, 1000).ToString();
await cosmoDbService.CreateItem(containerId, new SalesOrder { id = id, firstname = $"André {id}", lastname = $"Monteiro {id}" });
var result = await cosmoDbService.GetItem<SalesOrder>(containerId, id);

//var query = new QueryDefinition(
//    "select * from sales s where s.AccountNumber = @AccountInput ")
//.WithParameter("@AccountInput", "Account1");

//FeedIterator<SalesOrder> resultSet = container.GetItemQueryIterator<SalesOrder>(
//    query,
//    requestOptions: new QueryRequestOptions()
//    {
//        PartitionKey = new PartitionKey("Account1"),
//        MaxItemCount = 1
//    });

host.Run();

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();