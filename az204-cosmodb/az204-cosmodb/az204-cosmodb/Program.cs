using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Cosmo Db exercise\n");

string endpoint = "endpoint_key";
string key = "key";

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton(new CosmosClient(endpoint, key));
        services.AddScoped<ICosmoDbService, CosmoDbService>();
    })
    .Build();

await Run(host.Services);

static async Task Run(IServiceProvider services)
{
    string databaseId = "az204Database";
    string containerId = "az204Container";
    string partitionKey = "/id";

    using IServiceScope serviceScope = services.CreateScope();
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
}

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

public class SalesOrder
{
    public string id { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
}